namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SourceOfFundHistory_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SourceOfFundHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SourceOfFundId = c.Long(nullable: false),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        LastModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SourceOfFund", t => t.SourceOfFundId)
                .Index(t => t.SourceOfFundId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SourceOfFundHistory", "SourceOfFundId", "dbo.SourceOfFund");
            DropIndex("dbo.SourceOfFundHistory", new[] { "SourceOfFundId" });
            DropTable("dbo.SourceOfFundHistory");
        }
    }
}
