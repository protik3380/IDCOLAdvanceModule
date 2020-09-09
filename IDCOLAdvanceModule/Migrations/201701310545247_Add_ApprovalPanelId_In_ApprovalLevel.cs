namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ApprovalPanelId_In_ApprovalLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevel", "LevelOrder", c => c.Int(nullable: false));
            AddColumn("dbo.ApprovalLevel", "ApprovalPanelId", c => c.Long(nullable: false));
            CreateIndex("dbo.ApprovalLevel", "ApprovalPanelId");
            AddForeignKey("dbo.ApprovalLevel", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApprovalLevel", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropIndex("dbo.ApprovalLevel", new[] { "ApprovalPanelId" });
            DropColumn("dbo.ApprovalLevel", "ApprovalPanelId");
            DropColumn("dbo.ApprovalLevel", "LevelOrder");
        }
    }
}
