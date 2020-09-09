namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_CorporateAdvisoryPlaceofEvent_In_AdvanceCorporateAdvisoryRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "CorporateAdvisoryPlaceOfEvent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "CorporateAdvisoryPlaceOfEvent");
        }
    }
}
