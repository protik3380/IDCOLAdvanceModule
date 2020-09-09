using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface IBranchManager
    {
        ICollection<Loan_Bank_Branch> GetAll();
        ICollection<Loan_Bank_Branch> Get(Expression<Func<Loan_Bank_Branch, bool>> predicate);
        Loan_Bank_Branch GetById(long id);
        ICollection<Loan_Bank_Branch> GetByBankId(long id);
    }
}
