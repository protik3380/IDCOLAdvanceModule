namespace IDCOLAdvanceModule.UI
{
    partial class SelectReceiveDateUI
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.receiveMetroButton = new MetroFramework.Controls.MetroButton();
            this.receiveDateMetroDateTime = new MetroFramework.Controls.MetroDateTime();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.receiveMetroButton);
            this.groupBox1.Controls.Add(this.receiveDateMetroDateTime);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 139);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Receive Date Select";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(36, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(84, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Receive Date";
            // 
            // receiveMetroButton
            // 
            this.receiveMetroButton.Location = new System.Drawing.Point(292, 84);
            this.receiveMetroButton.Name = "receiveMetroButton";
            this.receiveMetroButton.Size = new System.Drawing.Size(75, 23);
            this.receiveMetroButton.TabIndex = 1;
            this.receiveMetroButton.Text = "Receive";
            this.receiveMetroButton.UseSelectable = true;
            this.receiveMetroButton.Click += new System.EventHandler(this.receiveMetroButton_Click);
            // 
            // receiveDateMetroDateTime
            // 
            this.receiveDateMetroDateTime.Checked = false;
            this.receiveDateMetroDateTime.Location = new System.Drawing.Point(144, 49);
            this.receiveDateMetroDateTime.MinimumSize = new System.Drawing.Size(0, 29);
            this.receiveDateMetroDateTime.Name = "receiveDateMetroDateTime";
            this.receiveDateMetroDateTime.ShowCheckBox = true;
            this.receiveDateMetroDateTime.Size = new System.Drawing.Size(223, 29);
            this.receiveDateMetroDateTime.TabIndex = 0;
            // 
            // SelectReceivedDateUI
            // 
            this.AcceptButton = this.receiveMetroButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(445, 160);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectReceivedDateUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Receive Date";
            this.Load += new System.EventHandler(this.SelectReceivedDateUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton receiveMetroButton;
        public MetroFramework.Controls.MetroDateTime receiveDateMetroDateTime;
    }
}