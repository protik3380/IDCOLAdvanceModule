namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryName_Added_In_AdvanceOverseasTravelRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "CountryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "CountryName");
        }
    }
}
