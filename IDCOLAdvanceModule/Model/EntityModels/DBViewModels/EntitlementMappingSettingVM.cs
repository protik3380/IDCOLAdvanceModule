
namespace IDCOLAdvanceModule.Model.EntityModels.DBViewModels
{
    public class EntitlementMappingSettingVM
    {
        public System.Guid Id { get; set; }
        public long HeaderId { get; set; }
        public long DetailId { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
        public long CostItemId { get; set; }
        public string CostItemName { get; set; }
        public decimal RankId { get; set; }
        public string RankName { get; set; }
        public decimal? EntitlementAmount { get; set; }
        public bool IsEntitlementMandatory { get; set; }
        public bool IsFullAmountEntitlement { get; set; }


    }
}
