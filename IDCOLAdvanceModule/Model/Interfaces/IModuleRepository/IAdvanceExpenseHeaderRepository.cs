using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Interfaces.Repository.BaseRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.Interfaces.IModuleRepository
{
    public interface IAdvanceExpenseHeaderRepository : IRepository<AdvanceExpenseHeader>
    {
        bool IsExpenseAlreadyEntryForRequisition(long requisitionId);
        ICollection<AdvanceExpenseHeader> GetByCriteria(AdvanceExpenseSearchCriteria criteria);
    }
}
