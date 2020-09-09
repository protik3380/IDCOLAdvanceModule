namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionFile_ExpenseFile_ModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileLocation = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        UploadedBy = c.String(),
                        UploadedOn = c.DateTime(nullable: false),
                        DeletedBy = c.String(),
                        DeletedOn = c.DateTime(),
                        AdvanceExpenseHeaderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceExpenseHeader", t => t.AdvanceExpenseHeaderId)
                .Index(t => t.AdvanceExpenseHeaderId);
            
            CreateTable(
                "dbo.RequisitionFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileLocation = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        UploadedBy = c.String(),
                        UploadedOn = c.DateTime(nullable: false),
                        DeletedBy = c.String(),
                        DeletedOn = c.DateTime(),
                        AdvanceRequisitionHeaderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionHeader", t => t.AdvanceRequisitionHeaderId)
                .Index(t => t.AdvanceRequisitionHeaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionFile", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropForeignKey("dbo.ExpenseFile", "AdvanceExpenseHeaderId", "dbo.AdvanceExpenseHeader");
            DropIndex("dbo.RequisitionFile", new[] { "AdvanceRequisitionHeaderId" });
            DropIndex("dbo.ExpenseFile", new[] { "AdvanceExpenseHeaderId" });
            DropTable("dbo.RequisitionFile");
            DropTable("dbo.ExpenseFile");
        }
    }
}
