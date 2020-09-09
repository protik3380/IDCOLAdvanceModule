namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryName_Added_In_AdvanceOverseasTravelExpenseHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "CountryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "CountryName");
        }
    }
}
