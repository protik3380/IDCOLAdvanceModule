namespace IDCOLAdvanceModule.UI
{
    partial class SelectRequisitionForExpenseEntryUI
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
            this.searchButton = new MetroFramework.Controls.MetroButton();
            this.toDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.fromDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.advanceRequisitionCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.expenseEntryButton = new MetroFramework.Controls.MetroButton();
            this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.expenseEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.approvedRequisitionDataGridView = new System.Windows.Forms.DataGridView();
            this.expenseEntryColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.requisitionNoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appliedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advanceAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.metroContextMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedRequisitionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchButton);
            this.groupBox1.Controls.Add(this.toDateTimePicker);
            this.groupBox1.Controls.Add(this.fromDateTimePicker);
            this.groupBox1.Controls.Add(this.metroLabel6);
            this.groupBox1.Controls.Add(this.metroLabel5);
            this.groupBox1.Controls.Add(this.advanceRequisitionCategoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel7);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(255, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 193);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Requisition for Adjustment/Reimbursement Entry";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(340, 159);
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
            this.toDateTimePicker.Location = new System.Drawing.Point(164, 78);
            this.toDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.ShowCheckBox = true;
            this.toDateTimePicker.Size = new System.Drawing.Size(251, 29);
            this.toDateTimePicker.TabIndex = 30;
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.Checked = false;
            this.fromDateTimePicker.Location = new System.Drawing.Point(164, 43);
            this.fromDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.ShowCheckBox = true;
            this.fromDateTimePicker.Size = new System.Drawing.Size(251, 29);
            this.fromDateTimePicker.TabIndex = 29;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(115, 78);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(22, 19);
            this.metroLabel6.TabIndex = 32;
            this.metroLabel6.Text = "To";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(96, 43);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(41, 19);
            this.metroLabel5.TabIndex = 31;
            this.metroLabel5.Text = "From";
            // 
            // advanceRequisitionCategoryComboBox
            // 
            this.advanceRequisitionCategoryComboBox.FormattingEnabled = true;
            this.advanceRequisitionCategoryComboBox.ItemHeight = 23;
            this.advanceRequisitionCategoryComboBox.Location = new System.Drawing.Point(164, 113);
            this.advanceRequisitionCategoryComboBox.Name = "advanceRequisitionCategoryComboBox";
            this.advanceRequisitionCategoryComboBox.Size = new System.Drawing.Size(251, 29);
            this.advanceRequisitionCategoryComboBox.TabIndex = 27;
            this.advanceRequisitionCategoryComboBox.UseSelectable = true;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(72, 113);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(64, 19);
            this.metroLabel7.TabIndex = 28;
            this.metroLabel7.Text = "Category";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.approvedRequisitionDataGridView);
            this.groupBox2.Controls.Add(this.expenseEntryButton);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(996, 280);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Approved Requisition list";
            // 
            // expenseEntryButton
            // 
            this.expenseEntryButton.Location = new System.Drawing.Point(862, 237);
            this.expenseEntryButton.Name = "expenseEntryButton";
            this.expenseEntryButton.Size = new System.Drawing.Size(120, 23);
            this.expenseEntryButton.TabIndex = 2;
            this.expenseEntryButton.Text = "Adjustment Entry";
            this.expenseEntryButton.UseSelectable = true;
            this.expenseEntryButton.Click += new System.EventHandler(this.expenseEntryButton_Click);
            // 
            // metroContextMenu1
            // 
            this.metroContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expenseEntryToolStripMenuItem});
            this.metroContextMenu1.Name = "metroContextMenu1";
            this.metroContextMenu1.Size = new System.Drawing.Size(147, 26);
            // 
            // expenseEntryToolStripMenuItem
            // 
            this.expenseEntryToolStripMenuItem.Name = "expenseEntryToolStripMenuItem";
            this.expenseEntryToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.expenseEntryToolStripMenuItem.Text = "Expense Entry";
            this.expenseEntryToolStripMenuItem.Click += new System.EventHandler(this.expenseEntryToolStripMenuItem_Click);
            // 
            // approvedRequisitionDataGridView
            // 
            this.approvedRequisitionDataGridView.AllowUserToAddRows = false;
            this.approvedRequisitionDataGridView.AllowUserToDeleteRows = false;
            this.approvedRequisitionDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.approvedRequisitionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.approvedRequisitionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.approvedRequisitionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.expenseEntryColumn,
            this.checkColumn,
            this.requisitionNoColumn,
            this.employeeNameColumn,
            this.categoryColumn,
            this.appliedDateColumn,
            this.fromDateColumn,
            this.toDateColumn,
            this.advanceAmountColumn});
            this.approvedRequisitionDataGridView.EnableHeadersVisualStyles = false;
            this.approvedRequisitionDataGridView.Location = new System.Drawing.Point(13, 23);
            this.approvedRequisitionDataGridView.MultiSelect = false;
            this.approvedRequisitionDataGridView.Name = "approvedRequisitionDataGridView";
            this.approvedRequisitionDataGridView.ReadOnly = true;
            this.approvedRequisitionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.approvedRequisitionDataGridView.Size = new System.Drawing.Size(971, 207);
            this.approvedRequisitionDataGridView.TabIndex = 3;
            this.approvedRequisitionDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.approvedRequisitionDataGridView_CellContentClick);
            // 
            // expenseEntryColumn
            // 
            this.expenseEntryColumn.HeaderText = "";
            this.expenseEntryColumn.Name = "expenseEntryColumn";
            this.expenseEntryColumn.ReadOnly = true;
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkColumn.Width = 20;
            // 
            // requisitionNoColumn
            // 
            this.requisitionNoColumn.HeaderText = "Requisition No.";
            this.requisitionNoColumn.Name = "requisitionNoColumn";
            this.requisitionNoColumn.ReadOnly = true;
            // 
            // employeeNameColumn
            // 
            this.employeeNameColumn.HeaderText = "Employee Name";
            this.employeeNameColumn.Name = "employeeNameColumn";
            this.employeeNameColumn.ReadOnly = true;
            this.employeeNameColumn.Width = 150;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Category";
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            // 
            // appliedDateColumn
            // 
            this.appliedDateColumn.HeaderText = "Applied Date";
            this.appliedDateColumn.Name = "appliedDateColumn";
            this.appliedDateColumn.ReadOnly = true;
            // 
            // fromDateColumn
            // 
            this.fromDateColumn.HeaderText = "From Date";
            this.fromDateColumn.Name = "fromDateColumn";
            this.fromDateColumn.ReadOnly = true;
            // 
            // toDateColumn
            // 
            this.toDateColumn.HeaderText = "To Date";
            this.toDateColumn.Name = "toDateColumn";
            this.toDateColumn.ReadOnly = true;
            // 
            // advanceAmountColumn
            // 
            this.advanceAmountColumn.HeaderText = "Advance amount";
            this.advanceAmountColumn.Name = "advanceAmountColumn";
            this.advanceAmountColumn.ReadOnly = true;
            this.advanceAmountColumn.Width = 150;
            // 
            // SelectRequisitionForExpenseEntryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1020, 506);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectRequisitionForExpenseEntryUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Requisition For Adjustment/Reimbursement Entry Form";
            this.Load += new System.EventHandler(this.SelectRequisitionForExpenseEntryUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.metroContextMenu1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.approvedRequisitionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton searchButton;
        private MetroFramework.Controls.MetroDateTime toDateTimePicker;
        private MetroFramework.Controls.MetroDateTime fromDateTimePicker;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox advanceRequisitionCategoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem expenseEntryToolStripMenuItem;
        private MetroFramework.Controls.MetroButton expenseEntryButton;
        private System.Windows.Forms.DataGridView approvedRequisitionDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn expenseEntryColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn requisitionNoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn appliedDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn advanceAmountColumn;


    }
}