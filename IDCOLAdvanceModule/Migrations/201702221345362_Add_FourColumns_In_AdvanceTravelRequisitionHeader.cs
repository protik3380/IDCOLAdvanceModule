namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FourColumns_In_AdvanceTravelRequisitionHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseHeader", "PlaceOfVisit", c => c.String());
            AddColumn("dbo.AdvanceExpenseHeader", "SourceOfFund", c => c.String());
            AddColumn("dbo.AdvanceExpenseHeader", "IsSponsorFinanced", c => c.Boolean());
            AddColumn("dbo.AdvanceExpenseHeader", "SponsorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvanceExpenseHeader", "SponsorName");
            DropColumn("dbo.AdvanceExpenseHeader", "IsSponsorFinanced");
            DropColumn("dbo.AdvanceExpenseHeader", "SourceOfFund");
            DropColumn("dbo.AdvanceExpenseHeader", "PlaceOfVisit");
        }
    }
}
