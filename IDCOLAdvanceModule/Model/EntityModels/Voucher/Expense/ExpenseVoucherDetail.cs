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
    public class ExpenseVoucherDetail
    {
        public long Id { get; set; }
        public long VoucherHeaderId { get; set; }
        public string AccountCode { get; set; }
        public decimal? DrAmount { get; set; }
        public decimal? CrAmount { get; set; }
        public string Description { get; set; }
        public bool IsNet { get; set; }
        public int? VendorId { get; set; }
        public int? VatTaxCategoryId { get; set; }
        public string Percentage { get; set; }

        public ExpenseVoucherHeader VoucherHeader { get; set; }
        public ICollection<AdvanceExpenseDetail> ExpenseDetails { get; set; }

        public static string GetDescriptionForAdvanceAdjustment()
        {
            return "Advance Adjustment";
        }
    }
}
