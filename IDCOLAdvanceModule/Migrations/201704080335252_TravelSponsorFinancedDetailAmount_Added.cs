namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TravelSponsorFinancedDetailAmount_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseDetail", "TravelSponsorFinancedDetailAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseDetail", "TravelSponsorFinancedDetailAmount");
        }
    }
}
