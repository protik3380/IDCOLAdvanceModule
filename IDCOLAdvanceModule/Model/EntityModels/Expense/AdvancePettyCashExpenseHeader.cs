using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Expense
{
    public class AdvancePettyCashExpenseHeader:AdvanceExpenseHeader
    {
        [NotMapped]
        public IEnumerable<AdvancePettyCashExpenseDetail> AdvancePettyCashExpenseDetails
        {
            get
            {
                return
                    base.AdvanceExpenseDetails.Cast<AdvancePettyCashExpenseDetail>();
            }
            set
            {
                base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value);
            }
        }

        [NotMapped]
        public IEnumerable<AdvancePettyCashRequisitionHeader> AdvancePettyCashRequisitionHeaders
        {
            get { return base.AdvanceRequisitionHeaders.Cast<AdvancePettyCashRequisitionHeader>(); }
            set
            {
                base.AdvanceRequisitionHeaders = new List<AdvanceRequisitionHeader>(value);
            }
        }

        public override ExpenseHistoryHeader GenerateExpenseHistoryHeaderFromExpense(AdvanceExpenseHeader expenseHeader,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryHeader expenseHistoryHeader = Mapper.Map<PettyCashExpenseHistoryHeader>(expenseHeader as AdvancePettyCashExpenseHeader);
            if (expenseHeader.AdvanceExpenseDetails != null)
            {
                var detail = expenseHeader.AdvanceExpenseDetails.Select(c => (AdvancePettyCashExpenseDetail)c).ToList();
                expenseHistoryHeader.ExpenseHistoryDetails = Mapper.Map<List<AdvancePettyCashExpenseDetail>, List<PettyCashExpenseHistoryDetail>>(detail).Select(c => (ExpenseHistoryDetail)c).ToList();
            }
            expenseHistoryHeader.HistoryModeId = (long)historyModeEnum;
            expenseHistoryHeader.AdvanceExpenseHeaderId = expenseHeader.Id;
            if (expenseHistoryHeader.ExpenseHistoryDetails != null)
                expenseHistoryHeader.ExpenseHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return expenseHistoryHeader;
        }
    }
}
