using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository
{
    public class OverseasTravelGroupMappingSettingRepository : BaseRepository<OverseasTravelGroupMappingSetting>, IOverseasTravelGroupMappingSettingRepository,IDisposable
    {
        public OverseasTravelGroupMappingSettingRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
           db.Dispose();
        }
    }
}
