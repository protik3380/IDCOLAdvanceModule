using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using System.Data.Entity.ModelConfiguration;

namespace IDCOLAdvanceModule.Context.AdvanceModuleContext.Configuration
{
    public class EmployeeCategoryDesignationViewConfiguration : EntityTypeConfiguration<EmployeeCategoryDesignationVM>
    {
        public EmployeeCategoryDesignationViewConfiguration()
        {
            this.HasKey(c => c.Id);
            this.ToTable("Advance_VW_GetDesignationWithEmployeeCategory");

        }
    }
}
