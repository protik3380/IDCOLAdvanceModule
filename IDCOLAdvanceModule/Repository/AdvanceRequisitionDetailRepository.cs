using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.Repository;
using IDCOLAdvanceModule.Repository.Base;
using System;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;

namespace IDCOLAdvanceModule.Repository
{
    public class AdvanceRequisitionDetailRepository : BaseRepository<AdvanceRequisitionDetail>, IAdvanceRequisitionDetailRepository, IDisposable
    {
        public AdvanceRequisitionDetailRepository()
            : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
