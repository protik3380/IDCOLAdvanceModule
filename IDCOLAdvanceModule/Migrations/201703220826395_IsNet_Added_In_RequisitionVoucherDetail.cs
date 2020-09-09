namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsNet_Added_In_RequisitionVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionVoucherDetail", "IsNet", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionVoucherDetail", "IsNet");
        }
    }
}
