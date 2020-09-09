namespace IDCOLAdvanceModule.UI.Settings
{
    partial class RequisitionCategoryWisePanelSettingsUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.advanceCategoryComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.approvalPanelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.requisitionCategoryWisePanelSetupDataGridView = new System.Windows.Forms.DataGridView();
            this.editColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.expenseCategoryWisePanelDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.expensePanelSaveButton = new MetroFramework.Controls.MetroButton();
            this.expenseAdvanceCategoryCombo = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.expensePanelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.requisitionCategoryWisePanelSetupDataGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expenseCategoryWisePanelDataGridView)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.advanceCategoryComboBox);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.approvalPanelComboBox);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(19, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setup Criteria";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(331, 102);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // advanceCategoryComboBox
            // 
            this.advanceCategoryComboBox.FormattingEnabled = true;
            this.advanceCategoryComboBox.ItemHeight = 23;
            this.advanceCategoryComboBox.Location = new System.Drawing.Point(166, 66);
            this.advanceCategoryComboBox.Name = "advanceCategoryComboBox";
            this.advanceCategoryComboBox.Size = new System.Drawing.Size(242, 29);
            this.advanceCategoryComboBox.TabIndex = 1;
            this.advanceCategoryComboBox.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(41, 68);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(118, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Advance Category";
            // 
            // approvalPanelComboBox
            // 
            this.approvalPanelComboBox.FormattingEnabled = true;
            this.approvalPanelComboBox.ItemHeight = 23;
            this.approvalPanelComboBox.Location = new System.Drawing.Point(166, 31);
            this.approvalPanelComboBox.Name = "approvalPanelComboBox";
            this.approvalPanelComboBox.Size = new System.Drawing.Size(242, 29);
            this.approvalPanelComboBox.TabIndex = 0;
            this.approvalPanelComboBox.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(83, 33);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(77, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Panel name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.requisitionCategoryWisePanelSetupDataGridView);
            this.groupBox2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 285);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Details List";
            // 
            // requisitionCategoryWisePanelSetupDataGridView
            // 
            this.requisitionCategoryWisePanelSetupDataGridView.AllowUserToAddRows = false;
            this.requisitionCategoryWisePanelSetupDataGridView.AllowUserToDeleteRows = false;
            this.requisitionCategoryWisePanelSetupDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.requisitionCategoryWisePanelSetupDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.requisitionCategoryWisePanelSetupDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requisitionCategoryWisePanelSetupDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editColumn,
            this.removeColumn,
            this.serialColumn,
            this.categoryNameColumn,
            this.panelNameColumn});
            this.requisitionCategoryWisePanelSetupDataGridView.EnableHeadersVisualStyles = false;
            this.requisitionCategoryWisePanelSetupDataGridView.Location = new System.Drawing.Point(6, 22);
            this.requisitionCategoryWisePanelSetupDataGridView.MultiSelect = false;
            this.requisitionCategoryWisePanelSetupDataGridView.Name = "requisitionCategoryWisePanelSetupDataGridView";
            this.requisitionCategoryWisePanelSetupDataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.requisitionCategoryWisePanelSetupDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.requisitionCategoryWisePanelSetupDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.requisitionCategoryWisePanelSetupDataGridView.Size = new System.Drawing.Size(466, 257);
            this.requisitionCategoryWisePanelSetupDataGridView.TabIndex = 0;
            this.requisitionCategoryWisePanelSetupDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.requisitionCategoryWisePanelSetupDataGridView_CellContentClick);
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
            // removeColumn
            // 
            this.removeColumn.HeaderText = "";
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.ReadOnly = true;
            this.removeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.removeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.removeColumn.Width = 60;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            this.serialColumn.Width = 60;
            // 
            // categoryNameColumn
            // 
            this.categoryNameColumn.HeaderText = "Category Name";
            this.categoryNameColumn.Name = "categoryNameColumn";
            this.categoryNameColumn.ReadOnly = true;
            this.categoryNameColumn.Width = 140;
            // 
            // panelNameColumn
            // 
            this.panelNameColumn.HeaderText = "Panel Name";
            this.panelNameColumn.Name = "panelNameColumn";
            this.panelNameColumn.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.expenseCategoryWisePanelDataGridView);
            this.groupBox3.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(47, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(478, 294);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Details List";
            // 
            // expenseCategoryWisePanelDataGridView
            // 
            this.expenseCategoryWisePanelDataGridView.AllowUserToAddRows = false;
            this.expenseCategoryWisePanelDataGridView.AllowUserToDeleteRows = false;
            this.expenseCategoryWisePanelDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.expenseCategoryWisePanelDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.expenseCategoryWisePanelDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.expenseCategoryWisePanelDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.expenseCategoryWisePanelDataGridView.EnableHeadersVisualStyles = false;
            this.expenseCategoryWisePanelDataGridView.Location = new System.Drawing.Point(6, 22);
            this.expenseCategoryWisePanelDataGridView.MultiSelect = false;
            this.expenseCategoryWisePanelDataGridView.Name = "expenseCategoryWisePanelDataGridView";
            this.expenseCategoryWisePanelDataGridView.ReadOnly = true;
            this.expenseCategoryWisePanelDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.expenseCategoryWisePanelDataGridView.Size = new System.Drawing.Size(466, 263);
            this.expenseCategoryWisePanelDataGridView.TabIndex = 1;
            this.expenseCategoryWisePanelDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.expenseCategoryWisePanelDataGridView_CellContentClick);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Serial";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Category Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 140;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Panel Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.expensePanelSaveButton);
            this.groupBox4.Controls.Add(this.expenseAdvanceCategoryCombo);
            this.groupBox4.Controls.Add(this.metroLabel3);
            this.groupBox4.Controls.Add(this.expensePanelComboBox);
            this.groupBox4.Controls.Add(this.metroLabel4);
            this.groupBox4.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(47, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(478, 145);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Setup Criteria";
            // 
            // expensePanelSaveButton
            // 
            this.expensePanelSaveButton.Location = new System.Drawing.Point(331, 102);
            this.expensePanelSaveButton.Name = "expensePanelSaveButton";
            this.expensePanelSaveButton.Size = new System.Drawing.Size(75, 23);
            this.expensePanelSaveButton.TabIndex = 2;
            this.expensePanelSaveButton.Text = "Save";
            this.expensePanelSaveButton.UseSelectable = true;
            this.expensePanelSaveButton.Click += new System.EventHandler(this.expensePanelSaveButton_Click);
            // 
            // expenseAdvanceCategoryCombo
            // 
            this.expenseAdvanceCategoryCombo.FormattingEnabled = true;
            this.expenseAdvanceCategoryCombo.ItemHeight = 23;
            this.expenseAdvanceCategoryCombo.Location = new System.Drawing.Point(166, 66);
            this.expenseAdvanceCategoryCombo.Name = "expenseAdvanceCategoryCombo";
            this.expenseAdvanceCategoryCombo.Size = new System.Drawing.Size(242, 29);
            this.expenseAdvanceCategoryCombo.TabIndex = 1;
            this.expenseAdvanceCategoryCombo.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(41, 68);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(118, 19);
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "Advance Category";
            // 
            // expensePanelComboBox
            // 
            this.expensePanelComboBox.FormattingEnabled = true;
            this.expensePanelComboBox.ItemHeight = 23;
            this.expensePanelComboBox.Location = new System.Drawing.Point(166, 31);
            this.expensePanelComboBox.Name = "expensePanelComboBox";
            this.expensePanelComboBox.Size = new System.Drawing.Size(242, 29);
            this.expensePanelComboBox.TabIndex = 0;
            this.expensePanelComboBox.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(83, 33);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(77, 19);
            this.metroLabel4.TabIndex = 3;
            this.metroLabel4.Text = "Panel name";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(12, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(513, 486);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Requisition Panel Selection for Advance Category";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(563, 13);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(557, 486);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Panel Selection by Advance Category for Expense Approval";
            // 
            // RequisitionCategoryWisePanelSettingsUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1132, 511);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.MaximizeBox = false;
            this.Name = "RequisitionCategoryWisePanelSettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Category Wise Panel Setup";
            this.Load += new System.EventHandler(this.RequisitionCategoryWisePanelSettingsUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.requisitionCategoryWisePanelSetupDataGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.expenseCategoryWisePanelDataGridView)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroComboBox approvalPanelComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox advanceCategoryComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton saveButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private MetroFramework.Controls.MetroButton expensePanelSaveButton;
        private MetroFramework.Controls.MetroComboBox expenseAdvanceCategoryCombo;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox expensePanelComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView requisitionCategoryWisePanelSetupDataGridView;
        private System.Windows.Forms.DataGridView expenseCategoryWisePanelDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn panelNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}