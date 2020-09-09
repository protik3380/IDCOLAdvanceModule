namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequisitionDetailHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequisitionHeaderHistoryId = c.Long(nullable: false),
                        NoOfUnit = c.Double(),
                        UnitCost = c.Decimal(precision: 18, scale: 2),
                        Purpose = c.String(),
                        AdvanceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        ReceipientOrPayeeName = c.String(),
                        IsThirdPartyReceipient = c.Boolean(nullable: false),
                        VatTaxTypeId = c.Long(),
                        MiscelleneousCostItemId = c.Long(),
                        OverseasTravelCostItemId = c.Long(),
                        OverseasFromDate = c.DateTime(),
                        OverseasToDate = c.DateTime(),
                        OverseasSponsorFinancedDetailAmount = c.Decimal(precision: 18, scale: 2),
                        Currency = c.String(),
                        ConversionRate = c.Double(),
                        TravelCostItemId = c.Long(),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        TravelSponsorFinancedDetailAmount = c.Decimal(precision: 18, scale: 2),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequisitionHistoryHeader", t => t.RequisitionHeaderHistoryId)
                .ForeignKey("dbo.VatTaxType", t => t.VatTaxTypeId)
                .ForeignKey("dbo.CostItem", t => t.MiscelleneousCostItemId)
                .ForeignKey("dbo.CostItem", t => t.OverseasTravelCostItemId)
                .ForeignKey("dbo.CostItem", t => t.TravelCostItemId)
                .Index(t => t.RequisitionHeaderHistoryId)
                .Index(t => t.VatTaxTypeId)
                .Index(t => t.MiscelleneousCostItemId)
                .Index(t => t.OverseasTravelCostItemId)
                .Index(t => t.TravelCostItemId);
            
            CreateTable(
                "dbo.RequisitionHistoryHeader",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceRequisitionHeaderId = c.Long(nullable: false),
                        RequesterUserName = c.String(),
                        RequesterDepartmentId = c.Decimal(precision: 18, scale: 2),
                        RequesterRankId = c.Decimal(precision: 18, scale: 2),
                        RequesterSupervisorId = c.Decimal(precision: 18, scale: 2),
                        Purpose = c.String(),
                        AdvanceCategoryId = c.Long(nullable: false),
                        RequisitionDate = c.DateTime(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        NoOfDays = c.Double(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Currency = c.String(),
                        ConversionRate = c.Double(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        LastModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        AdvanceRequisitionStatusId = c.Long(nullable: false),
                        RecommendedBy = c.String(),
                        RecommendedOn = c.DateTime(),
                        VerifiedBy = c.String(),
                        VerifiedOn = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedOn = c.DateTime(),
                        IsSourceOfFundVerified = c.Boolean(nullable: false),
                        IsFundAvailable = c.Boolean(nullable: false),
                        AdvanceIssueDate = c.DateTime(),
                        RejectedOn = c.DateTime(),
                        RejectedBy = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        PaidBy = c.String(),
                        RequisitionNo = c.String(),
                        SerialNo = c.Int(nullable: false),
                        PlaceOfEvent = c.String(),
                        PlaceOfVisitId = c.Long(),
                        OverseasSourceOfFund = c.String(),
                        IsOverseasSponsorFinanced = c.Boolean(),
                        OverseasSponsorName = c.String(),
                        OverseasSponsorFinancedHeaderAmount = c.Decimal(precision: 18, scale: 2),
                        CountryName = c.String(),
                        PlaceOfVisit = c.String(),
                        SourceOfFund = c.String(),
                        IsSponsorFinanced = c.Boolean(),
                        SponsorName = c.String(),
                        TravelSponsorFinancedHeaderAmount = c.Decimal(precision: 18, scale: 2),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceCategory", t => t.AdvanceCategoryId)
                .ForeignKey("dbo.AdvanceRequisitionHeader", t => t.AdvanceRequisitionHeaderId)
                .ForeignKey("dbo.AdvanceStatus", t => t.AdvanceRequisitionStatusId)
                .ForeignKey("dbo.PlaceOfVisit", t => t.PlaceOfVisitId)
                .Index(t => t.AdvanceRequisitionHeaderId)
                .Index(t => t.AdvanceCategoryId)
                .Index(t => t.AdvanceRequisitionStatusId)
                .Index(t => t.PlaceOfVisitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionDetailHistory", "TravelCostItemId", "dbo.CostItem");
            DropForeignKey("dbo.RequisitionDetailHistory", "OverseasTravelCostItemId", "dbo.CostItem");
            DropForeignKey("dbo.RequisitionDetailHistory", "MiscelleneousCostItemId", "dbo.CostItem");
            DropForeignKey("dbo.RequisitionDetailHistory", "VatTaxTypeId", "dbo.VatTaxType");
            DropForeignKey("dbo.RequisitionHistoryHeader", "PlaceOfVisitId", "dbo.PlaceOfVisit");
            DropForeignKey("dbo.RequisitionDetailHistory", "RequisitionHeaderHistoryId", "dbo.RequisitionHistoryHeader");
            DropForeignKey("dbo.RequisitionHistoryHeader", "AdvanceRequisitionStatusId", "dbo.AdvanceStatus");
            DropForeignKey("dbo.RequisitionHistoryHeader", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropForeignKey("dbo.RequisitionHistoryHeader", "AdvanceCategoryId", "dbo.AdvanceCategory");
            DropIndex("dbo.RequisitionHistoryHeader", new[] { "PlaceOfVisitId" });
            DropIndex("dbo.RequisitionHistoryHeader", new[] { "AdvanceRequisitionStatusId" });
            DropIndex("dbo.RequisitionHistoryHeader", new[] { "AdvanceCategoryId" });
            DropIndex("dbo.RequisitionHistoryHeader", new[] { "AdvanceRequisitionHeaderId" });
            DropIndex("dbo.RequisitionDetailHistory", new[] { "TravelCostItemId" });
            DropIndex("dbo.RequisitionDetailHistory", new[] { "OverseasTravelCostItemId" });
            DropIndex("dbo.RequisitionDetailHistory", new[] { "MiscelleneousCostItemId" });
            DropIndex("dbo.RequisitionDetailHistory", new[] { "VatTaxTypeId" });
            DropIndex("dbo.RequisitionDetailHistory", new[] { "RequisitionHeaderHistoryId" });
            DropTable("dbo.RequisitionHistoryHeader");
            DropTable("dbo.RequisitionDetailHistory");
        }
    }
}
