using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;

namespace IDCOLAdvanceModule.Model.Interfaces
{
    public interface IExpenseApprovalTrackerManager:IManager.BaseManager.IManager<ExpenseApprovalTracker>
    {
        ICollection<ExpenseApprovalTracker> GetByAuthorizedBy(string username);
    }
}
