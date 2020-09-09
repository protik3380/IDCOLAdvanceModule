namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionSourceOfFund_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequisitionSourceOfFund",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SourceOfFundId = c.Long(nullable: false),
                        Percentage = c.Double(nullable: false),
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
            DropForeignKey("dbo.RequisitionSourceOfFund", "SourceOfFundId", "dbo.SourceOfFund");
            DropIndex("dbo.RequisitionSourceOfFund", new[] { "SourceOfFundId" });
            DropTable("dbo.RequisitionSourceOfFund");
        }
    }
}
