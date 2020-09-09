namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_ApprovalStatuses_Reverted : DbMigration
    {
        public override void Up()
        {
            Sql(@"UPDATE [dbo].[ApprovalStatus] SET Name = 'Reverted' WHERE Id = 4");
        }
        
        public override void Down()
        {
            Sql(@"UPDATE [dbo].[ApprovalStatus] SET Name = 'Revert' WHERE Id = 4");
        }
    }
}
