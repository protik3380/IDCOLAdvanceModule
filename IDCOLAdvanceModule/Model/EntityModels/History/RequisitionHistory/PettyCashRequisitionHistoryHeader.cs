using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class PettyCashRequisitionHistoryHeader : RequisitionHistoryHeader
    {
        [NotMapped]
        public IEnumerable<PettyCashRequisitionHistoryDetail> PettyCashRequisitionDetailHistories
        {
            get
            {
                return
                    RequisitionHistoryDetails.Cast<PettyCashRequisitionHistoryDetail>();
            }
            set
            {
                base.RequisitionHistoryDetails = new List<RequisitionHistoryDetail>(value);
            }
        }
    }
}
