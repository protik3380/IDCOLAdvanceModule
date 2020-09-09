using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class CurrencyConversionRateDetail
    {
        public long Id { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double ConversionRate { get; set; }
        public long AdvanceOverseasTravelExpenseHeaderId { get; set; }

        public AdvanceOverseasTravelExpenseHeader AdvanceOverseasTravelExpenseHeader { get; set; }
    }
}
