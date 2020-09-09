using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.SearchModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetAdvanceRequisitionManager : IQueryManager<Advance_VW_GetAdvanceRequisition>
    {
        ICollection<Advance_VW_GetAdvanceRequisition> GetBySearchCriteria(AdvanceRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetBySearchCriteria(EmployeeRequisitionSearchCriteria criteria);
        ICollection<Advance_VW_GetAdvanceRequisition> GetRequisitionByExpendetureEntry(AdvanceRequisitionSearchCriteria criteria, string userName);
        ICollection<Advance_VW_GetAdvanceRequisition> GetAllUnadjustedRequisitionsForRequisitionApproval(string userName, long requisitionHeaderId);
        ICollection<Advance_VW_GetAdvanceRequisition> GetAllUnadjustedRequisitionsForExpenseApproval(string userName, long expenseHeaderId);
        Advance_VW_GetAdvanceRequisition GetByRequisitionHeaderId(long id);
    }
}
