namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RejectedOn_RejectedBy_Added_in_AdvanceExpenseHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "RejectedOn", c => c.DateTime());
            AddColumn("dbo.AdvanceExpenseHeader", "RejectedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "RejectedBy");
            DropColumn("dbo.AdvanceExpenseHeader", "RejectedOn");
        }
    }
}
