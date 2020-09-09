using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.Repository.BaseRepository;

namespace IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository
{
    public interface IChartOfAccountRepository : IRepository<Accounts_ChartOfAccounts>
    {
    }
}
