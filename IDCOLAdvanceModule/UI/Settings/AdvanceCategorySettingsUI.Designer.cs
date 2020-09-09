namespace IDCOLAdvanceModule.UI.Settings
{
    partial class AdvanceCategorySettingsUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.ceilingAmountTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.ceilingApplicableCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.advanceCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.advancedCategoryDataGridView = new System.Windows.Forms.DataGridView();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ceilingAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedCategoryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.advancedCategoryDataGridView);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.ceilingAmountTextBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.ceilingApplicableCheckBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.advanceCategoryComboBox);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 472);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(304, 162);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // ceilingAmountTextBox
            // 
            // 
            // 
            // 
            this.ceilingAmountTextBox.CustomButton.Image = null;
            this.ceilingAmountTextBox.CustomButton.Location = new System.Drawing.Point(178, 1);
            this.ceilingAmountTextBox.CustomButton.Name = "";
            this.ceilingAmountTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.ceilingAmountTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ceilingAmountTextBox.CustomButton.TabIndex = 1;
            this.ceilingAmountTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ceilingAmountTextBox.CustomButton.UseSelectable = true;
            this.ceilingAmountTextBox.CustomButton.Visible = false;
            this.ceilingAmountTextBox.Enabled = false;
            this.ceilingAmountTextBox.Lines = new string[0];
            this.ceilingAmountTextBox.Location = new System.Drawing.Point(179, 130);
            this.ceilingAmountTextBox.MaxLength = 32767;
            this.ceilingAmountTextBox.Name = "ceilingAmountTextBox";
            this.ceilingAmountTextBox.PasswordChar = '\0';
            this.ceilingAmountTextBox.PromptText = "Ceiling amount";
            this.ceilingAmountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ceilingAmountTextBox.SelectedText = "";
            this.ceilingAmountTextBox.SelectionLength = 0;
            this.ceilingAmountTextBox.SelectionStart = 0;
            this.ceilingAmountTextBox.ShortcutsEnabled = true;
            this.ceilingAmountTextBox.Size = new System.Drawing.Size(200, 23);
            this.ceilingAmountTextBox.TabIndex = 2;
            this.ceilingAmountTextBox.UseSelectable = true;
            this.ceilingAmountTextBox.WaterMark = "Ceiling amount";
            this.ceilingAmountTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ceilingAmountTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.ceilingAmountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ceilingAmountTextBox_KeyPress);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(61, 132);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(100, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Ceiling Amount";
            // 
            // ceilingApplicableCheckBox
            // 
            this.ceilingApplicableCheckBox.AutoSize = true;
            this.ceilingApplicableCheckBox.Location = new System.Drawing.Point(179, 97);
            this.ceilingApplicableCheckBox.Name = "ceilingApplicableCheckBox";
            this.ceilingApplicableCheckBox.Size = new System.Drawing.Size(119, 15);
            this.ceilingApplicableCheckBox.TabIndex = 1;
            this.ceilingApplicableCheckBox.Text = "Ceiling Applicable";
            this.ceilingApplicableCheckBox.UseSelectable = true;
            this.ceilingApplicableCheckBox.CheckedChanged += new System.EventHandler(this.ceilingApplicableCheckBox_CheckedChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(61, 35);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(64, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Category";
            // 
            // advanceCategoryComboBox
            // 
            this.advanceCategoryComboBox.FormattingEnabled = true;
            this.advanceCategoryComboBox.ItemHeight = 23;
            this.advanceCategoryComboBox.Location = new System.Drawing.Point(179, 32);
            this.advanceCategoryComboBox.Name = "advanceCategoryComboBox";
            this.advanceCategoryComboBox.Size = new System.Drawing.Size(200, 29);
            this.advanceCategoryComboBox.TabIndex = 0;
            this.advanceCategoryComboBox.UseSelectable = true;
            // 
            // advancedCategoryDataGridView
            // 
            this.advancedCategoryDataGridView.AllowUserToAddRows = false;
            this.advancedCategoryDataGridView.AllowUserToDeleteRows = false;
            this.advancedCategoryDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.advancedCategoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.advancedCategoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedCategoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.categoryColumn,
            this.ceilingAmountColumn});
            this.advancedCategoryDataGridView.EnableHeadersVisualStyles = false;
            this.advancedCategoryDataGridView.Location = new System.Drawing.Point(16, 191);
            this.advancedCategoryDataGridView.MultiSelect = false;
            this.advancedCategoryDataGridView.Name = "advancedCategoryDataGridView";
            this.advancedCategoryDataGridView.ReadOnly = true;
            this.advancedCategoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.advancedCategoryDataGridView.Size = new System.Drawing.Size(408, 275);
            this.advancedCategoryDataGridView.TabIndex = 6;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Category";
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            this.categoryColumn.Width = 200;
            // 
            // ceilingAmountColumn
            // 
            this.ceilingAmountColumn.HeaderText = "Ceiling Amount";
            this.ceilingAmountColumn.Name = "ceilingAmountColumn";
            this.ceilingAmountColumn.ReadOnly = true;
            this.ceilingAmountColumn.Width = 150;
            // 
            // AdvanceCategorySettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(476, 494);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "AdvanceCategorySettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advance Category Settings";
            this.Load += new System.EventHandler(this.AdvanceCategorySettingsUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedCategoryDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox ceilingAmountTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroCheckBox ceilingApplicableCheckBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox advanceCategoryComboBox;
        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.DataGridView advancedCategoryDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ceilingAmountColumn;
    }
}