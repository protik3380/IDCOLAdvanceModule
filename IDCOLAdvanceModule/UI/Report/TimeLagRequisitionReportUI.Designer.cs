namespace IDCOLAdvanceModule.UI.Report
{
    partial class TimeLagRequisitionReportUI
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
            this.levelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.Level = new MetroFramework.Controls.MetroLabel();
            this.panelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.requisitionNoTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.employeeNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.employeeIdTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.showButton = new MetroFramework.Controls.MetroButton();
            this.categoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timeLagReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.levelComboBox);
            this.groupBox1.Controls.Add(this.Level);
            this.groupBox1.Controls.Add(this.panelComboBox);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.requisitionNoTextBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.employeeNameTextBox);
            this.groupBox1.Controls.Add(this.employeeIdTextBox);
            this.groupBox1.Controls.Add(this.metroLabel5);
            this.groupBox1.Controls.Add(this.metroLabel4);
            this.groupBox1.Controls.Add(this.showButton);
            this.groupBox1.Controls.Add(this.categoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(180, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 242);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criteria";
            // 
            // levelComboBox
            // 
            this.levelComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.levelComboBox.FormattingEnabled = true;
            this.levelComboBox.ItemHeight = 23;
            this.levelComboBox.Location = new System.Drawing.Point(199, 172);
            this.levelComboBox.Name = "levelComboBox";
            this.levelComboBox.Size = new System.Drawing.Size(213, 29);
            this.levelComboBox.TabIndex = 5;
            this.levelComboBox.UseSelectable = true;
            // 
            // Level
            // 
            this.Level.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Level.AutoSize = true;
            this.Level.Location = new System.Drawing.Point(132, 176);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(38, 19);
            this.Level.TabIndex = 12;
            this.Level.Text = "Level";
            // 
            // panelComboBox
            // 
            this.panelComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelComboBox.FormattingEnabled = true;
            this.panelComboBox.ItemHeight = 23;
            this.panelComboBox.Location = new System.Drawing.Point(199, 137);
            this.panelComboBox.Name = "panelComboBox";
            this.panelComboBox.Size = new System.Drawing.Size(213, 29);
            this.panelComboBox.TabIndex = 4;
            this.panelComboBox.UseSelectable = true;
            this.panelComboBox.SelectionChangeCommitted += new System.EventHandler(this.panelComboBox_SelectionChangeCommitted);
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(130, 141);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(40, 19);
            this.metroLabel3.TabIndex = 11;
            this.metroLabel3.Text = "Panel";
            // 
            // requisitionNoTextBox
            // 
            this.requisitionNoTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.requisitionNoTextBox.CustomButton.Image = null;
            this.requisitionNoTextBox.CustomButton.Location = new System.Drawing.Point(191, 1);
            this.requisitionNoTextBox.CustomButton.Name = "";
            this.requisitionNoTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.requisitionNoTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.requisitionNoTextBox.CustomButton.TabIndex = 1;
            this.requisitionNoTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.requisitionNoTextBox.CustomButton.UseSelectable = true;
            this.requisitionNoTextBox.CustomButton.Visible = false;
            this.requisitionNoTextBox.Lines = new string[0];
            this.requisitionNoTextBox.Location = new System.Drawing.Point(199, 51);
            this.requisitionNoTextBox.MaxLength = 32767;
            this.requisitionNoTextBox.Name = "requisitionNoTextBox";
            this.requisitionNoTextBox.PasswordChar = '\0';
            this.requisitionNoTextBox.PromptText = "Requisition No.";
            this.requisitionNoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.requisitionNoTextBox.SelectedText = "";
            this.requisitionNoTextBox.SelectionLength = 0;
            this.requisitionNoTextBox.SelectionStart = 0;
            this.requisitionNoTextBox.ShortcutsEnabled = true;
            this.requisitionNoTextBox.Size = new System.Drawing.Size(213, 23);
            this.requisitionNoTextBox.TabIndex = 1;
            this.requisitionNoTextBox.UseSelectable = true;
            this.requisitionNoTextBox.WaterMark = "Requisition No.";
            this.requisitionNoTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.requisitionNoTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(73, 52);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(97, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Requisition No.";
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
            this.employeeNameTextBox.Location = new System.Drawing.Point(199, 108);
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
            this.employeeNameTextBox.TabIndex = 3;
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
            this.employeeIdTextBox.Location = new System.Drawing.Point(199, 80);
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
            this.employeeIdTextBox.TabIndex = 2;
            this.employeeIdTextBox.UseSelectable = true;
            this.employeeIdTextBox.WaterMark = "Employee Id";
            this.employeeIdTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.employeeIdTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(66, 108);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(104, 19);
            this.metroLabel5.TabIndex = 10;
            this.metroLabel5.Text = "Employee name";
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(88, 81);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(82, 19);
            this.metroLabel4.TabIndex = 9;
            this.metroLabel4.Text = "Employee Id";
            // 
            // showButton
            // 
            this.showButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.showButton.Location = new System.Drawing.Point(337, 206);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(75, 23);
            this.showButton.TabIndex = 6;
            this.showButton.Text = "Show";
            this.showButton.UseSelectable = true;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.ItemHeight = 23;
            this.categoryComboBox.Location = new System.Drawing.Point(199, 16);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(213, 29);
            this.categoryComboBox.TabIndex = 0;
            this.categoryComboBox.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(106, 20);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(64, 19);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "Category";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.timeLagReportViewer);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 254);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(823, 264);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // timeLagReportViewer
            // 
            this.timeLagReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLagReportViewer.Location = new System.Drawing.Point(3, 19);
            this.timeLagReportViewer.Name = "timeLagReportViewer";
            this.timeLagReportViewer.Size = new System.Drawing.Size(817, 242);
            this.timeLagReportViewer.TabIndex = 0;
            // 
            // TimeLagRequisitionReportUI
            // 
            this.AcceptButton = this.showButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(847, 534);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "TimeLagRequisitionReportUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Lag Report (Requisition)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TimeLagRequisitionReportUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox employeeNameTextBox;
        private MetroFramework.Controls.MetroTextBox employeeIdTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroButton showButton;
        private MetroFramework.Controls.MetroComboBox categoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox requisitionNoTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Microsoft.Reporting.WinForms.ReportViewer timeLagReportViewer;
        private MetroFramework.Controls.MetroComboBox levelComboBox;
        private MetroFramework.Controls.MetroLabel Level;
        private MetroFramework.Controls.MetroComboBox panelComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}