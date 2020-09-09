using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Expense
{
    public class AdvanceMiscelleaneousExpenseDetail : AdvanceExpenseDetail
    {
        [NotMapped]
        public AdvanceMiscelleaneousExpenseHeader AdvanceMiscelleaneousExpenseHeader
        {
            get { return base.AdvanceExpenseHeader as AdvanceMiscelleaneousExpenseHeader; }
            set { base.AdvanceExpenseHeader = value; }
        }
        [NotMapped]
        public AdvanceMiscelleneousRequisitionDetail AdvanceMiscelleneousRequisitionDetail
        {
            get
            {
                return base.AdvanceRequisitionDetail as AdvanceMiscelleneousRequisitionDetail;

            }
            set { base.AdvanceRequisitionDetail = value; }
        }

        public override ExpenseHistoryDetail GenerateRequisitionHistoryDetail(AdvanceExpenseDetail detail, long expenseHistoryHeaderId,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryDetail expenseHistoryDetail = Mapper.Map<MiscellaneousExpenseHistoryDetail>(detail);
            expenseHistoryDetail.ExpenseHistoryHeaderId = expenseHistoryHeaderId;
            expenseHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return expenseHistoryDetail;
        }
    }
}
