using System;
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
    public partial class RequisitionCategoryWiseCostItemSettingsUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly ICostItemManager _costItemManager;
        private readonly IRequisitionCategoryWiseCostItemSettingManager _requisitionCategoryWiseCostItemSettingManager;
        private AdvanceCategoryWiseCostItemSetting _updatebableRequisitionCategoryWiseCostItemSetting;
        private bool _isUpdateMode;
        private readonly long _categoryId;

        public RequisitionCategoryWiseCostItemSettingsUI()
        {
            InitializeComponent();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _costItemManager = new CostItemManager();
            _requisitionCategoryWiseCostItemSettingManager = new RequisitionCategoryWiseCostItemSettingManager();
            _isUpdateMode = false;
        }

        public RequisitionCategoryWiseCostItemSettingsUI(long categoryId)
        {
            _categoryId = categoryId;
            InitializeComponent();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _costItemManager = new CostItemManager();
            _requisitionCategoryWiseCostItemSettingManager = new RequisitionCategoryWiseCostItemSettingManager();
            _isUpdateMode = false;
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
                if (_categoryId > 0)
                {
                    advanceCategoryComboBox.SelectedValue = _categoryId;
                }
            }
        }

        private void LoadCostItemDropdown()
        {
            var costItems = _costItemManager.GetAll().ToList();
            if (costItems.Any())
            {
                costItems.Insert(0, new CostItem { Id = DefaultItem.Value, Name = DefaultItem.Text });
                costItemComboBox.DataSource = null;
                costItemComboBox.DisplayMember = "Name";
                costItemComboBox.ValueMember = "Id";
                costItemComboBox.DataSource = costItems;
            }
        }

        //private void LoadCategoryWiseCostItemSettingsListView()
        //{
        //    categoryCostItemSettingsListView.Items.Clear();
        //    var categoryWiseCostItemSettings = _requisitionCategoryWiseCostItemSettingManager.GetAll().ToList();
        //    if (categoryWiseCostItemSettings.Any())
        //    {
        //        int serial = 1;
        //        foreach (var categoryWiseCostItemSetting in categoryWiseCostItemSettings)
        //        {
        //            ListViewItem item = new ListViewItem();
        //            item.Text = serial.ToString();
        //            item.SubItems.Add(categoryWiseCostItemSetting.AdvanceCategory.Name);
        //            item.SubItems.Add(categoryWiseCostItemSetting.CostItem.Name);
        //            item.SubItems.Add(categoryWiseCostItemSetting.EntitlementMandatoryStatus);
        //            item.Tag = categoryWiseCostItemSetting;
        //            categoryCostItemSettingsListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadCategoryWiseCostItemSettingsGridView()
        {
            categoryCostItemSettingsDataGridView.Rows.Clear();
            var categoryWiseCostItemSettings = _requisitionCategoryWiseCostItemSettingManager.GetAll().ToList();
            if (categoryWiseCostItemSettings.Any())
            {
                int serial = 1;
                foreach (var categoryWiseCostItemSetting in categoryWiseCostItemSettings)
                {
                    DataGridViewRow row =new DataGridViewRow();
                    row.CreateCells(categoryCostItemSettingsDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = serial.ToString();
                    row.Cells[2].Value = categoryWiseCostItemSetting.AdvanceCategory.Name;
                    row.Cells[3].Value = categoryWiseCostItemSetting.CostItem.Name;
                    row.Cells[4].Value = categoryWiseCostItemSetting.EntitlementMandatoryStatus;
                    
                    row.Tag = categoryWiseCostItemSetting;
                    categoryCostItemSettingsDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void RequisitionCategoryWiseCostItemSettingsUI_Load(object sender, System.EventArgs e)
        {
            try
            {
                LoadAvanceCategoryDropdown();
                LoadCostItemDropdown();
                LoadCategoryWiseCostItemSettingsGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                AdvanceCategoryWiseCostItemSetting categoryWiseCostItemSetting =
                    new AdvanceCategoryWiseCostItemSetting();
                if (int.Parse(advanceCategoryComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    categoryWiseCostItemSetting.AdvanceCategoryId =
                        (long)advanceCategoryComboBox.SelectedValue;
                }
                if (int.Parse(costItemComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    categoryWiseCostItemSetting.CostItemId = (long)costItemComboBox.SelectedValue;
                }
                categoryWiseCostItemSetting.IsEntitlementMandatory = isEntitlementMandatoryCheckBox.Checked;
                if (!_isUpdateMode)
                {
                    bool isInserted = _requisitionCategoryWiseCostItemSettingManager.Insert(categoryWiseCostItemSetting);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Settings saved successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        advanceCategoryComboBox.SelectedValue = DefaultItem.Value;
                        costItemComboBox.SelectedValue = DefaultItem.Value;
                        LoadCategoryWiseCostItemSettingsGridView();
                    }
                    else
                    {
                        MessageBox.Show(@"Settings save failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    categoryWiseCostItemSetting.Id = _updatebableRequisitionCategoryWiseCostItemSetting.Id;
                    bool isUpdated = _requisitionCategoryWiseCostItemSettingManager.Edit(categoryWiseCostItemSetting);
                    if (isUpdated)
                    {
                        saveButton.Text = @"Save";
                        MessageBox.Show(@"Settings updated successfully", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadCategoryWiseCostItemSettingsGridView();
                    }
                    else
                    {
                        MessageBox.Show(@"Settings update failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                ClearInputs();
            }
            catch (BllException exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            advanceCategoryComboBox.SelectedValue = DefaultItem.Value;
            costItemComboBox.SelectedValue = DefaultItem.Value;
            isEntitlementMandatoryCheckBox.Checked = false;
        }

        private void categoryCostItemSettingsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (categoryCostItemSettingsDataGridView.SelectedRows.Count > 0)
                    {
                        var categoryCostItem =
                            categoryCostItemSettingsDataGridView.SelectedRows[0].Tag as AdvanceCategoryWiseCostItemSetting;
                        if (categoryCostItem != null)
                        {
                            saveButton.Text = @"Update";
                            _isUpdateMode = true;
                            _updatebableRequisitionCategoryWiseCostItemSetting = categoryCostItem;
                            advanceCategoryComboBox.SelectedValue =
                                _updatebableRequisitionCategoryWiseCostItemSetting.AdvanceCategoryId;
                            costItemComboBox.SelectedValue = _updatebableRequisitionCategoryWiseCostItemSetting.CostItemId;
                            isEntitlementMandatoryCheckBox.Checked =
                                _updatebableRequisitionCategoryWiseCostItemSetting.IsEntitlementMandatory;
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please select an item", @"Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
