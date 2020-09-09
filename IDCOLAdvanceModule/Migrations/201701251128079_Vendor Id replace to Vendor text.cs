namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VendorIdreplacetoVendortext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionDetail", "Vendor", c => c.String());
            DropColumn("dbo.AdvanceRequisitionDetail", "VendorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionDetail", "VendorId", c => c.Long());
            DropColumn("dbo.AdvanceRequisitionDetail", "Vendor");
        }
    }
}
