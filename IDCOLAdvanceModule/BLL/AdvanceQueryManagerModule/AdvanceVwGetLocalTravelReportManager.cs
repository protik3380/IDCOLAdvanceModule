using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetLocalTravelReportManager : IAdvance_VW_GetLocalTravelReportManager
    {
        private readonly IAdvance_VW_GetLocalTravelReportRepository _advanceVwGetLocalTravelReportRepository;

        public AdvanceVwGetLocalTravelReportManager()
        {
            _advanceVwGetLocalTravelReportRepository = new AdvanceVwGetLocalTravelReportRepository();
        }

        public AdvanceVwGetLocalTravelReportManager(IAdvance_VW_GetLocalTravelReportRepository advanceVwGetLocalTravelReportRepository)
        {
            _advanceVwGetLocalTravelReportRepository = advanceVwGetLocalTravelReportRepository;
        }

        public ICollection<Advance_VW_GetLocalTravelReport> GetAll(params Expression<Func<Advance_VW_GetLocalTravelReport, object>>[] includes)
        {
            return _advanceVwGetLocalTravelReportRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetLocalTravelReport> Get(Expression<Func<Advance_VW_GetLocalTravelReport, bool>> predicate, params Expression<Func<Advance_VW_GetLocalTravelReport, object>>[] includes)
        {
            return _advanceVwGetLocalTravelReportRepository.Get(predicate, includes);
        }

        public Advance_VW_GetLocalTravelReport GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetLocalTravelReport, bool>> predicate, params Expression<Func<Advance_VW_GetLocalTravelReport, object>>[] includes)
        {
            return _advanceVwGetLocalTravelReportRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetLocalTravelReport> GetLocalTravelReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetLocalTravelReport, bool>> query = c => true;
            if (!string.IsNullOrEmpty(criteria.EmployeeId))
            {
                query = query.AndAlso(c => c.EmployeeId.Contains(criteria.EmployeeId));
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                query = query.AndAlso(c => c.EmployeeName.Contains(criteria.EmployeeName));
            }
            if (criteria.DepartmentId != null)
            {
                query = query.AndAlso(c => c.DepartmentId == criteria.DepartmentId);
            }
            return Get(query);
        }
    }
}
