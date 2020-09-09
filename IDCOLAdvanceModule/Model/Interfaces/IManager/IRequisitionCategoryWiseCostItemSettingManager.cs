using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IRequisitionCategoryWiseCostItemSettingManager : IManager<AdvanceCategoryWiseCostItemSetting>
    {
        bool IsCategoryWiseCostItemSettingAlreadyExist(
            AdvanceCategoryWiseCostItemSetting categoryWiseCostItemSetting);

        ICollection<AdvanceCategoryWiseCostItemSetting> GetByAdvanceCategory(long categoryId);

        ICollection<AdvanceCategoryWiseCostItemSetting> Get(
            Expression<Func<AdvanceCategoryWiseCostItemSetting, bool>> predicate);
    }
}
