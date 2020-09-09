using IDCOLAdvanceModule.Model.EntityModels.Base;

namespace IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket
{
    public class DestinationUserForTicket
    {
        public long Id { get; set; }
        public long ApprovalPanelId { get; set; }
        public long ApprovalLevelId { get; set; }
        public long ApprovalTicketId { get; set; }
        public string DestinationUserName { get; set; }
        public ApprovalTicket  ApprovalTicket { get; set; }
        public ApprovalPanel ApprovalPanel { get; set; }
        public ApprovalLevel ApprovalLevel { get; set; }
    }
}