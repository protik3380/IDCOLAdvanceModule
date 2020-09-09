namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Expense_Amount_Added_To_AdvanceExpenseDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AdvanceExpenseDetail", "ExpenseAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AdvanceExpenseDetail", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseDetail", "Discriminator");
            DropColumn("dbo.AdvanceExpenseDetail", "ExpenseAmount");
            DropColumn("dbo.AdvanceExpenseHeader", "Discriminator");
        }
    }
}
