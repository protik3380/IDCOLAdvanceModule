namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSourceOfFundEntry_IsSourceOfFundVerify_Added_In_ApprovalLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevel", "IsSourceOfFundEntry", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApprovalLevel", "IsSourceOfFundVerify", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalLevel", "IsSourceOfFundVerify");
            DropColumn("dbo.ApprovalLevel", "IsSourceOfFundEntry");
        }
    }
}
