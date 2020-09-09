namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SponserAmount_Added_In_Header_And_Detail_For_Local_And_OverseasTravel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "OverseasSponsorFinancedHeaderAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AdvanceRequisitionHeader", "TravelSponsorFinancedHeaderAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AdvanceRequisitionDetail", "OverseasSponsorFinancedDetailAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AdvanceRequisitionDetail", "TravelSponsorFinancedDetailAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionDetail", "TravelSponsorFinancedDetailAmount");
            DropColumn("dbo.AdvanceRequisitionDetail", "OverseasSponsorFinancedDetailAmount");
            DropColumn("dbo.AdvanceRequisitionHeader", "TravelSponsorFinancedHeaderAmount");
            DropColumn("dbo.AdvanceRequisitionHeader", "OverseasSponsorFinancedHeaderAmount");
        }
    }
}
