using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Repository.Base;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;

namespace IDCOLAdvanceModule.Repository
{
    public class DestinationUserForTicketRepository : BaseRepository<DestinationUserForTicket>, IDestinationUserForTicketRepository,IDisposable
    {
        public DestinationUserForTicketRepository() : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
