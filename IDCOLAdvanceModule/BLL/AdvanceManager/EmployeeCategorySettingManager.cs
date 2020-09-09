using System;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class EmployeeCategorySettingManager : IEmployeeCategorySettingManager
    {
        private IEmployeeCategorySettingRepository _employeeCategorySettingRepository;
        private IEmployeeCategoryRepository _employeeCategoryRepository;
        public EmployeeCategorySettingManager()
        {
            _employeeCategorySettingRepository = new EmployeeCategorySettingRepository();
            _employeeCategoryRepository = new EmployeeCategoryRepository();
        }

        public EmployeeCategorySettingManager(IEmployeeCategorySettingRepository employeeCategorySettingRepository, IEmployeeCategoryRepository employeeCategoryRepository, IDesignationManager designationManager)
        {
            _employeeCategorySettingRepository = employeeCategorySettingRepository;
            _employeeCategoryRepository = employeeCategoryRepository;
            
        }

        public bool Insert(EmployeeCategorySetting entity)
        {
            return _employeeCategorySettingRepository.Insert(entity);
        }

        public bool Insert(ICollection<EmployeeCategorySetting> entityCollection)
        {
            if (entityCollection.Any())
            {
                //need to work for update here..
                /*var employeeCategoryId = entityCollection.ToList()[0].EmployeeCategoryId;
                var employeeCategory = _employeeCategoryRepository.GetFirstOrDefaultBy(c => c.Id == employeeCategoryId);

                var existingEmployeeCategorySettings = _employeeCategoryRepository.GetFirstOrDefaultBy(c=>c.Id==employeeCategoryId).;
                var existingOverseasTravelMapping = existingOverseasTravel.OverseasTravelGroupMappingSettings.ToList();
                var updatedOverseasTravelMapping = entity.OverseasTravelGroupMappingSettings.ToList();*/

                IsDesignationAlreadyExistInAnotherEmployeeCategory(entityCollection.ToList());
                return _employeeCategorySettingRepository.Insert(entityCollection);
            }
            return false;

        }

        public bool Edit(EmployeeCategorySetting entity)
        {
            return _employeeCategorySettingRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _employeeCategorySettingRepository.Delete(entity);
        }

        public EmployeeCategorySetting GetById(long id)
        {
            return _employeeCategorySettingRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<EmployeeCategorySetting> GetAll()
        {
            return _employeeCategorySettingRepository.GetAll();
        }

        public ICollection<EmployeeCategoryDesignationVM> GetEmployeeCategoryDesignationVM()
        {
            return _employeeCategorySettingRepository.GetEmployeeCategoryDesignationVm();
        }

        public bool DeleteEmployeeCategorySettings(long categoryId)
        {
            bool isDeletedSuccessfully = false;
            var itemsToDeleted = _employeeCategorySettingRepository.Get(c => c.EmployeeCategoryId == categoryId);
            if (itemsToDeleted != null)
            {
                int count = 0;
                foreach (var employeeCategorySetting in itemsToDeleted)
                {
                    bool isDeleted = Delete((int)employeeCategorySetting.Id);
                    if (isDeleted)
                    {
                        count++;
                    }
                }
                isDeletedSuccessfully = count == itemsToDeleted.Count;
            }
            return isDeletedSuccessfully;
        }

        public bool IsEmployeeExecutive(decimal rankId)
        {
            var employeeCategorySetting = _employeeCategorySettingRepository.Get(c => c.AdminRankId == rankId);

            if (employeeCategorySetting != null && employeeCategorySetting.Any(c=>c.EmployeeCategoryId==(int)EmployeeCategoryEnum.Executive))
            {
                return true;
            }
            return false;
        }

        public ICollection<EmployeeCategorySetting> GetByEmployeeCategoryId(long employeeCategoryId)
        {
            return
                _employeeCategorySettingRepository.Get(c => c.EmployeeCategoryId == employeeCategoryId,
                    c => c.EmployeeCategory).ToList();
        }

        public bool IsDesignationAlreadyExistInAnotherEmployeeCategory(
            List<EmployeeCategorySetting> employeeCategorySettings)
        {
            string errorMsg = "Designation(s) are already Mapped in " + Environment.NewLine;
            bool isValid = true;
            var executiveGroup = _employeeCategoryRepository.GetFirstOrDefaultBy(c => c.Name.Equals("Executive"));
          foreach (var employeeCategorySetting in employeeCategorySettings)
            {
                if (employeeCategorySetting.Id > 0)
                {
                    
                }
                else
                {
                    var employeeCategorySettingFromDb =
                        _employeeCategorySettingRepository.GetFirstOrDefaultBy(
                            c => c.AdminRankId == employeeCategorySetting.AdminRankId && c.EmployeeCategoryId != executiveGroup.Id, c => c.EmployeeCategory);
                    if (employeeCategorySettingFromDb != null)
                    {
                        isValid = false;
                        errorMsg = errorMsg + employeeCategorySetting.AdminRank.RankName+" mapped in "+employeeCategorySettingFromDb.EmployeeCategory.Name + Environment.NewLine;
                    }
                }
            }
            if (!isValid)
            {
                throw new BllException(errorMsg);
            }
            return true;
        }

        public EmployeeCategorySetting GetByAdminRankId(decimal adminRankId)
        {
            return _employeeCategorySettingRepository.GetFirstOrDefaultBy(c => c.AdminRankId == adminRankId && !c.EmployeeCategory.Name.Equals("Executive"),c=>c.EmployeeCategory);
        }
    }
}
