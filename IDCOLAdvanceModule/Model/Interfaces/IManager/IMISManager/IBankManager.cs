using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface IBankManager
    {
        ICollection<Loan_Bank> GetAll();
        ICollection<Loan_Bank> Get(Expression<Func<Loan_Bank, bool>> predicate);
        Loan_Bank GetById(long id);
    }
}
