using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class AdvanceVwGetExpenseSourceOfFundRepository : BaseQueryRepository<Advance_VW_GetExpenseSourceOfFund>, IAdvance_VW_GetExpenseSourceOfFundRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }

        public AdvanceVwGetExpenseSourceOfFundRepository()
            : base(new AdvanceQueryContext())
        {
        }
    }
}
