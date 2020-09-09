
using System.ComponentModel.DataAnnotations.Schema;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class AdvanceCategoryWiseCostItemSetting
    {
        public long Id { get; set; }
        public long AdvanceCategoryId { get; set; }
        public long CostItemId { get; set; }
        public bool IsEntitlementMandatory { get; set; }
        public AdvanceCategory AdvanceCategory { get; set; }
        public CostItem CostItem { get; set; }

        [NotMapped]
        public string EntitlementMandatoryStatus
        {
            get
            {
                if (IsEntitlementMandatory == true)
                {
                    return "Yes";
                }
                return "No";
            }
        }

        



    }
}
