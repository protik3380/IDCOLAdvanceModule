namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionNo_Added_In_AdvanceRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "RequisitionNo", c => c.String());
            AddColumn("dbo.AdvanceRequisitionHeader", "SerialNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "SerialNo");
            DropColumn("dbo.AdvanceRequisitionHeader", "RequisitionNo");
        }
    }
}
