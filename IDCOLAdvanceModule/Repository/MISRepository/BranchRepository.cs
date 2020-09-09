using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository.MISRepository
{
    public class BranchRepository : BaseRepository<Loan_Bank_Branch>, IBranchRepository, IDisposable
    {
        public BranchRepository()
            : base(new MISContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
