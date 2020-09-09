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
    public class AdvanceTravelExpenseHeader : AdvanceExpenseHeader
    {
        public string PlaceOfVisit { get; set; }
        public string SourceOfFund { get; set; }
        public bool IsSponsorFinanced { get; set; }
        public string SponsorName { get; set; }

        [NotMapped]
        public IEnumerable<AdvanceTravelExpenseDetail> AdvanceTravelExpenseDetails
        {
            get
            {
                return
                    base.AdvanceExpenseDetails.Cast<AdvanceTravelExpenseDetail>();
            }
            set
            {
                base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value);
            }
        }

        [NotMapped]
        public IEnumerable<AdvanceTravelRequisitionHeader> AdvanceTravelRequisitionHeaders
        {
            get { return base.AdvanceRequisitionHeaders.Cast<AdvanceTravelRequisitionHeader>(); }
            set
            {
                base.AdvanceRequisitionHeaders = new List<AdvanceRequisitionHeader>(value);
            }
        }

        public override ExpenseHistoryHeader GenerateExpenseHistoryHeaderFromExpense(AdvanceExpenseHeader expenseHeader,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryHeader expenseHistoryHeader = Mapper.Map<TravelExpenseHistoryHeader>(expenseHeader as AdvanceTravelExpenseHeader);
            if (expenseHeader.AdvanceExpenseDetails != null)
            {
                var detail = expenseHeader.AdvanceExpenseDetails.Select(c => (AdvanceTravelExpenseDetail)c).ToList();
                expenseHistoryHeader.ExpenseHistoryDetails = Mapper.Map<List<AdvanceTravelExpenseDetail>, List<TravelExpenseHistoryDetail>>(detail).Select(c => (ExpenseHistoryDetail)c).ToList();
            }
            expenseHistoryHeader.HistoryModeId = (long)historyModeEnum;
            expenseHistoryHeader.AdvanceExpenseHeaderId = expenseHeader.Id;
            if (expenseHistoryHeader.ExpenseHistoryDetails != null)
                expenseHistoryHeader.ExpenseHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return expenseHistoryHeader;
        }
    }
}
