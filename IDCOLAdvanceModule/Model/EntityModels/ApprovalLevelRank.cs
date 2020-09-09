using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ApprovalLevelRank
    {
        public int Id { get; set; }
        public int ApprovalLevelId { get; set; }
        public int Admin_RankId { get; set; }

        public Admin_Rank AdminRank { get; set; }
        
    }
}
