using System.ComponentModel.DataAnnotations.Schema;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class EntitlementMappingSettingDetail
    {
        public long Id { get; set; }
        public long RankID { get; set; }
        [NotMapped]
        public string RankName { get; set; }
        public decimal? EntitlementAmount { get; set; }
        public bool IsFullAmountEntitlement { get; set; }
        public long EntitlementMappingSettingHeaderId { get; set; }
        public EntitlementMappingSettingHeader EntitlementMappingSettingHeader { get; set; }



    }
}