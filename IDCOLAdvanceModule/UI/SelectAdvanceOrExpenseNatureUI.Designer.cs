namespace IDCOLAdvanceModule.UI
{
    partial class SelectAdvanceOrExpenseNatureUI
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
            this.nextButton = new MetroFramework.Controls.MetroButton();
            this.travelNatureComboBox = new MetroFramework.Controls.MetroComboBox();
            this.advanceOrExpenseNatureLabel = new MetroFramework.Controls.MetroLabel();
            this.idcolAdvanceDBDataSet1 = new IDCOLAdvanceModule.IDCOLAdvanceDBDataSet();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idcolAdvanceDBDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.nextButton);
            this.groupBox1.Controls.Add(this.travelNatureComboBox);
            this.groupBox1.Controls.Add(this.advanceOrExpenseNatureLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 127);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(265, 71);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseSelectable = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // travelNatureComboBox
            // 
            this.travelNatureComboBox.FormattingEnabled = true;
            this.travelNatureComboBox.ItemHeight = 23;
            this.travelNatureComboBox.Location = new System.Drawing.Point(148, 36);
            this.travelNatureComboBox.Name = "travelNatureComboBox";
            this.travelNatureComboBox.Size = new System.Drawing.Size(192, 29);
            this.travelNatureComboBox.TabIndex = 0;
            this.travelNatureComboBox.UseSelectable = true;
            // 
            // advanceOrExpenseNatureLabel
            // 
            this.advanceOrExpenseNatureLabel.AutoSize = true;
            this.advanceOrExpenseNatureLabel.Location = new System.Drawing.Point(33, 41);
            this.advanceOrExpenseNatureLabel.Name = "advanceOrExpenseNatureLabel";
            this.advanceOrExpenseNatureLabel.Size = new System.Drawing.Size(100, 19);
            this.advanceOrExpenseNatureLabel.TabIndex = 2;
            this.advanceOrExpenseNatureLabel.Text = "Advance nature";
            // 
            // idcolAdvanceDBDataSet1
            // 
            this.idcolAdvanceDBDataSet1.DataSetName = "IDCOLAdvanceDBDataSet";
            this.idcolAdvanceDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SelectAdvanceOrExpenseNatureUI
            // 
            this.AcceptButton = this.nextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(410, 155);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "SelectAdvanceOrExpenseNatureUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Advance Nature";
            this.Load += new System.EventHandler(this.SelectTravelNatureUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idcolAdvanceDBDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton nextButton;
        private MetroFramework.Controls.MetroComboBox travelNatureComboBox;
        public MetroFramework.Controls.MetroLabel advanceOrExpenseNatureLabel;
        private IDCOLAdvanceDBDataSet idcolAdvanceDBDataSet1;
    }
}