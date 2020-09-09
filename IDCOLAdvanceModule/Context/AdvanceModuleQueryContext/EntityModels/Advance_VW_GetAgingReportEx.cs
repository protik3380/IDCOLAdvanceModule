using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Context.AdvanceModuleQueryContext
{
    public partial class Advance_VW_GetAgingReport
    {
        #region CUSTOM CODE
        public string LT5 { get; set; }
        public string LT10 { get; set; }
        public string D20 { get; set; }
        public string D30 { get; set; }
        public string D60 { get; set; }
        public string M60 { get; set; }
        public void CalculateNoOfDayPassed()
        {
            if (DaysCount <= 15)
            {
                LT5 = "\u221A";
                LT10 = "-";
                D20 = "-";
                D30 = "-";
                D60 = "-";
                M60 = "-";
            }
            else if (DaysCount <= 15 && DaysCount >= 30)
            {
                LT5 = "-";
                LT10 = "\u221A";
                D20 = "-";
                D30 = "-";
                D60 = "-";
                M60 = "-";
            }
            else if (DaysCount <= 30 && DaysCount >= 45)
            {
                LT5 = "-";
                LT10 = "-";
                D20 = "\u221A";
                D30 = "-";
                D60 = "-";
                M60 = "-";
            }
            else if (DaysCount >= 45)
            {
                LT5 = "-";
                LT10 = "-";
                D20 = "-";
                D30 = "-";
                D60 = "-";
                M60 = "\u221A";
            }
        }
        #endregion
    }
}
