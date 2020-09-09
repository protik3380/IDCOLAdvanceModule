using System.ComponentModel.DataAnnotations.Schema;
using IDCOLAdvanceModule.Model.EntityModels.Base;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ExpenseApprovalTracker : ApprovalTracker
    {
        [NotMapped]
        public ExpenseApprovalTicket ExpenseApprovalTicket
        {
            get
            {
                return ApprovalTicket as ExpenseApprovalTicket;
            }
            set { ApprovalTicket = value; }
        }

        public ExpenseApprovalTracker()
        {
            TicketTypeId = (long)TicketTypeEnum.Expense;
        }
    }
}