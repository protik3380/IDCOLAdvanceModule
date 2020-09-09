using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Base
{
    public abstract class ApprovalTicket
    {
        public long Id { get; set; }
        public long TicketTypeId { get; set; }
        public string RequesterUserName { get; set; }
        public long ApprovalStatusId { get; set; }
        public long ApprovalPanelId { get; set; }
        public long ApprovalLevelId { get; set; }
        public string Remarks { get; set; }
        public string TicketNarration { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime AuthorizedOn { get; set; }

        public List<DestinationUserForTicket> DestinationUserForTickets { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }
        public virtual ApprovalPanel ApprovalPanel { get; set; }
        public virtual ApprovalLevel ApprovalLevel { get; set; }
        public virtual ICollection<ApprovalTracker> ApprovalTrackers { get; set; }
        public virtual TicketType TicketType { get; set; }
        public ICollection<ApplicationNotification> ApplicationNotifications { get; set; }
        public ICollection<EmailNotification> EmailNotifications { get; set; }

        [NotMapped]
        public ApprovalStatusEnum ApprovalStatusEnum { get; set; }
        public abstract ApprovalTracker GetTrackerInstance();
        public abstract void SetTicketNarration(string message = "");
        public abstract void UpdateTicketWithTracker(ApprovalStatusEnum requisitionStatusEnum);
        public abstract bool SetTrackerInstance();
        public abstract void UpdateTicketWithTracker(ApprovalStatusEnum approvalStatus, string approveByUserName);

        public abstract string GetNotificationMessage(UserTable requesterInfo, UserTable approvalAuthorityInfo);

        public abstract ICollection<ApplicationNotification> GetNotification(IEnumerable<Advance_VW_GetApprovalLevelMember> notifyingUserList, 
            UserTable requesterInfo, UserTable approvalAuthorityInfo);

        public abstract ICollection<EmailNotification> GetEmail(IEnumerable<Advance_VW_GetApprovalLevelMember> notifyingUserList,
            UserTable requesterInfo, UserTable approvalAuthorityInfo);

        public abstract string GetEmailSubject();

        public abstract string GetEmailMessageBody(UserTable requesterInfo, UserTable approvalAuthorityInfo);
        public abstract string GetEmailMessageHtmlBody(UserTable requesterInfo, UserTable approvalAuthorityInfo);

        public ICollection<string> GetAuthorizedUserName()
        {
            return ApprovalTrackers.Where(c => !string.IsNullOrEmpty(c.AuthorizedBy)).Select(c => c.AuthorizedBy).Distinct().ToList();
        }

        public abstract string GetCreatorUserName();
    }
}
