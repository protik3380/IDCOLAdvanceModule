using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.EntityModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IGeneralAccountConfigurationManager : IManager<GeneralAccountConfiguration>
    {
        ICollection<GeneralAccountConfiguration> GetByAccountTypeId(long accountTypeId);
        bool SetIsDefaultAccountToFalse(GeneralAccountConfiguration accountConfiguration);
        ICollection<Accounts_ChartOfAccounts> GetChartOfAccountBy(long accountTypeId);
    }
}
