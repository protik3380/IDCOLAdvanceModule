namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LocationWiseSettingsModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlaceOfVisit",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LocationGroupId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocationGroup", t => t.LocationGroupId)
                .Index(t => t.LocationGroupId);
            
            CreateTable(
                "dbo.LocationGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaceOfVisit", "LocationGroupId", "dbo.LocationGroup");
            DropIndex("dbo.PlaceOfVisit", new[] { "LocationGroupId" });
            DropTable("dbo.LocationGroup");
            DropTable("dbo.PlaceOfVisit");
        }
    }
}
