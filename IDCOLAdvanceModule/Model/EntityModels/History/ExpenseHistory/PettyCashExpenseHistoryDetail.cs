using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class PettyCashExpenseHistoryDetail:ExpenseHistoryDetail
    {
        [NotMapped]
        public PettyCashExpenseHistoryHeader PettyCashExpenseHistoryHeader
        {
            get { return ExpenseHistoryHeader as PettyCashExpenseHistoryHeader; }
            set { base.ExpenseHistoryHeader = value; }
        }
        
    }
}
