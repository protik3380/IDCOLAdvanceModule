using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.MISRepository
{
    public class ChartOfAccountRepository : BaseRepository<Accounts_ChartOfAccounts>, IChartOfAccountRepository, IDisposable
    {
        public ChartOfAccountRepository()
            : base(new MISContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
