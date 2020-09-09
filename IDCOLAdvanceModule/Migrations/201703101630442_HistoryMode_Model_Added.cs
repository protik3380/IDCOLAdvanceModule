namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryMode_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryMode",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistoryMode");
        }
    }
}
