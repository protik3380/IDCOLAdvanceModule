namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SentDate_Added_RequisitionVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionVoucherHeader", "SentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionVoucherHeader", "SentDate");
        }
    }
}
