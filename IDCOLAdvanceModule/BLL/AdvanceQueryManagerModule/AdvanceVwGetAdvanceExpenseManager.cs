using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetAdvanceExpenseManager : IAdvance_VW_GetAdvanceExpenseManager
    {
        private readonly IAdvance_VW_GetAdvanceExpenseRepository _advanceExpenseQueryRepository;
        private readonly IAdvanceExpenseHeaderRepository _advanceExpenseHeaderRepository;

        public AdvanceVwGetAdvanceExpenseManager()
        {
            _advanceExpenseQueryRepository = new AdvanceVwGetAdvanceExpenseRepository();
            _advanceExpenseHeaderRepository = new AdvanceExpenseHeaderRepository();
        }

        public AdvanceVwGetAdvanceExpenseManager(IAdvance_VW_GetAdvanceExpenseRepository advanceExpenseQueryRepository, IAdvanceExpenseHeaderRepository advanceExpenseHeaderRepository)
        {
            _advanceExpenseQueryRepository = advanceExpenseQueryRepository;
            _advanceExpenseHeaderRepository = advanceExpenseHeaderRepository;
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetAll(params Expression<Func<Advance_VW_GetAdvanceExpense, object>>[] includes)
        {
            return _advanceExpenseQueryRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetAdvanceExpense> Get(Expression<Func<Advance_VW_GetAdvanceExpense, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceExpense, object>>[] includes)
        {
            return _advanceExpenseQueryRepository.Get(predicate, includes);
        }

        public Advance_VW_GetAdvanceExpense GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetAdvanceExpense, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceExpense, object>>[] includes)
        {
            return _advanceExpenseQueryRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetAdvanceExpense> GetBySearchCriteria(AdvanceExpenseSearchCriteria criteria)
        {
            Expression<Func<AdvanceExpenseHeader, bool>> query = c => true;

            if (criteria.AdvanceCategoryId > 0)
            {
                query = query.AndAlso(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId);
            }
            if (criteria.DepartmentId > 0)
            {
                query = query.AndAlso(c => c.RequesterDepartmentId == criteria.DepartmentId);
            }
            if (criteria.CurrencyName != DefaultItem.Text)
            {
                query = query.AndAlso(c => c.Currency.Equals(criteria.CurrencyName));
            }
            if (criteria.RankId != DefaultItem.Value && criteria.RankId != null)
            {
                query = query.AndAlso(c => c.RequesterRankId == criteria.RankId);
            }
            if (!string.IsNullOrEmpty(criteria.RequesterUserName))
            {
                query = query.AndAlso(c => c.RequesterUserName.Equals(criteria.RequesterUserName));
            }
            if (criteria.FromDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.FromDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ToDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }

            if (!String.IsNullOrEmpty(criteria.Remarks))
            {
                query = query.AndAlso(
                     c => c.AdvanceExpenseDetails.Any(d => d.Remarks.ToLower().Contains(criteria.Remarks.ToLower())));
            }

            var headerIdList = _advanceExpenseHeaderRepository.Get(query, c => c.AdvanceExpenseDetails).Select(c => c.Id);

            var headerVwList = _advanceExpenseQueryRepository.Get(c => headerIdList.Contains(c.HeaderId));

            return headerVwList.ToList();
        }
    }
}
