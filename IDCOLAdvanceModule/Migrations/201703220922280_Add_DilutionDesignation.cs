namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DilutionDesignation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiluteDesignation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DesignationId = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DiluteDesignation");
        }
    }
}
