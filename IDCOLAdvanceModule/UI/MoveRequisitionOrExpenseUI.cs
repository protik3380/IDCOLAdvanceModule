using System.Windows.Forms;
using IDCOLAdvanceModule.Model.EntityModels;

namespace IDCOLAdvanceModule.UI
{
    public partial class MoveRequisitionOrExpenseUI : Form
    {
        private readonly RequisitionApprovalTicket _requisitionApprovalTicket;
        private readonly ExpenseApprovalTicket _expenseApprovalTicket;

        private MoveRequisitionOrExpenseUI()
        {
            InitializeComponent();
        }

        public MoveRequisitionOrExpenseUI(RequisitionApprovalTicket requisitionApprovalTicket)
        {
            _requisitionApprovalTicket = requisitionApprovalTicket;
        }

        public MoveRequisitionOrExpenseUI(ExpenseApprovalTicket expenseApprovalTicket)
        {
            _expenseApprovalTicket = expenseApprovalTicket;
        }
    }
}
