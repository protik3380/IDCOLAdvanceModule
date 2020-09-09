namespace IDCOLAdvanceModule.UI
{
    partial class ExpenseSearchUI
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
            this.expenseSearchContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.view360ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchButton = new MetroFramework.Controls.MetroButton();
            this.toDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.fromDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.advanceRequisitionCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.remarksTextBox = new MetroFramework.Controls.MetroTextBox();
            this.currencyComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.employeeTextBox = new MetroFramework.Controls.MetroTextBox();
            this.selectEmployeeButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.designationComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.departmentComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.expenseSearchDataGridView = new System.Windows.Forms.DataGridView();
            this.view360Column = new System.Windows.Forms.DataGridViewButtonColumn();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.viewColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.moveColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expenseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entrydateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expenseAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advanceAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reimburseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expenseSearchContextMenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expenseSearchDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // expenseSearchContextMenuStrip
            // 
            this.expenseSearchContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.view360ToolStripMenuItem,
            this.moveToolStripMenuItem});
            this.expenseSearchContextMenuStrip.Name = "expenseSearchContextMenuStrip";
            this.expenseSearchContextMenuStrip.Size = new System.Drawing.Size(121, 92);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // view360ToolStripMenuItem
            // 
            this.view360ToolStripMenuItem.Name = "view360ToolStripMenuItem";
            this.view360ToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.view360ToolStripMenuItem.Text = "360 View";
            this.view360ToolStripMenuItem.Click += new System.EventHandler(this.view360ToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.moveToolStripMenuItem.Text = "Move";
            this.moveToolStripMenuItem.Visible = false;
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.expenseSearchDataGridView);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 361);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(996, 280);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Adjustment/Reimbursement List";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(561, 307);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 33;
            this.searchButton.Text = "Search";
            this.searchButton.UseSelectable = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.Checked = false;
            this.toDateTimePicker.Location = new System.Drawing.Point(385, 237);
            this.toDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.ShowCheckBox = true;
            this.toDateTimePicker.Size = new System.Drawing.Size(251, 29);
            this.toDateTimePicker.TabIndex = 30;
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.Checked = false;
            this.fromDateTimePicker.Location = new System.Drawing.Point(385, 202);
            this.fromDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.ShowCheckBox = true;
            this.fromDateTimePicker.Size = new System.Drawing.Size(251, 29);
            this.fromDateTimePicker.TabIndex = 29;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(336, 239);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(22, 19);
            this.metroLabel6.TabIndex = 32;
            this.metroLabel6.Text = "To";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(317, 204);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(41, 19);
            this.metroLabel5.TabIndex = 31;
            this.metroLabel5.Text = "From";
            // 
            // advanceRequisitionCategoryComboBox
            // 
            this.advanceRequisitionCategoryComboBox.FormattingEnabled = true;
            this.advanceRequisitionCategoryComboBox.ItemHeight = 23;
            this.advanceRequisitionCategoryComboBox.Location = new System.Drawing.Point(385, 33);
            this.advanceRequisitionCategoryComboBox.Name = "advanceRequisitionCategoryComboBox";
            this.advanceRequisitionCategoryComboBox.Size = new System.Drawing.Size(251, 29);
            this.advanceRequisitionCategoryComboBox.TabIndex = 27;
            this.advanceRequisitionCategoryComboBox.UseSelectable = true;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(294, 33);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(64, 19);
            this.metroLabel7.TabIndex = 28;
            this.metroLabel7.Text = "Category";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.metroLabel8);
            this.groupBox1.Controls.Add(this.remarksTextBox);
            this.groupBox1.Controls.Add(this.currencyComboBox);
            this.groupBox1.Controls.Add(this.metroLabel4);
            this.groupBox1.Controls.Add(this.employeeTextBox);
            this.groupBox1.Controls.Add(this.selectEmployeeButton);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.designationComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.departmentComboBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.searchButton);
            this.groupBox1.Controls.Add(this.toDateTimePicker);
            this.groupBox1.Controls.Add(this.fromDateTimePicker);
            this.groupBox1.Controls.Add(this.metroLabel6);
            this.groupBox1.Controls.Add(this.metroLabel5);
            this.groupBox1.Controls.Add(this.advanceRequisitionCategoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel7);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(996, 347);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria for Adjustment/Reimbursement";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(299, 272);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(59, 19);
            this.metroLabel8.TabIndex = 44;
            this.metroLabel8.Text = "Remarks";
            // 
            // remarksTextBox
            // 
            // 
            // 
            // 
            this.remarksTextBox.CustomButton.Image = null;
            this.remarksTextBox.CustomButton.Location = new System.Drawing.Point(229, 1);
            this.remarksTextBox.CustomButton.Name = "";
            this.remarksTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.remarksTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.remarksTextBox.CustomButton.TabIndex = 1;
            this.remarksTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.remarksTextBox.CustomButton.UseSelectable = true;
            this.remarksTextBox.CustomButton.Visible = false;
            this.remarksTextBox.Lines = new string[0];
            this.remarksTextBox.Location = new System.Drawing.Point(385, 272);
            this.remarksTextBox.MaxLength = 32767;
            this.remarksTextBox.Name = "remarksTextBox";
            this.remarksTextBox.PasswordChar = '\0';
            this.remarksTextBox.PromptText = "Remarks";
            this.remarksTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.remarksTextBox.SelectedText = "";
            this.remarksTextBox.SelectionLength = 0;
            this.remarksTextBox.SelectionStart = 0;
            this.remarksTextBox.ShortcutsEnabled = true;
            this.remarksTextBox.Size = new System.Drawing.Size(251, 23);
            this.remarksTextBox.TabIndex = 43;
            this.remarksTextBox.UseSelectable = true;
            this.remarksTextBox.WaterMark = "Remarks";
            this.remarksTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.remarksTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // currencyComboBox
            // 
            this.currencyComboBox.FormattingEnabled = true;
            this.currencyComboBox.ItemHeight = 23;
            this.currencyComboBox.Location = new System.Drawing.Point(385, 167);
            this.currencyComboBox.Name = "currencyComboBox";
            this.currencyComboBox.Size = new System.Drawing.Size(251, 29);
            this.currencyComboBox.TabIndex = 41;
            this.currencyComboBox.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(297, 167);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(61, 19);
            this.metroLabel4.TabIndex = 42;
            this.metroLabel4.Text = "Currency";
            // 
            // employeeTextBox
            // 
            // 
            // 
            // 
            this.employeeTextBox.CustomButton.Image = null;
            this.employeeTextBox.CustomButton.Location = new System.Drawing.Point(229, 1);
            this.employeeTextBox.CustomButton.Name = "";
            this.employeeTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.employeeTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.employeeTextBox.CustomButton.TabIndex = 1;
            this.employeeTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.employeeTextBox.CustomButton.UseSelectable = true;
            this.employeeTextBox.CustomButton.Visible = false;
            this.employeeTextBox.Lines = new string[0];
            this.employeeTextBox.Location = new System.Drawing.Point(385, 138);
            this.employeeTextBox.MaxLength = 32767;
            this.employeeTextBox.Name = "employeeTextBox";
            this.employeeTextBox.PasswordChar = '\0';
            this.employeeTextBox.PromptText = "Employee";
            this.employeeTextBox.ReadOnly = true;
            this.employeeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.employeeTextBox.SelectedText = "";
            this.employeeTextBox.SelectionLength = 0;
            this.employeeTextBox.SelectionStart = 0;
            this.employeeTextBox.ShortcutsEnabled = true;
            this.employeeTextBox.Size = new System.Drawing.Size(251, 23);
            this.employeeTextBox.TabIndex = 38;
            this.employeeTextBox.UseSelectable = true;
            this.employeeTextBox.WaterMark = "Employee";
            this.employeeTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.employeeTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // selectEmployeeButton
            // 
            this.selectEmployeeButton.Location = new System.Drawing.Point(652, 138);
            this.selectEmployeeButton.Name = "selectEmployeeButton";
            this.selectEmployeeButton.Size = new System.Drawing.Size(34, 23);
            this.selectEmployeeButton.TabIndex = 39;
            this.selectEmployeeButton.Text = "...";
            this.selectEmployeeButton.UseSelectable = true;
            this.selectEmployeeButton.Click += new System.EventHandler(this.selectEmployeeButton_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(291, 138);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(67, 19);
            this.metroLabel3.TabIndex = 40;
            this.metroLabel3.Text = "Employee";
            // 
            // designationComboBox
            // 
            this.designationComboBox.FormattingEnabled = true;
            this.designationComboBox.ItemHeight = 23;
            this.designationComboBox.Location = new System.Drawing.Point(385, 103);
            this.designationComboBox.Name = "designationComboBox";
            this.designationComboBox.Size = new System.Drawing.Size(251, 29);
            this.designationComboBox.TabIndex = 35;
            this.designationComboBox.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(281, 107);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(77, 19);
            this.metroLabel2.TabIndex = 37;
            this.metroLabel2.Text = "Designation";
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.ItemHeight = 23;
            this.departmentComboBox.Location = new System.Drawing.Point(385, 68);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(251, 29);
            this.departmentComboBox.TabIndex = 34;
            this.departmentComboBox.UseSelectable = true;
            this.departmentComboBox.SelectionChangeCommitted += new System.EventHandler(this.departmentComboBox_SelectionChangeCommitted);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(278, 72);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(80, 19);
            this.metroLabel1.TabIndex = 36;
            this.metroLabel1.Text = "Department";
            // 
            // expenseSearchDataGridView
            // 
            this.expenseSearchDataGridView.AllowUserToAddRows = false;
            this.expenseSearchDataGridView.AllowUserToDeleteRows = false;
            this.expenseSearchDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.expenseSearchDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.expenseSearchDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.expenseSearchDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.view360Column,
            this.editColumn,
            this.viewColumn,
            this.moveColumn,
            this.serialColumn,
            this.expenseColumn,
            this.employeeColumn,
            this.categoryColumn,
            this.entrydateColumn,
            this.expenseAmountColumn,
            this.advanceAmountColumn,
            this.reimburseColumn});
            this.expenseSearchDataGridView.EnableHeadersVisualStyles = false;
            this.expenseSearchDataGridView.Location = new System.Drawing.Point(6, 22);
            this.expenseSearchDataGridView.MultiSelect = false;
            this.expenseSearchDataGridView.Name = "expenseSearchDataGridView";
            this.expenseSearchDataGridView.ReadOnly = true;
            this.expenseSearchDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.expenseSearchDataGridView.Size = new System.Drawing.Size(984, 239);
            this.expenseSearchDataGridView.TabIndex = 1;
            this.expenseSearchDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.expenseSearchDataGridView_CellContentClick);
            // 
            // view360Column
            // 
            this.view360Column.HeaderText = "";
            this.view360Column.Name = "view360Column";
            this.view360Column.ReadOnly = true;
            this.view360Column.Width = 90;
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 60;
            // 
            // viewColumn
            // 
            this.viewColumn.HeaderText = "";
            this.viewColumn.Name = "viewColumn";
            this.viewColumn.ReadOnly = true;
            this.viewColumn.Width = 60;
            // 
            // moveColumn
            // 
            this.moveColumn.HeaderText = "";
            this.moveColumn.Name = "moveColumn";
            this.moveColumn.ReadOnly = true;
            this.moveColumn.Width = 60;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.serialColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.serialColumn.Width = 60;
            // 
            // expenseColumn
            // 
            this.expenseColumn.HeaderText = "Expense No.";
            this.expenseColumn.Name = "expenseColumn";
            this.expenseColumn.ReadOnly = true;
            this.expenseColumn.Width = 120;
            // 
            // employeeColumn
            // 
            this.employeeColumn.HeaderText = "Employee Name";
            this.employeeColumn.Name = "employeeColumn";
            this.employeeColumn.ReadOnly = true;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Category";
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            // 
            // entrydateColumn
            // 
            this.entrydateColumn.HeaderText = "Expense Entry Date";
            this.entrydateColumn.Name = "entrydateColumn";
            this.entrydateColumn.ReadOnly = true;
            // 
            // expenseAmountColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.expenseAmountColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.expenseAmountColumn.HeaderText = "Expense Amount";
            this.expenseAmountColumn.Name = "expenseAmountColumn";
            this.expenseAmountColumn.ReadOnly = true;
            // 
            // advanceAmountColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.advanceAmountColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.advanceAmountColumn.HeaderText = "Advance Amount";
            this.advanceAmountColumn.Name = "advanceAmountColumn";
            this.advanceAmountColumn.ReadOnly = true;
            // 
            // reimburseColumn
            // 
            this.reimburseColumn.HeaderText = "Reimburse/(Refund)";
            this.reimburseColumn.Name = "reimburseColumn";
            this.reimburseColumn.ReadOnly = true;
            // 
            // ExpenseSearchUI
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1027, 651);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExpenseSearchUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Adjustment/Reimbursement";
            this.Load += new System.EventHandler(this.SearchRequisitionExpenseEntryUI_Load);
            this.expenseSearchContextMenuStrip.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expenseSearchDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroButton searchButton;
        private MetroFramework.Controls.MetroDateTime toDateTimePicker;
        private MetroFramework.Controls.MetroDateTime fromDateTimePicker;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox advanceRequisitionCategoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip expenseSearchContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem view360ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private MetroFramework.Controls.MetroComboBox designationComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox departmentComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox employeeTextBox;
        private MetroFramework.Controls.MetroButton selectEmployeeButton;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox currencyComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroTextBox remarksTextBox;
        private System.Windows.Forms.DataGridView expenseSearchDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn view360Column;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewButtonColumn viewColumn;
        private System.Windows.Forms.DataGridViewButtonColumn moveColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expenseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn entrydateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expenseAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn advanceAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reimburseColumn;
    }
}