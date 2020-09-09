using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IBaseRepository;

namespace IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository
{
    public interface IVoucherTypeRepository:IQueryRepository<Accounts_VoucherTypes>
    {
    }
}
