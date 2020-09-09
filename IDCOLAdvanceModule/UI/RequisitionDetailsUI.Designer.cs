namespace IDCOLAdvanceModule.UI
{
    partial class RequisitionDetailsUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detailsShowGroupBox = new System.Windows.Forms.GroupBox();
            this.advanceDetailsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detailsShowGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // detailsShowGroupBox
            // 
            this.detailsShowGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.detailsShowGroupBox.Controls.Add(this.advanceDetailsListView);
            this.detailsShowGroupBox.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsShowGroupBox.Location = new System.Drawing.Point(12, 12);
            this.detailsShowGroupBox.Name = "detailsShowGroupBox";
            this.detailsShowGroupBox.Size = new System.Drawing.Size(979, 277);
            this.detailsShowGroupBox.TabIndex = 5;
            this.detailsShowGroupBox.TabStop = false;
            // 
            // advanceDetailsListView
            // 
            this.advanceDetailsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.advanceDetailsListView.FullRowSelect = true;
            this.advanceDetailsListView.GridLines = true;
            this.advanceDetailsListView.Location = new System.Drawing.Point(16, 19);
            this.advanceDetailsListView.Name = "advanceDetailsListView";
            this.advanceDetailsListView.OwnerDraw = true;
            this.advanceDetailsListView.Size = new System.Drawing.Size(947, 232);
            this.advanceDetailsListView.TabIndex = 10;
            this.advanceDetailsListView.UseCompatibleStateImageBehavior = false;
            this.advanceDetailsListView.View = System.Windows.Forms.View.Details;
            this.advanceDetailsListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.advanceDetailsListView_DrawColumnHeader);
            this.advanceDetailsListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.advanceDetailsListView_DrawItem);
            this.advanceDetailsListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.advanceDetailsListView_DrawSubItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Travel cost items";
            this.columnHeader1.Width = 184;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Entitlements";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "No. of day(s)";
            this.columnHeader9.Width = 127;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Advance amount";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Advance amount (BDT)";
            this.columnHeader11.Width = 183;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Remarks";
            this.columnHeader12.Width = 307;
            // 
            // RequisitionDetailsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1003, 300);
            this.Controls.Add(this.detailsShowGroupBox);
            this.Name = "RequisitionDetailsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advance Requisition Details";
            this.Load += new System.EventHandler(this.AdvanceRequisitionDetailsUI_Load);
            this.detailsShowGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox detailsShowGroupBox;
        private System.Windows.Forms.ListView advanceDetailsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
    }
}