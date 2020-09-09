namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_HistoryOperationMode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryOperationMode",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistoryOperationMode");
        }
    }
}
