using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IAdvanceRequisitionCategoryManager:IManager<AdvanceCategory>
    {
        ICollection<AdvanceCategory> GetBy(long baseCategoryId);

        ICollection<AdvanceCategory> GetBy(long baseCategoryId, decimal rankId);
        ICollection<AdvanceCategory> GetAllWithApprovalPanel();

        ICollection<AdvanceCategory> GetCategoriesForExpenseApprovalPanel();


    }
}
