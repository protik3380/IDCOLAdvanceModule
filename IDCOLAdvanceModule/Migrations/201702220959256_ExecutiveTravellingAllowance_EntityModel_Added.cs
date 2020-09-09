namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExecutiveTravellingAllowance_EntityModel_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExecutiveTravellingAllowance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationGroupId = c.Int(nullable: false),
                        EmployeeCategoryId = c.Int(nullable: false),
                        CostItemId = c.Int(nullable: false),
                        Currency = c.String(),
                        EntitlementAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostItem_Id = c.Long(),
                        EmployeeCategory_Id = c.Long(),
                        LocationGroup_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostItem", t => t.CostItem_Id)
                .ForeignKey("dbo.EmployeeCategory", t => t.EmployeeCategory_Id)
                .ForeignKey("dbo.LocationGroup", t => t.LocationGroup_Id)
                .Index(t => t.CostItem_Id)
                .Index(t => t.EmployeeCategory_Id)
                .Index(t => t.LocationGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExecutiveTravellingAllowance", "LocationGroup_Id", "dbo.LocationGroup");
            DropForeignKey("dbo.ExecutiveTravellingAllowance", "EmployeeCategory_Id", "dbo.EmployeeCategory");
            DropForeignKey("dbo.ExecutiveTravellingAllowance", "CostItem_Id", "dbo.CostItem");
            DropIndex("dbo.ExecutiveTravellingAllowance", new[] { "LocationGroup_Id" });
            DropIndex("dbo.ExecutiveTravellingAllowance", new[] { "EmployeeCategory_Id" });
            DropIndex("dbo.ExecutiveTravellingAllowance", new[] { "CostItem_Id" });
            DropTable("dbo.ExecutiveTravellingAllowance");
        }
    }
}
