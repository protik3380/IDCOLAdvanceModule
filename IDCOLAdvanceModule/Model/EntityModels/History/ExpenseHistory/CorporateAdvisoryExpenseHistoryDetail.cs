using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class CorporateAdvisoryExpenseHistoryDetail: ExpenseHistoryDetail
    {
        [NotMapped]
        public CorporateAdvisoryExpenseHistoryHeader CorporateAdvisoryExpenseHistoryHeader
        {
            get { return ExpenseHistoryHeader as CorporateAdvisoryExpenseHistoryHeader; }
            set { base.ExpenseHistoryHeader = value; }
        }
    }
}
