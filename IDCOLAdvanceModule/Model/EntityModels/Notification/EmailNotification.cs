using System;
using IDCOLAdvanceModule.Model.EntityModels.Base;

namespace IDCOLAdvanceModule.Model.EntityModels.Notification
{
    public class EmailNotification
    {
        public long Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public string MessageHeader { get; set; }
        public string MessageFooter { get; set; }
        public string MessageContentHtml { get; set; }
        public string ToUserName { get; set; }
        public string CcUserName { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? SentDate { get; set; }
        public long ApprovalLevelId { get; set; }
        public long ApprovalTicketId { get; set; }
        public long TicketStatusId { get; set; }

        public ApprovalLevel ApprovalLevel { get; set; }
        public ApprovalTicket ApprovalTicket { get; set; }
        public ApprovalStatus TicketStatus { get; set; }
    }
}
