using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IExpenseVoucherHeaderManager : IManager<ExpenseVoucherHeader>
    {
        ICollection<ExpenseVoucherDetail> GetVoucherDetailsForRelatedRequisition(ICollection<AdvanceExpenseDetail> expenseDetails);
        string GetVoucherStatusByHeaderIdAndRecipientName(long headerId, string recipient);
        ExpenseVoucherHeader GetByHeaderIdAndRecipientName(long headerId, string recipient);
        ICollection<ExpenseVoucherHeader> GetAllDraftVoucher();
        ICollection<ExpenseVoucherHeader> GetAllSentVoucher();
    }
}
