using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetAdvanceExpenseDetailManager : IAdvance_VW_GetAdvanceExpenseDetailManager
    {
        private readonly IAdvance_VW_GetAdvanceExpenseDetailRepository _advanceVwGetAdvanceExpenseDetailRepository;

        public AdvanceVwGetAdvanceExpenseDetailManager()
        {
            _advanceVwGetAdvanceExpenseDetailRepository = new AdvanceVwGetAdvanceExpenseDetailRepository();
        }

        public AdvanceVwGetAdvanceExpenseDetailManager(IAdvance_VW_GetAdvanceExpenseDetailRepository advanceVwGetAdvanceExpenseDetailRepository)
        {
            _advanceVwGetAdvanceExpenseDetailRepository = advanceVwGetAdvanceExpenseDetailRepository;
        }

        public ICollection<Advance_VW_GetAdvanceExpenseDetail> GetAll(params Expression<Func<Advance_VW_GetAdvanceExpenseDetail, object>>[] includes)
        {
            return _advanceVwGetAdvanceExpenseDetailRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetAdvanceExpenseDetail> Get(Expression<Func<Advance_VW_GetAdvanceExpenseDetail, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceExpenseDetail, object>>[] includes)
        {
            return _advanceVwGetAdvanceExpenseDetailRepository.Get(predicate, includes);
        }

        public Advance_VW_GetAdvanceExpenseDetail GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetAdvanceExpenseDetail, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceExpenseDetail, object>>[] includes)
        {
            return _advanceVwGetAdvanceExpenseDetailRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetAdvanceExpenseDetail> GetByHeader(long id)
        {
            return Get(c => c.HeaderId == id);
        }
    }
}
