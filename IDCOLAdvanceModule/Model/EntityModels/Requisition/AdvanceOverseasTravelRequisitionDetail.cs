using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvanceOverseasTravelRequisitionDetail : AdvanceRequisitionDetail
    {
        public long? OverseasTravelCostItemId { get; set; }
        public CostItem OverseasTravelCostItem { get; set; }
        public DateTime? OverseasFromDate { get; set; }
        public DateTime? OverseasToDate { get; set; }
        public decimal? OverseasSponsorFinancedDetailAmount { get; set; }
        public string Currency { get; set; }
        public double ConversionRate { get; set; }


        [NotMapped]
        public AdvanceOverseasTravelRequisitionHeader AdvanceOverseasTravelRequisitionHeader
        {
            get { return base.AdvanceRequisitionHeader as AdvanceOverseasTravelRequisitionHeader; }
            set { base.AdvanceRequisitionHeader = value; }
        }

        public override RequisitionHistoryDetail GenerateRequisitionHistoryDetail(AdvanceRequisitionDetail detail,
            long requisitionHistoryHeaderId, HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryDetail requisitionHistoryDetail = Mapper.Map<OverseasTravelRequisitionHistoryDetail>(detail);
            requisitionHistoryDetail.RequisitionHistoryHeaderId = requisitionHistoryHeaderId;
            requisitionHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return requisitionHistoryDetail;
        }

        public override decimal GetAdvanceAmountInBdt()
        {
            return (decimal)ConversionRate * AdvanceAmount;
        }
    }
}
