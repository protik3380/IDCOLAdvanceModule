using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class CorporateAdvisoryRequisitionHistoryHeader :RequisitionHistoryHeader
    {

        public string CorporateAdvisoryPlaceOfEvent { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal TotalRevenue { get; set; }
        public string AdvanceCorporateRemarks { get; set; }
        [NotMapped]
        public IEnumerable<CorporateAdvisoryRequisitionHistoryDetail> CorporateAdvisoryRequisitionHistoryDetails
        {
            get
            {
                return
                    RequisitionHistoryDetails.Cast<CorporateAdvisoryRequisitionHistoryDetail>();
            }
            set
            {
                RequisitionHistoryDetails = new List<RequisitionHistoryDetail>(value);
            }
        }
    }
}
