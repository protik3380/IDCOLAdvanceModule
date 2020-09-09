using System.Collections.Generic;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetRequisitionSignatoryManager : IQueryManager<Advance_VW_GetRequisitionSignatory>
    {
        ICollection<Advance_VW_GetRequisitionSignatory> GetByRequisition(long id);
    }
}
