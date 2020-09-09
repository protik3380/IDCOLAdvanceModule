using System;
using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.SearchModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IAdvanceExpenseHeaderManager : IManager<AdvanceExpenseHeader>
    {
        bool IsExpenseAlreadyEntryForRequisition(long requisitionId);
        ICollection<AdvanceExpenseHeader> GetByCriteria(AdvanceExpenseSearchCriteria criteria);
        AdvanceExpenseHeader GetByRequisition(long id);
        bool PayExpense(long expenseId, string paidBy, DateTime paidOn);
        int GetSerialNo(DateTime createdOn);
        bool ExpensePayReceived(long expenseId, string receivedBy, DateTime receivedOn);
    }
}
