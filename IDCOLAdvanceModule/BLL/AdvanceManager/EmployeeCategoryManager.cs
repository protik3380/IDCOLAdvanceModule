using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class EmployeeCategoryManager : IEmployeeCategoryManager
    {
        private IEmployeeCategoryRepository _employeeCategoryRepository;
        private IEmployeeCategorySettingRepository _employeeCategorySettingRepository;
        private IDesignationManager _designationManager;

        public EmployeeCategoryManager()
        {
            _employeeCategoryRepository = new EmployeeCategoryRepository();
            _designationManager = new DesignationManager();
            _employeeCategorySettingRepository = new EmployeeCategorySettingRepository();
        }

        public EmployeeCategoryManager(IEmployeeCategoryRepository employeeCategoryRepository, IEmployeeCategorySettingRepository employeeCategorySettingRepository, IDesignationManager designationManager)
        {
            _employeeCategoryRepository = employeeCategoryRepository;
            _employeeCategorySettingRepository = employeeCategorySettingRepository;
            _designationManager = designationManager;
        }

        public bool Insert(EmployeeCategory entity)
        {
            return _employeeCategoryRepository.Insert(entity);
        }

        public bool Insert(ICollection<EmployeeCategory> entityCollection)
        {
            return _employeeCategoryRepository.Insert(entityCollection);
        }

        public bool Edit(EmployeeCategory entity)
        {
            EmployeeCategory existingEmployeeCategory = GetById(entity.Id);
            var existingEmployeeCategorySettings = existingEmployeeCategory.EmployeeCategorySettings;
            var updatedEmployeeCategorySettings = entity.EmployeeCategorySettings;

            if (entity.Id != (long) EmployeeCategoryEnum.Executive)
            {
                IsEmployeeCategorySettingsExecutive(updatedEmployeeCategorySettings);
                IsEmployeeCategorySettingsAvilable(entity.Id, updatedEmployeeCategorySettings);
            }

            foreach (var updatedEmployeeCategorySetting in updatedEmployeeCategorySettings)
            {
                if (
                    existingEmployeeCategorySettings.Select(c => c.AdminRankId)
                        .Contains(updatedEmployeeCategorySetting.AdminRankId))
                {
                    updatedEmployeeCategorySetting.Id =
                        existingEmployeeCategorySettings.First(
                            c => c.AdminRankId == updatedEmployeeCategorySetting.AdminRankId).Id;
                }
                    
            }
            var updateableItems = updatedEmployeeCategorySettings.Where(c => c.Id > 0).ToList();
            var itemIdList = updateableItems.Select(c => c.Id).ToList();
            var deleteableItems = existingEmployeeCategorySettings.Where(c => !itemIdList.Contains(c.Id)).ToList();
            var addeableItems = updatedEmployeeCategorySettings.Where(c => c.Id == 0).ToList();

            using (var ts = new TransactionScope())
            {
                entity.EmployeeCategorySettings = null;
                bool isEmployeeCategoryUpdated = _employeeCategoryRepository.Edit(entity);

                int deleteCount = 0;
                int updateCount = 0;

                bool isDeleted = false;
                bool isAdded = false;
                bool isUpdated = false;

                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var employeeCategorySetting in deleteableItems)
                    {
                        isDeleted = _employeeCategorySettingRepository.Delete(employeeCategorySetting);
                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    isAdded = _employeeCategorySettingRepository.Insert(addeableItems);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var item in updateableItems)
                    {
                        isUpdated = _employeeCategorySettingRepository.Edit(item);
                        if (isUpdated)
                        {
                            updateCount++;
                        }
                    }
                    isUpdated = updateCount == (updateableItems == null ? 0 : updateableItems.Count());
                }

                ts.Complete();

                return isUpdated || isDeleted || isAdded || isEmployeeCategoryUpdated;
            }
            
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _employeeCategoryRepository.Delete(entity);
        }

        public EmployeeCategory GetById(long id)
        {
            return _employeeCategoryRepository.GetFirstOrDefaultBy(c => c.Id == id,c=>c.EmployeeCategorySettings);
        }

        public ICollection<EmployeeCategory> GetAll()
        {
            return _employeeCategoryRepository.GetAll(c=>c.EmployeeCategorySettings);
        }

        public bool IsEmployeeCategorySettingsExecutive(ICollection<EmployeeCategorySetting> employeeCategorySettings)
        {
            var executiveEmployeeCategory = GetById((long) EmployeeCategoryEnum.Executive);
            var executiveEmployeeCategorySettings = executiveEmployeeCategory.EmployeeCategorySettings;
            string errorMessege = "Following Designation are not executive employee category" + Environment.NewLine;
            bool isFound = false;

            foreach (var employeeCategorySetting in employeeCategorySettings)
            {
                if (!executiveEmployeeCategorySettings.Select(c=>c.AdminRankId).Contains(employeeCategorySetting.AdminRankId))
                {
                    isFound = true;
                    var designation = _designationManager.GetById(employeeCategorySetting.AdminRankId);
                    errorMessege = errorMessege + designation.RankName + Environment.NewLine;
                }
            }
            if (isFound)
                throw new BllException(errorMessege);
            return true;
        }

        public bool IsEmployeeCategorySettingsAvilable(long employeeCategoryId, ICollection<EmployeeCategorySetting> employeeCategorySettings)
        {
            var existingEmployeeCategorySetting =
                _employeeCategorySettingRepository.Get(
                    c =>
                        c.EmployeeCategoryId != (long) EmployeeCategoryEnum.Executive &&
                        c.EmployeeCategoryId != employeeCategoryId).ToList();
            bool isFound = false;
            string errorMessege = "Following Designation are already mapped with another employee category" + Environment.NewLine;

            foreach (var employeeCategorySetting in employeeCategorySettings)
            {
                if (existingEmployeeCategorySetting.Select(c => c.AdminRankId).Contains(employeeCategorySetting.AdminRankId))
                {
                    isFound = true;
                    var designation = _designationManager.GetById(employeeCategorySetting.AdminRankId);
                    errorMessege = errorMessege + designation.RankName + Environment.NewLine;
                }
            }
            if (isFound)
                throw new BllException(errorMessege);
            return true;
        }
    }
}
