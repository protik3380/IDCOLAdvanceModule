namespace IDCOLAdvanceModule.UI
{
    partial class SelectPaymentDateUI
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
            this.paymentDateMetroDateTime = new MetroFramework.Controls.MetroDateTime();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.payMetroButton = new MetroFramework.Controls.MetroButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paymentDateMetroDateTime
            // 
            this.paymentDateMetroDateTime.Checked = false;
            this.paymentDateMetroDateTime.Location = new System.Drawing.Point(144, 49);
            this.paymentDateMetroDateTime.MinimumSize = new System.Drawing.Size(0, 29);
            this.paymentDateMetroDateTime.Name = "paymentDateMetroDateTime";
            this.paymentDateMetroDateTime.ShowCheckBox = true;
            this.paymentDateMetroDateTime.Size = new System.Drawing.Size(223, 29);
            this.paymentDateMetroDateTime.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.payMetroButton);
            this.groupBox1.Controls.Add(this.paymentDateMetroDateTime);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 139);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment Date Select";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(36, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(90, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Payment Date";
            // 
            // payMetroButton
            // 
            this.payMetroButton.Location = new System.Drawing.Point(270, 85);
            this.payMetroButton.Name = "payMetroButton";
            this.payMetroButton.Size = new System.Drawing.Size(75, 23);
            this.payMetroButton.TabIndex = 1;
            this.payMetroButton.Text = "Pay";
            this.payMetroButton.UseSelectable = true;
            this.payMetroButton.Click += new System.EventHandler(this.payMetroButton_Click);
            // 
            // SelectPaymentDateUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(445, 160);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "SelectPaymentDateUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Payment Date";
            this.Load += new System.EventHandler(this.SelectPaymentDateUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton payMetroButton;
        public MetroFramework.Controls.MetroDateTime paymentDateMetroDateTime;
    }
}