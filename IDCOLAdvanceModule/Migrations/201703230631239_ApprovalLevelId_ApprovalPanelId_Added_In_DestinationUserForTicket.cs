namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApprovalLevelId_ApprovalPanelId_Added_In_DestinationUserForTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DestinationUserForTicket", "ApprovalPanelId", c => c.Long(nullable: false));
            AddColumn("dbo.DestinationUserForTicket", "ApprovalLevelId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DestinationUserForTicket", "ApprovalLevelId");
            DropColumn("dbo.DestinationUserForTicket", "ApprovalPanelId");
        }
    }
}
