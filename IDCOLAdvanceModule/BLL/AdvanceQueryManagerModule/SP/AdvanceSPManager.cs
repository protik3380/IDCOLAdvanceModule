using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository.SP;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule.SP
{
    public class AdvanceSPManager : IDisposable
    {
        private readonly AdvanceSPRepository _advanceSpRepository;

        public AdvanceSPManager()
        {
            _advanceSpRepository = new AdvanceSPRepository();
        }

        public ICollection<Advance_SP_GetLocalTravelReport> GetLocalTravelReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_SP_GetLocalTravelReport, bool>> query = c => true;
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
            if (criteria.FromDate != null)
            {
                query = query.AndAlso(c => (c.ExpenseId != null && c.ExpenseFromDate.Value.Date >= criteria.FromDate.Value.Date) || (c.ExpenseId == null && c.RequisitionFromDate.Date >= criteria.FromDate.Value.Date));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => (c.ExpenseId != null && c.ExpenseToDate.Value.Date <= criteria.ToDate.Value.Date) || (c.ExpenseId == null && c.RequisitionToDate.Date <= criteria.ToDate.Value.Date));
            }
            return _advanceSpRepository.Advance_SP_GetLocalTravelReport(query);
        }

        public ICollection<Advance_SP_GetOverseasTravelReport> GetOverseasTravelReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_SP_GetOverseasTravelReport, bool>> query = c => true;
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
            if (criteria.FromDate != null)
            {    
                query = query.AndAlso(c => (c.ExpenseId != null && c.ExpenseFromDate.Value.Date >= criteria.FromDate.Value.Date) || (c.ExpenseId == null && c.RequisitionFromDate.Date >= criteria.FromDate.Value.Date));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => (c.ExpenseId != null && c.ExpenseToDate.Value.Date <= criteria.ToDate.Value.Date) || (c.ExpenseId == null && c.RequisitionToDate.Date <= criteria.ToDate.Value.Date));
            }
            return _advanceSpRepository.Advance_SP_GetOverseasTravelReport(query);
        }

        public void Dispose()
        {
            _advanceSpRepository.Dispose();
        }
    }
}
