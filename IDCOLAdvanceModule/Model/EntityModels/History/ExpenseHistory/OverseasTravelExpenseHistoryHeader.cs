using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class OverseasTravelExpenseHistoryHeader : ExpenseHistoryHeader
    {
        public long PlaceOfVisitId { get; set; }
        public string OverseasSourceOfFund { get; set; }
        public bool IsOverseasSponsorFinanced { get; set; }
        public string OverseasSponsorName { get; set; }
        public string CountryName { get; set; }
        
        [NotMapped]
        public IEnumerable<OverseasTravelExpenseHistoryDetail> OverseasTravelExpenseHistoryDetails
        {
            get
            {
                return
                    ExpenseHistoryDetails.Cast<OverseasTravelExpenseHistoryDetail>();
            }
            set
            {
                ExpenseHistoryDetails = new List<ExpenseHistoryDetail>(value);
            }
        }
    }
}
