using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using System.Data.Entity.ModelConfiguration;

namespace IDCOLAdvanceModule.Context.AdvanceModuleContext.Configuration
{
    public class EntitlementMappingSettingViewConfiguration : EntityTypeConfiguration<EntitlementMappingSettingVM>
    {
        public EntitlementMappingSettingViewConfiguration()
        {
            this.HasKey(c => c.Id);
            this.ToTable("Advance_VW_GetEntitlementMappingSetting");
        }
    }
}
