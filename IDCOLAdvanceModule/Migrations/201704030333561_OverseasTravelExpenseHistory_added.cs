namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverseasTravelExpenseHistory_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseHistoryHeader", "PlaceOfVisitId", c => c.Long());
            AddColumn("dbo.ExpenseHistoryHeader", "OverseasSourceOfFund", c => c.String());
            AddColumn("dbo.ExpenseHistoryHeader", "IsOverseasSponsorFinanced", c => c.Boolean());
            AddColumn("dbo.ExpenseHistoryHeader", "OverseasSponsorName", c => c.String());
            AddColumn("dbo.ExpenseHistoryHeader", "CountryName", c => c.String());
            AddColumn("dbo.ExpenseHistoryDetail", "OverseasTravelCostItemId", c => c.Long());
            AddColumn("dbo.ExpenseHistoryDetail", "OverseasFromDate", c => c.DateTime());
            AddColumn("dbo.ExpenseHistoryDetail", "OverseasToDate", c => c.DateTime());
            AddColumn("dbo.ExpenseHistoryDetail", "OverseasSponsorFinancedDetailAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ExpenseHistoryDetail", "Currency", c => c.String());
            AddColumn("dbo.ExpenseHistoryDetail", "ConversionRate", c => c.Double());
            CreateIndex("dbo.ExpenseHistoryDetail", "OverseasTravelCostItemId");
            AddForeignKey("dbo.ExpenseHistoryDetail", "OverseasTravelCostItemId", "dbo.CostItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseHistoryDetail", "OverseasTravelCostItemId", "dbo.CostItem");
            DropIndex("dbo.ExpenseHistoryDetail", new[] { "OverseasTravelCostItemId" });
            DropColumn("dbo.ExpenseHistoryDetail", "ConversionRate");
            DropColumn("dbo.ExpenseHistoryDetail", "Currency");
            DropColumn("dbo.ExpenseHistoryDetail", "OverseasSponsorFinancedDetailAmount");
            DropColumn("dbo.ExpenseHistoryDetail", "OverseasToDate");
            DropColumn("dbo.ExpenseHistoryDetail", "OverseasFromDate");
            DropColumn("dbo.ExpenseHistoryDetail", "OverseasTravelCostItemId");
            DropColumn("dbo.ExpenseHistoryHeader", "CountryName");
            DropColumn("dbo.ExpenseHistoryHeader", "OverseasSponsorName");
            DropColumn("dbo.ExpenseHistoryHeader", "IsOverseasSponsorFinanced");
            DropColumn("dbo.ExpenseHistoryHeader", "OverseasSourceOfFund");
            DropColumn("dbo.ExpenseHistoryHeader", "PlaceOfVisitId");
        }
    }
}
