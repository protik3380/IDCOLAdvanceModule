namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_GetLocalTravelReport_Department : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetLocalTravelReport]
                AS

                SELECT NEWID() Id,
                employee.EmployeeID EmployeeId,
                employee.UserName EmployeeUserName, 
                employee.FirstName + ' ' + employee.MiddleName + ' ' + employee.LastName AS EmployeeName,
				department.DepartmentID DepartmentId,
				department.DepartmentName,
                reqHeader.Id RequisitionId,
                reqHeader.RequisitionNo, 
                reqHeader.Purpose RequisitionPurpose,
                reqHeader.PlaceOfVisit RequisitionPlace,
                reqHeader.FromDate RequisitionFromDate,
                reqHeader.ToDate RequisitionToDate,
                reqHeader.AdvanceIssueDate,
                SUM(reqDetail.AdvanceAmount * reqHeader.ConversionRate) AS AdvanceAmount,
                expHeader.Id ExpenseId,
                expHeader.ExpenseNo, 
                expHeader.Purpose ExpensePurpose,
                expHeader.PlaceOfVisit ExpensePlace,
                expHeader.FromDate ExpenseFromDate,
                expHeader.ToDate ExpenseToDate,
                expHeader.ExpenseIssueDate,
                SUM(expDetail.ExpenseAmount * expHeader.ConversionRate) AS ExpenseAmount

                FROM AdvanceRequisitionHeader reqHeader
                LEFT JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                LEFT JOIN AdvanceCategory category ON category.Id = reqHeader.AdvanceCategoryId
                LEFT JOIN AdvanceExpenseHeader expHeader ON expHeader.Id = reqHeader.AdvanceExpenseHeaderId
                LEFT JOIN AdvanceExpenseDetail expDetail ON expDetail.AdvanceExpenseHeaderId = expHeader.Id
                LEFT JOIN IDCOLMIS.dbo.UserTable employee ON employee.UserName = reqHeader.RequesterUserName
				LEFT JOIN IDCOLMIS.dbo.Admin_Departments department ON department.DepartmentID = employee.DepartmentID

                GROUP BY
                employee.EmployeeID,
                employee.UserName,
                employee.FirstName,
                employee.MiddleName,
                employee.LastName,
				department.DepartmentID,
				department.DepartmentName,
                reqHeader.Id,
                reqHeader.RequisitionNo,
                reqHeader.Purpose,
                reqHeader.PlaceOfVisit,
                reqHeader.FromDate,
                reqHeader.ToDate,
                reqHeader.AdvanceIssueDate,
                reqHeader.AdvanceCategoryId,
                expHeader.Id,
                expHeader.ExpenseNo,
                expHeader.Purpose,
                expHeader.PlaceOfVisit,
                expHeader.FromDate,
                expHeader.ToDate,
                expHeader.ExpenseIssueDate,
                expHeader.AdvanceCategoryId

                HAVING reqHeader.AdvanceCategoryId = 1

                GO");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetLocalTravelReport]
                AS

                SELECT NEWID() Id,
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
                SUM(reqDetail.AdvanceAmount * reqHeader.ConversionRate) AS AdvanceAmount,
                expHeader.Id ExpenseId,
                expHeader.ExpenseNo, 
                expHeader.Purpose ExpensePurpose,
                expHeader.PlaceOfVisit ExpensePlace,
                expHeader.FromDate ExpenseFromDate,
                expHeader.ToDate ExpenseToDate,
                expHeader.ExpenseIssueDate,
                SUM(expDetail.ExpenseAmount * expHeader.ConversionRate) AS ExpenseAmount

                FROM AdvanceRequisitionHeader reqHeader
                LEFT JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                LEFT JOIN AdvanceCategory category ON category.Id = reqHeader.AdvanceCategoryId
                LEFT JOIN AdvanceExpenseHeader expHeader ON expHeader.Id = reqHeader.AdvanceExpenseHeaderId
                LEFT JOIN AdvanceExpenseDetail expDetail ON expDetail.AdvanceExpenseHeaderId = expHeader.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable employee ON employee.UserName = reqHeader.RequesterUserName

                GROUP BY
                employee.EmployeeID,
                employee.UserName,
                employee.FirstName,
                employee.MiddleName,
                employee.LastName,
                reqHeader.Id,
                reqHeader.RequisitionNo,
                reqHeader.Purpose,
                reqHeader.PlaceOfVisit,
                reqHeader.FromDate,
                reqHeader.ToDate,
                reqHeader.AdvanceIssueDate,
                reqHeader.AdvanceCategoryId,
                expHeader.Id,
                expHeader.ExpenseNo,
                expHeader.Purpose,
                expHeader.PlaceOfVisit,
                expHeader.FromDate,
                expHeader.ToDate,
                expHeader.ExpenseIssueDate,
                expHeader.AdvanceCategoryId

                HAVING reqHeader.AdvanceCategoryId = 1

                GO");
        }
    }
}
