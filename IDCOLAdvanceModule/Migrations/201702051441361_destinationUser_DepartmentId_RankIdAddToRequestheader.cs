namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class destinationUser_DepartmentId_RankIdAddToRequestheader : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesinationUserForTicket",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequisitionApprovalTicketId = c.Long(nullable: false),
                        DestinationUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequisitionApprovalTicket", t => t.RequisitionApprovalTicketId)
                .Index(t => t.RequisitionApprovalTicketId);
            
            AddColumn("dbo.AdvanceRequisitionHeader", "RequesterDepartmentId", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AdvanceRequisitionHeader", "RequesterRankID", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.RequisitionApprovalTicket", "DestinationUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequisitionApprovalTicket", "DestinationUserName", c => c.String());
            DropForeignKey("dbo.DesinationUserForTicket", "RequisitionApprovalTicketId", "dbo.RequisitionApprovalTicket");
            DropIndex("dbo.DesinationUserForTicket", new[] { "RequisitionApprovalTicketId" });
            DropColumn("dbo.AdvanceRequisitionHeader", "RequesterRankID");
            DropColumn("dbo.AdvanceRequisitionHeader", "RequesterDepartmentId");
            DropTable("dbo.DesinationUserForTicket");
        }
    }
}
