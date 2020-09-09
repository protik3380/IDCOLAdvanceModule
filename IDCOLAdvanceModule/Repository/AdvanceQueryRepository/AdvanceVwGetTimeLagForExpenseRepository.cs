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
    public class AdvanceVwGetTimeLagForExpenseRepository : BaseQueryRepository<Advance_VW_GetTimeLagForExpense>, IAdvance_VW_GetTimeLagForExpenseRepository
    {
        public AdvanceQueryContext Context
        {
            get
            {
                return db as AdvanceQueryContext;
            }
        }

        public AdvanceVwGetTimeLagForExpenseRepository()
            : base(new AdvanceQueryContext())
        {
        }
    }
}
