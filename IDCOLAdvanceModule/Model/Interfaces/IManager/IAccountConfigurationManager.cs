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
    public interface IAccountConfigurationManager : IManager<AccountConfiguration>
    {
        bool InsertUpdateDelete(ICollection<AccountConfiguration> accountConfigurations);

        ICollection<AccountConfiguration> GetBy(long advanceCategoryId,
            long accountTypeId);

        ICollection<Accounts_ChartOfAccounts> GetChartOfAccountBy(long advanceCategoryId, IList<long> accountTypeIdList);
        ICollection<Accounts_ChartOfAccounts> GetChartOfAccountByVoucherType(long advanceCategoryId, long voucherTypeId);
        bool SetIsDefaultAccountToFalse(AccountConfiguration accountConfiguration);

        ICollection<Accounts_ChartOfAccounts> GetChartOfAccountBy(
            long advanceCategoryId, long accountTypeId);
    }
}
