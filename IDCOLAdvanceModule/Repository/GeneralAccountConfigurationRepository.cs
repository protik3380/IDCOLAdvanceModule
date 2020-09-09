using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository
{
    public class GeneralAccountConfigurationRepository : BaseRepository<GeneralAccountConfiguration>,IGeneralAccountConfigurationRepository,IDisposable
    {
        public GeneralAccountConfigurationRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.SaveChanges();
        }
    }
}
