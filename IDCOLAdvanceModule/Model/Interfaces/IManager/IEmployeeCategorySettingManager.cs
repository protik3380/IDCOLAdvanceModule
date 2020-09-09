using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IEmployeeCategorySettingManager : IManager<EmployeeCategorySetting>
    {
        ICollection<EmployeeCategoryDesignationVM> GetEmployeeCategoryDesignationVM();
        bool DeleteEmployeeCategorySettings(long categoryId);
        bool IsEmployeeExecutive(decimal rankId);
        ICollection<EmployeeCategorySetting> GetByEmployeeCategoryId(long employeeCategoryId);
        EmployeeCategorySetting GetByAdminRankId(decimal adminRankId);
    }
}
