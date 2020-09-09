namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vendor_Renamed_ThirdPartyReceipient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionDetail", "ReceipientOrPayeeName", c => c.String());
            AddColumn("dbo.AdvanceRequisitionDetail", "IsThirdPartyReceipient", c => c.Boolean(nullable: false));
            DropColumn("dbo.AdvanceRequisitionDetail", "Vendor");
            DropColumn("dbo.AdvanceRequisitionDetail", "IsVendorReceipient");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionDetail", "IsVendorReceipient", c => c.Boolean(nullable: false));
            AddColumn("dbo.AdvanceRequisitionDetail", "Vendor", c => c.String());
            DropColumn("dbo.AdvanceRequisitionDetail", "IsThirdPartyReceipient");
            DropColumn("dbo.AdvanceRequisitionDetail", "ReceipientOrPayeeName");
        }
    }
}
