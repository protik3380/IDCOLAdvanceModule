namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_TotalRevenue_In_AdvanceCorporateAdvisoryRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "TotalRevenue", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "TotalRevenue");
        }
    }
}
