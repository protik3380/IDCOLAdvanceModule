namespace IDCOLAdvanceModule.UI.Report
{
    partial class RejectedRequisitionExpenseReportUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.fromDateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.employeeNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.employeeIdTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.showButton = new MetroFramework.Controls.MetroButton();
            this.departmentComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.categoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.entryTypeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.rejectedReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.toDateTimePicker);
            this.groupBox1.Controls.Add(this.fromDateTimePicker);
            this.groupBox1.Controls.Add(this.employeeNameTextBox);
            this.groupBox1.Controls.Add(this.employeeIdTextBox);
            this.groupBox1.Controls.Add(this.metroLabel7);
            this.groupBox1.Controls.Add(this.metroLabel6);
            this.groupBox1.Controls.Add(this.metroLabel5);
            this.groupBox1.Controls.Add(this.metroLabel4);
            this.groupBox1.Controls.Add(this.showButton);
            this.groupBox1.Controls.Add(this.departmentComboBox);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.categoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.entryTypeComboBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(172, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 281);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criteria";
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.toDateTimePicker.Checked = false;
            this.toDateTimePicker.Location = new System.Drawing.Point(199, 211);
            this.toDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.ShowCheckBox = true;
            this.toDateTimePicker.Size = new System.Drawing.Size(213, 29);
            this.toDateTimePicker.TabIndex = 6;
            this.toDateTimePicker.Value = new System.DateTime(2017, 3, 2, 18, 36, 3, 0);
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fromDateTimePicker.Checked = false;
            this.fromDateTimePicker.Location = new System.Drawing.Point(199, 177);
            this.fromDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.ShowCheckBox = true;
            this.fromDateTimePicker.Size = new System.Drawing.Size(213, 29);
            this.fromDateTimePicker.TabIndex = 5;
            this.fromDateTimePicker.Value = new System.DateTime(2017, 3, 2, 18, 35, 58, 0);
            // 
            // employeeNameTextBox
            // 
            this.employeeNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.employeeNameTextBox.CustomButton.Image = null;
            this.employeeNameTextBox.CustomButton.Location = new System.Drawing.Point(191, 1);
            this.employeeNameTextBox.CustomButton.Name = "";
            this.employeeNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.employeeNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.employeeNameTextBox.CustomButton.TabIndex = 1;
            this.employeeNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.employeeNameTextBox.CustomButton.UseSelectable = true;
            this.employeeNameTextBox.CustomButton.Visible = false;
            this.employeeNameTextBox.Lines = new string[0];
            this.employeeNameTextBox.Location = new System.Drawing.Point(199, 149);
            this.employeeNameTextBox.MaxLength = 32767;
            this.employeeNameTextBox.Name = "employeeNameTextBox";
            this.employeeNameTextBox.PasswordChar = '\0';
            this.employeeNameTextBox.PromptText = "Employee name";
            this.employeeNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.employeeNameTextBox.SelectedText = "";
            this.employeeNameTextBox.SelectionLength = 0;
            this.employeeNameTextBox.SelectionStart = 0;
            this.employeeNameTextBox.ShortcutsEnabled = true;
            this.employeeNameTextBox.Size = new System.Drawing.Size(213, 23);
            this.employeeNameTextBox.TabIndex = 4;
            this.employeeNameTextBox.UseSelectable = true;
            this.employeeNameTextBox.WaterMark = "Employee name";
            this.employeeNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.employeeNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // employeeIdTextBox
            // 
            this.employeeIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.employeeIdTextBox.CustomButton.Image = null;
            this.employeeIdTextBox.CustomButton.Location = new System.Drawing.Point(191, 1);
            this.employeeIdTextBox.CustomButton.Name = "";
            this.employeeIdTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.employeeIdTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.employeeIdTextBox.CustomButton.TabIndex = 1;
            this.employeeIdTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.employeeIdTextBox.CustomButton.UseSelectable = true;
            this.employeeIdTextBox.CustomButton.Visible = false;
            this.employeeIdTextBox.Lines = new string[0];
            this.employeeIdTextBox.Location = new System.Drawing.Point(199, 121);
            this.employeeIdTextBox.MaxLength = 32767;
            this.employeeIdTextBox.Name = "employeeIdTextBox";
            this.employeeIdTextBox.PasswordChar = '\0';
            this.employeeIdTextBox.PromptText = "Employee Id";
            this.employeeIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.employeeIdTextBox.SelectedText = "";
            this.employeeIdTextBox.SelectionLength = 0;
            this.employeeIdTextBox.SelectionStart = 0;
            this.employeeIdTextBox.ShortcutsEnabled = true;
            this.employeeIdTextBox.Size = new System.Drawing.Size(213, 23);
            this.employeeIdTextBox.TabIndex = 3;
            this.employeeIdTextBox.UseSelectable = true;
            this.employeeIdTextBox.WaterMark = "Employee Id";
            this.employeeIdTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.employeeIdTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel7
            // 
            this.metroLabel7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(148, 214);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(22, 19);
            this.metroLabel7.TabIndex = 14;
            this.metroLabel7.Text = "To";
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(129, 181);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(41, 19);
            this.metroLabel6.TabIndex = 13;
            this.metroLabel6.Text = "From";
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(66, 152);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(104, 19);
            this.metroLabel5.TabIndex = 12;
            this.metroLabel5.Text = "Employee name";
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(88, 125);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(82, 19);
            this.metroLabel4.TabIndex = 11;
            this.metroLabel4.Text = "Employee Id";
            // 
            // showButton
            // 
            this.showButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.showButton.Location = new System.Drawing.Point(337, 245);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(75, 23);
            this.showButton.TabIndex = 7;
            this.showButton.Text = "Show";
            this.showButton.UseSelectable = true;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.ItemHeight = 23;
            this.departmentComboBox.Location = new System.Drawing.Point(199, 87);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(213, 29);
            this.departmentComboBox.TabIndex = 2;
            this.departmentComboBox.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(90, 91);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(80, 19);
            this.metroLabel3.TabIndex = 10;
            this.metroLabel3.Text = "Department";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.ItemHeight = 23;
            this.categoryComboBox.Location = new System.Drawing.Point(199, 53);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(213, 29);
            this.categoryComboBox.TabIndex = 1;
            this.categoryComboBox.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(106, 57);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(64, 19);
            this.metroLabel2.TabIndex = 9;
            this.metroLabel2.Text = "Category";
            // 
            // entryTypeComboBox
            // 
            this.entryTypeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.entryTypeComboBox.FormattingEnabled = true;
            this.entryTypeComboBox.ItemHeight = 23;
            this.entryTypeComboBox.Location = new System.Drawing.Point(199, 19);
            this.entryTypeComboBox.Name = "entryTypeComboBox";
            this.entryTypeComboBox.Size = new System.Drawing.Size(213, 29);
            this.entryTypeComboBox.TabIndex = 0;
            this.entryTypeComboBox.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(134, 23);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(36, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Type";
            // 
            // rejectedReportViewer
            // 
            this.rejectedReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rejectedReportViewer.Location = new System.Drawing.Point(3, 19);
            this.rejectedReportViewer.Name = "rejectedReportViewer";
            this.rejectedReportViewer.Size = new System.Drawing.Size(817, 180);
            this.rejectedReportViewer.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rejectedReportViewer);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 293);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(823, 202);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // RejectedRequisitionExpenseReportUI
            // 
            this.AcceptButton = this.showButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(847, 507);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RejectedRequisitionExpenseReportUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rejected Requisition or Expense Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RejectedRequisitionExpenseReportUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroComboBox entryTypeComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton showButton;
        private Microsoft.Reporting.WinForms.ReportViewer rejectedReportViewer;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroTextBox employeeNameTextBox;
        private MetroFramework.Controls.MetroTextBox employeeIdTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox departmentComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox categoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime toDateTimePicker;
        private MetroFramework.Controls.MetroDateTime fromDateTimePicker;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel6;
    }
}