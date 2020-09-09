using System;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository
{
    public class ExpenseFileRepository : BaseRepository<ExpenseFile>, IExpenseFileRepository, IDisposable
    {
        private AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public ExpenseFileRepository()
            :base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
