namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description_Column_Added_In_RequisitionVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionVoucherDetail", "Description", c => c.String());
            AlterColumn("dbo.RequisitionVoucherDetail", "DrAmount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RequisitionVoucherDetail", "CrAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RequisitionVoucherDetail", "CrAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RequisitionVoucherDetail", "DrAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.RequisitionVoucherDetail", "Description");
        }
    }
}
