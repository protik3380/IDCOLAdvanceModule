using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class TravelRequisitionHistoryDetail: RequisitionHistoryDetail
    {
        public long? TravelCostItemId { get; set; }
        public CostItem TravelCostItem { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? TravelSponsorFinancedDetailAmount { get; set; }

        [NotMapped]
        public TravelRequisitionHistoryHeader TravelRequisitionHistoryHeader
        {
            get { return RequisitionHistoryHeader as TravelRequisitionHistoryHeader; }
            set { RequisitionHistoryHeader = value; }
        }
    }
}
