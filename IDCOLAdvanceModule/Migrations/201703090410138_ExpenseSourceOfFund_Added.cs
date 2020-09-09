namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseSourceOfFund_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseSourceOfFund",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SourceOfFundId = c.Long(nullable: false),
                        Percentage = c.Double(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        LastModifiedOn = c.DateTime(),
                        AdvanceExpenseHeaderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceExpenseHeader", t => t.AdvanceExpenseHeaderId)
                .ForeignKey("dbo.SourceOfFund", t => t.SourceOfFundId)
                .Index(t => t.SourceOfFundId)
                .Index(t => t.AdvanceExpenseHeaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseSourceOfFund", "SourceOfFundId", "dbo.SourceOfFund");
            DropForeignKey("dbo.ExpenseSourceOfFund", "AdvanceExpenseHeaderId", "dbo.AdvanceExpenseHeader");
            DropIndex("dbo.ExpenseSourceOfFund", new[] { "AdvanceExpenseHeaderId" });
            DropIndex("dbo.ExpenseSourceOfFund", new[] { "SourceOfFundId" });
            DropTable("dbo.ExpenseSourceOfFund");
        }
    }
}
