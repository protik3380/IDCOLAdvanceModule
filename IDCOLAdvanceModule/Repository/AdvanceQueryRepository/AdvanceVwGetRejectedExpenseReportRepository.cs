using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository
{
    public class AdvanceVwGetRejectedExpenseReportRepository : BaseQueryRepository<Advance_VW_GetRejectedExpenseReport>,IAdvance_VW_GetRejectedExpenseReportRepository,IDisposable
    {
        public AdvanceVwGetRejectedExpenseReportRepository() : base(new AdvanceQueryContext())
        {
        }

       
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
