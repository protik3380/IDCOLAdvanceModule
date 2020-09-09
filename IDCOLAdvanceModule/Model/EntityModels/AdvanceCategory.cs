using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class AdvanceCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? DisplaySerial { get; set; }
        public bool IsCeilingApplicable { get; set; }
        public decimal? CeilingAmount { get; set; }
        public long BaseAdvanceCategoryId { get; set; }
        public long? RequisitionApprovalPanelId { get; set; }
        public long? ExpenseApprovalPanelId { get; set; }

        public BaseAdvanceCategory BaseAdvanceCategory { get; set; }
        public ApprovalPanel RequisitionApprovalPanel { get; set; }
        public ApprovalPanel ExpenseApprovalPanel { get; set; }
    }
}
