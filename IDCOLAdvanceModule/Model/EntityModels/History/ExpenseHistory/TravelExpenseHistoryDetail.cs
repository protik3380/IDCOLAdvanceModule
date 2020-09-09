using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public class TravelExpenseHistoryDetail: ExpenseHistoryDetail
    {
        public long? TravelCostItemId { get; set; }
        public CostItem TravelCostItem { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
       public decimal? TravelSponsorFinancedDetailAmount { get; set; }


        [NotMapped]
        public TravelExpenseHistoryHeader TravelExpenseHistoryHeader
        {
            get
            {
                return ExpenseHistoryHeader as TravelExpenseHistoryHeader;
            }
            set { ExpenseHistoryHeader = value; }
        }
        public override decimal GetSponsorAmount()
        {
            return TravelSponsorFinancedDetailAmount ?? 0;
        }
    }
}
