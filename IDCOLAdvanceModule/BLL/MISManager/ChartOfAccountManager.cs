using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.MISRepository;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class ChartOfAccountManager : IChartOfAccountManager
    {
        private readonly IChartOfAccountRepository _chartOfAccountRepository;

        public ChartOfAccountManager()
        {
            _chartOfAccountRepository = new ChartOfAccountRepository();
        }

        public ChartOfAccountManager(IChartOfAccountRepository chartOfAccountRepository)
        {
            _chartOfAccountRepository = chartOfAccountRepository;
        }

        public ICollection<Accounts_ChartOfAccounts> GetAll()
        {
            ICollection<Accounts_ChartOfAccounts> chartOfAccountsList = _chartOfAccountRepository.GetAll();
            return chartOfAccountsList.Where(c => !c.IsDeleted).ToList();
        }

        public ICollection<Accounts_ChartOfAccounts> Get(Expression<Func<Accounts_ChartOfAccounts, bool>> predicate)
        {
            ICollection<Accounts_ChartOfAccounts> chartOfAccountsList = _chartOfAccountRepository.Get(predicate);
            return chartOfAccountsList.Where(c => !c.IsDeleted).ToList();
        }

        public Accounts_ChartOfAccounts GetByCode(string code)
        {
            return _chartOfAccountRepository.GetFirstOrDefaultBy(c => c.AccountCode == code && !c.IsDeleted);
        }
    }
}
