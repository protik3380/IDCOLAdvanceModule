using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class TravelExpenseHistoryHeader : ExpenseHistoryHeader
    {
        public string PlaceOfVisit { get; set; }
        public string SourceOfFund { get; set; }
        public bool IsSponsorFinanced { get; set; }
        public string SponsorName { get; set; }
        
        [NotMapped]
        public IEnumerable<TravelExpenseHistoryDetail> TravelExpenseHistoryDetails
        {
            get
            {
                return
                    ExpenseHistoryDetails.Cast<TravelExpenseHistoryDetail>();
            }
            set
            {
                ExpenseHistoryDetails = new List<ExpenseHistoryDetail>(value);
            }
        }
    }
}
