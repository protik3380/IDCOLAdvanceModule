using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class AdvanceVwGetRequisitionSourceOfFundRepository : BaseQueryRepository<Advance_VW_GetRequisitionSourceOfFund>, IAdvance_VW_GetRequisitionSourceOfFundRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }

        public AdvanceVwGetRequisitionSourceOfFundRepository()
            : base(new AdvanceQueryContext())
        {
        }
    }
}
