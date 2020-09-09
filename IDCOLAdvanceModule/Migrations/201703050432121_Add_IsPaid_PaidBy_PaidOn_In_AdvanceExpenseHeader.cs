namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsPaid_PaidBy_PaidOn_In_AdvanceExpenseHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.AdvanceExpenseHeader", "PaidBy", c => c.String());
            AddColumn("dbo.AdvanceExpenseHeader", "PaidOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "PaidOn");
            DropColumn("dbo.AdvanceExpenseHeader", "PaidBy");
            DropColumn("dbo.AdvanceExpenseHeader", "IsPaid");
        }
    }
}
