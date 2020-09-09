using System.Collections.Generic;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetExpenseSourceOfFundManager : IQueryManager<Advance_VW_GetExpenseSourceOfFund>
    {
        ICollection<Advance_VW_GetExpenseSourceOfFund> GetByExpense(long id);
    }
}
