namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsVatTaxAccount_Added_RequisitionVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionVoucherDetail", "IsVatTaxAccount", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionVoucherDetail", "IsVatTaxAccount");
        }
    }
}
