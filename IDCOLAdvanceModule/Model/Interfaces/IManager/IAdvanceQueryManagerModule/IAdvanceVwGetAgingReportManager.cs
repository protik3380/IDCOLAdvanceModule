using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.SearchModels;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvanceVwGetAgingReportManager : IQueryManager<Advance_VW_GetAgingReport>
    {
        ICollection<Advance_VW_GetAgingReport> GetAgingReport(ReportSearchCriteria criteria);
        ICollection<Advance_VW_GetAgingReport> GetSummaryReport(ReportSearchCriteria criteria);
    }
}
