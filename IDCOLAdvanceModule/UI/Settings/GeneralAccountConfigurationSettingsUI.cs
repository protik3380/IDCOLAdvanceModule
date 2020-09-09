using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using MetroFramework.Controls;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class GeneralAccountConfigurationSettingsUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IChartOfAccountManager _chartOfAccountManager;
        private readonly IGeneralAccountConfigurationManager _generalAccountConfigurationManager;
        private readonly IAccountTypeManager _accountTypeManager;
        private bool _isUpdateMode;
        private DataGridViewRow _updateListViewItem;

        public GeneralAccountConfigurationSettingsUI()
        {
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _chartOfAccountManager = new ChartOfAccountManager();
            _generalAccountConfigurationManager = new GeneralAccountConfigurationManager();
            _accountTypeManager = new AccountTypeManager();
            _isUpdateMode = false;
            InitializeComponent();
        }

        private void GeneralAccountConfigurationSettingsUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAccountCodeComboBox();
                LoadAccountTypeComboBox();
                var configurations = _generalAccountConfigurationManager.GetAll();
                LoadGeneralAccountConfigurationGridView(configurations);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        //private void LoadGeneralAccountConfigurationListView(ICollection<GeneralAccountConfiguration> configurations)
        //{
        //    generalAccountConfigurationListView.Items.Clear();
        //    foreach (GeneralAccountConfiguration configuration in configurations)
        //    {
        //        ListViewItem item = new ListViewItem(configuration.AccountType.Name);
        //        item.SubItems.Add(configuration.AccountCode);
        //        item.Tag = configuration;
        //        generalAccountConfigurationListView.Items.Add(item);
        //    }
        //}
        private void LoadGeneralAccountConfigurationGridView(ICollection<GeneralAccountConfiguration> configurations)
        {
            generalAccountConfigurationDataGridView.Rows.Clear();
            foreach (GeneralAccountConfiguration configuration in configurations)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(generalAccountConfigurationDataGridView);

                row.Cells[0].Value = configuration.AccountType.Name;
                row.Cells[1].Value = configuration.AccountCode;
                row.Cells[2].Value = "Edit";
                row.Cells[3].Value = "Remove";
                row.Tag = configuration;
                generalAccountConfigurationDataGridView.Rows.Add(row);
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
            accountTypeComboBox.Text = DefaultItem.Text;
            accountCodeComboBox.Text = DefaultItem.Text;
            isDefaultAccountCodeCheckBox.Checked = false;
            _isUpdateMode = false;
            saveButton.Text = @"Save";
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;

            if ((long)accountTypeComboBox.SelectedValue == DefaultItem.Value)
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

        //private void ChangeDefaultStatusInListView()
        //{
        //    if (isDefaultAccountCodeCheckBox.Checked)
        //    {
        //        var accountSettings = GetAccountConfigarationFromListView(generalAccountConfigurationListView).Where(c => c.AccountTypeId == (long)accountTypeComboBox.SelectedValue).ToList();
        //        accountSettings.ForEach(c => c.IsDefaultAccount = false);
        //    }
        //}
        private void ChangeDefaultStatusInGridView()
        {
            if (isDefaultAccountCodeCheckBox.Checked)
            {
                var accountSettings = GetAccountConfigarationFromGridView(generalAccountConfigurationDataGridView).Where(c => c.AccountTypeId == (long)accountTypeComboBox.SelectedValue).ToList();
                accountSettings.ForEach(c => c.IsDefaultAccount = false);
            }
        }

        //private bool IsAccountCodeAlreadyExist(GeneralAccountConfiguration accountConfiguration)
        //{
        //    var accountConfigurations = GetAccountConfigarationFromListView(generalAccountConfigurationListView).ToList();
        //    if (accountConfigurations.Any(c => c.AccountCode.Equals(accountConfiguration.AccountCode) && c.AccountTypeId == accountConfiguration.AccountTypeId))
        //    {
        //        throw new UiException("Account Code already exists.");
        //    }
        //    return true;
        //}
        private bool IsAccountCodeAlreadyExist(GeneralAccountConfiguration accountConfiguration)
        {
            var accountConfigurations = GetAccountConfigarationFromGridView(generalAccountConfigurationDataGridView).ToList();
            if (accountConfigurations.Any(c => c.AccountCode.Equals(accountConfiguration.AccountCode) && c.AccountTypeId == accountConfiguration.AccountTypeId))
            {
                throw new UiException("Account Code already exists.");
            }
            return true;
        }

        //private ICollection<GeneralAccountConfiguration> GetAccountConfigarationFromListView(ListView listView)
        //{
        //    List<GeneralAccountConfiguration> accountConfigurations = new List<GeneralAccountConfiguration>();
        //    foreach (ListViewItem item in listView.Items)
        //    {
        //        var accountConfiguration = item.Tag as GeneralAccountConfiguration;
        //        if (accountConfiguration != null)
        //            accountConfigurations.Add(accountConfiguration);
        //    }
        //    return accountConfigurations;
        //}
        private ICollection<GeneralAccountConfiguration> GetAccountConfigarationFromGridView(DataGridView gridView)
        {
            List<GeneralAccountConfiguration> accountConfigurations = new List<GeneralAccountConfiguration>();
            foreach (DataGridViewRow row in gridView.Rows)
            {
                var accountConfiguration = row.Tag as GeneralAccountConfiguration;
                if (accountConfiguration != null)
                    accountConfigurations.Add(accountConfiguration);
            }
            return accountConfigurations;
        }

        //private ListViewItem GetNewListViewItem(GeneralAccountConfiguration generalAccountConfiguration)
        //{
        //    ListViewItem item = new ListViewItem(accountTypeComboBox.Text);
        //    item.SubItems.Add(generalAccountConfiguration.AccountCode);
        //    item.Tag = generalAccountConfiguration;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewRow(GeneralAccountConfiguration generalAccountConfiguration)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(generalAccountConfigurationDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = accountTypeComboBox.Text;
            row.Cells[3].Value = generalAccountConfiguration.AccountCode;
            row.Tag = generalAccountConfiguration;
            return row;
        }



        private void ResetInputField(MetroComboBox metroComboBox, MetroCheckBox metroCheckBox)
        {
            metroComboBox.Text = DefaultItem.Text;
            metroCheckBox.Checked = false;
        }

        //ListViewItem SetChangedListViewItem(GeneralAccountConfiguration configuration, ListViewItem item)
        //{
        //    item.Text = accountTypeComboBox.Text;
        //    item.SubItems[1].Text = accountCodeComboBox.Text;
        //    item.Tag = configuration;
        //    return item;
        //}
        DataGridViewRow SetChangedListViewItem(GeneralAccountConfiguration configuration, DataGridViewRow row)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = accountTypeComboBox.Text;
            row.Cells[3].Value = accountCodeComboBox.Text;
            row.Tag = configuration;
            return row;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //ValidateSave();
                //GeneralAccountConfiguration accountConfiguration = new GeneralAccountConfiguration();
                //accountConfiguration.AccountTypeId = (long)accountTypeComboBox.SelectedValue;
                //accountConfiguration.AccountCode = accountCodeComboBox.SelectedValue.ToString();
                //accountConfiguration.IsDefaultAccount = isDefaultAccountCodeCheckBox.Checked;
                //bool isSaved = false;
                //if (!_isUpdateMode)
                //{
                //    IsAccountCodeAlreadyExist(accountConfiguration);
                //    var item = GetNewListViewItem(accountConfiguration);
                //    generalAccountConfigurationListView.Items.Add(item);
                //    isSaved = _generalAccountConfigurationManager.Insert(accountConfiguration);
                //}
                //else
                //{
                //    var updateableAccountConfigaration = _updateListViewItem.Tag as GeneralAccountConfiguration;
                //    accountConfiguration.Id = updateableAccountConfigaration.Id;
                //    SetChangedListViewItem(accountConfiguration, _updateListViewItem);
                //    isSaved = _generalAccountConfigurationManager.Edit(accountConfiguration);
                //}
                //ResetAccountCode();
                //FilterAccountConfigurationListView();
                //if (isSaved)
                //{
                //    MessageBox.Show(@"Account configuration setting saved successfully.", @"Success", MessageBoxButtons.OK,
                //        MessageBoxIcon.Information);

                //}
                //else
                //{
                //    MessageBox.Show(@"Account configuration setting failed to save.", @"Validation Error!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);
                //}

                ValidateSave();
                GeneralAccountConfiguration accountConfiguration = new GeneralAccountConfiguration();
                accountConfiguration.AccountTypeId = (long)accountTypeComboBox.SelectedValue;
                accountConfiguration.AccountCode = accountCodeComboBox.SelectedValue.ToString();
                accountConfiguration.IsDefaultAccount = isDefaultAccountCodeCheckBox.Checked;
                bool isSaved = false;
                if (!_isUpdateMode)
                {
                    IsAccountCodeAlreadyExist(accountConfiguration);
                    var item = GetNewGridViewRow(accountConfiguration);
                    generalAccountConfigurationDataGridView.Rows.Add(item);
                    isSaved = _generalAccountConfigurationManager.Insert(accountConfiguration);
                }
                else
                {
                    var updateableAccountConfigaration = _updateListViewItem.Tag as GeneralAccountConfiguration;
                    accountConfiguration.Id = updateableAccountConfigaration.Id;
                    SetChangedListViewItem(accountConfiguration, _updateListViewItem);
                    isSaved = _generalAccountConfigurationManager.Edit(accountConfiguration);
                }
                ResetAccountCode();
                FilterAccountConfigurationListView();
                if (isSaved)
                {
                    MessageBox.Show(@"Account configuration setting saved successfully.", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(@"Account configuration setting failed to save.", @"Validation Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ResetAccountCode()
        {
            accountCodeComboBox.Text = DefaultItem.Text;
        }

        private void ReloadListView(DataGridView gridView, ICollection<GeneralAccountConfiguration> configurations)
        {
            gridView.Rows.Clear();
            foreach (var configuration in configurations)
            {
                var item = GetNewGridViewRow(configuration);
                gridView.Rows.Add(item);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    EditAccountConfiguration();
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //}
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
            var accountTypeId = (long)accountTypeComboBox.SelectedValue;
            var configurations =
                _generalAccountConfigurationManager.GetByAccountTypeId(accountTypeId);
            LoadGeneralAccountConfigurationGridView(configurations);
        }

        private void generalAccountConfigurationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (generalAccountConfigurationDataGridView.SelectedRows.Count > 0)
                        {
                            var accountConfigaration = generalAccountConfigurationDataGridView.SelectedRows[0].Tag as GeneralAccountConfiguration;

                            if (accountConfigaration == null)
                                throw new UiException("No tag item found.");

                            _updateListViewItem = generalAccountConfigurationDataGridView.SelectedRows[0];
                            accountTypeComboBox.SelectedValue = accountConfigaration.AccountTypeId;
                            accountCodeComboBox.SelectedValue = accountConfigaration.AccountCode;
                            isDefaultAccountCodeCheckBox.Checked = accountConfigaration.IsDefaultAccount;
                            _isUpdateMode = true;
                            saveButton.Text = @"Update";
                        }
                        else
                        {
                            MessageBox.Show(@"Please Choose an item to edit", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        if (generalAccountConfigurationDataGridView.SelectedRows.Count > 0)
                        {
                            DialogResult result = MessageBox.Show(@"Are you sure you want to remove the selected general account configuration?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result == DialogResult.No)
                            {
                                return;
                            }
                            int detailItem =
                                     generalAccountConfigurationDataGridView.SelectedRows[0].Index;
                            var configuration =
                                generalAccountConfigurationDataGridView.SelectedRows[0].Tag as GeneralAccountConfiguration;
                            if (configuration != null)
                            {
                                var isRemoved = _generalAccountConfigurationManager.Delete(configuration.Id);
                                generalAccountConfigurationDataGridView.Rows.RemoveAt(detailItem);
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
                                MessageBox.Show(@"Null Reference.", @"Error!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Please Choose an item to edit.", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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
