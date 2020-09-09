using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class OverseasTravelGroup
    {
        public long Id { get; set; }
        public string TravelGroupName { get; set; }
        public long LocationGroupId { get; set; }
        public virtual LocationGroup LocationGroup { get; set; }
        public virtual ICollection<OverseasTravelGroupMappingSetting> OverseasTravelGroupMappingSettings { get; set; }
        public virtual ICollection<OverseasTravelWiseCostItemSetting> OverseasTravelWiseCostItemSettings { get; set; }
    }
}
