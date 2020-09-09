using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class RequisitionCategoryWisePanelSettingsUI : Form
    {
        private readonly IApprovalPanelManager _approvalPanelManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private AdvanceCategory _updateAdvanceRequisitionCategory;
        private bool _isUpdateMode;

        public RequisitionCategoryWisePanelSettingsUI()
        {
            InitializeComponent();
            _approvalPanelManager = new ApprovalPanelManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _isUpdateMode = false;
        }

        private void LoadApprovalPanelTypeComboBox()
        {
            approvalPanelComboBox.DataSource = null;
            List<ApprovalPanel> approvalPanels = _approvalPanelManager.GetAll().ToList();
            approvalPanels.Insert(0, new ApprovalPanel { Id = DefaultItem.Value, Name = DefaultItem.Text });

            approvalPanelComboBox.DisplayMember = "Name";
            approvalPanelComboBox.ValueMember = "Id";
            expensePanelComboBox.DisplayMember = "Name";
            expensePanelComboBox.ValueMember = "Id";
            approvalPanelComboBox.DataSource = approvalPanels;
            expensePanelComboBox.DataSource = approvalPanels;
        }

        private void LoadAvanceCategoryDropdown()
        {
            var categoryList = _advanceRequisitionCategoryManager.GetAll().ToList();
            if (categoryList.Any())
            {
                categoryList.Insert(0, new AdvanceCategory { Id = DefaultItem.Value, Name = DefaultItem.Text });
                advanceCategoryComboBox.DataSource = null;
                expenseAdvanceCategoryCombo.DisplayMember = "Name";
                expenseAdvanceCategoryCombo.ValueMember = "Id";
                advanceCategoryComboBox.DisplayMember = "Name";
                advanceCategoryComboBox.ValueMember = "Id";
                advanceCategoryComboBox.DataSource = categoryList;
                expenseAdvanceCategoryCombo.DataSource = categoryList;
            }
        }

        private void RequisitionCategoryWisePanelSettingsUI_Load(object sender, System.EventArgs e)
        {
            try
            {
                LoadApprovalPanelTypeComboBox();
                LoadAvanceCategoryDropdown();
                LoadAdvanceCategoryApprovalPanelMap();
                LoadAdvanceCategoryApprovalPanelForExpenses();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void LoadAdvanceCategoryApprovalPanelForExpenses()
        //{
        //    var requisitionCategoryList = _advanceRequisitionCategoryManager.GetCategoriesForExpenseApprovalPanel();
        //    int serial = 1;
        //    expenseCategoryWisePanelListView.Items.Clear();
        //    if (requisitionCategoryList != null & requisitionCategoryList.Any())
        //    {
        //        foreach (AdvanceCategory advanceRequisitionCategory in requisitionCategoryList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(advanceRequisitionCategory.Name);
        //            item.SubItems.Add(advanceRequisitionCategory.ExpenseApprovalPanel.Name);
        //            item.Tag = advanceRequisitionCategory;
        //            expenseCategoryWisePanelListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadAdvanceCategoryApprovalPanelForExpenses()
        {
            var requisitionCategoryList = _advanceRequisitionCategoryManager.GetCategoriesForExpenseApprovalPanel();
            int serial = 1;
            expenseCategoryWisePanelDataGridView.Rows.Clear();
            if (requisitionCategoryList != null & requisitionCategoryList.Any())
            {
                foreach (AdvanceCategory advanceRequisitionCategory in requisitionCategoryList)
                {
                    DataGridViewRow row =new DataGridViewRow();
                    row.CreateCells(expenseCategoryWisePanelDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = advanceRequisitionCategory.Name;
                    row.Cells[4].Value = advanceRequisitionCategory.ExpenseApprovalPanel.Name;
                    
                    row.Tag = advanceRequisitionCategory;
                    expenseCategoryWisePanelDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void LoadAdvanceCategoryApprovalPanelMap()
        {
            var advanceRequisitionCategoryList = _advanceRequisitionCategoryManager.GetAllWithApprovalPanel();
            int serial = 1;
            requisitionCategoryWisePanelSetupDataGridView.Rows.Clear();
            if (advanceRequisitionCategoryList != null & advanceRequisitionCategoryList.Any())
            {
                foreach (AdvanceCategory category in advanceRequisitionCategoryList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(requisitionCategoryWisePanelSetupDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = category.Name;
                    row.Cells[4].Value = category.RequisitionApprovalPanel.Name;
                    
                    row.Tag = category;
                    requisitionCategoryWisePanelSetupDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            var advanceCategoryId = (long)advanceCategoryComboBox.SelectedValue;
            var approvalPanelId = (long)approvalPanelComboBox.SelectedValue;
            try
            {
                if ((long)approvalPanelId == DefaultItem.Value)
                {
                    throw new UiException("Please Choose Approval Panel Category");
                }
                if ((long)advanceCategoryId == DefaultItem.Value)
                {
                    throw new UiException("Please Choose Advance Category");
                }

                var requisitionCategory = _advanceRequisitionCategoryManager.GetById(advanceCategoryId);
                requisitionCategory.RequisitionApprovalPanelId = approvalPanelId;
                bool isEdited = _advanceRequisitionCategoryManager.Edit(requisitionCategory);
                if (isEdited)
                {
                    if (_isUpdateMode)
                    {
                        _isUpdateMode = false;
                        saveButton.Text = @"Save";
                        MessageBox.Show(@"Successfully updated mapping.", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(@"Successfully mapped.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    LoadAdvanceCategoryApprovalPanelMap();
                    ClearAllDropdown();
                    return;
                }
                MessageBox.Show(@"Failed to mapped.", @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearAllDropdown()
        {
            advanceCategoryComboBox.SelectedValue = DefaultItem.Value;
            approvalPanelComboBox.SelectedValue = DefaultItem.Value;
            expensePanelComboBox.SelectedValue = DefaultItem.Value;
            expenseAdvanceCategoryCombo.SelectedValue = DefaultItem.Value;
        }

        private void expensePanelSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var advanceCategoryId = (long)expenseAdvanceCategoryCombo.SelectedValue;
                var approvalPanelId = (long)expensePanelComboBox.SelectedValue;
                if ((long)approvalPanelId == DefaultItem.Value)
                {
                    throw new UiException("Please Choose Approval Panel Category");
                }
                if ((long)advanceCategoryId == DefaultItem.Value)
                {
                    throw new UiException("Please Choose Advance Category");
                }

                var requisitionCategory = _advanceRequisitionCategoryManager.GetById(advanceCategoryId);
                requisitionCategory.ExpenseApprovalPanelId = approvalPanelId;
                bool isEdited = _advanceRequisitionCategoryManager.Edit(requisitionCategory);
                if (isEdited)
                {
                    if (_isUpdateMode)
                    {
                        _isUpdateMode = false;
                        saveButton.Text = @"Save";
                        MessageBox.Show(@"Successfully updated mapping.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(@"Successfully mapped.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    LoadAdvanceCategoryApprovalPanelForExpenses();
                    ClearAllDropdown();
                    return;
                }
                MessageBox.Show(@"Failed to mapped.", @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionCategoryWisePanelSetupDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (requisitionCategoryWisePanelSetupDataGridView.SelectedRows.Count > 0)
                        {
                            saveButton.Text = @"Update";
                            _isUpdateMode = true;
                            var advanceRequisitionCategory =
                                requisitionCategoryWisePanelSetupDataGridView.SelectedRows[0].Tag as AdvanceCategory;
                            advanceCategoryComboBox.SelectedValue = advanceRequisitionCategory.Id;
                            approvalPanelComboBox.SelectedValue = advanceRequisitionCategory.RequisitionApprovalPanelId;
                            _updateAdvanceRequisitionCategory = advanceRequisitionCategory;
                        }
                        else
                        {
                            MessageBox.Show(@"Please choose an item.", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        if (requisitionCategoryWisePanelSetupDataGridView.SelectedRows.Count > 0)
                        {
                            var requisitionCategory = requisitionCategoryWisePanelSetupDataGridView
                                .SelectedRows[0].Tag as AdvanceCategory;
                            DialogResult optionChoice = MessageBox.Show(@"Are you sure you want to remove this mapping?", @"Do you want to Remove this setting?", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Error);
                            if (optionChoice == DialogResult.Yes)
                            {
                                requisitionCategory.RequisitionApprovalPanelId = null;
                                bool isEdited = _advanceRequisitionCategoryManager.Edit(requisitionCategory);
                                if (isEdited)
                                {
                                    MessageBox.Show(@"Successfully Removed mapping.", @"Success", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                    LoadAdvanceCategoryApprovalPanelMap();
                                    ClearAllDropdown();
                                    return;
                                }
                                MessageBox.Show(@"Failed to mapped.", @"Error!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void expenseCategoryWisePanelDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (expenseCategoryWisePanelDataGridView.SelectedRows.Count > 0)
                        {
                            expensePanelSaveButton.Text = @"Update";
                            _isUpdateMode = true;
                            var expenseCategory =
                                expenseCategoryWisePanelDataGridView.SelectedRows[0].Tag as AdvanceCategory;
                            advanceCategoryComboBox.SelectedValue = expenseCategory.Id;
                            approvalPanelComboBox.SelectedValue = expenseCategory.ExpenseApprovalPanelId;
                            _updateAdvanceRequisitionCategory = expenseCategory;
                        }
                        else
                        {
                            MessageBox.Show(@"Please choose an item.", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        if (expenseCategoryWisePanelDataGridView.SelectedRows.Count > 0)
                        {
                            var advanceCategory = expenseCategoryWisePanelDataGridView.SelectedRows[0].Tag as AdvanceCategory;
                            DialogResult optionChoice = MessageBox.Show(@"Are you sure you want to remove this mapping?", @"Do you want to Remove this setting?", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Error);
                            if (optionChoice == DialogResult.Yes)
                            {
                                advanceCategory.ExpenseApprovalPanelId = null;
                                bool isEdited = _advanceRequisitionCategoryManager.Edit(advanceCategory);
                                if (isEdited)
                                {
                                    MessageBox.Show(@"Successfully Removed mapping.", @"Success", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                    LoadAdvanceCategoryApprovalPanelForExpenses();
                                    ClearAllDropdown();
                                    return;
                                }
                                MessageBox.Show(@"Failed to mapped.", @"Error!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
