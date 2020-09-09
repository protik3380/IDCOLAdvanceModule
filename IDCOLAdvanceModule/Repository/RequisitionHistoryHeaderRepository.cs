using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;

namespace IDCOLAdvanceModule.Repository
{
    public class RequisitionHistoryHeaderRepository : BaseRepository<RequisitionHistoryHeader>, IRequisitionHistoryHeaderRepository,IDisposable
    {
        public RequisitionHistoryHeaderRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
