namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApprovatTicketId_And_ApprovalLevelId_In_ApplicationNotifiation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationNotifiation", "ApprovalTicketId", c => c.Long(nullable: false));
            AddColumn("dbo.ApplicationNotifiation", "ApprovalLevelId", c => c.Long(nullable: false));
            CreateIndex("dbo.ApplicationNotifiation", "ApprovalTicketId");
            CreateIndex("dbo.ApplicationNotifiation", "ApprovalLevelId");
            AddForeignKey("dbo.ApplicationNotifiation", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.ApplicationNotifiation", "ApprovalTicketId", "dbo.ApprovalTicket", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationNotifiation", "ApprovalTicketId", "dbo.ApprovalTicket");
            DropForeignKey("dbo.ApplicationNotifiation", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropIndex("dbo.ApplicationNotifiation", new[] { "ApprovalLevelId" });
            DropIndex("dbo.ApplicationNotifiation", new[] { "ApprovalTicketId" });
            DropColumn("dbo.ApplicationNotifiation", "ApprovalLevelId");
            DropColumn("dbo.ApplicationNotifiation", "ApprovalTicketId");
        }
    }
}
