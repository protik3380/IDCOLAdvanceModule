using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetRejectedRequisitionReportManager : IAdvance_VW_GetRejectedRequisitionReportManager
    {
        private readonly IAdvance_VW_GetRejectedRequisitionReportRepository
            _advanceVwGetRejectedRequisitionReportRepository;

        public AdvanceVwGetRejectedRequisitionReportManager()
        {
            _advanceVwGetRejectedRequisitionReportRepository = new AdvanceVwGetRejectedRequisitionReportRepository();
        }

        public AdvanceVwGetRejectedRequisitionReportManager(IAdvance_VW_GetRejectedRequisitionReportRepository advanceVwGetRejectedRequisitionReportRepository)
        {
            _advanceVwGetRejectedRequisitionReportRepository = advanceVwGetRejectedRequisitionReportRepository;
        }

        public ICollection<Advance_VW_GetRejectedRequisitionReport> GetAll(params Expression<Func<Advance_VW_GetRejectedRequisitionReport, object>>[] includes)
        {
            return _advanceVwGetRejectedRequisitionReportRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetRejectedRequisitionReport> Get(Expression<Func<Advance_VW_GetRejectedRequisitionReport, bool>> predicate, params Expression<Func<Advance_VW_GetRejectedRequisitionReport, object>>[] includes)
        {
            return _advanceVwGetRejectedRequisitionReportRepository.Get(predicate, includes);
        }

        public List<Advance_VW_GetRejectedRequisitionReport> GetReport()
        {
            return _advanceVwGetRejectedRequisitionReportRepository.GetAll().ToList();
        }

        public Advance_VW_GetRejectedRequisitionReport GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetRejectedRequisitionReport, bool>> predicate, params Expression<Func<Advance_VW_GetRejectedRequisitionReport, object>>[] includes)
        {
            return _advanceVwGetRejectedRequisitionReportRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetRejectedRequisitionReport> GetRejectedRequisitionReport(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetRejectedRequisitionReport, bool>> query = c => c.RequisitionStatusId == (long)ApprovalStatusEnum.Rejected;
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
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.RequisitionApprovalDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate != null)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.RequisitionApprovalDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }
            return Get(query);
        } 
    }
}
