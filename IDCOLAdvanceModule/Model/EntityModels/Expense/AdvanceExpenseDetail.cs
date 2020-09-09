using System;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Expense
{
    public abstract class AdvanceExpenseDetail
    {
        public long Id { get; set; }
        public long AdvanceExpenseHeaderId { get; set; }
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
        public long? ExpenseVoucherDetailId { get; set; }
        public string Justification { get; set; }

        public bool HasVendor()
        {
            return ReceipientOrPayeeName != null;
        }

        public decimal GetAdvanceAmountInBDT(decimal conversionRate)
        {
            return conversionRate * AdvanceAmount;
        }

        public virtual decimal GetAdvanceAmountInBdt()
        {
            if (AdvanceExpenseHeader == null)
            {
                return AdvanceAmount;
            }
            return (decimal)AdvanceExpenseHeader.ConversionRate * AdvanceAmount;
        }

        public virtual decimal GetExpenseAmountInBdt()
        {
            if (AdvanceExpenseHeader == null)
            {
                return ExpenseAmount;
            }
            return (decimal)AdvanceExpenseHeader.ConversionRate * ExpenseAmount;
        }

        public virtual AdvanceExpenseHeader AdvanceExpenseHeader { get; set; }
        public virtual AdvanceRequisitionDetail AdvanceRequisitionDetail { get; set; }
        public virtual VatTaxType VatTaxType { get; set; }
        public virtual ExpenseVoucherDetail ExpenseVoucherDetail { get; set; }
        public decimal GetReimbursementOrRefundAmount()
        {
            return GetTotalActualExpenseAmount() - GetAdvanceAmountInBdt();
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
                return reimbursementAmount.ToString("N");
            return "(" + Math.Abs(reimbursementAmount).ToString("N") + ")";
        }
        public abstract ExpenseHistoryDetail GenerateRequisitionHistoryDetail(AdvanceExpenseDetail detail, long expenseHistoryHeaderId, HistoryModeEnum historyModeEnum);
    }
}