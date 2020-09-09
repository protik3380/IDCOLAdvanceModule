using System;
using System.Collections.Generic;
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
    public class AdvanceVwGetTimeLagForRequisitionManager : IAdvance_VW_GetTimeLagForRequisitionManager
    {
        private readonly IAdvance_VW_GetTimeLagForRequisitionRepository _advanceVwGetTimeLagForRequisitionRepository;

        public AdvanceVwGetTimeLagForRequisitionManager()
        {
            _advanceVwGetTimeLagForRequisitionRepository = new AdvanceVwGetTimeLagForRequisitionRepository();
        }

        public AdvanceVwGetTimeLagForRequisitionManager(IAdvance_VW_GetTimeLagForRequisitionRepository advanceVwGetTimeLagForRequisitionRepository)
        {
            _advanceVwGetTimeLagForRequisitionRepository = advanceVwGetTimeLagForRequisitionRepository;
        }

        public ICollection<Advance_VW_GetTimeLagForRequisition> GetAll(params Expression<Func<Advance_VW_GetTimeLagForRequisition, object>>[] includes)
        {
            return _advanceVwGetTimeLagForRequisitionRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetTimeLagForRequisition> Get(Expression<Func<Advance_VW_GetTimeLagForRequisition, bool>> predicate, params Expression<Func<Advance_VW_GetTimeLagForRequisition, object>>[] includes)
        {
            return _advanceVwGetTimeLagForRequisitionRepository.Get(predicate, includes);
        }

        public Advance_VW_GetTimeLagForRequisition GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetTimeLagForRequisition, bool>> predicate, params Expression<Func<Advance_VW_GetTimeLagForRequisition, object>>[] includes)
        {
            return _advanceVwGetTimeLagForRequisitionRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetTimeLagForRequisition> GetByCriteria(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetTimeLagForRequisition, bool>> query = c => true;

            if (!string.IsNullOrEmpty(criteria.EmployeeId))
            {
                query = query.AndAlso(c => c.EmployeeID.Contains(criteria.EmployeeId));
            }
            if (!string.IsNullOrEmpty(criteria.EmployeeName))
            {
                query = query.AndAlso(c => c.EmployeeName.Contains(criteria.EmployeeName));
            }
            if (criteria.CategoryId != null)
            {
                query = query.AndAlso(c => c.AdvanceCategoryId == criteria.CategoryId);
            }
            if (!string.IsNullOrEmpty(criteria.RequisitionNo))
            {
                query = query.AndAlso(c => c.RequisitionNo.Contains(criteria.RequisitionNo));
            }
            if (criteria.ApprovalPanelId != null)
            {
                query = query.AndAlso(c => c.PanelId == criteria.ApprovalPanelId.Value);
            }
            if (criteria.ApprovalLevelId != null)
            {
                query = query.AndAlso(c => c.LevelId.Value == criteria.ApprovalLevelId.Value);
            }

            return Get(query);
        }
    }
}
