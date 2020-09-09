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
    public class AdvanceMiscelleaneousExpenseHeader : AdvanceExpenseHeader
    {
        public string PlaceOfEvent { get; set; }
        [NotMapped]
        public IEnumerable<AdvanceMiscelleaneousExpenseDetail> AdvanceMiscelleaneousExpenseDetails
        {
            get
            {
                return
                    base.AdvanceExpenseDetails.Cast<AdvanceMiscelleaneousExpenseDetail>();
            }
            set
            {
                base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value);
            }
        }

        [NotMapped]
        public IEnumerable<AdvanceMiscelleneousRequisitionHeader> AdvanceMiscelleneousRequisitionHeaders
        {
            get { return base.AdvanceRequisitionHeaders.Cast<AdvanceMiscelleneousRequisitionHeader>(); }
            set
            {
                base.AdvanceRequisitionHeaders = new List<AdvanceRequisitionHeader>(value);
            }
        }

        public override ExpenseHistoryHeader GenerateExpenseHistoryHeaderFromExpense(AdvanceExpenseHeader expenseHeader,
            HistoryModeEnum historyModeEnum)
        {

            ExpenseHistoryHeader expenseHistoryHeader = Mapper.Map<MiscellaneousExpenseHistoryHeader>(expenseHeader as AdvanceMiscelleaneousExpenseHeader);
            if (expenseHeader.AdvanceExpenseDetails != null)
            {
                var detail = expenseHeader.AdvanceExpenseDetails.Select(c => (AdvanceMiscelleaneousExpenseDetail)c).ToList();
                expenseHistoryHeader.ExpenseHistoryDetails = Mapper.Map<List<AdvanceMiscelleaneousExpenseDetail>, List<MiscellaneousExpenseHistoryDetail>>(detail).Select(c => (ExpenseHistoryDetail)c).ToList();
            }
            expenseHistoryHeader.HistoryModeId = (long)historyModeEnum;
            expenseHistoryHeader.AdvanceExpenseHeaderId = expenseHeader.Id;
            if (expenseHistoryHeader.ExpenseHistoryDetails != null)
                expenseHistoryHeader.ExpenseHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return expenseHistoryHeader;
        }
    }
}
