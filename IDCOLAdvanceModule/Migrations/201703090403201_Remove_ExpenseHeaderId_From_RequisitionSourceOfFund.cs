namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_ExpenseHeaderId_From_RequisitionSourceOfFund : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RequisitionSourceOfFund", new[] { "AdvanceRequisitionHeaderId" });
            AlterColumn("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId", c => c.Long(nullable: false));
            CreateIndex("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId");
            DropColumn("dbo.RequisitionSourceOfFund", "AdvanceExpenseHeaderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequisitionSourceOfFund", "AdvanceExpenseHeaderId", c => c.Long());
            DropIndex("dbo.RequisitionSourceOfFund", new[] { "AdvanceRequisitionHeaderId" });
            AlterColumn("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId", c => c.Long());
            CreateIndex("dbo.RequisitionSourceOfFund", "AdvanceRequisitionHeaderId");
        }
    }
}
