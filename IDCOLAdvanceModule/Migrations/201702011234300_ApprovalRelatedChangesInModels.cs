namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApprovalRelatedChangesInModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionCategory", "ApprovalPanelId", c => c.Long());
            AddColumn("dbo.RequisitionApprovalTicket", "TicketNarration", c => c.String());
            AddColumn("dbo.RequisitionApprovalTicket", "AuthorizedBy", c => c.String());
            AddColumn("dbo.RequisitionApprovalTicket", "AuthorizedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequisitionApprovalTracker", "AuthorizedBy", c => c.String());
            AddColumn("dbo.RequisitionApprovalTracker", "AuthorizedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequisitionApprovalTracker", "TicketNarration", c => c.String());
            CreateIndex("dbo.AdvanceRequisitionCategory", "ApprovalPanelId");
            AddForeignKey("dbo.AdvanceRequisitionCategory", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
            DropColumn("dbo.RequisitionApprovalTicket", "ApprovedBy");
            DropColumn("dbo.RequisitionApprovalTracker", "ApprovedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequisitionApprovalTracker", "ApprovedBy", c => c.String());
            AddColumn("dbo.RequisitionApprovalTicket", "ApprovedBy", c => c.String());
            DropForeignKey("dbo.AdvanceRequisitionCategory", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropIndex("dbo.AdvanceRequisitionCategory", new[] { "ApprovalPanelId" });
            DropColumn("dbo.RequisitionApprovalTracker", "TicketNarration");
            DropColumn("dbo.RequisitionApprovalTracker", "AuthorizedOn");
            DropColumn("dbo.RequisitionApprovalTracker", "AuthorizedBy");
            DropColumn("dbo.RequisitionApprovalTicket", "AuthorizedOn");
            DropColumn("dbo.RequisitionApprovalTicket", "AuthorizedBy");
            DropColumn("dbo.RequisitionApprovalTicket", "TicketNarration");
            DropColumn("dbo.AdvanceRequisitionCategory", "ApprovalPanelId");
        }
    }
}
