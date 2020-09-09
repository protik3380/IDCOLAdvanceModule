using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IEntitlementManager : IManager<Entitlement>
    {
        Entitlement GetByTravelCostItemId(long id);
    }
}
