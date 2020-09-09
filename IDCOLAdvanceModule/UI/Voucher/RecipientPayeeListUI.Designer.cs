namespace IDCOLAdvanceModule.UI.Voucher
{
    partial class RecipientPayeeListUI
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
            this.recipientPayeeDataGridView = new System.Windows.Forms.DataGridView();
            this.goColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipientPayeeDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.recipientPayeeDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 325);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recipient/Payee List";
            // 
            // recipientPayeeDataGridView
            // 
            this.recipientPayeeDataGridView.AllowUserToAddRows = false;
            this.recipientPayeeDataGridView.AllowUserToDeleteRows = false;
            this.recipientPayeeDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.recipientPayeeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.recipientPayeeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recipientPayeeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.goColumn,
            this.nameColumn,
            this.voucherStatusColumn});
            this.recipientPayeeDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recipientPayeeDataGridView.EnableHeadersVisualStyles = false;
            this.recipientPayeeDataGridView.Location = new System.Drawing.Point(3, 19);
            this.recipientPayeeDataGridView.MultiSelect = false;
            this.recipientPayeeDataGridView.Name = "recipientPayeeDataGridView";
            this.recipientPayeeDataGridView.ReadOnly = true;
            this.recipientPayeeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.recipientPayeeDataGridView.Size = new System.Drawing.Size(453, 303);
            this.recipientPayeeDataGridView.TabIndex = 2;
            this.recipientPayeeDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.recipientPayeeDataGridView_CellContentClick);
            this.recipientPayeeDataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.recipientPayeeDataGridView_MouseDoubleClick);
            // 
            // goColumn
            // 
            this.goColumn.HeaderText = "";
            this.goColumn.Name = "goColumn";
            this.goColumn.ReadOnly = true;
            this.goColumn.Width = 60;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Width = 230;
            // 
            // voucherStatusColumn
            // 
            this.voucherStatusColumn.HeaderText = "Voucher Status";
            this.voucherStatusColumn.Name = "voucherStatusColumn";
            this.voucherStatusColumn.ReadOnly = true;
            this.voucherStatusColumn.Width = 120;
            // 
            // RecipientPayeeListUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(484, 350);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "RecipientPayeeListUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recipient/Payee List";
            this.Load += new System.EventHandler(this.RecipientPayeeListUI_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.recipientPayeeDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView recipientPayeeDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn goColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherStatusColumn;
    }
}