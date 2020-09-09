using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public abstract class ExpenseHistoryDetail
    {
        public long Id { get; set; }
        public long ExpenseHistoryHeaderId { get; set; }
        public long? AdvanceRequisitionDetailId { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public string Purpose { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string Remarks { get; set; }
        public string ReceipientOrPayeeName { get; set; }
        public bool IsThirdPartyReceipient { get; set; }
        public long? VatTaxTypeId { get; set; }
        public long HistoryModeId { get; set; }

        public HistoryMode HistoryMode { get; set; }

        public virtual ExpenseHistoryHeader ExpenseHistoryHeader { get; set; }
        public virtual VatTaxType VatTaxType { get; set; }

        public bool HasVendor()
        {
            return ReceipientOrPayeeName != null;
        }
        
        public virtual decimal GetAdvanceAmountInBdt()
        {
            if (ExpenseHistoryHeader == null)
            {
                return 1 * AdvanceAmount;
            }
            return (decimal)ExpenseHistoryHeader.ConversionRate * AdvanceAmount;
        }

        public virtual decimal GetExpenseAmountInBdt()
        {
            if (ExpenseHistoryHeader == null)
            {
                return 1 * ExpenseAmount;
            }
            return (decimal)ExpenseHistoryHeader.ConversionRate * ExpenseAmount;
        }

       
        public decimal GetReimbursementOrRefundAmount()
        {
            return GetTotalActualExpenseAmount() - AdvanceAmount;
        }

        public virtual decimal GetSponsorAmount()
        {
            return 0;
        }

        public virtual decimal GetTotalActualExpenseAmount()
        {
            return GetExpenseAmountInBdt() - GetSponsorAmount();
        }
        public string GetFormattedReimbursementAmount()
        {
            var reimbursementAmount = GetReimbursementOrRefundAmount();
            if (reimbursementAmount >= 0)
                return reimbursementAmount.ToString();
            return "(" + Math.Abs(reimbursementAmount) + ")";
        }
    }
}
