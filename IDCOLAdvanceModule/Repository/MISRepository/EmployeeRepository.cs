using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.Base;
using System;

namespace IDCOLAdvanceModule.Repository.MISRepository
{
    public class EmployeeRepository : BaseRepository<UserTable>, IEmployeeRepository, IDisposable
    {
        public EmployeeRepository()
            : base(new MISContext())
        {
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
