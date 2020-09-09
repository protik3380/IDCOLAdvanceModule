namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VatTaxInformation_Added_ExpenseVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseVoucherDetail", "VendorId", c => c.Int());
            AddColumn("dbo.ExpenseVoucherDetail", "VatTaxCategoryId", c => c.Int());
            AddColumn("dbo.ExpenseVoucherDetail", "Percentage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseVoucherDetail", "Percentage");
            DropColumn("dbo.ExpenseVoucherDetail", "VatTaxCategoryId");
            DropColumn("dbo.ExpenseVoucherDetail", "VendorId");
        }
    }
}
