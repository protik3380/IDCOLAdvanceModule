using System.Collections.Generic;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetRequisitionSourceOfFundManager : IQueryManager<Advance_VW_GetRequisitionSourceOfFund>
    {
        ICollection<Advance_VW_GetRequisitionSourceOfFund> GetByRequisition(long id);
    }
}
