using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Voucher;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;

namespace IDCOLAdvanceModule.Model.ViewModels
{
    public class RecipientVM
    {
        public string Name { get; set; }
        public bool IsThirdParty { get; set; }
        public long? VoucherStatusId { get; set; }
        public RequisitionVoucherHeader RequisitionVoucherHeader { get; set; }
        public ExpenseVoucherHeader ExpenseVoucherHeader { get; set; }
    }
}
