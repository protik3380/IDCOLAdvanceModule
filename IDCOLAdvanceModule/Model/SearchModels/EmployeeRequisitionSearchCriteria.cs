using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.SearchModels
{
    public class EmployeeRequisitionSearchCriteria
    {
        public int AdvanceCategoryId { get; set; }
        public bool IsRequisitionForLoggedInUser { get; set; }
        public string EmployeeUserName { get; set; }
        public string CurrencyName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
