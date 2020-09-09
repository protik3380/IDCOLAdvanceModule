namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailNotification_Model_Added : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationNotifiation", newName: "ApplicationNotification");
            DropPrimaryKey("dbo.ApplicationNotification");
            CreateTable(
                "dbo.EmailNotification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                        Cc = c.String(),
                        Subject = c.String(),
                        MessageBody = c.String(),
                        MessageHeader = c.String(),
                        MessageFooter = c.String(),
                        MessageContentHtml = c.String(),
                        ToUserName = c.String(),
                        CcUserName = c.String(),
                        IsSent = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        SentDate = c.DateTime(),
                        ApprovalLevelId = c.Long(nullable: false),
                        ApprovalTicketId = c.Long(nullable: false),
                        TicketStatusId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApprovalLevel", t => t.ApprovalLevelId)
                .ForeignKey("dbo.ApprovalTicket", t => t.ApprovalTicketId)
                .ForeignKey("dbo.ApprovalStatus", t => t.TicketStatusId)
                .Index(t => t.ApprovalLevelId)
                .Index(t => t.ApprovalTicketId)
                .Index(t => t.TicketStatusId);
            
            AlterColumn("dbo.ApplicationNotification", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.ApplicationNotification", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailNotification", "TicketStatusId", "dbo.ApprovalStatus");
            DropForeignKey("dbo.EmailNotification", "ApprovalTicketId", "dbo.ApprovalTicket");
            DropForeignKey("dbo.EmailNotification", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropIndex("dbo.EmailNotification", new[] { "TicketStatusId" });
            DropIndex("dbo.EmailNotification", new[] { "ApprovalTicketId" });
            DropIndex("dbo.EmailNotification", new[] { "ApprovalLevelId" });
            DropPrimaryKey("dbo.ApplicationNotification");
            AlterColumn("dbo.ApplicationNotification", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.EmailNotification");
            AddPrimaryKey("dbo.ApplicationNotification", "Id");
            RenameTable(name: "dbo.ApplicationNotification", newName: "ApplicationNotifiation");
        }
    }
}
