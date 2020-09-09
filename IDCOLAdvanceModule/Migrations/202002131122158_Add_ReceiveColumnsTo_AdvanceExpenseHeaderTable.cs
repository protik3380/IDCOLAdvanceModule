namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ReceiveColumnsTo_AdvanceExpenseHeaderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "IsReceived", c => c.Boolean(nullable: false));
            AddColumn("dbo.AdvanceExpenseHeader", "ReceivedBy", c => c.String());
            AddColumn("dbo.AdvanceExpenseHeader", "ReceivedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "ReceivedOn");
            DropColumn("dbo.AdvanceExpenseHeader", "ReceivedBy");
            DropColumn("dbo.AdvanceExpenseHeader", "IsReceived");
        }
    }
}
