namespace IDCOLAdvanceModule.UI.Settings
{
    partial class ApprovalPanelSetupUI
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
            this.panelTypeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.panelNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.approvePanelGridView = new System.Windows.Forms.DataGridView();
            this.approvalPanelContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.sIColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvePanelGridView)).BeginInit();
            this.approvalPanelContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resetButton);
            this.groupBox1.Controls.Add(this.panelTypeComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.panelNameTextBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(219, 99);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 5;
            this.resetButton.Text = "Reset";
            this.resetButton.UseSelectable = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // panelTypeComboBox
            // 
            this.panelTypeComboBox.FormattingEnabled = true;
            this.panelTypeComboBox.ItemHeight = 23;
            this.panelTypeComboBox.Location = new System.Drawing.Point(207, 35);
            this.panelTypeComboBox.Name = "panelTypeComboBox";
            this.panelTypeComboBox.Size = new System.Drawing.Size(168, 29);
            this.panelTypeComboBox.TabIndex = 0;
            this.panelTypeComboBox.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(90, 40);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(69, 19);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Panel type";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(300, 99);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panelNameTextBox
            // 
            // 
            // 
            // 
            this.panelNameTextBox.CustomButton.Image = null;
            this.panelNameTextBox.CustomButton.Location = new System.Drawing.Point(146, 1);
            this.panelNameTextBox.CustomButton.Name = "";
            this.panelNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.panelNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.panelNameTextBox.CustomButton.TabIndex = 1;
            this.panelNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelNameTextBox.CustomButton.UseSelectable = true;
            this.panelNameTextBox.CustomButton.Visible = false;
            this.panelNameTextBox.Lines = new string[0];
            this.panelNameTextBox.Location = new System.Drawing.Point(207, 70);
            this.panelNameTextBox.MaxLength = 32767;
            this.panelNameTextBox.Name = "panelNameTextBox";
            this.panelNameTextBox.PasswordChar = '\0';
            this.panelNameTextBox.PromptText = "Panel type name";
            this.panelNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.panelNameTextBox.SelectedText = "";
            this.panelNameTextBox.SelectionLength = 0;
            this.panelNameTextBox.SelectionStart = 0;
            this.panelNameTextBox.ShortcutsEnabled = true;
            this.panelNameTextBox.Size = new System.Drawing.Size(168, 23);
            this.panelNameTextBox.TabIndex = 1;
            this.panelNameTextBox.UseSelectable = true;
            this.panelNameTextBox.WaterMark = "Panel type name";
            this.panelNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.panelNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(82, 71);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(77, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Panel name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.approvePanelGridView);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(490, 229);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Approval Panel";
            // 
            // approvePanelGridView
            // 
            this.approvePanelGridView.AllowUserToAddRows = false;
            this.approvePanelGridView.AllowUserToDeleteRows = false;
            this.approvePanelGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.approvePanelGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.approvePanelGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.approvePanelGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.sIColumn,
            this.panelColumn,
            this.panelTypeColumn});
            this.approvePanelGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.approvePanelGridView.EnableHeadersVisualStyles = false;
            this.approvePanelGridView.Location = new System.Drawing.Point(3, 19);
            this.approvePanelGridView.MultiSelect = false;
            this.approvePanelGridView.Name = "approvePanelGridView";
            this.approvePanelGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.approvePanelGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.approvePanelGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.approvePanelGridView.Size = new System.Drawing.Size(484, 207);
            this.approvePanelGridView.TabIndex = 0;
            this.approvePanelGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.approvePanelGridView_CellContentClick);
            // 
            // approvalPanelContextMenu
            // 
            this.approvalPanelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.approvalPanelContextMenu.Name = "approvalPanelContextMenu";
            this.approvalPanelContextMenu.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
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
            // sIColumn
            // 
            this.sIColumn.HeaderText = "Serial";
            this.sIColumn.Name = "sIColumn";
            this.sIColumn.ReadOnly = true;
            this.sIColumn.Width = 40;
            // 
            // panelColumn
            // 
            this.panelColumn.HeaderText = "Panel";
            this.panelColumn.Name = "panelColumn";
            this.panelColumn.ReadOnly = true;
            this.panelColumn.Width = 220;
            // 
            // panelTypeColumn
            // 
            this.panelTypeColumn.HeaderText = "Panel Type";
            this.panelTypeColumn.Name = "panelTypeColumn";
            this.panelTypeColumn.ReadOnly = true;
            this.panelTypeColumn.Width = 120;
            // 
            // ApprovalPanelSetupUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(514, 408);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ApprovalPanelSetupUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Approval Panel Setup";
            this.Load += new System.EventHandler(this.ApprovalPanelSetupUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.approvePanelGridView)).EndInit();
            this.approvalPanelContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroComboBox panelTypeComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton saveButton;
        private MetroFramework.Controls.MetroTextBox panelNameTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroContextMenu approvalPanelContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.DataGridView approvePanelGridView;
        private MetroFramework.Controls.MetroButton resetButton;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sIColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn panelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn panelTypeColumn;
    }
}