using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface IChartOfAccountManager
    {
        ICollection<Accounts_ChartOfAccounts> GetAll();
        ICollection<Accounts_ChartOfAccounts> Get(Expression<Func<Accounts_ChartOfAccounts, bool>> predicate);
        Accounts_ChartOfAccounts GetByCode(string code);
    }
}
