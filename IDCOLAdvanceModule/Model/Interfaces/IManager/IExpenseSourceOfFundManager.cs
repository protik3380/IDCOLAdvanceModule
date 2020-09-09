using IDCOLAdvanceModule.Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IExpenseSourceOfFundManager : IManager<ExpenseSourceOfFund>
    {
        ICollection<ExpenseSourceOfFund> GetAllByExpenseHeaderId(long expenseHeaderId);
        bool Insert(ICollection<ExpenseSourceOfFund> entityCollection, long expenseHeaderId);
    }
}
