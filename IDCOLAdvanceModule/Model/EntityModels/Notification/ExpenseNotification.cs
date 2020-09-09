using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Notification
{
    public class ExpenseNotification : ApplicationNotification
    {
        public ExpenseNotification()
        {
            NotificationTypeId = (long) NotificaitonTypeEnum.Expense;
        }
        public long ExpenseHeaderId { get; set; }
        public virtual AdvanceExpenseHeader ExpenseHeader { get; set; }
    }
}