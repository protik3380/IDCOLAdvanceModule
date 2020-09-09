namespace IDCOLAdvanceModule.UI
{
    partial class SelectExpenseNatureUI
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.adjustmentRadioButton = new MetroFramework.Controls.MetroRadioButton();
            this.newExpenseEntryRadioButton = new MetroFramework.Controls.MetroRadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nextButton = new MetroFramework.Controls.MetroButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(25, 20);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(355, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Do you want to adjust? Or provide a new adjustment entry?";
            // 
            // adjustmentRadioButton
            // 
            this.adjustmentRadioButton.AutoSize = true;
            this.adjustmentRadioButton.Checked = true;
            this.adjustmentRadioButton.Location = new System.Drawing.Point(13, 56);
            this.adjustmentRadioButton.Name = "adjustmentRadioButton";
            this.adjustmentRadioButton.Size = new System.Drawing.Size(85, 15);
            this.adjustmentRadioButton.TabIndex = 0;
            this.adjustmentRadioButton.TabStop = true;
            this.adjustmentRadioButton.Text = "Adjustment";
            this.adjustmentRadioButton.UseSelectable = true;
            // 
            // newExpenseEntryRadioButton
            // 
            this.newExpenseEntryRadioButton.AutoSize = true;
            this.newExpenseEntryRadioButton.Location = new System.Drawing.Point(139, 56);
            this.newExpenseEntryRadioButton.Name = "newExpenseEntryRadioButton";
            this.newExpenseEntryRadioButton.Size = new System.Drawing.Size(251, 15);
            this.newExpenseEntryRadioButton.TabIndex = 1;
            this.newExpenseEntryRadioButton.Text = "New adjustment entry (without requisition)";
            this.newExpenseEntryRadioButton.UseSelectable = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nextButton);
            this.groupBox1.Controls.Add(this.newExpenseEntryRadioButton);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.adjustmentRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(271, 97);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next";
            this.nextButton.UseSelectable = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // SelectExpenseNatureUI
            // 
            this.AcceptButton = this.nextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(429, 158);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "SelectExpenseNatureUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Adjustment Nature";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroRadioButton adjustmentRadioButton;
        private MetroFramework.Controls.MetroRadioButton newExpenseEntryRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton nextButton;
    }
}