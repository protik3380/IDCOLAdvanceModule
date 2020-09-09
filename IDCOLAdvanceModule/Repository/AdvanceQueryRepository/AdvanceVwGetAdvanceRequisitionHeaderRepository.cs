using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IBaseRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class AdvanceVwGetAdvanceRequisitionHeaderRepository : BaseQueryRepository<Advance_VW_GetAdvanceRequisition>, IAdvance_VW_GetAdvanceRequisitionHeaderRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }

        public AdvanceVwGetAdvanceRequisitionHeaderRepository()
            : base(new AdvanceQueryContext())
        {
        }
    }
}
