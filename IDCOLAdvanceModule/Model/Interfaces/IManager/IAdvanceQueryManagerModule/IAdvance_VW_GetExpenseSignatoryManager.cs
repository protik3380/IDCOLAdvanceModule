using System.Collections.Generic;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetExpenseSignatoryManager : IQueryManager<Advance_VW_GetExpenseSignatory>
    {
        ICollection<Advance_VW_GetExpenseSignatory> GetByExpense(long id);
    }
}
