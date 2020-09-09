namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeneralAccountConfiguration_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeneralAccountConfiguration",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountTypeId = c.Long(nullable: false),
                        AccountCode = c.String(),
                        IsDefaultAccount = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountType", t => t.AccountTypeId)
                .Index(t => t.AccountTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeneralAccountConfiguration", "AccountTypeId", "dbo.AccountType");
            DropIndex("dbo.GeneralAccountConfiguration", new[] { "AccountTypeId" });
            DropTable("dbo.GeneralAccountConfiguration");
        }
    }
}
