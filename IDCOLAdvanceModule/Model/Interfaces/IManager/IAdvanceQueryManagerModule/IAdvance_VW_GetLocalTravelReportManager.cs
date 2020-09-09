using System.Collections.Generic;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.SearchModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetLocalTravelReportManager : IQueryManager<Advance_VW_GetLocalTravelReport>
    {
        ICollection<Advance_VW_GetLocalTravelReport> GetLocalTravelReport(ReportSearchCriteria criteria);
    }
}
