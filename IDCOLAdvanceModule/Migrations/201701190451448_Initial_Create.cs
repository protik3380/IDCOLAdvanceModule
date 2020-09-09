namespace IDCOLAdvanceModule.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvanceRequisitionCategory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DisplaySerial = c.Int(),
                        IsCeilingApplicable = c.Boolean(nullable: false),
                        CeilingAmount = c.Decimal(precision: 18, scale: 2),
                        BaseAdvanceCategoryId = c.Long(nullable: false),
                        BaseAdvanceRequisitionCategory_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseAdvanceRequisitionCategory", t => t.BaseAdvanceRequisitionCategory_Id)
                .Index(t => t.BaseAdvanceRequisitionCategory_Id);

            CreateTable(
                "dbo.BaseAdvanceRequisitionCategory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DisplaySerial = c.Int(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AdvanceRequisitionDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceRequisitionHeaderId = c.Long(nullable: false),
                        NoOfUnit = c.Double(),
                        UnitCost = c.Decimal(precision: 18, scale: 2),
                        Purpose = c.String(),
                        AdvanceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        VendorId = c.Long(),
                        IsVendorReceipient = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        LastModifiedOn = c.DateTime(),
                        MiscelleneousCostItemId = c.Long(),
                        TravelCostItemId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionHeader", t => t.AdvanceRequisitionHeaderId, cascadeDelete: true)
                .ForeignKey("dbo.CostItem", t => t.MiscelleneousCostItemId)
                .ForeignKey("dbo.CostItem", t => t.TravelCostItemId)
                .Index(t => t.AdvanceRequisitionHeaderId)
                .Index(t => t.MiscelleneousCostItemId)
                .Index(t => t.TravelCostItemId);

            CreateTable(
                "dbo.AdvanceRequisitionHeader",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeUserName = c.String(),
                        Purpose = c.String(),
                        AdvanceRequisitionCategoryId = c.Long(nullable: false),
                        RequisitionDate = c.DateTime(nullable: false),
                        FromDate = c.DateTime(),
                        NoOfDays = c.Double(nullable: false),
                        ToDate = c.DateTime(),
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
                        PlaceOfEvent = c.String(),
                        PlaceOfVisit = c.String(),
                        SourceOfFund = c.String(),
                        IsSponsorFinanced = c.Boolean(),
                        SponsorName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionCategory", t => t.AdvanceRequisitionCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AdvanceRequisitionStatus", t => t.AdvanceRequisitionStatusId, cascadeDelete: true)
                .Index(t => t.AdvanceRequisitionCategoryId)
                .Index(t => t.AdvanceRequisitionStatusId);

            CreateTable(
                "dbo.AdvanceRequisitionStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.CostItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.RequisitionCategoryWiseCostItemSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceRequisitionCategoryId = c.Long(nullable: false),
                        CostItemId = c.Long(nullable: false),
                        IsEntitlementMandatory = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionCategory", t => t.AdvanceRequisitionCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.CostItem", t => t.CostItemId, cascadeDelete: true)
                .Index(t => t.AdvanceRequisitionCategoryId)
                .Index(t => t.CostItemId);

            CreateTable(
                "dbo.EmployeeCategory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EmployeeCategorySetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeCategoryId = c.Long(nullable: false),
                        AdminRankId = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeCategory", t => t.EmployeeCategoryId, cascadeDelete: true)
                .Index(t => t.EmployeeCategoryId);

            CreateTable(
                "dbo.EntitlementMappingSettingDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RankID = c.Long(nullable: false),
                        EntitlementAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EntitlementMappingSettingHeaderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntitlementMappingSettingHeader", t => t.EntitlementMappingSettingHeaderId, cascadeDelete: true)
                .Index(t => t.EntitlementMappingSettingHeaderId);

            CreateTable(
                "dbo.EntitlementMappingSettingHeader",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceRequisitionCategoryId = c.Long(nullable: false),
                        CostItemId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionCategory", t => t.AdvanceRequisitionCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.CostItem", t => t.CostItemId, cascadeDelete: true)
                .Index(t => t.AdvanceRequisitionCategoryId)
                .Index(t => t.CostItemId);

            CreateTable(
                "dbo.Entitlement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserRankId = c.Int(nullable: false),
                        EntitlementAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostItemId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostItem", t => t.CostItemId, cascadeDelete: true)
                .Index(t => t.CostItemId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Entitlement", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.EntitlementMappingSettingDetail", "EntitlementMappingSettingHeaderId", "dbo.EntitlementMappingSettingHeader");
            DropForeignKey("dbo.EntitlementMappingSettingHeader", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.EntitlementMappingSettingHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.EmployeeCategorySetting", "EmployeeCategoryId", "dbo.EmployeeCategory");
            DropForeignKey("dbo.AdvanceRequisitionDetail", "TravelCostItemId", "dbo.CostItem");
            DropForeignKey("dbo.AdvanceRequisitionDetail", "MiscelleneousCostItemId", "dbo.CostItem");
            DropForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionStatusId", "dbo.AdvanceRequisitionStatus");
            DropForeignKey("dbo.AdvanceRequisitionDetail", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategory_Id", "dbo.BaseAdvanceRequisitionCategory");
            DropIndex("dbo.Entitlement", new[] { "CostItemId" });
            DropIndex("dbo.EntitlementMappingSettingHeader", new[] { "CostItemId" });
            DropIndex("dbo.EntitlementMappingSettingHeader", new[] { "AdvanceRequisitionCategoryId" });
            DropIndex("dbo.EntitlementMappingSettingDetail", new[] { "EntitlementMappingSettingHeaderId" });
            DropIndex("dbo.EmployeeCategorySetting", new[] { "EmployeeCategoryId" });
            DropIndex("dbo.RequisitionCategoryWiseCostItemSetting", new[] { "CostItemId" });
            DropIndex("dbo.RequisitionCategoryWiseCostItemSetting", new[] { "AdvanceRequisitionCategoryId" });
            DropIndex("dbo.AdvanceRequisitionHeader", new[] { "AdvanceRequisitionStatusId" });
            DropIndex("dbo.AdvanceRequisitionHeader", new[] { "AdvanceRequisitionCategoryId" });
            DropIndex("dbo.AdvanceRequisitionDetail", new[] { "TravelCostItemId" });
            DropIndex("dbo.AdvanceRequisitionDetail", new[] { "MiscelleneousCostItemId" });
            DropIndex("dbo.AdvanceRequisitionDetail", new[] { "AdvanceRequisitionHeaderId" });
            DropIndex("dbo.AdvanceRequisitionCategory", new[] { "BaseAdvanceRequisitionCategory_Id" });
            DropTable("dbo.Entitlement");
            DropTable("dbo.EntitlementMappingSettingHeader");
            DropTable("dbo.EntitlementMappingSettingDetail");
            DropTable("dbo.EmployeeCategorySetting");
            DropTable("dbo.EmployeeCategory");
            DropTable("dbo.RequisitionCategoryWiseCostItemSetting");
            DropTable("dbo.CostItem");
            DropTable("dbo.AdvanceRequisitionStatus");
            DropTable("dbo.AdvanceRequisitionHeader");
            DropTable("dbo.AdvanceRequisitionDetail");
            DropTable("dbo.BaseAdvanceRequisitionCategory");
            DropTable("dbo.AdvanceRequisitionCategory");
        }
    }
}
