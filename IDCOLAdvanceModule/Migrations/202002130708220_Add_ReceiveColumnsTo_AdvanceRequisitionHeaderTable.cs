namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ReceiveColumnsTo_AdvanceRequisitionHeaderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "IsReceived", c => c.Boolean(nullable: false));
            AddColumn("dbo.AdvanceRequisitionHeader", "ReceivedBy", c => c.String());
            AddColumn("dbo.AdvanceRequisitionHeader", "ReceivedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "ReceivedOn");
            DropColumn("dbo.AdvanceRequisitionHeader", "ReceivedBy");
            DropColumn("dbo.AdvanceRequisitionHeader", "IsReceived");
        }
    }
}
