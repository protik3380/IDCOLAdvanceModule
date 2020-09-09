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
    public class AdvancePettyCashExpenseDetail:AdvanceExpenseDetail
    {
        [NotMapped]
        public AdvancePettyCashExpenseHeader AdvancePettyCashExpenseHeader
        {
            get { return AdvanceExpenseHeader as AdvancePettyCashExpenseHeader; }
            set { base.AdvanceExpenseHeader = value; }
        }


        [NotMapped]
        public AdvancePettyCashRequisitionDetail AdvanceTravelRequisitionDetail
        {
            get
            {
                return base.AdvanceRequisitionDetail as AdvancePettyCashRequisitionDetail;

            }
            set
            {
                base.AdvanceRequisitionDetail = value;
            }
        }

        public override ExpenseHistoryDetail GenerateRequisitionHistoryDetail(AdvanceExpenseDetail detail, long expenseHistoryHeaderId,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryDetail expenseHistoryDetail = Mapper.Map<PettyCashExpenseHistoryDetail>(detail);
            expenseHistoryDetail.ExpenseHistoryHeaderId = expenseHistoryHeaderId;
            expenseHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return expenseHistoryDetail;
        }
    }
}
