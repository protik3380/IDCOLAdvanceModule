namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Property_SuperVisorId_decimal_RequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdvanceRequisitionHeader", "RequistionSupervisorId", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdvanceRequisitionHeader", "RequistionSupervisorId", c => c.Long());
        }
    }
}
