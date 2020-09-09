namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_PaidOn_From_AdvanceRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "PaidOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "PaidOn", c => c.DateTime());
        }
    }
}
