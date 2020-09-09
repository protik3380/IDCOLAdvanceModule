namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFullAmountEntitlement_Added_ExecutiveOverseasTravellingAllowance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExecutiveOverseasTravellingAllowance", "IsFullAmountEntitlement", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "EntitlementAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "EntitlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ExecutiveOverseasTravellingAllowance", "IsFullAmountEntitlement");
        }
    }
}
