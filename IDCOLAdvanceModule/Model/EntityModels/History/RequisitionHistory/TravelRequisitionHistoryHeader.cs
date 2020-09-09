using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class TravelRequisitionHistoryHeader: RequisitionHistoryHeader
    {
        public string PlaceOfVisit { get; set; }
        public string SourceOfFund { get; set; }
        public bool IsSponsorFinanced { get; set; }
        public string SponsorName { get; set; }
        public decimal? TravelSponsorFinancedHeaderAmount { get; set; }


        [NotMapped]
        public IEnumerable<TravelRequisitionHistoryDetail> TravelRequisitionDetailHistories
        {
            get
            {
                return
                    RequisitionHistoryDetails.Cast<TravelRequisitionHistoryDetail>();
            }
            set
            {
                base.RequisitionHistoryDetails = new List<RequisitionHistoryDetail>(value);
            }
        }

       
    }
}
