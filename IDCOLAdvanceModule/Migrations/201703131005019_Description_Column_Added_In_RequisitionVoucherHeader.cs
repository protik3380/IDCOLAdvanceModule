namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description_Column_Added_In_RequisitionVoucherHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionVoucherHeader", "VoucherDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionVoucherHeader", "VoucherDescription");
        }
    }
}
