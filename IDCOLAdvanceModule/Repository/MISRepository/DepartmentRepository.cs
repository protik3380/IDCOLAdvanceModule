using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.Base;
using System;

namespace IDCOLAdvanceModule.Repository.MISRepository
{
    public class DepartmentRepository : BaseRepository<Admin_Departments>, IDepartmentRepository, IDisposable
    {
        public DepartmentRepository()
            : base(new MISContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
