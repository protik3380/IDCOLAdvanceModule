using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.SearchModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement
{
    public class ApprovalProcessManager : IApprovalProcessManager
    {
        private readonly IApprovalPanelManager _approvalPanelManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IRequisitionApprovalTicketManager _requisitionApprovalTicketManager;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;
        private readonly IAdvance_VW_GetAdvanceExpenseManager _advanceVwGetAdvanceExpenseManager;
        private readonly IApprovalLevelManager _approvalLevelManager;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceRequisitionManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IExpenseApprovalTicketManager _expenseApprovalTicketManager;
        private readonly IAdvanceExpenseHeaderManager _expenseHeaderManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvance_VW_GetHeadOfDepartmentManager _advanceVwGetHeadOfDepartmentManager;
        private readonly IAdvanceRequisitionDetailRepository _advanceRequisitionDetailRepository;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IDestinationUserForTicketManager _destinationUserForTicketManager;
        private readonly IRequisitionApprovalTrackerManager _requisitionApprovalTrackerManager;
        private readonly IExpenseApprovalTrackerManager _expenseApprovalTrackerManager;
        private readonly IApprovalLevelMemberManager _approvalLevelMemberManager;
        private readonly IEmployeeLeaveManager _employeeLeaveManager;
        private readonly IAdvance_VW_GetApprovalLevelMemberManager _advanceVwGetApprovalLevelMemberManager;

        public ApprovalProcessManager()
        {
            _approvalPanelManager = new ApprovalPanelManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionApprovalTicketManager = new RequisitionApprovalTicketManager();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _advanceVwGetAdvanceExpenseManager = new AdvanceVwGetAdvanceExpenseManager();
            _expenseHeaderManager = new AdvanceExpenseManager();
            _approvalLevelManager = new ApprovalLevelManager();
            _advanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _expenseApprovalTicketManager = new ExpenseApprovalTicketManager();
            _employeeManager = new EmployeeManager();
            _advanceVwGetHeadOfDepartmentManager = new AdvanceVwGetHeadOfDepartmentManager();
            _advanceRequisitionDetailRepository = new AdvanceRequisitionDetailRepository();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _destinationUserForTicketManager = new DestinationUserForTicketManager();
            _requisitionApprovalTrackerManager = new RequisitionApprovalTrackerManager();
            _expenseApprovalTrackerManager = new ExpenseApprovalTrackerManager();
            _approvalLevelMemberManager = new ApprovalLevelMemberManager();
            _employeeLeaveManager = new EmployeeLeaveManager();
            _advanceVwGetApprovalLevelMemberManager = new AdvanceVwGetApprovalLevelMemberManager();
        }

        public ApprovalProcessManager(IApprovalPanelManager approvalPanelManager, IAdvanceRequisitionCategoryManager advanceRequisitionCategoryManager, IRequisitionApprovalTicketManager requisitionApprovalTicketManager, IAdvance_VW_GetAdvanceRequisitionManager advanceVwGetAdvanceRequisitionManager, IAdvance_VW_GetAdvanceExpenseManager advanceVwGetAdvanceExpenseManager, IApprovalLevelManager approvalLevelManager, IAdvance_VW_GetAdvanceRequisitionManager advanceRequisitionManager, IAdvanceRequisitionHeaderManager advanceRequisitionHeaderManager, IExpenseApprovalTicketManager expenseApprovalTicketManager, IAdvanceExpenseHeaderManager expenseHeaderManager, IEmployeeManager employeeManager, IAdvance_VW_GetHeadOfDepartmentManager advanceVwGetHeadOfDepartmentManager, IAdvanceRequisitionDetailRepository advanceRequisitionDetailRepository, IAdvanceExpenseHeaderManager advanceExpenseHeaderManager, IDestinationUserForTicketManager destinationUserForTicketManager, IRequisitionApprovalTrackerManager requisitionApprovalTrackerManager, IExpenseApprovalTrackerManager expenseApprovalTrackerManager, IApprovalLevelMemberManager approvalLevelMemberManager, IEmployeeLeaveManager employeeLeaveManager, IAdvance_VW_GetApprovalLevelMemberManager advanceVwGetApprovalLevelMemberManager)
        {
            _approvalPanelManager = approvalPanelManager;
            _advanceRequisitionCategoryManager = advanceRequisitionCategoryManager;
            _requisitionApprovalTicketManager = requisitionApprovalTicketManager;
            _advanceVwGetAdvanceRequisitionManager = advanceVwGetAdvanceRequisitionManager;
            _advanceVwGetAdvanceExpenseManager = advanceVwGetAdvanceExpenseManager;
            _approvalLevelManager = approvalLevelManager;
            _advanceRequisitionManager = advanceRequisitionManager;
            _advanceRequisitionHeaderManager = advanceRequisitionHeaderManager;
            _expenseApprovalTicketManager = expenseApprovalTicketManager;
            _expenseHeaderManager = expenseHeaderManager;
            _employeeManager = employeeManager;
            _advanceVwGetHeadOfDepartmentManager = advanceVwGetHeadOfDepartmentManager;
            _advanceRequisitionDetailRepository = advanceRequisitionDetailRepository;
            _advanceExpenseHeaderManager = advanceExpenseHeaderManager;
            _destinationUserForTicketManager = destinationUserForTicketManager;
            _requisitionApprovalTrackerManager = requisitionApprovalTrackerManager;
            _expenseApprovalTrackerManager = expenseApprovalTrackerManager;
            _approvalLevelMemberManager = approvalLevelMemberManager;
            _employeeLeaveManager = employeeLeaveManager;
            _advanceVwGetApprovalLevelMemberManager = advanceVwGetApprovalLevelMemberManager;
        }

        public RequisitionApprovalTicket SendRequisitionForApproval(AdvanceRequisitionHeader requisition, string sentByUserName)
        {
            /* As a System, When a user send the Requisition for approval I want to identify the Requisition approval Panel and Requisition Approval Level
             * from my settings  for that particular Requisition, so that I can notify the Approval level’s users to approve. */

            if (requisition == null)
            {
                throw new BllException("No requisition found on sending for approval.");
            }

            if (requisition.Id <= 0)
            {
                throw new Exception("Send for approval is only applicable for existing requisitions, please create a requisition first to send for approval.");
            }

            if (requisition.AdvanceCategoryId <= 0)
            {
                throw new BllException("No category is set for requistion, error while trying to send for approval!");
            }

            var requisitionCategory =
                _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId);

            if (requisitionCategory.RequisitionApprovalPanel == null)
            {
                throw new BllException("No approval panel is set for requisition cateogry " + requisitionCategory.Name);
            }

            var approvalPanel = requisitionCategory.RequisitionApprovalPanel;

            if (approvalPanel.ApprovalLevels == null || !approvalPanel.ApprovalLevels.Any())
            {
                throw new BllException("No approval level is configured yet for the approval panel " + approvalPanel.Name + ", configure the panel levels properly.");
            }

            var startingLevel =
                approvalPanel.ApprovalLevels.FirstOrDefault(c => c.LevelOrder == Utility.Utility.StartingLevelNo);

            if (startingLevel == null)
            {
                throw new BllException("No starting level found for panel " + approvalPanel.Name + ", please set a level order to " + Utility.Utility.StartingLevelNo + " to identify it as starting level.");
            }

            RequisitionApprovalTicket approvalTicket = null;

            if (!requisition.RequisitionApprovalTickets.Any())
            {
                approvalTicket = RequisitionApprovalTicket.GenerateTicket(requisition, startingLevel, sentByUserName, ApprovalStatusEnum.ApprovalInitiated);
            }
            else
            {
                approvalTicket = requisition.RequisitionApprovalTickets.FirstOrDefault();
            }

            if (approvalTicket != null)
            {
                if (approvalTicket.Id == 0)
                {
                    bool isInserted = _requisitionApprovalTicketManager.Insert(approvalTicket);
                    if (!isInserted)
                    {
                        throw new BllException("Ticket Generation Failed! Please contact with vendor");
                    }
                }

                requisition.AdvanceRequisitionStatusId = (long)AdvanceStatusEnum.ApprovalOnProgress;
                _advanceRequisitionHeaderManager.Edit(requisition);

                approvalTicket.UpdateTicketWithTracker(ApprovalStatusEnum.SentForApproval);
                bool isUpdated = _requisitionApprovalTicketManager.Edit(approvalTicket);

                bool isSet = SetNextPriorityMember(approvalTicket);

                if (isSet)
                {
                    _requisitionApprovalTicketManager.Edit(approvalTicket);
                }
                if (!isSet)
                {
                    isSet = SendToNextLevel(approvalTicket);
                }
            }

            return approvalTicket;
        }

        public ICollection<RequisitionApprovalTicket> SendRequisitionForApproval(ICollection<AdvanceRequisitionHeader> requisitionHeaders, string sentByUsername)
        {
            ICollection<RequisitionApprovalTicket> tickets = null;

            foreach (var advanceRequisitionHeader in requisitionHeaders)
            {
                var ticket = SendRequisitionForApproval(advanceRequisitionHeader, sentByUsername);
                if (ticket != null && tickets == null)
                {
                    tickets = new List<RequisitionApprovalTicket>();
                }

                if (ticket != null)
                {
                    tickets.Add(ticket);
                }
            }

            return tickets;
        }

        public ExpenseApprovalTicket SendExpenseForApproval(AdvanceExpenseHeader expenseHeader, string sentByUserName)
        {
            /* As a System, When a user send the Requisition for approval I want to identify the Requisition approval Panel and Requisition Approval Level
             * from my settings  for that particular Requisition, so that I can notify the Approval level’s users to approve. */

            if (expenseHeader == null)
            {
                throw new BllException("No Expense Information found on sending for approval.");
            }

            if (expenseHeader.Id <= 0)
            {
                throw new Exception("Send for approval is only applicable for existing Expense, please create a Expense first to send for approval.");
            }

            if (expenseHeader.AdvanceCategoryId <= 0)
            {
                throw new BllException("No category is set for Expense, error while trying to send for approval!");
            }

            var advanceCategory =
                _advanceRequisitionCategoryManager.GetById(expenseHeader.AdvanceCategoryId);

            if (advanceCategory.ExpenseApprovalPanel == null)
            {
                throw new BllException("No approval panel is set for Expense cateogry " + advanceCategory.Name);
            }

            var approvalPanel = advanceCategory.ExpenseApprovalPanel;


            if (approvalPanel.ApprovalLevels == null || !approvalPanel.ApprovalLevels.Any())
            {
                throw new BllException("No approval level is configured yet for the approval panel " + approvalPanel.Name + ", configure the panel level's properly.");
            }

            var startingLevel =
                approvalPanel.ApprovalLevels.FirstOrDefault(c => c.LevelOrder == Utility.Utility.StartingLevelNo);

            if (startingLevel == null)
            {
                throw new BllException("No starting level found for panel " + approvalPanel.Name + " , please set a level order to " + Utility.Utility.StartingLevelNo + " to identify it as starting level.");
            }

            ExpenseApprovalTicket approvalTicket = null;

            if (!expenseHeader.ExpenseApprovalTickets.Any())
            {
                approvalTicket = ExpenseApprovalTicket.GenerateTicket(expenseHeader, startingLevel, sentByUserName, ApprovalStatusEnum.ApprovalInitiated);
            }
            else
            {
                approvalTicket = expenseHeader.ExpenseApprovalTickets.FirstOrDefault();
            }

            if (approvalTicket != null)
            {
                if (approvalTicket.Id == 0)
                {
                    bool isInserted = _expenseApprovalTicketManager.Insert(approvalTicket);
                    if (!isInserted)
                    {
                        throw new BllException("Ticket Generation Failed! Please contact with vendor");
                    }
                }

                expenseHeader.AdvanceExpenseStatusId = (long)AdvanceStatusEnum.ApprovalOnProgress;
                _advanceExpenseHeaderManager.Edit(expenseHeader);

                approvalTicket.UpdateTicketWithTracker(ApprovalStatusEnum.SentForApproval);
                bool isUpdated = _expenseApprovalTicketManager.Edit(approvalTicket);

                bool isSet = SetNextPriorityMember(approvalTicket);

                if (isSet)
                {
                    _expenseApprovalTicketManager.Edit(approvalTicket);
                }
                if (!isSet)
                {
                    isSet = SendToNextLevel(approvalTicket);
                }
            }

            //if (approvalTicket != null && approvalTicket.Id == 0)
            //{
            //    approvalTicket.SetTrackerInstance();

            //    //using (var ts = new TransactionScope())
            //    //{
            //    bool isInserted = _expenseApprovalTicketManager.Insert(approvalTicket);

            //    if (isInserted)
            //    {
            //        expenseHeader.AdvanceExpenseStatusId = (long)AdvanceStatusEnum.ApprovalOnProgress;
            //        _expenseHeaderManager.Edit(expenseHeader);
            //        approvalTicket.UpdateTicketWithTracker(ApprovalStatusEnum.SentForApproval);

            //        bool isUpdated = _expenseApprovalTicketManager.Edit(approvalTicket);
            //        if (isUpdated)
            //        {
            //            SendToNextLevel(approvalTicket);
            //        }
            //    }

            //    //    ts.Complete();
            //    //}
            //}

            return approvalTicket;
        }

        public ICollection<ExpenseApprovalTicket> SendExpenseForApproval(ICollection<AdvanceExpenseHeader> expenseHeaders,
            string sentByUserName)
        {
            ICollection<ExpenseApprovalTicket> tickets = null;

            foreach (var expenseHeader in expenseHeaders)
            {
                var ticket = SendExpenseForApproval(expenseHeader, sentByUserName);
                if (ticket != null && tickets == null)
                {
                    tickets = new List<ExpenseApprovalTicket>();
                }

                if (ticket != null)
                {
                    tickets.Add(ticket);
                }
            }

            return tickets;
        }

        public bool SendToNextLevel(ExpenseApprovalTicket approvalTicket)
        {
            if (approvalTicket == null)
            {
                throw new Exception("No Approval Ticket is found processing for approval!");
            }
            bool isUpdated = false;

            //using (var ts = new TransactionScope())
            //{
            if (approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.SentForApproval)
            {
                approvalTicket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
                isUpdated = _expenseApprovalTicketManager.Edit(approvalTicket);
            }
            else
            {
                ApprovalLevel nextLevel = null;
                if (approvalTicket.ApprovalPanelId != null && approvalTicket.ApprovalPanelId > 0)
                {
                    var approvalPanel = _approvalPanelManager.GetById(approvalTicket.ApprovalPanelId);

                    bool isTopLevel = approvalPanel.IsTopLevel(approvalTicket.ApprovalLevelId);
                    if (!isTopLevel)
                    {
                        //nextLevel = approvalPanel.GetNextLevel(approvalTicket.ApprovalLevelId);
                        //var nextLevelApprovalMembers = GetNextApprovalLevelMembersForExpense(nextLevel.Id, approvalPanel.Id, approvalTicket.AdvanceExpenseHeaderId);

                        nextLevel = approvalPanel.GetNextLevel(approvalTicket.ApprovalLevelId);

                        if (nextLevel == null)
                        {
                            isTopLevel = true;
                        }

                        //if (nextLevel != null && !isTopLevel)
                        if (nextLevel != null)
                        {
                            _advanceVwGetApprovalLevelMemberManager.CheckLevelMemberExists(nextLevel.Id, approvalTicket.Id);

                            approvalTicket.ApprovalLevelId = nextLevel.Id;
                            approvalTicket.ApprovalLevel = nextLevel;
                            approvalTicket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
                            approvalTicket.ApprovalLevel = null;
                            var isSet = SetNextPriorityMember(approvalTicket);
                            if (isSet)
                            {
                                return _expenseApprovalTicketManager.Edit(approvalTicket);
                            }
                            if (!isSet)
                            {
                                return SendToNextLevel(approvalTicket);
                            }
                        }
                    }
                    // this  top level is used when the requester is only top level approval member.
                    if (isTopLevel)
                    {
                        var header =
                            _expenseHeaderManager.GetById(approvalTicket.AdvanceExpenseHeaderId);
                        if (Session.LoginUserName.ToLower().Equals("admin"))
                        {
                            bool isSet = SetNextPriorityMember(approvalTicket, 2);
                            if (!isSet)
                            {
                                throw new BllException("Sorry! Since the adjustment# " + header.ExpenseNo + " is in the top level, and there is no next member in this level, you cannot move this adjustment.");
                            }
                            if (header.AdvanceExpenseStatusId == (long)AdvanceStatusEnum.Approved)
                            {
                                throw new BllException("Adjustment# " + header.ExpenseNo + " is already approved.");
                            }
                        }
                        approvalTicket.UpdateTicketWithTracker(ApprovalStatusEnum.Approved, approvalTicket.AuthorizedBy);
                        isUpdated = _expenseApprovalTicketManager.Edit(approvalTicket);
                        header.AdvanceExpenseStatusId = (long)AdvanceStatusEnum.Approved;
                        header.ApprovedBy = approvalTicket.AuthorizedBy;
                        header.ApprovedOn = DateTime.Now;
                        bool isHeaderUpdated = _expenseHeaderManager.Edit(header);
                        isUpdated = isUpdated && isHeaderUpdated;
                    }
                }
                //}
                //ts.Complete();

            }
            return isUpdated;
        }

        public bool SendToNextLevel(ICollection<ExpenseApprovalTicket> expenseApprovalTickets)
        {
            bool isUpdated = false;

            if (expenseApprovalTickets.Any())
            {
                int updateCount = 0;
                foreach (var requisitionApprovalTicket in expenseApprovalTickets)
                {
                    bool isSentToNextLevel = SendToNextLevel(requisitionApprovalTicket);
                    if (isSentToNextLevel)
                    {
                        updateCount++;
                    }
                }

                isUpdated = updateCount > 0 && updateCount == expenseApprovalTickets.Count();
            }

            return isUpdated;
        }

        public bool SendToNextLevel(RequisitionApprovalTicket approvalTicket)
        {
            if (approvalTicket == null)
            {
                throw new Exception("No Approval Ticket is found processing for approval!");
            }
            bool isUpdated = false;

            //using (var ts = new TransactionScope())
            //{

            ApprovalLevel nextLevel = null;
            if (approvalTicket.ApprovalPanelId > 0)
            {
                var approvalPanel = _approvalPanelManager.GetById(approvalTicket.ApprovalPanelId);

                bool isTopLevel = approvalPanel.IsTopLevel(approvalTicket.ApprovalLevelId);

                if (!isTopLevel)
                {
                    nextLevel = approvalPanel.GetNextLevel(approvalTicket.ApprovalLevelId);

                    if (nextLevel == null)
                    {
                        isTopLevel = true;
                    }

                    if (nextLevel != null)
                    {
                        _advanceVwGetApprovalLevelMemberManager.CheckLevelMemberExists(nextLevel.Id, approvalTicket.Id);

                        approvalTicket.ApprovalLevelId = nextLevel.Id;
                        approvalTicket.ApprovalLevel = nextLevel;
                        approvalTicket.ApprovalLevel = null;

                        var isSet = SetNextPriorityMember(approvalTicket);
                        if (isSet)
                        {
                            return _requisitionApprovalTicketManager.Edit(approvalTicket);
                        }
                        if (!isSet)
                        {
                            return SendToNextLevel(approvalTicket);
                        }
                    }
                }
                if (isTopLevel)
                {
                    var header =
                        _advanceRequisitionHeaderManager.GetById(approvalTicket.AdvanceRequisitionHeaderId);
                    if (Session.LoginUserName.ToLower().Equals("admin"))
                    {
                        bool isSet = SetNextPriorityMember(approvalTicket, 2);
                        if (!isSet)
                        {
                            throw new BllException("Sorry! Since the requisition# " + header.RequisitionNo + " is in the top level, and there is no next member in this level, you cannot move this requisition.");
                        }
                        if (header.AdvanceRequisitionStatusId == (long)AdvanceStatusEnum.Approved)
                        {
                            throw new BllException("Requisition# " + header.RequisitionNo + " is already approved.");
                        }
                    }
                    approvalTicket.UpdateTicketWithTracker(ApprovalStatusEnum.Approved, approvalTicket.AuthorizedBy);
                    isUpdated = _requisitionApprovalTicketManager.Edit(approvalTicket);

                    header.AdvanceRequisitionStatusId = (long)AdvanceStatusEnum.Approved;
                    header.ApprovedBy = approvalTicket.AuthorizedBy;
                    header.ApprovedOn = DateTime.Now;
                    bool isHeaderUpdated = _advanceRequisitionHeaderManager.Edit(header);
                    isUpdated = isUpdated && isHeaderUpdated;
                }

                // this  top level is used when the requester is only top level approval member.

                //}
                //}

                //ts.Complete();

            }

            return isUpdated;
        }

        public ICollection<ApprovalLevelMember> GetNextApprovalLevelMembersForRequisition(long approvalLevelId, long approvalPanelId, long requisitionHeaderId)
        {
            var approvalLevelMembers = _approvalLevelManager.GetLevelWithApprovalLevelMembersForRequsition(approvalPanelId, approvalLevelId, requisitionHeaderId).ApprovalLevelMembers;
            if (approvalLevelMembers == null || approvalLevelMembers.Count == 0)
            {
                throw new BllException("No Member found for next approval level, Please contact with admin");
            }
            return approvalLevelMembers;
        }

        public ICollection<ApprovalLevelMember> GetNextApprovalLevelMembersForExpense(long approvalLevelId, long approvalPanelId, long expenseHeaderId)
        {
            var approvalLevelMembers = _approvalLevelManager.GetLevelWithApprovalLevelMembersForExpense(approvalPanelId, approvalLevelId, expenseHeaderId).ApprovalLevelMembers;
            if (approvalLevelMembers == null || approvalLevelMembers.Count == 0)
            {
                throw new BllException("No Member found for next approval level, Please contact with admin");
            }
            return approvalLevelMembers;
        }

        public bool RemoveRequisition(ICollection<AdvanceRequisitionHeader> headers)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                bool isRemoved;
                int removeCount = 0;
                foreach (var header in headers)
                {
                    isRemoved = RemoveRequisition(header);
                    if (isRemoved)
                        removeCount++;
                }
                isRemoved = headers.Count == removeCount;
                ts.Complete();
                return isRemoved;
            }
        }

        public bool RemoveRequisition(AdvanceRequisitionHeader header)
        {
            header.IsDeleted = true;
            header.AdvanceRequisitionStatusId = (long)AdvanceStatusEnum.Removed;
            return _advanceRequisitionHeaderManager.Edit(header);
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRemovedRequisitions(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = new List<Advance_VW_GetAdvanceRequisition>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                requisitionList = GetRemovedRequisitionsForMember(memberUserName);
            }
            else
            {
                requisitionList = GetRemovedRequisitionsForOthers(memberUserName);
            }

            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRemovedRequisitionsForOthers(string createdUserName)
        {
            if (string.IsNullOrEmpty(createdUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.CreatedBy.Equals(createdUserName) && !c.RequesterUserName.Equals(createdUserName)) && c.RequistionApprovalTicketId == null && c.IsDeleted == true);
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRemovedRequisitionsForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && c.RequistionApprovalTicketId == null && c.IsDeleted == true);
            return requisitionList;
        }

        public bool RemoveExpense(ICollection<AdvanceExpenseHeader> headers)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                bool isRemoved;
                int removeCount = 0;
                foreach (var header in headers)
                {
                    isRemoved = RemovExpense(header);
                    if (isRemoved)
                        removeCount++;
                }
                isRemoved = headers.Count == removeCount;
                ts.Complete();
                return isRemoved;
            }
        }

        public bool RemovExpense(AdvanceExpenseHeader header)
        {
            header.IsDeleted = true;
            header.AdvanceExpenseStatusId = (long)AdvanceStatusEnum.Removed;
            return _expenseHeaderManager.Edit(header);
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetRemovedExpenses(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expnesesList = new List<Advance_VW_GetAdvanceExpense>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                expnesesList = GetRemovedExpensesForMember(memberUserName);
            }
            if (criteria.AdvanceCategoryId > 0)
            {
                expnesesList = expnesesList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expnesesList = expnesesList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expnesesList = expnesesList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return expnesesList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetRemovedExpensesForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expensesList =
                _advanceVwGetAdvanceExpenseManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && c.ExpenseApprovalTicketId == null && c.IsDeleted == true);
            return expensesList;
        }

        public bool SendToNextLevel(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets)
        {
            bool isUpdated = false;

            if (requisitionApprovalTickets.Any())
            {
                int updateCount = 0;
                foreach (var requisitionApprovalTicket in requisitionApprovalTickets)
                {
                    bool isSentToNextLevel = SendToNextLevel(requisitionApprovalTicket);
                    if (isSentToNextLevel)
                    {
                        updateCount++;
                    }
                }

                isUpdated = updateCount > 0 && updateCount == requisitionApprovalTickets.Count();
            }

            return isUpdated;
        }

        public bool Approve(ExpenseApprovalTicket expenseApprovalTicket, string approveByUserName)
        {
            bool isApproved = false;

            var ticket = _expenseApprovalTicketManager.GetById(expenseApprovalTicket.Id);
            if (ticket == null)
            {
                throw new Exception("No Approval Expense Ticket is found to approve the request!");
            }

            ticket.Remarks = expenseApprovalTicket.Remarks;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.Approved, approveByUserName);
            //using (var ts = new TransactionScope())
            //{
            if (expenseApprovalTicket.DestinationUserForTickets.Any(c => c.Id == 0))
            {
                var approvalPanelForFindingNextLevel = _approvalPanelManager.GetById(ticket.ApprovalPanelId);
                var nextLevel = approvalPanelForFindingNextLevel.GetNextLevel(expenseApprovalTicket.ApprovalLevelId);

                foreach (DestinationUserForTicket destinationUserForTicket in expenseApprovalTicket.DestinationUserForTickets)
                {
                    if (destinationUserForTicket.Id == 0)
                    {
                        destinationUserForTicket.ApprovalLevelId = nextLevel.Id;
                    }
                }
                ticket.DestinationUserForTickets = expenseApprovalTicket.DestinationUserForTickets;
            }
            isApproved = _expenseApprovalTicketManager.Edit(ticket);

            var approvalPanel = _approvalPanelManager.GetById(ticket.ApprovalPanelId);

            bool isTopLevel = approvalPanel.IsTopLevel(ticket.ApprovalLevelId);

            if (isTopLevel)
            {
                var header = _expenseHeaderManager.GetById(expenseApprovalTicket.AdvanceExpenseHeaderId);
                header.AdvanceExpenseStatusId = (long)AdvanceStatusEnum.Approved;
                header.ApprovedBy = approveByUserName;
                header.ApprovedOn = DateTime.Now;
                _expenseHeaderManager.Edit(header);
                isApproved = true;
            }
            else
            {
                ticket.Remarks = null;
                SendToNextLevel(ticket);
            }

            //    ts.Complete();
            //}

            return isApproved;
        }

        public bool Approve(RequisitionApprovalTicket requisitionApprovalTicket, AdvanceRequisitionHeader advanceRequisitionHeader, string approveByUserName)
        {
            //using (TransactionScope ts = new TransactionScope())
            //{
            bool isRequisitionUpdated = _advanceRequisitionHeaderManager.Edit(advanceRequisitionHeader);
            bool isApproved = Approve(requisitionApprovalTicket, approveByUserName);
            //ts.Complete();
            return isRequisitionUpdated || isApproved;
            //}   
        }

        public bool Approve(ExpenseApprovalTicket expenseApprovalTicket, AdvanceExpenseHeader advanceExpenseHeader,
            string approveByUserName)
        {
            //using (TransactionScope ts = new TransactionScope())
            //{
            bool isRequisitionUpdated = _expenseHeaderManager.Edit(advanceExpenseHeader);
            bool isApproved = Approve(expenseApprovalTicket, approveByUserName);
            //ts.Complete();
            return isRequisitionUpdated || isApproved;

            //}   
        }

        public bool Approve(RequisitionApprovalTicket requisitionApprovalTicket, string approveByUserName)
        {
            /* 6.	As a System when authorized user will approve the Requisition, 
             * I want to set the Approval Status of the Requisition Approval Ticket to “Approved” along with Approved by and Remarks and on that instance,
             * create an entry in Requisition Approval Tracker cloning the Requisition Approval Ticket information. */

            bool isApproved = false;

            var ticket = _requisitionApprovalTicketManager.GetById(requisitionApprovalTicket.Id);
            if (ticket == null)
            {
                throw new Exception("No Approval Requisition Ticket is found to approve the request!");
            }

            ticket.Remarks = requisitionApprovalTicket.Remarks;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.Approved, approveByUserName);
            //using (var ts = new TransactionScope())
            //{
            if (requisitionApprovalTicket.DestinationUserForTickets.Any(c => c.Id == 0))
            {
                var approvalPanelForFindingNextLevel = _approvalPanelManager.GetById(ticket.ApprovalPanelId);
                var nextLevel = approvalPanelForFindingNextLevel.GetNextLevel(requisitionApprovalTicket.ApprovalLevelId);

                foreach (DestinationUserForTicket destinationUserForTicket in requisitionApprovalTicket.DestinationUserForTickets)
                {
                    if (destinationUserForTicket.Id == 0)
                    {
                        destinationUserForTicket.ApprovalLevelId = nextLevel.Id;
                    }
                }
                ticket.DestinationUserForTickets = requisitionApprovalTicket.DestinationUserForTickets;
            }
            isApproved = _requisitionApprovalTicketManager.Edit(ticket);

            var approvalPanel = _approvalPanelManager.GetById(ticket.ApprovalPanelId);

            bool isTopLevel = approvalPanel.IsTopLevel(ticket.ApprovalLevelId);

            if (isTopLevel)
            {
                var header =
                    _advanceRequisitionHeaderManager.GetById(requisitionApprovalTicket.AdvanceRequisitionHeaderId);
                header.AdvanceRequisitionStatusId = (long)AdvanceStatusEnum.Approved;
                header.ApprovedBy = approveByUserName;
                header.ApprovedOn = DateTime.Now;
                _advanceRequisitionHeaderManager.Edit(header);
                isApproved = true;
            }
            else
            {
                ticket.Remarks = null;
                SendToNextLevel(ticket);
            }

            //    ts.Complete();
            //}

            return isApproved;
        }

        public bool Approve(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets, string approveByUserName)
        {
            bool isApproveSuccess = false;
            if (requisitionApprovalTickets.Any())
            {
                if (requisitionApprovalTickets.Any(c => !c.ApprovalLevel.IsApprovalAuthority))
                {
                    throw new BllException("You have selected item(s) which are waiting for verfication. You must verify item once at a time.");
                }
                int approveCount = 0;
                foreach (var requisitionApprovalTicket in requisitionApprovalTickets)
                {
                    bool isApprove = Approve(requisitionApprovalTicket, approveByUserName);
                    if (isApprove)
                    {
                        approveCount++;
                    }
                }

                isApproveSuccess = approveCount > 0 && approveCount == requisitionApprovalTickets.Count();
            }

            return isApproveSuccess;
        }

        public bool Approve(ICollection<ExpenseApprovalTicket> approvalTickets, string approveByUserName)
        {
            bool isApproveSuccess = false;
            if (approvalTickets.Any())
            {
                if (approvalTickets.Any(c => !c.ApprovalLevel.IsApprovalAuthority))
                {
                    throw new BllException("You have selected item(s) which are waiting for verfication. You must verify item once at a time.");
                }
                int approveCount = 0;
                foreach (var approvalTicket in approvalTickets)
                {
                    bool isApprove = Approve(approvalTicket, approveByUserName);
                    if (isApprove)
                    {
                        approveCount++;
                    }
                }

                isApproveSuccess = approveCount > 0 && approveCount == approvalTickets.Count();
            }

            return isApproveSuccess;
        }

        public bool Revert(RequisitionApprovalTicket requisitionApprovalTicket, string revertedByUserName)
        {
            if (requisitionApprovalTicket == null)
            {
                throw new Exception("No request found to revert.");
            }
            bool isReverted = false;
            var ticket = _requisitionApprovalTicketManager.GetById(requisitionApprovalTicket.Id);
            ticket.Remarks = requisitionApprovalTicket.Remarks;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.Reverted, revertedByUserName);

            //using (var ts = new TransactionScope())
            //{
            isReverted = _requisitionApprovalTicketManager.Edit(ticket);
            if (isReverted)
            {
                var header = _advanceRequisitionHeaderManager.GetById(ticket.AdvanceRequisitionHeaderId);
                header.AdvanceRequisitionStatusId = (long)AdvanceStatusEnum.Reverted;
                _advanceRequisitionHeaderManager.Edit(header);
            }
            //    ts.Complete();
            //}
            return isReverted;
        }

        public bool Revert(ExpenseApprovalTicket expenseApprovalTicket, string revertedByUserName)
        {
            if (expenseApprovalTicket == null)
            {
                throw new Exception("No request found to revert.");
            }
            bool isReverted = false;
            var ticket = _expenseApprovalTicketManager.GetById(expenseApprovalTicket.Id);
            ticket.Remarks = expenseApprovalTicket.Remarks;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.Reverted, revertedByUserName);

            //using (var ts = new TransactionScope())
            //{
            isReverted = _expenseApprovalTicketManager.Edit(ticket);
            if (isReverted)
            {
                var header = _expenseHeaderManager.GetById(ticket.AdvanceExpenseHeaderId);
                header.AdvanceExpenseStatusId = (long)AdvanceStatusEnum.Reverted;
                _expenseHeaderManager.Edit(header);
            }
            //    ts.Complete();
            //}
            return isReverted;
        }

        public bool Revert(ICollection<ExpenseApprovalTicket> expenseApprovalTickets, string revertedByUserName)
        {
            if (expenseApprovalTickets.Any())
            {
                var revertSuccessCount = 0;
                foreach (var expenseApprovalTicket in expenseApprovalTickets)
                {
                    bool isReverted = Revert(expenseApprovalTicket, revertedByUserName);
                    if (isReverted)
                    {
                        revertSuccessCount++;
                    }
                }
                return revertSuccessCount > 0 && revertSuccessCount == expenseApprovalTickets.Count();
            }
            else
            {
                throw new Exception("No Requisitions Found to revert!");
            }
        }

        public bool Revert(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets, string revertedByUserName)
        {
            if (requisitionApprovalTickets.Any())
            {
                var revertSuccessCount = 0;
                foreach (var requisitionApprovalTicket in requisitionApprovalTickets)
                {
                    bool isReverted = Revert(requisitionApprovalTicket, revertedByUserName);
                    if (isReverted)
                    {
                        revertSuccessCount++;
                    }
                }
                return revertSuccessCount > 0 && revertSuccessCount == requisitionApprovalTickets.Count();
            }
            else
            {
                throw new Exception("No Requisitions Found to revert!");
            }
        }

        public bool Reject(RequisitionApprovalTicket requisitionApprovalTicket, string rejectedByUserName)
        {
            if (requisitionApprovalTicket == null)
            {
                throw new Exception("No request found to reject.");
            }
            bool isRejected = false;
            var ticket = _requisitionApprovalTicketManager.GetById(requisitionApprovalTicket.Id);
            ticket.Remarks = requisitionApprovalTicket.Remarks;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.Rejected, rejectedByUserName);
            //using (var ts = new TransactionScope())
            //{
            isRejected = _requisitionApprovalTicketManager.Edit(ticket);
            if (isRejected)
            {
                var header = _advanceRequisitionHeaderManager.GetById(ticket.AdvanceRequisitionHeaderId);
                header.AdvanceRequisitionStatusId = (long)AdvanceStatusEnum.Rejected;
                header.RejectedOn = DateTime.Now;
                header.RejectedBy = rejectedByUserName;
                _advanceRequisitionHeaderManager.Edit(header);
            }
            //    ts.Complete();
            //}
            return isRejected;
        }

        public bool Reject(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets, string rejectedByUserName)
        {
            if (requisitionApprovalTickets.Any())
            {
                var revertSuccessCount = 0;
                foreach (var requisitionApprovalTicket in requisitionApprovalTickets)
                {
                    bool isReverted = Revert(requisitionApprovalTicket, rejectedByUserName);
                    if (isReverted)
                    {
                        revertSuccessCount++;
                    }
                }
                return revertSuccessCount > 0 && revertSuccessCount == requisitionApprovalTickets.Count();
            }
            else
            {
                throw new Exception("No requisition found to reject.");
            }
        }

        public bool Reject(ExpenseApprovalTicket expenseApprovalTicket, string rejectedByUserName)
        {
            if (expenseApprovalTicket == null)
            {
                throw new Exception("No request found to reject.");
            }
            bool isRejected = false;
            var ticket = _expenseApprovalTicketManager.GetById(expenseApprovalTicket.Id);
            ticket.Remarks = expenseApprovalTicket.Remarks;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.Rejected, rejectedByUserName);
            //using (var ts = new TransactionScope())
            //{
            isRejected = _expenseApprovalTicketManager.Edit(ticket);
            if (isRejected)
            {
                var header = _expenseHeaderManager.GetById(ticket.AdvanceExpenseHeaderId);
                header.AdvanceExpenseStatusId = (long)AdvanceStatusEnum.Rejected;
                header.RejectedOn = DateTime.Now;
                header.RejectedBy = rejectedByUserName;
                _expenseHeaderManager.Edit(header);
            }
            //    ts.Complete();
            //}
            return isRejected;
        }

        public bool Reject(ICollection<ExpenseApprovalTicket> expenseApprovalTickets, string rejectedByUserName)
        {
            if (expenseApprovalTickets.Any())
            {
                var revertSuccessCount = 0;
                foreach (var expenseApprovalTicket in expenseApprovalTickets)
                {
                    bool isReverted = Revert(expenseApprovalTicket, rejectedByUserName);
                    if (isReverted)
                    {
                        revertSuccessCount++;
                    }
                }
                return revertSuccessCount > 0 && revertSuccessCount == expenseApprovalTickets.Count();
            }
            else
            {
                throw new Exception("No expense found to reject.");
            }
        }

        public bool SendRevertedRequisitionsForApproval(ICollection<AdvanceRequisitionHeader> headers, string sentByUser)
        {
            if (headers == null || !headers.Any())
            {
                throw new Exception("Requisitions not found to send for approval, Error while trying to send reverted requisitions while there was no requisitions to send!");
            }

            int successCount = 0;
            foreach (var advanceRequisitionHeader in headers)
            {
                bool isSuccess = SendRevertedRequisitionsForApproval(advanceRequisitionHeader, sentByUser);

                if (isSuccess)
                {
                    successCount++;
                }
            }

            return successCount > 0 && successCount == headers.Count;
        }

        public bool SendRevertedExpensesForApproval(ICollection<AdvanceExpenseHeader> headers, string sentByUser)
        {
            if (headers == null || !headers.Any())
            {
                throw new Exception("Expenses not found to send for approval, Error while trying to send reverted requisitions while there was no requisitions to send!");

            }

            int successCount = 0;
            foreach (var expenseHeader in headers)
            {
                bool isSuccess = SendRevertedExpensesForApproval(expenseHeader, sentByUser);

                if (isSuccess)
                {
                    successCount++;
                }
            }

            return successCount > 0 && successCount == headers.Count;
        }

        public bool SendRevertedExpensesForApproval(AdvanceExpenseHeader header, string sentByUser)
        {
            if (header == null)
            {
                throw new BllException(
                    "No Requisitions found for Send to approal! Error occurred while trying to send reverted requisition for approval");
            }

            var ticket = _expenseApprovalTicketManager.GetByExpenseHeaderId(header.Id);

            if (ticket == null)
            {
                throw new BllException(
                    "No Ticket found for existing approval, the requisition is expected to be reverted against an existing approval process! Error occurred while trying to send reverted requisition for approval");
            }
            var requisitionCategory =
                _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);

            if (requisitionCategory.ExpenseApprovalPanel == null)
            {
                throw new BllException("No approval panel is set for requisition cateogry " + requisitionCategory.Name);
            }

            var approvalPanel = requisitionCategory.ExpenseApprovalPanel;

            if (approvalPanel.ApprovalLevels == null || !approvalPanel.ApprovalLevels.Any())
            {
                throw new BllException("No approval level is configured yet for the approval panel " + approvalPanel.Name + ", configure the panel level's properly.");
            }

            var startingLevel =
                approvalPanel.ApprovalLevels.FirstOrDefault(c => c.LevelOrder == Utility.Utility.StartingLevelNo);

            if (startingLevel == null)
            {
                throw new BllException("No starting level found for panel " + approvalPanel.Name + " , please set a level order to " + Utility.Utility.StartingLevelNo + " to identify it as starting level.");
            }
            //setting Panel and level
            ticket.ApprovalPanelId = approvalPanel.Id;
            ticket.ApprovalLevelId = startingLevel.Id;


            bool isSentSuccess = false;
            ticket.Remarks = null;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.SentForApproval, sentByUser);

            using (var ts = new TransactionScope())
            {
                bool isSentForApproval = _expenseApprovalTicketManager.Edit(ticket);

                if (isSentForApproval)
                {
                    ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);

                    isSentSuccess = _expenseApprovalTicketManager.Edit(ticket);
                }
                ts.Complete();

                return isSentSuccess;
            }
        }

        public bool SendRevertedRequisitionsForApproval(AdvanceRequisitionHeader header, string sentByUser)
        {
            if (header == null)
            {
                throw new BllException(
                    "No Requisitions found for Send to approal! Error occurred while trying to send reverted requisition for approval");
            }

            var ticket = _requisitionApprovalTicketManager.GetByAdvanceRequisitionHeaderId(header.Id);

            if (ticket == null)
            {
                throw new BllException(
                    "No Ticket found for existing approval, the requisition is expected to be reverted against an existing approval process! Error occurred while trying to send reverted requisition for approval");
            }
            var requisitionCategory =
                _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);

            if (requisitionCategory.RequisitionApprovalPanel == null)
            {
                throw new BllException("No approval panel is set for requisition cateogry " + requisitionCategory.Name);
            }

            var approvalPanel = requisitionCategory.RequisitionApprovalPanel;

            if (approvalPanel.ApprovalLevels == null || !approvalPanel.ApprovalLevels.Any())
            {
                throw new BllException("No approval level is configured yet for the approval panel " + approvalPanel.Name + ", configure the panel level's properly.");
            }

            var startingLevel =
                approvalPanel.ApprovalLevels.FirstOrDefault(c => c.LevelOrder == Utility.Utility.StartingLevelNo);

            if (startingLevel == null)
            {
                throw new BllException("No starting level found for panel " + approvalPanel.Name + " , please set a level order to " + Utility.Utility.StartingLevelNo + " to identify it as starting level.");
            }
            //setting Panel and level
            ticket.ApprovalPanelId = approvalPanel.Id;
            ticket.ApprovalLevelId = startingLevel.Id;

            bool isSentSuccess = false;
            ticket.Remarks = null;
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.SentForApproval, sentByUser);

            //using (var ts = new TransactionScope())
            //{
            bool isSentForApproval = _requisitionApprovalTicketManager.Edit(ticket);
            bool isSet = SetNextPriorityMember(ticket);

            if (isSet)
            {
                _requisitionApprovalTicketManager.Edit(ticket);
            }
            if (!isSet)
            {
                isSentSuccess = SendToNextLevel(ticket);
            }

            //if (isSentForApproval)
            //{
            //    ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
            //    isSentSuccess = _requisitionApprovalTicketManager.Edit(ticket);
            //}
            //ts.Complete();

            return isSentSuccess;
            //}
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetWaitingForApprovalRequisitionsForMember(string memberUserName)
        {
            List<RequisitionApprovalTicket> usersWaitingForApprovalTickets = new List<RequisitionApprovalTicket>();

            ICollection<Advance_VW_GetAdvanceRequisition> waitingForApprovalRequisitionList = null;

            // get members requisitions by level
            // get tickets  which has no specific destination member and this member is in approval level
            //var withoutDestinationTickets =
            //    _requisitionApprovalTicketManager.GetTicketsForMemberWithoutSpecificDestinationMember(memberUserName,
            //        ApprovalStatusEnum.WaitingForApproval);

            //if (withoutDestinationTickets != null && withoutDestinationTickets.Any())
            //{
            //    usersWaitingForApprovalTickets.AddRange(withoutDestinationTickets);
            //}

            // get tickets which has this member as unit head

            //var unitHeadTickets = _requisitionApprovalTicketManager.GetTicketsForUnitHead(memberUserName,
            //    ApprovalStatusEnum.WaitingForApproval);

            //if (unitHeadTickets != null && unitHeadTickets.Any())
            //{
            //    usersWaitingForApprovalTickets.AddRange(unitHeadTickets);
            //}
            // get tickets which has this member as department head
            //var headOfDeptTickets = _requisitionApprovalTicketManager.GetTicketsForDeptHead(memberUserName,
            //    ApprovalStatusEnum.WaitingForApproval);

            //if (headOfDeptTickets != null && headOfDeptTickets.Any())
            //{
            //    usersWaitingForApprovalTickets.AddRange(headOfDeptTickets);
            //}

            //Adding Dilute Members
            var diluteRequisitionTicketOfMember =
                _requisitionApprovalTicketManager.GetTicketsForDiluteMember(memberUserName, ApprovalStatusEnum.WaitingForApproval);
            if (diluteRequisitionTicketOfMember != null && diluteRequisitionTicketOfMember.Any())
            {
                usersWaitingForApprovalTickets.AddRange(diluteRequisitionTicketOfMember);
            }
            //Taking Distict
            if (usersWaitingForApprovalTickets != null && usersWaitingForApprovalTickets.Any())
            {
                usersWaitingForApprovalTickets = usersWaitingForApprovalTickets.DistinctBy(c => c.Id).ToList();
            }
            if (usersWaitingForApprovalTickets != null && usersWaitingForApprovalTickets.Any())
            {
                var requisitionHeaderIdList = usersWaitingForApprovalTickets.Select(c => c.AdvanceRequisitionHeaderId);
                waitingForApprovalRequisitionList =
                    _advanceRequisitionManager.Get(c => requisitionHeaderIdList.Contains(c.HeaderId)).ToList();
            }
            //filtering if requester or creator is a member of approval level
            if (waitingForApprovalRequisitionList != null && waitingForApprovalRequisitionList.Any())
            {
                waitingForApprovalRequisitionList =
                   waitingForApprovalRequisitionList.Where(c => !c.RequesterUserName.ToLower().Equals(memberUserName.ToLower()) || !c.CreatedBy.ToLower().Equals(memberUserName.ToLower())).ToList();
            }

            return waitingForApprovalRequisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetWaitingForApprovalRequisitionsForMember(string memberUserName, AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                GetWaitingForApprovalRequisitionsForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.DepartmentId != null)
            {
                requisitionList = requisitionList.Where(c => c.EmployeeDepartmentID == criteria.DepartmentId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                requisitionList = requisitionList.Where(c => c.EmployeeName.Contains(criteria.EmployeeName)).ToList();
            }

            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetDraftRequisitionsForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && c.RequistionApprovalTicketId == null && c.IsDeleted == false);
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetDraftRequisitionsForOtherMembers(string createdUserName)
        {
            if (string.IsNullOrEmpty(createdUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.CreatedBy.Equals(createdUserName) && !c.RequesterUserName.Equals(createdUserName)) && c.RequistionApprovalTicketId == null && c.IsDeleted == false);
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetDraftRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = new List<Advance_VW_GetAdvanceRequisition>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                requisitionList = GetDraftRequisitionsForMember(memberUserName);
            }
            else
            {
                requisitionList = GetDraftRequisitionsForOtherMembers(memberUserName);
            }

            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetSentRequisitionsForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && (c.ApprovalStatusId != null && c.ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval));
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetSentRequisitionsForOtherMembers(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (!c.RequesterUserName.Equals(memberUserName) && c.CreatedBy == memberUserName) && (c.ApprovalStatusId != null && c.ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval));
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetSentRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = new List<Advance_VW_GetAdvanceRequisition>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                requisitionList = GetSentRequisitionsForMember(memberUserName);
            }
            else
            {
                requisitionList = GetSentRequisitionsForOtherMembers(memberUserName);
            }
            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisitionsForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && (c.ApprovalStatusId != null && c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved) && (c.IsPaid!=true) && (c.ApprovedOn != null && DbFunctions.DiffDays(c.ApprovedOn, DateTime.Now) <= Utility.Utility.TimeDuration));
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = GetApprovedRequisitionsForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return requisitionList;
        }


        public ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionsForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && (c.IsPaid == true && c.IsReceived!=true  ) && (c.AdvanceIssueDate != null && DbFunctions.DiffDays(c.AdvanceIssueDate, DateTime.Now) <= Utility.Utility.TimeDuration));
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = GetApprovedRequisitionsForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetReceivedRequisitionsForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && (c.IsReceived ==true) && (c.AdvanceIssueDate != null && DbFunctions.DiffDays(c.AdvanceIssueDate, DateTime.Now) <= Utility.Utility.TimeDuration));
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetReceivedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = GetReceivedRequisitionsForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRevertedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            List<RequisitionApprovalTicket> revertedRequisitionApprovalTickets = new List<RequisitionApprovalTicket>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                revertedRequisitionApprovalTickets = _requisitionApprovalTicketManager.GetRevertedTicketsForRequester(memberUserName).ToList();
            }
            else
            {
                revertedRequisitionApprovalTickets = _requisitionApprovalTicketManager.GetRevertedTicketsForOtherMembers(memberUserName).ToList();
            }
            List<Advance_VW_GetAdvanceRequisition> requisitionList = new List<Advance_VW_GetAdvanceRequisition>();
            if (revertedRequisitionApprovalTickets != null && revertedRequisitionApprovalTickets.Any())
            {
                var requisitionHeaderIdList =
               revertedRequisitionApprovalTickets.Select(c => c.AdvanceRequisitionHeaderId).ToList();
                requisitionList =
                    _advanceVwGetAdvanceRequisitionManager.Get(c => requisitionHeaderIdList.Contains(c.HeaderId))
                        .ToList();
                if (criteria.AdvanceCategoryId > 0)
                {
                    requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
                }
                if (criteria.FromDate != null)
                {
                    requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
                }
                if (criteria.ToDate != null)
                {
                    requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
                }
            }

            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRevertedRequisitionsForMember(string memberUserName)
        {
            List<RequisitionApprovalTicket> revertedRequisitionApprovalTickets =
               _requisitionApprovalTicketManager.GetRevertedTicketsForRequester(memberUserName).ToList();
            List<RequisitionApprovalTicket> revertedRequisitionApprovalTicketsForOtherMember =
               _requisitionApprovalTicketManager.GetRevertedTicketsForOtherMembers(memberUserName).ToList();
            revertedRequisitionApprovalTickets.AddRange(revertedRequisitionApprovalTicketsForOtherMember);
            List<Advance_VW_GetAdvanceRequisition> requisitionList = new List<Advance_VW_GetAdvanceRequisition>();
            if (revertedRequisitionApprovalTickets != null && revertedRequisitionApprovalTickets.Any())
            {
                var requisitionHeaderIdList =
               revertedRequisitionApprovalTickets.Select(c => c.AdvanceRequisitionHeaderId).ToList();
                requisitionList =
                    _advanceVwGetAdvanceRequisitionManager.Get(c => requisitionHeaderIdList.Contains(c.HeaderId))
                        .ToList();
            }

            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRevertedRequisitionsForOtherMembers(string memberUserName)
        {
            List<RequisitionApprovalTicket> revertedRequisitionApprovalTickets =
               _requisitionApprovalTicketManager.GetRevertedTicketsForOtherMembers(memberUserName).ToList();
            List<Advance_VW_GetAdvanceRequisition> requisitionList = new List<Advance_VW_GetAdvanceRequisition>();
            if (revertedRequisitionApprovalTickets != null && revertedRequisitionApprovalTickets.Any())
            {
                var requisitionHeaderIdList =
               revertedRequisitionApprovalTickets.Select(c => c.AdvanceRequisitionHeaderId).ToList();
                requisitionList =
                    _advanceVwGetAdvanceRequisitionManager.Get(c => requisitionHeaderIdList.Contains(c.HeaderId))
                        .ToList();
            }

            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRejectedRequisitionsForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(
                    c => (c.RequesterUserName == memberUserName) && (c.ApprovalStatusId != null && c.ApprovalStatusId == (long)ApprovalStatusEnum.Rejected) && (c.RejectedOn != null && DbFunctions.DiffDays(c.RejectedOn, DateTime.Now) <= Utility.Utility.TimeDuration));
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRejectedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = GetRejectedRequisitionsForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetWaitingForApprovalExpensesForMember(string memberUserName)
        {
            List<ExpenseApprovalTicket> usersWaitingForApprovalTickets = new List<ExpenseApprovalTicket>();

            ICollection<Advance_VW_GetAdvanceExpense> waitingForApprovalExpenseList = null;

            // get members requisitions by level
            // get tickets  which has no specific destination member and this member is in approval level
            var withoutDestinationTickets =
                _expenseApprovalTicketManager.GetTicketsForMemberWithoutSpecificDestinationMember(memberUserName,
                    ApprovalStatusEnum.WaitingForApproval);

            if (withoutDestinationTickets != null && withoutDestinationTickets.Any())
            {
                usersWaitingForApprovalTickets.AddRange(withoutDestinationTickets);
            }

            // get tickets which has this member as unit head

            var unitHeadTickets = _expenseApprovalTicketManager.GetTicketsForUnitHead(memberUserName,
                ApprovalStatusEnum.WaitingForApproval);

            if (unitHeadTickets != null && unitHeadTickets.Any())
            {
                usersWaitingForApprovalTickets.AddRange(unitHeadTickets);
            }
            // get tickets which has this member as department head
            var headOfDeptTickets = _expenseApprovalTicketManager.GetTicketsForDeptHead(memberUserName,
                ApprovalStatusEnum.WaitingForApproval);

            if (headOfDeptTickets != null && headOfDeptTickets.Any())
            {
                usersWaitingForApprovalTickets.AddRange(headOfDeptTickets);
            }

            //Adding Dilute Members
            var diluteRequisitionTicketOfMember =
                _expenseApprovalTicketManager.GetTicketsForDiluteMember(memberUserName, ApprovalStatusEnum.WaitingForApproval);
            if (diluteRequisitionTicketOfMember != null && diluteRequisitionTicketOfMember.Any())
            {
                usersWaitingForApprovalTickets.AddRange(diluteRequisitionTicketOfMember);
            }
            //Taking Distict
            if (usersWaitingForApprovalTickets != null && usersWaitingForApprovalTickets.Any())
            {
                usersWaitingForApprovalTickets = usersWaitingForApprovalTickets.DistinctBy(c => c.Id).ToList();
            }

            if (usersWaitingForApprovalTickets != null && usersWaitingForApprovalTickets.Any())
            {
                var expenseHeaderIdList = usersWaitingForApprovalTickets.Select(c => c.AdvanceExpenseHeaderId);
                waitingForApprovalExpenseList =
                    _advanceVwGetAdvanceExpenseManager.Get(c => expenseHeaderIdList.Contains(c.HeaderId)).ToList();
            }
            //filtering if requester or creator is a member of approval level
            if (waitingForApprovalExpenseList != null && waitingForApprovalExpenseList.Any())
            {
                waitingForApprovalExpenseList =
                   waitingForApprovalExpenseList.Where(c => !c.RequesterUserName.ToLower().Equals(memberUserName.ToLower()) || !c.CreatedBy.ToLower().Equals(memberUserName.ToLower())).ToList();
            }

            return waitingForApprovalExpenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetWaitingForApprovalExpensesForMember(string memberUserName, AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList =
                GetWaitingForApprovalExpensesForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.DepartmentId != null)
            {
                expenseList = expenseList.Where(c => c.EmployeeDepartmentID == criteria.DepartmentId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                expenseList = expenseList.Where(c => c.EmployeeName.Contains(criteria.EmployeeName)).ToList();
            }

            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetDraftExpensesForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expensesList =
                _advanceVwGetAdvanceExpenseManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && c.ExpenseApprovalTicketId == null && c.IsDeleted == false);
            return expensesList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetDraftExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expensesList = new List<Advance_VW_GetAdvanceExpense>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                expensesList = GetDraftExpensesForMember(memberUserName);
            }
            else
            {
                expensesList = GetDraftExpensesForMember(memberUserName);
            }

            if (criteria.AdvanceCategoryId > 0)
            {
                expensesList = expensesList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expensesList = expensesList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expensesList = expensesList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return expensesList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetDraftExpensesForOtherMembers(string createdUserName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetSentExpensesForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expensesList =
                _advanceVwGetAdvanceExpenseManager.Get(
                    c =>
                        (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) &&
                        (c.ApprovalStatusId != null &&
                         c.ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval));
            return expensesList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetSentExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList = new List<Advance_VW_GetAdvanceExpense>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                expenseList = GetSentExpensesForMember(memberUserName);
            }
            else
            {
                expenseList = GetSentExpensesForMember(memberUserName);
            }
            if (criteria.AdvanceCategoryId > 0)
            {
                expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetSentExpensesForOtherMembers(string memberUserName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpensesForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList = _advanceVwGetAdvanceExpenseManager.Get(
                c =>
                    (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) &&
                    (c.ApprovalStatusId != null && c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved && c.IsPaid!=true) && (c.ApprovedOn != null && DbFunctions.DiffDays(c.ApprovedOn, DateTime.Now) <= Utility.Utility.TimeDuration)
                );
            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList = GetApprovedExpensesForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetRevertedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            List<ExpenseApprovalTicket> revertedExpenseApprovalTickets = new List<ExpenseApprovalTicket>();
            if (criteria.IsRequisitionForLoggedInUser)
            {
                revertedExpenseApprovalTickets = _expenseApprovalTicketManager.GetRevertedTicketsForRequester(memberUserName).ToList();
            }
            //else
            //{
            //    revertedExpenseApprovalTickets = _requisitionApprovalTicketManager.GetRevertedTicketsForOtherMembers(memberUserName).ToList();
            //}
            List<Advance_VW_GetAdvanceExpense> expenseList = new List<Advance_VW_GetAdvanceExpense>();
            if (revertedExpenseApprovalTickets != null && revertedExpenseApprovalTickets.Any())
            {
                var expenseHeaderIdList =
               revertedExpenseApprovalTickets.Select(c => c.AdvanceExpenseHeaderId).ToList();
                expenseList =
                    _advanceVwGetAdvanceExpenseManager.Get(c => expenseHeaderIdList.Contains(c.HeaderId))
                        .ToList();
                if (criteria.AdvanceCategoryId > 0)
                {
                    expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
                }
                if (criteria.FromDate != null)
                {
                    expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
                }
                if (criteria.ToDate != null)
                {
                    expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
                }
            }

            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetRevertedExpensesForMember(string memberUserName)
        {

            List<ExpenseApprovalTicket> revertedExpenseApprovalTickets =
                _expenseApprovalTicketManager.GetRevertedTicketsForRequester(memberUserName).ToList();

            List<Advance_VW_GetAdvanceExpense> expensesList = new List<Advance_VW_GetAdvanceExpense>();
            if (revertedExpenseApprovalTickets != null && revertedExpenseApprovalTickets.Any())
            {
                var expenseHeaderIdList =
               revertedExpenseApprovalTickets.Select(c => c.AdvanceExpenseHeaderId).ToList();
                expensesList =
                    _advanceVwGetAdvanceExpenseManager.Get(c => expenseHeaderIdList.Contains(c.HeaderId))
                        .ToList();
            }

            return expensesList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetRevertedExpensesForOtherMembers(string memberUserName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetRejectedExpensesForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList = _advanceVwGetAdvanceExpenseManager.Get(
                c =>
                    (c.RequesterUserName == memberUserName) &&
                    (c.ApprovalStatusId != null && c.ApprovalStatusId == (long)ApprovalStatusEnum.Rejected) && (c.RejectedOn != null && DbFunctions.DiffDays(c.RejectedOn, DateTime.Now) <= Utility.Utility.TimeDuration)
                );
            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetRejectedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList = GetRejectedExpensesForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionForMember(AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                GetApprovedRequisition(criteria);

            if (requisitionList != null)
            {
                requisitionList = requisitionList.Where(c => c.IsPaid == true).ToList();

            }

            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionForMember()
        {
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                GetApprovedRequisition();
            if (requisitionList != null)
            {
                requisitionList = requisitionList.Where(c => c.IsPaid == true).ToList();

            }
            List<long> notPaidAdvanceRequisitionHeaderId = new List<long>();
            foreach (Advance_VW_GetAdvanceRequisition advanceVwGetAdvanceRequisition in requisitionList)
            {
                var requisitonHeader = _advanceRequisitionHeaderManager.GetById(advanceVwGetAdvanceRequisition.HeaderId);
                foreach (AdvanceRequisitionDetail detail in requisitonHeader.AdvanceRequisitionDetails)
                {
                    if (detail.RequisitionVoucherDetailId == null)
                    {
                        notPaidAdvanceRequisitionHeaderId.Add(requisitonHeader.Id);
                    }
                }
            }
            requisitionList =
                requisitionList.Where(c => notPaidAdvanceRequisitionHeaderId.Contains(c.HeaderId)).ToList();
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetUnPaidRequisitionForMember(AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                GetApprovedRequisition(criteria);

            if (requisitionList != null)
            {
                requisitionList = requisitionList.Where(c => c.IsPaid == false).ToList();

            }
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetUnPaidRequisitionForMember()
        {
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                GetApprovedRequisition();

            if (requisitionList != null)
            {
                requisitionList = requisitionList.Where(c => c.IsPaid == false).ToList();

            }
            return requisitionList;
        }


        public ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember(AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> paidExpenseList = GetApprovedExpenses(criteria);
            if (paidExpenseList != null)
            {
                paidExpenseList = paidExpenseList.Where(c => c.IsPaid == true).ToList();

            }
            return paidExpenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember()
        {
            ICollection<Advance_VW_GetAdvanceExpense> paidExpenseList = GetApprovedExpenses();
            if (paidExpenseList != null)
            {
                paidExpenseList = paidExpenseList.Where(c => c.IsPaid == true).ToList();
            }
            List<long> notPaidAdvanceExpenseHeaderId = new List<long>();
            foreach (Advance_VW_GetAdvanceExpense advanceVwGetAdvanceExpense in paidExpenseList)
            {
                var expenseHeader = _advanceExpenseHeaderManager.GetById(advanceVwGetAdvanceExpense.HeaderId);
                foreach (AdvanceExpenseDetail detail in expenseHeader.AdvanceExpenseDetails)
                {
                    if (detail.ExpenseVoucherDetailId == null)
                    {
                        notPaidAdvanceExpenseHeaderId.Add(expenseHeader.Id);
                    }
                }
            }
            paidExpenseList =
                paidExpenseList.Where(c => notPaidAdvanceExpenseHeaderId.Contains(c.HeaderId)).ToList();
            return paidExpenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList = GetPaidExpensesForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return expenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember(string memberUserName)
        {

           ICollection<Advance_VW_GetAdvanceExpense> paidExpenseList = _advanceVwGetAdvanceExpenseManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && (c.IsPaid == true && c.IsReceived != true));

           // ICollection<Advance_VW_GetAdvanceExpense> paidExpenseList = GetApprovedExpenses();
            //if (paidExpenseList != null)
            //{
            //    paidExpenseList = paidExpenseList.Where(c => c.IsPaid == true && c.RequesterUserName == memberUserName).ToList();
            //}
            List<long> notPaidAdvanceExpenseHeaderId = new List<long>();
            foreach (Advance_VW_GetAdvanceExpense advanceVwGetAdvanceExpense in paidExpenseList)
            {
                var expenseHeader = _advanceExpenseHeaderManager.GetById(advanceVwGetAdvanceExpense.HeaderId);
                foreach (AdvanceExpenseDetail detail in expenseHeader.AdvanceExpenseDetails)
                {
                    if (detail.ExpenseVoucherDetailId == null)
                    {
                        notPaidAdvanceExpenseHeaderId.Add(expenseHeader.Id);
                    }
                }
            }
            paidExpenseList =
                paidExpenseList.Where(c => notPaidAdvanceExpenseHeaderId.Contains(c.HeaderId)).ToList();
            return paidExpenseList;
        }
       
        public ICollection<Advance_VW_GetAdvanceExpense> GetReceivedExpensesForMember(string memberUserName)
        {
            if (string.IsNullOrEmpty(memberUserName))
            {
                throw new BllException("Member user name not found.");
            }
            ICollection<Advance_VW_GetAdvanceExpense >expenseList =
                _advanceVwGetAdvanceExpenseManager.Get(
                    c => (c.RequesterUserName == memberUserName || c.CreatedBy == memberUserName) && (c.IsReceived == true));
            return expenseList;

        }
        public ICollection<Advance_VW_GetAdvanceExpense> GetReceivedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList = GetReceivedExpensesForMember(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            return expenseList;
        }
        public ICollection<Advance_VW_GetAdvanceExpense> GetUnpaidExpensesForMember(AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> unPaidExpenseList = GetApprovedExpenses(criteria);
            if (unPaidExpenseList != null)
            {
                unPaidExpenseList = unPaidExpenseList.Where(c => c.IsPaid == false).ToList();

            }
            return unPaidExpenseList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetUnpaidExpensesForMember()
        {
            ICollection<Advance_VW_GetAdvanceExpense> unPaidExpenseList = GetApprovedExpenses();
            if (unPaidExpenseList != null)
            {
                unPaidExpenseList = unPaidExpenseList.Where(c => c.IsPaid == false).ToList();

            }
            return unPaidExpenseList;
        }

        //public ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpenses()
        //{
        //    ICollection<Advance_VW_GetAdvanceExpense> expensesList =
        //       _advanceVwGetAdvanceExpenseManager.Get(c => c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved && c.IsPaid!=true);
        //    return expensesList;
        //}
        public ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpenses()
        {
            ICollection<Advance_VW_GetAdvanceExpense> expensesList =
                _advanceVwGetAdvanceExpenseManager.Get(c => c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved);
            return expensesList;
        }
        public ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisition(
            AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(c => c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved);

            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.DepartmentId != null)
            {
                requisitionList = requisitionList.Where(c => c.EmployeeDepartmentID == criteria.DepartmentId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                requisitionList = requisitionList.Where(c => c.EmployeeName.Contains(criteria.EmployeeName)).ToList();
            }
            List<long> remainingRequisitionVoucherEntryList = new List<long>();
            foreach (Advance_VW_GetAdvanceRequisition advanceVwGetAdvanceRequisition in requisitionList)
            {
                var requisitionHeader = _advanceRequisitionHeaderManager.GetById(advanceVwGetAdvanceRequisition.HeaderId);
                foreach (AdvanceRequisitionDetail detail in requisitionHeader.AdvanceRequisitionDetails)
                {
                    if (detail.RequisitionVoucherDetailId == null)
                    {
                        remainingRequisitionVoucherEntryList.Add(requisitionHeader.Id);
                    }
                }
            }
            requisitionList =
                requisitionList.Where(c => remainingRequisitionVoucherEntryList.Contains(c.HeaderId)).ToList();
            return requisitionList;

        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisition()
        {
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
               _advanceVwGetAdvanceRequisitionManager.Get(c => c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved);
            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpenses(
            AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expensesList =
                _advanceVwGetAdvanceExpenseManager.Get(c => c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved);

            if (criteria.AdvanceCategoryId > 0)
            {
                expensesList = expensesList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.DepartmentId != null)
            {
                expensesList = expensesList.Where(c => c.EmployeeDepartmentID == criteria.DepartmentId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expensesList = expensesList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expensesList = expensesList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                expensesList = expensesList.Where(c => c.EmployeeName.Contains(criteria.EmployeeName)).ToList();
            }
            List<long> remainingExpenseVoucherEntryList = new List<long>();
            foreach (Advance_VW_GetAdvanceExpense advanceVwGetAdvanceExpense in expensesList)
            {
                var expenseHeader = _advanceExpenseHeaderManager.GetById(advanceVwGetAdvanceExpense.HeaderId);
                foreach (AdvanceExpenseDetail detail in expenseHeader.AdvanceExpenseDetails)
                {
                    if (detail.ExpenseVoucherDetailId == null)
                    {
                        remainingExpenseVoucherEntryList.Add(expenseHeader.Id);
                    }
                }
            }
            expensesList =
                expensesList.Where(c => remainingExpenseVoucherEntryList.Contains(c.HeaderId)).ToList();
            return expensesList;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRequisitionsByApprovedBy(string memberUserName)
        {
            ICollection<RequisitionApprovalTracker> trackerList =
                _requisitionApprovalTrackerManager.GetByAuthorizedBy(memberUserName).DistinctBy(c => c.RequisitionApprovalTicket.AdvanceRequisitionHeaderId).ToList();

            List<long> headerIdList =
                trackerList.Select(c => c.RequisitionApprovalTicket.AdvanceRequisitionHeaderId).ToList();

            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                _advanceVwGetAdvanceRequisitionManager.Get(c => headerIdList.Contains(c.HeaderId));

            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetExpensesByApprovedBy(string memberUserName)
        {
            ICollection<ExpenseApprovalTracker> trackerList =
                _expenseApprovalTrackerManager.GetByAuthorizedBy(memberUserName).DistinctBy(c => c.ExpenseApprovalTicket.AdvanceExpenseHeaderId).ToList();

            List<long> headerIdList =
                trackerList.Select(c => c.ExpenseApprovalTicket.AdvanceExpenseHeaderId).ToList();

            ICollection<Advance_VW_GetAdvanceExpense> expenses =
                _advanceVwGetAdvanceExpenseManager.Get(c => headerIdList.Contains(c.HeaderId));

            return expenses;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRequisitionsByApprovedBy(string memberUserName, AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = GetRequisitionsByApprovedBy(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                requisitionList = requisitionList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.DepartmentId != null)
            {
                requisitionList = requisitionList.Where(c => c.EmployeeDepartmentID == criteria.DepartmentId).ToList();
            }
            if (criteria.FromDate != null)
            {
                requisitionList = requisitionList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                requisitionList = requisitionList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                requisitionList = requisitionList.Where(c => c.EmployeeName.Contains(criteria.EmployeeName)).ToList();
            }

            return requisitionList;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetExpensesByApprovedBy(string memberUserName, AdvanceRequisitionSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new BllException("Search criteria not set.");
            }
            ICollection<Advance_VW_GetAdvanceExpense> expenseList =
                GetExpensesByApprovedBy(memberUserName);
            if (criteria.AdvanceCategoryId > 0)
            {
                expenseList = expenseList.Where(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId).ToList();
            }
            if (criteria.DepartmentId != null)
            {
                expenseList = expenseList.Where(c => c.EmployeeDepartmentID == criteria.DepartmentId).ToList();
            }
            if (criteria.FromDate != null)
            {
                expenseList = expenseList.Where(c => c.FromDate.Date >= criteria.FromDate.Value.Date).ToList();
            }
            if (criteria.ToDate != null)
            {
                expenseList = expenseList.Where(c => c.ToDate.Date <= criteria.ToDate.Value.Date).ToList();
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                expenseList = expenseList.Where(c => c.EmployeeName.Contains(criteria.EmployeeName)).ToList();
            }

            return expenseList;
        }

        public bool SetNextPriorityMember(RequisitionApprovalTicket ticket, int priority = 1)
        {
            var requisitionHeader = _advanceRequisitionHeaderManager.GetById(ticket.AdvanceRequisitionHeaderId);
            var approvalLevel = _approvalLevelManager.GetById(ticket.ApprovalLevelId);

            if (priority == 1 && approvalLevel.IsLineSupervisor)
            {
                var lineSupervisor = _employeeManager.GetById((long)requisitionHeader.RequesterSupervisorId.Value);
                if (!_employeeLeaveManager.IsEmployeeOnLeave(lineSupervisor.UserName))
                {
                    var destinationUserForTicket = new DestinationUserForTicket
                    {
                        ApprovalLevelId = approvalLevel.Id,
                        ApprovalPanelId = approvalLevel.ApprovalPanelId,
                        ApprovalTicketId = ticket.Id,
                        DestinationUserName = lineSupervisor.UserName
                    };

                    if (ticket.DestinationUserForTickets == null)
                    {
                        ticket.DestinationUserForTickets = new List<DestinationUserForTicket>();
                    }

                    ticket.DestinationUserForTickets.Add(destinationUserForTicket);
                    ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
                    return true;
                }
                priority++;
            }

            if (priority == 1 && approvalLevel.IsHeadOfDepartment)
            {
                var deptHead = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(requisitionHeader.RequesterUserName);
                if (!_employeeLeaveManager.IsEmployeeOnLeave(deptHead.HeadOfDepartmentUserName))
                {
                    var destinationUserForTicket = new DestinationUserForTicket
                    {
                        ApprovalLevelId = approvalLevel.Id,
                        ApprovalPanelId = approvalLevel.ApprovalPanelId,
                        ApprovalTicketId = ticket.Id,
                        DestinationUserName = deptHead.HeadOfDepartmentUserName
                    };

                    if (ticket.DestinationUserForTickets == null)
                    {
                        ticket.DestinationUserForTickets = new List<DestinationUserForTicket>();
                    }

                    ticket.DestinationUserForTickets.Add(destinationUserForTicket);
                    ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
                    return true;
                }
                priority++;
            }

            var member = _approvalLevelMemberManager.GetByLevelAndPriority(ticket.ApprovalLevelId, priority);
            if (member == null)
            {
                ticket.UpdateTicketWithTracker(ApprovalStatusEnum.ApprovalSkipped);
                return false;
            }
            if (member.EmployeeUserName.Equals(requisitionHeader.RequesterUserName) ||
                member.EmployeeUserName.Equals(requisitionHeader.CreatedBy))
            {
                ticket.UpdateTicketWithTracker(ApprovalStatusEnum.ApprovalSkipped);

                return SetNextPriorityMember(ticket, ++priority);
            }
            var isMemberOnLeave = _employeeLeaveManager.IsEmployeeOnLeave(member.EmployeeUserName);
            if (isMemberOnLeave)
            {
                ticket.UpdateTicketWithTracker(ApprovalStatusEnum.ApprovalSkipped);
                return SetNextPriorityMember(ticket, ++priority);
            }

            var userForTicket = new DestinationUserForTicket
            {
                ApprovalLevelId = approvalLevel.Id,
                ApprovalPanelId = approvalLevel.ApprovalPanelId,
                ApprovalTicketId = ticket.Id,
                DestinationUserName = member.EmployeeUserName
            };

            if (ticket.DestinationUserForTickets == null)
            {
                ticket.DestinationUserForTickets = new List<DestinationUserForTicket>();
            }

            ticket.DestinationUserForTickets.Add(userForTicket);
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
            return true;
        }

        public bool SetNextPriorityMember(ExpenseApprovalTicket ticket, int priority = 1)
        {
            var expenseHeader = _advanceExpenseHeaderManager.GetById(ticket.AdvanceExpenseHeaderId);
            var approvalLevel = _approvalLevelManager.GetById(ticket.ApprovalLevelId);

            if (priority == 1 && approvalLevel.IsLineSupervisor)
            {
                var lineSupervisor = _employeeManager.GetById((long)expenseHeader.RequesterSupervisorId.Value);
                if (!_employeeLeaveManager.IsEmployeeOnLeave(lineSupervisor.UserName))
                {
                    var destinationUserForTicket = new DestinationUserForTicket
                    {
                        ApprovalLevelId = approvalLevel.Id,
                        ApprovalPanelId = approvalLevel.ApprovalPanelId,
                        ApprovalTicketId = ticket.Id,
                        DestinationUserName = lineSupervisor.UserName
                    };

                    if (ticket.DestinationUserForTickets == null)
                    {
                        ticket.DestinationUserForTickets = new List<DestinationUserForTicket>();
                    }

                    ticket.DestinationUserForTickets.Add(destinationUserForTicket);
                    ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
                    return true;
                }
            }

            if (priority == 1 && approvalLevel.IsHeadOfDepartment)
            {
                var deptHead = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(expenseHeader.RequesterUserName);
                if (!_employeeLeaveManager.IsEmployeeOnLeave(deptHead.HeadOfDepartmentUserName))
                {
                    var destinationUserForTicket = new DestinationUserForTicket
                    {
                        ApprovalLevelId = approvalLevel.Id,
                        ApprovalPanelId = approvalLevel.ApprovalPanelId,
                        ApprovalTicketId = ticket.Id,
                        DestinationUserName = deptHead.HeadOfDepartmentUserName
                    };

                    if (ticket.DestinationUserForTickets == null)
                    {
                        ticket.DestinationUserForTickets = new List<DestinationUserForTicket>();
                    }

                    ticket.DestinationUserForTickets.Add(destinationUserForTicket);
                    ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
                    return true;
                }
            }

            var member = _approvalLevelMemberManager.GetByLevelAndPriority(ticket.ApprovalLevelId, priority);
            if (member == null)
            {
                ticket.UpdateTicketWithTracker(ApprovalStatusEnum.ApprovalSkipped);
                return false;
            }
            if (member.EmployeeUserName.Equals(expenseHeader.RequesterUserName) ||
                member.EmployeeUserName.Equals(expenseHeader.CreatedBy))
            {
                ticket.UpdateTicketWithTracker(ApprovalStatusEnum.ApprovalSkipped);

                return SetNextPriorityMember(ticket, ++priority);
            }
            var isMemberOnLeave = _employeeLeaveManager.IsEmployeeOnLeave(member.EmployeeUserName);
            if (isMemberOnLeave)
            {
                ticket.UpdateTicketWithTracker(ApprovalStatusEnum.ApprovalSkipped);
                return SetNextPriorityMember(ticket, ++priority);
            }

            var userForTicket = new DestinationUserForTicket
            {
                ApprovalLevelId = approvalLevel.Id,
                ApprovalPanelId = approvalLevel.ApprovalPanelId,
                ApprovalTicketId = ticket.Id,
                DestinationUserName = member.EmployeeUserName
            };

            if (ticket.DestinationUserForTickets == null)
            {
                ticket.DestinationUserForTickets = new List<DestinationUserForTicket>();
            }

            ticket.DestinationUserForTickets.Add(userForTicket);
            ticket.UpdateTicketWithTracker(ApprovalStatusEnum.WaitingForApproval);
            return true;
        }

        public bool Forward(RequisitionApprovalTicket ticket)
        {
            var approvalLevel = _approvalLevelManager.GetById(ticket.ApprovalLevelId);

            int nextPriority = 0;
            var currentDestinationMember = _destinationUserForTicketManager.GetBy(ticket.Id, ticket.ApprovalLevelId,
                ticket.ApprovalPanelId).LastOrDefault();

            if (currentDestinationMember != null)
            {
                var levelMember =
                approvalLevel.ApprovalLevelMembers.FirstOrDefault(
                    c => !c.IsDeleted && c.EmployeeUserName.ToLower().Equals(currentDestinationMember.DestinationUserName.ToLower()));
                if (levelMember == null)
                {
                    nextPriority = 2;
                }
                else
                {
                    nextPriority = levelMember.PriorityOrder + 1;
                }
            }

            bool isSet = SetNextPriorityMember(ticket, nextPriority);
            if (isSet)
            {
                _requisitionApprovalTicketManager.Edit(ticket);
                return true;
            }
            return false;
        }

        public bool Forward(ExpenseApprovalTicket ticket)
        {
            var approvalLevel = _approvalLevelManager.GetById(ticket.ApprovalLevelId);

            int nextPriority = 0;
            var currentDestinationMember = _destinationUserForTicketManager.GetBy(ticket.Id, ticket.ApprovalLevelId,
                ticket.ApprovalPanelId).LastOrDefault();

            if (currentDestinationMember != null)
            {
                var levelMember =
                approvalLevel.ApprovalLevelMembers.FirstOrDefault(
                    c => !c.IsDeleted && c.EmployeeUserName.ToLower().Equals(currentDestinationMember.DestinationUserName.ToLower()));
                if (levelMember == null)
                {
                    nextPriority = 2;
                }
                else
                {
                    nextPriority = levelMember.PriorityOrder + 1;
                }
            }

            bool isSet = SetNextPriorityMember(ticket, nextPriority);
            if (isSet)
            {
                _expenseApprovalTicketManager.Edit(ticket);
                return true;
            }
            return false;
        }

        public bool Move(RequisitionApprovalTicket ticket, string username)
        {
            if (!username.ToLower().Equals("admin"))
            {
                throw new BllException("Sorry! Only admin can move a requisition.");
            }
            bool isForwarded = Forward(ticket);
            bool isMoved = false;
            if (!isForwarded)
            {
                isMoved = SendToNextLevel(ticket);
            }
            return isMoved || isForwarded;
        }

        public bool Move(ExpenseApprovalTicket ticket, string username)
        {
            if (!username.ToLower().Equals("admin"))
            {
                throw new BllException("Sorry! Only admin can move an adjustment.");
            }
            bool isForwarded = Forward(ticket);
            bool isMoved = false;
            if (!isForwarded)
            {
                isMoved = SendToNextLevel(ticket);
            }
            return isMoved || isForwarded;
        }
    }
}
