namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DestinationUserForTicket_ApprovalTicketIdAdded : DbMigration
    {
        public override void Up()
        {
            Sql(@"USE [IDCOLAdvanceDB]
                GO

                ALTER TABLE [dbo].[DestinationUserForTicket] DROP CONSTRAINT [FK_dbo.DesinationUserForTicket_dbo.ApprovalTicket_ApprovalTicket_Id]
                GO
                ");

            DropForeignKey("dbo.DestinationUserForTicket", "ApprovalTicket_Id", "dbo.ApprovalTicket");
            DropIndex("dbo.DestinationUserForTicket", new[] { "ApprovalTicket_Id" });
            RenameColumn(table: "dbo.DestinationUserForTicket", name: "RequisitionApprovalTicketId", newName: "ApprovalTicketId");
            RenameIndex(table: "dbo.DestinationUserForTicket", name: "IX_RequisitionApprovalTicketId", newName: "IX_ApprovalTicketId");
            CreateIndex("dbo.DestinationUserForTicket", "ApprovalPanelId");
            CreateIndex("dbo.DestinationUserForTicket", "ApprovalLevelId");
            AddForeignKey("dbo.DestinationUserForTicket", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.DestinationUserForTicket", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
            DropColumn("dbo.DestinationUserForTicket", "ApprovalTicket_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DestinationUserForTicket", "ApprovalTicket_Id", c => c.Long());
            DropForeignKey("dbo.DestinationUserForTicket", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropForeignKey("dbo.DestinationUserForTicket", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropIndex("dbo.DestinationUserForTicket", new[] { "ApprovalLevelId" });
            DropIndex("dbo.DestinationUserForTicket", new[] { "ApprovalPanelId" });
            RenameIndex(table: "dbo.DestinationUserForTicket", name: "IX_ApprovalTicketId", newName: "IX_RequisitionApprovalTicketId");
            RenameColumn(table: "dbo.DestinationUserForTicket", name: "ApprovalTicketId", newName: "RequisitionApprovalTicketId");
            CreateIndex("dbo.DestinationUserForTicket", "ApprovalTicket_Id");
            AddForeignKey("dbo.DestinationUserForTicket", "ApprovalTicket_Id", "dbo.ApprovalTicket", "Id");
        }
    }
}
