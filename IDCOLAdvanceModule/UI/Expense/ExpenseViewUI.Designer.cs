namespace IDCOLAdvanceModule.UI.Expense
{
    partial class ExpenseViewUI
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
            this.viewSupportingFilesButton = new MetroFramework.Controls.MetroButton();
            this.expenseReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.view360Button = new MetroFramework.Controls.MetroButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewSupportingFilesButton
            // 
            this.viewSupportingFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewSupportingFilesButton.Location = new System.Drawing.Point(12, 475);
            this.viewSupportingFilesButton.Name = "viewSupportingFilesButton";
            this.viewSupportingFilesButton.Size = new System.Drawing.Size(183, 38);
            this.viewSupportingFilesButton.TabIndex = 16;
            this.viewSupportingFilesButton.Text = "View Supporting File(s)";
            this.viewSupportingFilesButton.UseSelectable = true;
            this.viewSupportingFilesButton.Click += new System.EventHandler(this.viewSupportingFilesButton_Click);
            // 
            // expenseReportViewer
            // 
            this.expenseReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expenseReportViewer.Location = new System.Drawing.Point(3, 16);
            this.expenseReportViewer.Name = "expenseReportViewer";
            this.expenseReportViewer.Size = new System.Drawing.Size(1120, 439);
            this.expenseReportViewer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.expenseReportViewer);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1126, 458);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // view360Button
            // 
            this.view360Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.view360Button.Location = new System.Drawing.Point(955, 475);
            this.view360Button.Name = "view360Button";
            this.view360Button.Size = new System.Drawing.Size(183, 38);
            this.view360Button.TabIndex = 17;
            this.view360Button.Text = "360 View";
            this.view360Button.UseSelectable = true;
            this.view360Button.Click += new System.EventHandler(this.view360Button_Click);
            // 
            // ExpenseViewUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1150, 524);
            this.Controls.Add(this.view360Button);
            this.Controls.Add(this.viewSupportingFilesButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExpenseViewUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adjustment/Reimbursement View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ExpenseViewUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton viewSupportingFilesButton;
        private Microsoft.Reporting.WinForms.ReportViewer expenseReportViewer;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton view360Button;
    }
}