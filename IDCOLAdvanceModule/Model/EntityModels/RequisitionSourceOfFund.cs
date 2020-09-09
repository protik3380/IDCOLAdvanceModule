using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IModel;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class RequisitionSourceOfFund : IAudit
    {
        public long Id { get; set; }
        public long SourceOfFundId { get; set; }
        public double Percentage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public long AdvanceRequisitionHeaderId { get; set; }
        public AdvanceRequisitionHeader AdvanceRequisitionHeader { get; set; }
        public SourceOfFund SourceOfFund { get; set; }
    }
}
