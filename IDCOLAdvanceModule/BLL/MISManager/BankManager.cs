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
    public class BankManager : IBankManager
    {
        private readonly IBankRepository _bankRepository;

        public BankManager()
        {
            _bankRepository = new BankRepository();
        }

        public BankManager(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public ICollection<Loan_Bank> GetAll()
        {
            ICollection<Loan_Bank> banks = _bankRepository.GetAll();
            return banks.Where(c => c.IsActive).ToList();
        }

        public ICollection<Loan_Bank> Get(Expression<Func<Loan_Bank, bool>> predicate)
        {
            ICollection<Loan_Bank> banks = _bankRepository.Get(predicate);
            return banks.Where(c => c.IsActive).ToList();
        }

        public Loan_Bank GetById(long id)
        {
            return _bankRepository.GetFirstOrDefaultBy(c => c.Bank_ID == id && c.IsActive);
        }
    }
}
