using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.MISRepository;
using System.Collections.Generic;
using System.Linq;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class DesignationManager : IDesignationManager
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly List<decimal> _designationIdToBeExcluded = new List<decimal> { 9, 10, 11, 12, 13, 14, 24, 32, 33, 34, 35, 36, 37 };

        public DesignationManager()
        {
            _designationRepository = new DesignationRepository();
        }

        public ICollection<Admin_Rank> GetAll()
        {
            return _designationRepository.Get(c => c.IsActive);
        }

        public ICollection<Admin_Rank> GetByDepartmentId(long departmentId)
        {
            return
                _designationRepository.Get(c => c.IsActive && c.UserTables.Any(d => d.DepartmentID == departmentId))
                    .ToList();
        }

        public Admin_Rank GetById(decimal rankId)
        {
            return _designationRepository.GetFirstOrDefaultBy(c => c.RankID == rankId);
        }

        public ICollection<Admin_Rank> GetFiltered()
        {
            return _designationRepository.Get(c => c.IsActive && !_designationIdToBeExcluded.Contains(c.RankID));
        }
    }
}
