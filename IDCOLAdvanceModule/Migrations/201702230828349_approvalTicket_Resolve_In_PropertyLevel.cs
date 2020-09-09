namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class approvalTicket_Resolve_In_PropertyLevel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApprovalTracker", new[] { "ApprovalTicket_Id" });
            RenameColumn(table: "dbo.ApprovalTracker", name: "ApprovalTicket_Id", newName: "ApprovalTicketId");
            AlterColumn("dbo.ApprovalTracker", "ApprovalTicketId", c => c.Long(nullable: false));
            CreateIndex("dbo.ApprovalTracker", "ApprovalTicketId");
            DropColumn("dbo.ApprovalTracker", "RequisitionApprovalTicketId");
            Sql(@"BEGIN TRANSACTION
				SET QUOTED_IDENTIFIER ON
				SET ARITHABORT ON
				SET NUMERIC_ROUNDABORT OFF
				SET CONCAT_NULL_YIELDS_NULL ON
				SET ANSI_NULLS ON
				SET ANSI_PADDING ON
				SET ANSI_WARNINGS ON
				COMMIT
				BEGIN TRANSACTION
				GO
				ALTER TABLE dbo.AdvanceExpenseHeader
					DROP CONSTRAINT [FK_dbo.AdvanceExpenseHeader_dbo.AdvanceExpenseStatus_AdvanceExpenseStatusId]
				GO
				ALTER TABLE dbo.AdvanceExpenseStatus SET (LOCK_ESCALATION = TABLE)
				GO
				COMMIT
				BEGIN TRANSACTION
				GO
				ALTER TABLE dbo.AdvanceExpenseHeader SET (LOCK_ESCALATION = TABLE)
				GO
				COMMIT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApprovalTracker", "RequisitionApprovalTicketId", c => c.Long(nullable: false));
            DropIndex("dbo.ApprovalTracker", new[] { "ApprovalTicketId" });
            AlterColumn("dbo.ApprovalTracker", "ApprovalTicketId", c => c.Long());
            RenameColumn(table: "dbo.ApprovalTracker", name: "ApprovalTicketId", newName: "ApprovalTicket_Id");
            CreateIndex("dbo.ApprovalTracker", "ApprovalTicket_Id");
        }
    }
}
