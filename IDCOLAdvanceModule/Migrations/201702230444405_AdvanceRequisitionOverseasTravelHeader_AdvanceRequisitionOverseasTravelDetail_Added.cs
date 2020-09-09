namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceRequisitionOverseasTravelHeader_AdvanceRequisitionOverseasTravelDetail_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "PlaceOfVisitId", c => c.Long());
            AddColumn("dbo.AdvanceRequisitionHeader", "OverseasSourceOfFund", c => c.String());
            AddColumn("dbo.AdvanceRequisitionHeader", "IsOverseasSponsorFinanced", c => c.Boolean());
            AddColumn("dbo.AdvanceRequisitionHeader", "OverseasSponsorName", c => c.String());
            AddColumn("dbo.AdvanceRequisitionDetail", "OverseasTravelCostItemId", c => c.Long());
            AddColumn("dbo.AdvanceRequisitionDetail", "OverseasFromDate", c => c.DateTime());
            AddColumn("dbo.AdvanceRequisitionDetail", "OverseasToDate", c => c.DateTime());
            CreateIndex("dbo.AdvanceRequisitionHeader", "PlaceOfVisitId");
            CreateIndex("dbo.AdvanceRequisitionDetail", "OverseasTravelCostItemId");
            AddForeignKey("dbo.AdvanceRequisitionDetail", "OverseasTravelCostItemId", "dbo.CostItem", "Id");
            AddForeignKey("dbo.AdvanceRequisitionHeader", "PlaceOfVisitId", "dbo.PlaceOfVisit", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdvanceRequisitionHeader", "PlaceOfVisitId", "dbo.PlaceOfVisit");
            DropForeignKey("dbo.AdvanceRequisitionDetail", "OverseasTravelCostItemId", "dbo.CostItem");
            DropIndex("dbo.AdvanceRequisitionDetail", new[] { "OverseasTravelCostItemId" });
            DropIndex("dbo.AdvanceRequisitionHeader", new[] { "PlaceOfVisitId" });
            DropColumn("dbo.AdvanceRequisitionDetail", "OverseasToDate");
            DropColumn("dbo.AdvanceRequisitionDetail", "OverseasFromDate");
            DropColumn("dbo.AdvanceRequisitionDetail", "OverseasTravelCostItemId");
            DropColumn("dbo.AdvanceRequisitionHeader", "OverseasSponsorName");
            DropColumn("dbo.AdvanceRequisitionHeader", "IsOverseasSponsorFinanced");
            DropColumn("dbo.AdvanceRequisitionHeader", "OverseasSourceOfFund");
            DropColumn("dbo.AdvanceRequisitionHeader", "PlaceOfVisitId");
        }
    }
}
