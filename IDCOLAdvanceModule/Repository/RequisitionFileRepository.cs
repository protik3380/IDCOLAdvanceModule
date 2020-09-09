using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;
using IDCOLAdvanceModule.Model;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;

namespace IDCOLAdvanceModule.Repository
{
    public class RequisitionFileRepository : BaseRepository<RequisitionFile>, IRequisitionFileRepository, IDisposable
    {
        private AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public RequisitionFileRepository()
            :base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
