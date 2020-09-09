namespace IDCOLAdvanceModule.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addIsDiluteApplicableInApprovalLevelModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevel", "IsDiluteApplicable", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.ApprovalLevel", "IsDiluteApplicable");
        }
    }
}
