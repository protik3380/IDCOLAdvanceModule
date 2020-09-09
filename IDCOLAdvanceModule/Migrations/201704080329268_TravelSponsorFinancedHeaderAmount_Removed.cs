namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TravelSponsorFinancedHeaderAmount_Removed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "TravelSponsorFinancedHeaderAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "TravelSponsorFinancedHeaderAmount", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
