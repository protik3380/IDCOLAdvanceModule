namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriorityOrder_Column_Added_In_ApprovalLevelMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevelMember", "PriorityOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalLevelMember", "PriorityOrder");
        }
    }
}
