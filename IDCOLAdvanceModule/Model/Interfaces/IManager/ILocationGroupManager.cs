using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface ILocationGroupManager :IManager<LocationGroup>
    {
        bool InsertLocationGroupAndUpdatePlaceOfVisit(LocationGroup entity, ICollection<PlaceOfVisit> placeOfVisitList);
        bool IsLocationAvailable(LocationGroup locationGroup);
    }
}
