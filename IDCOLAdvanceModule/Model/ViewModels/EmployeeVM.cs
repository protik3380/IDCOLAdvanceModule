using System;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.EntityModels.ViewModels
{
    public class EmployeeVM
    {
        public EmployeeVM(UserTable employee, Admin_Rank adminRank, Admin_Departments adminDepartment)
        {
            this._employee = employee;
            this._adminRank = adminRank;
            this._adminDepartment = adminDepartment;
        }

        public string EmployeeName
        {
            get
            {
                if (_employee == null)
                {
                    return "N/A";
                }
                return _employee.FullName;
            }
        }

        public string Designation
        {
            get
            {
                if (_adminRank == null)
                {
                    return "N/A";
                }
                return _adminRank.RankName;
            }
        }

        public string EmployeeId
        {
            get
            {
                if (_employee == null)
                {
                    return "N/A";
                }
                return _employee.EmployeeID;
            }
        }

        public string Department
        {
            get
            {
                if (_adminDepartment == null)
                {
                    return "N/A";
                }
                return _adminDepartment.DepartmentName;
            }
        }

        public string EmployeeUserName
        {
            get
            {
                if (_employee == null)
                {
                    throw new Exception("");
                }
                return _employee.UserName;
            }
        }

        private UserTable _employee;
        private Admin_Rank _adminRank;
        private Admin_Departments _adminDepartment;

    }
}
