namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VatTaxType_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VatTaxType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VatTaxType");
        }
    }
}
