using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using CurrencyManager = IDCOLAdvanceModule.BLL.MISManager.CurrencyManager;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class ExecutiveOverseasTravelingAllowanceUI : Form
    {
        private readonly ICurrencyManager _currencyManager;
        private readonly ICostItemManager _costItemManager;
        private IOverseasTravelGroupManager _overseasTravelGroupManager;
        private IOverseasTravelWiseCostItemSettingManager _overseasTravelWiseCostItemSettingManager;
        private bool _isUpdateMode;
        private ExecutiveOverseasTravellingAllowance _updateableExecutiveOverseasTravellingAllowance;
        private readonly IEmployeeCategoryManager _employeeCategoryManager;
        private readonly ILocationGroupManager _locationGroupManager;
        private readonly IExecutiveOverseasTravellingAllowanceManager _executiveOverseasTravellingAllowanceManager;
        public ExecutiveOverseasTravelingAllowanceUI()
        {
            _currencyManager = new CurrencyManager();
            _costItemManager = new CostItemManager();
            _overseasTravelGroupManager = new OverseasTravelGroupManager();
            _overseasTravelWiseCostItemSettingManager = new OverseasTravelWiseCostItemSettingManager();
            _employeeCategoryManager = new EmployeeCategoryManager();
            _isUpdateMode = false;
            _locationGroupManager = new LocationGroupManager();
            _executiveOverseasTravellingAllowanceManager = new ExecutiveOverseasTravellingAllowanceManager();
            InitializeComponent();
        }

        private void OverseasTravelCostItemSettingUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadEmployeeCategory();
                LoadCurrencyComboBox();
                LoadTravelCostItemDropDown();
                LoadLocationGroup();
                LoadExecutiveOverseasTravellingAllowance();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadEmployeeCategory()
        {
            var employeeCategories = _employeeCategoryManager.GetAll().Where(c => !c.Name.Equals("Executive")).ToList();
            if (employeeCategories.Any())
            {
                employeeCategories.Insert(0, new EmployeeCategory() { Id = DefaultItem.Value, Name = DefaultItem.Text });
                employeeCategoryComboBox.DataSource = employeeCategories;
                employeeCategoryComboBox.DisplayMember = "Name";
                employeeCategoryComboBox.ValueMember = "Id";
            }
        }

        private void LoadLocationGroup()
        {
            var locationGroups = _locationGroupManager.GetAll().ToList();
            if (locationGroups.Any())
            {
                locationGroups.Insert(0, new LocationGroup() { Id = DefaultItem.Value, Name = DefaultItem.Text });
                locationGroupComboBox.DataSource = locationGroups;
                locationGroupComboBox.DisplayMember = "Name";
                locationGroupComboBox.ValueMember = "Id";
            }
        }

        private void LoadCurrencyComboBox()
        {
            currencyComboBox.DataSource = null;

            List<Solar_CurrencyInfo> currencyList =
                _currencyManager.GetAllForAdvanceRequisitionAndExpense().ToList();
            currencyList.Insert(0, new Solar_CurrencyInfo { CurrencyID = DefaultItem.Value, ShortName = DefaultItem.Text });
            currencyComboBox.DisplayMember = "ShortName";
            currencyComboBox.ValueMember = "CurrencyID";
            currencyComboBox.DataSource = currencyList;
        }

        private void LoadTravelCostItemDropDown()
        {
            var costItems = _costItemManager.GetByBaseAdvanceCategory((long)BaseAdvanceCategoryEnum.Travel).ToList();
            if (costItems.Any())
            {
                costItems.Insert(0, new CostItem { Id = DefaultItem.Value, Name = DefaultItem.Text });
                costItemComboBox.DataSource = costItems;
                costItemComboBox.DisplayMember = "Name";
                costItemComboBox.ValueMember = "Id";
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                ExecutiveOverseasTravellingAllowance executiveOverseasTravellingAllowance = new ExecutiveOverseasTravellingAllowance
                {
                    LocationGroupId = (long)locationGroupComboBox.SelectedValue,
                    CostItemId = (long)costItemComboBox.SelectedValue,
                    Currency = currencyComboBox.Text,
                    EntitlementAmount = entitlementAmountTextBox.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(entitlementAmountTextBox.Text),
                    EmployeeCategoryId = (long)employeeCategoryComboBox.SelectedValue,
                    IsFullAmountEntitlement =  isFullAmountEntitlementCheckBox.Checked
                   
                };
                if (!_isUpdateMode)
                {
                    bool isSaved = _executiveOverseasTravellingAllowanceManager.Insert(executiveOverseasTravellingAllowance);
                    if (isSaved)
                    {
                        MessageBox.Show(@"Successfully saved.", @"Success", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    }
                }
                else
                {
                    executiveOverseasTravellingAllowance.Id = _updateableExecutiveOverseasTravellingAllowance.Id;
                    bool isUpdated = _executiveOverseasTravellingAllowanceManager.Edit(executiveOverseasTravellingAllowance);
                    if (isUpdated)
                    {
                        MessageBox.Show(@"Successfully updated.", @"Success", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        saveButton.Text = @"Save";
                        _isUpdateMode = false;
                    }
                }
                LoadExecutiveOverseasTravellingAllowance();
                ClearInputs();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }
        }

        private void ClearInputs()
        {
            locationGroupComboBox.SelectedValue = DefaultItem.Value;
            costItemComboBox.SelectedValue = DefaultItem.Value;
            currencyComboBox.SelectedValue = DefaultItem.Value;
            entitlementAmountTextBox.Text = string.Empty;
            employeeCategoryComboBox.SelectedValue = DefaultItem.Value;
            isFullAmountEntitlementCheckBox.Checked = false;
        }

        private bool ValidateSave()
        {
            string errorMessege = string.Empty;

            if ((long)locationGroupComboBox.SelectedValue == DefaultItem.Value)
            {
                errorMessege += "Travel Group is required." + Environment.NewLine;
            }
            if ((long)costItemComboBox.SelectedValue == DefaultItem.Value)
            {
                errorMessege += "Cost Item is required." + Environment.NewLine;
            }
            if (Convert.ToDecimal(currencyComboBox.SelectedValue) == Convert.ToDecimal(DefaultItem.Value))
            {
                errorMessege += "Currency is required." + Environment.NewLine;
            }
            if (!isFullAmountEntitlementCheckBox.Checked && string.IsNullOrEmpty(entitlementAmountTextBox.Text))
            {
                errorMessege += "Entitlement Amount is required." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessege))
            {
                throw new UiException(errorMessege);
            }
            return true;
        }

        //private void LoadExecutiveOverseasTravellingAllowance()
        //{
        //    executiveOverseasTravellingAllowanceListView.Items.Clear();
        //    var executiveOverseasTravellingAllowances = _executiveOverseasTravellingAllowanceManager.GetAll().ToList();
        //    int serial = 1;
        //    foreach (var overseasTravelWiseCostItemSetting in executiveOverseasTravellingAllowances)
        //    {
        //        ListViewItem item = new ListViewItem(serial.ToString());
        //        item.SubItems.Add(overseasTravelWiseCostItemSetting.CostItem.Name);
        //        item.SubItems.Add(overseasTravelWiseCostItemSetting.LocationGroup.Name);
        //        item.SubItems.Add(overseasTravelWiseCostItemSetting.Currency);
        //        item.SubItems.Add(overseasTravelWiseCostItemSetting.EntitlementAmount.ToString());
        //        item.Tag = overseasTravelWiseCostItemSetting;
        //        executiveOverseasTravellingAllowanceListView.Items.Add(item);
        //    }
        //}

        private void LoadExecutiveOverseasTravellingAllowance()
        {
            executiveOverseasTravellingAllowanceDataGridView.Rows.Clear();
            var executiveOverseasTravellingAllowances = _executiveOverseasTravellingAllowanceManager.GetAll().ToList();
            int serial = 1;
            foreach (var overseasTravelWiseCostItemSetting in executiveOverseasTravellingAllowances)
            {
                DataGridViewRow row =new DataGridViewRow();
                row.CreateCells(executiveOverseasTravellingAllowanceDataGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = serial.ToString();
                row.Cells[2].Value = overseasTravelWiseCostItemSetting.CostItem.Name;
                row.Cells[3].Value = overseasTravelWiseCostItemSetting.LocationGroup.Name;
                row.Cells[4].Value = overseasTravelWiseCostItemSetting.Currency;
                row.Cells[5].Value = overseasTravelWiseCostItemSetting.EntitlementAmount.ToString();
                
                row.Tag = overseasTravelWiseCostItemSetting;
                executiveOverseasTravellingAllowanceDataGridView.Rows.Add(row);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (executiveOverseasTravellingAllowanceListView.SelectedItems.Count > 0)
                //{
                //    saveButton.Text = @"Update";
                //    _isUpdateMode = true;
                //    ExecutiveOverseasTravellingAllowance executiveOverseasTravellingAllowance = executiveOverseasTravellingAllowanceListView.SelectedItems[0].Tag as ExecutiveOverseasTravellingAllowance;
                //    _updateableExecutiveOverseasTravellingAllowance = executiveOverseasTravellingAllowance;
                //    locationGroupComboBox.SelectedValue = executiveOverseasTravellingAllowance.LocationGroupId;
                //    costItemComboBox.SelectedValue = executiveOverseasTravellingAllowance.CostItemId;
                //    currencyComboBox.Text = executiveOverseasTravellingAllowance.Currency;
                //    entitlementAmountTextBox.Text = executiveOverseasTravellingAllowance.EntitlementAmount == null ? string.Empty : executiveOverseasTravellingAllowance.EntitlementAmount.ToString();
                //    employeeCategoryComboBox.SelectedValue = executiveOverseasTravellingAllowance.EmployeeCategoryId;
                //    isFullAmountEntitlementCheckBox.Checked =
                //        executiveOverseasTravellingAllowance.IsFullAmountEntitlement;
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
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

        private void isFullAmountEntitlementCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (isFullAmountEntitlementCheckBox.Checked)
                {
                    entitlementAmountTextBox.Enabled = false;
                    entitlementAmountTextBox.Text = string.Empty;
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

        private void executiveOverseasTravellingAllowanceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (executiveOverseasTravellingAllowanceDataGridView.SelectedRows.Count > 0)
                    {
                        saveButton.Text = @"Update";
                        _isUpdateMode = true;
                        ExecutiveOverseasTravellingAllowance executiveOverseasTravellingAllowance = executiveOverseasTravellingAllowanceDataGridView.SelectedRows[0].Tag as ExecutiveOverseasTravellingAllowance;
                        _updateableExecutiveOverseasTravellingAllowance = executiveOverseasTravellingAllowance;
                        locationGroupComboBox.SelectedValue = executiveOverseasTravellingAllowance.LocationGroupId;
                        costItemComboBox.SelectedValue = executiveOverseasTravellingAllowance.CostItemId;
                        currencyComboBox.Text = executiveOverseasTravellingAllowance.Currency;
                        entitlementAmountTextBox.Text = executiveOverseasTravellingAllowance.EntitlementAmount == null ? string.Empty : executiveOverseasTravellingAllowance.EntitlementAmount.ToString();
                        employeeCategoryComboBox.SelectedValue = executiveOverseasTravellingAllowance.EmployeeCategoryId;
                        isFullAmountEntitlementCheckBox.Checked =
                            executiveOverseasTravellingAllowance.IsFullAmountEntitlement;
                    }
                    else
                    {
                        MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
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
