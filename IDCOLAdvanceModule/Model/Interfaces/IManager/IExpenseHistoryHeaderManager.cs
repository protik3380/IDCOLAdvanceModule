using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IExpenseHistoryHeaderManager : IManager<ExpenseHistoryHeader>
    {
        ICollection<ExpenseHistoryHeader> GetAllBy(long expenseHeaderId);
    }
}
