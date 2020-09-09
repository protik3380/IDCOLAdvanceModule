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
    public class AdvanceVwGetRejectedExpenseReportManager : IAdvance_VW_GetRejectedExpenseReportManager
    {
        private IAdvance_VW_GetRejectedExpenseReportRepository _advanceVwGetRejectedExpenseReportRepository;

        public AdvanceVwGetRejectedExpenseReportManager()
        {
            _advanceVwGetRejectedExpenseReportRepository = new AdvanceVwGetRejectedExpenseReportRepository();
        }

        public AdvanceVwGetRejectedExpenseReportManager(IAdvance_VW_GetRejectedExpenseReportRepository advanceVwGetRejectedExpenseReportRepository)
        {
            _advanceVwGetRejectedExpenseReportRepository = advanceVwGetRejectedExpenseReportRepository;
        }
        public ICollection<Advance_VW_GetRejectedExpenseReport> GetAll(params Expression<Func<Advance_VW_GetRejectedExpenseReport, object>>[] includes)
        {
            return _advanceVwGetRejectedExpenseReportRepository.GetAll();
        }

        public ICollection<Advance_VW_GetRejectedExpenseReport> Get(Expression<Func<Advance_VW_GetRejectedExpenseReport, bool>> predicate, params Expression<Func<Advance_VW_GetRejectedExpenseReport, object>>[] includes)
        {
            return _advanceVwGetRejectedExpenseReportRepository.Get(predicate);
        }

        public Advance_VW_GetRejectedExpenseReport GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetRejectedExpenseReport, bool>> predicate, params Expression<Func<Advance_VW_GetRejectedExpenseReport, object>>[] includes)
        {
            return _advanceVwGetRejectedExpenseReportRepository.GetFirstOrDefaultBy(predicate);
        }
        public ICollection<Advance_VW_GetRejectedExpenseReport> GetRejectedExpenseReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetRejectedExpenseReport, bool>> query = c => c.ExpenseStatusId == (long)ApprovalStatusEnum.Rejected;
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
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ExpenseApprovalDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ExpenseApprovalDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }
            return Get(query);
        }
    }
}
