
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class EntitlementMappingSettingHeader
    {

        public long Id { get; set; }
        public long AdvanceCategoryId { get; set; }
        public long CostItemId { get; set; }
        public AdvanceCategory AdvanceCategory { get; set; }
        public CostItem CostItem { get; set; }
        public List<EntitlementMappingSettingDetail> EntitlementMappingSettingDetails { get; set; }


    }
}
