using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.ViewModels
{
    public class TimeLagForExpenseReport
    {
        public long TrackerPanelId { get; set; }
        public string TrackerPanelName { get; set; }
        public long TrackerLevelId { get; set; }
        public string TrackerLevelName { get; set; }
        public long TrackerApprovalStatusId { get; set; }
        public string TrackerStatusName { get; set; }
        public string TrackerAuthorizedBy { get; set; }
        public DateTime TrackerAuthorizedOn { get; set; }
        public long TicketId { get; set; }
        public long TrackerId { get; set; }
        public string TicketAuthorizedBy { get; set; }
        public DateTime TicketAuthorizedOn { get; set; }
        public long AdvanceRequisitionHeaderId { get; set; }
        public long AdvanceCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string AdvanceAmount { get; set; }
        public string RequesterUserName { get; set; }
        public string RequesterFullName { get; set; }
        public bool IsReleasedForLevel { get; set; }
        public bool IsPendingForLevel { get; set; }
        public double WaitingTime { get; set; }
    }
}
