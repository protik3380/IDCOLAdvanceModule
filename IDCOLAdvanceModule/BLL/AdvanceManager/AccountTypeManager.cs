using IDCOLAdvanceModule.Model.Interfaces.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class AccountTypeManager : IAccountTypeManager
    {
        private IAccountTypeRepository _accountTypeRepository;
        private IAccountConfigurationRepository _accountConfigurationRepository;

        public AccountTypeManager()
        {
            _accountTypeRepository = new AccountTypeRepository();
            _accountConfigurationRepository = new AccountConfigurationRepository();
        }

        public AccountTypeManager(IAccountTypeRepository accountTypeRepository, IAccountConfigurationRepository accountConfigurationRepository)
        {
            _accountTypeRepository = accountTypeRepository;
            _accountConfigurationRepository = accountConfigurationRepository;
        }
        public bool Insert(AccountType entity)
        {
            return _accountTypeRepository.Insert(entity);
        }

        public bool Insert(ICollection<AccountType> entityCollection)
        {
            return _accountTypeRepository.Insert(entityCollection);
        }

        public bool Edit(AccountType entity)
        {
            var existingAccountType = GetById(entity.Id);

            var existingAccountConfigarations = existingAccountType.AccountConfigurations.ToList();


            var updatedAccountConfigarations = entity.AccountConfigurations.ToList();

            var updateableItems = updatedAccountConfigarations.Where(c => c.Id > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            var deleteableItems = existingAccountConfigarations.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedAccountConfigarations.Where(c => c.Id == 0).ToList();


            using (var ts = new TransactionScope())
            {
                entity.AccountConfigurations = null;
                var isAcountTypeUpdated = _accountTypeRepository.Edit(entity);

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
                    addeableItems.ForEach(c => { c.Id = entity.Id; });
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

                return isUpdated || isDeleted || isAdded || isAcountTypeUpdated;
            }
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _accountTypeRepository.Delete(entity);
        }

        public AccountType GetById(long id)
        {
            return _accountTypeRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.AccountConfigurations);
        }

        public ICollection<AccountType> GetAll()
        {
            return _accountTypeRepository.GetAll(c => c.AccountConfigurations);
        }
    }
}
