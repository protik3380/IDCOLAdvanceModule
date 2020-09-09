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
    public class CurrencyRepository : BaseRepository<Solar_CurrencyInfo>, ICurrencyRepository, IDisposable
    {
        public CurrencyRepository()
            : base(new MISContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
