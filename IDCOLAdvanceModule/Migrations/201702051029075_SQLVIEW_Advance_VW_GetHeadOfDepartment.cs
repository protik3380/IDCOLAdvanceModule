namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SQLVIEW_Advance_VW_GetHeadOfDepartment : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetHeadOfDepartment]
                    AS
                    SELECT NEWID() AS Id, hod.EmployeeUserName AS HeadOfDepartmentUserName, ut.FirstName + ' ' + ut.MiddleName + ' ' + ut.LastName AS HeadOfDepartmentFullName, dept.DepartmentID, dept.DepartmentName, 
                    hod.Id AS HODId
                    FROM dbo.HeadOfDepartment AS hod
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments AS dept ON hod.DepartmentId = dept.DepartmentID
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable AS ut ON hod.EmployeeUserName = ut.UserName
                    GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetHeadOfDepartment')
                    DROP VIEW Advance_VW_GetHeadOfDepartment");
        }
    }
}
