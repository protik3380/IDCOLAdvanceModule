namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseNo_Added_In_AdvanceExpenseHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "ExpenseNo", c => c.String());
            AddColumn("dbo.AdvanceExpenseHeader", "SerialNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "SerialNo");
            DropColumn("dbo.AdvanceExpenseHeader", "ExpenseNo");
        }
    }
}
