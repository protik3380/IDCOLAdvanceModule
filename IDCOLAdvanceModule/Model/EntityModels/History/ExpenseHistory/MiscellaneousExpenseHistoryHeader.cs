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
    public class MiscellaneousExpenseHistoryHeader : ExpenseHistoryHeader
    {
        public string PlaceOfEvent { get; set; }
        [NotMapped]
        public IEnumerable<MiscellaneousExpenseHistoryDetail> MiscellaneousExpenseHistoryDetails
        {
            get
            {
                return
                    ExpenseHistoryDetails.Cast<MiscellaneousExpenseHistoryDetail>();
            }
            set
            {
                ExpenseHistoryDetails = new List<ExpenseHistoryDetail>(value);
            }
        }

    }
}
