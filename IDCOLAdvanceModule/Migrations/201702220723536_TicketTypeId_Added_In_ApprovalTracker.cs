namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketTypeId_Added_In_ApprovalTracker : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DesinationUserForTicket", newName: "DestinationUserForTicket");
            AddColumn("dbo.ApprovalTracker", "TicketTypeId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalTracker", "TicketTypeId");
            RenameTable(name: "dbo.DestinationUserForTicket", newName: "DesinationUserForTicket");
        }
    }
}
