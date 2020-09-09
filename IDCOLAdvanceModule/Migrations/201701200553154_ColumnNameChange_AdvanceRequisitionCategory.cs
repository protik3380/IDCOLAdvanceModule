namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNameChange_AdvanceRequisitionCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategory_Id", "dbo.BaseAdvanceRequisitionCategory");
            DropIndex("dbo.AdvanceRequisitionCategory", new[] { "BaseAdvanceRequisitionCategory_Id" });
            RenameColumn(table: "dbo.AdvanceRequisitionCategory", name: "BaseAdvanceRequisitionCategory_Id", newName: "BaseAdvanceRequisitionCategoryId");
            AlterColumn("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId");
            AddForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", "dbo.BaseAdvanceRequisitionCategory", "Id", cascadeDelete: true);
            DropColumn("dbo.AdvanceRequisitionCategory", "BaseAdvanceCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionCategory", "BaseAdvanceCategoryId", c => c.Long(nullable: false));
            DropForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", "dbo.BaseAdvanceRequisitionCategory");
            DropIndex("dbo.AdvanceRequisitionCategory", new[] { "BaseAdvanceRequisitionCategoryId" });
            AlterColumn("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", c => c.Long());
            RenameColumn(table: "dbo.AdvanceRequisitionCategory", name: "BaseAdvanceRequisitionCategoryId", newName: "BaseAdvanceRequisitionCategory_Id");
            CreateIndex("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategory_Id");
            AddForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategory_Id", "dbo.BaseAdvanceRequisitionCategory", "Id");
        }
    }
}
