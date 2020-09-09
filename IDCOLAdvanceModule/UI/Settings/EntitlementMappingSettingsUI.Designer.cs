namespace IDCOLAdvanceModule.UI.Settings
{
    partial class EntitlementMappingSettingsUI
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.employeeDesignationDataGridView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.designationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isFullAmountEntitlementCheckBox = new System.Windows.Forms.CheckBox();
            this.requisitionCategorywWiseCostItemButton = new MetroFramework.Controls.MetroButton();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.entitlementAmountTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.costItemComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.advanceCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.entitlementMappingSettingDataGridView = new System.Windows.Forms.DataGridView();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SerialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designationMappingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advanceCategoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costItemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entitlementAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullEntitlementAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entitlementMappingSettingContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDesignationDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entitlementMappingSettingDataGridView)).BeginInit();
            this.entitlementMappingSettingContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.employeeDesignationDataGridView);
            this.groupBox1.Controls.Add(this.isFullAmountEntitlementCheckBox);
            this.groupBox1.Controls.Add(this.requisitionCategorywWiseCostItemButton);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.entitlementAmountTextBox);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.costItemComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.advanceCategoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 464);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entitlement Setting";
            // 
            // employeeDesignationDataGridView
            // 
            this.employeeDesignationDataGridView.AllowUserToAddRows = false;
            this.employeeDesignationDataGridView.AllowUserToDeleteRows = false;
            this.employeeDesignationDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.employeeDesignationDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeDesignationDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.employeeDesignationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employeeDesignationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.designationColumn});
            this.employeeDesignationDataGridView.EnableHeadersVisualStyles = false;
            this.employeeDesignationDataGridView.Location = new System.Drawing.Point(6, 155);
            this.employeeDesignationDataGridView.MultiSelect = false;
            this.employeeDesignationDataGridView.Name = "employeeDesignationDataGridView";
            this.employeeDesignationDataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeDesignationDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.employeeDesignationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeDesignationDataGridView.Size = new System.Drawing.Size(413, 262);
            this.employeeDesignationDataGridView.TabIndex = 10;
            this.employeeDesignationDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.employeeDesignationDataGridView_CellContentClick);
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Width = 25;
            // 
            // designationColumn
            // 
            this.designationColumn.HeaderText = "Designation";
            this.designationColumn.Name = "designationColumn";
            this.designationColumn.ReadOnly = true;
            this.designationColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.designationColumn.Width = 340;
            // 
            // isFullAmountEntitlementCheckBox
            // 
            this.isFullAmountEntitlementCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.isFullAmountEntitlementCheckBox.AutoSize = true;
            this.isFullAmountEntitlementCheckBox.Location = new System.Drawing.Point(129, 100);
            this.isFullAmountEntitlementCheckBox.Name = "isFullAmountEntitlementCheckBox";
            this.isFullAmountEntitlementCheckBox.Size = new System.Drawing.Size(163, 20);
            this.isFullAmountEntitlementCheckBox.TabIndex = 9;
            this.isFullAmountEntitlementCheckBox.Text = "Full Amount Entitlement";
            this.isFullAmountEntitlementCheckBox.UseVisualStyleBackColor = true;
            this.isFullAmountEntitlementCheckBox.CheckStateChanged += new System.EventHandler(this.isFullAmountEntitlementcheckBox_CheckStateChanged);
            // 
            // requisitionCategorywWiseCostItemButton
            // 
            this.requisitionCategorywWiseCostItemButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.requisitionCategorywWiseCostItemButton.Location = new System.Drawing.Point(313, 34);
            this.requisitionCategorywWiseCostItemButton.Name = "requisitionCategorywWiseCostItemButton";
            this.requisitionCategorywWiseCostItemButton.Size = new System.Drawing.Size(106, 23);
            this.requisitionCategorywWiseCostItemButton.TabIndex = 3;
            this.requisitionCategorywWiseCostItemButton.Text = "Cost Item Setting";
            this.requisitionCategorywWiseCostItemButton.UseSelectable = true;
            this.requisitionCategorywWiseCostItemButton.Click += new System.EventHandler(this.requisitionCategorywWiseCostItemButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(335, 423);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // entitlementAmountTextBox
            // 
            this.entitlementAmountTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.entitlementAmountTextBox.CustomButton.Image = null;
            this.entitlementAmountTextBox.CustomButton.Location = new System.Drawing.Point(157, 1);
            this.entitlementAmountTextBox.CustomButton.Name = "";
            this.entitlementAmountTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.entitlementAmountTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.entitlementAmountTextBox.CustomButton.TabIndex = 1;
            this.entitlementAmountTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.entitlementAmountTextBox.CustomButton.UseSelectable = true;
            this.entitlementAmountTextBox.CustomButton.Visible = false;
            this.entitlementAmountTextBox.Lines = new string[0];
            this.entitlementAmountTextBox.Location = new System.Drawing.Point(129, 126);
            this.entitlementAmountTextBox.MaxLength = 32767;
            this.entitlementAmountTextBox.Name = "entitlementAmountTextBox";
            this.entitlementAmountTextBox.PasswordChar = '\0';
            this.entitlementAmountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.entitlementAmountTextBox.SelectedText = "";
            this.entitlementAmountTextBox.SelectionLength = 0;
            this.entitlementAmountTextBox.SelectionStart = 0;
            this.entitlementAmountTextBox.ShortcutsEnabled = true;
            this.entitlementAmountTextBox.Size = new System.Drawing.Size(179, 23);
            this.entitlementAmountTextBox.TabIndex = 2;
            this.entitlementAmountTextBox.UseSelectable = true;
            this.entitlementAmountTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.entitlementAmountTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.entitlementAmountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.entitlementAmountTextBox_KeyPress);
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(48, 126);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(74, 19);
            this.metroLabel3.TabIndex = 6;
            this.metroLabel3.Text = "Entitlement";
            // 
            // costItemComboBox
            // 
            this.costItemComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.costItemComboBox.FormattingEnabled = true;
            this.costItemComboBox.ItemHeight = 23;
            this.costItemComboBox.Location = new System.Drawing.Point(129, 65);
            this.costItemComboBox.Name = "costItemComboBox";
            this.costItemComboBox.Size = new System.Drawing.Size(179, 29);
            this.costItemComboBox.TabIndex = 1;
            this.costItemComboBox.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(57, 65);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(65, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Cost Item";
            // 
            // advanceCategoryComboBox
            // 
            this.advanceCategoryComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.advanceCategoryComboBox.FormattingEnabled = true;
            this.advanceCategoryComboBox.ItemHeight = 23;
            this.advanceCategoryComboBox.Location = new System.Drawing.Point(129, 30);
            this.advanceCategoryComboBox.Name = "advanceCategoryComboBox";
            this.advanceCategoryComboBox.Size = new System.Drawing.Size(179, 29);
            this.advanceCategoryComboBox.TabIndex = 0;
            this.advanceCategoryComboBox.UseSelectable = true;
            this.advanceCategoryComboBox.SelectionChangeCommitted += new System.EventHandler(this.advanceCategoryComboBox_SelectionChangeCommitted);
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(4, 30);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(118, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Advance Category";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.entitlementMappingSettingDataGridView);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(439, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(778, 464);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // entitlementMappingSettingDataGridView
            // 
            this.entitlementMappingSettingDataGridView.AllowUserToAddRows = false;
            this.entitlementMappingSettingDataGridView.AllowUserToDeleteRows = false;
            this.entitlementMappingSettingDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entitlementMappingSettingDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.entitlementMappingSettingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entitlementMappingSettingDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.removeColumn,
            this.SerialColumn,
            this.designationMappingColumn,
            this.advanceCategoryColumn,
            this.costItemColumn,
            this.entitlementAmountColumn,
            this.fullEntitlementAmountColumn});
            this.entitlementMappingSettingDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entitlementMappingSettingDataGridView.EnableHeadersVisualStyles = false;
            this.entitlementMappingSettingDataGridView.Location = new System.Drawing.Point(3, 19);
            this.entitlementMappingSettingDataGridView.MultiSelect = false;
            this.entitlementMappingSettingDataGridView.Name = "entitlementMappingSettingDataGridView";
            this.entitlementMappingSettingDataGridView.ReadOnly = true;
            this.entitlementMappingSettingDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entitlementMappingSettingDataGridView.Size = new System.Drawing.Size(772, 442);
            this.entitlementMappingSettingDataGridView.TabIndex = 0;
            this.entitlementMappingSettingDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.entitlementMappingSettingDataGridView_CellContentClick);
            // 
            // removeColumn
            // 
            this.removeColumn.HeaderText = "";
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.ReadOnly = true;
            this.removeColumn.Width = 70;
            // 
            // SerialColumn
            // 
            this.SerialColumn.HeaderText = "Serial";
            this.SerialColumn.Name = "SerialColumn";
            this.SerialColumn.ReadOnly = true;
            this.SerialColumn.Width = 50;
            // 
            // designationMappingColumn
            // 
            this.designationMappingColumn.HeaderText = "Designation";
            this.designationMappingColumn.Name = "designationMappingColumn";
            this.designationMappingColumn.ReadOnly = true;
            // 
            // advanceCategoryColumn
            // 
            this.advanceCategoryColumn.HeaderText = "Advance Category";
            this.advanceCategoryColumn.Name = "advanceCategoryColumn";
            this.advanceCategoryColumn.ReadOnly = true;
            // 
            // costItemColumn
            // 
            this.costItemColumn.HeaderText = "Cost Item";
            this.costItemColumn.Name = "costItemColumn";
            this.costItemColumn.ReadOnly = true;
            // 
            // entitlementAmountColumn
            // 
            this.entitlementAmountColumn.HeaderText = "Entitlement Amount";
            this.entitlementAmountColumn.Name = "entitlementAmountColumn";
            this.entitlementAmountColumn.ReadOnly = true;
            this.entitlementAmountColumn.Width = 150;
            // 
            // fullEntitlementAmountColumn
            // 
            this.fullEntitlementAmountColumn.HeaderText = "Full Entitlement Amount";
            this.fullEntitlementAmountColumn.Name = "fullEntitlementAmountColumn";
            this.fullEntitlementAmountColumn.ReadOnly = true;
            this.fullEntitlementAmountColumn.Width = 150;
            // 
            // entitlementMappingSettingContextMenu
            // 
            this.entitlementMappingSettingContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.entitlementMappingSettingContextMenu.Name = "entitlementMappingSettingContextMenu";
            this.entitlementMappingSettingContextMenu.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // EntitlementMappingSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1224, 499);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "EntitlementMappingSettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entitlement Mapping Setting";
            this.Load += new System.EventHandler(this.EntitlementMappingSettingsUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDesignationDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entitlementMappingSettingDataGridView)).EndInit();
            this.entitlementMappingSettingContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox entitlementAmountTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox costItemComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox advanceCategoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroContextMenu entitlementMappingSettingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private MetroFramework.Controls.MetroButton requisitionCategorywWiseCostItemButton;
        private System.Windows.Forms.CheckBox isFullAmountEntitlementCheckBox;
        private System.Windows.Forms.DataGridView employeeDesignationDataGridView;
        private System.Windows.Forms.DataGridView entitlementMappingSettingDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationMappingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn advanceCategoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costItemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn entitlementAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullEntitlementAmountColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationColumn;
    }
}