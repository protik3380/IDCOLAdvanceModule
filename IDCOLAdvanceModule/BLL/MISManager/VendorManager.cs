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
    public class VendorManager:IVendorManager
    {
        private IVendorRepository _vendorRepository;

        public VendorManager()
        {
            _vendorRepository = new VendorRepository();
        }

        public ICollection<Accounts_NC_VendorInfo> GetAll()
        {
            return _vendorRepository.GetAll();
        }

        public ICollection<Accounts_NC_VendorInfo> Get(Expression<Func<Accounts_NC_VendorInfo, bool>> predicate)
        {
            return _vendorRepository.Get(predicate);
        }
    }
}
