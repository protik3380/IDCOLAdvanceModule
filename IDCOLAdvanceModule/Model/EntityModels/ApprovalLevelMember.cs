using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IModel;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ApprovalLevelMember : IAudit
    {
        public long Id { get; set; }
        public long ApprovalLevelId { get; set; }
        public string EmployeeUserName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int PriorityOrder { get; set; }
        public bool IsDeleted { get; set; }

        public ApprovalLevel ApprovalLevel { get; set; }
        

    }
}
