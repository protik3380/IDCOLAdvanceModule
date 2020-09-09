using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class RequisitionVoucherHeaderManager : IRequisitionVoucherHeaderManager
    {
        private readonly IRequisitionVoucherHeaderRepository _requisitionVoucherHeaderRepository;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvanceRequisitionDetailRepository _advanceRequisitionDetailRepository;
        private readonly IRequisitionVoucherDetailRepository _requisitionVoucherDetailRepository;

        public RequisitionVoucherHeaderManager()
        {
            _requisitionVoucherHeaderRepository = new RequisitionVoucherHeaderRepository();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceRequisitionDetailRepository = new AdvanceRequisitionDetailRepository();
            _requisitionVoucherDetailRepository = new RequisitionVoucherDetailRepository();
        }

        public RequisitionVoucherHeaderManager(IRequisitionVoucherHeaderRepository requisitionVoucherHeaderRepository, IAdvanceRequisitionHeaderManager advanceRequisitionHeaderManager, IAdvanceRequisitionDetailRepository advanceRequisitionDetailRepository, IRequisitionVoucherDetailRepository requisitionVoucherDetailRepository)
        {
            _requisitionVoucherHeaderRepository = requisitionVoucherHeaderRepository;
            _advanceRequisitionHeaderManager = advanceRequisitionHeaderManager;
            _advanceRequisitionDetailRepository = advanceRequisitionDetailRepository;
            _requisitionVoucherDetailRepository = requisitionVoucherDetailRepository;
        }

        public bool Insert(RequisitionVoucherHeader entity)
        {
            entity.VoucherStatusId = Convert.ToInt64(VoucherStatusEnum.Draft);
            List<RequisitionVoucherDetail> voucherDetailList = entity.RequisitionVoucherDetails.ToList();
            voucherDetailList.ForEach(c =>
            {
                if (c.RequisitionDetails != null)
                {
                    foreach (var d in c.RequisitionDetails.ToList())
                    {
                        d.RequisitionVoucherDetail = c;
                    }
                }
            });

            var allRequisitionDeails = new List<AdvanceRequisitionDetail>();
            foreach (RequisitionVoucherDetail c in voucherDetailList)
            {
                if (c.RequisitionDetails != null && c.RequisitionDetails.Any())
                {
                    allRequisitionDeails.AddRange(c.RequisitionDetails);
                }

                c.RequisitionDetails = null;
            }

            using (var ts = new TransactionScope())
            {
                bool isInserted = _requisitionVoucherHeaderRepository.Insert(entity);
                bool isRequistionDetailUpdated = false;

                if (allRequisitionDeails.Any())
                {
                    int successCount = 0;
                    bool isSuccess;
                    foreach (var detail in allRequisitionDeails)
                    {
                        if (detail != null)
                        {
                            detail.RequisitionVoucherDetailId = detail.RequisitionVoucherDetail.Id;
                            isSuccess = _advanceRequisitionDetailRepository.Edit(detail);
                            if (isSuccess)
                            {
                                successCount++;
                            }
                        }
                    }
                    isRequistionDetailUpdated = allRequisitionDeails.Count() == successCount;
                }

                ts.Complete();
                return isInserted && isRequistionDetailUpdated;
            }
        }

        public bool Insert(ICollection<RequisitionVoucherHeader> entityCollection)
        {
            return _requisitionVoucherHeaderRepository.Insert(entityCollection);
        }

        public bool Edit(RequisitionVoucherHeader entity)
        {
            var existingHeader = GetById(entity.Id);

            var existingDetails = existingHeader.RequisitionVoucherDetails.ToList();

            var updatedDetails = entity.RequisitionVoucherDetails.ToList();

            var updateableItems = updatedDetails.Where(c => c.Id > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();
            var deleteableItems = existingDetails.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedDetails.Where(c => c.Id == 0).ToList();


            using (var ts = new TransactionScope())
            {
                entity.RequisitionVoucherDetails = null;
                var isHeaderUpdated = _requisitionVoucherHeaderRepository.Edit(entity);

                int deleteCount = 0, updateCount = 0;
                bool isDeleted = false, isUpdated = false, isAdded = false;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var requisitionVoucherDetail in deleteableItems)
                    {
                        foreach (AdvanceRequisitionDetail requisitionDetail in requisitionVoucherDetail.RequisitionDetails)
                        {
                            requisitionDetail.RequisitionVoucherDetailId = null;
                            _advanceRequisitionDetailRepository.Edit(requisitionDetail);
                        }
                        isDeleted = _requisitionVoucherDetailRepository.Delete(requisitionVoucherDetail);
                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    addeableItems.ForEach(c => { c.VoucherHeaderId = entity.Id; });
                    isAdded = _requisitionVoucherDetailRepository.Insert(addeableItems);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var item in updateableItems)
                    {
                        foreach (AdvanceRequisitionDetail requisitionDetail in item.RequisitionDetails)
                        {
                            requisitionDetail.RequisitionVoucherDetailId = item.Id;
                            _advanceRequisitionDetailRepository.Edit(requisitionDetail);
                        }
                        isUpdated = _requisitionVoucherDetailRepository.Edit(item);
                        if (isUpdated)
                        {
                            updateCount++;
                        }
                    }
                    isUpdated = updateCount == (updateableItems == null ? 0 : updateableItems.Count());
                }

                ts.Complete();

                return isUpdated || isDeleted || isAdded || isHeaderUpdated;
            }
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _requisitionVoucherHeaderRepository.Delete(entity);
        }

        public RequisitionVoucherHeader GetById(long id)
        {
            return _requisitionVoucherHeaderRepository.GetFirstOrDefaultBy(c => c.Id == id,
                c => c.RequisitionVoucherDetails,
                c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails),
                c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails.Select(e => e.AdvanceRequisitionHeader)),
                c => c.RequisitionHeader,
                c => c.VoucherStatus);
        }

        public ICollection<RequisitionVoucherHeader> GetAll()
        {
            return _requisitionVoucherHeaderRepository.GetAll(c => c.RequisitionHeader,
                        c => c.RequisitionVoucherDetails,
                        c => c.RequisitionHeader,
                        c => c.VoucherStatus,
                        c => c.VoucherStatus,
                        c => c.RequisitionHeader.AdvanceRequisitionDetails,
                        c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails),
                        c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails.Select(e => e.AdvanceRequisitionHeader))
                        );
        }

        public string GetVoucherStatusByHeaderIdAndRecipientName(long headerId, string recipient)
        {
            RequisitionVoucherHeader voucherHeader = GetByHeaderIdAndRecipientName(headerId, recipient);
            return voucherHeader == null ? "Not Created" : voucherHeader.VoucherStatus.Name;
        }

        public RequisitionVoucherHeader GetByHeaderIdAndRecipientName(long headerId, string recipient)
        {
            RequisitionVoucherHeader voucherHeader =
                _requisitionVoucherHeaderRepository.GetFirstOrDefaultBy(
                    c => c.RequisitionHeaderId == headerId && c.RecipientName.Equals(recipient),
                    c => c.VoucherStatus,
                    c => c.RequisitionHeader,
                    c => c.RequisitionVoucherDetails,
                    c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails),
                    c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails.Select(e => e.AdvanceRequisitionHeader)));
            return voucherHeader;
        }

        public ICollection<RequisitionVoucherHeader> GetAllDraftVoucher()
        {
            return _requisitionVoucherHeaderRepository.Get(c => c.VoucherStatusId == (long)VoucherStatusEnum.Draft,
                c => c.RequisitionHeader,
                c => c.RequisitionVoucherDetails,
                c => c.RequisitionHeader,
                c => c.VoucherStatus,
                c => c.VoucherStatus,
                c => c.RequisitionHeader.AdvanceRequisitionDetails,
                c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails),
                c =>
                    c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails.Select(e => e.AdvanceRequisitionHeader)));
        }

        public ICollection<RequisitionVoucherHeader> GetAllSentVoucher()
        {
            return _requisitionVoucherHeaderRepository.Get(c => c.VoucherStatusId == (long)VoucherStatusEnum.Sent
                && (c.SentDate != null && DbFunctions.DiffDays(c.SentDate, DateTime.Now) <= Utility.Utility.TimeDuration)
                ,
                c => c.RequisitionHeader,
                c => c.RequisitionVoucherDetails,
                c => c.RequisitionHeader,
                c => c.VoucherStatus,
                c => c.VoucherStatus,
                c => c.RequisitionHeader.AdvanceRequisitionDetails,
                c => c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails),
                c =>
                    c.RequisitionVoucherDetails.Select(d => d.RequisitionDetails.Select(e => e.AdvanceRequisitionHeader)));
        }
    }
}
