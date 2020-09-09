using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class BaseAdvanceCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? DisplaySerial { get; set; }
        public ICollection<AdvanceCategory> AdvanceCategories { get; set; }
    }
}
