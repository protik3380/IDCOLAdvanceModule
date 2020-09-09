using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class MiscelleneousRequisitionHistoryHeader: RequisitionHistoryHeader
    {
        public string PlaceOfEvent { get; set; }

        [NotMapped]
        public IEnumerable<MiscelleneousRequisitionHistoryDetail> MiscelleneousRequisitionDetailHistories
        {
            get
            {
                return
                    RequisitionHistoryDetails.Cast<MiscelleneousRequisitionHistoryDetail>();
            }
            set
            {
                RequisitionHistoryDetails = new List<RequisitionHistoryDetail>(value);
            }
        }
    }
}
