namespace IDCOLAdvanceModule.UI
{
    partial class SelectDesignationUI
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
            this.designationListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.selectButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // designationListView
            // 
            this.designationListView.CheckBoxes = true;
            this.designationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.designationListView.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.designationListView.FullRowSelect = true;
            this.designationListView.GridLines = true;
            this.designationListView.Location = new System.Drawing.Point(12, 12);
            this.designationListView.Name = "designationListView";
            this.designationListView.OwnerDraw = true;
            this.designationListView.Size = new System.Drawing.Size(378, 337);
            this.designationListView.TabIndex = 6;
            this.designationListView.UseCompatibleStateImageBehavior = false;
            this.designationListView.View = System.Windows.Forms.View.Details;
            this.designationListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.designationListView_DrawColumnHeader);
            this.designationListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.designationListView_DrawItem);
            this.designationListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.designationListView_DrawSubItem);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Designation";
            this.columnHeader2.Width = 360;
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(316, 355);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(74, 23);
            this.selectButton.TabIndex = 7;
            this.selectButton.Text = "Select";
            this.selectButton.UseSelectable = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // SelectDesignationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(402, 393);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.designationListView);
            this.Name = "SelectDesignationUI";
            this.Text = "Dilute Designation(s)";
            this.Load += new System.EventHandler(this.SelectDesignation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView designationListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MetroFramework.Controls.MetroButton selectButton;

    }
}