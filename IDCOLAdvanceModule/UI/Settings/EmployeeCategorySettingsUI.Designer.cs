namespace IDCOLAdvanceModule.UI.Settings
{
    partial class EmployeeCategorySettingsUI
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
            this.employeeCategorySettingsDataGridView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.designationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.employeeCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeCategorySettingsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.employeeCategorySettingsDataGridView);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.employeeCategoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 461);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // employeeCategorySettingsDataGridView
            // 
            this.employeeCategorySettingsDataGridView.AllowUserToAddRows = false;
            this.employeeCategorySettingsDataGridView.AllowUserToDeleteRows = false;
            this.employeeCategorySettingsDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeCategorySettingsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.employeeCategorySettingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employeeCategorySettingsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.designationColumn});
            this.employeeCategorySettingsDataGridView.EnableHeadersVisualStyles = false;
            this.employeeCategorySettingsDataGridView.Location = new System.Drawing.Point(8, 67);
            this.employeeCategorySettingsDataGridView.MultiSelect = false;
            this.employeeCategorySettingsDataGridView.Name = "employeeCategorySettingsDataGridView";
            this.employeeCategorySettingsDataGridView.ReadOnly = true;
            this.employeeCategorySettingsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeCategorySettingsDataGridView.Size = new System.Drawing.Size(402, 349);
            this.employeeCategorySettingsDataGridView.TabIndex = 4;
            this.employeeCategorySettingsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.employeeCategorySettingsDataGridView_CellContentClick);
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.checkColumn.Width = 25;
            // 
            // designationColumn
            // 
            this.designationColumn.HeaderText = "Designation";
            this.designationColumn.Name = "designationColumn";
            this.designationColumn.ReadOnly = true;
            this.designationColumn.Width = 300;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(321, 422);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // employeeCategoryComboBox
            // 
            this.employeeCategoryComboBox.FormattingEnabled = true;
            this.employeeCategoryComboBox.ItemHeight = 23;
            this.employeeCategoryComboBox.Location = new System.Drawing.Point(136, 26);
            this.employeeCategoryComboBox.Name = "employeeCategoryComboBox";
            this.employeeCategoryComboBox.Size = new System.Drawing.Size(178, 29);
            this.employeeCategoryComboBox.TabIndex = 1;
            this.employeeCategoryComboBox.UseSelectable = true;
            this.employeeCategoryComboBox.SelectionChangeCommitted += new System.EventHandler(this.categoryComboBox_SelectionChangeCommitted);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(66, 29);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(64, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Category";
            // 
            // EmployeeCategorySettingsUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(449, 499);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "EmployeeCategorySettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Category Settings";
            this.Load += new System.EventHandler(this.EmployeeCategorySettingsUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeCategorySettingsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox employeeCategoryComboBox;
        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.DataGridView employeeCategorySettingsDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationColumn;
    }
}