namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountType_AccountConfiguration_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountConfiguration",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceCategoryId = c.Long(nullable: false),
                        AccountTypeId = c.Long(nullable: false),
                        AccountCode = c.String(),
                        IsDefaultAccount = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountType", t => t.AccountTypeId)
                .ForeignKey("dbo.AdvanceCategory", t => t.AdvanceCategoryId)
                .Index(t => t.AdvanceCategoryId)
                .Index(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.AccountType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountConfiguration", "AdvanceCategoryId", "dbo.AdvanceCategory");
            DropForeignKey("dbo.AccountConfiguration", "AccountTypeId", "dbo.AccountType");
            DropIndex("dbo.AccountConfiguration", new[] { "AccountTypeId" });
            DropIndex("dbo.AccountConfiguration", new[] { "AdvanceCategoryId" });
            DropTable("dbo.AccountType");
            DropTable("dbo.AccountConfiguration");
        }
    }
}
