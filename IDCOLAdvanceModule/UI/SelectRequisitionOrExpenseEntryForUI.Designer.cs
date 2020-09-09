namespace IDCOLAdvanceModule.UI
{
    partial class SelectRequisitionOrExpenseEntryForUI
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
            this.requisitionOrExpenseForLabel = new MetroFramework.Controls.MetroLabel();
            this.nextButton = new MetroFramework.Controls.MetroButton();
            this.otherRadioButton = new MetroFramework.Controls.MetroRadioButton();
            this.ownRadioButton = new MetroFramework.Controls.MetroRadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.requisitionOrExpenseForLabel);
            this.groupBox1.Controls.Add(this.nextButton);
            this.groupBox1.Controls.Add(this.otherRadioButton);
            this.groupBox1.Controls.Add(this.ownRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(65, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // requisitionOrExpenseForLabel
            // 
            this.requisitionOrExpenseForLabel.AutoSize = true;
            this.requisitionOrExpenseForLabel.Location = new System.Drawing.Point(20, 27);
            this.requisitionOrExpenseForLabel.Name = "requisitionOrExpenseForLabel";
            this.requisitionOrExpenseForLabel.Size = new System.Drawing.Size(93, 19);
            this.requisitionOrExpenseForLabel.TabIndex = 2;
            this.requisitionOrExpenseForLabel.Text = "Requisition for";
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(123, 101);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseSelectable = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // otherRadioButton
            // 
            this.otherRadioButton.AutoSize = true;
            this.otherRadioButton.Location = new System.Drawing.Point(145, 63);
            this.otherRadioButton.Name = "otherRadioButton";
            this.otherRadioButton.Size = new System.Drawing.Size(53, 15);
            this.otherRadioButton.TabIndex = 0;
            this.otherRadioButton.Text = "Other";
            this.otherRadioButton.UseSelectable = true;
            // 
            // ownRadioButton
            // 
            this.ownRadioButton.AutoSize = true;
            this.ownRadioButton.Checked = true;
            this.ownRadioButton.Location = new System.Drawing.Point(43, 63);
            this.ownRadioButton.Name = "ownRadioButton";
            this.ownRadioButton.Size = new System.Drawing.Size(48, 15);
            this.ownRadioButton.TabIndex = 0;
            this.ownRadioButton.TabStop = true;
            this.ownRadioButton.Text = "Own";
            this.ownRadioButton.UseSelectable = true;
            // 
            // SelectRequisitionOrExpenseEntryForUI
            // 
            this.AcceptButton = this.nextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(371, 168);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "SelectRequisitionOrExpenseEntryForUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Requisition For";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton nextButton;
        private MetroFramework.Controls.MetroRadioButton otherRadioButton;
        private MetroFramework.Controls.MetroRadioButton ownRadioButton;
        public MetroFramework.Controls.MetroLabel requisitionOrExpenseForLabel;
    }
}