using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework.Controls;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class AccountConfigurationSettingsUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IChartOfAccountManager _chartOfAccountManager;
        private readonly IAccountConfigurationManager _accountConfigurationManager;
        private readonly IAccountTypeManager _accountTypeManager;
        private bool _isUpdateMode;
        private DataGridViewRow _updateListViewItem;

        public AccountConfigurationSettingsUI()
        {
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _chartOfAccountManager = new ChartOfAccountManager();
            _accountConfigurationManager = new AccountConfigurationManager();
            _accountTypeManager = new AccountTypeManager();
            _isUpdateMode = false;
            InitializeComponent();
        }

        private void AccountConfigurationSettingUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdvanceCategoryComboBox();
                LoadAccountCodeComboBox();
                LoadAccountTypeComboBox();
                var accountConfigurations = _accountConfigurationManager.GetAll();
                LoadAccountConfigurationGridView(accountConfigurations);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadAdvanceCategoryComboBox()
        {
            var advanceCategories = _advanceRequisitionCategoryManager.GetAll().ToList();
            advanceCategories.Insert(0, new AdvanceCategory() { Id = DefaultItem.Value, Name = DefaultItem.Text });

            advanceCategoryComboBox.DataSource = null;
            advanceCategoryComboBox.DisplayMember = "Name";
            advanceCategoryComboBox.ValueMember = "Id";
            advanceCategoryComboBox.DataSource = advanceCategories;
        }

        private void LoadAccountCodeComboBox()
        {
            var accountCodes = _chartOfAccountManager.GetAll().ToList();
            accountCodes.Insert(0, new Accounts_ChartOfAccounts() { AccountCode = DefaultItem.Text });

            accountCodeComboBox.DataSource = null;
            accountCodeComboBox.DisplayMember = "AccountCodeAndDescription";
            accountCodeComboBox.ValueMember = "AccountCode";
            accountCodeComboBox.DataSource = accountCodes;
        }

        private void LoadAccountTypeComboBox()
        {
            var accountTypes = _accountTypeManager.GetAll().ToList();
            accountTypes.Insert(0, new AccountType { Id = DefaultItem.Value, Name = DefaultItem.Text });

            accountTypeComboBox.DataSource = null;
            accountTypeComboBox.DisplayMember = "Name";
            accountTypeComboBox.ValueMember = "Id";
            accountTypeComboBox.DataSource = accountTypes;
        }
        
        private void LoadAccountConfigurationGridView(ICollection<AccountConfiguration> accountConfigurations)
        {
            accountConfigurationDataGridView.Rows.Clear();
            foreach (AccountConfiguration configuration in accountConfigurations)
            {
                DataGridViewRow row=new DataGridViewRow();
                row.CreateCells(accountConfigurationDataGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = "Remove";
                row.Cells[2].Value = configuration.AdvanceCategory.Name;
                row.Cells[3].Value = configuration.AccountType.Name;
                row.Cells[4].Value = configuration.AccountCode;
                
                row.Tag = configuration;
                accountConfigurationDataGridView.Rows.Add(row);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearInputs();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            advanceCategoryComboBox.Text = DefaultItem.Text;
            accountTypeComboBox.Text = DefaultItem.Text;
            accountCodeComboBox.Text = DefaultItem.Text;
            isDefaultAccountCodeCheckBox.Checked = false;
            _isUpdateMode = false;
            saveButton.Text = @"Save";
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;

            if ((long)advanceCategoryComboBox.SelectedValue == (long)DefaultItem.Value)
            {
                errorMessage += "Advance Category is not selected." + Environment.NewLine;
            }
            if ((long)accountTypeComboBox.SelectedValue == (long)DefaultItem.Value)
            {
                errorMessage += "Account type is not selected." + Environment.NewLine;
            }
            if (accountCodeComboBox.Text == DefaultItem.Text)
            {
                errorMessage += "Account Code is not selected." + Environment.NewLine;
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

        private void ChangeDefaultStatusInListView()
        {
            if (isDefaultAccountCodeCheckBox.Checked)
            {
                var accountSettings = GetAccountConfigarationFromGridView(accountConfigurationDataGridView).Where(c => c.AccountTypeId == (long)accountTypeComboBox.SelectedValue && c.AdvanceCategoryId == (long)advanceCategoryComboBox.SelectedValue).ToList();
                accountSettings.ForEach(c => c.IsDefaultAccount = false);
            }
        }

        private bool IsAccountCodeAlreadyExist(AccountConfiguration accountConfiguration)
        {
            var accountConfigurations = GetAccountConfigarationFromGridView(accountConfigurationDataGridView).ToList();
            if (accountConfigurations.Any(c => c.AccountCode.Equals(accountConfiguration.AccountCode) && c.AccountTypeId == accountConfiguration.AccountTypeId && c.AdvanceCategoryId == accountConfiguration.AdvanceCategoryId))
            {
                throw new UiException("Account Code already exists.");
            }
            return true;
        }
        
        private ICollection<AccountConfiguration> GetAccountConfigarationFromGridView(DataGridView gridView)
        {
            List<AccountConfiguration> accountConfigurations = new List<AccountConfiguration>();
            foreach (DataGridViewRow row in gridView.Rows)
            {
                var accountConfiguration = row.Tag as AccountConfiguration;
                if (accountConfiguration != null)
                    accountConfigurations.Add(accountConfiguration);
            }
            return accountConfigurations;
        }

        private DataGridViewRow  GetNewGridViewItem(AccountConfiguration accountConfiguration)
        {
            DataGridViewRow row=new DataGridViewRow();
            row.CreateCells(accountConfigurationDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = advanceCategoryComboBox.Text;
            row.Cells[3].Value = accountTypeComboBox.Text;
            row.Cells[4].Value = accountConfiguration.AccountCode;
            
            row.Tag = accountConfiguration;
            return row;
        }

        private void ResetInputField(MetroComboBox metroComboBox, MetroCheckBox metroCheckBox)
        {
            metroComboBox.Text = DefaultItem.Text;
            metroCheckBox.Checked = false;
        }

        DataGridViewRow  SetChangedGridViewItem(AccountConfiguration accountConfiguration, DataGridViewRow row)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = advanceCategoryComboBox.Text;
            row.Cells[3].Value = accountTypeComboBox.Text;
            row.Cells[4].Value = accountCodeComboBox.Text;
           
            row.Tag = accountConfiguration;
            return row;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                AccountConfiguration accountConfiguration = new AccountConfiguration();
                accountConfiguration.AdvanceCategoryId = (long)advanceCategoryComboBox.SelectedValue;
                accountConfiguration.AccountTypeId = (long)accountTypeComboBox.SelectedValue;
                accountConfiguration.AccountCode = accountCodeComboBox.SelectedValue.ToString();
                accountConfiguration.IsDefaultAccount = isDefaultAccountCodeCheckBox.Checked;
                bool isSaved = false;
                if (!_isUpdateMode)
                {
                    IsAccountCodeAlreadyExist(accountConfiguration);
                    var item = GetNewGridViewItem(accountConfiguration);
                    accountConfigurationDataGridView.Rows.Add(item);
                    isSaved = _accountConfigurationManager.Insert(accountConfiguration);
                }
                else
                {
                    var updateableAccountConfigaration = _updateListViewItem.Tag as AccountConfiguration;
                    accountConfiguration.Id = updateableAccountConfigaration.Id;
                    SetChangedGridViewItem(accountConfiguration, _updateListViewItem);
                    isSaved = _accountConfigurationManager.Edit(accountConfiguration);
                    saveButton.Text = @"Save";
                }
                ResetAccountCodeInputs();
                FilterAccountConfigurationListView();
                if (isSaved)
                {
                    MessageBox.Show(@"Account configuration setting saved successfully.", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(@"Account configuration setting failed to save.", @"Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ResetAccountCodeInputs()
        {
            accountCodeComboBox.Text = DefaultItem.Text;
        }

        private void EditAccountConfiguration()
        {
            if (accountConfigurationDataGridView.SelectedRows.Count > 0)
            {
                var accountConfigaration = accountConfigurationDataGridView.SelectedRows[0].Tag as AccountConfiguration;

                if (accountConfigaration == null)
                    throw new UiException("No tag item found.");

                _updateListViewItem = accountConfigurationDataGridView.SelectedRows[0];
                advanceCategoryComboBox.SelectedValue = accountConfigaration.AdvanceCategoryId;
                accountTypeComboBox.SelectedValue = accountConfigaration.AccountTypeId;
                accountCodeComboBox.SelectedValue = accountConfigaration.AccountCode;
                isDefaultAccountCodeCheckBox.Checked = accountConfigaration.IsDefaultAccount;
                _isUpdateMode = true;
                saveButton.Text = @"Update";
            }
            else
            {
                MessageBox.Show(@"Please Choose an item to edit.", @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RemoveAccountConfiguration()
        {
            if (accountConfigurationDataGridView.SelectedRows.Count > 0)
            {
                int detailItem =
                         accountConfigurationDataGridView.SelectedRows[0].Index;
                var configuration =
                    accountConfigurationDataGridView.SelectedRows[0].Tag as AccountConfiguration;
                if (configuration != null)
                {
                    DialogResult result = MessageBox.Show(@"Are you sure you want to remove the selected account configuration?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    var isRemoved = _accountConfigurationManager.Delete(configuration.Id);
                    accountConfigurationDataGridView.Rows.RemoveAt(detailItem);
                    if (isRemoved)
                    {
                        MessageBox.Show(@"Account configuration setting removed successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(@"Account configuration setting failed to remove.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(@"Null Reference", @"Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(@"Please choose an item to edit.", @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceCategoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FilterAccountConfigurationListView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accountTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FilterAccountConfigurationListView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterAccountConfigurationListView()
        {
            var advanceCategoryId = (long)advanceCategoryComboBox.SelectedValue;
            var accountTypeId = (long)accountTypeComboBox.SelectedValue;
            var accountConfigurations =
                _accountConfigurationManager.GetBy(advanceCategoryId, accountTypeId);
            LoadAccountConfigurationGridView(accountConfigurations);
        }

        private void accountConfigurationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView) sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        EditAccountConfiguration();
                    }

                    else  if(e.ColumnIndex==1)
                    {
                        RemoveAccountConfiguration(); 
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
