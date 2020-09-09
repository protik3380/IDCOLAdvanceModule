namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceExpenseHeaderId_Added_In_RequisitionSourceOfFund : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RequisitionSourceOfFund", new[] { "AdvanceRequisitionHeaderId" });
            AddColumn("dbo.RequisitionSourceOfFund", "AdvanceExpenseHeaderId", c => c.Long());
            AlterColumn("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId", c => c.Long());
            CreateIndex("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequisitionSourceOfFund", new[] { "AdvanceRequisitionHeaderId" });
            AlterColumn("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId", c => c.Long(nullable: false));
            DropColumn("dbo.RequisitionSourceOfFund", "AdvanceExpenseHeaderId");
            CreateIndex("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId");
        }
    }
}
