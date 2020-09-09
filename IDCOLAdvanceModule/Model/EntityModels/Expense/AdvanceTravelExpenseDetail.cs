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
    public class AdvanceTravelExpenseDetail : AdvanceExpenseDetail
    {
        public long? TravelCostItemId { get; set; }
        public CostItem TravelCostItem { get; set; }
        public DateTime? FromDate { get; set; }
        public decimal? TravelSponsorFinancedDetailAmount { get; set; }
        public DateTime? ToDate { get; set; }

        [NotMapped]
        public AdvanceTravelExpenseHeader AdvanceTravelExpenseHeader
        {
            get
            {
                return AdvanceExpenseHeader as AdvanceTravelExpenseHeader;
            }
            set { AdvanceExpenseHeader = value; }
        }

        [NotMapped]
        public AdvanceTravelRequisitionDetail AdvanceTravelRequisitionDetail 
        {
            get
            {
                return base.AdvanceRequisitionDetail as AdvanceTravelRequisitionDetail;
                
            }
            set
            {
                base.AdvanceRequisitionDetail = value;
            }
        }
        public override decimal GetSponsorAmount()
        {
            return TravelSponsorFinancedDetailAmount ?? 0;
        }

        public override ExpenseHistoryDetail GenerateRequisitionHistoryDetail(AdvanceExpenseDetail detail, long expenseHistoryHeaderId,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryDetail expenseHistoryDetail = Mapper.Map<TravelExpenseHistoryDetail>(detail);
            expenseHistoryDetail.ExpenseHistoryHeaderId = expenseHistoryHeaderId;
            expenseHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return expenseHistoryDetail;
        }
    }
}
