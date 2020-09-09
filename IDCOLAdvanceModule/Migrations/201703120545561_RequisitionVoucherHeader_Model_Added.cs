namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionVoucherHeader_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequisitionVoucherHeader",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequisitionHeaderId = c.Long(nullable: false),
                        VoucherEntryDate = c.DateTime(nullable: false),
                        ChequeNo = c.String(),
                        BankId = c.Long(nullable: false),
                        BranchId = c.Long(nullable: false),
                        RecipientName = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        VoucherStatusId = c.Long(nullable: false),
                        Currency = c.String(),
                        ConversionRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionHeader", t => t.RequisitionHeaderId)
                .ForeignKey("dbo.VoucherStatus", t => t.VoucherStatusId)
                .Index(t => t.RequisitionHeaderId)
                .Index(t => t.VoucherStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionVoucherHeader", "VoucherStatusId", "dbo.VoucherStatus");
            DropForeignKey("dbo.RequisitionVoucherHeader", "RequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropIndex("dbo.RequisitionVoucherHeader", new[] { "VoucherStatusId" });
            DropIndex("dbo.RequisitionVoucherHeader", new[] { "RequisitionHeaderId" });
            DropTable("dbo.RequisitionVoucherHeader");
        }
    }
}
