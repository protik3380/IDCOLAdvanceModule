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
    public class AdvanceVwGetAdvanceRequisitionDetailRepository : BaseQueryRepository<Advance_VW_GetAdvanceRequisitionDetail>, IAdvance_VW_GetAdvanceRequisitionDetailRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }

        public AdvanceVwGetAdvanceRequisitionDetailRepository()
            : base(new AdvanceQueryContext())
        {
        }
    }
}
