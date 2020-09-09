namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionVoucherDetailId_Added_In_AdvanceRequisitionDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequisitionVoucherDetail", "RequisitionDetailId", "dbo.AdvanceRequisitionDetail");
            DropIndex("dbo.RequisitionVoucherDetail", new[] { "RequisitionDetailId" });
            AddColumn("dbo.AdvanceRequisitionDetail", "RequisitionVoucherDetailId", c => c.Long());
            CreateIndex("dbo.AdvanceRequisitionDetail", "RequisitionVoucherDetailId");
            AddForeignKey("dbo.AdvanceRequisitionDetail", "RequisitionVoucherDetailId", "dbo.RequisitionVoucherDetail", "Id");
            DropColumn("dbo.RequisitionVoucherDetail", "RequisitionDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequisitionVoucherDetail", "RequisitionDetailId", c => c.Long());
            DropForeignKey("dbo.AdvanceRequisitionDetail", "RequisitionVoucherDetailId", "dbo.RequisitionVoucherDetail");
            DropIndex("dbo.AdvanceRequisitionDetail", new[] { "RequisitionVoucherDetailId" });
            DropColumn("dbo.AdvanceRequisitionDetail", "RequisitionVoucherDetailId");
            CreateIndex("dbo.RequisitionVoucherDetail", "RequisitionDetailId");
            AddForeignKey("dbo.RequisitionVoucherDetail", "RequisitionDetailId", "dbo.AdvanceRequisitionDetail", "Id");
        }
    }
}
