using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExpenseVoucherHeaderManager : IExpenseVoucherHeaderManager
    {
        private readonly IExpenseVoucherHeaderRepository _expenseVoucherHeaderRepository;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IAdvanceExpenseDetailRepository _advanceExpenseDetailRepository;
        private readonly IAdvanceRequisitionDetailRepository _advanceRequisitionDetailRepository;
        private readonly IExpenseVoucherDetailRepository _expenseVoucherDetailRepository;

        public ExpenseVoucherHeaderManager()
        {
            _expenseVoucherHeaderRepository = new ExpenseVoucherHeaderRepository();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceExpenseDetailRepository = new AdvanceExpenseDetailRepository();
            _advanceRequisitionDetailRepository = new AdvanceRequisitionDetailRepository();
            _expenseVoucherDetailRepository = new ExpenseVoucherDetailRepository();
        }

        public ExpenseVoucherHeaderManager(IExpenseVoucherHeaderRepository expenseVoucherHeaderRepository, IAdvanceExpenseHeaderManager advanceExpenseHeaderManager, IAdvanceExpenseDetailRepository advanceExpenseDetailRepository)
        {
            _expenseVoucherHeaderRepository = expenseVoucherHeaderRepository;
            _advanceExpenseHeaderManager = advanceExpenseHeaderManager;
            _advanceExpenseDetailRepository = advanceExpenseDetailRepository;
        }

        public bool Insert(ExpenseVoucherHeader entity)
        {
            entity.VoucherStatusId = Convert.ToInt64(VoucherStatusEnum.Draft);
            List<ExpenseVoucherDetail> voucherDetailList = entity.ExpenseVoucherDetails.ToList();
            voucherDetailList.ForEach(c =>
            {
                if (c.ExpenseDetails != null)
                {
                    foreach (var d in c.ExpenseDetails.ToList())
                    {
                        d.ExpenseVoucherDetail = c;
                    }
                }
            });

            var allRequisitionDeails = new List<AdvanceExpenseDetail>();
            foreach (ExpenseVoucherDetail c in voucherDetailList)
            {
                if (c.ExpenseDetails != null && c.ExpenseDetails.Any())
                {
                    allRequisitionDeails.AddRange(c.ExpenseDetails);
                }

                c.ExpenseDetails = null;
            }

            using (var ts = new TransactionScope())
            {
                bool isInserted = _expenseVoucherHeaderRepository.Insert(entity);
                bool isRequistionDetailUpdated = false;

                if (allRequisitionDeails.Any())
                {
                    int successCount = 0;
                    bool isSuccess;
                    foreach (var detail in allRequisitionDeails)
                    {
                        if (detail != null)
                        {
                            detail.ExpenseVoucherDetailId = detail.ExpenseVoucherDetail.Id;
                            isSuccess = _advanceExpenseDetailRepository.Edit(detail);
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

        public bool Insert(ICollection<ExpenseVoucherHeader> entityCollection)
        {
            return _expenseVoucherHeaderRepository.Insert(entityCollection);
        }

        public bool Edit(ExpenseVoucherHeader entity)
        {
            var existingHeader = GetById(entity.Id);

            var existingDetails = existingHeader.ExpenseVoucherDetails.ToList();

            var updatedDetails = entity.ExpenseVoucherDetails.ToList();

            var updateableItems = updatedDetails.Where(c => c.Id > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();
            var deleteableItems = existingDetails.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedDetails.Where(c => c.Id == 0).ToList();


            using (var ts = new TransactionScope())
            {
                entity.ExpenseVoucherDetails = null;
                var isHeaderUpdated = _expenseVoucherHeaderRepository.Edit(entity);

                int deleteCount = 0, updateCount = 0;
                bool isDeleted = false, isUpdated = false, isAdded = false;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var expenseVoucherDetail in deleteableItems)
                    {
                        foreach (var expenseDetail in expenseVoucherDetail.ExpenseDetails)
                        {
                            expenseDetail.ExpenseVoucherDetailId = null;
                            _advanceExpenseDetailRepository.Edit(expenseDetail);
                        }
                        isDeleted = _expenseVoucherDetailRepository.Delete(expenseVoucherDetail);
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
                    isAdded = _expenseVoucherDetailRepository.Insert(addeableItems);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var item in updateableItems)
                    {
                        foreach (var expenseDetail in item.ExpenseDetails)
                        {
                            expenseDetail.ExpenseVoucherDetailId = item.Id;
                            _advanceExpenseDetailRepository.Edit(expenseDetail);
                        }
                        isUpdated = _expenseVoucherDetailRepository.Edit(item);
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
            return _expenseVoucherHeaderRepository.Delete(entity);
        }

        public ExpenseVoucherHeader GetById(long id)
        {
            return _expenseVoucherHeaderRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.ExpenseHeader,
                        c => c.ExpenseVoucherDetails,
                        c => c.VoucherStatus,
                        c => c.ExpenseHeader.AdvanceExpenseDetails,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders.Select(d => d.AdvanceRequisitionDetails),
                        c => c.ExpenseVoucherDetails.Select(d => d.ExpenseDetails));
        }

        public ICollection<ExpenseVoucherHeader> GetAll()
        {
            return _expenseVoucherHeaderRepository.GetAll(c => c.ExpenseHeader,
                        c => c.ExpenseVoucherDetails,
                        c => c.VoucherStatus,
                        c => c.ExpenseHeader.AdvanceExpenseDetails,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders.Select(d => d.AdvanceRequisitionDetails),
                        c => c.ExpenseVoucherDetails.Select(d => d.ExpenseDetails));
        }

        public ICollection<ExpenseVoucherDetail> GetVoucherDetailsForRelatedRequisition(ICollection<AdvanceExpenseDetail> expenseDetails)
        {
            var requisitionDetailIds = expenseDetails.Select(c => c.AdvanceRequisitionDetailId).ToList();
            if (requisitionDetailIds == null && !requisitionDetailIds.Any())
            {
                return null;
            }

            var requistionDetailList = _advanceRequisitionDetailRepository.Get(c => requisitionDetailIds.Contains(c.Id), c => c.RequisitionVoucherDetail);

            List<ExpenseVoucherDetail> voucherDetails = new List<ExpenseVoucherDetail>();
            if (expenseDetails != null && expenseDetails.Any())
            {
                foreach (var expenseDetail in expenseDetails)
                {
                    if (expenseDetail != null)
                    {

                        var requisitionDetail =
                            requistionDetailList.FirstOrDefault(c => c.Id == expenseDetail.AdvanceRequisitionDetailId);

                        if (requisitionDetail != null)
                        {
                            var voucherDetail = requisitionDetail.RequisitionVoucherDetail;
                            if (voucherDetail == null)
                            {
                                continue;
                            }

                            var advanceAmount = requisitionDetail.AdvanceAmount;
                            ExpenseVoucherDetail detail = new ExpenseVoucherDetail();
                            detail.AccountCode = voucherDetail.AccountCode;
                            detail.CrAmount = advanceAmount;
                            detail.Description = ExpenseVoucherDetail.GetDescriptionForAdvanceAdjustment();
                            voucherDetails.Add(detail);
                        }
                    }
                }
            }

            return voucherDetails;
        }

        public string GetVoucherStatusByHeaderIdAndRecipientName(long headerId, string recipient)
        {
            ExpenseVoucherHeader voucherHeader = GetByHeaderIdAndRecipientName(headerId, recipient);
            return voucherHeader == null ? "Not Created" : voucherHeader.VoucherStatus.Name;
        }

        public ExpenseVoucherHeader GetByHeaderIdAndRecipientName(long headerId, string recipient)
        {
            ExpenseVoucherHeader voucherHeader =
                _expenseVoucherHeaderRepository.GetFirstOrDefaultBy(
                    c => c.ExpenseHeaderId == headerId && c.RecipientName.Equals(recipient),
                    c => c.VoucherStatus,
                    c => c.ExpenseHeader,
                    c => c.ExpenseVoucherDetails,
                    c => c.ExpenseVoucherDetails.Select(d => d.ExpenseDetails),
                    c => c.ExpenseVoucherDetails.Select(d => d.ExpenseDetails.Select(e => e.AdvanceExpenseHeader))
                    );
            return voucherHeader;
        }

        public ICollection<ExpenseVoucherHeader> GetAllDraftVoucher()
        {
            return _expenseVoucherHeaderRepository.Get(c => c.VoucherStatusId == (long)VoucherStatusEnum.Draft, c => c.ExpenseHeader,
                        c => c.ExpenseVoucherDetails,
                        c => c.VoucherStatus,
                        c => c.ExpenseHeader.AdvanceExpenseDetails,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders.Select(d => d.AdvanceRequisitionDetails),
                        c => c.ExpenseVoucherDetails.Select(d => d.ExpenseDetails));
        }

        public ICollection<ExpenseVoucherHeader> GetAllSentVoucher()
        {
            return _expenseVoucherHeaderRepository.Get(c => c.VoucherStatusId == (long)VoucherStatusEnum.Sent &&
                        (c.SentDate != null && DbFunctions.DiffDays(c.SentDate, DateTime.Now) <= Utility.Utility.TimeDuration),
                        c => c.ExpenseHeader,
                        c => c.ExpenseVoucherDetails,
                        c => c.VoucherStatus,
                        c => c.ExpenseHeader.AdvanceExpenseDetails,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders,
                        c => c.ExpenseHeader.AdvanceRequisitionHeaders.Select(d => d.AdvanceRequisitionDetails),
                        c => c.ExpenseVoucherDetails.Select(d => d.ExpenseDetails));
        }
    }
}
