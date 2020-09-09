namespace IDCOLAdvanceModule.UI.Settings
{
    partial class EmployeeLeaveEntryUI
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
            this.employeeInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.resetButton = new MetroFramework.Controls.MetroButton();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel25 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel23 = new MetroFramework.Controls.MetroLabel();
            this.toDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.fromDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.selectEmployeeButton = new MetroFramework.Controls.MetroButton();
            this.employeeIdTextBox = new MetroFramework.Controls.MetroTextBox();
            this.designationTextBox = new MetroFramework.Controls.MetroTextBox();
            this.employeeNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.employeeLeaveContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeeLeaveDataGridView = new System.Windows.Forms.DataGridView();
            this.slColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.employeeInfoGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.employeeLeaveContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeLeaveDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // employeeInfoGroupBox
            // 
            this.employeeInfoGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.employeeInfoGroupBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.employeeInfoGroupBox.Controls.Add(this.resetButton);
            this.employeeInfoGroupBox.Controls.Add(this.saveButton);
            this.employeeInfoGroupBox.Controls.Add(this.metroLabel25);
            this.employeeInfoGroupBox.Controls.Add(this.metroLabel23);
            this.employeeInfoGroupBox.Controls.Add(this.toDateTimePicker);
            this.employeeInfoGroupBox.Controls.Add(this.metroLabel11);
            this.employeeInfoGroupBox.Controls.Add(this.fromDateTimePicker);
            this.employeeInfoGroupBox.Controls.Add(this.metroLabel10);
            this.employeeInfoGroupBox.Controls.Add(this.selectEmployeeButton);
            this.employeeInfoGroupBox.Controls.Add(this.employeeIdTextBox);
            this.employeeInfoGroupBox.Controls.Add(this.designationTextBox);
            this.employeeInfoGroupBox.Controls.Add(this.employeeNameTextBox);
            this.employeeInfoGroupBox.Controls.Add(this.metroLabel3);
            this.employeeInfoGroupBox.Controls.Add(this.metroLabel2);
            this.employeeInfoGroupBox.Controls.Add(this.metroLabel1);
            this.employeeInfoGroupBox.Location = new System.Drawing.Point(44, 16);
            this.employeeInfoGroupBox.Name = "employeeInfoGroupBox";
            this.employeeInfoGroupBox.Size = new System.Drawing.Size(437, 223);
            this.employeeInfoGroupBox.TabIndex = 20;
            this.employeeInfoGroupBox.TabStop = false;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(231, 189);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 23;
            this.resetButton.Text = "Reset";
            this.resetButton.UseSelectable = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(312, 189);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 22;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // metroLabel25
            // 
            this.metroLabel25.AutoSize = true;
            this.metroLabel25.ForeColor = System.Drawing.Color.Red;
            this.metroLabel25.Location = new System.Drawing.Point(111, 156);
            this.metroLabel25.Name = "metroLabel25";
            this.metroLabel25.Size = new System.Drawing.Size(15, 19);
            this.metroLabel25.TabIndex = 21;
            this.metroLabel25.Text = "*";
            this.metroLabel25.UseCustomForeColor = true;
            // 
            // metroLabel23
            // 
            this.metroLabel23.AutoSize = true;
            this.metroLabel23.ForeColor = System.Drawing.Color.Red;
            this.metroLabel23.Location = new System.Drawing.Point(110, 125);
            this.metroLabel23.Name = "metroLabel23";
            this.metroLabel23.Size = new System.Drawing.Size(15, 19);
            this.metroLabel23.TabIndex = 20;
            this.metroLabel23.Text = "*";
            this.metroLabel23.UseCustomForeColor = true;
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.Checked = false;
            this.toDateTimePicker.Location = new System.Drawing.Point(157, 154);
            this.toDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.ShowCheckBox = true;
            this.toDateTimePicker.Size = new System.Drawing.Size(230, 29);
            this.toDateTimePicker.TabIndex = 17;
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.Location = new System.Drawing.Point(93, 159);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(22, 19);
            this.metroLabel11.TabIndex = 19;
            this.metroLabel11.Text = "To";
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.Checked = false;
            this.fromDateTimePicker.Location = new System.Drawing.Point(157, 119);
            this.fromDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.ShowCheckBox = true;
            this.fromDateTimePicker.Size = new System.Drawing.Size(230, 29);
            this.fromDateTimePicker.TabIndex = 16;
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.Location = new System.Drawing.Point(74, 127);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(41, 19);
            this.metroLabel10.TabIndex = 18;
            this.metroLabel10.Text = "From";
            // 
            // selectEmployeeButton
            // 
            this.selectEmployeeButton.Enabled = false;
            this.selectEmployeeButton.Location = new System.Drawing.Point(388, 13);
            this.selectEmployeeButton.Name = "selectEmployeeButton";
            this.selectEmployeeButton.Size = new System.Drawing.Size(34, 23);
            this.selectEmployeeButton.TabIndex = 7;
            this.selectEmployeeButton.Text = "...";
            this.selectEmployeeButton.UseSelectable = true;
            this.selectEmployeeButton.Click += new System.EventHandler(this.selectEmployeeButton_Click);
            // 
            // employeeIdTextBox
            // 
            // 
            // 
            // 
            this.employeeIdTextBox.CustomButton.Image = null;
            this.employeeIdTextBox.CustomButton.Location = new System.Drawing.Point(208, 1);
            this.employeeIdTextBox.CustomButton.Name = "";
            this.employeeIdTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.employeeIdTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.employeeIdTextBox.CustomButton.TabIndex = 1;
            this.employeeIdTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.employeeIdTextBox.CustomButton.UseSelectable = true;
            this.employeeIdTextBox.CustomButton.Visible = false;
            this.employeeIdTextBox.Lines = new string[0];
            this.employeeIdTextBox.Location = new System.Drawing.Point(157, 67);
            this.employeeIdTextBox.MaxLength = 32767;
            this.employeeIdTextBox.Name = "employeeIdTextBox";
            this.employeeIdTextBox.PasswordChar = '\0';
            this.employeeIdTextBox.ReadOnly = true;
            this.employeeIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.employeeIdTextBox.SelectedText = "";
            this.employeeIdTextBox.SelectionLength = 0;
            this.employeeIdTextBox.SelectionStart = 0;
            this.employeeIdTextBox.ShortcutsEnabled = true;
            this.employeeIdTextBox.Size = new System.Drawing.Size(230, 23);
            this.employeeIdTextBox.TabIndex = 2;
            this.employeeIdTextBox.UseSelectable = true;
            this.employeeIdTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.employeeIdTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // designationTextBox
            // 
            // 
            // 
            // 
            this.designationTextBox.CustomButton.Image = null;
            this.designationTextBox.CustomButton.Location = new System.Drawing.Point(208, 1);
            this.designationTextBox.CustomButton.Name = "";
            this.designationTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.designationTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.designationTextBox.CustomButton.TabIndex = 1;
            this.designationTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.designationTextBox.CustomButton.UseSelectable = true;
            this.designationTextBox.CustomButton.Visible = false;
            this.designationTextBox.Lines = new string[0];
            this.designationTextBox.Location = new System.Drawing.Point(157, 40);
            this.designationTextBox.MaxLength = 32767;
            this.designationTextBox.Name = "designationTextBox";
            this.designationTextBox.PasswordChar = '\0';
            this.designationTextBox.ReadOnly = true;
            this.designationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.designationTextBox.SelectedText = "";
            this.designationTextBox.SelectionLength = 0;
            this.designationTextBox.SelectionStart = 0;
            this.designationTextBox.ShortcutsEnabled = true;
            this.designationTextBox.Size = new System.Drawing.Size(230, 23);
            this.designationTextBox.TabIndex = 1;
            this.designationTextBox.UseSelectable = true;
            this.designationTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.designationTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // employeeNameTextBox
            // 
            // 
            // 
            // 
            this.employeeNameTextBox.CustomButton.Image = null;
            this.employeeNameTextBox.CustomButton.Location = new System.Drawing.Point(208, 1);
            this.employeeNameTextBox.CustomButton.Name = "";
            this.employeeNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.employeeNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.employeeNameTextBox.CustomButton.TabIndex = 1;
            this.employeeNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.employeeNameTextBox.CustomButton.UseSelectable = true;
            this.employeeNameTextBox.CustomButton.Visible = false;
            this.employeeNameTextBox.Lines = new string[0];
            this.employeeNameTextBox.Location = new System.Drawing.Point(157, 13);
            this.employeeNameTextBox.MaxLength = 32767;
            this.employeeNameTextBox.Name = "employeeNameTextBox";
            this.employeeNameTextBox.PasswordChar = '\0';
            this.employeeNameTextBox.ReadOnly = true;
            this.employeeNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.employeeNameTextBox.SelectedText = "";
            this.employeeNameTextBox.SelectionLength = 0;
            this.employeeNameTextBox.SelectionStart = 0;
            this.employeeNameTextBox.ShortcutsEnabled = true;
            this.employeeNameTextBox.Size = new System.Drawing.Size(230, 23);
            this.employeeNameTextBox.TabIndex = 0;
            this.employeeNameTextBox.UseSelectable = true;
            this.employeeNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.employeeNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(41, 69);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(83, 19);
            this.metroLabel3.TabIndex = 8;
            this.metroLabel3.Text = "Employee ID";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(47, 40);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(77, 19);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "Designation";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(17, 13);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(107, 19);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "Employee Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.employeeLeaveDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 245);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 235);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // employeeLeaveContextMenu
            // 
            this.employeeLeaveContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.employeeLeaveContextMenu.Name = "advanceDetailsContextMenu";
            this.employeeLeaveContextMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.removeToolStripMenuItem.Text = "Delete";
            this.removeToolStripMenuItem.Visible = false;
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // employeeLeaveDataGridView
            // 
            this.employeeLeaveDataGridView.AllowUserToAddRows = false;
            this.employeeLeaveDataGridView.AllowUserToDeleteRows = false;
            this.employeeLeaveDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeLeaveDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.employeeLeaveDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employeeLeaveDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slColumn,
            this.fromDateColumn,
            this.toDateColumn,
            this.editColumn,
            this.removeColumn});
            this.employeeLeaveDataGridView.EnableHeadersVisualStyles = false;
            this.employeeLeaveDataGridView.Location = new System.Drawing.Point(7, 13);
            this.employeeLeaveDataGridView.MultiSelect = false;
            this.employeeLeaveDataGridView.Name = "employeeLeaveDataGridView";
            this.employeeLeaveDataGridView.ReadOnly = true;
            this.employeeLeaveDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeLeaveDataGridView.Size = new System.Drawing.Size(480, 216);
            this.employeeLeaveDataGridView.TabIndex = 0;
            this.employeeLeaveDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.employeeLeaveDataGridView_CellContentClick);
            // 
            // slColumn
            // 
            this.slColumn.HeaderText = "Sl#";
            this.slColumn.Name = "slColumn";
            this.slColumn.ReadOnly = true;
            this.slColumn.Width = 70;
            // 
            // fromDateColumn
            // 
            this.fromDateColumn.HeaderText = "From Date";
            this.fromDateColumn.Name = "fromDateColumn";
            this.fromDateColumn.ReadOnly = true;
            this.fromDateColumn.Width = 120;
            // 
            // toDateColumn
            // 
            this.toDateColumn.HeaderText = "To Date";
            this.toDateColumn.Name = "toDateColumn";
            this.toDateColumn.ReadOnly = true;
            this.toDateColumn.Width = 120;
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 60;
            // 
            // removeColumn
            // 
            this.removeColumn.HeaderText = "";
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.ReadOnly = true;
            this.removeColumn.Width = 60;
            // 
            // EmployeeLeaveEntryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(520, 489);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.employeeInfoGroupBox);
            this.Name = "EmployeeLeaveEntryUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Leave Entry";
            this.Load += new System.EventHandler(this.EmployeeLeaveEntryUI_Load);
            this.employeeInfoGroupBox.ResumeLayout(false);
            this.employeeInfoGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.employeeLeaveContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employeeLeaveDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox employeeInfoGroupBox;
        private MetroFramework.Controls.MetroButton selectEmployeeButton;
        private MetroFramework.Controls.MetroTextBox employeeIdTextBox;
        private MetroFramework.Controls.MetroTextBox designationTextBox;
        private MetroFramework.Controls.MetroTextBox employeeNameTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel25;
        private MetroFramework.Controls.MetroLabel metroLabel23;
        private MetroFramework.Controls.MetroDateTime toDateTimePicker;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroDateTime fromDateTimePicker;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroContextMenu employeeLeaveContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private MetroFramework.Controls.MetroButton resetButton;
        private System.Windows.Forms.DataGridView employeeLeaveDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn slColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toDateColumn;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
    }
}