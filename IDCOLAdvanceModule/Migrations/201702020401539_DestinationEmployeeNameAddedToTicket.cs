namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DestinationEmployeeNameAddedToTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionApprovalTicket", "DestinationUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionApprovalTicket", "DestinationUserName");
        }
    }
}
