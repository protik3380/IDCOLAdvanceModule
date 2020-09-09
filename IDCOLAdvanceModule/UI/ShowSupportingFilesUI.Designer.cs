namespace IDCOLAdvanceModule.UI
{
    partial class ShowSupportingFilesUI
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
            this.supportingFileDataGridView = new System.Windows.Forms.DataGridView();
            this.closeButton = new MetroFramework.Controls.MetroButton();
            this.downloadButton = new MetroFramework.Controls.MetroButton();
            this.supportingFilesContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supportingFileDataGridView)).BeginInit();
            this.supportingFilesContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.supportingFileDataGridView);
            this.groupBox1.Controls.Add(this.closeButton);
            this.groupBox1.Controls.Add(this.downloadButton);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 250);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // supportingFileDataGridView
            // 
            this.supportingFileDataGridView.AllowUserToAddRows = false;
            this.supportingFileDataGridView.AllowUserToDeleteRows = false;
            this.supportingFileDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.supportingFileDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.supportingFileDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.supportingFileDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.downloadColumn,
            this.fileNameColumn});
            this.supportingFileDataGridView.EnableHeadersVisualStyles = false;
            this.supportingFileDataGridView.Location = new System.Drawing.Point(7, 18);
            this.supportingFileDataGridView.MultiSelect = false;
            this.supportingFileDataGridView.Name = "supportingFileDataGridView";
            this.supportingFileDataGridView.ReadOnly = true;
            this.supportingFileDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.supportingFileDataGridView.Size = new System.Drawing.Size(472, 182);
            this.supportingFileDataGridView.TabIndex = 3;
            this.supportingFileDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.supportingFileDataGridView_CellContentClick);
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
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(352, 209);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 1;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseSelectable = true;
            this.downloadButton.Visible = false;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // supportingFilesContextMenu
            // 
            this.supportingFilesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem});
            this.supportingFilesContextMenu.Name = "uploadedFilesContextMenu";
            this.supportingFilesContextMenu.Size = new System.Drawing.Size(129, 26);
            this.supportingFilesContextMenu.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            // 
            // downloadColumn
            // 
            this.downloadColumn.HeaderText = "";
            this.downloadColumn.Name = "downloadColumn";
            this.downloadColumn.ReadOnly = true;
            this.downloadColumn.Width = 90;
            // 
            // fileNameColumn
            // 
            this.fileNameColumn.HeaderText = "File Name";
            this.fileNameColumn.Name = "fileNameColumn";
            this.fileNameColumn.ReadOnly = true;
            this.fileNameColumn.Width = 330;
            // 
            // ShowSupportingFilesUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(509, 264);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ShowSupportingFilesUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Supporting File(s)";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.supportingFileDataGridView)).EndInit();
            this.supportingFilesContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton closeButton;
        private MetroFramework.Controls.MetroButton downloadButton;
        private MetroFramework.Controls.MetroContextMenu supportingFilesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.DataGridView supportingFileDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn downloadColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameColumn;
    }
}