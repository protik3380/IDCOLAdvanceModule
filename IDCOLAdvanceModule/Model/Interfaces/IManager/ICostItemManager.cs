using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface ICostItemManager : IManager<CostItem>
    {
        ICollection<CostItem> GetByAdvanceCategory(long categoryId);
        bool IsEntitlementMandatoryFor(long categoryId, long costItemId);
        ICollection<CostItem> GetByBaseAdvanceCategory(long baseCategoryId);
    }
}
