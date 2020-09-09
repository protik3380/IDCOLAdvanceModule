namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_AdvanceCategory_Table_Data : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO BaseAdvanceCategory (Name) VALUES ('Corporate Advisory');
                UPDATE AdvanceCategory SET DisplaySerial = 1 WHERE Id = 1;
                UPDATE AdvanceCategory SET DisplaySerial = 2 WHERE Id = 2;
                UPDATE AdvanceCategory SET DisplaySerial = 1 WHERE Id = 3;
                UPDATE AdvanceCategory SET DisplaySerial = 1 WHERE Id = 4;
                UPDATE AdvanceCategory SET DisplaySerial = 2 WHERE Id = 5;
                UPDATE AdvanceCategory SET DisplaySerial = 3 WHERE Id = 6;
                UPDATE AdvanceCategory SET Name = 'Others', DisplaySerial = 5 WHERE Id = 7;
                UPDATE AdvanceCategory SET DisplaySerial = 1, BaseAdvanceCategoryId = 4 WHERE Id = 8;
                INSERT INTO AdvanceCategory (Name, DisplaySerial, IsCeilingApplicable, BaseAdvanceCategoryId) VALUES ('Procurement', 4, 0, 3);"
                );
        }
        
        public override void Down()
        {
        }
    }
}
