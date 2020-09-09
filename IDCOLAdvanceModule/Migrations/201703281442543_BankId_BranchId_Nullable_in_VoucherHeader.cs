namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankId_BranchId_Nullable_in_VoucherHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionVoucherHeader", "VoucherTypeId", c => c.Long());
            AddColumn("dbo.ExpenseVoucherHeader", "VoucherTypeId", c => c.Long());
            AlterColumn("dbo.RequisitionVoucherHeader", "BankId", c => c.Long());
            AlterColumn("dbo.RequisitionVoucherHeader", "BranchId", c => c.Long());
            AlterColumn("dbo.ExpenseVoucherHeader", "BankId", c => c.Long());
            AlterColumn("dbo.ExpenseVoucherHeader", "BranchId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExpenseVoucherHeader", "BranchId", c => c.Long(nullable: false));
            AlterColumn("dbo.ExpenseVoucherHeader", "BankId", c => c.Long(nullable: false));
            AlterColumn("dbo.RequisitionVoucherHeader", "BranchId", c => c.Long(nullable: false));
            AlterColumn("dbo.RequisitionVoucherHeader", "BankId", c => c.Long(nullable: false));
            DropColumn("dbo.ExpenseVoucherHeader", "VoucherTypeId");
            DropColumn("dbo.RequisitionVoucherHeader", "VoucherTypeId");
        }
    }
}
