using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using AutoMapper;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class AdvanceRequistionManager : IAdvanceRequisitionHeaderManager
    {
        private readonly IAdvanceRequisitionHeaderRepository _advanceRequisitionHeaderRepository;
        private readonly IAdvanceRequisitionDetailRepository _advanceRequisitionDetailRepository;
        private readonly IRequisitionFileManager _requisitionFileManager;
        private readonly IRequisitionHistoryHeaderManager _requisitionHistoryHeaderManager;
        private readonly IRequisitionHistoryDetailRepository _requisitionHistoryDetailManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IEmployeeManager _employeeManager;

        public AdvanceRequistionManager()
        {
            _advanceRequisitionHeaderRepository = new AdvanceRequisitionHeaderRepository();
            _advanceRequisitionDetailRepository = new AdvanceRequisitionDetailRepository();
            _requisitionFileManager = new RequisitionFileManager();
            _requisitionHistoryHeaderManager = new RequisitionHistoryManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionHistoryDetailManager = new RequisitionHistoryDetailRepository();
            _employeeManager = new EmployeeManager();
        }

        public AdvanceRequistionManager(IAdvanceRequisitionHeaderRepository advanceRequisitionHeaderRepository, IAdvanceRequisitionDetailRepository advanceRequisitionDetailRepository, IRequisitionFileManager requisitionFileManager, IRequisitionHistoryHeaderManager requisitionHistoryHeaderManager, IRequisitionHistoryDetailRepository requisitionHistoryDetailManager, IAdvanceRequisitionCategoryManager advanceRequisitionCategoryManager, IEmployeeManager employeeManager)
        {
            _advanceRequisitionHeaderRepository = advanceRequisitionHeaderRepository;
            _advanceRequisitionDetailRepository = advanceRequisitionDetailRepository;
            _requisitionFileManager = requisitionFileManager;
            _requisitionHistoryHeaderManager = requisitionHistoryHeaderManager;
            _requisitionHistoryDetailManager = requisitionHistoryDetailManager;
            _advanceRequisitionCategoryManager = advanceRequisitionCategoryManager;
            _employeeManager = employeeManager;
        }

        public bool Insert(AdvanceRequisitionHeader entity)
        {
            entity.CreatedOn = DateTime.Now;
            if (entity.RequesterDepartmentId == null)
                throw new BllException("Department is not assigned for requester. Please contact to admin.");
            if (entity.RequesterRankId == null)
                throw new BllException("Designation is not assigned for requester. Please contact to admin.");
            if (entity.RequesterSupervisorId == null)
                throw new BllException("Supervisor is not assigned for requester. Please contact to admin.");
            entity.SerialNo = GetSerialNo(entity.CreatedOn);
            entity.GenerateRequisitionNo();

            entity.RequisitionHistoryHeaders = new List<RequisitionHistoryHeader>() { entity.GenerateRequisitionHistoryHeaderFromRequisition(entity, HistoryModeEnum.Create) };
            bool isRequisitionInserted = _advanceRequisitionHeaderRepository.Insert(entity);

            return isRequisitionInserted;
        }

        public bool Insert(ICollection<AdvanceRequisitionHeader> entityCollection)
        {
            return _advanceRequisitionHeaderRepository.Insert(entityCollection);
        }

        public bool Edit(AdvanceRequisitionHeader entity)
        {
            var existingHeader = GetById(entity.Id);

            var existingDetails = existingHeader.AdvanceRequisitionDetails.ToList();

            var existingFiles = existingHeader.RequisitionFiles;

            var updatedDetails = entity.AdvanceRequisitionDetails.ToList();

            var updatedFiles = entity.RequisitionFiles;

            var updateableItems = updatedDetails.Where(c => c.Id > 0).ToList();

            var updateableFiles = updatedFiles.Where(c => c.Id > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            var fileIdList = updateableFiles.Select(c => c.Id).ToList();

            var deleteableItems = existingDetails.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedDetails.Where(c => c.Id == 0).ToList();

            var addeableFiles = updatedFiles.Where(c => c.Id == 0).ToList();

            using (var ts = new TransactionScope())
            {
                entity.AdvanceRequisitionDetails = null;
                entity.RequisitionFiles = null;
                var isUpdated = _advanceRequisitionHeaderRepository.Edit(entity);
                HistoryModeEnum historyMode = HistoryModeEnum.Edit;
                if (entity.IsDeleted)
                {
                    historyMode = HistoryModeEnum.Delete;
                }
                var historyHeader = entity.GenerateRequisitionHistoryHeaderFromRequisition(entity, historyMode);
                historyHeader.LastModifiedOn = DateTime.Now;
                historyHeader.AdvanceRequisitionHeaderId = entity.Id;
                var isHistoryHeaderAdded = _requisitionHistoryHeaderManager.Insert(historyHeader);

                int deleteCount = 0;
                bool isDeleted = false;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var advanceRequisitionDetail in deleteableItems)
                    {
                        isDeleted = _advanceRequisitionDetailRepository.Delete(advanceRequisitionDetail);
                        _requisitionHistoryDetailManager.Insert(advanceRequisitionDetail.GenerateRequisitionHistoryDetail(advanceRequisitionDetail, historyHeader.Id, HistoryModeEnum.Delete));

                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    addeableItems.ForEach(c => { c.AdvanceRequisitionHeaderId = entity.Id; });
                    _advanceRequisitionDetailRepository.Insert(addeableItems);
                    foreach (var advanceRequisitionDetail in addeableItems)
                    {
                        _requisitionHistoryDetailManager.Insert(advanceRequisitionDetail.GenerateRequisitionHistoryDetail(advanceRequisitionDetail, historyHeader.Id, HistoryModeEnum.Create));
                    }
                }

                if (addeableFiles != null && addeableFiles.Any())
                {
                    addeableFiles.ForEach(c => { c.AdvanceRequisitionHeaderId = entity.Id; });
                    _requisitionFileManager.Insert(addeableFiles);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var advanceRequisitionDetail in updateableItems)
                    {
                        _advanceRequisitionDetailRepository.Edit(advanceRequisitionDetail);
                        _requisitionHistoryDetailManager.Insert(advanceRequisitionDetail.GenerateRequisitionHistoryDetail(advanceRequisitionDetail, historyHeader.Id, HistoryModeEnum.Edit));
                    }
                }

                if (updateableFiles != null && updateableFiles.Any())
                {
                    foreach (var item in updateableFiles)
                    {
                        _requisitionFileManager.Edit(item);
                    }
                }

                ts.Complete();

                return isUpdated || isDeleted;
            }
        }

        public bool Delete(long id)
        {
            var advanceHeader = GetById(id);
            return _advanceRequisitionHeaderRepository.Delete(advanceHeader);
        }

        public AdvanceRequisitionHeader GetById(long id)
        {
            AdvanceRequisitionHeader header = _advanceRequisitionHeaderRepository.GetFirstOrDefaultBy(
                c => c.Id == id,
                c => c.AdvanceRequisitionDetails,
                //c => c.AdvanceRequisitionDetails.Select(d => d.AdvanceRequisitionHeader),
                c => c.RequisitionApprovalTickets,
                c => c.AdvanceRequisitionStatus,
                c => c.AdvanceCategory,
                c => c.AdvanceExpenseHeader,
                c => c.RequisitionApprovalTickets.Select(d => d.ApprovalLevel),
                c => c.RequisitionApprovalTickets.Select(d => d.ApprovalStatus),
                c => c.RequisitionApprovalTickets.Select(d => d.ApprovalPanel),
                c => c.RequisitionApprovalTickets.Select(d => d.ApprovalLevel.ApprovalLevelMembers),
                c => c.RequisitionApprovalTickets.Select(d => d.ApprovalTrackers),
                c => c.RequisitionHistoryHeaders.Select(d => d.HistoryMode),
                c => c.RequisitionApprovalTickets.Select(d => d.DestinationUserForTickets)
                );
            if (header != null)
                header.RequisitionFiles = _requisitionFileManager.GetByHeaderId(id);
            return header;
        }

        public ICollection<AdvanceRequisitionHeader> GetAll()
        {
            ICollection<AdvanceRequisitionHeader> headers = _advanceRequisitionHeaderRepository.GetAll(
                c => c.AdvanceRequisitionDetails,
                c => c.AdvanceExpenseHeader,
                //c => c.AdvanceRequisitionDetails.Select(d => d.AdvanceRequisitionHeader),
                c => c.RequisitionApprovalTickets,
                c => c.AdvanceRequisitionDetails,
                c => c.RequisitionApprovalTickets.Select(d => d.DestinationUserForTickets)
                );
            foreach (AdvanceRequisitionHeader advanceRequisitionHeader in headers)
            {
                advanceRequisitionHeader.RequisitionFiles =
                    _requisitionFileManager.GetByHeaderId(advanceRequisitionHeader.Id);
            }

            return headers;
        }

        public ICollection<AdvanceRequisitionHeader> GetByEmployeeUserName(string username)
        {
            ICollection<AdvanceRequisitionHeader> headers = _advanceRequisitionHeaderRepository.Get(
                c => c.RequesterUserName.Equals(username),
                c => c.AdvanceCategory,
                c => c.AdvanceRequisitionDetails,
                c => c.AdvanceExpenseHeader,
                //c => c.AdvanceRequisitionDetails.Select(d => d.AdvanceRequisitionHeader),
                c => c.AdvanceRequisitionStatus,
                c => c.RequisitionApprovalTickets,
                c => c.RequisitionApprovalTickets.Select(d => d.DestinationUserForTickets),
                c => c.RequisitionFiles
                ).ToList();
            foreach (AdvanceRequisitionHeader advanceRequisitionHeader in headers)
            {
                advanceRequisitionHeader.RequisitionFiles =
                    _requisitionFileManager.GetByHeaderId(advanceRequisitionHeader.Id);
            }

            return headers;
        }

        public ICollection<AdvanceRequisitionHeader> GetByCreatedUser(string username)
        {
            ICollection<AdvanceRequisitionHeader> headers = _advanceRequisitionHeaderRepository.Get(
                c => c.CreatedBy.Equals(username),
                c => c.AdvanceCategory,
                c => c.AdvanceRequisitionDetails,
                c => c.AdvanceExpenseHeader,
                //c => c.AdvanceRequisitionDetails.Select(d => d.AdvanceRequisitionHeader),
                c => c.AdvanceRequisitionStatus,
                c => c.RequisitionApprovalTickets,
                c => c.RequisitionApprovalTickets.Select(d => d.DestinationUserForTickets),
                c => c.RequisitionFiles
                ).ToList();
            foreach (AdvanceRequisitionHeader advanceRequisitionHeader in headers)
            {
                advanceRequisitionHeader.RequisitionFiles =
                    _requisitionFileManager.GetByHeaderId(advanceRequisitionHeader.Id);
            }

            return headers;
        }

        public ICollection<AdvanceRequisitionSearchCriteriaVM> GetBySearchCriteria(AdvanceRequisitionSearchCriteria criteria)
        {
            return _advanceRequisitionHeaderRepository.GetBySearchCriteria(criteria);
        }

        public bool IsSourceofFundVerifiedForRequisition(long requisitionId)
        {
            bool isVerified = false;
            isVerified =
                _advanceRequisitionHeaderRepository.Get(c => c.Id == requisitionId && c.IsSourceOfFundVerified == true)
                    .Any();
            return isVerified;
        }

        public bool PayRequisition(long requisitionId, string paidBy, DateTime paidOn)
        {
            bool isPaid = false;
            var requisition = GetById(requisitionId);
            if (requisition != null)
            {
                requisition.PaidBy = paidBy;
                requisition.AdvanceIssueDate = paidOn;
                requisition.IsPaid = true;

                isPaid = _advanceRequisitionHeaderRepository.Edit(requisition);
            }
            return isPaid;
        }

        public int GetSerialNo(DateTime createdOn)
        {
            var maxData = _advanceRequisitionHeaderRepository.Get(
                    c => (DbFunctions.TruncateTime(c.CreatedOn).Value.Year ==
                        DbFunctions.TruncateTime(createdOn).Value.Year) &&
                        (DbFunctions.TruncateTime(c.CreatedOn).Value.Month ==
                        DbFunctions.TruncateTime(createdOn).Value.Month)).Select(c => c.SerialNo);

            var max = (!maxData.Any()) ? 0 : maxData.Max();
            return max + 1;
        }

        public List<string> GetRecipientName(AdvanceRequisitionHeader header)
        {
            ICollection<AdvanceRequisitionDetail> details = header.AdvanceRequisitionDetails;
            ICollection<string> recipientList = new List<string>();
            foreach (AdvanceRequisitionDetail detail in details)
            {
                recipientList.Add(!detail.IsThirdPartyReceipient
                    ? _employeeManager.GetByUserName(header.RequesterUserName).FullName
                    : detail.ReceipientOrPayeeName);
            }
            return recipientList.Distinct().ToList();
        }

        public bool RequisitionPayReceived(long requisitionId, string receivedBy, DateTime receivedOn)
        {
            bool isReceived = false;
            var requisition = GetById(requisitionId);
            if (requisition != null)
            {
                requisition.ReceivedBy = receivedBy;
                requisition.ReceivedOn = receivedOn;
                requisition.IsReceived = true;

                isReceived = _advanceRequisitionHeaderRepository.Edit(requisition);
            }
            return isReceived;
        }
    }
}
