using System;
using IDCOLAdvanceModule.Model.EntityModels.Base;

namespace IDCOLAdvanceModule.Model.EntityModels.Notification
{
    public abstract class ApplicationNotification
    {
        public long Id { get; set; }
        public long NotificationTypeId { get; set; }
        public virtual string Message { get; set; }
        public string ToUserName { get; set; }
        public virtual DateTime NotificationDate { get; set; }
        public bool IsRead { get; set; }
        public long TicketStatusId { get; set; }
        public long ApprovalTicketId { get; set; }
        public long ApprovalLevelId { get; set; }
        public DateTime? ReadDate { get; set; }

        public ApprovalLevel ApprovalLevel { get; set; }
        public virtual ApprovalTicket ApprovalTicket { get; set; }
        public virtual ApprovalStatus TicketStatus { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}