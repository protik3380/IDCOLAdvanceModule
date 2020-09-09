namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PanelTypes_ApprovalPanels_ApprovalLevels_ApprovalLevelForApprovalPanels_added_Initially : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApprovalLevel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApprovalPanel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PanelTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PanelType", t => t.PanelTypeId)
                .Index(t => t.PanelTypeId);
            
            CreateTable(
                "dbo.PanelType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApprovalLevelForApprovalPanel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApprovalPanelId = c.Int(nullable: false),
                        ApprovalLevelId = c.Int(nullable: false),
                        ParentApprovalLevelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.RequisitionApprovalTicket", "ApprovalPanelId");
            CreateIndex("dbo.RequisitionApprovalTicket", "ApprovalLevelId");
            CreateIndex("dbo.RequisitionApprovalTracker", "ApprovalPanelId");
            CreateIndex("dbo.RequisitionApprovalTracker", "ApprovalLevelId");
            AddForeignKey("dbo.RequisitionApprovalTicket", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.RequisitionApprovalTracker", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.RequisitionApprovalTicket", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
            AddForeignKey("dbo.RequisitionApprovalTracker", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionApprovalTracker", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropForeignKey("dbo.RequisitionApprovalTicket", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropForeignKey("dbo.ApprovalPanel", "PanelTypeId", "dbo.PanelType");
            DropForeignKey("dbo.RequisitionApprovalTracker", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropForeignKey("dbo.RequisitionApprovalTicket", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropIndex("dbo.ApprovalPanel", new[] { "PanelTypeId" });
            DropIndex("dbo.RequisitionApprovalTracker", new[] { "ApprovalLevelId" });
            DropIndex("dbo.RequisitionApprovalTracker", new[] { "ApprovalPanelId" });
            DropIndex("dbo.RequisitionApprovalTicket", new[] { "ApprovalLevelId" });
            DropIndex("dbo.RequisitionApprovalTicket", new[] { "ApprovalPanelId" });
            DropTable("dbo.ApprovalLevelForApprovalPanel");
            DropTable("dbo.PanelType");
            DropTable("dbo.ApprovalPanel");
            DropTable("dbo.ApprovalLevel");
        }
    }
}
