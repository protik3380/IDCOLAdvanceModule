namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MiscellaneousExpenseHistory_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseHistoryHeader", "PlaceOfEvent", c => c.String());
            AddColumn("dbo.ExpenseHistoryHeader", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ExpenseHistoryDetail", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseHistoryDetail", "Discriminator");
            DropColumn("dbo.ExpenseHistoryHeader", "Discriminator");
            DropColumn("dbo.ExpenseHistoryHeader", "PlaceOfEvent");
        }
    }
}
