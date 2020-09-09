namespace IDCOLAdvanceModule.UI.Settings
{
    partial class ApprovalPanelTypeSetupUI
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
            this.resetButton = new MetroFramework.Controls.MetroButton();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.panelTypeNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.approvalPeanelTypeGridView = new System.Windows.Forms.DataGridView();
            this.panelTypeContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.sLColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvalPeanelTypeGridView)).BeginInit();
            this.panelTypeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resetButton);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.panelTypeNameTextBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(179, 64);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 3;
            this.resetButton.Text = "Reset";
            this.resetButton.UseSelectable = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(260, 64);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panelTypeNameTextBox
            // 
            // 
            // 
            // 
            this.panelTypeNameTextBox.CustomButton.Image = null;
            this.panelTypeNameTextBox.CustomButton.Location = new System.Drawing.Point(146, 1);
            this.panelTypeNameTextBox.CustomButton.Name = "";
            this.panelTypeNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.panelTypeNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.panelTypeNameTextBox.CustomButton.TabIndex = 1;
            this.panelTypeNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelTypeNameTextBox.CustomButton.UseSelectable = true;
            this.panelTypeNameTextBox.CustomButton.Visible = false;
            this.panelTypeNameTextBox.Lines = new string[0];
            this.panelTypeNameTextBox.Location = new System.Drawing.Point(167, 35);
            this.panelTypeNameTextBox.MaxLength = 32767;
            this.panelTypeNameTextBox.Name = "panelTypeNameTextBox";
            this.panelTypeNameTextBox.PasswordChar = '\0';
            this.panelTypeNameTextBox.PromptText = "Panel type name";
            this.panelTypeNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.panelTypeNameTextBox.SelectedText = "";
            this.panelTypeNameTextBox.SelectionLength = 0;
            this.panelTypeNameTextBox.SelectionStart = 0;
            this.panelTypeNameTextBox.ShortcutsEnabled = true;
            this.panelTypeNameTextBox.Size = new System.Drawing.Size(168, 23);
            this.panelTypeNameTextBox.TabIndex = 0;
            this.panelTypeNameTextBox.UseSelectable = true;
            this.panelTypeNameTextBox.WaterMark = "Panel type name";
            this.panelTypeNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.panelTypeNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(35, 37);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(106, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Panel type name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.approvalPeanelTypeGridView);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Approval Panel Type";
            // 
            // approvalPeanelTypeGridView
            // 
            this.approvalPeanelTypeGridView.AllowUserToAddRows = false;
            this.approvalPeanelTypeGridView.AllowUserToDeleteRows = false;
            this.approvalPeanelTypeGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.approvalPeanelTypeGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.approvalPeanelTypeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.approvalPeanelTypeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.sLColumn,
            this.panelTypeColumn});
            this.approvalPeanelTypeGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.approvalPeanelTypeGridView.EnableHeadersVisualStyles = false;
            this.approvalPeanelTypeGridView.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.approvalPeanelTypeGridView.Location = new System.Drawing.Point(3, 19);
            this.approvalPeanelTypeGridView.MultiSelect = false;
            this.approvalPeanelTypeGridView.Name = "approvalPeanelTypeGridView";
            this.approvalPeanelTypeGridView.ReadOnly = true;
            this.approvalPeanelTypeGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.approvalPeanelTypeGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.approvalPeanelTypeGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.approvalPeanelTypeGridView.Size = new System.Drawing.Size(363, 143);
            this.approvalPeanelTypeGridView.TabIndex = 0;
            this.approvalPeanelTypeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.approveanelTypeGridView_CellContentClick);
            // 
            // panelTypeContextMenu
            // 
            this.panelTypeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.panelTypeContextMenu.Name = "panelTypeContextMenu";
            this.panelTypeContextMenu.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
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
            // sLColumn
            // 
            this.sLColumn.HeaderText = "Serial";
            this.sLColumn.Name = "sLColumn";
            this.sLColumn.ReadOnly = true;
            this.sLColumn.Width = 70;
            // 
            // panelTypeColumn
            // 
            this.panelTypeColumn.HeaderText = "Panel Type";
            this.panelTypeColumn.Name = "panelTypeColumn";
            this.panelTypeColumn.ReadOnly = true;
            this.panelTypeColumn.Width = 170;
            // 
            // ApprovalPanelTypeSetupUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(391, 306);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ApprovalPanelTypeSetupUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Approval Panel Type Setup";
            this.Load += new System.EventHandler(this.ApprovalPanelTypeSetupUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.approvalPeanelTypeGridView)).EndInit();
            this.panelTypeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox panelTypeNameTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroContextMenu panelTypeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.DataGridView approvalPeanelTypeGridView;
        private MetroFramework.Controls.MetroButton resetButton;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn panelTypeColumn;
    }
}