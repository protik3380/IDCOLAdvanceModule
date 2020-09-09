namespace IDCOLAdvanceModule.UI.Settings
{
    partial class DiluteDesignationSetupUI
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
            this.diluteDesignationSettingsListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // diluteDesignationSettingsListView
            // 
            this.diluteDesignationSettingsListView.CheckBoxes = true;
            this.diluteDesignationSettingsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.diluteDesignationSettingsListView.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diluteDesignationSettingsListView.FullRowSelect = true;
            this.diluteDesignationSettingsListView.GridLines = true;
            this.diluteDesignationSettingsListView.Location = new System.Drawing.Point(9, 12);
            this.diluteDesignationSettingsListView.MultiSelect = false;
            this.diluteDesignationSettingsListView.Name = "diluteDesignationSettingsListView";
            this.diluteDesignationSettingsListView.OwnerDraw = true;
            this.diluteDesignationSettingsListView.Size = new System.Drawing.Size(378, 337);
            this.diluteDesignationSettingsListView.TabIndex = 5;
            this.diluteDesignationSettingsListView.UseCompatibleStateImageBehavior = false;
            this.diluteDesignationSettingsListView.View = System.Windows.Forms.View.Details;
            this.diluteDesignationSettingsListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.diluteDesignationSettingsListView_DrawColumnHeader);
            this.diluteDesignationSettingsListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.diluteDesignationSettingsListView_DrawItem);
            this.diluteDesignationSettingsListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.diluteDesignationSettingsListView_DrawSubItem);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Designation";
            this.columnHeader2.Width = 366;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(311, 355);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // DiluteDesignationSetupUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(398, 386);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.diluteDesignationSettingsListView);
            this.Name = "DiluteDesignationSetupUI";
            this.Text = "Dilute Designation Setup";
            this.Load += new System.EventHandler(this.DiluteDesignationSetupUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView diluteDesignationSettingsListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MetroFramework.Controls.MetroButton saveButton;
    }
}