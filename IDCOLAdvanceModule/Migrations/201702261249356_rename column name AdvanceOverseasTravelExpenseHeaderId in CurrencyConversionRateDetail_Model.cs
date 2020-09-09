namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamecolumnnameAdvanceOverseasTravelExpenseHeaderIdinCurrencyConversionRateDetail_Model : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CurrencyConversionRateDetail", new[] { "AdvanceOverseasTravelExpenseHeader_Id" });
            RenameColumn(table: "dbo.CurrencyConversionRateDetail", name: "AdvanceOverseasTravelExpenseHeader_Id", newName: "AdvanceOverseasTravelExpenseHeaderId");
            AlterColumn("dbo.CurrencyConversionRateDetail", "AdvanceOverseasTravelExpenseHeaderId", c => c.Long(nullable: false));
            CreateIndex("dbo.CurrencyConversionRateDetail", "AdvanceOverseasTravelExpenseHeaderId");
            DropColumn("dbo.CurrencyConversionRateDetail", "OverseasExpenseHeaderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CurrencyConversionRateDetail", "OverseasExpenseHeaderId", c => c.Long(nullable: false));
            DropIndex("dbo.CurrencyConversionRateDetail", new[] { "AdvanceOverseasTravelExpenseHeaderId" });
            AlterColumn("dbo.CurrencyConversionRateDetail", "AdvanceOverseasTravelExpenseHeaderId", c => c.Long());
            RenameColumn(table: "dbo.CurrencyConversionRateDetail", name: "AdvanceOverseasTravelExpenseHeaderId", newName: "AdvanceOverseasTravelExpenseHeader_Id");
            CreateIndex("dbo.CurrencyConversionRateDetail", "AdvanceOverseasTravelExpenseHeader_Id");
        }
    }
}
