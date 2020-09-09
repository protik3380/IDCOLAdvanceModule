namespace IDCOLAdvanceModule.UI._360View
{
    partial class Expense360ViewUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.currentApprovalLevelLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel15 = new MetroFramework.Controls.MetroLabel();
            this.requisitionStatusLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel18 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.viewSupportingFilesButton = new MetroFramework.Controls.MetroButton();
            this.showRequisitionButton = new MetroFramework.Controls.MetroButton();
            this.reimburseOrRefundAmountLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.expenseAmountLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.showDetailsButton = new MetroFramework.Controls.MetroButton();
            this.toDateLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.departmentLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.fromDateLabel = new MetroFramework.Controls.MetroLabel();
            this.designationLabel = new MetroFramework.Controls.MetroLabel();
            this.employeeIdLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.advanceRequisitionAmountLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.expenseNoLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.employeeNameLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.employeeDataGridView = new System.Windows.Forms.DataGridView();
            this.serialEmployeeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expenseTrackingHistoryDataGridView = new System.Windows.Forms.DataGridView();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requisitionDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarksColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expenseTrackingHistoryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.expenseTrackingHistoryDataGridView);
            this.groupBox3.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 348);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1016, 266);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Expense Tracking History";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.employeeDataGridView);
            this.groupBox2.Controls.Add(this.metroLabel4);
            this.groupBox2.Controls.Add(this.currentApprovalLevelLabel);
            this.groupBox2.Controls.Add(this.metroLabel15);
            this.groupBox2.Controls.Add(this.requisitionStatusLabel);
            this.groupBox2.Controls.Add(this.metroLabel18);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(504, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 330);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Approval Information";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(14, 85);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(156, 19);
            this.metroLabel4.TabIndex = 2;
            this.metroLabel4.Text = "Level Approval Members";
            // 
            // currentApprovalLevelLabel
            // 
            this.currentApprovalLevelLabel.AutoSize = true;
            this.currentApprovalLevelLabel.Location = new System.Drawing.Point(157, 52);
            this.currentApprovalLevelLabel.Name = "currentApprovalLevelLabel";
            this.currentApprovalLevelLabel.Size = new System.Drawing.Size(104, 19);
            this.currentApprovalLevelLabel.TabIndex = 0;
            this.currentApprovalLevelLabel.Text = "<Current Level>";
            // 
            // metroLabel15
            // 
            this.metroLabel15.AutoSize = true;
            this.metroLabel15.Location = new System.Drawing.Point(29, 52);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(86, 19);
            this.metroLabel15.TabIndex = 0;
            this.metroLabel15.Text = "Current Level";
            // 
            // requisitionStatusLabel
            // 
            this.requisitionStatusLabel.AutoSize = true;
            this.requisitionStatusLabel.Location = new System.Drawing.Point(157, 24);
            this.requisitionStatusLabel.Name = "requisitionStatusLabel";
            this.requisitionStatusLabel.Size = new System.Drawing.Size(109, 19);
            this.requisitionStatusLabel.TabIndex = 0;
            this.requisitionStatusLabel.Text = "<Current Status>";
            // 
            // metroLabel18
            // 
            this.metroLabel18.AutoSize = true;
            this.metroLabel18.Location = new System.Drawing.Point(24, 24);
            this.metroLabel18.Name = "metroLabel18";
            this.metroLabel18.Size = new System.Drawing.Size(91, 19);
            this.metroLabel18.TabIndex = 0;
            this.metroLabel18.Text = "Current Status";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.viewSupportingFilesButton);
            this.groupBox1.Controls.Add(this.showRequisitionButton);
            this.groupBox1.Controls.Add(this.reimburseOrRefundAmountLabel);
            this.groupBox1.Controls.Add(this.metroLabel10);
            this.groupBox1.Controls.Add(this.expenseAmountLabel);
            this.groupBox1.Controls.Add(this.metroLabel8);
            this.groupBox1.Controls.Add(this.showDetailsButton);
            this.groupBox1.Controls.Add(this.toDateLabel);
            this.groupBox1.Controls.Add(this.metroLabel11);
            this.groupBox1.Controls.Add(this.departmentLabel);
            this.groupBox1.Controls.Add(this.metroLabel7);
            this.groupBox1.Controls.Add(this.fromDateLabel);
            this.groupBox1.Controls.Add(this.designationLabel);
            this.groupBox1.Controls.Add(this.employeeIdLabel);
            this.groupBox1.Controls.Add(this.metroLabel9);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.metroLabel5);
            this.groupBox1.Controls.Add(this.advanceRequisitionAmountLabel);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.expenseNoLabel);
            this.groupBox1.Controls.Add(this.metroLabel6);
            this.groupBox1.Controls.Add(this.employeeNameLabel);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 330);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Requisition Information";
            // 
            // viewSupportingFilesButton
            // 
            this.viewSupportingFilesButton.Location = new System.Drawing.Point(18, 282);
            this.viewSupportingFilesButton.Name = "viewSupportingFilesButton";
            this.viewSupportingFilesButton.Size = new System.Drawing.Size(138, 32);
            this.viewSupportingFilesButton.TabIndex = 7;
            this.viewSupportingFilesButton.Text = "View Supporting File(s)";
            this.viewSupportingFilesButton.UseSelectable = true;
            this.viewSupportingFilesButton.Click += new System.EventHandler(this.viewSupportingFilesButton_Click);
            // 
            // showRequisitionButton
            // 
            this.showRequisitionButton.Location = new System.Drawing.Point(360, 244);
            this.showRequisitionButton.Name = "showRequisitionButton";
            this.showRequisitionButton.Size = new System.Drawing.Size(111, 32);
            this.showRequisitionButton.TabIndex = 6;
            this.showRequisitionButton.Text = "Show Requisition(s)";
            this.showRequisitionButton.UseSelectable = true;
            this.showRequisitionButton.Click += new System.EventHandler(this.showRequisitionButton_Click);
            // 
            // reimburseOrRefundAmountLabel
            // 
            this.reimburseOrRefundAmountLabel.AutoSize = true;
            this.reimburseOrRefundAmountLabel.Location = new System.Drawing.Point(214, 249);
            this.reimburseOrRefundAmountLabel.Name = "reimburseOrRefundAmountLabel";
            this.reimburseOrRefundAmountLabel.Size = new System.Drawing.Size(74, 19);
            this.reimburseOrRefundAmountLabel.TabIndex = 4;
            this.reimburseOrRefundAmountLabel.Text = "<Amount>";
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.Location = new System.Drawing.Point(22, 249);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(176, 19);
            this.metroLabel10.TabIndex = 5;
            this.metroLabel10.Text = "Reimburse/(Refund) Amount";
            // 
            // expenseAmountLabel
            // 
            this.expenseAmountLabel.AutoSize = true;
            this.expenseAmountLabel.Location = new System.Drawing.Point(214, 199);
            this.expenseAmountLabel.Name = "expenseAmountLabel";
            this.expenseAmountLabel.Size = new System.Drawing.Size(74, 19);
            this.expenseAmountLabel.TabIndex = 2;
            this.expenseAmountLabel.Text = "<Amount>";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(91, 199);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(107, 19);
            this.metroLabel8.TabIndex = 3;
            this.metroLabel8.Text = "Expense Amount";
            // 
            // showDetailsButton
            // 
            this.showDetailsButton.Location = new System.Drawing.Point(360, 282);
            this.showDetailsButton.Name = "showDetailsButton";
            this.showDetailsButton.Size = new System.Drawing.Size(111, 32);
            this.showDetailsButton.TabIndex = 1;
            this.showDetailsButton.Text = "Show Details";
            this.showDetailsButton.UseSelectable = true;
            this.showDetailsButton.Click += new System.EventHandler(this.showDetailsButton_Click);
            // 
            // toDateLabel
            // 
            this.toDateLabel.AutoSize = true;
            this.toDateLabel.Location = new System.Drawing.Point(214, 174);
            this.toDateLabel.Name = "toDateLabel";
            this.toDateLabel.Size = new System.Drawing.Size(71, 19);
            this.toDateLabel.TabIndex = 0;
            this.toDateLabel.Text = "<To Date>";
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.Location = new System.Drawing.Point(145, 174);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(53, 19);
            this.metroLabel11.TabIndex = 0;
            this.metroLabel11.Text = "To Date";
            // 
            // departmentLabel
            // 
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Location = new System.Drawing.Point(214, 124);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Size = new System.Drawing.Size(98, 19);
            this.departmentLabel.TabIndex = 0;
            this.departmentLabel.Text = "<Department>";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(118, 124);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(80, 19);
            this.metroLabel7.TabIndex = 0;
            this.metroLabel7.Text = "Department";
            // 
            // fromDateLabel
            // 
            this.fromDateLabel.AutoSize = true;
            this.fromDateLabel.Location = new System.Drawing.Point(214, 149);
            this.fromDateLabel.Name = "fromDateLabel";
            this.fromDateLabel.Size = new System.Drawing.Size(90, 19);
            this.fromDateLabel.TabIndex = 0;
            this.fromDateLabel.Text = "<From Date>";
            // 
            // designationLabel
            // 
            this.designationLabel.AutoSize = true;
            this.designationLabel.Location = new System.Drawing.Point(214, 74);
            this.designationLabel.Name = "designationLabel";
            this.designationLabel.Size = new System.Drawing.Size(95, 19);
            this.designationLabel.TabIndex = 0;
            this.designationLabel.Text = "<Designation>";
            // 
            // employeeIdLabel
            // 
            this.employeeIdLabel.AutoSize = true;
            this.employeeIdLabel.Location = new System.Drawing.Point(214, 99);
            this.employeeIdLabel.Name = "employeeIdLabel";
            this.employeeIdLabel.Size = new System.Drawing.Size(101, 19);
            this.employeeIdLabel.TabIndex = 0;
            this.employeeIdLabel.Text = "<Employee ID>";
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(126, 149);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(72, 19);
            this.metroLabel9.TabIndex = 0;
            this.metroLabel9.Text = "From Date";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(121, 74);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(77, 19);
            this.metroLabel3.TabIndex = 0;
            this.metroLabel3.Text = "Designation";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(115, 99);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(83, 19);
            this.metroLabel5.TabIndex = 0;
            this.metroLabel5.Text = "Employee ID";
            // 
            // advanceRequisitionAmountLabel
            // 
            this.advanceRequisitionAmountLabel.AutoSize = true;
            this.advanceRequisitionAmountLabel.Location = new System.Drawing.Point(214, 224);
            this.advanceRequisitionAmountLabel.Name = "advanceRequisitionAmountLabel";
            this.advanceRequisitionAmountLabel.Size = new System.Drawing.Size(74, 19);
            this.advanceRequisitionAmountLabel.TabIndex = 0;
            this.advanceRequisitionAmountLabel.Text = "<Amount>";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(88, 224);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(110, 19);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "Advance Amount";
            // 
            // expenseNoLabel
            // 
            this.expenseNoLabel.AutoSize = true;
            this.expenseNoLabel.Location = new System.Drawing.Point(214, 24);
            this.expenseNoLabel.Name = "expenseNoLabel";
            this.expenseNoLabel.Size = new System.Drawing.Size(96, 19);
            this.expenseNoLabel.TabIndex = 0;
            this.expenseNoLabel.Text = "<Expense No>";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(120, 24);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(78, 19);
            this.metroLabel6.TabIndex = 0;
            this.metroLabel6.Text = "Expense No";
            // 
            // employeeNameLabel
            // 
            this.employeeNameLabel.AutoSize = true;
            this.employeeNameLabel.Location = new System.Drawing.Point(214, 49);
            this.employeeNameLabel.Name = "employeeNameLabel";
            this.employeeNameLabel.Size = new System.Drawing.Size(125, 19);
            this.employeeNameLabel.TabIndex = 0;
            this.employeeNameLabel.Text = "<Employee Name>";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(91, 49);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(107, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Employee Name";
            // 
            // employeeDataGridView
            // 
            this.employeeDataGridView.AllowUserToAddRows = false;
            this.employeeDataGridView.AllowUserToDeleteRows = false;
            this.employeeDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.employeeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employeeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialEmployeeColumn,
            this.employeeIdColumn,
            this.employeeNameColumn,
            this.designationColumn});
            this.employeeDataGridView.EnableHeadersVisualStyles = false;
            this.employeeDataGridView.Location = new System.Drawing.Point(14, 124);
            this.employeeDataGridView.MultiSelect = false;
            this.employeeDataGridView.Name = "employeeDataGridView";
            this.employeeDataGridView.ReadOnly = true;
            this.employeeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeDataGridView.Size = new System.Drawing.Size(495, 178);
            this.employeeDataGridView.TabIndex = 3;
            // 
            // serialEmployeeColumn
            // 
            this.serialEmployeeColumn.HeaderText = "Serial";
            this.serialEmployeeColumn.Name = "serialEmployeeColumn";
            this.serialEmployeeColumn.ReadOnly = true;
            this.serialEmployeeColumn.Width = 60;
            // 
            // employeeIdColumn
            // 
            this.employeeIdColumn.HeaderText = "Employee ID";
            this.employeeIdColumn.Name = "employeeIdColumn";
            this.employeeIdColumn.ReadOnly = true;
            this.employeeIdColumn.Width = 120;
            // 
            // employeeNameColumn
            // 
            this.employeeNameColumn.HeaderText = "Employee Name";
            this.employeeNameColumn.Name = "employeeNameColumn";
            this.employeeNameColumn.ReadOnly = true;
            this.employeeNameColumn.Width = 150;
            // 
            // designationColumn
            // 
            this.designationColumn.HeaderText = "Designation";
            this.designationColumn.Name = "designationColumn";
            this.designationColumn.ReadOnly = true;
            this.designationColumn.Width = 110;
            // 
            // expenseTrackingHistoryDataGridView
            // 
            this.expenseTrackingHistoryDataGridView.AllowUserToAddRows = false;
            this.expenseTrackingHistoryDataGridView.AllowUserToDeleteRows = false;
            this.expenseTrackingHistoryDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.expenseTrackingHistoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.expenseTrackingHistoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.expenseTrackingHistoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialColumn,
            this.requisitionDescriptionColumn,
            this.remarksColumn});
            this.expenseTrackingHistoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expenseTrackingHistoryDataGridView.EnableHeadersVisualStyles = false;
            this.expenseTrackingHistoryDataGridView.Location = new System.Drawing.Point(3, 19);
            this.expenseTrackingHistoryDataGridView.MultiSelect = false;
            this.expenseTrackingHistoryDataGridView.Name = "expenseTrackingHistoryDataGridView";
            this.expenseTrackingHistoryDataGridView.ReadOnly = true;
            this.expenseTrackingHistoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.expenseTrackingHistoryDataGridView.Size = new System.Drawing.Size(1010, 244);
            this.expenseTrackingHistoryDataGridView.TabIndex = 1;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Width = 70;
            // 
            // requisitionDescriptionColumn
            // 
            this.requisitionDescriptionColumn.HeaderText = "Requisition Description";
            this.requisitionDescriptionColumn.Name = "requisitionDescriptionColumn";
            this.requisitionDescriptionColumn.ReadOnly = true;
            this.requisitionDescriptionColumn.Width = 630;
            // 
            // remarksColumn
            // 
            this.remarksColumn.HeaderText = "Remarks";
            this.remarksColumn.Name = "remarksColumn";
            this.remarksColumn.ReadOnly = true;
            this.remarksColumn.Width = 250;
            // 
            // Expense360ViewUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1039, 626);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Expense360ViewUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expense 360 View";
            this.Load += new System.EventHandler(this.Expense360ViewUI_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expenseTrackingHistoryDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroLabel currentApprovalLevelLabel;
        private MetroFramework.Controls.MetroLabel metroLabel15;
        private MetroFramework.Controls.MetroLabel requisitionStatusLabel;
        private MetroFramework.Controls.MetroLabel metroLabel18;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton showDetailsButton;
        private MetroFramework.Controls.MetroLabel toDateLabel;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroLabel departmentLabel;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel fromDateLabel;
        private MetroFramework.Controls.MetroLabel designationLabel;
        private MetroFramework.Controls.MetroLabel employeeIdLabel;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel advanceRequisitionAmountLabel;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel employeeNameLabel;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel reimburseOrRefundAmountLabel;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroLabel expenseAmountLabel;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroButton showRequisitionButton;
        private MetroFramework.Controls.MetroButton viewSupportingFilesButton;
        private MetroFramework.Controls.MetroLabel expenseNoLabel;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.DataGridView employeeDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialEmployeeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationColumn;
        private System.Windows.Forms.DataGridView expenseTrackingHistoryDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn requisitionDescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarksColumn;
    }
}