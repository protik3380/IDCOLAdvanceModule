namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FromCurrency_And_ToCurrency_Made_String : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrencyConversionRateDetail", "FromCurrency", c => c.String());
            AddColumn("dbo.CurrencyConversionRateDetail", "ToCurrency", c => c.String());
            DropColumn("dbo.CurrencyConversionRateDetail", "FromCurrencyId");
            DropColumn("dbo.CurrencyConversionRateDetail", "ToCurrencyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CurrencyConversionRateDetail", "ToCurrencyId", c => c.Long(nullable: false));
            AddColumn("dbo.CurrencyConversionRateDetail", "FromCurrencyId", c => c.Long(nullable: false));
            DropColumn("dbo.CurrencyConversionRateDetail", "ToCurrency");
            DropColumn("dbo.CurrencyConversionRateDetail", "FromCurrency");
        }
    }
}
