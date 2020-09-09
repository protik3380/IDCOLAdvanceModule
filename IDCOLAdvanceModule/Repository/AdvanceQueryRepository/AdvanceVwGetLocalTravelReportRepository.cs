using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class AdvanceVwGetLocalTravelReportRepository : BaseQueryRepository<Advance_VW_GetLocalTravelReport>, IAdvance_VW_GetLocalTravelReportRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }

        public AdvanceVwGetLocalTravelReportRepository()
            : base(new AdvanceQueryContext())
        {
        }
    }
}
