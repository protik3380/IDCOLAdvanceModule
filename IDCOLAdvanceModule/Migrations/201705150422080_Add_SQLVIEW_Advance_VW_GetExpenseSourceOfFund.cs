namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SQLVIEW_Advance_VW_GetExpenseSourceOfFund : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Advance_VW_GetExpenseSourceOfFund
                AS
                SELECT NEWID() AS Id,
                header.Id ExpenseHeaderId,
                source.Id SourceOfFundId,
                source.Name SourceOfFundName,
                expenseSource.Percentage
                FROM ExpenseSourceOfFund expenseSource 
                LEFT JOIN AdvanceExpenseHeader header
                ON header.Id = expenseSource.AdvanceExpenseHeaderId
                LEFT JOIN SourceOfFund source 
                ON expenseSource.SourceOfFundId = source.Id"
                );
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetExpenseSourceOfFund')
                    DROP VIEW Advance_VW_GetExpenseSourceOfFund");
        }
    }
}
