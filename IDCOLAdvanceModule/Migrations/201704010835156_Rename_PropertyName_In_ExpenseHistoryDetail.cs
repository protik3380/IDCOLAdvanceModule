namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_PropertyName_In_ExpenseHistoryDetail : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ExpenseHistoryDetail", new[] { "ExpenseHistoryHeader_Id" });
            RenameColumn(table: "dbo.ExpenseHistoryDetail", name: "ExpenseHistoryHeader_Id", newName: "ExpenseHistoryHeaderId");
            AlterColumn("dbo.ExpenseHistoryDetail", "ExpenseHistoryHeaderId", c => c.Long(nullable: false));
            CreateIndex("dbo.ExpenseHistoryDetail", "ExpenseHistoryHeaderId");
            DropColumn("dbo.ExpenseHistoryDetail", "ExpenseHistroyHeaderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExpenseHistoryDetail", "ExpenseHistroyHeaderId", c => c.Long(nullable: false));
            DropIndex("dbo.ExpenseHistoryDetail", new[] { "ExpenseHistoryHeaderId" });
            AlterColumn("dbo.ExpenseHistoryDetail", "ExpenseHistoryHeaderId", c => c.Long());
            RenameColumn(table: "dbo.ExpenseHistoryDetail", name: "ExpenseHistoryHeaderId", newName: "ExpenseHistoryHeader_Id");
            CreateIndex("dbo.ExpenseHistoryDetail", "ExpenseHistoryHeader_Id");
        }
    }
}
