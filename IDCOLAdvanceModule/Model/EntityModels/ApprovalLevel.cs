using System;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.IModel;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ApprovalLevel : IAudit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int LevelOrder { get; set; }
        public long ApprovalPanelId { get; set; }
        public bool IsDiluteApplicable { get; set; }
        public bool IsLineSupervisor { get; set; }
        public bool IsHeadOfDepartment { get; set; }
        public bool IsSourceOfFundEntry { get; set; }
        public bool IsSourceOfFundVerify { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsApprovalAuthority { get; set; }

        public ApprovalPanel ApprovalPanel { get; set; }
        public ICollection<RequisitionApprovalTicket> RequisitionApprovalTickets { get; set; }
        public ICollection<RequisitionApprovalTracker> RequisitionApprovalTrackers { get; set; }
        public ICollection<ApprovalLevelMember> ApprovalLevelMembers { get; set; }
        public ICollection<ApplicationNotification> ApplicationNotifications { get; set; }
        public ICollection<EmailNotification> EmailNotifications { get; set; }

        public bool HasMembers()
        {
            bool hasMembers = ApprovalLevelMembers != null && ApprovalLevelMembers.Any();

            return hasMembers;
        }
    }
}
