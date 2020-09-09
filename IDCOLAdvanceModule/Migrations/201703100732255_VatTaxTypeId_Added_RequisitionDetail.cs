namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VatTaxTypeId_Added_RequisitionDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceRequisitionDetail", "VatTaxTypeId", c => c.Long());
            CreateIndex("dbo.AdvanceRequisitionDetail", "VatTaxTypeId");
            AddForeignKey("dbo.AdvanceRequisitionDetail", "VatTaxTypeId", "dbo.VatTaxType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdvanceRequisitionDetail", "VatTaxTypeId", "dbo.VatTaxType");
            DropIndex("dbo.AdvanceRequisitionDetail", new[] { "VatTaxTypeId" });
            DropColumn("dbo.AdvanceRequisitionDetail", "VatTaxTypeId");
        }
    }
}
