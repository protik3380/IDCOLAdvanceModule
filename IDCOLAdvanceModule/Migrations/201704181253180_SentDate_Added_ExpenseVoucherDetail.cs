namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SentDate_Added_ExpenseVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseVoucherHeader", "SentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseVoucherHeader", "SentDate");
        }
    }
}
