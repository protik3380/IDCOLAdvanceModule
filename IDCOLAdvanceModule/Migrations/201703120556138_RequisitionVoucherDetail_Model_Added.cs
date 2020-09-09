namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionVoucherDetail_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequisitionVoucherDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VoucherHeaderId = c.Long(nullable: false),
                        RequisitionDetailId = c.Long(),
                        AccountCode = c.String(),
                        DrAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CrAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionDetail", t => t.RequisitionDetailId)
                .ForeignKey("dbo.RequisitionVoucherHeader", t => t.VoucherHeaderId)
                .Index(t => t.VoucherHeaderId)
                .Index(t => t.RequisitionDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionVoucherDetail", "VoucherHeaderId", "dbo.RequisitionVoucherHeader");
            DropForeignKey("dbo.RequisitionVoucherDetail", "RequisitionDetailId", "dbo.AdvanceRequisitionDetail");
            DropIndex("dbo.RequisitionVoucherDetail", new[] { "RequisitionDetailId" });
            DropIndex("dbo.RequisitionVoucherDetail", new[] { "VoucherHeaderId" });
            DropTable("dbo.RequisitionVoucherDetail");
        }
    }
}
