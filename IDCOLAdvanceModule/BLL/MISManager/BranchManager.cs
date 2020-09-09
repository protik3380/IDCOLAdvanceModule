using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.MISRepository;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class BranchManager : IBranchManager
    {
        private readonly IBranchRepository _branchRepository;

        public BranchManager()
        {
            _branchRepository = new BranchRepository();
        }

        public BranchManager(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public ICollection<Loan_Bank_Branch> GetAll()
        {
            ICollection<Loan_Bank_Branch> branches = _branchRepository.GetAll();
            return branches.Where(c => c.IsActive != null && c.IsActive.Value).ToList();
        }

        public ICollection<Loan_Bank_Branch> Get(Expression<Func<Loan_Bank_Branch, bool>> predicate)
        {
            ICollection<Loan_Bank_Branch> branches = _branchRepository.Get(predicate);
            return branches.Where(c => c.IsActive != null && c.IsActive.Value).ToList();
        }

        public Loan_Bank_Branch GetById(long id)
        {
            return
                _branchRepository.GetFirstOrDefaultBy(c => c.Branch_ID == id && c.IsActive != null && c.IsActive.Value);
        }

        public ICollection<Loan_Bank_Branch> GetByBankId(long id)
        {
            ICollection<Loan_Bank_Branch> branches = _branchRepository.Get(c => c.Bank_ID == id);
            return branches.Where(c => c.IsActive != null && c.IsActive.Value).ToList();
        }
    }
}
