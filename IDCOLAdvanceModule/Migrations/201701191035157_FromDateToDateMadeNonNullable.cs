namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FromDateToDateMadeNonNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdvanceRequisitionHeader", "FromDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AdvanceRequisitionHeader", "ToDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdvanceRequisitionHeader", "ToDate", c => c.DateTime());
            AlterColumn("dbo.AdvanceRequisitionHeader", "FromDate", c => c.DateTime());
        }
    }
}
