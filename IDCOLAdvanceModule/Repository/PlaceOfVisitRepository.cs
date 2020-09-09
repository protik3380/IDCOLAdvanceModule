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
    public class PlaceOfVisitRepository:BaseRepository<PlaceOfVisit> , IPlaceOfVisitRepository,IDisposable
    {
        public PlaceOfVisitRepository() : base(new AdvanceContext())
        {
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
