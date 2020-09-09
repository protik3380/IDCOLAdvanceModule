using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IRequisitionApprovalTicketManager : IManager<RequisitionApprovalTicket>
    {
        RequisitionApprovalTicket GetByAdvanceRequisitionHeaderId(long advanceRequisitionHeaderId);
        RequisitionApprovalTicket GetByAdvanceRequisitionHeaderIdWithNotApproved(long advanceRequisitionHeaderId);
        ICollection<RequisitionApprovalTicket> GetAllByApprovePanelIdAndApproveLevelId(int approvePanelId, int approveLevelId);
        ICollection<RequisitionApprovalTicket> GetTicketsForUnitHead(string memberUserName, ApprovalStatusEnum approvalStatus);
        ICollection<RequisitionApprovalTicket> GetTicketsForDeptHead(string memberUserName, ApprovalStatusEnum approvalStatus);
        //ICollection<RequisitionApprovalTicket> GetTicketsForDestinationMember(string memberUserName, ApprovalStatusEnum approvalStatus);
        ICollection<RequisitionApprovalTicket> GetRevertedTicketsForRequester(string requesterUserName);
        ICollection<RequisitionApprovalTicket> GetRevertedTicketsForOtherMembers(string requesterUserName);
        ICollection<RequisitionApprovalTicket> GetTicketsForMemberWithoutSpecificDestinationMember(string memberUserName, ApprovalStatusEnum waitingForApproval);
        DateTime? GetAuthorizedOnById(long? requisitionApprovalTicketId);
        ICollection<RequisitionApprovalTicket> GetTicketsForDiluteMember(string memberUserName,ApprovalStatusEnum approvalStatus);
    }
}
