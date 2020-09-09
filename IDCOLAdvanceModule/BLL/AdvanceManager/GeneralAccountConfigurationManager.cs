using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class GeneralAccountConfigurationManager : IGeneralAccountConfigurationManager
    {
        private IGeneralAccountConfigurationRepository _generalAccountConfigurationRepository;
        private IChartOfAccountManager _chartOfAccountManager;

        public GeneralAccountConfigurationManager()
        {
            _generalAccountConfigurationRepository = new GeneralAccountConfigurationRepository();
            _chartOfAccountManager = new ChartOfAccountManager();
        }

        public GeneralAccountConfigurationManager(IGeneralAccountConfigurationRepository generalAccountConfigurationRepository)
        {
            _generalAccountConfigurationRepository = generalAccountConfigurationRepository;
        }
        public bool Insert(GeneralAccountConfiguration entity)
        {
            if (entity.IsDefaultAccount)
            {
                SetIsDefaultAccountToFalse(entity);
            }
            bool isInserted = _generalAccountConfigurationRepository.Insert(entity);
            return isInserted;
        }

        public bool Insert(ICollection<GeneralAccountConfiguration> entityCollection)
        {
            return _generalAccountConfigurationRepository.Insert(entityCollection);
        }

        public bool Edit(GeneralAccountConfiguration entity)
        {
            if (entity.IsDefaultAccount)
            {
                SetIsDefaultAccountToFalse(entity);
            }
            bool isEdited = _generalAccountConfigurationRepository.Edit(entity);
            return isEdited;
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _generalAccountConfigurationRepository.Delete(entity);
        }

        public GeneralAccountConfiguration GetById(long id)
        {
            return _generalAccountConfigurationRepository.GetFirstOrDefaultBy(c => c.Id == id, c=>c.AccountType);
        }

        public ICollection<GeneralAccountConfiguration> GetAll()
        {
            return _generalAccountConfigurationRepository.GetAll(c => c.AccountType);
        }

        public ICollection<GeneralAccountConfiguration> GetByAccountTypeId(long accountTypeId)
        {
            var configurations = GetAll().AsQueryable();
            
            if (accountTypeId != DefaultItem.Value)
            {
                configurations = configurations.Where(c => c.AccountTypeId == accountTypeId);
            }
            return configurations.ToList();
        }
        public ICollection<Accounts_ChartOfAccounts> GetChartOfAccountBy(long accountTypeId)
        {
            var configurations = GetAll().AsQueryable();

            if (accountTypeId != DefaultItem.Value)
            {
                configurations = configurations.Where(c => c.AccountTypeId == accountTypeId);
            }
            var configurationList =  configurations.ToList();
            List<Accounts_ChartOfAccounts> chartOfAccountList = new List<Accounts_ChartOfAccounts>();
            foreach (GeneralAccountConfiguration configuration in configurationList)
            {
                var account = _chartOfAccountManager.Get(c => c.AccountCode.Equals(configuration.AccountCode)).FirstOrDefault();
                if (account != null)
                    chartOfAccountList.Add(account);
            }
            return chartOfAccountList;
        }
        public bool SetIsDefaultAccountToFalse(GeneralAccountConfiguration accountConfiguration)
        {
            var accountConfigurations =
                     _generalAccountConfigurationRepository.Get(
                         c => c.AccountTypeId == accountConfiguration.AccountTypeId)
                         .ToList();
            accountConfigurations.ForEach(c => c.IsDefaultAccount = false);
            foreach (GeneralAccountConfiguration configuration in accountConfigurations)
            {
                _generalAccountConfigurationRepository.Edit(configuration);
            }
            return true;
        }

      
    }
}
