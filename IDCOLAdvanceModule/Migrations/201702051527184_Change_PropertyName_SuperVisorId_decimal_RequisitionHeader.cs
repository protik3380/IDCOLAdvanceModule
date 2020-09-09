namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_PropertyName_SuperVisorId_decimal_RequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "RequesterSupervisorId", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.AdvanceRequisitionHeader", "RequistionSupervisorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "RequistionSupervisorId", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.AdvanceRequisitionHeader", "RequesterSupervisorId");
        }
    }
}
