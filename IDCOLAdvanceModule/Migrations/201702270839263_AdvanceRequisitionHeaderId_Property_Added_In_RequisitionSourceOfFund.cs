namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceRequisitionHeaderId_Property_Added_In_RequisitionSourceOfFund : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId", c => c.Long(nullable: false));
            CreateIndex("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId");
            AddForeignKey("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropIndex("dbo.RequisitionSourceOfFund", new[] { "AdvanceRequisitionHeaderId" });
            DropColumn("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId");
        }
    }
}
