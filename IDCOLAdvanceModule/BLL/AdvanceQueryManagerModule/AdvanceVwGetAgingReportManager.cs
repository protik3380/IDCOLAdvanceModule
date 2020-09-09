using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetAgingReportManager : IAdvanceVwGetAgingReportManager
    {
        private readonly IAdvanceVwGetAgingReportRepository _agingReportRepository;
        public AdvanceVwGetAgingReportManager()
        {
            _agingReportRepository = new AdvanceVwGetAgingReportRepository();
        }

        public AdvanceVwGetAgingReportManager(IAdvanceVwGetAgingReportRepository agingReportRepository)
        {
            _agingReportRepository = agingReportRepository;
        }

        public ICollection<Advance_VW_GetAgingReport> GetAll(params Expression<Func<Advance_VW_GetAgingReport, object>>[] includes)
        {
            return _agingReportRepository.GetAll();
        }

        public ICollection<Advance_VW_GetAgingReport> Get(Expression<Func<Advance_VW_GetAgingReport, bool>> predicate, params Expression<Func<Advance_VW_GetAgingReport, object>>[] includes)
        {
            return _agingReportRepository.Get(predicate, includes);
        }

        public Advance_VW_GetAgingReport GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetAgingReport, bool>> predicate, params Expression<Func<Advance_VW_GetAgingReport, object>>[] includes)
        {
            return _agingReportRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetAgingReport> GetAgingReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetAgingReport, bool>> query = c => c.IsAdvancePaid == true;
            if (!string.IsNullOrEmpty(criteria.EmployeeId))
            {
                query = query.AndAlso(c => c.EmployeeID.Contains(criteria.EmployeeId));
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                query = query.AndAlso(c => c.EmployeeName.Contains(criteria.EmployeeName));
            }
            if (criteria.DepartmentId != null)
            {
                query = query.AndAlso(c => c.DepartmentID == criteria.DepartmentId);
            }
            if (criteria.CategoryId != null)
            {
                query = query.AndAlso(c => c.AdvanceCategoryId == criteria.CategoryId);
            }
            if (criteria.BaseCategoryId != null)
            {
                query = query.AndAlso(c => c.BaseAdvanceCategoryId == criteria.BaseCategoryId);
            }
            if (criteria.FromDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.AdvanceIssueDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.AdvanceIssueDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }
            if (criteria.IsAdjusted != null && criteria.IsAdjusted.Value == true)
            {
                query = query.AndAlso(c => c.ExpenseStatusId == (long)AdvanceStatusEnum.Approved);
            }
            else if (criteria.IsAdjusted != null && criteria.IsAdjusted.Value == false)
            {
                query = query.AndAlso(c => c.ExpenseStatusId != (long)AdvanceStatusEnum.Approved);
            }
            var agingReports = Get(query);
            if (agingReports != null && agingReports.Any())
            {
                foreach (var advanceVwGetAgingReportChild in agingReports)
                {
                    advanceVwGetAgingReportChild.CalculateNoOfDayPassed();
                }
            }
            if (agingReports == null)
            {
                return null;
            }
            return agingReports.ToList();
        }

        public ICollection<Advance_VW_GetAgingReport> GetSummaryReport(ReportSearchCriteria criteria)
        {

            Expression<Func<Advance_VW_GetAgingReport, bool>> query = c => c.IsAdvancePaid == true;
            if (criteria.FromDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.AdvanceIssueDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.AdvanceIssueDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }
            return Get(query);
        }
    }
}
