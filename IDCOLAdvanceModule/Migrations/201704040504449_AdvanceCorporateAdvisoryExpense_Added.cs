namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceCorporateAdvisoryExpense_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "CorporateAdvisoryPlaceOfEvent", c => c.String());
            AddColumn("dbo.AdvanceExpenseHeader", "NoOfUnit", c => c.Double());
            AddColumn("dbo.AdvanceExpenseHeader", "UnitCost", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AdvanceExpenseHeader", "TotalRevenue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AdvanceExpenseHeader", "AdvanceCorporateRemarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "AdvanceCorporateRemarks");
            DropColumn("dbo.AdvanceExpenseHeader", "TotalRevenue");
            DropColumn("dbo.AdvanceExpenseHeader", "UnitCost");
            DropColumn("dbo.AdvanceExpenseHeader", "NoOfUnit");
            DropColumn("dbo.AdvanceExpenseHeader", "CorporateAdvisoryPlaceOfEvent");
        }
    }
}
