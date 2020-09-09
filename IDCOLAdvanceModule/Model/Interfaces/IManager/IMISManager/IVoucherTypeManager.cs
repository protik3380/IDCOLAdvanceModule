using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager
{
    public interface IVoucherTypeManager:IQueryManager<Accounts_VoucherTypes>
    {
        ICollection<Accounts_VoucherTypes> GetOperationalTypes();
    }
}
