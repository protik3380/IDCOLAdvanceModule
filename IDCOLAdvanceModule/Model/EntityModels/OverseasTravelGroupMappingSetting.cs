using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class OverseasTravelGroupMappingSetting
    {
        public long Id { get; set; }
        public long RankId { get; set; }
        [NotMapped]
        public string RankName { get; set; }

        public long OverseasTravelGroupId { get; set; }
        public virtual OverseasTravelGroup OverseasTravelGroup { get; set; }    
    }
}
