using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class EmployeeLeaveManager : IEmployeeLeaveManager
    {
        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;

        public EmployeeLeaveManager()
        {
            _employeeLeaveRepository = new EmployeeLeaveRepository();
        }

        public EmployeeLeaveManager(IEmployeeLeaveRepository employeeLeaveRepository)
        {
            _employeeLeaveRepository = employeeLeaveRepository;
        }

        public bool Insert(EmployeeLeave entity)
        {
            entity.IsDeleted = false;
            return _employeeLeaveRepository.Insert(entity);
        }

        public bool Insert(ICollection<EmployeeLeave> entityCollection)
        {
            entityCollection.ToList().ForEach(c => c.IsDeleted = false);
            return _employeeLeaveRepository.Insert(entityCollection);
        }

        public bool Edit(EmployeeLeave entity)
        {
            return _employeeLeaveRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _employeeLeaveRepository.Delete(entity);
        }

        public EmployeeLeave GetById(long id)
        {
            return _employeeLeaveRepository.GetFirstOrDefaultBy(c => c.Id == id && !c.IsDeleted);
        }

        public ICollection<EmployeeLeave> GetAll()
        {
            return _employeeLeaveRepository.GetAll(c => !c.IsDeleted);
        }

        public ICollection<EmployeeLeave> GetAllByEmployeeUserName(string name)
        {
            return _employeeLeaveRepository.Get(c => c.EmployeeUsername == name && c.IsDeleted == false).ToList();
        }

        public bool IsEmployeeOnLeave(string username, DateTime? date = null)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }
            ICollection<EmployeeLeave> data = _employeeLeaveRepository.Get(
                c =>
                    c.EmployeeUsername.ToLower().Equals(username.ToLower()) &&
                    (DbFunctions.TruncateTime(c.FromDate) >= DbFunctions.TruncateTime(date) &&
                    DbFunctions.TruncateTime(c.ToDate) <= DbFunctions.TruncateTime(date)));
            return data != null && data.Count != 0;
        }
    }
}
