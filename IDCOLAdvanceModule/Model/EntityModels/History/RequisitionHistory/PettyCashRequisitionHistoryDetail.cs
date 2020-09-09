using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class PettyCashRequisitionHistoryDetail : RequisitionHistoryDetail
    {
        [NotMapped]
        public PettyCashRequisitionHistoryHeader PettyCashRequisitionHistoryHeader
        {
            get { return RequisitionHistoryHeader as PettyCashRequisitionHistoryHeader; }
            set { base.RequisitionHistoryHeader = value; }
        }
    }
}
