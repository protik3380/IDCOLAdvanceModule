namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Justification_Column_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseDetail", "Justification", c => c.String());
            AddColumn("dbo.AdvanceRequisitionDetail", "Justification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionDetail", "Justification");
            DropColumn("dbo.AdvanceExpenseDetail", "Justification");
        }
    }
}
