using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetExpenseSourceOfFundManager : IAdvance_VW_GetExpenseSourceOfFundManager
    {
        private readonly IAdvance_VW_GetExpenseSourceOfFundRepository _advanceVwGetExpenseSourceOfFundRepository;

        public AdvanceVwGetExpenseSourceOfFundManager()
        {
            _advanceVwGetExpenseSourceOfFundRepository = new AdvanceVwGetExpenseSourceOfFundRepository();
        }

        public AdvanceVwGetExpenseSourceOfFundManager(IAdvance_VW_GetExpenseSourceOfFundRepository advanceVwGetExpenseSourceOfFundRepository)
        {
            _advanceVwGetExpenseSourceOfFundRepository = advanceVwGetExpenseSourceOfFundRepository;
        }

        public ICollection<Advance_VW_GetExpenseSourceOfFund> GetAll(params Expression<Func<Advance_VW_GetExpenseSourceOfFund, object>>[] includes)
        {
            return _advanceVwGetExpenseSourceOfFundRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetExpenseSourceOfFund> Get(Expression<Func<Advance_VW_GetExpenseSourceOfFund, bool>> predicate, params Expression<Func<Advance_VW_GetExpenseSourceOfFund, object>>[] includes)
        {
            return _advanceVwGetExpenseSourceOfFundRepository.Get(predicate, includes);
        }

        public Advance_VW_GetExpenseSourceOfFund GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetExpenseSourceOfFund, bool>> predicate, params Expression<Func<Advance_VW_GetExpenseSourceOfFund, object>>[] includes)
        {
            return _advanceVwGetExpenseSourceOfFundRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetExpenseSourceOfFund> GetByExpense(long id)
        {
            return _advanceVwGetExpenseSourceOfFundRepository.Get(c => c.ExpenseHeaderId.Value == id);
        }
    }
}
