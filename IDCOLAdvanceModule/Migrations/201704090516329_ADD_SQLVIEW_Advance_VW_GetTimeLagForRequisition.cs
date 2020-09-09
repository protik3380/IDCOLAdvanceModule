namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_SQLVIEW_Advance_VW_GetTimeLagForRequisition : DbMigration
    {
        public override void Up()
        {
            Sql(@"USE [IDCOLMIS]
                GO

                SET ANSI_NULLS ON
                GO

                SET QUOTED_IDENTIFIER ON
                GO

                CREATE FUNCTION [dbo].[GetWorkingDaysCount] (@DateFrom date, @DateTo date)
                returns INT
                AS
                BEGIN
                DECLARE @DayCount INT = DATEDIFF(day, @DateFrom, @DateTo);
                DECLARE @PublicHolidayCount INT = [dbo].[GetPublicHolidayCount](@DateFrom, @DateTo);
                DECLARE @WeeklyHolidayCount INT = [dbo].[GetWeeklyHolidaysCount](@DateFrom, @DateTo);
                DECLARE @WorkingDayCount INT = @DayCount - (@PublicHolidayCount + @WeeklyHolidayCount);
                return @WorkingDayCount;
                END
                GO



                USE [IDCOLAdvanceDB]
                GO

                SET ANSI_NULLS ON
                GO

                SET QUOTED_IDENTIFIER ON
                GO

                CREATE VIEW [dbo].[Advance_VW_GetTimeLagForRequisition]
                AS
                SELECT NEWID() as Id,
                panel.Id PanelId,
                panel.Name PanelName,
                level.Id LevelId,
                level.Name LevelName,
                IDCOLMIS.dbo.GetWorkingDaysCount(ticket.AuthorizedOn, GETDATE()) AS PendingDays
                FROM IDCOLAdvanceDB.dbo.ApprovalPanel panel
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalLevel level
                ON level.ApprovalPanelId = panel.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalTicket ticket
                ON ticket.ApprovalLevelId = level.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader requisition
                ON requisition.Id = ticket.AdvanceRequisitionHeaderId
                WHERE ticket.TicketTypeId = 1 AND ticket.ApprovalStatusId = 3
                GO
                ");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetTimeLagForRequisition')
                    DROP VIEW Advance_VW_GetTimeLagForRequisition");
        }
    }
}
