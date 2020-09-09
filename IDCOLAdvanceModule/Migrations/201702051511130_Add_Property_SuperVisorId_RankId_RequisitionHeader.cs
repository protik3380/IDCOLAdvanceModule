namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Property_SuperVisorId_RankId_RequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "RequistionSupervisorId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceRequisitionHeader", "RequistionSupervisorId");
        }
    }
}
