using IDCOLAdvanceModule.Context.MISContext;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface IDesignationManager
    {
        ICollection<Admin_Rank> GetAll();
        ICollection<Admin_Rank> GetByDepartmentId(long departmentId);
        Admin_Rank GetById(decimal rankId);
        ICollection<Admin_Rank> GetFiltered();
    }
}
