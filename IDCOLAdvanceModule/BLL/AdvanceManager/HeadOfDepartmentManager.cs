using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class HeadOfDepartmentManager : IHeadOfDepartmentManager
    {
        private IHeadOfDepartmentRepository _headOfDepartmentRepository;

        public HeadOfDepartmentManager()
        {
            _headOfDepartmentRepository = new HeadOfDepartmentRepository();
        }

        public HeadOfDepartmentManager(IHeadOfDepartmentRepository headOfDepartmentRepository)
        {
            _headOfDepartmentRepository = headOfDepartmentRepository;
        }

        public bool Insert(HeadOfDepartment entity)
        {
            HeadOfDepartment headOfDepartment = GetByDepartmentId(entity.DepartmentId);
            if (headOfDepartment != null)
            {
                throw new BllException("Head of department is already set for this department.");
            }
            return _headOfDepartmentRepository.Insert(entity);
        }

        public bool Insert(ICollection<HeadOfDepartment> entityCollection)
        {
            return _headOfDepartmentRepository.Insert(entityCollection);
        }

        public bool Edit(HeadOfDepartment entity)
        {
            return _headOfDepartmentRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            HeadOfDepartment entity = GetById(id);
            return _headOfDepartmentRepository.Delete(entity);
        }

        public HeadOfDepartment GetById(long id)
        {
            return _headOfDepartmentRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<HeadOfDepartment> GetAll()
        {
            return _headOfDepartmentRepository.GetAll();
        }

        public HeadOfDepartment GetByDepartmentId(decimal id)
        {
            return _headOfDepartmentRepository.GetFirstOrDefaultBy(c => c.DepartmentId == id);
        }

        public List<decimal> GetDepartmentIdByUserName(string memberUserName)
        {
            List<decimal> departmentIdList = null;
            var hod = _headOfDepartmentRepository.Get(c => c.EmployeeUserName == memberUserName);
            if (hod != null)
            {
                departmentIdList = new List<decimal>();
                departmentIdList.AddRange(hod.Select(c =>c.DepartmentId));
            }
            return departmentIdList;
        }
    }
}
