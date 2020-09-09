namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_SP_Advance_SP_GetLocalTravelReport : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROC Advance_SP_GetLocalTravelReport
                AS
                CREATE TABLE #RequisitionInfo
                (
	                RequisitionId bigint,
	                AdvanceAmount decimal(18, 2)
                )

                CREATE TABLE #ExpenseInfo
                (
	                RequisitionId bigint,
	                ExpenseId bigint,
	                ExpenseNo varchar(50),
	                ExpensePurpose varchar(5000),
	                ExpensePlace varchar(500),
	                ExpenseFromDate datetime,
	                ExpenseToDate datetime,
	                ExpenseIssueDate datetime,
	                ExpenseAmount decimal(18, 2)
                )

                INSERT INTO #RequisitionInfo
                SELECT reqHeader.Id, CASE WHEN reqHeader.AdvanceCategoryId = 2 THEN ISNULL(SUM(d.AdvanceAmount * d.ConversionRate), 0)
                ELSE ISNULL(SUM(d.AdvanceAmount * reqHeader.ConversionRate), 0) 
                END AS AdvanceAmountInBDT 
                FROM AdvanceRequisitionHeader reqHeader LEFT OUTER JOIN 
                AdvanceRequisitionDetail d ON d.AdvanceRequisitionHeaderId = reqHeader.Id
                GROUP BY 
                reqHeader.Id, reqHeader.AdvanceCategoryId

                INSERT INTO #ExpenseInfo
                SELECT 
                reqHeader.Id RequisitionId,
                expHeader.Id ExpenseId,
                expHeader.ExpenseNo,
                expHeader.Purpose,
                expHeader.PlaceOfVisit,
                expHeader.FromDate,
                expHeader.ToDate,
                expHeader.ExpenseIssueDate,
                CASE WHEN expHeader.AdvanceCategoryId = 2 THEN SUM(expDetail.ExpenseAmount * expDetail.ConversionRate)
                ELSE SUM(expDetail.ExpenseAmount * expHeader.ConversionRate) 
                END AS ExpenseAmountInBDT

                FROM IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader reqHeader
                LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceExpenseHeader expHeader ON expHeader.Id = reqHeader.AdvanceExpenseHeaderId
                LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceExpenseDetail expDetail ON expDetail.AdvanceExpenseHeaderId = expHeader.Id

                GROUP BY 
                reqHeader.Id,
                expHeader.Id,
                expHeader.ExpenseNo,
                expHeader.Purpose,
                expHeader.PlaceOfVisit,
                expHeader.FromDate,
                expHeader.ToDate,
                expHeader.ExpenseIssueDate,
                expHeader.AdvanceCategoryId

                SELECT NEWID() AS Id,
                employee.EmployeeID EmployeeId,
                employee.UserName EmployeeUserName, 
                employee.FirstName + ' ' + employee.MiddleName + ' ' + employee.LastName AS EmployeeName,
                reqHeader.Id RequisitionId,
                reqHeader.RequisitionNo, 
                reqHeader.Purpose RequisitionPurpose,
                reqHeader.PlaceOfVisit RequisitionPlace,
                reqHeader.FromDate RequisitionFromDate,
                reqHeader.ToDate RequisitionToDate,
                reqHeader.AdvanceIssueDate,
                ri.AdvanceAmount,
                ei.ExpenseId,
                ei.ExpenseNo,
                ei.ExpensePurpose,
                ei.ExpensePlace,
                ei.ExpenseFromDate,
                ei.ExpenseToDate,
                ei.ExpenseIssueDate,
                ei.ExpenseAmount
                FROM 
                AdvanceRequisitionHeader reqHeader 
                LEFT OUTER JOIN #RequisitionInfo ri ON ri.RequisitionId = reqHeader.Id
                LEFT OUTER JOIN #ExpenseInfo ei ON ei.RequisitionId = reqHeader.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable employee ON employee.UserName = reqHeader.RequesterUserName
                WHERE reqHeader.AdvanceCategoryId = 1

                DELETE FROM #RequisitionInfo
                DROP TABLE #RequisitionInfo
                DELETE FROM #ExpenseInfo
                DROP TABLE #ExpenseInfo");
        }
        
        public override void Down()
        {
            Sql(@"DROP PROCEDURE Advance_SP_GetLocalTravelReport");
        }
    }
}
