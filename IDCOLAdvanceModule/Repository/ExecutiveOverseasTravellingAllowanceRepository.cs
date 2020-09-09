using System;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository
{
    public class ExecutiveOverseasTravellingAllowanceRepository : BaseRepository<ExecutiveOverseasTravellingAllowance>,IExecutiveOverseasTravellingAllowanceRepository, IDisposable
    {
        public ExecutiveOverseasTravellingAllowanceRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
