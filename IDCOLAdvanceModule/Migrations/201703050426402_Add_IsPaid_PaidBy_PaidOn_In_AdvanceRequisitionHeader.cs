namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsPaid_PaidBy_PaidOn_In_AdvanceRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.AdvanceRequisitionHeader", "PaidBy", c => c.String());
            AddColumn("dbo.AdvanceRequisitionHeader", "PaidOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "PaidOn");
            DropColumn("dbo.AdvanceRequisitionHeader", "PaidBy");
            DropColumn("dbo.AdvanceRequisitionHeader", "IsPaid");
        }
    }
}
