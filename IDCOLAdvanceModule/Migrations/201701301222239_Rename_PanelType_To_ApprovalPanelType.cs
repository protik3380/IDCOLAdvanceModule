namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_PanelType_To_ApprovalPanelType : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PanelType", newName: "ApprovalPanelType");
            RenameColumn(table: "dbo.ApprovalPanel", name: "PanelTypeId", newName: "ApprovalPanelTypeId");
            RenameIndex(table: "dbo.ApprovalPanel", name: "IX_PanelTypeId", newName: "IX_ApprovalPanelTypeId");
            Sql(@"IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ApprovalPanel_dbo.PanelType_PanelTypeId]') 
                AND parent_object_id = OBJECT_ID(N'[dbo].[ApprovalPanel]'))
                ALTER TABLE [dbo].[ApprovalPanel] DROP CONSTRAINT [FK_dbo.ApprovalPanel_dbo.PanelType_PanelTypeId]
                GO
                ");
        }
        
        public override void Down()
        {
            Sql(@"ALTER TABLE [dbo].[ApprovalPanel]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ApprovalPanel_dbo.PanelType_PanelTypeId] FOREIGN KEY([ApprovalPanelTypeId])
                REFERENCES [dbo].[ApprovalPanelType] ([Id])
                GO

                ALTER TABLE [dbo].[ApprovalPanel] CHECK CONSTRAINT [FK_dbo.ApprovalPanel_dbo.PanelType_PanelTypeId]
                GO
                ");
            RenameIndex(table: "dbo.ApprovalPanel", name: "IX_ApprovalPanelTypeId", newName: "IX_PanelTypeId");
            RenameColumn(table: "dbo.ApprovalPanel", name: "ApprovalPanelTypeId", newName: "PanelTypeId");
            RenameTable(name: "dbo.ApprovalPanelType", newName: "PanelType");
        }
    }
}
