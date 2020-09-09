namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SQLVIEW_Advance_VW_GetRequisitionSignatory : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Advance_VW_GetRequisitionSignatory
                AS
                SELECT NEWID() AS Id,
                header.Id RequisitionId,
                level.Name LevelName,
                u.FirstName + ' ' + u.MiddleName + ' ' + u.LastName AS SignatoryName,
                rank.RankName AS SignatoryDesignation
                FROM ApprovalTracker tracker
                LEFT JOIN ApprovalTicket ticket
                ON tracker.ApprovalTicketId = ticket.Id
                LEFT JOIN ApprovalLevel level
                ON level.Id = tracker.ApprovalLevelId
                JOIN AdvanceRequisitionHeader header
                ON header.Id = ticket.AdvanceRequisitionHeaderId
                LEFT JOIN IDCOLMIS.dbo.UserTable u
                ON u.UserName = tracker.AuthorizedBy
                LEFT JOIN IDCOLMIS.dbo.Admin_Rank rank
                ON u.RankID = rank.RankID
                WHERE tracker.ApprovalStatusId = 5"
                );
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetRequisitionSignatory')
                    DROP VIEW Advance_VW_GetRequisitionSignatory");
        }
    }
}
