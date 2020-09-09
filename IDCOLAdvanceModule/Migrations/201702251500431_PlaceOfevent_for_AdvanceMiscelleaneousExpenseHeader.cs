namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceOfevent_for_AdvanceMiscelleaneousExpenseHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "PlaceOfEvent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "PlaceOfEvent");
        }
    }
}
