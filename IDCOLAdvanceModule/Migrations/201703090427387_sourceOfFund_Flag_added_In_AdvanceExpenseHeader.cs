namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sourceOfFund_Flag_added_In_AdvanceExpenseHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "IsSourceOfEntered", c => c.Boolean(nullable: false));
            AddColumn("dbo.AdvanceExpenseHeader", "IsSourceOfFundVerified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "IsSourceOfFundVerified");
            DropColumn("dbo.AdvanceExpenseHeader", "IsSourceOfEntered");
        }
    }
}
