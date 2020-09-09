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
    public class AdvanceVwGetAdvanceExpenseDetailRepository : BaseQueryRepository<Advance_VW_GetAdvanceExpenseDetail>, IAdvance_VW_GetAdvanceExpenseDetailRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }

        public AdvanceVwGetAdvanceExpenseDetailRepository()
            : base(new AdvanceQueryContext())
        {
        }
    }
}
