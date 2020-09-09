namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Relationship_In_ApprovalLevelForApprovalPanel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ApprovalLevelForApprovalPanel", "ApprovalPanelId");
            CreateIndex("dbo.ApprovalLevelForApprovalPanel", "ApprovalLevelId");
            CreateIndex("dbo.ApprovalLevelForApprovalPanel", "ParentApprovalLevelId");
            AddForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
            AddForeignKey("dbo.ApprovalLevelForApprovalPanel", "ParentApprovalLevelId", "dbo.ApprovalLevel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApprovalLevelForApprovalPanel", "ParentApprovalLevelId", "dbo.ApprovalLevel");
            DropForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropIndex("dbo.ApprovalLevelForApprovalPanel", new[] { "ParentApprovalLevelId" });
            DropIndex("dbo.ApprovalLevelForApprovalPanel", new[] { "ApprovalLevelId" });
            DropIndex("dbo.ApprovalLevelForApprovalPanel", new[] { "ApprovalPanelId" });
        }
    }
}
