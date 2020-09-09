using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetPendingExpenseReportManager : IAdvance_VW_GetPendingExpenseReportManager
    {
        private readonly IAdvance_VW_GetPendingExpenseReportRepository _advanceVwGetPendingExpenseReportRepository;

        public AdvanceVwGetPendingExpenseReportManager()
        {
            _advanceVwGetPendingExpenseReportRepository = new AdvanceVwGetPendingExpenseRepository();
        }

        public AdvanceVwGetPendingExpenseReportManager(IAdvance_VW_GetPendingExpenseReportRepository advanceVwGetPendingExpenseReportRepository)
        {
            _advanceVwGetPendingExpenseReportRepository = advanceVwGetPendingExpenseReportRepository;
        }

        public ICollection<Advance_VW_GetPendingExpenseReport> GetAll(params Expression<Func<Advance_VW_GetPendingExpenseReport, object>>[] includes)
        {
            return _advanceVwGetPendingExpenseReportRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetPendingExpenseReport> Get(Expression<Func<Advance_VW_GetPendingExpenseReport, bool>> predicate, params Expression<Func<Advance_VW_GetPendingExpenseReport, object>>[] includes)
        {
            return _advanceVwGetPendingExpenseReportRepository.Get(predicate, includes);
        }

        public Advance_VW_GetPendingExpenseReport GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetPendingExpenseReport, bool>> predicate, params Expression<Func<Advance_VW_GetPendingExpenseReport, object>>[] includes)
        {
            return _advanceVwGetPendingExpenseReportRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetPendingExpenseReport> GetPendingExpenseReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetPendingExpenseReport, bool>> query = c => c.ExpenseStatusId == (long)ApprovalStatusEnum.WaitingForApproval;
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
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ExpenseEntryDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ExpenseEntryDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }
            return Get(query);
        }
    }
}
