namespace IDCOLAdvanceModule.UI.Settings
{
    partial class ApprovalLevelSetupUI
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
            this.approvalAuthorityCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.sourceOfFundVerifyCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.sourceOfFundEntryCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.headOfDepartmentCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.lineSupervisorCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.resetButton = new MetroFramework.Controls.MetroButton();
            this.addButton = new MetroFramework.Controls.MetroButton();
            this.levelOrderTextBox = new MetroFramework.Controls.MetroTextBox();
            this.approvalLevelTextBox = new MetroFramework.Controls.MetroTextBox();
            this.approvalPanelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.approvalLevelDataGridView = new System.Windows.Forms.DataGridView();
            this.approvalLevelContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upButton = new MetroFramework.Controls.MetroButton();
            this.downButton = new MetroFramework.Controls.MetroButton();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.levelNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lavelOrderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvalLevelDataGridView)).BeginInit();
            this.approvalLevelContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.approvalAuthorityCheckBox);
            this.groupBox1.Controls.Add(this.sourceOfFundVerifyCheckBox);
            this.groupBox1.Controls.Add(this.sourceOfFundEntryCheckBox);
            this.groupBox1.Controls.Add(this.headOfDepartmentCheckBox);
            this.groupBox1.Controls.Add(this.lineSupervisorCheckBox);
            this.groupBox1.Controls.Add(this.resetButton);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.levelOrderTextBox);
            this.groupBox1.Controls.Add(this.approvalLevelTextBox);
            this.groupBox1.Controls.Add(this.approvalPanelComboBox);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 238);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Approval Level Setup";
            // 
            // approvalAuthorityCheckBox
            // 
            this.approvalAuthorityCheckBox.AutoSize = true;
            this.approvalAuthorityCheckBox.Location = new System.Drawing.Point(139, 208);
            this.approvalAuthorityCheckBox.Name = "approvalAuthorityCheckBox";
            this.approvalAuthorityCheckBox.Size = new System.Drawing.Size(124, 15);
            this.approvalAuthorityCheckBox.TabIndex = 6;
            this.approvalAuthorityCheckBox.Text = "Approval Authority";
            this.approvalAuthorityCheckBox.UseSelectable = true;
            // 
            // sourceOfFundVerifyCheckBox
            // 
            this.sourceOfFundVerifyCheckBox.AutoSize = true;
            this.sourceOfFundVerifyCheckBox.Location = new System.Drawing.Point(139, 187);
            this.sourceOfFundVerifyCheckBox.Name = "sourceOfFundVerifyCheckBox";
            this.sourceOfFundVerifyCheckBox.Size = new System.Drawing.Size(135, 15);
            this.sourceOfFundVerifyCheckBox.TabIndex = 6;
            this.sourceOfFundVerifyCheckBox.Text = "Source of Fund Verify";
            this.sourceOfFundVerifyCheckBox.UseSelectable = true;
            // 
            // sourceOfFundEntryCheckBox
            // 
            this.sourceOfFundEntryCheckBox.AutoSize = true;
            this.sourceOfFundEntryCheckBox.Location = new System.Drawing.Point(139, 166);
            this.sourceOfFundEntryCheckBox.Name = "sourceOfFundEntryCheckBox";
            this.sourceOfFundEntryCheckBox.Size = new System.Drawing.Size(133, 15);
            this.sourceOfFundEntryCheckBox.TabIndex = 5;
            this.sourceOfFundEntryCheckBox.Text = "Source of Fund Entry";
            this.sourceOfFundEntryCheckBox.UseSelectable = true;
            // 
            // headOfDepartmentCheckBox
            // 
            this.headOfDepartmentCheckBox.AutoSize = true;
            this.headOfDepartmentCheckBox.Location = new System.Drawing.Point(139, 144);
            this.headOfDepartmentCheckBox.Name = "headOfDepartmentCheckBox";
            this.headOfDepartmentCheckBox.Size = new System.Drawing.Size(165, 15);
            this.headOfDepartmentCheckBox.TabIndex = 4;
            this.headOfDepartmentCheckBox.Text = "Level for Department Head";
            this.headOfDepartmentCheckBox.UseSelectable = true;
            // 
            // lineSupervisorCheckBox
            // 
            this.lineSupervisorCheckBox.AutoSize = true;
            this.lineSupervisorCheckBox.Location = new System.Drawing.Point(139, 123);
            this.lineSupervisorCheckBox.Name = "lineSupervisorCheckBox";
            this.lineSupervisorCheckBox.Size = new System.Drawing.Size(151, 15);
            this.lineSupervisorCheckBox.TabIndex = 3;
            this.lineSupervisorCheckBox.Text = "Level for Line Supervisor";
            this.lineSupervisorCheckBox.UseSelectable = true;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(312, 171);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Reset";
            this.resetButton.UseSelectable = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(312, 200);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "Add";
            this.addButton.UseSelectable = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // levelOrderTextBox
            // 
            // 
            // 
            // 
            this.levelOrderTextBox.CustomButton.Image = null;
            this.levelOrderTextBox.CustomButton.Location = new System.Drawing.Point(226, 1);
            this.levelOrderTextBox.CustomButton.Name = "";
            this.levelOrderTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.levelOrderTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.levelOrderTextBox.CustomButton.TabIndex = 1;
            this.levelOrderTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.levelOrderTextBox.CustomButton.UseSelectable = true;
            this.levelOrderTextBox.CustomButton.Visible = false;
            this.levelOrderTextBox.Lines = new string[0];
            this.levelOrderTextBox.Location = new System.Drawing.Point(139, 93);
            this.levelOrderTextBox.MaxLength = 32767;
            this.levelOrderTextBox.Name = "levelOrderTextBox";
            this.levelOrderTextBox.PasswordChar = '\0';
            this.levelOrderTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.levelOrderTextBox.SelectedText = "";
            this.levelOrderTextBox.SelectionLength = 0;
            this.levelOrderTextBox.SelectionStart = 0;
            this.levelOrderTextBox.ShortcutsEnabled = true;
            this.levelOrderTextBox.Size = new System.Drawing.Size(248, 23);
            this.levelOrderTextBox.TabIndex = 2;
            this.levelOrderTextBox.UseSelectable = true;
            this.levelOrderTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.levelOrderTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // approvalLevelTextBox
            // 
            // 
            // 
            // 
            this.approvalLevelTextBox.CustomButton.Image = null;
            this.approvalLevelTextBox.CustomButton.Location = new System.Drawing.Point(226, 1);
            this.approvalLevelTextBox.CustomButton.Name = "";
            this.approvalLevelTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.approvalLevelTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.approvalLevelTextBox.CustomButton.TabIndex = 1;
            this.approvalLevelTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.approvalLevelTextBox.CustomButton.UseSelectable = true;
            this.approvalLevelTextBox.CustomButton.Visible = false;
            this.approvalLevelTextBox.Lines = new string[0];
            this.approvalLevelTextBox.Location = new System.Drawing.Point(139, 64);
            this.approvalLevelTextBox.MaxLength = 32767;
            this.approvalLevelTextBox.Name = "approvalLevelTextBox";
            this.approvalLevelTextBox.PasswordChar = '\0';
            this.approvalLevelTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.approvalLevelTextBox.SelectedText = "";
            this.approvalLevelTextBox.SelectionLength = 0;
            this.approvalLevelTextBox.SelectionStart = 0;
            this.approvalLevelTextBox.ShortcutsEnabled = true;
            this.approvalLevelTextBox.Size = new System.Drawing.Size(248, 23);
            this.approvalLevelTextBox.TabIndex = 1;
            this.approvalLevelTextBox.UseSelectable = true;
            this.approvalLevelTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.approvalLevelTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // approvalPanelComboBox
            // 
            this.approvalPanelComboBox.FormattingEnabled = true;
            this.approvalPanelComboBox.ItemHeight = 23;
            this.approvalPanelComboBox.Location = new System.Drawing.Point(139, 29);
            this.approvalPanelComboBox.Name = "approvalPanelComboBox";
            this.approvalPanelComboBox.Size = new System.Drawing.Size(248, 29);
            this.approvalPanelComboBox.TabIndex = 0;
            this.approvalPanelComboBox.UseSelectable = true;
            this.approvalPanelComboBox.SelectionChangeCommitted += new System.EventHandler(this.approvalPanelComboBox_SelectionChangeCommitted);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(39, 93);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(75, 19);
            this.metroLabel3.TabIndex = 10;
            this.metroLabel3.Text = "Level order";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(39, 64);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(75, 19);
            this.metroLabel2.TabIndex = 9;
            this.metroLabel2.Text = "Level name";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(37, 34);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(77, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Panel name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.approvalLevelDataGridView);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 251);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 273);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // approvalLevelDataGridView
            // 
            this.approvalLevelDataGridView.AllowUserToAddRows = false;
            this.approvalLevelDataGridView.AllowUserToDeleteRows = false;
            this.approvalLevelDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.approvalLevelDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.approvalLevelDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.approvalLevelDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.levelNameColumn,
            this.lavelOrderColumn});
            this.approvalLevelDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.approvalLevelDataGridView.EnableHeadersVisualStyles = false;
            this.approvalLevelDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.approvalLevelDataGridView.Location = new System.Drawing.Point(3, 19);
            this.approvalLevelDataGridView.Name = "approvalLevelDataGridView";
            this.approvalLevelDataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.approvalLevelDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.approvalLevelDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.approvalLevelDataGridView.Size = new System.Drawing.Size(422, 251);
            this.approvalLevelDataGridView.TabIndex = 0;
            this.approvalLevelDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.approvalLevelDataGridView_CellContentClick);
            // 
            // approvalLevelContextMenu
            // 
            this.approvalLevelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.approvalLevelContextMenu.Name = "approvalLevelContextMenu";
            this.approvalLevelContextMenu.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // upButton
            // 
            this.upButton.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.upButton.Location = new System.Drawing.Point(449, 326);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(45, 44);
            this.upButton.TabIndex = 2;
            this.upButton.Text = "▲";
            this.upButton.UseSelectable = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.downButton.Location = new System.Drawing.Point(449, 376);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(45, 44);
            this.downButton.TabIndex = 2;
            this.downButton.Text = "▼";
            this.downButton.UseSelectable = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // editColumn
            // 
            this.editColumn.HeaderText = "";
            this.editColumn.Name = "editColumn";
            this.editColumn.ReadOnly = true;
            this.editColumn.Width = 50;
            // 
            // levelNameColumn
            // 
            this.levelNameColumn.HeaderText = "Level Name";
            this.levelNameColumn.Name = "levelNameColumn";
            this.levelNameColumn.ReadOnly = true;
            this.levelNameColumn.Width = 200;
            // 
            // lavelOrderColumn
            // 
            this.lavelOrderColumn.HeaderText = "Level Order";
            this.lavelOrderColumn.Name = "lavelOrderColumn";
            this.lavelOrderColumn.ReadOnly = true;
            this.lavelOrderColumn.Width = 120;
            // 
            // ApprovalLevelSetupUI
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(505, 533);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ApprovalLevelSetupUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Approval Level Setup ";
            this.Load += new System.EventHandler(this.ApprovalLevelSetupUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.approvalLevelDataGridView)).EndInit();
            this.approvalLevelContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox approvalLevelTextBox;
        private MetroFramework.Controls.MetroComboBox approvalPanelComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton addButton;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroTextBox levelOrderTextBox;
        private MetroFramework.Controls.MetroButton upButton;
        private MetroFramework.Controls.MetroButton downButton;
        private MetroFramework.Controls.MetroCheckBox headOfDepartmentCheckBox;
        private MetroFramework.Controls.MetroCheckBox lineSupervisorCheckBox;
        private MetroFramework.Controls.MetroContextMenu approvalLevelContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private MetroFramework.Controls.MetroCheckBox sourceOfFundVerifyCheckBox;
        private MetroFramework.Controls.MetroCheckBox sourceOfFundEntryCheckBox;
        private MetroFramework.Controls.MetroCheckBox approvalAuthorityCheckBox;
        private MetroFramework.Controls.MetroButton resetButton;
        private System.Windows.Forms.DataGridView approvalLevelDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lavelOrderColumn;
    }
}