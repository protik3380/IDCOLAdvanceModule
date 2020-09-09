namespace IDCOLAdvanceModule.UI
{
    partial class NotificationUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.notificationTabPage = new MetroFramework.Controls.MetroTabPage();
            this.notificationDataGridView = new System.Windows.Forms.DataGridView();
            this.goColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectAllButton = new MetroFramework.Controls.MetroButton();
            this.markAsReadButton = new MetroFramework.Controls.MetroButton();
            this.historyTabPage = new MetroFramework.Controls.MetroTabPage();
            this.historyDataGridView = new System.Windows.Forms.DataGridView();
            this.view360Column = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.notificationTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.notificationDataGridView)).BeginInit();
            this.historyTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.historyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroTabControl1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 430);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.notificationTabPage);
            this.metroTabControl1.Controls.Add(this.historyTabPage);
            this.metroTabControl1.Location = new System.Drawing.Point(6, 19);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(874, 405);
            this.metroTabControl1.TabIndex = 1;
            this.metroTabControl1.UseSelectable = true;
            // 
            // notificationTabPage
            // 
            this.notificationTabPage.Controls.Add(this.notificationDataGridView);
            this.notificationTabPage.Controls.Add(this.selectAllButton);
            this.notificationTabPage.Controls.Add(this.markAsReadButton);
            this.notificationTabPage.HorizontalScrollbarBarColor = true;
            this.notificationTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this.notificationTabPage.HorizontalScrollbarSize = 10;
            this.notificationTabPage.Location = new System.Drawing.Point(4, 38);
            this.notificationTabPage.Name = "notificationTabPage";
            this.notificationTabPage.Size = new System.Drawing.Size(866, 363);
            this.notificationTabPage.TabIndex = 0;
            this.notificationTabPage.Text = "Notification";
            this.notificationTabPage.VerticalScrollbarBarColor = true;
            this.notificationTabPage.VerticalScrollbarHighlightOnWheel = false;
            this.notificationTabPage.VerticalScrollbarSize = 10;
            // 
            // notificationDataGridView
            // 
            this.notificationDataGridView.AllowUserToAddRows = false;
            this.notificationDataGridView.AllowUserToDeleteRows = false;
            this.notificationDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.notificationDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.notificationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.notificationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.goColumn,
            this.checkColumn,
            this.serialColumn,
            this.messageColumn,
            this.dateColumn});
            this.notificationDataGridView.EnableHeadersVisualStyles = false;
            this.notificationDataGridView.Location = new System.Drawing.Point(3, 4);
            this.notificationDataGridView.Name = "notificationDataGridView";
            this.notificationDataGridView.ReadOnly = true;
            this.notificationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.notificationDataGridView.Size = new System.Drawing.Size(859, 319);
            this.notificationDataGridView.TabIndex = 12;
            this.notificationDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.notificationDataGridView_CellContentClick);
            // 
            // goColumn
            // 
            this.goColumn.HeaderText = "";
            this.goColumn.Name = "goColumn";
            this.goColumn.ReadOnly = true;
            this.goColumn.Width = 50;
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Width = 20;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Width = 60;
            // 
            // messageColumn
            // 
            this.messageColumn.HeaderText = "Message";
            this.messageColumn.Name = "messageColumn";
            this.messageColumn.ReadOnly = true;
            this.messageColumn.Width = 550;
            // 
            // dateColumn
            // 
            this.dateColumn.HeaderText = "Date";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.Width = 150;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(4, 329);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(90, 23);
            this.selectAllButton.TabIndex = 11;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseSelectable = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // markAsReadButton
            // 
            this.markAsReadButton.Location = new System.Drawing.Point(779, 329);
            this.markAsReadButton.Name = "markAsReadButton";
            this.markAsReadButton.Size = new System.Drawing.Size(87, 23);
            this.markAsReadButton.TabIndex = 5;
            this.markAsReadButton.Text = "Mark as read";
            this.markAsReadButton.UseSelectable = true;
            this.markAsReadButton.Click += new System.EventHandler(this.markAsReadButton_Click);
            // 
            // historyTabPage
            // 
            this.historyTabPage.Controls.Add(this.historyDataGridView);
            this.historyTabPage.HorizontalScrollbarBarColor = true;
            this.historyTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this.historyTabPage.HorizontalScrollbarSize = 10;
            this.historyTabPage.Location = new System.Drawing.Point(4, 38);
            this.historyTabPage.Name = "historyTabPage";
            this.historyTabPage.Size = new System.Drawing.Size(866, 363);
            this.historyTabPage.TabIndex = 1;
            this.historyTabPage.Text = "History";
            this.historyTabPage.VerticalScrollbarBarColor = true;
            this.historyTabPage.VerticalScrollbarHighlightOnWheel = false;
            this.historyTabPage.VerticalScrollbarSize = 10;
            // 
            // historyDataGridView
            // 
            this.historyDataGridView.AllowUserToAddRows = false;
            this.historyDataGridView.AllowUserToDeleteRows = false;
            this.historyDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.historyDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.historyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.historyDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.view360Column,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.historyDataGridView.EnableHeadersVisualStyles = false;
            this.historyDataGridView.Location = new System.Drawing.Point(2, 4);
            this.historyDataGridView.MultiSelect = false;
            this.historyDataGridView.Name = "historyDataGridView";
            this.historyDataGridView.ReadOnly = true;
            this.historyDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.historyDataGridView.Size = new System.Drawing.Size(859, 327);
            this.historyDataGridView.TabIndex = 13;
            this.historyDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.historyDataGridView_CellContentClick);
            // 
            // view360Column
            // 
            this.view360Column.HeaderText = "";
            this.view360Column.Name = "view360Column";
            this.view360Column.ReadOnly = true;
            this.view360Column.Width = 70;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Serial";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Message";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 470;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Date";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // NotificationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(910, 454);
            this.Controls.Add(this.groupBox1);
            this.Name = "NotificationUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification";
            this.Load += new System.EventHandler(this.NotificationUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.notificationTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.notificationDataGridView)).EndInit();
            this.historyTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.historyDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage notificationTabPage;
        private MetroFramework.Controls.MetroTabPage historyTabPage;
        private MetroFramework.Controls.MetroButton markAsReadButton;
        private MetroFramework.Controls.MetroButton selectAllButton;
        private System.Windows.Forms.DataGridView notificationDataGridView;
        private System.Windows.Forms.DataGridView historyDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn goColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewButtonColumn view360Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

    }
}