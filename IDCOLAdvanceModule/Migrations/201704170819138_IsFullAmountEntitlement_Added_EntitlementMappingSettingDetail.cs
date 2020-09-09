namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFullAmountEntitlement_Added_EntitlementMappingSettingDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntitlementMappingSettingDetail", "IsFullAmountEntitlement", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EntitlementMappingSettingDetail", "EntitlementAmount", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.EntitlementMappingSettingHeader", "IsFullAmountEntitlement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EntitlementMappingSettingHeader", "IsFullAmountEntitlement", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EntitlementMappingSettingDetail", "EntitlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.EntitlementMappingSettingDetail", "IsFullAmountEntitlement");
        }
    }
}
