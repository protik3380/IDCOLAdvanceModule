using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class MiscellaneousExpenseHistoryDetail: ExpenseHistoryDetail
    {
        [NotMapped]
        public MiscellaneousExpenseHistoryHeader MiscellaneousExpenseHistoryHeader
        {
            get { return ExpenseHistoryHeader as MiscellaneousExpenseHistoryHeader; }
            set { base.ExpenseHistoryHeader = value; }
        }
    }
}
