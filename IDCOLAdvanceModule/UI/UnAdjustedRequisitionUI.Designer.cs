namespace IDCOLAdvanceModule.UI
{
    partial class UnadjustedRequisitionUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.requisitionContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requisitionDataGridView = new System.Windows.Forms.DataGridView();
            this.viewColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appliedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advanceAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.requisitionContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.requisitionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.requisitionDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(991, 258);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // requisitionContextMenu
            // 
            this.requisitionContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.requisitionContextMenu.Name = "requisitionContextMenu";
            this.requisitionContextMenu.Size = new System.Drawing.Size(121, 26);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.viewToolStripMenuItem.Text = "360 View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // requisitionDataGridView
            // 
            this.requisitionDataGridView.AllowUserToAddRows = false;
            this.requisitionDataGridView.AllowUserToDeleteRows = false;
            this.requisitionDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.requisitionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.requisitionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requisitionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.viewColumn,
            this.serialColumn,
            this.employeeColumn,
            this.categoryColumn,
            this.appliedDateColumn,
            this.fromColumn,
            this.toColumn,
            this.advanceAmountColumn});
            this.requisitionDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requisitionDataGridView.EnableHeadersVisualStyles = false;
            this.requisitionDataGridView.Location = new System.Drawing.Point(3, 16);
            this.requisitionDataGridView.MultiSelect = false;
            this.requisitionDataGridView.Name = "requisitionDataGridView";
            this.requisitionDataGridView.ReadOnly = true;
            this.requisitionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.requisitionDataGridView.Size = new System.Drawing.Size(985, 239);
            this.requisitionDataGridView.TabIndex = 8;
            this.requisitionDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.requisitionDataGridView_CellContentClick);
            // 
            // viewColumn
            // 
            this.viewColumn.HeaderText = "";
            this.viewColumn.Name = "viewColumn";
            this.viewColumn.ReadOnly = true;
            this.viewColumn.Width = 90;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Width = 60;
            // 
            // employeeColumn
            // 
            this.employeeColumn.HeaderText = "Employee Name";
            this.employeeColumn.Name = "employeeColumn";
            this.employeeColumn.ReadOnly = true;
            this.employeeColumn.Width = 150;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Category";
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            // 
            // appliedDateColumn
            // 
            this.appliedDateColumn.HeaderText = "Applied Date";
            this.appliedDateColumn.Name = "appliedDateColumn";
            this.appliedDateColumn.ReadOnly = true;
            this.appliedDateColumn.Width = 150;
            // 
            // fromColumn
            // 
            this.fromColumn.HeaderText = "From Date";
            this.fromColumn.Name = "fromColumn";
            this.fromColumn.ReadOnly = true;
            this.fromColumn.Width = 120;
            // 
            // toColumn
            // 
            this.toColumn.HeaderText = "To date";
            this.toColumn.Name = "toColumn";
            this.toColumn.ReadOnly = true;
            this.toColumn.Width = 120;
            // 
            // advanceAmountColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.advanceAmountColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.advanceAmountColumn.HeaderText = "Advance Amount";
            this.advanceAmountColumn.Name = "advanceAmountColumn";
            this.advanceAmountColumn.ReadOnly = true;
            this.advanceAmountColumn.Width = 150;
            // 
            // UnadjustedRequisitionUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1015, 282);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "UnadjustedRequisitionUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Previous Pending Requisition(s)";
            this.groupBox1.ResumeLayout(false);
            this.requisitionContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.requisitionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroContextMenu requisitionContextMenu;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.DataGridView requisitionDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn viewColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn appliedDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn advanceAmountColumn;
    }
}