namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RejectedOn_RejectedBy_Added_in_AdvanceRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "RejectedOn", c => c.DateTime());
            AddColumn("dbo.AdvanceRequisitionHeader", "RejectedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "RejectedBy");
            DropColumn("dbo.AdvanceRequisitionHeader", "RejectedOn");
        }
    }
}
