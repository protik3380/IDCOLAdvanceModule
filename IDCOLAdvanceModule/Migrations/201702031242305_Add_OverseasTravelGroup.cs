namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_OverseasTravelGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OverseasTravelGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TravelGroupName = c.String(),
                        LocationGroupId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocationGroup", t => t.LocationGroupId)
                .Index(t => t.LocationGroupId);
            
            CreateTable(
                "dbo.OverseasTravelGroupMappingSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RankId = c.Long(nullable: false),
                        OverseasTravelGroupId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OverseasTravelGroup", t => t.OverseasTravelGroupId)
                .Index(t => t.OverseasTravelGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OverseasTravelGroupMappingSetting", "OverseasTravelGroupId", "dbo.OverseasTravelGroup");
            DropForeignKey("dbo.OverseasTravelGroup", "LocationGroupId", "dbo.LocationGroup");
            DropIndex("dbo.OverseasTravelGroupMappingSetting", new[] { "OverseasTravelGroupId" });
            DropIndex("dbo.OverseasTravelGroup", new[] { "LocationGroupId" });
            DropTable("dbo.OverseasTravelGroupMappingSetting");
            DropTable("dbo.OverseasTravelGroup");
        }
    }
}
