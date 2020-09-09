using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Expense
{
    public class AdvanceOverseasTravelExpenseDetail : AdvanceExpenseDetail
    {
        public long? OverseasTravelCostItemId { get; set; }
        public CostItem OverseasTravelCostItem { get; set; }
        public DateTime? OverseasFromDate { get; set; }
        public DateTime? OverseasToDate { get; set; }
        public decimal? OverseasSponsorFinancedDetailAmount { get; set; }
        public string Currency { get; set; }
        public double ConversionRate { get; set; }

        [NotMapped]
        public AdvanceOverseasTravelExpenseHeader AdvanceOverseasTravelExpenseHeader
        {
            get { return base.AdvanceExpenseHeader as AdvanceOverseasTravelExpenseHeader; }
            set { base.AdvanceExpenseHeader = value; }
        }

        [NotMapped]
        public AdvanceOverseasTravelRequisitionDetail AdvanceOverseasTravelRequisitionDetail
        {
            get { return base.AdvanceRequisitionDetail as AdvanceOverseasTravelRequisitionDetail; }
            set { base.AdvanceRequisitionDetail = value; }
        }

        public override decimal GetAdvanceAmountInBdt()
        {
            return (decimal)ConversionRate * AdvanceAmount;
        }

        public override decimal GetExpenseAmountInBdt()
        {
            return (decimal)ConversionRate * ExpenseAmount;
        }

        public override decimal GetSponsorAmount()
        {
            return OverseasSponsorFinancedDetailAmount ?? 0;
        }

        public override ExpenseHistoryDetail GenerateRequisitionHistoryDetail(AdvanceExpenseDetail detail, long expenseHistoryHeaderId,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryDetail expenseHistoryDetail = Mapper.Map<OverseasTravelExpenseHistoryDetail>(detail);
            expenseHistoryDetail.ExpenseHistoryHeaderId = expenseHistoryHeaderId;
            expenseHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return expenseHistoryDetail;
        }
    }
}
