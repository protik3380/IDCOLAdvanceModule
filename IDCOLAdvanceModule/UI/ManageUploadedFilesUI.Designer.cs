namespace IDCOLAdvanceModule.UI
{
    partial class ManageUploadedFilesUI
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
            this.closeButton = new MetroFramework.Controls.MetroButton();
            this.uploadedFilesContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadedFilesDataGridView = new System.Windows.Forms.DataGridView();
            this.downloadColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.uploadedFilesContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadedFilesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uploadedFilesDataGridView);
            this.groupBox1.Controls.Add(this.closeButton);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 248);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(16, 209);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseSelectable = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // uploadedFilesContextMenu
            // 
            this.uploadedFilesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.downloadToolStripMenuItem});
            this.uploadedFilesContextMenu.Name = "uploadedFilesContextMenu";
            this.uploadedFilesContextMenu.Size = new System.Drawing.Size(129, 48);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // uploadedFilesDataGridView
            // 
            this.uploadedFilesDataGridView.AllowUserToAddRows = false;
            this.uploadedFilesDataGridView.AllowUserToDeleteRows = false;
            this.uploadedFilesDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uploadedFilesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.uploadedFilesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uploadedFilesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.downloadColumn,
            this.removeColumn,
            this.fileNameColumn});
            this.uploadedFilesDataGridView.EnableHeadersVisualStyles = false;
            this.uploadedFilesDataGridView.Location = new System.Drawing.Point(7, 15);
            this.uploadedFilesDataGridView.MultiSelect = false;
            this.uploadedFilesDataGridView.Name = "uploadedFilesDataGridView";
            this.uploadedFilesDataGridView.ReadOnly = true;
            this.uploadedFilesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.uploadedFilesDataGridView.Size = new System.Drawing.Size(460, 188);
            this.uploadedFilesDataGridView.TabIndex = 3;
            this.uploadedFilesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.uploadedFilesDataGridView_CellContentClick);
            // 
            // downloadColumn
            // 
            this.downloadColumn.HeaderText = "";
            this.downloadColumn.Name = "downloadColumn";
            this.downloadColumn.ReadOnly = true;
            this.downloadColumn.Width = 90;
            // 
            // removeColumn
            // 
            this.removeColumn.HeaderText = "";
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.ReadOnly = true;
            this.removeColumn.Width = 70;
            // 
            // fileNameColumn
            // 
            this.fileNameColumn.HeaderText = "File Name";
            this.fileNameColumn.Name = "fileNameColumn";
            this.fileNameColumn.ReadOnly = true;
            this.fileNameColumn.Width = 250;
            // 
            // ManageUploadedFilesUI
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(497, 266);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ManageUploadedFilesUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Uploaded File(s)";
            this.groupBox1.ResumeLayout(false);
            this.uploadedFilesContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uploadedFilesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroContextMenu uploadedFilesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private MetroFramework.Controls.MetroButton closeButton;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.DataGridView uploadedFilesDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn downloadColumn;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameColumn;
    }
}