namespace IDCOLAdvanceModule.UI.Voucher
{
    partial class PreviewVoucherUI
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
            this.previewVoucherDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.branchComboBox = new System.Windows.Forms.ComboBox();
            this.currencyComboBox = new System.Windows.Forms.ComboBox();
            this.branchLabel = new System.Windows.Forms.Label();
            this.conversionRateTextBox = new System.Windows.Forms.TextBox();
            this.bankComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VoucherEntryDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.chequeDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.bankNameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.voucherDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.chequeDateLabel = new System.Windows.Forms.Label();
            this.recipientOrPayeeLabel = new System.Windows.Forms.Label();
            this.chequeNoTextBox = new System.Windows.Forms.TextBox();
            this.chequeNoLabel = new System.Windows.Forms.Label();
            this.voucherOwnerTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.previewVoucherButton = new System.Windows.Forms.Button();
            this.sendVoucherButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.previewVoucherDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewVoucherDataGridView
            // 
            this.previewVoucherDataGridView.AllowUserToAddRows = false;
            this.previewVoucherDataGridView.AllowUserToDeleteRows = false;
            this.previewVoucherDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.previewVoucherDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.previewVoucherDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.previewVoucherDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewVoucherDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.previewVoucherDataGridView.Location = new System.Drawing.Point(3, 16);
            this.previewVoucherDataGridView.MultiSelect = false;
            this.previewVoucherDataGridView.Name = "previewVoucherDataGridView";
            this.previewVoucherDataGridView.RowTemplate.ReadOnly = true;
            this.previewVoucherDataGridView.Size = new System.Drawing.Size(779, 233);
            this.previewVoucherDataGridView.TabIndex = 0;
            this.previewVoucherDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.previewVoucherDataGridView_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "AccountCode";
            this.Column1.HeaderText = "AccountCode";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "AccountDesc";
            this.Column2.HeaderText = "AccountHead";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.DataPropertyName = "Description";
            this.Column3.HeaderText = "Description";
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DebitAmount";
            this.Column4.HeaderText = "Dr Amount";
            this.Column4.Name = "Column4";
            this.Column4.Width = 130;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CreditAmount";
            this.Column5.HeaderText = "Cr Amount";
            this.Column5.Name = "Column5";
            this.Column5.Width = 130;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.previewVoucherDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 274);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 252);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview Voucher";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.branchComboBox);
            this.groupBox2.Controls.Add(this.currencyComboBox);
            this.groupBox2.Controls.Add(this.branchLabel);
            this.groupBox2.Controls.Add(this.conversionRateTextBox);
            this.groupBox2.Controls.Add(this.bankComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.VoucherEntryDateTimePicker);
            this.groupBox2.Controls.Add(this.chequeDateDateTimePicker);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.bankNameLabel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.voucherDescriptionTextBox);
            this.groupBox2.Controls.Add(this.chequeDateLabel);
            this.groupBox2.Controls.Add(this.recipientOrPayeeLabel);
            this.groupBox2.Controls.Add(this.chequeNoTextBox);
            this.groupBox2.Controls.Add(this.chequeNoLabel);
            this.groupBox2.Controls.Add(this.voucherOwnerTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(779, 256);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Voucher Information";
            // 
            // branchComboBox
            // 
            this.branchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.branchComboBox.Enabled = false;
            this.branchComboBox.FormattingEnabled = true;
            this.branchComboBox.Location = new System.Drawing.Point(578, 111);
            this.branchComboBox.Name = "branchComboBox";
            this.branchComboBox.Size = new System.Drawing.Size(142, 21);
            this.branchComboBox.TabIndex = 12;
            // 
            // currencyComboBox
            // 
            this.currencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currencyComboBox.Enabled = false;
            this.currencyComboBox.FormattingEnabled = true;
            this.currencyComboBox.Location = new System.Drawing.Point(578, 19);
            this.currencyComboBox.Name = "currencyComboBox";
            this.currencyComboBox.Size = new System.Drawing.Size(122, 21);
            this.currencyComboBox.TabIndex = 4;
            this.currencyComboBox.SelectedIndexChanged += new System.EventHandler(this.currencyComboBox_SelectedIndexChanged);
            // 
            // branchLabel
            // 
            this.branchLabel.AutoSize = true;
            this.branchLabel.Location = new System.Drawing.Point(530, 116);
            this.branchLabel.Name = "branchLabel";
            this.branchLabel.Size = new System.Drawing.Size(41, 13);
            this.branchLabel.TabIndex = 14;
            this.branchLabel.Text = "Branch";
            // 
            // conversionRateTextBox
            // 
            this.conversionRateTextBox.Enabled = false;
            this.conversionRateTextBox.Location = new System.Drawing.Point(578, 45);
            this.conversionRateTextBox.Name = "conversionRateTextBox";
            this.conversionRateTextBox.Size = new System.Drawing.Size(87, 20);
            this.conversionRateTextBox.TabIndex = 5;
            this.conversionRateTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.conversionRateTextBox_KeyPress);
            // 
            // bankComboBox
            // 
            this.bankComboBox.Enabled = false;
            this.bankComboBox.FormattingEnabled = true;
            this.bankComboBox.Location = new System.Drawing.Point(578, 84);
            this.bankComboBox.Name = "bankComboBox";
            this.bankComboBox.Size = new System.Drawing.Size(142, 21);
            this.bankComboBox.TabIndex = 10;
            this.bankComboBox.SelectedIndexChanged += new System.EventHandler(this.bankComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(522, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Currency";
            // 
            // VoucherEntryDateTimePicker
            // 
            this.VoucherEntryDateTimePicker.Checked = false;
            this.VoucherEntryDateTimePicker.CustomFormat = "MM/dd/yyyy";
            this.VoucherEntryDateTimePicker.Enabled = false;
            this.VoucherEntryDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VoucherEntryDateTimePicker.Location = new System.Drawing.Point(115, 115);
            this.VoucherEntryDateTimePicker.Name = "VoucherEntryDateTimePicker";
            this.VoucherEntryDateTimePicker.ShowCheckBox = true;
            this.VoucherEntryDateTimePicker.Size = new System.Drawing.Size(143, 20);
            this.VoucherEntryDateTimePicker.TabIndex = 9;
            // 
            // chequeDateDateTimePicker
            // 
            this.chequeDateDateTimePicker.Checked = false;
            this.chequeDateDateTimePicker.CustomFormat = "MM/dd/yyyy";
            this.chequeDateDateTimePicker.Enabled = false;
            this.chequeDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.chequeDateDateTimePicker.Location = new System.Drawing.Point(579, 164);
            this.chequeDateDateTimePicker.Name = "chequeDateDateTimePicker";
            this.chequeDateDateTimePicker.ShowCheckBox = true;
            this.chequeDateDateTimePicker.Size = new System.Drawing.Size(143, 20);
            this.chequeDateDateTimePicker.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(485, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Conversion Rate";
            // 
            // bankNameLabel
            // 
            this.bankNameLabel.AutoSize = true;
            this.bankNameLabel.Location = new System.Drawing.Point(508, 87);
            this.bankNameLabel.Name = "bankNameLabel";
            this.bankNameLabel.Size = new System.Drawing.Size(63, 13);
            this.bankNameLabel.TabIndex = 13;
            this.bankNameLabel.Text = "Bank Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Voucher Entry Date";
            // 
            // voucherDescriptionTextBox
            // 
            this.voucherDescriptionTextBox.Enabled = false;
            this.voucherDescriptionTextBox.Location = new System.Drawing.Point(115, 48);
            this.voucherDescriptionTextBox.Multiline = true;
            this.voucherDescriptionTextBox.Name = "voucherDescriptionTextBox";
            this.voucherDescriptionTextBox.Size = new System.Drawing.Size(276, 61);
            this.voucherDescriptionTextBox.TabIndex = 4;
            // 
            // chequeDateLabel
            // 
            this.chequeDateLabel.AutoSize = true;
            this.chequeDateLabel.Location = new System.Drawing.Point(503, 168);
            this.chequeDateLabel.Name = "chequeDateLabel";
            this.chequeDateLabel.Size = new System.Drawing.Size(70, 13);
            this.chequeDateLabel.TabIndex = 11;
            this.chequeDateLabel.Text = "Cheque Date";
            // 
            // recipientOrPayeeLabel
            // 
            this.recipientOrPayeeLabel.AutoSize = true;
            this.recipientOrPayeeLabel.Location = new System.Drawing.Point(6, 52);
            this.recipientOrPayeeLabel.Name = "recipientOrPayeeLabel";
            this.recipientOrPayeeLabel.Size = new System.Drawing.Size(103, 13);
            this.recipientOrPayeeLabel.TabIndex = 0;
            this.recipientOrPayeeLabel.Text = "Voucher Description";
            // 
            // chequeNoTextBox
            // 
            this.chequeNoTextBox.Enabled = false;
            this.chequeNoTextBox.Location = new System.Drawing.Point(578, 138);
            this.chequeNoTextBox.Name = "chequeNoTextBox";
            this.chequeNoTextBox.Size = new System.Drawing.Size(144, 20);
            this.chequeNoTextBox.TabIndex = 7;
            // 
            // chequeNoLabel
            // 
            this.chequeNoLabel.AutoSize = true;
            this.chequeNoLabel.Location = new System.Drawing.Point(511, 141);
            this.chequeNoLabel.Name = "chequeNoLabel";
            this.chequeNoLabel.Size = new System.Drawing.Size(61, 13);
            this.chequeNoLabel.TabIndex = 8;
            this.chequeNoLabel.Text = "Cheque No";
            // 
            // voucherOwnerTextBox
            // 
            this.voucherOwnerTextBox.Enabled = false;
            this.voucherOwnerTextBox.Location = new System.Drawing.Point(115, 20);
            this.voucherOwnerTextBox.Name = "voucherOwnerTextBox";
            this.voucherOwnerTextBox.Size = new System.Drawing.Size(276, 20);
            this.voucherOwnerTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Recipient/Payee";
            // 
            // previewVoucherButton
            // 
            this.previewVoucherButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.previewVoucherButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.previewVoucherButton.Location = new System.Drawing.Point(672, 529);
            this.previewVoucherButton.Name = "previewVoucherButton";
            this.previewVoucherButton.Size = new System.Drawing.Size(119, 33);
            this.previewVoucherButton.TabIndex = 2;
            this.previewVoucherButton.Text = "Preview Voucher";
            this.previewVoucherButton.UseVisualStyleBackColor = false;
            this.previewVoucherButton.Click += new System.EventHandler(this.previewVoucherButton_Click);
            // 
            // sendVoucherButton
            // 
            this.sendVoucherButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendVoucherButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.sendVoucherButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.sendVoucherButton.Location = new System.Drawing.Point(545, 529);
            this.sendVoucherButton.Name = "sendVoucherButton";
            this.sendVoucherButton.Size = new System.Drawing.Size(121, 33);
            this.sendVoucherButton.TabIndex = 3;
            this.sendVoucherButton.Text = "Send Voucher";
            this.sendVoucherButton.UseVisualStyleBackColor = false;
            this.sendVoucherButton.Click += new System.EventHandler(this.sendVoucherButton_Click);
            // 
            // PreviewVoucherUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(809, 566);
            this.Controls.Add(this.sendVoucherButton);
            this.Controls.Add(this.previewVoucherButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PreviewVoucherUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview Voucher ";
            ((System.ComponentModel.ISupportInitialize)(this.previewVoucherDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView previewVoucherDataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox conversionRateTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox voucherDescriptionTextBox;
        private System.Windows.Forms.Label recipientOrPayeeLabel;
        private System.Windows.Forms.TextBox voucherOwnerTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button previewVoucherButton;
        private System.Windows.Forms.Button sendVoucherButton;
        private System.Windows.Forms.ComboBox currencyComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox branchComboBox;
        private System.Windows.Forms.Label branchLabel;
        private System.Windows.Forms.ComboBox bankComboBox;
        private System.Windows.Forms.DateTimePicker chequeDateDateTimePicker;
        private System.Windows.Forms.Label bankNameLabel;
        private System.Windows.Forms.Label chequeDateLabel;
        private System.Windows.Forms.TextBox chequeNoTextBox;
        private System.Windows.Forms.Label chequeNoLabel;
        private System.Windows.Forms.DateTimePicker VoucherEntryDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}