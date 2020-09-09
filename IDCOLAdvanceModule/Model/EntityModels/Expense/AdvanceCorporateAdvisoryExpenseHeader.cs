using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.Expense
{
    public class AdvanceCorporateAdvisoryExpenseHeader :AdvanceExpenseHeader
    {

        public string CorporateAdvisoryPlaceOfEvent { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal TotalRevenue { get; set; }
        public string AdvanceCorporateRemarks { get; set; }

        [NotMapped]
        public IEnumerable<AdvanceCorporateAdvisoryExpenseDetail> AdvanceCorporateAdvisoryExpenseDetails
        {
            get
            {
                return
                    base.AdvanceExpenseDetails.Cast<AdvanceCorporateAdvisoryExpenseDetail>();
            }
            set
            {
                base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value);
            }
        }

        [NotMapped]
        public IEnumerable<AdvanceCorporateAdvisoryRequisitionHeader> AdvanceCorporateAdvisoryRequisitionHeaders
        {
            get { return base.AdvanceRequisitionHeaders.Cast<AdvanceCorporateAdvisoryRequisitionHeader>(); }
            set
            {
                base.AdvanceRequisitionHeaders = new List<AdvanceRequisitionHeader>(value);
            }
        }

        public override ExpenseHistoryHeader GenerateExpenseHistoryHeaderFromExpense(AdvanceExpenseHeader expenseHeader,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryHeader expenseHistoryHeader = Mapper.Map<CorporateAdvisoryExpenseHistoryHeader>(expenseHeader as AdvanceCorporateAdvisoryExpenseHeader);
            if (expenseHeader.AdvanceExpenseDetails != null)
            {
                var detail = expenseHeader.AdvanceExpenseDetails.Select(c => (AdvanceCorporateAdvisoryExpenseDetail)c).ToList();
                expenseHistoryHeader.ExpenseHistoryDetails = Mapper.Map<List<AdvanceCorporateAdvisoryExpenseDetail>, List<CorporateAdvisoryExpenseHistoryDetail>>(detail).Select(c => (ExpenseHistoryDetail)c).ToList();
            }
            expenseHistoryHeader.HistoryModeId = (long)historyModeEnum;
            expenseHistoryHeader.AdvanceExpenseHeaderId = expenseHeader.Id;
            if (expenseHistoryHeader.ExpenseHistoryDetails != null)
                expenseHistoryHeader.ExpenseHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return expenseHistoryHeader;
        }
    }
}
