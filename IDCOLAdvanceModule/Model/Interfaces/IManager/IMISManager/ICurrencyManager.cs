using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface ICurrencyManager
    {
        ICollection<Solar_CurrencyInfo> GetAll();
        ICollection<Solar_CurrencyInfo> GetAllForAdvanceRequisitionAndExpense();
        ICollection<Solar_CurrencyInfo> Get(Expression<Func<Solar_CurrencyInfo, bool>> predicate);
        ICollection<Solar_CurrencyInfo> GetAdvanceRequsitionCategoryWiseCurrencyInfo(long id);
    }
}
