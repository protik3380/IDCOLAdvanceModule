namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TravelExpenseHistory_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseHistoryHeader", "PlaceOfVisit", c => c.String());
            AddColumn("dbo.ExpenseHistoryHeader", "SourceOfFund", c => c.String());
            AddColumn("dbo.ExpenseHistoryHeader", "IsSponsorFinanced", c => c.Boolean());
            AddColumn("dbo.ExpenseHistoryHeader", "SponsorName", c => c.String());
            AddColumn("dbo.ExpenseHistoryDetail", "TravelCostItemId", c => c.Long());
            AddColumn("dbo.ExpenseHistoryDetail", "FromDate", c => c.DateTime());
            AddColumn("dbo.ExpenseHistoryDetail", "ToDate", c => c.DateTime());
            CreateIndex("dbo.ExpenseHistoryDetail", "TravelCostItemId");
            AddForeignKey("dbo.ExpenseHistoryDetail", "TravelCostItemId", "dbo.CostItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseHistoryDetail", "TravelCostItemId", "dbo.CostItem");
            DropIndex("dbo.ExpenseHistoryDetail", new[] { "TravelCostItemId" });
            DropColumn("dbo.ExpenseHistoryDetail", "ToDate");
            DropColumn("dbo.ExpenseHistoryDetail", "FromDate");
            DropColumn("dbo.ExpenseHistoryDetail", "TravelCostItemId");
            DropColumn("dbo.ExpenseHistoryHeader", "SponsorName");
            DropColumn("dbo.ExpenseHistoryHeader", "IsSponsorFinanced");
            DropColumn("dbo.ExpenseHistoryHeader", "SourceOfFund");
            DropColumn("dbo.ExpenseHistoryHeader", "PlaceOfVisit");
        }
    }
}
