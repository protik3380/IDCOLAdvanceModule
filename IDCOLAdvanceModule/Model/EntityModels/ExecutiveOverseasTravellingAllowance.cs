using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ExecutiveOverseasTravellingAllowance
    {
        public long Id { get; set; }
        public long LocationGroupId { get; set; }
        public long EmployeeCategoryId { get; set; }
        public long CostItemId { get; set; }
        public string Currency { get; set; }
        public decimal? EntitlementAmount { get; set; }
        public bool IsFullAmountEntitlement { get; set; }
        public LocationGroup LocationGroup { get; set; }
        public EmployeeCategory EmployeeCategory { get; set; }
        public CostItem CostItem { get; set; }

    }
}
