namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseRelatedModels_ApprovalTicket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequisitionApprovalTicket", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropForeignKey("dbo.RequisitionApprovalTracker", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropForeignKey("dbo.RequisitionApprovalTicket", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropForeignKey("dbo.RequisitionApprovalTracker", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropForeignKey("dbo.DesinationUserForTicket", "RequisitionApprovalTicketId", "dbo.RequisitionApprovalTicket");
            DropForeignKey("dbo.RequisitionApprovalTracker", "RequisitionApprovalTicketId", "dbo.RequisitionApprovalTicket");
            RenameTable(name: "dbo.RequisitionApprovalTicket", newName: "ApprovalTicket");
            RenameTable(name: "dbo.RequisitionApprovalTracker", newName: "ApprovalTracker");
          
            DropIndex("dbo.ApprovalTracker", new[] { "RequisitionApprovalTicketId" });
            CreateTable(
                "dbo.AdvanceExpenseHeader",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequesterUserName = c.String(),
                        RequesterDepartmentId = c.Decimal(precision: 18, scale: 2),
                        RequesterRankId = c.Decimal(precision: 18, scale: 2),
                        RequesterSupervisorId = c.Decimal(precision: 18, scale: 2),
                        AdvanceRequisitionHeaderId = c.Long(),
                        Purpose = c.String(),
                        AdvanceCategoryId = c.Long(nullable: false),
                        ExpenseEntryDate = c.DateTime(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        NoOfDays = c.Double(nullable: false),
                        Currency = c.String(),
                        ConversionRate = c.Double(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        LastModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        AdvanceExpenseStatusId = c.Long(nullable: false),
                        RecommendedBy = c.String(),
                        RecommendedOn = c.DateTime(),
                        VerifiedBy = c.String(),
                        VerifiedOn = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceCategory", t => t.AdvanceCategoryId)
                .ForeignKey("dbo.AdvanceExpenseStatus", t => t.AdvanceExpenseStatusId)
                .ForeignKey("dbo.AdvanceRequisitionHeader", t => t.AdvanceRequisitionHeaderId)
                .Index(t => t.AdvanceRequisitionHeaderId)
                .Index(t => t.AdvanceCategoryId)
                .Index(t => t.AdvanceExpenseStatusId);
            
            CreateTable(
                "dbo.AdvanceExpenseDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceExpenseHeaderId = c.Long(nullable: false),
                        AdvanceRequisitionDetailId = c.Long(),
                        NoOfUnit = c.Double(),
                        UnitCost = c.Decimal(precision: 18, scale: 2),
                        Purpose = c.String(),
                        AdvanceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        Vendor = c.String(),
                        IsVendorReceipient = c.Boolean(nullable: false),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceExpenseHeader", t => t.AdvanceExpenseHeaderId)
                .ForeignKey("dbo.AdvanceRequisitionDetail", t => t.AdvanceRequisitionDetailId)
                .Index(t => t.AdvanceExpenseHeaderId)
                .Index(t => t.AdvanceRequisitionDetailId);
            
            CreateTable(
                "dbo.AdvanceExpenseStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ApprovalTicket", "TicketTypeId", c => c.Long(nullable: false));
            AddColumn("dbo.ApprovalTicket", "AdvanceExpenseHeader_Id", c => c.Long());
            AddColumn("dbo.ApprovalTicket", "ApprovalLevel_Id", c => c.Long());
            AddColumn("dbo.ApprovalTicket", "ApprovalPanel_Id", c => c.Long());
            AddColumn("dbo.AdvanceRequisitionDetail", "FromDate", c => c.DateTime());
            AddColumn("dbo.AdvanceRequisitionDetail", "ToDate", c => c.DateTime());
            AddColumn("dbo.DesinationUserForTicket", "ApprovalTicket_Id", c => c.Long());
            AddColumn("dbo.ApprovalTracker", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ApprovalTracker", "ApprovalTicket_Id", c => c.Long());
            AddColumn("dbo.ApprovalTracker", "ApprovalLevel_Id", c => c.Long());
            AddColumn("dbo.ApprovalTracker", "ApprovalPanel_Id", c => c.Long());
            CreateIndex("dbo.ApprovalTicket", "TicketTypeId");
            CreateIndex("dbo.ApprovalTicket", "AdvanceExpenseHeader_Id");
            CreateIndex("dbo.ApprovalTicket", "ApprovalLevel_Id");
            CreateIndex("dbo.ApprovalTicket", "ApprovalPanel_Id");
            CreateIndex("dbo.ApprovalTracker", "ApprovalTicket_Id");
            CreateIndex("dbo.ApprovalTracker", "ApprovalLevel_Id");
            CreateIndex("dbo.ApprovalTracker", "ApprovalPanel_Id");
            CreateIndex("dbo.DesinationUserForTicket", "ApprovalTicket_Id");
            AddForeignKey("dbo.ApprovalTicket", "TicketTypeId", "dbo.TicketType", "Id");
            AddForeignKey("dbo.ApprovalTicket", "AdvanceExpenseHeader_Id", "dbo.AdvanceExpenseHeader", "Id");
            AddForeignKey("dbo.ApprovalTicket", "ApprovalPanel_Id", "dbo.ApprovalPanel", "Id");
            AddForeignKey("dbo.ApprovalTracker", "ApprovalPanel_Id", "dbo.ApprovalPanel", "Id");
            AddForeignKey("dbo.ApprovalTicket", "ApprovalLevel_Id", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.ApprovalTracker", "ApprovalLevel_Id", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.DesinationUserForTicket", "ApprovalTicket_Id", "dbo.ApprovalTicket", "Id");
            AddForeignKey("dbo.ApprovalTracker", "ApprovalTicket_Id", "dbo.ApprovalTicket", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApprovalTracker", "ApprovalTicket_Id", "dbo.ApprovalTicket");
            DropForeignKey("dbo.DesinationUserForTicket", "ApprovalTicket_Id", "dbo.ApprovalTicket");
            DropForeignKey("dbo.ApprovalTracker", "ApprovalLevel_Id", "dbo.ApprovalLevel");
            DropForeignKey("dbo.ApprovalTicket", "ApprovalLevel_Id", "dbo.ApprovalLevel");
            DropForeignKey("dbo.ApprovalTracker", "ApprovalPanel_Id", "dbo.ApprovalPanel");
            DropForeignKey("dbo.ApprovalTicket", "ApprovalPanel_Id", "dbo.ApprovalPanel");
            DropForeignKey("dbo.ApprovalTicket", "AdvanceExpenseHeader_Id", "dbo.AdvanceExpenseHeader");
            DropForeignKey("dbo.ApprovalTicket", "TicketTypeId", "dbo.TicketType");
            DropForeignKey("dbo.AdvanceExpenseHeader", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropForeignKey("dbo.AdvanceExpenseHeader", "AdvanceExpenseStatusId", "dbo.AdvanceExpenseStatus");
            DropForeignKey("dbo.AdvanceExpenseDetail", "AdvanceRequisitionDetailId", "dbo.AdvanceRequisitionDetail");
            DropForeignKey("dbo.AdvanceExpenseDetail", "AdvanceExpenseHeaderId", "dbo.AdvanceExpenseHeader");
            DropForeignKey("dbo.AdvanceExpenseHeader", "AdvanceCategoryId", "dbo.AdvanceCategory");
            DropIndex("dbo.DesinationUserForTicket", new[] { "ApprovalTicket_Id" });
            DropIndex("dbo.ApprovalTracker", new[] { "ApprovalPanel_Id" });
            DropIndex("dbo.ApprovalTracker", new[] { "ApprovalLevel_Id" });
            DropIndex("dbo.ApprovalTracker", new[] { "ApprovalTicket_Id" });
            DropIndex("dbo.AdvanceExpenseDetail", new[] { "AdvanceRequisitionDetailId" });
            DropIndex("dbo.AdvanceExpenseDetail", new[] { "AdvanceExpenseHeaderId" });
            DropIndex("dbo.AdvanceExpenseHeader", new[] { "AdvanceExpenseStatusId" });
            DropIndex("dbo.AdvanceExpenseHeader", new[] { "AdvanceCategoryId" });
            DropIndex("dbo.AdvanceExpenseHeader", new[] { "AdvanceRequisitionHeaderId" });
            DropIndex("dbo.ApprovalTicket", new[] { "ApprovalPanel_Id" });
            DropIndex("dbo.ApprovalTicket", new[] { "ApprovalLevel_Id" });
            DropIndex("dbo.ApprovalTicket", new[] { "AdvanceExpenseHeader_Id" });
            DropIndex("dbo.ApprovalTicket", new[] { "TicketTypeId" });
            DropColumn("dbo.ApprovalTracker", "ApprovalPanel_Id");
            DropColumn("dbo.ApprovalTracker", "ApprovalLevel_Id");
            DropColumn("dbo.ApprovalTracker", "ApprovalTicket_Id");
            DropColumn("dbo.ApprovalTracker", "Discriminator");
            DropColumn("dbo.DesinationUserForTicket", "ApprovalTicket_Id");
            DropColumn("dbo.AdvanceRequisitionDetail", "ToDate");
            DropColumn("dbo.AdvanceRequisitionDetail", "FromDate");
            DropColumn("dbo.ApprovalTicket", "ApprovalPanel_Id");
            DropColumn("dbo.ApprovalTicket", "ApprovalLevel_Id");
            DropColumn("dbo.ApprovalTicket", "AdvanceExpenseHeader_Id");
            DropColumn("dbo.ApprovalTicket", "TicketTypeId");
            DropTable("dbo.TicketType");
            DropTable("dbo.AdvanceExpenseStatus");
            DropTable("dbo.AdvanceExpenseDetail");
            DropTable("dbo.AdvanceExpenseHeader");
            CreateIndex("dbo.ApprovalTracker", "RequisitionApprovalTicketId");
            AddForeignKey("dbo.RequisitionApprovalTracker", "RequisitionApprovalTicketId", "dbo.RequisitionApprovalTicket", "Id");
            AddForeignKey("dbo.DesinationUserForTicket", "RequisitionApprovalTicketId", "dbo.RequisitionApprovalTicket", "Id");
            AddForeignKey("dbo.RequisitionApprovalTracker", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.RequisitionApprovalTicket", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.RequisitionApprovalTracker", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
            AddForeignKey("dbo.RequisitionApprovalTicket", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
            RenameTable(name: "dbo.ApprovalTracker", newName: "RequisitionApprovalTracker");
            RenameTable(name: "dbo.ApprovalTicket", newName: "RequisitionApprovalTicket");
        }
    }
}
