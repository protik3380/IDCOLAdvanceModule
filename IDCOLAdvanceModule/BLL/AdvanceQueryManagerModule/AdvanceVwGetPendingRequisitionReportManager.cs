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
    public class AdvanceVwGetPendingRequisitionReportManager : IAdvance_VW_GetPendingRequisitionReportManager
    {
        private readonly IAdvance_VW_GetPendingRequisitionReportRepository _advanceVwGetPendingRequisitionReportRepository;

        public AdvanceVwGetPendingRequisitionReportManager()
        {
            _advanceVwGetPendingRequisitionReportRepository = new AdvanceVwGetPendingRequisitionRepository();
        }

        public AdvanceVwGetPendingRequisitionReportManager(IAdvance_VW_GetPendingRequisitionReportRepository advanceVwGetPendingRequisitionReportRepository)
        {
            _advanceVwGetPendingRequisitionReportRepository = advanceVwGetPendingRequisitionReportRepository;
        }

        public ICollection<Advance_VW_GetPendingRequisitionReport> GetAll(params Expression<Func<Advance_VW_GetPendingRequisitionReport, object>>[] includes)
        {
            return _advanceVwGetPendingRequisitionReportRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetPendingRequisitionReport> Get(Expression<Func<Advance_VW_GetPendingRequisitionReport, bool>> predicate, params Expression<Func<Advance_VW_GetPendingRequisitionReport, object>>[] includes)
        {
            return _advanceVwGetPendingRequisitionReportRepository.Get(predicate, includes);
        }

        public Advance_VW_GetPendingRequisitionReport GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetPendingRequisitionReport, bool>> predicate, params Expression<Func<Advance_VW_GetPendingRequisitionReport, object>>[] includes)
        {
            return _advanceVwGetPendingRequisitionReportRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetPendingRequisitionReport> GetPendingRequisitionReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetPendingRequisitionReport, bool>> query = c => c.RequisitionStatusId == (long)ApprovalStatusEnum.WaitingForApproval;

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
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.RequisitionEntryDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.RequisitionEntryDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }

            return Get(query);
        }
    }
}
