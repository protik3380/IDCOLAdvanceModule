using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using System.Data.Entity.ModelConfiguration;

namespace IDCOLAdvanceModule.Context.AdvanceModuleContext.Configuration
{
    class AdvanceRequisitionSearchCriteriaViewConfiguration : EntityTypeConfiguration<AdvanceRequisitionSearchCriteriaVM>
    {
        public AdvanceRequisitionSearchCriteriaViewConfiguration()
        {
            this.HasKey(c => c.Id);
            this.ToTable("Advance_VW_GetAdvanceRequisitionBySearchCriteria");
        }
    }
}
