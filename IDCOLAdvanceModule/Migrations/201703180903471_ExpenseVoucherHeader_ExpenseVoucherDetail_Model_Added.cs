namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseVoucherHeader_ExpenseVoucherDetail_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseVoucherDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VoucherHeaderId = c.Long(nullable: false),
                        AccountCode = c.String(),
                        DrAmount = c.Decimal(precision: 18, scale: 2),
                        CrAmount = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseVoucherHeader", t => t.VoucherHeaderId)
                .Index(t => t.VoucherHeaderId);
            
            CreateTable(
                "dbo.ExpenseVoucherHeader",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExpenseHeaderId = c.Long(nullable: false),
                        VoucherEntryDate = c.DateTime(nullable: false),
                        ChequeNo = c.String(),
                        BankId = c.Long(nullable: false),
                        BranchId = c.Long(nullable: false),
                        RecipientName = c.String(),
                        VoucherDescription = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        VoucherStatusId = c.Long(nullable: false),
                        Currency = c.String(),
                        ConversionRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceExpenseHeader", t => t.ExpenseHeaderId)
                .ForeignKey("dbo.VoucherStatus", t => t.VoucherStatusId)
                .Index(t => t.ExpenseHeaderId)
                .Index(t => t.VoucherStatusId);
            
            AddColumn("dbo.AdvanceExpenseDetail", "ExpenseVoucherDetailId", c => c.Long());
            CreateIndex("dbo.AdvanceExpenseDetail", "ExpenseVoucherDetailId");
            AddForeignKey("dbo.AdvanceExpenseDetail", "ExpenseVoucherDetailId", "dbo.ExpenseVoucherDetail", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseVoucherHeader", "VoucherStatusId", "dbo.VoucherStatus");
            DropForeignKey("dbo.ExpenseVoucherDetail", "VoucherHeaderId", "dbo.ExpenseVoucherHeader");
            DropForeignKey("dbo.ExpenseVoucherHeader", "ExpenseHeaderId", "dbo.AdvanceExpenseHeader");
            DropForeignKey("dbo.AdvanceExpenseDetail", "ExpenseVoucherDetailId", "dbo.ExpenseVoucherDetail");
            DropIndex("dbo.ExpenseVoucherHeader", new[] { "VoucherStatusId" });
            DropIndex("dbo.ExpenseVoucherHeader", new[] { "ExpenseHeaderId" });
            DropIndex("dbo.ExpenseVoucherDetail", new[] { "VoucherHeaderId" });
            DropIndex("dbo.AdvanceExpenseDetail", new[] { "ExpenseVoucherDetailId" });
            DropColumn("dbo.AdvanceExpenseDetail", "ExpenseVoucherDetailId");
            DropTable("dbo.ExpenseVoucherHeader");
            DropTable("dbo.ExpenseVoucherDetail");
        }
    }
}
