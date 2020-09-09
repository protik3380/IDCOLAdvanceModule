namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsApprovalAuthority_Added_In_ApprovalLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevel", "IsApprovalAuthority", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalLevel", "IsApprovalAuthority");
        }
    }
}
