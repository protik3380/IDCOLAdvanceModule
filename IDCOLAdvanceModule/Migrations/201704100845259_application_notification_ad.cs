namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class application_notification_ad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationNotifiation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationTypeId = c.Long(nullable: false),
                        Message = c.String(),
                        ToUserName = c.String(),
                        NotificationDate = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        TicketStatusId = c.Long(nullable: false),
                        ExpenseHeaderId = c.Long(),
                        RequisitionId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationType", t => t.NotificationTypeId)
                .ForeignKey("dbo.ApprovalStatus", t => t.TicketStatusId)
                .ForeignKey("dbo.AdvanceExpenseHeader", t => t.ExpenseHeaderId)
                .ForeignKey("dbo.AdvanceRequisitionHeader", t => t.RequisitionId)
                .Index(t => t.NotificationTypeId)
                .Index(t => t.TicketStatusId)
                .Index(t => t.ExpenseHeaderId)
                .Index(t => t.RequisitionId);
            
            CreateTable(
                "dbo.NotificationType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationNotifiation", "RequisitionId", "dbo.AdvanceRequisitionHeader");
            DropForeignKey("dbo.ApplicationNotifiation", "ExpenseHeaderId", "dbo.AdvanceExpenseHeader");
            DropForeignKey("dbo.ApplicationNotifiation", "TicketStatusId", "dbo.ApprovalStatus");
            DropForeignKey("dbo.ApplicationNotifiation", "NotificationTypeId", "dbo.NotificationType");
            DropIndex("dbo.ApplicationNotifiation", new[] { "RequisitionId" });
            DropIndex("dbo.ApplicationNotifiation", new[] { "ExpenseHeaderId" });
            DropIndex("dbo.ApplicationNotifiation", new[] { "TicketStatusId" });
            DropIndex("dbo.ApplicationNotifiation", new[] { "NotificationTypeId" });
            DropTable("dbo.NotificationType");
            DropTable("dbo.ApplicationNotifiation");
        }
    }
}
