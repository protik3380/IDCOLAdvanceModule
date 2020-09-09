using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class LocationGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PlaceOfVisit> PlaceOfVisits { get; set; }
        public virtual ICollection<OverseasTravelGroup> OverseasTravelGroups { get; set; }
    }
}
