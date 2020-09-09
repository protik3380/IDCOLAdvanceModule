namespace IDCOLAdvanceModule.UI.Settings
{
    partial class RequisitionCategoryWiseCostItemSettingsUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.categoryCostItemSettingsDataGridView = new System.Windows.Forms.DataGridView();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costItemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entitlementMendatoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.isEntitlementMandatoryCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.costItemComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.advanceCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoryCostItemSettingsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.categoryCostItemSettingsDataGridView);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.isEntitlementMandatoryCheckBox);
            this.groupBox1.Controls.Add(this.costItemComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.advanceCategoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 474);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advance Requisition Category Wise Cost Item Settings";
            // 
            // categoryCostItemSettingsDataGridView
            // 
            this.categoryCostItemSettingsDataGridView.AllowUserToAddRows = false;
            this.categoryCostItemSettingsDataGridView.AllowUserToDeleteRows = false;
            this.categoryCostItemSettingsDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryCostItemSettingsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.categoryCostItemSettingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.categoryCostItemSettingsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.serialColumn,
            this.categoryColumn,
            this.costItemColumn,
            this.entitlementMendatoryColumn});
            this.categoryCostItemSettingsDataGridView.EnableHeadersVisualStyles = false;
            this.categoryCostItemSettingsDataGridView.Location = new System.Drawing.Point(7, 168);
            this.categoryCostItemSettingsDataGridView.MultiSelect = false;
            this.categoryCostItemSettingsDataGridView.Name = "categoryCostItemSettingsDataGridView";
            this.categoryCostItemSettingsDataGridView.ReadOnly = true;
            this.categoryCostItemSettingsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.categoryCostItemSettingsDataGridView.Size = new System.Drawing.Size(536, 300);
            this.categoryCostItemSettingsDataGridView.TabIndex = 5;
            this.categoryCostItemSettingsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.categoryCostItemSettingsDataGridView_CellContentClick);
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 50;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Width = 60;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Category";
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            // 
            // costItemColumn
            // 
            this.costItemColumn.HeaderText = "Cost Item";
            this.costItemColumn.Name = "costItemColumn";
            this.costItemColumn.ReadOnly = true;
            // 
            // entitlementMendatoryColumn
            // 
            this.entitlementMendatoryColumn.HeaderText = "Entitlement Mendatory";
            this.entitlementMendatoryColumn.Name = "entitlementMendatoryColumn";
            this.entitlementMendatoryColumn.ReadOnly = true;
            this.entitlementMendatoryColumn.Width = 180;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(336, 132);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // isEntitlementMandatoryCheckBox
            // 
            this.isEntitlementMandatoryCheckBox.AutoSize = true;
            this.isEntitlementMandatoryCheckBox.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.isEntitlementMandatoryCheckBox.Location = new System.Drawing.Point(186, 111);
            this.isEntitlementMandatoryCheckBox.Name = "isEntitlementMandatoryCheckBox";
            this.isEntitlementMandatoryCheckBox.Size = new System.Drawing.Size(157, 15);
            this.isEntitlementMandatoryCheckBox.TabIndex = 2;
            this.isEntitlementMandatoryCheckBox.Text = "Entitlement Mandatory?";
            this.isEntitlementMandatoryCheckBox.UseSelectable = true;
            // 
            // costItemComboBox
            // 
            this.costItemComboBox.FormattingEnabled = true;
            this.costItemComboBox.ItemHeight = 23;
            this.costItemComboBox.Location = new System.Drawing.Point(186, 76);
            this.costItemComboBox.Name = "costItemComboBox";
            this.costItemComboBox.Size = new System.Drawing.Size(225, 29);
            this.costItemComboBox.TabIndex = 1;
            this.costItemComboBox.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(115, 76);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(65, 19);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "Cost Item";
            // 
            // advanceCategoryComboBox
            // 
            this.advanceCategoryComboBox.FormattingEnabled = true;
            this.advanceCategoryComboBox.ItemHeight = 23;
            this.advanceCategoryComboBox.Location = new System.Drawing.Point(186, 41);
            this.advanceCategoryComboBox.Name = "advanceCategoryComboBox";
            this.advanceCategoryComboBox.Size = new System.Drawing.Size(225, 29);
            this.advanceCategoryComboBox.TabIndex = 1;
            this.advanceCategoryComboBox.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(62, 41);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(118, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Advance Category";
            // 
            // RequisitionCategoryWiseCostItemSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(578, 498);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "RequisitionCategoryWiseCostItemSettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Requisition Category Wise CostI tem Settings";
            this.Load += new System.EventHandler(this.RequisitionCategoryWiseCostItemSettingsUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoryCostItemSettingsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton saveButton;
        private MetroFramework.Controls.MetroCheckBox isEntitlementMandatoryCheckBox;
        private MetroFramework.Controls.MetroComboBox costItemComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox advanceCategoryComboBox;
        private System.Windows.Forms.DataGridView categoryCostItemSettingsDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costItemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn entitlementMendatoryColumn;
    }
}