using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class PettyCashExpenseHistoryHeader:ExpenseHistoryHeader
    {
        [NotMapped]
        public IEnumerable<PettyCashExpenseHistoryDetail> PettyCashExpenseHistoryDetails
        {
            get
            {
                return
                    ExpenseHistoryDetails.Cast<PettyCashExpenseHistoryDetail>();
            }
            set
            {
                base.ExpenseHistoryDetails = new List<ExpenseHistoryDetail>(value);
            }
        }
    }
}
