using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
   public class PlaceOfVisit
    {
       public long Id { get; set; }
       public string Name { get; set; }
       public long? LocationGroupId { get; set; }
       public LocationGroup LocationGroup { get; set; }
    }
}
