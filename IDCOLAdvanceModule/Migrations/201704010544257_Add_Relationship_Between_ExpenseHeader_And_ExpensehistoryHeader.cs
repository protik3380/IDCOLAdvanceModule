namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Relationship_Between_ExpenseHeader_And_ExpensehistoryHeader : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ExpenseHistoryHeader", "AdvanceExpenseHeaderId");
            AddForeignKey("dbo.ExpenseHistoryHeader", "AdvanceExpenseHeaderId", "dbo.AdvanceExpenseHeader", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseHistoryHeader", "AdvanceExpenseHeaderId", "dbo.AdvanceExpenseHeader");
            DropIndex("dbo.ExpenseHistoryHeader", new[] { "AdvanceExpenseHeaderId" });
        }
    }
}
