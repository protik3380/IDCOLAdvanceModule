namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VatTaxTypeId_Added_ExpenseDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseDetail", "VatTaxTypeId", c => c.Long());
            CreateIndex("dbo.AdvanceExpenseDetail", "VatTaxTypeId");
            AddForeignKey("dbo.AdvanceExpenseDetail", "VatTaxTypeId", "dbo.VatTaxType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdvanceExpenseDetail", "VatTaxTypeId", "dbo.VatTaxType");
            DropIndex("dbo.AdvanceExpenseDetail", new[] { "VatTaxTypeId" });
            DropColumn("dbo.AdvanceExpenseDetail", "VatTaxTypeId");
        }
    }
}
