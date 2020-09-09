using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class Advance_VW_TimeLagRequisitionRepository : BaseQueryRepository<Advance_VW_TimeLagRequisition>, IAdvance_VW_TimeLagRequisitionRepository, IDisposable
    {
        public Advance_VW_TimeLagRequisitionRepository() : base(new AdvanceQueryContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
