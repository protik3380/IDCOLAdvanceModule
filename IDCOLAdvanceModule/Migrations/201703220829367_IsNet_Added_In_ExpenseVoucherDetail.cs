namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsNet_Added_In_ExpenseVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseVoucherDetail", "IsNet", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseVoucherDetail", "IsNet");
        }
    }
}
