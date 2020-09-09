using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class OverseasTravelWiseCostItemSetting
    {
        public long Id { get; set; }
        public long OverseasTravelGroupId { get; set; }
        public long CostItemId { get; set; }
        public string Currency { get; set; }
        public decimal EntitlementAmount { get; set; }

        public virtual OverseasTravelGroup OverseasTravelGroup { get; set; }
        public virtual CostItem CostItem { get; set; }

    }
}
