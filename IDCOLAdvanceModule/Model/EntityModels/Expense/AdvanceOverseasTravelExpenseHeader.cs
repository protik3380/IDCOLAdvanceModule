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
    public class AdvanceOverseasTravelExpenseHeader: AdvanceExpenseHeader
    {
        public long PlaceOfVisitId { get; set; }
        public string OverseasSourceOfFund { get; set; }
        public bool IsOverseasSponsorFinanced { get; set; }
        public string OverseasSponsorName { get; set; }
        public string CountryName { get; set; }

        public PlaceOfVisit PlaceOfVisit { get; set; }
        public ICollection<CurrencyConversionRateDetail> CurrencyConversionRateDetails { get; set; }

        [NotMapped]
        public IEnumerable<AdvanceOverseasTravelExpenseDetail> AdvanceOverseasTravelExpenseDetails
        {
            get
            {
                return
                    base.AdvanceExpenseDetails.Cast<AdvanceOverseasTravelExpenseDetail>();
            }
            set
            {
                base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value);
            }
        }

        [NotMapped]
        public IEnumerable<AdvanceOverseasTravelRequisitionHeader> AdvanceOverseasTravelRequisitionHeaders
        {
            get
            {
                return
                    base.AdvanceRequisitionHeaders.Cast<AdvanceOverseasTravelRequisitionHeader>();
            }
            set
            {
                base.AdvanceRequisitionHeaders = new List<AdvanceRequisitionHeader>(value);
            }
        }

        public override ExpenseHistoryHeader GenerateExpenseHistoryHeaderFromExpense(AdvanceExpenseHeader expenseHeader,
            HistoryModeEnum historyModeEnum)
        {
            ExpenseHistoryHeader expenseHistoryHeader = Mapper.Map<OverseasTravelExpenseHistoryHeader>(expenseHeader as AdvanceOverseasTravelExpenseHeader);
            if (expenseHeader.AdvanceExpenseDetails != null)
            {
                var detail = expenseHeader.AdvanceExpenseDetails.Select(c => (AdvanceOverseasTravelExpenseDetail)c).ToList();
                expenseHistoryHeader.ExpenseHistoryDetails = Mapper.Map<List<AdvanceOverseasTravelExpenseDetail>, List<OverseasTravelExpenseHistoryDetail>>(detail).Select(c => (ExpenseHistoryDetail)c).ToList();
            }
            expenseHistoryHeader.HistoryModeId = (long)historyModeEnum;
            expenseHistoryHeader.AdvanceExpenseHeaderId = expenseHeader.Id;
            if (expenseHistoryHeader.ExpenseHistoryDetails != null)
                expenseHistoryHeader.ExpenseHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return expenseHistoryHeader;
        }
    }
}
