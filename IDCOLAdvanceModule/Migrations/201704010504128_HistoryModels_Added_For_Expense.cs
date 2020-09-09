namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryModels_Added_For_Expense : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseHistoryDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExpenseHistroyHeaderId = c.Long(nullable: false),
                        AdvanceRequisitionDetailId = c.Long(),
                        NoOfUnit = c.Double(),
                        UnitCost = c.Decimal(precision: 18, scale: 2),
                        Purpose = c.String(),
                        AdvanceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        ReceipientOrPayeeName = c.String(),
                        IsThirdPartyReceipient = c.Boolean(nullable: false),
                        VatTaxTypeId = c.Long(),
                        HistoryModeId = c.Long(nullable: false),
                        ExpenseHistoryHeader_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseHistoryHeader", t => t.ExpenseHistoryHeader_Id)
                .ForeignKey("dbo.HistoryMode", t => t.HistoryModeId)
                .ForeignKey("dbo.VatTaxType", t => t.VatTaxTypeId)
                .Index(t => t.VatTaxTypeId)
                .Index(t => t.HistoryModeId)
                .Index(t => t.ExpenseHistoryHeader_Id);
            
            CreateTable(
                "dbo.ExpenseHistoryHeader",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceExpenseHeaderId = c.Long(nullable: false),
                        RequesterUserName = c.String(),
                        RequesterDepartmentId = c.Decimal(precision: 18, scale: 2),
                        RequesterRankId = c.Decimal(precision: 18, scale: 2),
                        RequesterSupervisorId = c.Decimal(precision: 18, scale: 2),
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
                        RejectedOn = c.DateTime(),
                        RejectedBy = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        PaidBy = c.String(),
                        ExpenseIssueDate = c.DateTime(),
                        IsSourceOfEntered = c.Boolean(nullable: false),
                        IsSourceOfFundVerified = c.Boolean(nullable: false),
                        ExpenseNo = c.String(),
                        SerialNo = c.Int(nullable: false),
                        HistoryModeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceCategory", t => t.AdvanceCategoryId)
                .ForeignKey("dbo.AdvanceStatus", t => t.AdvanceExpenseStatusId)
                .ForeignKey("dbo.HistoryMode", t => t.HistoryModeId)
                .Index(t => t.AdvanceCategoryId)
                .Index(t => t.AdvanceExpenseStatusId)
                .Index(t => t.HistoryModeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseHistoryDetail", "VatTaxTypeId", "dbo.VatTaxType");
            DropForeignKey("dbo.ExpenseHistoryDetail", "HistoryModeId", "dbo.HistoryMode");
            DropForeignKey("dbo.ExpenseHistoryHeader", "HistoryModeId", "dbo.HistoryMode");
            DropForeignKey("dbo.ExpenseHistoryDetail", "ExpenseHistoryHeader_Id", "dbo.ExpenseHistoryHeader");
            DropForeignKey("dbo.ExpenseHistoryHeader", "AdvanceExpenseStatusId", "dbo.AdvanceStatus");
            DropForeignKey("dbo.ExpenseHistoryHeader", "AdvanceCategoryId", "dbo.AdvanceCategory");
            DropIndex("dbo.ExpenseHistoryHeader", new[] { "HistoryModeId" });
            DropIndex("dbo.ExpenseHistoryHeader", new[] { "AdvanceExpenseStatusId" });
            DropIndex("dbo.ExpenseHistoryHeader", new[] { "AdvanceCategoryId" });
            DropIndex("dbo.ExpenseHistoryDetail", new[] { "ExpenseHistoryHeader_Id" });
            DropIndex("dbo.ExpenseHistoryDetail", new[] { "HistoryModeId" });
            DropIndex("dbo.ExpenseHistoryDetail", new[] { "VatTaxTypeId" });
            DropTable("dbo.ExpenseHistoryHeader");
            DropTable("dbo.ExpenseHistoryDetail");
        }
    }
}
