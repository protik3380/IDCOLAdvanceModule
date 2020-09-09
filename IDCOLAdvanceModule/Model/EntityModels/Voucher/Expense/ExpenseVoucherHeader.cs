using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense
{
    public class ExpenseVoucherHeader
    {
        public long Id { get; set; }
        public long ExpenseHeaderId { get; set; }
        public DateTime VoucherEntryDate { get; set; }
        public string ChequeNo { get; set; }
        public long? BankId { get; set; }
        public long? BranchId { get; set; }
        public string RecipientName { get; set; }
        public string VoucherDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long VoucherStatusId { get; set; }
        public string Currency { get; set; }
        public decimal ConversionRate { get; set; }
        public long? VoucherTypeId { get; set; }
        public DateTime? SentDate { get; set; }

        public AdvanceExpenseHeader ExpenseHeader { get; set; }
        public VoucherStatus VoucherStatus { get; set; }
        public ICollection<ExpenseVoucherDetail> ExpenseVoucherDetails { get; set; }

        public decimal? GetTotalDrAmount()
        {
            return ExpenseVoucherDetails.Sum(c => c.DrAmount) ?? 0;
        }

        public decimal? GetTotalCrAmount()
        {
            return ExpenseVoucherDetails.Sum(c => c.CrAmount) ?? 0;
        }
    }
}
