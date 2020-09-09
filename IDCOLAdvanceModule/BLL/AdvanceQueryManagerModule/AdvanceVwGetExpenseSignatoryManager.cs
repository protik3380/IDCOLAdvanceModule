using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetExpenseSignatoryManager : IAdvance_VW_GetExpenseSignatoryManager
    {
        private readonly IAdvance_VW_GetExpenseSignatoryRepository _advanceVwGetExpenseSignatoryRepository;

        public AdvanceVwGetExpenseSignatoryManager()
        {
            _advanceVwGetExpenseSignatoryRepository = new AdvanceVwGetExpenseSignatoryRepository();
        }

        public AdvanceVwGetExpenseSignatoryManager(IAdvance_VW_GetExpenseSignatoryRepository advanceVwGetExpenseSignatoryRepository)
        {
            _advanceVwGetExpenseSignatoryRepository = advanceVwGetExpenseSignatoryRepository;
        }

        public ICollection<Advance_VW_GetExpenseSignatory> GetAll(params Expression<Func<Advance_VW_GetExpenseSignatory, object>>[] includes)
        {
            return _advanceVwGetExpenseSignatoryRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetExpenseSignatory> Get(Expression<Func<Advance_VW_GetExpenseSignatory, bool>> predicate, params Expression<Func<Advance_VW_GetExpenseSignatory, object>>[] includes)
        {
            return _advanceVwGetExpenseSignatoryRepository.Get(predicate, includes);
        }

        public Advance_VW_GetExpenseSignatory GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetExpenseSignatory, bool>> predicate, params Expression<Func<Advance_VW_GetExpenseSignatory, object>>[] includes)
        {
            return _advanceVwGetExpenseSignatoryRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetExpenseSignatory> GetByExpense(long id)
        {
            return Get(c => c.ExpenseId == id);
        }
    }
}
