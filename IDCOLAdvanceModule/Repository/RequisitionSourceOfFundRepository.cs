using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Repository.Base;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;

namespace IDCOLAdvanceModule.Repository
{
    public class RequisitionSourceOfFundRepository : BaseRepository<RequisitionSourceOfFund>, IRequisitionSourceOfFundRepository,IDisposable
    {
        public RequisitionSourceOfFundRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
           db.Dispose();
        }
    }
}
