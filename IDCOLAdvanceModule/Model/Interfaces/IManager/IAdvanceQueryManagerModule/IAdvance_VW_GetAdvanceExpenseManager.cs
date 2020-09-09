using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.SearchModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetAdvanceExpenseManager : IQueryManager<Advance_VW_GetAdvanceExpense>
    {
        ICollection<Advance_VW_GetAdvanceExpense> GetBySearchCriteria(AdvanceExpenseSearchCriteria criteria);
    }
}
