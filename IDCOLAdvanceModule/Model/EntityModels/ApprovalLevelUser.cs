using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ApprovalLevelUser
    {
        public int Id { get; set; }
        public int ApprovalLevelId { get; set; }
        public int UserId { get; set; }
        
    }
}
