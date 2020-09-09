using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface IVendorManager
    {
        ICollection<Accounts_NC_VendorInfo> GetAll();
        ICollection<Accounts_NC_VendorInfo> Get(Expression<Func<Accounts_NC_VendorInfo, bool>> predicate);
    }
}
