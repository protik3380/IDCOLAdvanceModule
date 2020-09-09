namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceCorporateAdvisoryRequisition_Model_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "NoOfUnit", c => c.Double());
            AddColumn("dbo.AdvanceRequisitionHeader", "UnitCost", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AdvanceRequisitionHeader", "AdvanceCorporateRemarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "AdvanceCorporateRemarks");
            DropColumn("dbo.AdvanceRequisitionHeader", "UnitCost");
            DropColumn("dbo.AdvanceRequisitionHeader", "NoOfUnit");
        }
    }
}
