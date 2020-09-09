namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class Entitlement
    {
        public long Id { get; set; }
        public int UserRankId { get; set; }
        public decimal EntitlementAmount { get; set; }
        public long CostItemId { get; set; }
        public CostItem CostItem { get; set; }

    }
}