using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class AdvanceExpenseManager : IAdvanceExpenseHeaderManager
    {
        private readonly IAdvanceExpenseHeaderRepository _advanceExpenseHeaderRepository;
        private readonly IAdvanceExpenseDetailRepository _advanceExpenseDetailRepository;
        private readonly ICurrencyConversionRateDetailManager _currencyConversionRateDetailManager;
        private readonly IExpenseFileManager _expenseFileManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IExpenseHistoryHeaderManager _expenseHistoryHeaderManager;
        private readonly IExpenseHistoryDetailRepository _expenseHistoryDetailRepository;
        private readonly IAdvance_VW_GetAdvanceExpenseRepository _advanceExpenseQueryRepository;

        public AdvanceExpenseManager()
        {
            _advanceExpenseHeaderRepository = new AdvanceExpenseHeaderRepository();
            _advanceExpenseDetailRepository = new AdvanceExpenseDetailRepository();
            _currencyConversionRateDetailManager = new CurrencyConversionRateDetailManager();
            _expenseFileManager = new ExpenseFileManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _expenseHistoryHeaderManager = new ExpenseHistoryManager();
            _expenseHistoryDetailRepository = new ExpenseHistoryDetailRepository();
            _advanceExpenseQueryRepository = new AdvanceVwGetAdvanceExpenseRepository();
        }

        public AdvanceExpenseManager(IAdvanceExpenseHeaderRepository advanceExpenseHeaderRepository, IAdvanceExpenseDetailRepository advanceExpenseDetailRepository, ICurrencyConversionRateDetailManager currencyConversionRateDetailManager, IExpenseFileManager expenseFileManager, IAdvanceRequisitionHeaderManager advanceRequisitionHeaderManager, IExpenseHistoryHeaderManager expenseHistoryHeaderManager, IExpenseHistoryDetailRepository expenseHistoryDetailRepository, IAdvance_VW_GetAdvanceExpenseRepository advanceExpenseQueryRepository)
        {
            _advanceExpenseHeaderRepository = advanceExpenseHeaderRepository;
            _advanceExpenseDetailRepository = advanceExpenseDetailRepository;
            _currencyConversionRateDetailManager = currencyConversionRateDetailManager;
            _expenseFileManager = expenseFileManager;
            _advanceRequisitionHeaderManager = advanceRequisitionHeaderManager;
            _expenseHistoryHeaderManager = expenseHistoryHeaderManager;
            _expenseHistoryDetailRepository = expenseHistoryDetailRepository;
            _advanceExpenseQueryRepository = advanceExpenseQueryRepository;
        }

        public bool Insert(AdvanceExpenseHeader entity)
        {
            if (entity.RequesterDepartmentId == null)
                throw new BllException("Department is not assigned for requester. Please contact to admin.");
            if (entity.RequesterRankId == null)
                throw new BllException("Designation is not assigned for requester. Please contact to admin.");
            if (entity.RequesterSupervisorId == null)
                throw new BllException("Supervisor is not assigned for requester. Please contact to admin.");

            using (TransactionScope ts = new TransactionScope())
            {
                List<AdvanceRequisitionHeader> requisitionHeaders = new List<AdvanceRequisitionHeader>();
                if (entity.AdvanceRequisitionHeaders != null)
                {
                    foreach (AdvanceRequisitionHeader requisitionHeader in entity.AdvanceRequisitionHeaders)
                    {
                        if (IsExpenseAlreadyEntryForRequisition(requisitionHeader.Id))
                        {
                            throw new Exception("Expense entry is already done for the requisition " + requisitionHeader.RequisitionNo + ".");
                        }
                        requisitionHeaders.Add(requisitionHeader);
                    }
                }
                entity.AdvanceRequisitionHeaders = null;
                entity.SerialNo = GetSerialNo(entity.CreatedOn);
                entity.GenerateExpenseNo();

                entity.ExpenseHistoryHeaders = new List<ExpenseHistoryHeader>() { entity.GenerateExpenseHistoryHeaderFromExpense(entity, HistoryModeEnum.Create) };

                bool isInserted = _advanceExpenseHeaderRepository.Insert(entity);

                foreach (AdvanceRequisitionHeader requisitionHeader in requisitionHeaders)
                {
                    requisitionHeader.AdvanceExpenseHeaderId = entity.Id;
                    _advanceRequisitionHeaderManager.Edit(requisitionHeader);
                }
               
                ts.Complete();
                return isInserted;
            }
        }

        public bool Insert(ICollection<AdvanceExpenseHeader> entityCollection)
        {
            return _advanceExpenseHeaderRepository.Insert(entityCollection);
        }

        public bool Edit(AdvanceExpenseHeader entity)
        {
            entity.LastModifiedOn = DateTime.Now;
            bool isCurrencyConversationUpdated = false;
            var existingHeader = GetById(entity.Id);
            var overseasTravelExpense = entity as AdvanceOverseasTravelExpenseHeader;
            if (overseasTravelExpense != null)
            {
                if (overseasTravelExpense.CurrencyConversionRateDetails != null)
                {
                    isCurrencyConversationUpdated = _currencyConversionRateDetailManager.Insert(overseasTravelExpense.CurrencyConversionRateDetails,
                    entity.Id);
                    if (isCurrencyConversationUpdated)
                    {
                        overseasTravelExpense.CurrencyConversionRateDetails = null;
                    }
                }
            }

            List<AdvanceRequisitionHeader> requisitionHeaders = new List<AdvanceRequisitionHeader>();
            if (entity.AdvanceRequisitionHeaders != null)
            {
                requisitionHeaders.AddRange(entity.AdvanceRequisitionHeaders);
            }
            entity.AdvanceRequisitionHeaders = null;

            var existingDetails = existingHeader.AdvanceExpenseDetails.ToList();

            var existingFIles = existingHeader.ExpenseFiles.ToList();

            var updatedDetails = entity.AdvanceExpenseDetails.ToList();

            var updatedFiles = entity.ExpenseFiles.ToList();

            var updateableItems = updatedDetails.Where(c => c.Id > 0).ToList();

            var updateableFiles = updatedFiles.Where(c => c.Id >= 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            var fileIdList = updatedFiles.Select(c => c.Id).ToList();

            var deleteableItems = existingDetails.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedDetails.Where(c => c.Id == 0).ToList();

            var addeableFiles = updatedFiles.Where(c => c.Id == 0).ToList();

            using (var ts = new TransactionScope())
            {
                entity.AdvanceExpenseDetails = null;
                entity.ExpenseFiles = null;
                var isUpdated = _advanceExpenseHeaderRepository.Edit(entity);
                HistoryModeEnum historyMode = HistoryModeEnum.Edit;
                if (entity.IsDeleted)
                {
                    historyMode = HistoryModeEnum.Delete;
                }
                var historyHeader = entity.GenerateExpenseHistoryHeaderFromExpense(entity, historyMode);
                historyHeader.LastModifiedOn = DateTime.Now;
                historyHeader.AdvanceExpenseHeaderId = entity.Id;
                var isHistoryHeaderAdded = _expenseHistoryHeaderManager.Insert(historyHeader);


                int deleteCount = 0;
                bool isDeleted = false;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var advanceExpenseDetail in deleteableItems)
                    {
                        isDeleted = _advanceExpenseDetailRepository.Delete(advanceExpenseDetail);
                        _expenseHistoryDetailRepository.Insert(advanceExpenseDetail.GenerateRequisitionHistoryDetail(advanceExpenseDetail, historyHeader.Id, HistoryModeEnum.Delete));
                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    addeableItems.ForEach(c => { c.AdvanceExpenseHeaderId = entity.Id; });
                    _advanceExpenseDetailRepository.Insert(addeableItems);
                    foreach (var advanceExpenseDetail in addeableItems)
                    {
                        _expenseHistoryDetailRepository.Insert(advanceExpenseDetail.GenerateRequisitionHistoryDetail(advanceExpenseDetail, historyHeader.Id, HistoryModeEnum.Create));
                    }
                }

                if (addeableFiles != null && addeableFiles.Any())
                {
                    addeableFiles.ForEach(c => { c.AdvanceExpenseHeaderId = entity.Id; });
                    _expenseFileManager.Insert(addeableFiles);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var item in updateableItems)
                    {
                        _advanceExpenseDetailRepository.Edit(item);
                        _expenseHistoryDetailRepository.Insert(item.GenerateRequisitionHistoryDetail(item, historyHeader.Id, HistoryModeEnum.Edit));
                    }
                }

                if (updateableFiles != null && updateableFiles.Any())
                {
                    foreach (var item in updateableFiles)
                    {
                        _expenseFileManager.Edit(item);
                    }
                }

                foreach (AdvanceRequisitionHeader requisitionHeader in requisitionHeaders)
                {
                    requisitionHeader.AdvanceExpenseHeaderId = entity.Id;
                    _advanceRequisitionHeaderManager.Edit(requisitionHeader);
                }

                ts.Complete();

                return isUpdated || isDeleted;
            }
        }

        public bool Delete(long id)
        {
            var advanceExpenseHeader = GetById(id);
            return _advanceExpenseHeaderRepository.Delete(advanceExpenseHeader);
        }

        public AdvanceExpenseHeader GetById(long id)
        {
            AdvanceExpenseHeader header = _advanceExpenseHeaderRepository.GetFirstOrDefaultBy(
                c => c.Id == id,
                c => c.AdvanceExpenseDetails,
                c => c.AdvanceRequisitionHeaders,
                c => c.AdvanceRequisitionHeaders.Select(d => d.AdvanceRequisitionDetails),
                c => c.AdvanceExpenseDetails.Select(d => d.AdvanceRequisitionDetail),
                c => c.AdvanceExpenseDetails.Select(d => d.AdvanceRequisitionDetail).Select(e => e.AdvanceRequisitionHeader),
                c => c.AdvanceRequisitionHeaders.Select(d => d.RequisitionFiles),
                c => c.AdvanceCategory,
                c => c.AdvanceExpenseStatus,
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalPanel),
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalStatus),
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalLevel.ApprovalLevelMembers),
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalTrackers),
                c => c.AdvanceRequisitionHeaders.Select(d => d.RequisitionFiles),
                c => c.ExpenseApprovalTickets.Select(d => d.DestinationUserForTickets)
                );
            header.ExpenseFiles = _expenseFileManager.GetByHeaderId(header.Id);
            return header;
        }

        public ICollection<AdvanceExpenseHeader> GetAll()
        {
            ICollection<AdvanceExpenseHeader> headers = _advanceExpenseHeaderRepository.GetAll(c => c.AdvanceExpenseDetails,
                c => c.AdvanceRequisitionHeaders,
                c => c.AdvanceRequisitionHeaders.Select(d => d.AdvanceRequisitionDetails),
                c => c.AdvanceExpenseDetails.Select(d => d.AdvanceRequisitionDetail),
                c => c.AdvanceExpenseDetails.Select(d => d.AdvanceRequisitionDetail).Select(e => e.AdvanceRequisitionHeader),
                c => c.AdvanceRequisitionHeaders.Select(d => d.RequisitionFiles),
                c => c.AdvanceCategory,
                c => c.AdvanceExpenseStatus,
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalPanel),
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalStatus),
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalLevel.ApprovalLevelMembers),
                c => c.ExpenseApprovalTickets.Select(d => d.ApprovalTrackers),
                c => c.AdvanceRequisitionHeaders.Select(d => d.RequisitionFiles),
                c => c.ExpenseApprovalTickets.Select(d => d.DestinationUserForTickets)
                );
            foreach (AdvanceExpenseHeader advanceExpenseHeader in headers)
            {
                advanceExpenseHeader.ExpenseFiles =
                    _expenseFileManager.GetByHeaderId(advanceExpenseHeader.Id);
            }

            return headers;
        }

        public bool IsExpenseAlreadyEntryForRequisition(long requisitionId)
        {
            return _advanceExpenseHeaderRepository.IsExpenseAlreadyEntryForRequisition(requisitionId);
        }

        public ICollection<AdvanceExpenseHeader> GetByCriteria(AdvanceExpenseSearchCriteria criteria)
        {
            ICollection<AdvanceExpenseHeader> headers = _advanceExpenseHeaderRepository.GetByCriteria(criteria);
            foreach (AdvanceExpenseHeader advanceExpenseHeader in headers)
            {
                advanceExpenseHeader.ExpenseFiles =
                    _expenseFileManager.GetByHeaderId(advanceExpenseHeader.Id);
            }
            return headers;
        }

        public AdvanceExpenseHeader GetByRequisition(long id)
        {
            throw new NotImplementedException();
            //AdvanceExpenseHeader header = _advanceExpenseHeaderRepository.GetFirstOrDefaultBy(
            //    c => c.AdvanceRequisitionHeaders.Any(d=>d.Id == id)
            //    );
            //if (header != null)
            //{
            //    header.ExpenseFiles = _expenseFileManager.GetByHeaderId(header.Id);
            //}
            //return header;
        }

        public bool PayExpense(long expenseId, string paidBy, DateTime paidOn)
        {
            bool isPaid = false;
            var requisition = GetById(expenseId);
            if (requisition != null)
            {
                requisition.PaidBy = paidBy;
                requisition.ExpenseIssueDate = paidOn;
                requisition.IsPaid = true;

                isPaid = _advanceExpenseHeaderRepository.Edit(requisition);
            }
            return isPaid;
        }

        public int GetSerialNo(DateTime createdOn)
        {
            var maxData = _advanceExpenseHeaderRepository.Get(
                    c => (DbFunctions.TruncateTime(c.CreatedOn).Value.Year ==
                        DbFunctions.TruncateTime(createdOn).Value.Year) &&
                        (DbFunctions.TruncateTime(c.CreatedOn).Value.Month ==
                        DbFunctions.TruncateTime(createdOn).Value.Month)).Select(c => c.SerialNo);

            var max = (!maxData.Any()) ? 0 : maxData.Max();
            return max + 1;
        }

        public bool ExpensePayReceived(long expenseId, string receivedBy, DateTime receivedOn)
        {
            bool isReceived = false;
            var expense = GetById(expenseId);
            if (expense != null)
            {
                expense.ReceivedBy = receivedBy;
                expense.ReceivedOn = receivedOn;
                expense.IsReceived = true;

                isReceived = _advanceExpenseHeaderRepository.Edit(expense);
            }
            return isReceived;
        }
    }
}
