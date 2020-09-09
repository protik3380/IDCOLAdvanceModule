namespace IDCOLAdvanceModule.UI
{
    partial class CurrencyConversionRateDetailUI
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addButton = new MetroFramework.Controls.MetroButton();
            this.conversionRateTextBox = new System.Windows.Forms.MaskedTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.toCurrencyComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.fromCurrencyComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.currencyConversionDataGridView = new System.Windows.Forms.DataGridView();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conversionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.currencyConversionContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currencyConversionDataGridView)).BeginInit();
            this.currencyConversionContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.conversionRateTextBox);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.toCurrencyComboBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.fromCurrencyComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(77, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details of Currency Conversion Rate";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(266, 122);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "Add";
            this.addButton.UseSelectable = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // conversionRateTextBox
            // 
            this.conversionRateTextBox.Location = new System.Drawing.Point(150, 93);
            this.conversionRateTextBox.Name = "conversionRateTextBox";
            this.conversionRateTextBox.Size = new System.Drawing.Size(191, 23);
            this.conversionRateTextBox.TabIndex = 9;
            this.conversionRateTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.conversionRateTextBox_KeyPress);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(28, 97);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(104, 19);
            this.metroLabel3.TabIndex = 8;
            this.metroLabel3.Text = "Conversion Rate";
            // 
            // toCurrencyComboBox
            // 
            this.toCurrencyComboBox.FormattingEnabled = true;
            this.toCurrencyComboBox.ItemHeight = 23;
            this.toCurrencyComboBox.Location = new System.Drawing.Point(149, 57);
            this.toCurrencyComboBox.Name = "toCurrencyComboBox";
            this.toCurrencyComboBox.Size = new System.Drawing.Size(192, 29);
            this.toCurrencyComboBox.TabIndex = 6;
            this.toCurrencyComboBox.UseSelectable = true;
            this.toCurrencyComboBox.SelectionChangeCommitted += new System.EventHandler(this.toCurrencyComboBox_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(54, 62);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(78, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "To Currency";
            // 
            // fromCurrencyComboBox
            // 
            this.fromCurrencyComboBox.FormattingEnabled = true;
            this.fromCurrencyComboBox.ItemHeight = 23;
            this.fromCurrencyComboBox.Location = new System.Drawing.Point(149, 22);
            this.fromCurrencyComboBox.Name = "fromCurrencyComboBox";
            this.fromCurrencyComboBox.Size = new System.Drawing.Size(192, 29);
            this.fromCurrencyComboBox.TabIndex = 4;
            this.fromCurrencyComboBox.UseSelectable = true;
            this.fromCurrencyComboBox.SelectionChangeCommitted += new System.EventHandler(this.fromCurrencyComboBox_SelectionChangeCommitted);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(35, 27);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(97, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "From Currency";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.currencyConversionDataGridView);
            this.groupBox2.Controls.Add(this.saveButton);
            this.groupBox2.Controls.Add(this.cancelButton);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(18, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(544, 262);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "History of Conversion Rate";
            // 
            // currencyConversionDataGridView
            // 
            this.currencyConversionDataGridView.AllowUserToAddRows = false;
            this.currencyConversionDataGridView.AllowUserToDeleteRows = false;
            this.currencyConversionDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.currencyConversionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.currencyConversionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.currencyConversionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.removeColumn,
            this.serialColumn,
            this.fromColumn,
            this.toColumn,
            this.conversionColumn});
            this.currencyConversionDataGridView.EnableHeadersVisualStyles = false;
            this.currencyConversionDataGridView.Location = new System.Drawing.Point(4, 23);
            this.currencyConversionDataGridView.MultiSelect = false;
            this.currencyConversionDataGridView.Name = "currencyConversionDataGridView";
            this.currencyConversionDataGridView.ReadOnly = true;
            this.currencyConversionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.currencyConversionDataGridView.Size = new System.Drawing.Size(535, 202);
            this.currencyConversionDataGridView.TabIndex = 13;
            this.currencyConversionDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.currencyConversionDataGridView_CellContentClick);
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
            this.removeColumn.Width = 70;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Width = 60;
            // 
            // fromColumn
            // 
            this.fromColumn.HeaderText = "From(Currency)";
            this.fromColumn.Name = "fromColumn";
            this.fromColumn.ReadOnly = true;
            // 
            // toColumn
            // 
            this.toColumn.HeaderText = "To(Currency)";
            this.toColumn.Name = "toColumn";
            this.toColumn.ReadOnly = true;
            // 
            // conversionColumn
            // 
            this.conversionColumn.HeaderText = "Conversion Rate";
            this.conversionColumn.Name = "conversionColumn";
            this.conversionColumn.ReadOnly = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(463, 231);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(382, 231);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseSelectable = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // currencyConversionContextMenu
            // 
            this.currencyConversionContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.currencyConversionContextMenu.Name = "currencyConversionContextMenu";
            this.currencyConversionContextMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // CurrencyConversionRateDetailUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(579, 483);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CurrencyConversionRateDetailUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Currency Conversion Rate Detail";
            this.Load += new System.EventHandler(this.CurrencyConversionRateDetailUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currencyConversionDataGridView)).EndInit();
            this.currencyConversionContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox conversionRateTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox toCurrencyComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox fromCurrencyComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton addButton;
        private MetroFramework.Controls.MetroButton saveButton;
        private MetroFramework.Controls.MetroButton cancelButton;
        private MetroFramework.Controls.MetroContextMenu currencyConversionContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.DataGridView currencyConversionDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn conversionColumn;
    }
}