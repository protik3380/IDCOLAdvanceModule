namespace IDCOLAdvanceModule.UI.Entry
{
    partial class RequisitionViewUI
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
            this.requisitionReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.viewSupportingFilesButton = new MetroFramework.Controls.MetroButton();
            this.view360Button = new MetroFramework.Controls.MetroButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // requisitionReportViewer
            // 
            this.requisitionReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requisitionReportViewer.Location = new System.Drawing.Point(3, 16);
            this.requisitionReportViewer.Name = "requisitionReportViewer";
            this.requisitionReportViewer.Size = new System.Drawing.Size(1120, 439);
            this.requisitionReportViewer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.requisitionReportViewer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1126, 458);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // viewSupportingFilesButton
            // 
            this.viewSupportingFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewSupportingFilesButton.Location = new System.Drawing.Point(12, 476);
            this.viewSupportingFilesButton.Name = "viewSupportingFilesButton";
            this.viewSupportingFilesButton.Size = new System.Drawing.Size(183, 38);
            this.viewSupportingFilesButton.TabIndex = 14;
            this.viewSupportingFilesButton.Text = "View Supporting File(s)";
            this.viewSupportingFilesButton.UseSelectable = true;
            this.viewSupportingFilesButton.Click += new System.EventHandler(this.viewSupportingFilesButton_Click);
            // 
            // view360Button
            // 
            this.view360Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.view360Button.Location = new System.Drawing.Point(955, 476);
            this.view360Button.Name = "view360Button";
            this.view360Button.Size = new System.Drawing.Size(183, 38);
            this.view360Button.TabIndex = 15;
            this.view360Button.Text = "360 View";
            this.view360Button.UseSelectable = true;
            this.view360Button.Click += new System.EventHandler(this.view360Button_Click);
            // 
            // RequisitionViewUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1150, 524);
            this.Controls.Add(this.view360Button);
            this.Controls.Add(this.viewSupportingFilesButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "RequisitionViewUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Requisition View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RequisitionViewUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer requisitionReportViewer;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton viewSupportingFilesButton;
        private MetroFramework.Controls.MetroButton view360Button;
    }
}