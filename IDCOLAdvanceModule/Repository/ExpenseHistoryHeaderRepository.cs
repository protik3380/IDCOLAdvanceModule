using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Repository.Base;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;

namespace IDCOLAdvanceModule.Repository
{
    public class ExpenseHistoryHeaderRepository : BaseRepository<ExpenseHistoryHeader>, IExpenseHistoryHeaderRepository,IDisposable
    {
        public ExpenseHistoryHeaderRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
