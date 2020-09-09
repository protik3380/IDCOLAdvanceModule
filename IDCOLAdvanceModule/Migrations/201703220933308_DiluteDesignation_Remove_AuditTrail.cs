namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiluteDesignation_Remove_AuditTrail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DiluteDesignation", "CreatedBy");
            DropColumn("dbo.DiluteDesignation", "CreatedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DiluteDesignation", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.DiluteDesignation", "CreatedBy", c => c.String());
        }
    }
}
