namespace IDCOLAdvanceModule.UI.Settings
{
    partial class ExecutiveOverseasTravelingAllowanceUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isFullAmountEntitlementCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.executiveOverseasTravellingAllowanceDataGridView = new System.Windows.Forms.DataGridView();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costItemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationGroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencyComboBox = new MetroFramework.Controls.MetroComboBox();
            this.costItemComboBox = new MetroFramework.Controls.MetroComboBox();
            this.employeeCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.locationGroupComboBox = new MetroFramework.Controls.MetroComboBox();
            this.entitlementAmountTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.executiveOverseasTravellingAllowanceContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.executiveOverseasTravellingAllowanceDataGridView)).BeginInit();
            this.executiveOverseasTravellingAllowanceContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.isFullAmountEntitlementCheckBox);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.currencyComboBox);
            this.groupBox1.Controls.Add(this.costItemComboBox);
            this.groupBox1.Controls.Add(this.employeeCategoryComboBox);
            this.groupBox1.Controls.Add(this.locationGroupComboBox);
            this.groupBox1.Controls.Add(this.entitlementAmountTextBox);
            this.groupBox1.Controls.Add(this.metroLabel4);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.metroLabel5);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(22, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 575);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Overseas Travel Group Setup ";
            // 
            // isFullAmountEntitlementCheckBox
            // 
            this.isFullAmountEntitlementCheckBox.AutoSize = true;
            this.isFullAmountEntitlementCheckBox.Location = new System.Drawing.Point(277, 172);
            this.isFullAmountEntitlementCheckBox.Name = "isFullAmountEntitlementCheckBox";
            this.isFullAmountEntitlementCheckBox.Size = new System.Drawing.Size(153, 15);
            this.isFullAmountEntitlementCheckBox.TabIndex = 9;
            this.isFullAmountEntitlementCheckBox.Text = "Full Amount Entitlement";
            this.isFullAmountEntitlementCheckBox.UseSelectable = true;
            this.isFullAmountEntitlementCheckBox.CheckStateChanged += new System.EventHandler(this.isFullAmountEntitlementCheckBox_CheckStateChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(401, 222);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.executiveOverseasTravellingAllowanceDataGridView);
            this.groupBox2.Location = new System.Drawing.Point(8, 252);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(642, 307);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // executiveOverseasTravellingAllowanceDataGridView
            // 
            this.executiveOverseasTravellingAllowanceDataGridView.AllowUserToAddRows = false;
            this.executiveOverseasTravellingAllowanceDataGridView.AllowUserToDeleteRows = false;
            this.executiveOverseasTravellingAllowanceDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.executiveOverseasTravellingAllowanceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.executiveOverseasTravellingAllowanceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.executiveOverseasTravellingAllowanceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.serialColumn,
            this.costItemColumn,
            this.locationGroupColumn,
            this.currencyColumn,
            this.amountColumn});
            this.executiveOverseasTravellingAllowanceDataGridView.EnableHeadersVisualStyles = false;
            this.executiveOverseasTravellingAllowanceDataGridView.Location = new System.Drawing.Point(7, 14);
            this.executiveOverseasTravellingAllowanceDataGridView.MultiSelect = false;
            this.executiveOverseasTravellingAllowanceDataGridView.Name = "executiveOverseasTravellingAllowanceDataGridView";
            this.executiveOverseasTravellingAllowanceDataGridView.ReadOnly = true;
            this.executiveOverseasTravellingAllowanceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.executiveOverseasTravellingAllowanceDataGridView.Size = new System.Drawing.Size(629, 287);
            this.executiveOverseasTravellingAllowanceDataGridView.TabIndex = 0;
            this.executiveOverseasTravellingAllowanceDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.executiveOverseasTravellingAllowanceDataGridView_CellContentClick);
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 60;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Width = 60;
            // 
            // costItemColumn
            // 
            this.costItemColumn.HeaderText = "Cost Item";
            this.costItemColumn.Name = "costItemColumn";
            this.costItemColumn.ReadOnly = true;
            // 
            // locationGroupColumn
            // 
            this.locationGroupColumn.HeaderText = "Location Group";
            this.locationGroupColumn.Name = "locationGroupColumn";
            this.locationGroupColumn.ReadOnly = true;
            this.locationGroupColumn.Width = 150;
            // 
            // currencyColumn
            // 
            this.currencyColumn.HeaderText = "Currency";
            this.currencyColumn.Name = "currencyColumn";
            this.currencyColumn.ReadOnly = true;
            // 
            // amountColumn
            // 
            this.amountColumn.HeaderText = "Amount";
            this.amountColumn.Name = "amountColumn";
            this.amountColumn.ReadOnly = true;
            // 
            // currencyComboBox
            // 
            this.currencyComboBox.FormattingEnabled = true;
            this.currencyComboBox.ItemHeight = 23;
            this.currencyComboBox.Location = new System.Drawing.Point(277, 137);
            this.currencyComboBox.Name = "currencyComboBox";
            this.currencyComboBox.Size = new System.Drawing.Size(199, 29);
            this.currencyComboBox.TabIndex = 5;
            this.currencyComboBox.UseSelectable = true;
            // 
            // costItemComboBox
            // 
            this.costItemComboBox.FormattingEnabled = true;
            this.costItemComboBox.ItemHeight = 23;
            this.costItemComboBox.Location = new System.Drawing.Point(277, 99);
            this.costItemComboBox.Name = "costItemComboBox";
            this.costItemComboBox.Size = new System.Drawing.Size(199, 29);
            this.costItemComboBox.TabIndex = 5;
            this.costItemComboBox.UseSelectable = true;
            // 
            // employeeCategoryComboBox
            // 
            this.employeeCategoryComboBox.FormattingEnabled = true;
            this.employeeCategoryComboBox.ItemHeight = 23;
            this.employeeCategoryComboBox.Location = new System.Drawing.Point(277, 23);
            this.employeeCategoryComboBox.Name = "employeeCategoryComboBox";
            this.employeeCategoryComboBox.Size = new System.Drawing.Size(199, 29);
            this.employeeCategoryComboBox.TabIndex = 5;
            this.employeeCategoryComboBox.UseSelectable = true;
            // 
            // locationGroupComboBox
            // 
            this.locationGroupComboBox.FormattingEnabled = true;
            this.locationGroupComboBox.ItemHeight = 23;
            this.locationGroupComboBox.Location = new System.Drawing.Point(277, 61);
            this.locationGroupComboBox.Name = "locationGroupComboBox";
            this.locationGroupComboBox.Size = new System.Drawing.Size(199, 29);
            this.locationGroupComboBox.TabIndex = 5;
            this.locationGroupComboBox.UseSelectable = true;
            // 
            // entitlementAmountTextBox
            // 
            // 
            // 
            // 
            this.entitlementAmountTextBox.CustomButton.Image = null;
            this.entitlementAmountTextBox.CustomButton.Location = new System.Drawing.Point(177, 1);
            this.entitlementAmountTextBox.CustomButton.Name = "";
            this.entitlementAmountTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.entitlementAmountTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.entitlementAmountTextBox.CustomButton.TabIndex = 1;
            this.entitlementAmountTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.entitlementAmountTextBox.CustomButton.UseSelectable = true;
            this.entitlementAmountTextBox.CustomButton.Visible = false;
            this.entitlementAmountTextBox.Lines = new string[0];
            this.entitlementAmountTextBox.Location = new System.Drawing.Point(277, 193);
            this.entitlementAmountTextBox.MaxLength = 32767;
            this.entitlementAmountTextBox.Name = "entitlementAmountTextBox";
            this.entitlementAmountTextBox.PasswordChar = '\0';
            this.entitlementAmountTextBox.PromptText = "Entitlement amount";
            this.entitlementAmountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.entitlementAmountTextBox.SelectedText = "";
            this.entitlementAmountTextBox.SelectionLength = 0;
            this.entitlementAmountTextBox.SelectionStart = 0;
            this.entitlementAmountTextBox.ShortcutsEnabled = true;
            this.entitlementAmountTextBox.Size = new System.Drawing.Size(199, 23);
            this.entitlementAmountTextBox.TabIndex = 1;
            this.entitlementAmountTextBox.UseSelectable = true;
            this.entitlementAmountTextBox.WaterMark = "Entitlement amount";
            this.entitlementAmountTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.entitlementAmountTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.entitlementAmountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.entitlementAmountTextBox_KeyPress);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(198, 144);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(61, 19);
            this.metroLabel4.TabIndex = 0;
            this.metroLabel4.Text = "Currency";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(194, 107);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(65, 19);
            this.metroLabel3.TabIndex = 0;
            this.metroLabel3.Text = "Cost Item";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(134, 196);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(125, 19);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "Entitlement Amount";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(151, 28);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(108, 19);
            this.metroLabel5.TabIndex = 0;
            this.metroLabel5.Text = "Employee Group";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(161, 66);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(98, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Location group";
            // 
            // executiveOverseasTravellingAllowanceContextMenuStrip
            // 
            this.executiveOverseasTravellingAllowanceContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.executiveOverseasTravellingAllowanceContextMenuStrip.Name = "overseasTravelCostItemContextMenuStrip";
            this.executiveOverseasTravellingAllowanceContextMenuStrip.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // ExecutiveOverseasTravelingAllowanceUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(699, 615);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ExecutiveOverseasTravelingAllowanceUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Executive Overseas Travelling Allowance";
            this.Load += new System.EventHandler(this.OverseasTravelCostItemSettingUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.executiveOverseasTravellingAllowanceDataGridView)).EndInit();
            this.executiveOverseasTravellingAllowanceContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroComboBox locationGroupComboBox;
        private MetroFramework.Controls.MetroTextBox entitlementAmountTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox costItemComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroComboBox currencyComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.ContextMenuStrip executiveOverseasTravellingAllowanceContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private MetroFramework.Controls.MetroComboBox employeeCategoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroCheckBox isFullAmountEntitlementCheckBox;
        private System.Windows.Forms.DataGridView executiveOverseasTravellingAllowanceDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costItemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationGroupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountColumn;
    }
}