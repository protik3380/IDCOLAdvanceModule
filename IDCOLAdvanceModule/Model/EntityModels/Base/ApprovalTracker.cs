using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.Base
{
    public class ApprovalTracker
    {
        public long Id { get; set; }
        public long ApprovalTicketId { get; set; }
        public long ApprovalStatusId { get; set; }
        public long ApprovalPanelId { get; set; }
        public long ApprovalLevelId { get; set; }
        public string Remarks { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime AuthorizedOn { get; set; }
        public long TicketTypeId { get; set; }
        public string TicketNarration { get; set; }

        public virtual ApprovalTicket ApprovalTicket { get; set; }
        public virtual ApprovalPanel ApprovalPanel { get; set; }
        public virtual ApprovalLevel ApprovalLevel { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }
    }
}
