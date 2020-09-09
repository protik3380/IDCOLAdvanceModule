namespace IDCOLAdvanceModule.UI.Settings
{
    partial class JustificationEntryUI
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
            this.entitleValueLabel = new MetroFramework.Controls.MetroLabel();
            this.okButton = new MetroFramework.Controls.MetroButton();
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.justificationTextBox = new MetroFramework.Controls.MetroTextBox();
            this.justficationHeaderLabel = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.entitleValueLabel);
            this.groupBox1.Controls.Add(this.okButton);
            this.groupBox1.Controls.Add(this.cancelButton);
            this.groupBox1.Controls.Add(this.justificationTextBox);
            this.groupBox1.Controls.Add(this.justficationHeaderLabel);
            this.groupBox1.Location = new System.Drawing.Point(13, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // entitleValueLabel
            // 
            this.entitleValueLabel.AutoSize = true;
            this.entitleValueLabel.Location = new System.Drawing.Point(276, 20);
            this.entitleValueLabel.Name = "entitleValueLabel";
            this.entitleValueLabel.Size = new System.Drawing.Size(16, 19);
            this.entitleValueLabel.TabIndex = 11;
            this.entitleValueLabel.Text = "0";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(205, 111);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseSelectable = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(124, 111);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseSelectable = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // justificationTextBox
            // 
            // 
            // 
            // 
            this.justificationTextBox.CustomButton.Image = null;
            this.justificationTextBox.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.justificationTextBox.CustomButton.Name = "";
            this.justificationTextBox.CustomButton.Size = new System.Drawing.Size(59, 59);
            this.justificationTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.justificationTextBox.CustomButton.TabIndex = 1;
            this.justificationTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.justificationTextBox.CustomButton.UseSelectable = true;
            this.justificationTextBox.CustomButton.Visible = false;
            this.justificationTextBox.Lines = new string[0];
            this.justificationTextBox.Location = new System.Drawing.Point(82, 41);
            this.justificationTextBox.MaxLength = 32767;
            this.justificationTextBox.Multiline = true;
            this.justificationTextBox.Name = "justificationTextBox";
            this.justificationTextBox.PasswordChar = '\0';
            this.justificationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.justificationTextBox.SelectedText = "";
            this.justificationTextBox.SelectionLength = 0;
            this.justificationTextBox.SelectionStart = 0;
            this.justificationTextBox.ShortcutsEnabled = true;
            this.justificationTextBox.Size = new System.Drawing.Size(200, 64);
            this.justificationTextBox.TabIndex = 4;
            this.justificationTextBox.UseSelectable = true;
            this.justificationTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.justificationTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // justficationHeaderLabel
            // 
            this.justficationHeaderLabel.AutoSize = true;
            this.justficationHeaderLabel.Location = new System.Drawing.Point(6, 19);
            this.justficationHeaderLabel.Name = "justficationHeaderLabel";
            this.justficationHeaderLabel.Size = new System.Drawing.Size(276, 19);
            this.justficationHeaderLabel.TabIndex = 3;
            this.justficationHeaderLabel.Text = "Please provide justification to take more than ";
            // 
            // JustificationEntryUI
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(380, 167);
            this.Controls.Add(this.groupBox1);
            this.Name = "JustificationEntryUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Justification";
            this.Load += new System.EventHandler(this.JustificationEntryUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public MetroFramework.Controls.MetroLabel justficationHeaderLabel;
        private MetroFramework.Controls.MetroTextBox justificationTextBox;
        private MetroFramework.Controls.MetroButton okButton;
        private MetroFramework.Controls.MetroButton cancelButton;
        public MetroFramework.Controls.MetroLabel entitleValueLabel;
    }
}