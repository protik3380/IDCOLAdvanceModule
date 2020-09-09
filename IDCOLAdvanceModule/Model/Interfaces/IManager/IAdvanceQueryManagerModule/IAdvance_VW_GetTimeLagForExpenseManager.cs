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
    public interface IAdvance_VW_GetTimeLagForExpenseManager : IQueryManager<Advance_VW_GetTimeLagForExpense>
    {
        ICollection<Advance_VW_GetTimeLagForExpense> GetByCriteria(ReportSearchCriteria criteria);
    }
}
