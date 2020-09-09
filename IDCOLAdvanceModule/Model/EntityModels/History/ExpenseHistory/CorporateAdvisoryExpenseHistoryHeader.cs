using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class CorporateAdvisoryExpenseHistoryHeader: ExpenseHistoryHeader
    {
        public string CorporateAdvisoryPlaceOfEvent { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal TotalRevenue { get; set; }
        public string AdvanceCorporateRemarks { get; set; }

        [NotMapped]
        public IEnumerable<CorporateAdvisoryExpenseHistoryDetail> CorporateAdvisoryExpenseHistoryDetails
        {
            get
            {
                return
                    ExpenseHistoryDetails.Cast<CorporateAdvisoryExpenseHistoryDetail>();
            }
            set
            {
                ExpenseHistoryDetails = new List<ExpenseHistoryDetail>(value);
            }
        }
    }
}
