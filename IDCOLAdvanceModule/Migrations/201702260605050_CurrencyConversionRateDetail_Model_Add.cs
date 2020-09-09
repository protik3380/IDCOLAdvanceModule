namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CurrencyConversionRateDetail_Model_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrencyConversionRateDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FromCurrencyId = c.Long(nullable: false),
                        ToCurrencyId = c.Long(nullable: false),
                        ConversionRate = c.Double(nullable: false),
                        OverseasExpenseHeaderId = c.Long(nullable: false),
                        AdvanceOverseasTravelExpenseHeader_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceExpenseHeader", t => t.AdvanceOverseasTravelExpenseHeader_Id)
                .Index(t => t.AdvanceOverseasTravelExpenseHeader_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrencyConversionRateDetail", "AdvanceOverseasTravelExpenseHeader_Id", "dbo.AdvanceExpenseHeader");
            DropIndex("dbo.CurrencyConversionRateDetail", new[] { "AdvanceOverseasTravelExpenseHeader_Id" });
           DropTable("dbo.CurrencyConversionRateDetail");
        }
    }
}
