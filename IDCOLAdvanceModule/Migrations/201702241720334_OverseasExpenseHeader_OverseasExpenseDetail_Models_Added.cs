namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverseasExpenseHeader_OverseasExpenseDetail_Models_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "PlaceOfVisitId", c => c.Long());
            AddColumn("dbo.AdvanceExpenseHeader", "OverseasSourceOfFund", c => c.String());
            AddColumn("dbo.AdvanceExpenseHeader", "IsOverseasSponsorFinanced", c => c.Boolean());
            AddColumn("dbo.AdvanceExpenseHeader", "OverseasSponsorName", c => c.String());
            AddColumn("dbo.AdvanceExpenseDetail", "OverseasTravelCostItemId", c => c.Long());
            AddColumn("dbo.AdvanceExpenseDetail", "OverseasFromDate", c => c.DateTime());
            AddColumn("dbo.AdvanceExpenseDetail", "OverseasToDate", c => c.DateTime());
            CreateIndex("dbo.AdvanceExpenseHeader", "PlaceOfVisitId");
            CreateIndex("dbo.AdvanceExpenseDetail", "OverseasTravelCostItemId");
            AddForeignKey("dbo.AdvanceExpenseDetail", "OverseasTravelCostItemId", "dbo.CostItem", "Id");
            AddForeignKey("dbo.AdvanceExpenseHeader", "PlaceOfVisitId", "dbo.PlaceOfVisit", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdvanceExpenseHeader", "PlaceOfVisitId", "dbo.PlaceOfVisit");
            DropForeignKey("dbo.AdvanceExpenseDetail", "OverseasTravelCostItemId", "dbo.CostItem");
            DropIndex("dbo.AdvanceExpenseDetail", new[] { "OverseasTravelCostItemId" });
            DropIndex("dbo.AdvanceExpenseHeader", new[] { "PlaceOfVisitId" });
            DropColumn("dbo.AdvanceExpenseDetail", "OverseasToDate");
            DropColumn("dbo.AdvanceExpenseDetail", "OverseasFromDate");
            DropColumn("dbo.AdvanceExpenseDetail", "OverseasTravelCostItemId");
            DropColumn("dbo.AdvanceExpenseHeader", "OverseasSponsorName");
            DropColumn("dbo.AdvanceExpenseHeader", "IsOverseasSponsorFinanced");
            DropColumn("dbo.AdvanceExpenseHeader", "OverseasSourceOfFund");
            DropColumn("dbo.AdvanceExpenseHeader", "PlaceOfVisitId");
        }
    }
}
