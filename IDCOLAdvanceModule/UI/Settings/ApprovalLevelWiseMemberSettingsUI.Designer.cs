namespace IDCOLAdvanceModule.UI.Settings
{
    partial class ApprovalLevelWiseMemberSettingsUI
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
            this.employeeDataGridView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.employeeIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.approvalLevelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.approvalPanelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.designationComboBox = new MetroFramework.Controls.MetroComboBox();
            this.designationCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.selectEmployeeDataGridView = new System.Windows.Forms.DataGridView();
            this.downButton = new MetroFramework.Controls.MetroButton();
            this.upButton = new MetroFramework.Controls.MetroButton();
            this.selectedMemberContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.selectEmployeeIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectEmployeeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectDesignationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectPriorityOrderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectEmployeeDataGridView)).BeginInit();
            this.selectedMemberContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.employeeDataGridView);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.approvalLevelComboBox);
            this.groupBox1.Controls.Add(this.approvalPanelComboBox);
            this.groupBox1.Controls.Add(this.designationComboBox);
            this.groupBox1.Controls.Add(this.designationCheckBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 444);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Approval Level Wise Member Settings";
            // 
            // employeeDataGridView
            // 
            this.employeeDataGridView.AllowUserToAddRows = false;
            this.employeeDataGridView.AllowUserToDeleteRows = false;
            this.employeeDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.employeeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employeeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.employeeIdColumn,
            this.employeeNameColumn});
            this.employeeDataGridView.EnableHeadersVisualStyles = false;
            this.employeeDataGridView.Location = new System.Drawing.Point(6, 133);
            this.employeeDataGridView.MultiSelect = false;
            this.employeeDataGridView.Name = "employeeDataGridView";
            this.employeeDataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.employeeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeDataGridView.Size = new System.Drawing.Size(433, 269);
            this.employeeDataGridView.TabIndex = 7;
            this.employeeDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.employeeDataGridView_CellContentClick);
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Width = 20;
            // 
            // employeeIdColumn
            // 
            this.employeeIdColumn.HeaderText = "Employee ID";
            this.employeeIdColumn.Name = "employeeIdColumn";
            this.employeeIdColumn.ReadOnly = true;
            this.employeeIdColumn.Width = 150;
            // 
            // employeeNameColumn
            // 
            this.employeeNameColumn.HeaderText = "Employee Name";
            this.employeeNameColumn.Name = "employeeNameColumn";
            this.employeeNameColumn.ReadOnly = true;
            this.employeeNameColumn.Width = 200;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(357, 408);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // approvalLevelComboBox
            // 
            this.approvalLevelComboBox.FormattingEnabled = true;
            this.approvalLevelComboBox.ItemHeight = 23;
            this.approvalLevelComboBox.Location = new System.Drawing.Point(168, 63);
            this.approvalLevelComboBox.Name = "approvalLevelComboBox";
            this.approvalLevelComboBox.Size = new System.Drawing.Size(250, 29);
            this.approvalLevelComboBox.TabIndex = 1;
            this.approvalLevelComboBox.UseSelectable = true;
            this.approvalLevelComboBox.SelectionChangeCommitted += new System.EventHandler(this.approvalLevelComboBox_SelectionChangeCommitted);
            // 
            // approvalPanelComboBox
            // 
            this.approvalPanelComboBox.FormattingEnabled = true;
            this.approvalPanelComboBox.ItemHeight = 23;
            this.approvalPanelComboBox.Location = new System.Drawing.Point(168, 28);
            this.approvalPanelComboBox.Name = "approvalPanelComboBox";
            this.approvalPanelComboBox.Size = new System.Drawing.Size(250, 29);
            this.approvalPanelComboBox.TabIndex = 0;
            this.approvalPanelComboBox.UseSelectable = true;
            this.approvalPanelComboBox.SelectionChangeCommitted += new System.EventHandler(this.approvalPanelComboBox_SelectionChangeCommitted);
            // 
            // designationComboBox
            // 
            this.designationComboBox.FormattingEnabled = true;
            this.designationComboBox.ItemHeight = 23;
            this.designationComboBox.Location = new System.Drawing.Point(168, 98);
            this.designationComboBox.Name = "designationComboBox";
            this.designationComboBox.Size = new System.Drawing.Size(250, 29);
            this.designationComboBox.TabIndex = 2;
            this.designationComboBox.UseSelectable = true;
            this.designationComboBox.SelectionChangeCommitted += new System.EventHandler(this.designationComboBox_SelectionChangeCommitted);
            // 
            // designationCheckBox
            // 
            this.designationCheckBox.AutoSize = true;
            this.designationCheckBox.Location = new System.Drawing.Point(52, 104);
            this.designationCheckBox.Name = "designationCheckBox";
            this.designationCheckBox.Size = new System.Drawing.Size(86, 15);
            this.designationCheckBox.TabIndex = 6;
            this.designationCheckBox.Text = "Designation";
            this.designationCheckBox.UseSelectable = true;
            this.designationCheckBox.CheckedChanged += new System.EventHandler(this.designationCheckBox_CheckedChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(100, 66);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(38, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Level";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(98, 32);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(40, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Panel";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.selectEmployeeDataGridView);
            this.groupBox2.Controls.Add(this.downButton);
            this.groupBox2.Controls.Add(this.upButton);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(464, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(635, 443);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Members";
            // 
            // selectEmployeeDataGridView
            // 
            this.selectEmployeeDataGridView.AllowUserToAddRows = false;
            this.selectEmployeeDataGridView.AllowUserToDeleteRows = false;
            this.selectEmployeeDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.selectEmployeeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.selectEmployeeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectEmployeeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.selectEmployeeIdColumn,
            this.selectEmployeeNameColumn,
            this.selectDesignationColumn,
            this.selectPriorityOrderColumn});
            this.selectEmployeeDataGridView.EnableHeadersVisualStyles = false;
            this.selectEmployeeDataGridView.Location = new System.Drawing.Point(6, 22);
            this.selectEmployeeDataGridView.MultiSelect = false;
            this.selectEmployeeDataGridView.Name = "selectEmployeeDataGridView";
            this.selectEmployeeDataGridView.ReadOnly = true;
            this.selectEmployeeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.selectEmployeeDataGridView.Size = new System.Drawing.Size(571, 421);
            this.selectEmployeeDataGridView.TabIndex = 8;
            this.selectEmployeeDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectEmployeeDataGridView_CellContentClick);
            // 
            // downButton
            // 
            this.downButton.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.downButton.Location = new System.Drawing.Point(583, 238);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(45, 44);
            this.downButton.TabIndex = 5;
            this.downButton.Text = "▼";
            this.downButton.UseSelectable = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.upButton.Location = new System.Drawing.Point(583, 188);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(45, 44);
            this.upButton.TabIndex = 6;
            this.upButton.Text = "▲";
            this.upButton.UseSelectable = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // selectedMemberContextMenu
            // 
            this.selectedMemberContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.selectedMemberContextMenu.Name = "selectedMemberContextMenu";
            this.selectedMemberContextMenu.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 70;
            // 
            // selectEmployeeIdColumn
            // 
            this.selectEmployeeIdColumn.HeaderText = "Employee ID";
            this.selectEmployeeIdColumn.Name = "selectEmployeeIdColumn";
            this.selectEmployeeIdColumn.ReadOnly = true;
            this.selectEmployeeIdColumn.Width = 110;
            // 
            // selectEmployeeNameColumn
            // 
            this.selectEmployeeNameColumn.HeaderText = "Employee Name";
            this.selectEmployeeNameColumn.Name = "selectEmployeeNameColumn";
            this.selectEmployeeNameColumn.ReadOnly = true;
            this.selectEmployeeNameColumn.Width = 180;
            // 
            // selectDesignationColumn
            // 
            this.selectDesignationColumn.HeaderText = "Designation";
            this.selectDesignationColumn.Name = "selectDesignationColumn";
            this.selectDesignationColumn.ReadOnly = true;
            // 
            // selectPriorityOrderColumn
            // 
            this.selectPriorityOrderColumn.HeaderText = "Priority Order";
            this.selectPriorityOrderColumn.Name = "selectPriorityOrderColumn";
            this.selectPriorityOrderColumn.ReadOnly = true;
            this.selectPriorityOrderColumn.Width = 80;
            // 
            // ApprovalLevelWiseMemberSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1111, 476);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ApprovalLevelWiseMemberSettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Approval Level Wise Member Settings";
            this.Load += new System.EventHandler(this.ApprovalLevelWiseMemberSettingsUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selectEmployeeDataGridView)).EndInit();
            this.selectedMemberContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroComboBox designationComboBox;
        private MetroFramework.Controls.MetroCheckBox designationCheckBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroComboBox approvalLevelComboBox;
        private MetroFramework.Controls.MetroComboBox approvalPanelComboBox;
        private MetroFramework.Controls.MetroButton saveButton;
        private MetroFramework.Controls.MetroContextMenu selectedMemberContextMenu;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private MetroFramework.Controls.MetroButton downButton;
        private MetroFramework.Controls.MetroButton upButton;
        private System.Windows.Forms.DataGridView employeeDataGridView;
        private System.Windows.Forms.DataGridView selectEmployeeDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectEmployeeIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectEmployeeNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectDesignationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn selectPriorityOrderColumn;
    }
}