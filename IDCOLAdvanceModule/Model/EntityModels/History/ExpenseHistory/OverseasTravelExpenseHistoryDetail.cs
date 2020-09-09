using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class OverseasTravelExpenseHistoryDetail : ExpenseHistoryDetail
    {
        public long? OverseasTravelCostItemId { get; set; }
        public CostItem OverseasTravelCostItem { get; set; }
        public DateTime? OverseasFromDate { get; set; }
        public DateTime? OverseasToDate { get; set; }
        public decimal? OverseasSponsorFinancedDetailAmount { get; set; }
        public string Currency { get; set; }
        public double ConversionRate { get; set; }

        [NotMapped]
        public OverseasTravelExpenseHistoryHeader OverseasTravelExpenseHistoryHeader
        {
            get
            {
                return ExpenseHistoryHeader as OverseasTravelExpenseHistoryHeader;
            }
            set { ExpenseHistoryHeader = value; }
        }
        public override decimal GetAdvanceAmountInBdt()
        {
            return (decimal)ConversionRate * AdvanceAmount;
        }

        public override decimal GetExpenseAmountInBdt()
        {
            return (decimal)ConversionRate * ExpenseAmount;
        }

        public override decimal GetSponsorAmount()
        {
            return OverseasSponsorFinancedDetailAmount ?? 0;
        }
    }
}
