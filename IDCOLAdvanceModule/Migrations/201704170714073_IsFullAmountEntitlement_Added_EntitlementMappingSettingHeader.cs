namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFullAmountEntitlement_Added_EntitlementMappingSettingHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntitlementMappingSettingHeader", "IsFullAmountEntitlement", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntitlementMappingSettingHeader", "IsFullAmountEntitlement");
        }
    }
}
