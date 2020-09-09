using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Notification
{
    public class RequisitionNotification : ApplicationNotification
    {
        public RequisitionNotification()
        {
            NotificationTypeId = (long)NotificaitonTypeEnum.Requisition;
        }

        public long RequisitionId { get; set; }

        public virtual AdvanceRequisitionHeader Requisition { get; set; }
    }
}
