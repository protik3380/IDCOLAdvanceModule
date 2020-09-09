namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Currency_ConversionRate_In_OverseasTravelExpenseDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseDetail", "Currency", c => c.String());
            AddColumn("dbo.AdvanceExpenseDetail", "ConversionRate", c => c.Double());
            DropColumn("dbo.AdvanceRequisitionHeader", "OverseasSponsorFinancedHeaderAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "OverseasSponsorFinancedHeaderAmount", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.AdvanceExpenseDetail", "ConversionRate");
            DropColumn("dbo.AdvanceExpenseDetail", "Currency");
        }
    }
}
