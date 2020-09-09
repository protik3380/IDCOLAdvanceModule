using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class AdvanceVwGetAdvanceExpenseRepository : BaseQueryRepository<Advance_VW_GetAdvanceExpense>, IAdvance_VW_GetAdvanceExpenseRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }
        public AdvanceVwGetAdvanceExpenseRepository()
            : base(new AdvanceQueryContext())
        {
        }

        

    }
}
