using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.MISRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ICollection<UserTable> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public UserTable GetById(long id)
        {
            return _employeeRepository.GetFirstOrDefaultBy(c => c.UserId == id);
        }

        public UserTable GetByUserName(string userName)
        {
            return _employeeRepository.GetFirstOrDefaultBy(c => c.UserName.Equals(userName));
        }

        public ICollection<UserTable> Get(Expression<Func<UserTable, bool>> predicate)
        {
            return _employeeRepository.Get(predicate);
        }

        public ICollection<UserTable> GetByDesignationId(long id)
        {
            return _employeeRepository.Get(c => c.RankID == id && !c.ISDeleted, c => c.Admin_Rank, c => c.UserTable2, c => c.Admin_Departments);
        }

        public ICollection<UserTable> GetBy(decimal? departmentId,ICollection<Admin_Rank> rankList,  long? designationId, string search)
        {
            var employeeList = Get(c => !c.ISDeleted && c.RankID != null).AsEnumerable();
            if (rankList!=null)
            {
                var rankIdList = rankList.Select(c => c.RankID).ToList();
                employeeList = employeeList.Where(c => c.RankID != null && rankIdList.Contains((decimal) c.RankID)).AsEnumerable();
            }
            if (departmentId != null)
            {
                employeeList = employeeList.Where(c => c.DepartmentID == departmentId).AsEnumerable();
            }
            if (designationId != null)
            {
                employeeList = employeeList.Where(c => c.RankID == designationId).AsEnumerable();
            }
            if (!string.IsNullOrEmpty(search))
            {
                employeeList =
                    employeeList.Where(
                        c => (c.FirstName != null && c.FirstName.ToLower().Contains(search.ToLower()))
                             || (c.MiddleName != null && c.MiddleName.ToLower().Contains(search.ToLower()))
                             || (c.LastName != null && c.LastName.ToLower().Contains(search.ToLower()))
                             || (c.EmployeeID != null && c.EmployeeID.ToLower().Contains(search.ToLower()))).AsEnumerable();
            }

            return employeeList.ToList();
        }

        public EmployeeVM GetDetailInformation(string username)
        {
            UserTable employee = _employeeRepository.GetFirstOrDefaultBy(c => c.UserName.Equals(username),
                c => c.Admin_Departments, c => c.Admin_Rank);
            if (employee == null)
            {
                throw new Exception("No data found");
            }
            var employeeVm = new EmployeeVM(employee, employee.Admin_Rank, employee.Admin_Departments);
            return employeeVm;
        }
    }
}
