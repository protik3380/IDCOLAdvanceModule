using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvanceTravelRequisitionDetail : AdvanceRequisitionDetail
    {
        public long? TravelCostItemId { get; set; }
        public CostItem TravelCostItem { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? TravelSponsorFinancedDetailAmount { get; set; }

        [NotMapped]
        public AdvanceTravelRequisitionHeader AdvanceTravelRequisitionHeader
        {
            get { return base.AdvanceRequisitionHeader as AdvanceTravelRequisitionHeader; }
            set { base.AdvanceRequisitionHeader = value; }
        }

        [NotMapped]
        public IEnumerable<AdvanceTravelExpenseDetail> AdvanceTravelExpenseDetails
        {
            get { return base.AdvanceExpenseDetails.Cast<AdvanceTravelExpenseDetail>(); }
            set { base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value); }
        }

        public override RequisitionHistoryDetail GenerateRequisitionHistoryDetail(AdvanceRequisitionDetail detail,
            long requisitionHistoryHeaderId, HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryDetail requisitionHistoryDetail = Mapper.Map<TravelRequisitionHistoryDetail>(detail);
            requisitionHistoryDetail.RequisitionHistoryHeaderId = requisitionHistoryHeaderId;
            requisitionHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return requisitionHistoryDetail;
        }
    }
}
