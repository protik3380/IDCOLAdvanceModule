using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class OverseasTravelRequisitionHistoryHeader :RequisitionHistoryHeader
    {
        public long PlaceOfVisitId { get; set; }
        public string OverseasSourceOfFund { get; set; }
        public bool IsOverseasSponsorFinanced { get; set; }
        public string OverseasSponsorName { get; set; }
        public decimal? OverseasSponsorFinancedHeaderAmount { get; set; }
        public string CountryName { get; set; }

        public PlaceOfVisit PlaceOfVisit { get; set; }

        [NotMapped]
        public IEnumerable<OverseasTravelRequisitionHistoryDetail> OverseasTravelRequisitionDetailHistories
        {
            get
            {
                return
                    RequisitionHistoryDetails.Cast<OverseasTravelRequisitionHistoryDetail>();
            }
            set
            {
                RequisitionHistoryDetails = new List<RequisitionHistoryDetail>(value);
            }
        }
        
    }
}
