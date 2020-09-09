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
    public class ExpenseSourceOfFundRepository : BaseRepository<ExpenseSourceOfFund>, IExpenseSourceOfFundRepository,IDisposable
    {
        public ExpenseSourceOfFundRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
