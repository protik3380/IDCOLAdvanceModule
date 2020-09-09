using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class CostItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<AdvanceCategoryWiseCostItemSetting> RequisitionCategoryWiseCostItemSetting { get; set; }
        public virtual ICollection<OverseasTravelWiseCostItemSetting> OverseasTravelWiseCostItemSettings { get; set; }
    }
}