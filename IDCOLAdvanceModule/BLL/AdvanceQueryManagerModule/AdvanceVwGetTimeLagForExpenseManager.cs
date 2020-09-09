using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetTimeLagForExpenseManager : IAdvance_VW_GetTimeLagForExpenseManager
    {
        private readonly IAdvance_VW_GetTimeLagForExpenseRepository _advanceVwGetTimeLagForExpenseRepository;

        public AdvanceVwGetTimeLagForExpenseManager()
        {
            _advanceVwGetTimeLagForExpenseRepository = new AdvanceVwGetTimeLagForExpenseRepository();
        }

        public AdvanceVwGetTimeLagForExpenseManager(IAdvance_VW_GetTimeLagForExpenseRepository advanceVwGetTimeLagForExpenseRepository)
        {
            _advanceVwGetTimeLagForExpenseRepository = advanceVwGetTimeLagForExpenseRepository;
        }

        public ICollection<Advance_VW_GetTimeLagForExpense> GetAll(params Expression<Func<Advance_VW_GetTimeLagForExpense, object>>[] includes)
        {
            return _advanceVwGetTimeLagForExpenseRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetTimeLagForExpense> Get(Expression<Func<Advance_VW_GetTimeLagForExpense, bool>> predicate, params Expression<Func<Advance_VW_GetTimeLagForExpense, object>>[] includes)
        {
            return _advanceVwGetTimeLagForExpenseRepository.Get(predicate, includes);
        }

        public Advance_VW_GetTimeLagForExpense GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetTimeLagForExpense, bool>> predicate, params Expression<Func<Advance_VW_GetTimeLagForExpense, object>>[] includes)
        {
            return _advanceVwGetTimeLagForExpenseRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetTimeLagForExpense> GetByCriteria(ReportSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetTimeLagForExpense, bool>> query = c => true;

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
                query = query.AndAlso(c => c.ExpenseNo.Contains(criteria.ExpenseNo));
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
