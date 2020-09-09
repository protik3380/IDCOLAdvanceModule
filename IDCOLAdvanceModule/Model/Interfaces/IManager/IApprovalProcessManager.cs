using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.SearchModels;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IApprovalProcessManager
    {
        RequisitionApprovalTicket SendRequisitionForApproval(AdvanceRequisitionHeader requisition, string sentByUserName);
        ICollection<RequisitionApprovalTicket> SendRequisitionForApproval(ICollection<AdvanceRequisitionHeader> requisitionHeaders, string sentByUserName);

        ExpenseApprovalTicket SendExpenseForApproval(AdvanceExpenseHeader requisition, string sentByUserName);
        ICollection<ExpenseApprovalTicket> SendExpenseForApproval(ICollection<AdvanceExpenseHeader> requisitionHeaders, string sentByUserName);

        bool SendToNextLevel(RequisitionApprovalTicket approvalTicket);
        bool SendToNextLevel(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets);
        bool SendToNextLevel(ExpenseApprovalTicket expenseApprovalTicket);
        bool SendToNextLevel(ICollection<ExpenseApprovalTicket> expenseApprovalTicket);

        bool Approve(RequisitionApprovalTicket requisitionApprovalTicket, string approveByUserName);
        bool Approve(ExpenseApprovalTicket expenseApprovalTicket, string approveByUserName);
        bool Approve(RequisitionApprovalTicket requisitionApprovalTicket,
            AdvanceRequisitionHeader advanceRequisitionHeader, string approveByUserName);
        bool Approve(ExpenseApprovalTicket expenseApprovalTicket,
            AdvanceExpenseHeader advanceExpenseHeader, string approveByUserName);
        bool Approve(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets, string approveByUserName);
        bool Approve(ICollection<ExpenseApprovalTicket> expenseApprovalTickets, string approveByUserName);

        bool Revert(RequisitionApprovalTicket requisitionApprovalTicket, string revertedByUserName);
        bool Revert(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets, string revertedByUserName);
        bool Revert(ExpenseApprovalTicket expenseApprovalTicket, string revertedByUserName);
        bool Revert(ICollection<ExpenseApprovalTicket> requisitionApprovalTickets, string revertedByUserName);

        bool Reject(RequisitionApprovalTicket requisitionApprovalTicket, string rejectedByUserName);
        bool Reject(ICollection<RequisitionApprovalTicket> requisitionApprovalTickets, string rejectedByUserName);
        bool Reject(ExpenseApprovalTicket expenseApprovalTicket, string rejectedByUserName);
        bool Reject(ICollection<ExpenseApprovalTicket> expenseApprovalTickets, string rejectedByUserName);

        bool SendRevertedRequisitionsForApproval(ICollection<AdvanceRequisitionHeader> headers, string sentByUser);
        bool SendRevertedRequisitionsForApproval(AdvanceRequisitionHeader header, string sentByUser);
        bool SendRevertedExpensesForApproval(ICollection<AdvanceExpenseHeader> headers, string sentByUser);
        bool SendRevertedExpensesForApproval(AdvanceExpenseHeader header, string sentByUser);

        ICollection<Advance_VW_GetAdvanceRequisition> GetWaitingForApprovalRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetWaitingForApprovalRequisitionsForMember(string memberUserName, AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetDraftRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetDraftRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetDraftRequisitionsForOtherMembers(string createdUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetSentRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetSentRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetSentRequisitionsForOtherMembers(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRevertedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetReceivedRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetReceivedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRevertedRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRevertedRequisitionsForOtherMembers(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRejectedRequisitionsForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRejectedRequisitionsForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);

        ICollection<Advance_VW_GetAdvanceExpense> GetWaitingForApprovalExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetWaitingForApprovalExpensesForMember(string memberUserName, AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetDraftExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetDraftExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetDraftExpensesForOtherMembers(string createdUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetSentExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetSentExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetSentExpensesForOtherMembers(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetRevertedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetRevertedExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetRevertedExpensesForOtherMembers(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetRejectedExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetRejectedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionForMember(AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetPaidRequisitionForMember();
        ICollection<Advance_VW_GetAdvanceRequisition> GetUnPaidRequisitionForMember(AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetUnPaidRequisitionForMember();
        ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember(AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember();
        ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetPaidExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);

        ICollection<Advance_VW_GetAdvanceExpense> GetReceivedExpensesForMember(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetReceivedExpensesForMember(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetUnpaidExpensesForMember(AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetUnpaidExpensesForMember();
        ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpenses(
            AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetApprovedExpenses();

        ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisition(
            AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetApprovedRequisition();

        ICollection<ApprovalLevelMember> GetNextApprovalLevelMembersForRequisition(long approvalLevelId, long approvalPanelId,
            long requisitionHeaderId);
        ICollection<ApprovalLevelMember> GetNextApprovalLevelMembersForExpense(long approvalLevelId, long approvalPanelId, long expenseHeaderId);
        bool RemoveRequisition(ICollection<AdvanceRequisitionHeader> headers);
        bool RemoveRequisition(AdvanceRequisitionHeader header);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRemovedRequisitions(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRemovedRequisitionsForOthers(string createdUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRemovedRequisitionsForMember(string memberUserName);
        bool RemoveExpense(ICollection<AdvanceExpenseHeader> headers);
        bool RemovExpense(AdvanceExpenseHeader header);
        ICollection<Advance_VW_GetAdvanceExpense> GetRemovedExpenses(string memberUserName, EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetRemovedExpensesForMember(string memberUserName);

        ICollection<Advance_VW_GetAdvanceRequisition> GetRequisitionsByApprovedBy(string memberUserName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRequisitionsByApprovedBy(string memberUserName, AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceExpense> GetExpensesByApprovedBy(string memberUserName);
        ICollection<Advance_VW_GetAdvanceExpense> GetExpensesByApprovedBy(string memberUserName, AdvanceRequisitionSearchCriteria criteria);

        bool SetNextPriorityMember(RequisitionApprovalTicket ticket, int priority = 1);
        bool SetNextPriorityMember(ExpenseApprovalTicket ticket, int priority = 1);

        bool Forward(RequisitionApprovalTicket ticket);
        bool Forward(ExpenseApprovalTicket ticket);

        bool Move(RequisitionApprovalTicket ticket, string username);
        bool Move(ExpenseApprovalTicket ticket, string username);
    }
}
