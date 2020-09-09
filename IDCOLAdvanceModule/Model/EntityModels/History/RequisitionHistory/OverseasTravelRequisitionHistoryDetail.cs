using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class OverseasTravelRequisitionHistoryDetail : RequisitionHistoryDetail
    {
        public long? OverseasTravelCostItemId { get; set; }
        public CostItem OverseasTravelCostItem { get; set; }
        public DateTime? OverseasFromDate { get; set; }
        public DateTime? OverseasToDate { get; set; }
        public decimal? OverseasSponsorFinancedDetailAmount { get; set; }
        public string Currency { get; set; }
        public double ConversionRate { get; set; }


        [NotMapped]
        public OverseasTravelRequisitionHistoryHeader OverseasTravelRequisitionHistoryHeader
        {
            get { return RequisitionHistoryHeader as OverseasTravelRequisitionHistoryHeader; }
            set { RequisitionHistoryHeader = value; }
        }
    }
}
