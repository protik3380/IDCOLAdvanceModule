using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IPlaceofVistManager :IManager<PlaceOfVisit>
    {
        ICollection<PlaceOfVisit> GetByLocationGroupId(long locationGroupId);
        bool IsLocationIdNull(ICollection<PlaceOfVisit> placeOfVisits);
        
    }
}
