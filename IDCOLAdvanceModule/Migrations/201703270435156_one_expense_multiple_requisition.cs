namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one_expense_multiple_requisition : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdvanceExpenseHeader", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropIndex("dbo.AdvanceExpenseHeader", new[] { "AdvanceRequisitionHeaderId" });
            AddColumn("dbo.AdvanceRequisitionHeader", "AdvanceExpenseHeaderId", c => c.Long());
            CreateIndex("dbo.AdvanceRequisitionHeader", "AdvanceExpenseHeaderId");
            AddForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceExpenseHeaderId", "dbo.AdvanceExpenseHeader", "Id");
            DropColumn("dbo.AdvanceExpenseHeader", "AdvanceRequisitionHeaderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "AdvanceRequisitionHeaderId", c => c.Long());
            DropForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceExpenseHeaderId", "dbo.AdvanceExpenseHeader");
            DropIndex("dbo.AdvanceRequisitionHeader", new[] { "AdvanceExpenseHeaderId" });
            DropColumn("dbo.AdvanceRequisitionHeader", "AdvanceExpenseHeaderId");
            CreateIndex("dbo.AdvanceExpenseHeader", "AdvanceRequisitionHeaderId");
            AddForeignKey("dbo.AdvanceExpenseHeader", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader", "Id");
        }
    }
}
