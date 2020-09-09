namespace IDCOLAdvanceModule.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class Column_Renamed_ExpenseDetail_ThirdPartyAdded : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.AdvanceExpenseDetail", "ReceipientOrPayeeName", c => c.String());
			AddColumn("dbo.AdvanceExpenseDetail", "IsThirdPartyReceipient", c => c.Boolean(nullable: false));
			DropColumn("dbo.AdvanceExpenseDetail", "Vendor");
			DropColumn("dbo.AdvanceExpenseDetail", "IsVendorReceipient");

			
		}
		
		public override void Down()
		{
			AddColumn("dbo.AdvanceExpenseDetail", "IsVendorReceipient", c => c.Boolean(nullable: false));
			AddColumn("dbo.AdvanceExpenseDetail", "Vendor", c => c.String());
			DropColumn("dbo.AdvanceExpenseDetail", "IsThirdPartyReceipient");
			DropColumn("dbo.AdvanceExpenseDetail", "ReceipientOrPayeeName");
		}
	}
}
