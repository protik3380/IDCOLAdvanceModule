namespace IDCOLAdvanceModule.UI.Settings
{
    partial class AccountConfigurationSettingsUI
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.advanceCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.accountConfigurationDataGridView = new System.Windows.Forms.DataGridView();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.isDefaultAccountCodeCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.accountTypeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.resetButton = new MetroFramework.Controls.MetroButton();
            this.accountCodeComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accountConfigurationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(100, 28);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(118, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Advance Category";
            // 
            // advanceCategoryComboBox
            // 
            this.advanceCategoryComboBox.FormattingEnabled = true;
            this.advanceCategoryComboBox.ItemHeight = 23;
            this.advanceCategoryComboBox.Location = new System.Drawing.Point(238, 26);
            this.advanceCategoryComboBox.Name = "advanceCategoryComboBox";
            this.advanceCategoryComboBox.Size = new System.Drawing.Size(349, 29);
            this.advanceCategoryComboBox.TabIndex = 1;
            this.advanceCategoryComboBox.UseSelectable = true;
            this.advanceCategoryComboBox.SelectionChangeCommitted += new System.EventHandler(this.advanceCategoryComboBox_SelectionChangeCommitted);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(126, 100);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(92, 19);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "Account Code";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.accountConfigurationDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(706, 427);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Configuration";
            // 
            // accountConfigurationDataGridView
            // 
            this.accountConfigurationDataGridView.AllowUserToAddRows = false;
            this.accountConfigurationDataGridView.AllowUserToDeleteRows = false;
            this.accountConfigurationDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.accountConfigurationDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.accountConfigurationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accountConfigurationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.removeColumn,
            this.categoryColumn,
            this.accountTypeColumn,
            this.accountCodeColumn});
            this.accountConfigurationDataGridView.EnableHeadersVisualStyles = false;
            this.accountConfigurationDataGridView.Location = new System.Drawing.Point(6, 23);
            this.accountConfigurationDataGridView.MultiSelect = false;
            this.accountConfigurationDataGridView.Name = "accountConfigurationDataGridView";
            this.accountConfigurationDataGridView.ReadOnly = true;
            this.accountConfigurationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.accountConfigurationDataGridView.Size = new System.Drawing.Size(694, 387);
            this.accountConfigurationDataGridView.TabIndex = 7;
            this.accountConfigurationDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.accountConfigurationDataGridView_CellContentClick);
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.editColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.editColumn.Width = 60;
            // 
            // removeColumn
            // 
            this.removeColumn.HeaderText = "";
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.ReadOnly = true;
            this.removeColumn.Width = 70;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Advance Category";
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            this.categoryColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.categoryColumn.Width = 200;
            // 
            // accountTypeColumn
            // 
            this.accountTypeColumn.HeaderText = "Account Type";
            this.accountTypeColumn.Name = "accountTypeColumn";
            this.accountTypeColumn.ReadOnly = true;
            this.accountTypeColumn.Width = 170;
            // 
            // accountCodeColumn
            // 
            this.accountCodeColumn.HeaderText = "Account Code";
            this.accountCodeColumn.Name = "accountCodeColumn";
            this.accountCodeColumn.ReadOnly = true;
            this.accountCodeColumn.Width = 150;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(517, 157);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(70, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // isDefaultAccountCodeCheckBox
            // 
            this.isDefaultAccountCodeCheckBox.AutoSize = true;
            this.isDefaultAccountCodeCheckBox.Location = new System.Drawing.Point(330, 134);
            this.isDefaultAccountCodeCheckBox.Name = "isDefaultAccountCodeCheckBox";
            this.isDefaultAccountCodeCheckBox.Size = new System.Drawing.Size(114, 15);
            this.isDefaultAccountCodeCheckBox.TabIndex = 4;
            this.isDefaultAccountCodeCheckBox.Text = "Default Account?";
            this.isDefaultAccountCodeCheckBox.UseSelectable = true;
            // 
            // accountTypeComboBox
            // 
            this.accountTypeComboBox.FormattingEnabled = true;
            this.accountTypeComboBox.ItemHeight = 23;
            this.accountTypeComboBox.Location = new System.Drawing.Point(238, 62);
            this.accountTypeComboBox.Name = "accountTypeComboBox";
            this.accountTypeComboBox.Size = new System.Drawing.Size(349, 29);
            this.accountTypeComboBox.TabIndex = 4;
            this.accountTypeComboBox.UseSelectable = true;
            this.accountTypeComboBox.SelectionChangeCommitted += new System.EventHandler(this.accountTypeComboBox_SelectionChangeCommitted);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(131, 64);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(87, 19);
            this.metroLabel3.TabIndex = 0;
            this.metroLabel3.Text = "Account Type";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(436, 157);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 5;
            this.resetButton.Text = "Reset";
            this.resetButton.UseSelectable = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // accountCodeComboBox
            // 
            this.accountCodeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.accountCodeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.accountCodeComboBox.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountCodeComboBox.FormattingEnabled = true;
            this.accountCodeComboBox.Location = new System.Drawing.Point(238, 98);
            this.accountCodeComboBox.Name = "accountCodeComboBox";
            this.accountCodeComboBox.Size = new System.Drawing.Size(349, 24);
            this.accountCodeComboBox.TabIndex = 6;
            // 
            // AccountConfigurationSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(730, 638);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.accountCodeComboBox);
            this.Controls.Add(this.accountTypeComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.advanceCategoryComboBox);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.isDefaultAccountCodeCheckBox);
            this.Controls.Add(this.resetButton);
            this.Name = "AccountConfigurationSettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Configuration Settings";
            this.Load += new System.EventHandler(this.AccountConfigurationSettingUI_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accountConfigurationDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox advanceCategoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroCheckBox isDefaultAccountCodeCheckBox;
        private MetroFramework.Controls.MetroButton saveButton;
        private MetroFramework.Controls.MetroComboBox accountTypeComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton resetButton;
        private System.Windows.Forms.ComboBox accountCodeComboBox;
        private System.Windows.Forms.DataGridView accountConfigurationDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountCodeColumn;
    }
}