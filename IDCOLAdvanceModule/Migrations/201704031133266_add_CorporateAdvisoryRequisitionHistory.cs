namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_CorporateAdvisoryRequisitionHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionHistoryHeader", "CorporateAdvisoryPlaceOfEvent", c => c.String());
            AddColumn("dbo.RequisitionHistoryHeader", "NoOfUnit", c => c.Double());
            AddColumn("dbo.RequisitionHistoryHeader", "UnitCost", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.RequisitionHistoryHeader", "TotalRevenue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.RequisitionHistoryHeader", "AdvanceCorporateRemarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionHistoryHeader", "AdvanceCorporateRemarks");
            DropColumn("dbo.RequisitionHistoryHeader", "TotalRevenue");
            DropColumn("dbo.RequisitionHistoryHeader", "UnitCost");
            DropColumn("dbo.RequisitionHistoryHeader", "NoOfUnit");
            DropColumn("dbo.RequisitionHistoryHeader", "CorporateAdvisoryPlaceOfEvent");
        }
    }
}
