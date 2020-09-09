using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.Base;
using System;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class AdvanceVwGetAgingReportRepository : BaseQueryRepository<Advance_VW_GetAgingReport>, IAdvanceVwGetAgingReportRepository, IDisposable
    {
        public AdvanceQueryContext Context
        {
            get { return db as AdvanceQueryContext; }
        }

        public AdvanceVwGetAgingReportRepository()
            : base(new AdvanceQueryContext())
        {
        }



        public void Dispose()
        {
            db.Dispose();
        }
    }
}
