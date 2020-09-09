namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReadDate_Added_In_ApplicationNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationNotification", "ReadDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationNotification", "ReadDate");
        }
    }
}
