using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Accounts.NerdCastle.Common.Enum;
using Accounts.NerdCastle.Utility;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class AccountConfigurationManager : IAccountConfigurationManager
    {
        private IAccountConfigurationRepository _accountConfigurationRepository;
        private IChartOfAccountManager _chartOfAccountManager;

        public AccountConfigurationManager()
        {
            _accountConfigurationRepository = new AccountConfigurationRepository();
            _chartOfAccountManager = new ChartOfAccountManager();
        }

        public AccountConfigurationManager(IAccountConfigurationRepository accountConfigurationRepository)
        {
            _accountConfigurationRepository = accountConfigurationRepository;
        }
        public bool Insert(AccountConfiguration entity)
        {

            if (entity.IsDefaultAccount)
            {
                SetIsDefaultAccountToFalse(entity);
            }
            bool isInserted = _accountConfigurationRepository.Insert(entity);
            return isInserted;
        }

        public bool Insert(ICollection<AccountConfiguration> entityCollection)
        {
            return _accountConfigurationRepository.Insert(entityCollection);
        }

        public bool Edit(AccountConfiguration entity)
        {
            if (entity.IsDefaultAccount)
            {
                SetIsDefaultAccountToFalse(entity);
            }
            bool isEdited = _accountConfigurationRepository.Edit(entity);
            return isEdited;
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _accountConfigurationRepository.Delete(entity);
        }

        public AccountConfiguration GetById(long id)
        {
            return _accountConfigurationRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.AdvanceCategory,
                c => c.AccountType);
        }

        public ICollection<AccountConfiguration> GetAll()
        {
            return _accountConfigurationRepository.GetAll(c => c.AdvanceCategory,
                c => c.AccountType);
        }

        public bool InsertUpdateDelete(ICollection<AccountConfiguration> accountConfigurations)
        {
            var existingAccountConfiguration = GetAll().ToList();

            var updatedDetails = accountConfigurations.ToList();

            var updateableItems = updatedDetails.Where(c => c.Id > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            var deleteableItems = existingAccountConfiguration.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedDetails.Where(c => c.Id == 0).ToList();


            using (var ts = new TransactionScope())
            {
                int deleteCount = 0, updateCount = 0;
                bool isDeleted = false, isAdded = false, isUpdated = false;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var accountConfiguration in deleteableItems)
                    {
                        isDeleted = _accountConfigurationRepository.Delete(accountConfiguration);

                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    isAdded = _accountConfigurationRepository.Insert(addeableItems);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var accountConfiguration in updateableItems)
                    {
                        isUpdated = _accountConfigurationRepository.Edit(accountConfiguration);
                        if (isUpdated)
                        {
                            updateCount++;
                        }
                    }
                    isUpdated = updateCount > 0;
                }

                ts.Complete();

                return isUpdated || isDeleted || isAdded;
            }
        }

        public ICollection<AccountConfiguration> GetBy(long advanceCategoryId, long accountTypeId)
        {
            var accountConfigurations = GetAll().AsQueryable();
            if (advanceCategoryId != DefaultItem.Value)
            {
                accountConfigurations = accountConfigurations.Where(c => c.AdvanceCategoryId == advanceCategoryId);
            }
            if (accountTypeId != DefaultItem.Value)
            {
                accountConfigurations = accountConfigurations.Where(c => c.AccountTypeId == accountTypeId);
            }
            return accountConfigurations.ToList();
        }

        public ICollection<Accounts_ChartOfAccounts> GetChartOfAccountBy(long advanceCategoryId, IList<long> accountTypeIdList)
        {
            if (advanceCategoryId>0 && accountTypeIdList!=null)
            {
                var accountConfigurations =
                    _accountConfigurationRepository.Get(
                        c => c.AdvanceCategoryId == advanceCategoryId && accountTypeIdList.Contains(c.AccountTypeId));


                if (accountConfigurations!=null && accountConfigurations.Any())
                {
                    var accountCodeList = accountConfigurations.Select(c => c.AccountCode);
                    var chartOfAccountList =
                        _chartOfAccountManager.Get(c => accountCodeList.Contains(c.AccountCode));

                    return chartOfAccountList.OrderBy(c=>c.AccountCode).ToList();
                }

            }

            return null;
        }

        public ICollection<Accounts_ChartOfAccounts> GetChartOfAccountBy(long advanceCategoryId, long accountTypeId)
        {
            var accountConfigurations = GetAll().AsQueryable();
            if (advanceCategoryId > 0)
            {
                accountConfigurations = accountConfigurations.Where(c => c.AdvanceCategoryId == advanceCategoryId);
            }
            if (accountTypeId > 0)
            {
                accountConfigurations = accountConfigurations.Where(c => c.AccountTypeId == accountTypeId);
            }
            var accountConfigurationList =  accountConfigurations.ToList();

            var accountCodes = accountConfigurationList.Select(c => c.AccountCode);

            var chartOfAccountList = _chartOfAccountManager.Get(c => accountCodes.Contains(c.AccountCode));

            return chartOfAccountList.OrderBy(c=>c.AccountCode).ToList();
        }

        public ICollection<Accounts_ChartOfAccounts> GetChartOfAccountByVoucherType(long advanceCategoryId, long voucherTypeId)
        {
            List<long> accountTypeIdList = null;

            if (voucherTypeId == (int)VoucherTypeEnum.DebitVoucher)
            {
                accountTypeIdList = new List<long>()
                        {
                            (long) AccountTypeEnum.BankPaymentAccount,
                            (long) AccountTypeEnum.ReimbursementPayableAccount,
                            (long) AccountTypeEnum.VatAccount, 
                            (long) AccountTypeEnum.TaxAccount
                        };


            }

            if (voucherTypeId == (int)VoucherTypeEnum.CreditVoucher)
            {
                accountTypeIdList = new List<long>()
                        {
                            (long) AccountTypeEnum.BankDepositAccount,
                            (long) AccountTypeEnum.RefundReceiveableAccount,
                            (long) AccountTypeEnum.VatAccount, 
                            (long) AccountTypeEnum.TaxAccount
                        };

            }

            if (voucherTypeId == (int)VoucherTypeEnum.JournalVoucher)
            {
                accountTypeIdList = new List<long>()
                        {
                            (long) AccountTypeEnum.ReimbursementPayableAccount,
                            (long) AccountTypeEnum.RefundReceiveableAccount,
                            (long) AccountTypeEnum.VatAccount, 
                            (long) AccountTypeEnum.TaxAccount
                        };


            }
            var accountsCodes = GetChartOfAccountBy(advanceCategoryId, accountTypeIdList);

            return accountsCodes;
        }

        public bool SetIsDefaultAccountToFalse(AccountConfiguration accountConfiguration)
        {
            var accountConfigurations =
                     _accountConfigurationRepository.Get(
                         c => c.AccountTypeId == accountConfiguration.AccountTypeId && c.AdvanceCategoryId == accountConfiguration.AdvanceCategoryId)
                         .ToList();
            accountConfigurations.ForEach(c => c.IsDefaultAccount = false);
            foreach (AccountConfiguration configuration in accountConfigurations)
            {
                _accountConfigurationRepository.Edit(configuration);
            }
            return true;
        }
    }
}
