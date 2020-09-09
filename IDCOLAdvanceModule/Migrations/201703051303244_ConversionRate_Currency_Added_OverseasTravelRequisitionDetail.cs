namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConversionRate_Currency_Added_OverseasTravelRequisitionDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionDetail", "Currency", c => c.String());
            AddColumn("dbo.AdvanceRequisitionDetail", "ConversionRate", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionDetail", "ConversionRate");
            DropColumn("dbo.AdvanceRequisitionDetail", "Currency");
        }
    }
}
