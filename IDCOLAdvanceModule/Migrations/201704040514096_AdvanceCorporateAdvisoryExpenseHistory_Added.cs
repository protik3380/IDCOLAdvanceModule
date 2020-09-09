namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceCorporateAdvisoryExpenseHistory_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseHistoryHeader", "CorporateAdvisoryPlaceOfEvent", c => c.String());
            AddColumn("dbo.ExpenseHistoryHeader", "NoOfUnit", c => c.Double());
            AddColumn("dbo.ExpenseHistoryHeader", "UnitCost", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ExpenseHistoryHeader", "TotalRevenue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ExpenseHistoryHeader", "AdvanceCorporateRemarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseHistoryHeader", "AdvanceCorporateRemarks");
            DropColumn("dbo.ExpenseHistoryHeader", "TotalRevenue");
            DropColumn("dbo.ExpenseHistoryHeader", "UnitCost");
            DropColumn("dbo.ExpenseHistoryHeader", "NoOfUnit");
            DropColumn("dbo.ExpenseHistoryHeader", "CorporateAdvisoryPlaceOfEvent");
        }
    }
}
