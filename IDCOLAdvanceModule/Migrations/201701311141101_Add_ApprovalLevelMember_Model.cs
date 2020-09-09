namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ApprovalLevelMember_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApprovalLevelMember",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApprovalLevelId = c.Long(nullable: false),
                        EmployeeUserName = c.Long(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        LastModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApprovalLevel", t => t.ApprovalLevelId)
                .Index(t => t.ApprovalLevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApprovalLevelMember", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropIndex("dbo.ApprovalLevelMember", new[] { "ApprovalLevelId" });
            DropTable("dbo.ApprovalLevelMember");
        }
    }
}
