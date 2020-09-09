using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetHeadOfDepartmentManager : IAdvance_VW_GetHeadOfDepartmentManager
    {
        private readonly IAdvance_VW_GetHeadOfDepartmentRepository _advanceVwGetHeadOfDepartmentRepository;
        private readonly IDepartmentManager _departmentManager;
        private readonly IEmployeeManager _employeeManager;

        public AdvanceVwGetHeadOfDepartmentManager()
        {
            _advanceVwGetHeadOfDepartmentRepository = new AdvanceVwGetHeadOfDepartmentRepository();
            _departmentManager = new DepartmentManager();
            _employeeManager = new EmployeeManager();
        }

        public AdvanceVwGetHeadOfDepartmentManager(IAdvance_VW_GetHeadOfDepartmentRepository advanceVwGetHeadOfDepartmentRepository, IDepartmentManager departmentManager, IEmployeeManager employeeManager)
        {
            _advanceVwGetHeadOfDepartmentRepository = advanceVwGetHeadOfDepartmentRepository;
            _departmentManager = departmentManager;
            _employeeManager = employeeManager;
        }

        public ICollection<Advance_VW_GetHeadOfDepartment> GetAll(params Expression<Func<Advance_VW_GetHeadOfDepartment, object>>[] includes)
        {
            return _advanceVwGetHeadOfDepartmentRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetHeadOfDepartment> Get(Expression<Func<Advance_VW_GetHeadOfDepartment, bool>> predicate, params Expression<Func<Advance_VW_GetHeadOfDepartment, object>>[] includes)
        {
            return _advanceVwGetHeadOfDepartmentRepository.Get(predicate, includes);
        }

        public Advance_VW_GetHeadOfDepartment GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetHeadOfDepartment, bool>> predicate, params Expression<Func<Advance_VW_GetHeadOfDepartment, object>>[] includes)
        {
            return _advanceVwGetHeadOfDepartmentRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public Advance_VW_GetHeadOfDepartment GetHeadOfDepartmentOfAnEmployee(string employeeUserName)
        {
            if (string.IsNullOrEmpty(employeeUserName))
            {
                throw new BllException("Employee user name not sent.");
            }
            UserTable employee = _employeeManager.GetByUserName(employeeUserName);
            if (employee == null)
            {
                throw new BllException("No employee found with this user name.");
            }
            if (employee.DepartmentID == null)
            {
                throw new BllException("No department is set against this employee.");
            }
            decimal departmentId = employee.DepartmentID.Value;
            return GetFirstOrDefaultBy(c => c.DepartmentID == departmentId);
        }
    }
}
