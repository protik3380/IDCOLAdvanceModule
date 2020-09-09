using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IExpenseApprovalTicketManager : IManager.BaseManager.IManager<ExpenseApprovalTicket>
    {
        ExpenseApprovalTicket GetByExpenseHeaderId(long id);
        ICollection<ExpenseApprovalTicket> GetExpenseTicketsForRequester(string requesterUserName);
        ICollection<ExpenseApprovalTicket> GetRevertedTicketsForRequester(string memberUserName);
        ICollection<ExpenseApprovalTicket> GetTicketsForDestinationMember(string memberUserName, ApprovalStatusEnum approvalStatus);
        ICollection<ExpenseApprovalTicket> GetTicketsForDeptHead(string memberUserName, ApprovalStatusEnum approvalStatus);
        ICollection<ExpenseApprovalTicket> GetTicketsForUnitHead(string memberUserName, ApprovalStatusEnum approvalStatus);
        ICollection<ExpenseApprovalTicket> GetTicketsForMemberWithoutSpecificDestinationMember(string memberUserName, ApprovalStatusEnum waitingForApproval);
        ExpenseApprovalTicket GetByAdvanceExpenseHeaderIdWithNotApproved(long expneseHeaderId);
        DateTime? GetAuthorizedOnById(long? expenseApprovalTicketId);
        IEnumerable<ExpenseApprovalTicket> GetTicketsForDiluteMember(string memberUserName, ApprovalStatusEnum approvaStatus);
    }
}
