using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class EntitlementMappingSettingsUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly ICostItemManager _costItemManager;
        private readonly List<Admin_Rank> _designationList;
        private IRequisitionCategoryWiseCostItemSettingManager _requisitionCategoryWiseCostItemSettingManager;
        private readonly IEntitlementMappingSettingManager _entitlementMappingSettingManager;

        public EntitlementMappingSettingsUI()
        {
            InitializeComponent();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _costItemManager = new CostItemManager();
            IDesignationManager designationManager = new DesignationManager();
            _designationList = designationManager.GetFiltered().ToList();
            _entitlementMappingSettingManager = new EntitlementMappingSettingManager();
            _requisitionCategoryWiseCostItemSettingManager = new RequisitionCategoryWiseCostItemSettingManager();
        }

        private void LoadAvanceCategoryDropdown()
        {
            var categoryList = _advanceRequisitionCategoryManager.GetAll().ToList();
            if (categoryList.Any())
            {
                categoryList.Insert(0, new AdvanceCategory { Id = DefaultItem.Value, Name = DefaultItem.Text });
                advanceCategoryComboBox.DataSource = null;
                advanceCategoryComboBox.DisplayMember = "Name";
                advanceCategoryComboBox.ValueMember = "Id";
                advanceCategoryComboBox.DataSource = categoryList;
            }
        }

        //private void LoadEntitlementMappingSetting()
        //{
        //    entitlementMappingSettingListView.Items.Clear();
        //    var entitlementMappingSettings = _entitlementMappingSettingManager.GetEntitlementMappingSettingVm();
        //    if (entitlementMappingSettings != null)
        //    {
        //        int serial = 1;
        //        foreach (EntitlementMappingSettingVM mappingSetting in entitlementMappingSettings)
        //        {
        //            ListViewItem item = new ListViewItem();
        //            item.Text = serial.ToString();
        //            item.SubItems.Add(mappingSetting.RankName);
        //            item.SubItems.Add(mappingSetting.CategoryName);
        //            item.SubItems.Add(mappingSetting.CostItemName);
        //            item.SubItems.Add(mappingSetting.EntitlementAmount == null ? string.Empty : mappingSetting.EntitlementAmount.ToString());
        //            item.SubItems.Add(mappingSetting.IsFullAmountEntitlement? "Yes" : "No");
        //            item.Tag = mappingSetting;
        //            entitlementMappingSettingListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadEntitlementMappingSetting()
        {
            entitlementMappingSettingDataGridView.Rows.Clear();
            var entitlementMappingSettings = _entitlementMappingSettingManager.GetEntitlementMappingSettingVm();
            if (entitlementMappingSettings != null)
            {
                int serial = 1;
                foreach (EntitlementMappingSettingVM mappingSetting in entitlementMappingSettings)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(entitlementMappingSettingDataGridView);

                    row.Cells[0].Value = "Remove";
                    row.Cells[1].Value = serial.ToString();
                    row.Cells[2].Value = mappingSetting.RankName;
                    row.Cells[3].Value = mappingSetting.CategoryName;
                    row.Cells[4].Value = mappingSetting.CostItemName;
                    row.Cells[5].Value = mappingSetting.EntitlementAmount == null
                        ? string.Empty
                        : mappingSetting.EntitlementAmount.ToString();
                    row.Cells[6].Value = mappingSetting.IsFullAmountEntitlement ? "Yes" : "No";

                    row.Tag = mappingSetting;
                    entitlementMappingSettingDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void LoadCostItemDropdownByAdvanceCategory()
        {
            if (int.Parse(advanceCategoryComboBox.SelectedValue.ToString()) != DefaultItem.Value)
            {
                var selectedAdvanceCategoryId = (long)advanceCategoryComboBox.SelectedValue;
                var costItems = _costItemManager.GetByAdvanceCategory(selectedAdvanceCategoryId).ToList();
                costItems.Insert(0, new CostItem { Id = DefaultItem.Value, Name = DefaultItem.Text });
                if (costItems.Any())
                {
                    costItemComboBox.DataSource = null;
                    costItemComboBox.DisplayMember = "Name";
                    costItemComboBox.ValueMember = "Id";
                    costItemComboBox.DataSource = costItems;
                }
            }
            else
            {
                var costItems = new List<CostItem>();
                costItems.Insert(0, new CostItem { Id = DefaultItem.Value, Name = DefaultItem.Text });
                costItemComboBox.DataSource = null;
                costItemComboBox.DisplayMember = "Name";
                costItemComboBox.ValueMember = "Id";
                costItemComboBox.DataSource = costItems;
            }
        }

        //private void LoadDesignationListView()
        //{
        //    foreach (Admin_Rank designation in _designationList)
        //    {
        //        ListViewItem item = new ListViewItem();
        //        item.Text = designation.RankName;
        //        item.Tag = designation;
        //        //if (categoryId != null && categoryId == designation.EmployeeCategoryId)
        //        //{
        //        //    item.Checked = true;
        //        //}
        //        employeeDesignationListView.Items.Add(item);
        //    }
        //}

        private void LoadDesignationGridView()
        {
            foreach (Admin_Rank designation in _designationList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(employeeDesignationDataGridView);
                row.Cells[1].Value = designation.RankName;
                row.Tag = designation;
                employeeDesignationDataGridView.Rows.Add(row);
            }
        }
        private void EntitlementMappingSettingsUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAvanceCategoryDropdown();
                LoadCostItemDropdownByAdvanceCategory();
                //LoadDesignationListView();
                LoadDesignationGridView();
                LoadEntitlementMappingSetting();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceCategoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadCostItemDropdownByAdvanceCategory();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //ValidateSave();
                //var entitlementMappingSettingHeader = new EntitlementMappingSettingHeader();
                //entitlementMappingSettingHeader.AdvanceCategoryId =
                //    (long)advanceCategoryComboBox.SelectedValue;
                //entitlementMappingSettingHeader.CostItemId = (long)costItemComboBox.SelectedValue;
                //List<EntitlementMappingSettingDetail> entitlementMappingSettingDetails =
                //    new List<EntitlementMappingSettingDetail>();
                //if (employeeDesignationListView.CheckedItems.Count > 0)
                //{
                //    foreach (ListViewItem item in employeeDesignationListView.CheckedItems)
                //    {
                //        var detail = new EntitlementMappingSettingDetail();
                //        var designation = item.Tag as Admin_Rank;
                //        if (designation != null)
                //        {
                //            detail.RankID = (long)designation.RankID;
                //            detail.RankName = designation.RankName;
                //        }
                //        detail.EntitlementAmount = entitlementAmountTextBox.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(entitlementAmountTextBox.Text);
                //        detail.IsFullAmountEntitlement = isFullAmountEntitlementCheckBox.Checked;
                //        entitlementMappingSettingDetails.Add(detail);
                //    }
                //    entitlementMappingSettingHeader.EntitlementMappingSettingDetails = entitlementMappingSettingDetails;
                //    bool isInserted = _entitlementMappingSettingManager.Insert(entitlementMappingSettingHeader);
                //    if (isInserted)
                //    {
                //        MessageBox.Show(@"Entitlement settings saved successfully.", @"Success",
                //            MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        LoadEntitlementMappingSetting();
                //        this.Close();
                //    }
                //    else
                //    {
                //        MessageBox.Show(@"Entitlement settings save failed.", @"Error!", MessageBoxButtons.OK,
                //            MessageBoxIcon.Error);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select designation(s).", @"Error!", MessageBoxButtons.OK,
                //           MessageBoxIcon.Error);
                //}
                ValidateSave();
                var entitlementMappingSettingHeader = new EntitlementMappingSettingHeader();
                entitlementMappingSettingHeader.AdvanceCategoryId =
                    (long)advanceCategoryComboBox.SelectedValue;
                entitlementMappingSettingHeader.CostItemId = (long)costItemComboBox.SelectedValue;
                List<EntitlementMappingSettingDetail> entitlementMappingSettingDetails =
                    new List<EntitlementMappingSettingDetail>();
                int checkedDesignation = 0;
                foreach (DataGridViewRow row in employeeDesignationDataGridView.Rows)
                {
                    bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                    if (isChecked)
                    {
                        checkedDesignation++;
                    }
                }

                if (checkedDesignation > 0)
                {
                    foreach (DataGridViewRow viewRow in employeeDesignationDataGridView.Rows)
                    {
                        if ((bool)viewRow.Cells[0].EditedFormattedValue)
                        {
                            var detail = new EntitlementMappingSettingDetail();
                            var designation = viewRow.Tag as Admin_Rank;
                            if (designation != null)
                            {
                                detail.RankID = (long)designation.RankID;
                                detail.RankName = designation.RankName;
                            }
                            detail.EntitlementAmount = entitlementAmountTextBox.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(entitlementAmountTextBox.Text);
                            detail.IsFullAmountEntitlement = isFullAmountEntitlementCheckBox.Checked;
                            entitlementMappingSettingDetails.Add(detail);
                        }

                    }
                    entitlementMappingSettingHeader.EntitlementMappingSettingDetails = entitlementMappingSettingDetails;
                    bool isInserted = _entitlementMappingSettingManager.Insert(entitlementMappingSettingHeader);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Entitlement settings saved successfully.", @"Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEntitlementMappingSetting();
                    }
                    else
                    {
                        MessageBox.Show(@"Entitlement settings save failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                else
                {
                    MessageBox.Show(@"Please select designation(s).", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;

            if (int.Parse(advanceCategoryComboBox.SelectedValue.ToString()) == DefaultItem.Value)
            {
                errorMessage += "Please select a advance category." + Environment.NewLine;
            }
            if (int.Parse(costItemComboBox.SelectedValue.ToString()) == DefaultItem.Value)
            {
                errorMessage += "Please select a cost item." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(entitlementAmountTextBox.Text) && !isFullAmountEntitlementCheckBox.Checked)
            {
                errorMessage += "Please provide entitlement amount." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            else
            {
                return true;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (entitlementMappingSettingListView.SelectedItems.Count > 0)
            //    {
            //        var detailItem = entitlementMappingSettingListView.SelectedItems[0].Tag as EntitlementMappingSettingVM;
            //        if (detailItem != null)
            //        {
            //            bool isDeleted = _entitlementMappingSettingManager.DeleteEntitlementMappingSetting(detailItem.DetailId);
            //            if (isDeleted)
            //            {
            //                LoadEntitlementMappingSetting();
            //                MessageBox.Show(@"Selected item removed successfully", @"Success", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Information);
            //            }
            //            else
            //            {
            //                MessageBox.Show(@"Selected item remove failed.", @"Warning!", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(@"Please select an item to remove.", @"Warning!", MessageBoxButtons.OK,
            //                MessageBoxIcon.Warning);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void requisitionCategorywWiseCostItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                var entitlementMappingSettingHeader = new EntitlementMappingSettingHeader();
                entitlementMappingSettingHeader.AdvanceCategoryId =
                        (long)advanceCategoryComboBox.SelectedValue;
                RequisitionCategoryWiseCostItemSettingsUI costItemUI = new RequisitionCategoryWiseCostItemSettingsUI(entitlementMappingSettingHeader.AdvanceCategoryId);
                costItemUI.ShowDialog();
                LoadCostItemDropdownByAdvanceCategory();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void isFullAmountEntitlementcheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (isFullAmountEntitlementCheckBox.Checked)
                {
                    entitlementAmountTextBox.Text = string.Empty;
                    entitlementAmountTextBox.Enabled = false;
                }
                else
                {
                    entitlementAmountTextBox.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void entitlementAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Utility.Utility.MakeTextBoxToTakeNumbersOnly(sender, e);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void employeeDesignationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ToggleCellCheck(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)employeeDesignationDataGridView[column, row].EditedFormattedValue;
            employeeDesignationDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void entitlementMappingSettingDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (entitlementMappingSettingDataGridView.SelectedRows.Count > 0)
                    {
                        var detailItem = entitlementMappingSettingDataGridView.SelectedRows[0].Tag as EntitlementMappingSettingVM;
                        if (detailItem != null)
                        {
                            bool isDeleted = _entitlementMappingSettingManager.DeleteEntitlementMappingSetting(detailItem.DetailId);
                            if (isDeleted)
                            {
                                LoadEntitlementMappingSetting();
                                MessageBox.Show(@"Selected item removed successfully", @"Success", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(@"Selected item remove failed.", @"Warning!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please select an item to remove.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
