namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeleted_Column_Added_In_ApprovalLevelMember_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevelMember", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalLevelMember", "IsDeleted");
        }
    }
}
