namespace IDCOLAdvanceModule.UI.Settings
{
    partial class HeadOfDepartmentSetupUI
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
            this.headOfDepartmentSetupGroupBox = new System.Windows.Forms.GroupBox();
            this.headOfDepaertmentDataGridView = new System.Windows.Forms.DataGridView();
            this.resetButton = new MetroFramework.Controls.MetroButton();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.selectEmployeeButton = new MetroFramework.Controls.MetroButton();
            this.employeeNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.departmentComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.headOfDepartmentContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.departmentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headOfDepartmentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headOfDepartmentSetupGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headOfDepaertmentDataGridView)).BeginInit();
            this.headOfDepartmentContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // headOfDepartmentSetupGroupBox
            // 
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.headOfDepaertmentDataGridView);
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.resetButton);
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.saveButton);
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.selectEmployeeButton);
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.employeeNameTextBox);
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.metroLabel2);
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.departmentComboBox);
            this.headOfDepartmentSetupGroupBox.Controls.Add(this.metroLabel1);
            this.headOfDepartmentSetupGroupBox.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headOfDepartmentSetupGroupBox.Location = new System.Drawing.Point(12, 12);
            this.headOfDepartmentSetupGroupBox.Name = "headOfDepartmentSetupGroupBox";
            this.headOfDepartmentSetupGroupBox.Size = new System.Drawing.Size(633, 436);
            this.headOfDepartmentSetupGroupBox.TabIndex = 0;
            this.headOfDepartmentSetupGroupBox.TabStop = false;
            this.headOfDepartmentSetupGroupBox.Text = "Head of Department Setup";
            // 
            // headOfDepaertmentDataGridView
            // 
            this.headOfDepaertmentDataGridView.AllowUserToAddRows = false;
            this.headOfDepaertmentDataGridView.AllowUserToDeleteRows = false;
            this.headOfDepaertmentDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.headOfDepaertmentDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.headOfDepaertmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.headOfDepaertmentDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.departmentColumn,
            this.headOfDepartmentColumn});
            this.headOfDepaertmentDataGridView.EnableHeadersVisualStyles = false;
            this.headOfDepaertmentDataGridView.Location = new System.Drawing.Point(7, 139);
            this.headOfDepaertmentDataGridView.MultiSelect = false;
            this.headOfDepaertmentDataGridView.Name = "headOfDepaertmentDataGridView";
            this.headOfDepaertmentDataGridView.ReadOnly = true;
            this.headOfDepaertmentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.headOfDepaertmentDataGridView.Size = new System.Drawing.Size(620, 291);
            this.headOfDepaertmentDataGridView.TabIndex = 8;
            this.headOfDepaertmentDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.headOfDepaertmentDataGridView_CellContentClick);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(278, 103);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Reset";
            this.resetButton.UseSelectable = true;
            this.resetButton.Visible = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(362, 103);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // selectEmployeeButton
            // 
            this.selectEmployeeButton.Location = new System.Drawing.Point(453, 74);
            this.selectEmployeeButton.Name = "selectEmployeeButton";
            this.selectEmployeeButton.Size = new System.Drawing.Size(34, 23);
            this.selectEmployeeButton.TabIndex = 1;
            this.selectEmployeeButton.Text = "...";
            this.selectEmployeeButton.UseSelectable = true;
            this.selectEmployeeButton.Click += new System.EventHandler(this.selectEmployeeButton_Click);
            // 
            // employeeNameTextBox
            // 
            // 
            // 
            // 
            this.employeeNameTextBox.CustomButton.Image = null;
            this.employeeNameTextBox.CustomButton.Location = new System.Drawing.Point(163, 1);
            this.employeeNameTextBox.CustomButton.Name = "";
            this.employeeNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.employeeNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.employeeNameTextBox.CustomButton.TabIndex = 1;
            this.employeeNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.employeeNameTextBox.CustomButton.UseSelectable = true;
            this.employeeNameTextBox.CustomButton.Visible = false;
            this.employeeNameTextBox.Lines = new string[0];
            this.employeeNameTextBox.Location = new System.Drawing.Point(252, 74);
            this.employeeNameTextBox.MaxLength = 32767;
            this.employeeNameTextBox.Name = "employeeNameTextBox";
            this.employeeNameTextBox.PasswordChar = '\0';
            this.employeeNameTextBox.PromptText = "Employee name";
            this.employeeNameTextBox.ReadOnly = true;
            this.employeeNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.employeeNameTextBox.SelectedText = "";
            this.employeeNameTextBox.SelectionLength = 0;
            this.employeeNameTextBox.SelectionStart = 0;
            this.employeeNameTextBox.ShortcutsEnabled = true;
            this.employeeNameTextBox.Size = new System.Drawing.Size(185, 23);
            this.employeeNameTextBox.TabIndex = 3;
            this.employeeNameTextBox.UseSelectable = true;
            this.employeeNameTextBox.WaterMark = "Employee name";
            this.employeeNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.employeeNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(151, 74);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(67, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Employee";
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.ItemHeight = 23;
            this.departmentComboBox.Location = new System.Drawing.Point(252, 38);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(185, 29);
            this.departmentComboBox.TabIndex = 0;
            this.departmentComboBox.UseSelectable = true;
            this.departmentComboBox.SelectionChangeCommitted += new System.EventHandler(this.departmentComboBox_SelectionChangeCommitted);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(138, 42);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(80, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Department";
            // 
            // headOfDepartmentContextMenu
            // 
            this.headOfDepartmentContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.headOfDepartmentContextMenu.Name = "headOfDepartmentContextMenu";
            this.headOfDepartmentContextMenu.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 60;
            // 
            // departmentColumn
            // 
            this.departmentColumn.HeaderText = "Department";
            this.departmentColumn.Name = "departmentColumn";
            this.departmentColumn.ReadOnly = true;
            this.departmentColumn.Width = 200;
            // 
            // headOfDepartmentColumn
            // 
            this.headOfDepartmentColumn.HeaderText = "Head of Department";
            this.headOfDepartmentColumn.Name = "headOfDepartmentColumn";
            this.headOfDepartmentColumn.ReadOnly = true;
            this.headOfDepartmentColumn.Width = 300;
            // 
            // HeadOfDepartmentSetupUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(658, 469);
            this.Controls.Add(this.headOfDepartmentSetupGroupBox);
            this.MaximizeBox = false;
            this.Name = "HeadOfDepartmentSetupUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Head of Department Setup";
            this.Load += new System.EventHandler(this.HeadOfDepartmentSetupUI_Load);
            this.headOfDepartmentSetupGroupBox.ResumeLayout(false);
            this.headOfDepartmentSetupGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headOfDepaertmentDataGridView)).EndInit();
            this.headOfDepartmentContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox headOfDepartmentSetupGroupBox;
        private MetroFramework.Controls.MetroTextBox employeeNameTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox departmentComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton selectEmployeeButton;
        private MetroFramework.Controls.MetroButton saveButton;
        private MetroFramework.Controls.MetroContextMenu headOfDepartmentContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private MetroFramework.Controls.MetroButton resetButton;
        private System.Windows.Forms.DataGridView headOfDepaertmentDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn departmentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn headOfDepartmentColumn;
    }
}