using System;
using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IEmployeeLeaveManager : IManager<EmployeeLeave>
    {
        ICollection<EmployeeLeave> GetAllByEmployeeUserName(string name);
        bool IsEmployeeOnLeave(string username, DateTime? date = null);
    }
}
