using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class MiscelleneousRequisitionHistoryDetail : RequisitionHistoryDetail
    {
        public long? MiscelleneousCostItemId { get; set; }
        public CostItem MiscelleneousCostItem { get; set; }

        [NotMapped]
        public MiscelleneousRequisitionHistoryHeader MiscelleneousRequisitionHistoryHeader
        {
            get { return RequisitionHistoryHeader as MiscelleneousRequisitionHistoryHeader; }
            set { RequisitionHistoryHeader = value; }
        }
    }
}
