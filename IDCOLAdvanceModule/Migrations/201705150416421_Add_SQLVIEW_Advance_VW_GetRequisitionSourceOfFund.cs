namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SQLVIEW_Advance_VW_GetRequisitionSourceOfFund : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Advance_VW_GetRequisitionSourceOfFund
                AS
                SELECT NEWID() AS Id,
                header.Id RequisitionHeaderId,
                source.Id SourceOfFundId,
                source.Name SourceOfFundName,
                requisitionSource.Percentage
                FROM RequisitionSourceOfFund requisitionSource 
                LEFT JOIN AdvanceRequisitionHeader header
                ON header.Id = requisitionSource.AdvanceRequisitionHeaderId
                LEFT JOIN SourceOfFund source 
                ON requisitionSource.SourceOfFundId = source.Id"
                );
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetRequisitionSourceOfFund')
                    DROP VIEW Advance_VW_GetRequisitionSourceOfFund");
        }
    }
}
