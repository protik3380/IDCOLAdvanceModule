namespace IDCOLAdvanceModule.UI.Settings
{
    partial class GeneralAccountConfigurationSettingsUI
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
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.accountCodeComboBox = new System.Windows.Forms.ComboBox();
            this.accountTypeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.isDefaultAccountCodeCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.generalAccountConfigurationDataGridView = new System.Windows.Forms.DataGridView();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.accountTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resetButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generalAccountConfigurationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(517, 108);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(70, 23);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // accountCodeComboBox
            // 
            this.accountCodeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.accountCodeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.accountCodeComboBox.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountCodeComboBox.FormattingEnabled = true;
            this.accountCodeComboBox.Location = new System.Drawing.Point(183, 50);
            this.accountCodeComboBox.Name = "accountCodeComboBox";
            this.accountCodeComboBox.Size = new System.Drawing.Size(404, 24);
            this.accountCodeComboBox.TabIndex = 16;
            // 
            // accountTypeComboBox
            // 
            this.accountTypeComboBox.FormattingEnabled = true;
            this.accountTypeComboBox.ItemHeight = 23;
            this.accountTypeComboBox.Location = new System.Drawing.Point(183, 13);
            this.accountTypeComboBox.Name = "accountTypeComboBox";
            this.accountTypeComboBox.Size = new System.Drawing.Size(404, 29);
            this.accountTypeComboBox.TabIndex = 12;
            this.accountTypeComboBox.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(76, 13);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(87, 19);
            this.metroLabel3.TabIndex = 7;
            this.metroLabel3.Text = "Account Type";
            // 
            // isDefaultAccountCodeCheckBox
            // 
            this.isDefaultAccountCodeCheckBox.AutoSize = true;
            this.isDefaultAccountCodeCheckBox.Location = new System.Drawing.Point(330, 84);
            this.isDefaultAccountCodeCheckBox.Name = "isDefaultAccountCodeCheckBox";
            this.isDefaultAccountCodeCheckBox.Size = new System.Drawing.Size(114, 15);
            this.isDefaultAccountCodeCheckBox.TabIndex = 13;
            this.isDefaultAccountCodeCheckBox.Text = "Default Account?";
            this.isDefaultAccountCodeCheckBox.UseSelectable = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.generalAccountConfigurationDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 427);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Configuration";
            // 
            // generalAccountConfigurationDataGridView
            // 
            this.generalAccountConfigurationDataGridView.AllowUserToAddRows = false;
            this.generalAccountConfigurationDataGridView.AllowUserToDeleteRows = false;
            this.generalAccountConfigurationDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.generalAccountConfigurationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.generalAccountConfigurationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.removeColumn,
            this.accountTypeColumn,
            this.accountCodeColumn});
            this.generalAccountConfigurationDataGridView.EnableHeadersVisualStyles = false;
            this.generalAccountConfigurationDataGridView.Location = new System.Drawing.Point(7, 23);
            this.generalAccountConfigurationDataGridView.MultiSelect = false;
            this.generalAccountConfigurationDataGridView.Name = "generalAccountConfigurationDataGridView";
            this.generalAccountConfigurationDataGridView.ReadOnly = true;
            this.generalAccountConfigurationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.generalAccountConfigurationDataGridView.Size = new System.Drawing.Size(671, 354);
            this.generalAccountConfigurationDataGridView.TabIndex = 7;
            this.generalAccountConfigurationDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.generalAccountConfigurationDataGridView_CellContentClick);
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 60;
            // 
            // removeColumn
            // 
            this.removeColumn.HeaderText = "";
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.ReadOnly = true;
            this.removeColumn.Width = 60;
            // 
            // accountTypeColumn
            // 
            this.accountTypeColumn.HeaderText = "Account Type";
            this.accountTypeColumn.Name = "accountTypeColumn";
            this.accountTypeColumn.ReadOnly = true;
            this.accountTypeColumn.Width = 250;
            // 
            // accountCodeColumn
            // 
            this.accountCodeColumn.HeaderText = "Account Code";
            this.accountCodeColumn.Name = "accountCodeColumn";
            this.accountCodeColumn.ReadOnly = true;
            this.accountCodeColumn.Width = 250;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(436, 108);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 14;
            this.resetButton.Text = "Reset";
            this.resetButton.UseSelectable = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(71, 53);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(92, 19);
            this.metroLabel2.TabIndex = 8;
            this.metroLabel2.Text = "Account Code";
            // 
            // GeneralAccountConfigurationSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(724, 596);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.accountCodeComboBox);
            this.Controls.Add(this.accountTypeComboBox);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.isDefaultAccountCodeCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.metroLabel2);
            this.Name = "GeneralAccountConfigurationSettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Configuration Settings";
            this.Load += new System.EventHandler(this.GeneralAccountConfigurationSettingsUI_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.generalAccountConfigurationDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.ComboBox accountCodeComboBox;
        private MetroFramework.Controls.MetroComboBox accountTypeComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroCheckBox isDefaultAccountCodeCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton resetButton;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.DataGridView generalAccountConfigurationDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountCodeColumn;
    }
}