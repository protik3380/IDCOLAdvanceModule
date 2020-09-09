namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VatTaxInformation_Added_RequisitionVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionVoucherDetail", "VendorId", c => c.Int());
            AddColumn("dbo.RequisitionVoucherDetail", "VatTaxCategoryId", c => c.Int());
            AddColumn("dbo.RequisitionVoucherDetail", "Percentage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionVoucherDetail", "Percentage");
            DropColumn("dbo.RequisitionVoucherDetail", "VatTaxCategoryId");
            DropColumn("dbo.RequisitionVoucherDetail", "VendorId");
        }
    }
}
