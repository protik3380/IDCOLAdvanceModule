namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceExpenseId_Modified_AdvanceExpenseHeader : DbMigration
    {
        public override void Up()
        {

            DropTable("dbo.AdvanceExpenseStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdvanceExpenseStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
