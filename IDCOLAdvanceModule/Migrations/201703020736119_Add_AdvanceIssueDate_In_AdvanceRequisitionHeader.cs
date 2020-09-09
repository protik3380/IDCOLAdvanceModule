namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AdvanceIssueDate_In_AdvanceRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "AdvanceIssueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "AdvanceIssueDate");
        }
    }
}
