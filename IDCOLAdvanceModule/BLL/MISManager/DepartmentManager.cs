using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.MISRepository;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class DepartmentManager : IDepartmentManager
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentManager()
        {
            _departmentRepository = new DepartmentRepository();
        }

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public bool Insert(Context.MISContext.Admin_Departments entity)
        {
            return _departmentRepository.Insert(entity);
        }

        public bool Insert(ICollection<Context.MISContext.Admin_Departments> entityCollection)
        {
            return _departmentRepository.Insert(entityCollection);
        }

        public bool Edit(Context.MISContext.Admin_Departments entity)
        {
            return _departmentRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _departmentRepository.Delete(entity);
        }

        public Context.MISContext.Admin_Departments GetById(long id)
        {
            return _departmentRepository.GetFirstOrDefaultBy(c => c.DepartmentID == id);
        }

        public ICollection<Context.MISContext.Admin_Departments> GetAll()
        {
            return _departmentRepository.GetAll();
        }
    }
}
