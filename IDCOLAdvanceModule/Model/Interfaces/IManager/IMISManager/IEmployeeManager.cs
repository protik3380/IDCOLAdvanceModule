using IDCOLAdvanceModule.Context.MISContext;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface IEmployeeManager
    {
        ICollection<UserTable> GetAll();
        UserTable GetById(long id);
        UserTable GetByUserName(string userName);
        ICollection<UserTable> GetByDesignationId(long id);
        ICollection<UserTable> Get(Expression<Func<UserTable, bool>> predicate);
        ICollection<UserTable> GetBy(decimal? departmentId, ICollection<Admin_Rank> rankList,  long? designationId, string search);
        EmployeeVM GetDetailInformation(string username);
    }
}
