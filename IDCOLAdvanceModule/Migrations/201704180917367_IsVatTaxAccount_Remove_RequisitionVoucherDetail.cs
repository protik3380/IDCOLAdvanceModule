namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsVatTaxAccount_Remove_RequisitionVoucherDetail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RequisitionVoucherDetail", "IsVatTaxAccount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequisitionVoucherDetail", "IsVatTaxAccount", c => c.Boolean(nullable: false));
        }
    }
}
