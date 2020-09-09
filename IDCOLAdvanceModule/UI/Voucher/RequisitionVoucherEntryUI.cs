using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Accounts.NerdCastle.Common.Enum;
using Accounts.NerdCastle.NCBLL;
using Accounts.NerdCastle.NerdCastleUI.ModalUI;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.ViewModels;
using MetroFramework;
using AccountConfigurationManager = IDCOLAdvanceModule.BLL.AdvanceManager.AccountConfigurationManager;
using Accounts_NC_VatTaxCategory = Accounts.NerdCastle.NCDAO.Accounts_NC_VatTaxCategory;
using DefaultItem = IDCOLAdvanceModule.Utility.DefaultItem;


namespace IDCOLAdvanceModule.UI.Voucher
{
    public partial class RequisitionVoucherEntryUI : Form
    {
        private readonly IBankManager _bankManager;
        private readonly IBranchManager _branchManager;
        private readonly IChartOfAccountManager _chartOfAccountManager;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IRequisitionVoucherHeaderManager _requisitionVoucherHeaderManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly UserTable _requester;
        private readonly AdvanceRequisitionHeader _requisitionHeader;
        private bool _isDetailUpdateMode;
        private bool _isRemoveMode;
        private DataGridViewRow _updateableDetailItem;
        private bool _isUpdateMode;
        private readonly RequisitionVoucherHeader _updateableVoucherHeader;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IRequisitionSourceOfFundManager _requisitionSourceOfFundManager;
        private readonly IVoucherTypeManager _voucherTypeManager;
        private readonly RecipientVM _recipient;
        private readonly IAccountConfigurationManager _accountConfigurationManager;
        private readonly AdvancedFormMode _formMode;
        private readonly Accounts_NC_VendorInfoManager _accountsNcVendorInfoManager;
        private readonly Accounts_NC_VatTaxCategoryManager _accountsNcVatTaxCategoryManager;



        private RequisitionVoucherEntryUI()
        {
            InitializeComponent();
            _bankManager = new BankManager();
            _branchManager = new BranchManager();
            _chartOfAccountManager = new ChartOfAccountManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
            _employeeManager = new EmployeeManager();
            _requisitionVoucherHeaderManager = new RequisitionVoucherHeaderManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionSourceOfFundManager = new RequisitionSourceOfFundManager();
            _voucherTypeManager = new VoucherTypeManager();
            _accountConfigurationManager = new AccountConfigurationManager();

            _isDetailUpdateMode = false;
            _isRemoveMode = false;
            _isUpdateMode = false;
            _formMode = AdvancedFormMode.Create;
            _accountsNcVendorInfoManager = new Accounts_NC_VendorInfoManager();
            _accountsNcVatTaxCategoryManager = new Accounts_NC_VatTaxCategoryManager();
        }

        public RequisitionVoucherEntryUI(AdvanceRequisitionHeader requisitionHeader, RecipientVM recipient)
            : this()
        {
            _requisitionHeader = requisitionHeader;
            _recipient = recipient;
            LoadRecipientPayeNameTextBox();
            LoadVoucherDescriptionFromHeaderPurpose();
            LoadRequisitionDetailInformationFromRequisition(requisitionHeader);
            _requester = _employeeManager.GetByUserName(requisitionHeader.RequesterUserName);
        }

        private void LoadRecipientPayeNameTextBox()
        {
            recipientPayeeTextBox.Text = _recipient.Name;
        }

        private void LoadVoucherDescriptionFromHeaderPurpose()
        {
            voucherDescriptionTextBox.Text = _requisitionHeader.Purpose;
        }

        public RequisitionVoucherEntryUI(RequisitionVoucherHeader requisitionVoucherHeader, AdvancedFormMode formMode)
            : this()
        {
            this._formMode = formMode;
            _updateableVoucherHeader = requisitionVoucherHeader;
            _recipient = new RecipientVM { Name = requisitionVoucherHeader.RecipientName };
            LoadRecipientPayeNameTextBox();
            _isUpdateMode = formMode == AdvancedFormMode.Update;
            _requisitionHeader = _advanceRequisitionHeaderManager.GetById(requisitionVoucherHeader.RequisitionHeaderId);
            _requester = _employeeManager.GetByUserName(requisitionVoucherHeader.RequisitionHeader.RequesterUserName);
            LoadVoucherDetailVoucherDetailGridView(requisitionVoucherHeader.RequisitionVoucherDetails.ToList());
            SetTotalAmountInGridView(voucherDetailDataGridView);
            if (formMode == AdvancedFormMode.Update)
            {
                SetFormToUpdateMode();
            }
            else if (formMode == AdvancedFormMode.View)
            {
                SetFormToViewMode();
            }
        }

        private void LoadVoucherTypeComboBox()
        {
            var voucherTypes = _voucherTypeManager.GetOperationalTypes().ToList();

            voucherTypes.Insert(0, new Accounts_VoucherTypes() { VouTypeId = (short)DefaultItem.Value, VouType = DefaultItem.Text });
            voucherTypeComboBox.DataSource = null;
            voucherTypeComboBox.DisplayMember = "VouType";
            voucherTypeComboBox.ValueMember = "VouTypeId";
            voucherTypeComboBox.DataSource = voucherTypes;
            if (_formMode == AdvancedFormMode.Update)
            {
                voucherTypeComboBox.SelectedValue = (short)_updateableVoucherHeader.VoucherTypeId;
            }
        }

        private void LoadVoucherHeader(RequisitionVoucherHeader requisitionVoucherHeader)
        {
            if (requisitionVoucherHeader.BankId != null)
            {
                var bank = _bankManager.GetById((long)requisitionVoucherHeader.BankId);
                if (bank != null)
                {
                    bankComboBox.Text = bank.Bank_Name;
                }
            }
            if (requisitionVoucherHeader.BranchId != null)
            {
                var branch = _branchManager.GetById((long)requisitionVoucherHeader.BranchId);
                if (branch != null)
                {
                    branchComboBox.Text = branch.Branch_Name;
                }
            }
            if (requisitionVoucherHeader.VoucherTypeId != null)
            {
                var voucherType = _voucherTypeManager.Get(c => c.VouTypeId == requisitionVoucherHeader.VoucherTypeId).FirstOrDefault();

                if (voucherType != null) voucherTypeComboBox.Text = voucherType.VouType;
            }
            recipientPayeeTextBox.Text = _recipient.Name;
            chequeTextBox.Text = requisitionVoucherHeader.ChequeNo;
            voucherDescriptionTextBox.Text = requisitionVoucherHeader.VoucherDescription;
        }

        private void SetFormToUpdateMode()
        {
            _isUpdateMode = true;
            saveButton.Text = @"Update";
        }

        private void SetFormToViewMode()
        {
            voucherHeaderGroupBox.Enabled = false;
            voucherDetailsGroupBox.Enabled = false;
            addButton.Visible = false;
            saveButton.Visible = false;
        }

        private void RequisitionPaymentUI_Load(object sender, EventArgs e)
        {
            try
            {
                voucherDateDateTime.Value = DateTime.Now;
                LoadRequisitionBasicInformation();
                LoadVoucherTypeComboBox();
                LoadAccountComboByAccountCodes();
                LoadBankComboBox();
                LoadBranchComboBox(DefaultItem.Value);
                LoadVendor();
                if (_isUpdateMode && _updateableVoucherHeader != null)
                {
                    LoadVoucherHeader(_updateableVoucherHeader);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVendor()
        {
            vendorComboBox.DataSource = null;
            List<Accounts.NerdCastle.NCDAO.Accounts_NC_VendorInfo> vendors = new List<Accounts.NerdCastle.NCDAO.Accounts_NC_VendorInfo>
            {
                new Accounts.NerdCastle.NCDAO.Accounts_NC_VendorInfo { Id= (int) DefaultItem.Value, Name = DefaultItem.Text}
            };
            vendors.AddRange(_accountsNcVendorInfoManager.GetAll());
            vendorComboBox.DataSource = null;
            vendorComboBox.DisplayMember = "Name";
            vendorComboBox.ValueMember = "Id";
            vendorComboBox.DataSource = vendors;
        }

        private void LoadRequisitionBasicInformation()
        {
            if (_requisitionHeader == null)
            {
                throw new UiException("Requisition not found.");
            }
            requisitionNoTextBox.Text = _requisitionHeader.RequisitionNo;
            AdvanceCategory category = _advanceRequisitionCategoryManager.GetById(_requisitionHeader.AdvanceCategoryId);
            if (category == null)
            {
                throw new UiException("Category not found.");
            }
            advanceCategoryTextBox.Text = category.Name;
            if (_requester == null)
            {
                throw new UiException("Requester information not found.");
            }
            designationTextBox.Text = _requester.Admin_Rank == null ? "N/A" : _requester.Admin_Rank.RankName;
            employeeNameTextBox.Text = _requester.FullName;
            employeeIdTextBox.Text = _requester.EmployeeID;
            departmentTextBox.Text = _requester.Admin_Departments == null ? "N/A" : _requester.Admin_Departments.DepartmentName;
            fromDateTextBox.Text = _requisitionHeader.FromDate.ToString("dd-MMM-yyyy");
            toDateTextBox.Text = _requisitionHeader.ToDate.ToString("dd-MMM-yyyy");
            advanceAmountTextBox.Text = _requisitionHeader.GetTotalAdvanceAmount().ToString("N");
            purposeTextBox.Text = _requisitionHeader.Purpose;
            if (_requisitionHeader.AdvanceIssueDate == null)
            {
                throw new UiException("Advance is not issued yet.");
            }
            advanceIssueDateTextBox.Text = _requisitionHeader.AdvanceIssueDate.Value.ToString("dd-MMM-yyyy");
            ICollection<RequisitionSourceOfFund> requisitionSourceOfFunds = _requisitionSourceOfFundManager.GetAllByAdvanceRequisitionHeaderId(_requisitionHeader.Id);
            LoadRequisitionSourceOfFundListView(requisitionSourceOfFunds);
        }

        private void LoadRequisitionSourceOfFundListView(ICollection<RequisitionSourceOfFund> requisitionSourceOfFunds)
        {
            requisitionSourceOfFundListView.Items.Clear();
            if (requisitionSourceOfFunds != null && requisitionSourceOfFunds.Any())
            {
                int serial = 1;
                foreach (var requisitionSourceOfFund in requisitionSourceOfFunds)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = serial.ToString();
                    item.SubItems.Add(requisitionSourceOfFund.SourceOfFund.Name);
                    item.SubItems.Add(requisitionSourceOfFund.Percentage.ToString("N"));
                    item.Tag = requisitionSourceOfFund;
                    requisitionSourceOfFundListView.Items.Add(item);
                    serial++;
                }
            }
        }

        private void LoadRequisitionDetailInformationFromRequisition(AdvanceRequisitionHeader header)
        {
            ICollection<AdvanceRequisitionDetail> details;
            if (_recipient == null)
            {
                details = header.AdvanceRequisitionDetails.Where(c => c.RequisitionVoucherDetailId == null).ToList();
            }
            else
            {
                if (!_recipient.IsThirdParty)
                {
                    details = header.AdvanceRequisitionDetails.Where(c => c.RequisitionVoucherDetailId == null && string.IsNullOrEmpty(c.ReceipientOrPayeeName)).ToList();
                }
                else
                {
                    details = header.AdvanceRequisitionDetails.Where(c => c.RequisitionVoucherDetailId == null && c.ReceipientOrPayeeName != null && c.ReceipientOrPayeeName.Equals(_recipient.Name)).ToList();
                }
            }
            requisitionDetailsListView.Items.Clear();

            foreach (AdvanceRequisitionDetail detail in details)
            {
                ListViewItem item = new ListViewItem(string.Empty);
                item.SubItems.Add(detail.Purpose);
                item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                //item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                //    ? detail.ReceipientOrPayeeName :
                //    "N/A");
                item.SubItems.Add(detail.VatTaxTypeId != null
                    ? _vatTaxTypeManager.GetById(detail.VatTaxTypeId.Value).Name
                    : "N/A");
                item.Tag = detail;
                requisitionDetailsListView.Items.Add(item);
            }
            GenerateSerialNoInListView(requisitionDetailsListView);
        }

        private void LoadRequisitionDetailInformationFromRequisition(ICollection<AdvanceRequisitionDetail> details)
        {
            List<AdvanceRequisitionDetail> existingRequisitionDetail = GetRequisitionDetailFromListView().ToList();
            foreach (AdvanceRequisitionDetail detail in details)
            {
                ListViewItem item = new ListViewItem(string.Empty);
                item.SubItems.Add(detail.Purpose);
                item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                //item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                //    ? detail.ReceipientOrPayeeName :
                //    "N/A");
                item.SubItems.Add(detail.VatTaxTypeId != null
                    ? _vatTaxTypeManager.GetById(detail.VatTaxTypeId.Value).Name
                    : "N/A");
                item.Checked = true;
                item.Tag = detail;
                if (!existingRequisitionDetail.Select(c => c.Id).Contains(detail.Id))
                    requisitionDetailsListView.Items.Add(item);
            }
            GenerateSerialNoInListView(requisitionDetailsListView);
        }

        private ICollection<AdvanceRequisitionDetail> GetRequisitionDetailFromListView()
        {
            List<AdvanceRequisitionDetail> details = new List<AdvanceRequisitionDetail>();
            foreach (ListViewItem item in requisitionDetailsListView.Items)
            {
                var detail = item.Tag as AdvanceRequisitionDetail;
                if (detail != null)
                {
                    details.Add(detail);
                }
            }
            return details;
        }

        private void LoadBankComboBox()
        {
            List<Loan_Bank> banks = new List<Loan_Bank>
            {
                new Loan_Bank{Bank_ID = DefaultItem.Value, Bank_Name = DefaultItem.Text}
            };
            banks.AddRange(_bankManager.GetAll());
            bankComboBox.DataSource = null;
            bankComboBox.DisplayMember = "Bank_Name";
            bankComboBox.ValueMember = "Bank_ID";
            bankComboBox.DataSource = banks;
        }

        private void LoadBranchComboBox(long bankId)
        {
            List<Loan_Bank_Branch> branches = new List<Loan_Bank_Branch>
            {
                new Loan_Bank_Branch{Branch_ID = DefaultItem.Value, Branch_Name = DefaultItem.Text}
            };
            branches.AddRange(_branchManager.GetByBankId(bankId));
            branchComboBox.DataSource = null;
            branchComboBox.DisplayMember = "Branch_Name";
            branchComboBox.ValueMember = "Branch_ID";
            branchComboBox.DataSource = branches;
        }

        private void LoadAccountCodeComboBox(ICollection<Accounts_ChartOfAccounts> chartOfAccounts = null)
        {
            List<Accounts_ChartOfAccounts> chartOfAccountsList = new List<Accounts_ChartOfAccounts>
            {
                new Accounts_ChartOfAccounts{AccountCode = DefaultItem.Text}
            };
            accountCodeComboBox.DataSource = null;
            if (chartOfAccounts != null)
            {
                chartOfAccountsList.AddRange(chartOfAccounts);
            }
            else
            {
                chartOfAccountsList.AddRange(_chartOfAccountManager.GetAll());
            }

            accountCodeComboBox.ValueMember = "AccountCode";
            accountCodeComboBox.DisplayMember = "AccountCodeAndDescription";
            accountCodeComboBox.DataSource = chartOfAccountsList.ToList();
        }

        private void bankComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                long bankId = Convert.ToInt64(bankComboBox.SelectedValue);
                LoadBranchComboBox(bankId);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionDetailsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (requisitionDetailsListView.CheckedItems.Count > 0)
                {
                    LoadAccountComboByAccountCodes();
                    ListView.CheckedListViewItemCollection items = requisitionDetailsListView.CheckedItems;
                    if (voucherTypeComboBox.Text == DefaultItem.Text)
                    {
                        foreach (ListViewItem item in items)
                        {
                            item.Checked = false;
                        }
                        throw new UiException("Voucher type is not selected.");
                    }
                    amountTextBox.Text = GetTotalAmount(items).ToString("0.00");
                }
                else if (requisitionDetailsListView.CheckedItems.Count == 0 && _isDetailUpdateMode)
                {
                    ListView.CheckedListViewItemCollection items = requisitionDetailsListView.CheckedItems;
                    amountTextBox.Text = GetTotalAmount(items).ToString("0.00");
                }
                else if (requisitionDetailsListView.CheckedItems.Count == 0 && !_isRemoveMode)
                {
                    ClearVoucherDetailForm();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadAccountComboByAccountCodes()
        {
            var categoryId = _requisitionHeader.AdvanceCategoryId;
            if (requisitionDetailsListView.Items.Count > 0)
            {
                var accountsCodes = _accountConfigurationManager.GetChartOfAccountBy(categoryId, (long)AccountTypeEnum.AdvanceAccount).ToList();
                LoadAccountCodeComboBox(accountsCodes);
            }
            else
            {
                List<long> accountTypeIdList = null;
                var voucherTypeId = Convert.ToInt32(voucherTypeComboBox.SelectedValue.ToString());
                if (voucherTypeId > DefaultItem.Value)
                {
                    var accountsCodes =
                        _accountConfigurationManager.GetChartOfAccountByVoucherType(categoryId, voucherTypeId);
                    LoadAccountCodeComboBox(accountsCodes);
                }
            }
        }

        private void ClearVoucherHeaderForm()
        {
            recipientPayeeTextBox.Text = string.Empty;
            amountTextBox.Text = string.Empty;
            bankComboBox.Text = DefaultItem.Text;
            branchComboBox.Text = DefaultItem.Text;
            chequeTextBox.Text = string.Empty;
            voucherDescriptionTextBox.Text = string.Empty;
        }

        private void ClearVoucherDetailForm()
        {
            accountCodeComboBox.Text = DefaultItem.Text;
            debitAmountTextBox.Text = string.Empty;
            creditAmountTextBox.Text = string.Empty;
            descriptionTextBox.Text = string.Empty;
            isNetCheckBox.Checked = false;
            DisableVatTaxGroupBox();
            addButton.Text = @"Add";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();
                RequisitionVoucherDetail detail = new RequisitionVoucherDetail();
                var selectedAccount = accountCodeComboBox.SelectedItem as Accounts_ChartOfAccounts;
                if (selectedAccount == null)
                {
                    throw new UiException("Account is not tagged.");
                }
                detail.AccountCode = accountCodeComboBox.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(debitAmountTextBox.Text))
                {
                    detail.DrAmount = Convert.ToDecimal(debitAmountTextBox.Text);
                }
                if (!string.IsNullOrEmpty(creditAmountTextBox.Text))
                {
                    detail.CrAmount = Convert.ToDecimal(creditAmountTextBox.Text);
                }
                detail.Description = !string.IsNullOrEmpty(descriptionTextBox.Text)
                    ? descriptionTextBox.Text
                    : null;
                detail.RequisitionDetails = GetCheckedRequisitionDetails();
                detail.IsNet = isNetCheckBox.Checked;
                detail.VendorId = (int)vendorComboBox.SelectedValue == (int)DefaultItem.Value ? (int?)null : (int)vendorComboBox.SelectedValue;
                detail.VatTaxCategoryId = categoryComboBox.SelectedValue == null || (int)categoryComboBox.SelectedValue == (int)DefaultItem.Value ? (int?)null : (int)categoryComboBox.SelectedValue;
                detail.Percentage = percentageTextBox.Text == string.Empty ? null : percentageTextBox.Text;
                Accounts_ChartOfAccounts chartOfAccounts = _chartOfAccountManager.GetByCode(detail.AccountCode);
                RemoveTotalAmountFromGridView(voucherDetailDataGridView);
                if (!_isDetailUpdateMode)
                {
                    var item = GetNewGridViewItem(detail, chartOfAccounts.AccountCodeAndDescription);
                    voucherDetailDataGridView.Rows.Add(item);
                }
                else
                {
                    var updateableDetail = _updateableDetailItem.Tag as RequisitionVoucherDetail;
                    updateableDetail.AccountCode = detail.AccountCode;
                    updateableDetail.CrAmount = detail.CrAmount;
                    updateableDetail.DrAmount = detail.DrAmount;
                    updateableDetail.Description = detail.Description;
                    updateableDetail.IsNet = detail.IsNet;
                    updateableDetail.Percentage = detail.Percentage;
                    updateableDetail.VatTaxCategoryId = detail.VatTaxCategoryId;
                    updateableDetail.VendorId = detail.VendorId;
                    updateableDetail.RequisitionDetails = detail.RequisitionDetails;
                    SetChangedGridViewItemByDetail(updateableDetail, _updateableDetailItem, chartOfAccounts.AccountCodeAndDescription);
                    ResetChangeMode();
                }
                amountTextBox.Text = string.Empty;
                RemoveAddedItemsFromRequisitionDetailsListView();
                ClearVoucherDetailForm();
                SetDescriptionAccordingToVoucherDescription();
                SetTotalAmountInGridView(voucherDetailDataGridView);
                LoadAccountComboByAccountCodes();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetChangeMode()
        {
            addButton.Text = @"Add";
            _isDetailUpdateMode = false;
            ResetUpdateableRequisitionDetailItem();
            _updateableDetailItem = null;
        }

        //private ListViewItem SetChangedListViewItemByDetail(RequisitionVoucherDetail detail, ListViewItem item, string accountCodeAndDescription)
        //{
        //    item.Text = accountCodeAndDescription;
        //    item.SubItems[1].Text = detail.Description;
        //    item.SubItems[2].Text = detail.DrAmount != null && detail.DrAmount > 0 ? detail.DrAmount.Value.ToString("N") : string.Empty;
        //    item.SubItems[3].Text = detail.CrAmount != null && detail.CrAmount > 0 ? detail.CrAmount.Value.ToString("N") : string.Empty;
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(RequisitionVoucherDetail detail, DataGridViewRow row, string accountCodeAndDescription)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = accountCodeAndDescription;
            row.Cells[3].Value = detail.Description;
            row.Cells[4].Value = detail.DrAmount != null && detail.DrAmount > 0 ? detail.DrAmount.Value.ToString("N") : string.Empty;
            row.Cells[5].Value = detail.CrAmount != null && detail.CrAmount > 0 ? detail.CrAmount.Value.ToString("N") : string.Empty;

            row.Tag = detail;
            return row;
        }

        //private ListViewItem GetNewListViewItem(RequisitionVoucherDetail detail, string accountCodeAndDescription)
        //{
        //    ListViewItem item = new ListViewItem(accountCodeAndDescription);
        //    item.SubItems.Add(detail.Description);
        //    item.SubItems.Add(detail.DrAmount != null && detail.DrAmount > 0 ? detail.DrAmount.Value.ToString("N") : string.Empty);
        //    item.SubItems.Add(detail.CrAmount != null && detail.CrAmount > 0 ? detail.CrAmount.Value.ToString("N") : string.Empty);
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItem(RequisitionVoucherDetail detail, string accountCodeAndDescription)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(voucherDetailDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = accountCodeAndDescription;
            row.Cells[3].Value = detail.Description;
            row.Cells[4].Value = detail.DrAmount != null && detail.DrAmount > 0 ? detail.DrAmount.Value.ToString("N") : string.Empty;
            row.Cells[5].Value = detail.CrAmount != null && detail.CrAmount > 0 ? detail.CrAmount.Value.ToString("N") : string.Empty;

            row.Tag = detail;
            return row;
        }
        private ICollection<AdvanceRequisitionDetail> GetCheckedRequisitionDetails()
        {
            ListView.CheckedListViewItemCollection items = requisitionDetailsListView.CheckedItems;
            ICollection<AdvanceRequisitionDetail> detailList = new List<AdvanceRequisitionDetail>();
            foreach (ListViewItem item in items)
            {
                AdvanceRequisitionDetail detail = item.Tag as AdvanceRequisitionDetail;
                detailList.Add(detail);
            }
            return detailList;
        }

        private void RemoveAddedItemsFromRequisitionDetailsListView()
        {
            ListView.CheckedListViewItemCollection items = requisitionDetailsListView.CheckedItems;
            foreach (ListViewItem item in items)
            {
                requisitionDetailsListView.Items.Remove(item);
            }
        }

        private void UncheckAllItemsFromRequisitionDetailsListView()
        {
            ListView.CheckedListViewItemCollection items = requisitionDetailsListView.CheckedItems;
            foreach (ListViewItem item in items)
            {
                item.Checked = false;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                RequisitionVoucherHeader voucherHeader = new RequisitionVoucherHeader();
                voucherHeader.RecipientName = recipientPayeeTextBox.Text;
                voucherHeader.VoucherEntryDate = voucherDateDateTime.Value;
                if (Convert.ToInt64(voucherTypeComboBox.SelectedValue) != DefaultItem.Value)
                {
                    voucherHeader.VoucherTypeId = Convert.ToInt64(voucherTypeComboBox.SelectedValue);
                }
                if (Convert.ToInt64(bankComboBox.SelectedValue) != DefaultItem.Value)
                {
                    voucherHeader.BankId = Convert.ToInt64(bankComboBox.SelectedValue);
                }
                if (Convert.ToInt64(branchComboBox.SelectedValue) != DefaultItem.Value)
                {
                    voucherHeader.BranchId = Convert.ToInt64(branchComboBox.SelectedValue);
                }
                if (!string.IsNullOrEmpty(chequeTextBox.Text))
                {
                    voucherHeader.ChequeNo = chequeTextBox.Text;
                }
                voucherHeader.VoucherDescription = voucherDescriptionTextBox.Text;
                voucherHeader.RequisitionVoucherDetails = GetVoucherDetailsFromGridView();
                voucherHeader.CreatedBy = Session.LoginUserName;
                voucherHeader.CreatedOn = DateTime.Now;
                voucherHeader.RequisitionHeaderId = _requisitionHeader.Id;
                voucherHeader.Currency = "TK";
                voucherHeader.ConversionRate = 1;
                if (!_isUpdateMode)
                {
                    bool isInserted = _requisitionVoucherHeaderManager.Insert(voucherHeader);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Payment entry successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Payment entry failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    voucherHeader.Id = _updateableVoucherHeader.Id;
                    voucherHeader.VoucherStatusId = _updateableVoucherHeader.VoucherStatusId;
                    bool isInserted = _requisitionVoucherHeaderManager.Edit(voucherHeader);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Payment update successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Payment update failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateAdd()
        {
            var isNetValue = isNetCheckBox.Checked ? 1 : 0;
            string errorMessage = string.Empty;
            if (voucherTypeComboBox.Text == DefaultItem.Text)
            {
                errorMessage += "Voucher type is not selected. " + Environment.NewLine;
            }
            if (accountCodeComboBox.SelectedValue == DefaultItem.Text)
            {
                errorMessage += "Account code is not selected." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                errorMessage += "Description is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(creditAmountTextBox.Text) && string.IsNullOrEmpty(debitAmountTextBox.Text))
            {
                errorMessage += "Both credit amount and debit amount must not be empty." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(creditAmountTextBox.Text) && !string.IsNullOrEmpty(debitAmountTextBox.Text))
            {
                errorMessage += "Both credit amount and debit amount must not be provided." + Environment.NewLine;
            }

            if (!IsUpdateableItemIsAlreadySetNet())
            {
                if (GetIsNetCount() + isNetValue > 1)
                {
                    errorMessage += "You can atmost one is net in a voucher." + Environment.NewLine;
                }
            }
            var selectedAccount = accountCodeComboBox.SelectedItem as Accounts_ChartOfAccounts;
            if (selectedAccount != null && selectedAccount.IsVatTaxAccount)
            {
                if (vendorComboBox.Text == DefaultItem.Text)
                {
                    errorMessage += "Vendor is not selected." + Environment.NewLine;
                }
                if (categoryComboBox.Text == DefaultItem.Text)
                {
                    errorMessage += "Category is not selected." + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(percentageTextBox.Text))
                {
                    errorMessage += "Percentage is not provided." + Environment.NewLine;
                }
            }

            if (errorMessage != string.Empty)
            {
                throw new UiException(errorMessage);
            }
            return true;
        }

        private bool IsUpdateableItemIsAlreadySetNet()
        {
            if (_isDetailUpdateMode && isNetCheckBox.Checked)
            {
                var requisitionDetail = _updateableDetailItem.Tag as RequisitionVoucherDetail;
                if (requisitionDetail != null)
                    return requisitionDetail.IsNet;
            }
            return false;
        }

        private int GetIsNetCount()
        {
            var isNetCount = GetVoucherDetailsFromGridView().Where(c => c.IsNet == true).Select(c => c.IsNet).Count();
            return isNetCount;
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if (voucherTypeComboBox.Text == DefaultItem.Text)
            {
                errorMessage += "Voucher type is not selected. " + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(recipientPayeeTextBox.Text))
            {
                errorMessage += "Recipient/payee is not selected." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(voucherDescriptionTextBox.Text))
            {
                errorMessage += "Voucher description is not provided." + Environment.NewLine;
            }
            if (GetTotalDebitAmount() != GetTotalCreditAmount())
            {
                errorMessage += "Debit & credit amount is not equal." + Environment.NewLine;
            }
            if (requisitionDetailsListView.Items.Count > 0)
            {
                errorMessage += "Some particulars are missing. You must add voucher information fot all particulars." + Environment.NewLine;
            }
            if (GetIsNetCount() > 1)
            {
                errorMessage += "You can atmost one is net in a voucher" + Environment.NewLine;
            }
            if (GetIsNetCount() == 0)
            {
                errorMessage += "You must check one is net in a voucher" + Environment.NewLine;
            }
            if (errorMessage != string.Empty)
            {
                throw new UiException(errorMessage);
            }
            return true;
        }

        private double GetTotalAmount(ListView.CheckedListViewItemCollection items)
        {
            double totalAmount = 0;
            foreach (ListViewItem item in items)
            {
                double amount = Convert.ToDouble(item.SubItems[2].Text);
                totalAmount += amount;
            }
            return totalAmount;
        }

        private string GetReceipientPayeeName(ListView.CheckedListViewItemCollection items)
        {
            string allReceipientPayee = string.Empty;
            foreach (ListViewItem item in items)
            {
                AdvanceRequisitionDetail detail = item.Tag as AdvanceRequisitionDetail;
                if (detail == null)
                {
                    throw new UiException("Requisition detail item is not tagged.");
                }
                string name = detail.IsThirdPartyReceipient ? detail.ReceipientOrPayeeName : _requester.FullName;
                allReceipientPayee += name != "N/A" ? name + "," : _requester.FullName + ",";
            }
            List<string> receipientPayeeNameList = allReceipientPayee.Split(',').ToList();
            receipientPayeeNameList = receipientPayeeNameList.Distinct().ToList();
            if (receipientPayeeNameList.Count > 2)
            {
                throw new UiException("You cannot add more than one recipient in the same voucher.");
            }
            return receipientPayeeNameList.First().TrimEnd(',');
        }

        private void CalculateAutoBalance()
        {
            if (amountTextBox.Text == string.Empty)
            {
                var totalDebitAmount = GetTotalDebitAmount();
                var totalCreditAmount = GetTotalCreditAmount();

                var difference = Math.Abs(totalCreditAmount - totalDebitAmount);
                if (totalCreditAmount > totalDebitAmount)
                {
                    debitAmountTextBox.Text = difference.ToString();
                }
                if (totalDebitAmount > totalCreditAmount)
                {
                    creditAmountTextBox.Text = difference.ToString();
                }
            }
        }

        private decimal GetTotalDebitAmount()
        {
            decimal debitAmount = 0;
            //foreach (ListViewItem item in voucherDetailListView.Items)
            //{
            //    RequisitionVoucherDetail detail = item.Tag as RequisitionVoucherDetail;
            //    if (detail != null && detail.DrAmount != null)
            //    {
            //        debitAmount += detail.DrAmount.Value;
            //    }
            //}
            foreach (DataGridViewRow row in voucherDetailDataGridView.Rows)
            {
                RequisitionVoucherDetail detail = row.Tag as RequisitionVoucherDetail;
                if (detail != null && detail.DrAmount != null)
                {
                    debitAmount += detail.DrAmount.Value;
                }
            }
            return debitAmount;
        }

        private decimal GetTotalCreditAmount()
        {
            decimal creditAmount = 0;
            //foreach (ListViewItem item in voucherDetailListView.Items)
            //{
            //    RequisitionVoucherDetail detail = item.Tag as RequisitionVoucherDetail;
            //    if (detail != null && detail.CrAmount != null)
            //    {
            //        creditAmount += detail.CrAmount.Value;
            //    }
            //}

            foreach (DataGridViewRow  row in voucherDetailDataGridView.Rows)
            {
                RequisitionVoucherDetail detail = row.Tag as RequisitionVoucherDetail;
                if (detail != null && detail.CrAmount != null)
                {
                    creditAmount += detail.CrAmount.Value;
                }
            }
            return creditAmount;
        }

        //private void SetTotalAmountInListView(ListView listView)
        //{
        //    ListViewItem item = new ListViewItem();
        //    item.Text = string.Empty;
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(GetTotalDebitAmount().ToString("N"));
        //    item.SubItems.Add(GetTotalCreditAmount().ToString("N"));
        //    item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
        //    listView.Items.Add(item);
        //}

        private void SetTotalAmountInGridView(DataGridView gridView)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gridView);
            row.Cells[0] = new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();
            row.Cells[3].Value = @"Total";
            row.Cells[4].Value = GetTotalDebitAmount().ToString("N");
            row.Cells[5].Value = GetTotalCreditAmount().ToString("N");

            row.Cells[3].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[4].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[5].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);

            row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridView.Rows.Add(row);
        }

        //private void RemoveTotalAmountFromListView(ListView listView)
        //{
        //    if (listView.Items.Count > 1)
        //    {
        //        int detailItem =
        //                listView.Items.Count - 1;
        //        listView.Items.RemoveAt(detailItem);
        //    }
        //}

        private void RemoveTotalAmountFromGridView(DataGridView gridView)
        {
            if (gridView.Rows.Count > 1)
            {
                int detailItem =
                        gridView.Rows.Count - 1;
                gridView.Rows.RemoveAt(detailItem);
            }
        }

        //private List<RequisitionVoucherDetail> GetVoucherDetailsFromListView()
        //{
        //    List<RequisitionVoucherDetail> voucherDetails = new List<RequisitionVoucherDetail>();
        //    foreach (ListViewItem item in voucherDetailListView.Items)
        //    {
        //        RequisitionVoucherDetail detail = item.Tag as RequisitionVoucherDetail;
        //        if (detail != null)
        //        {
        //            voucherDetails.Add(detail);
        //        }
        //    }
        //    return voucherDetails;
        //}

        private List<RequisitionVoucherDetail> GetVoucherDetailsFromGridView()
        {
            List<RequisitionVoucherDetail> voucherDetails = new List<RequisitionVoucherDetail>();
            foreach (DataGridViewRow row in voucherDetailDataGridView.Rows)
            {
                RequisitionVoucherDetail detail = row.Tag as RequisitionVoucherDetail;
                if (detail != null)
                {
                    voucherDetails.Add(detail);
                }
            }
            return voucherDetails;
        }

        private void removeVoucherDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (voucherDetailListView.SelectedItems.Count > 0)
                //{
                //    _isRemoveMode = true;
                //    int detailItem = voucherDetailListView.SelectedItems[0].Index;
                //    RequisitionVoucherDetail voucherDetail =
                //        voucherDetailListView.SelectedItems[0].Tag as RequisitionVoucherDetail;
                //    if (voucherDetail == null)
                //    {
                //        throw new UiException("Voucher detail cannot be deleted.");
                //    }
                //    RemoveTotalAmountFromListView(voucherDetailListView);
                //    voucherDetailListView.Items.RemoveAt(detailItem);
                //    LoadRequisitionDetailInformationFromRequisition(voucherDetail.RequisitionDetails);
                //    _isRemoveMode = false;
                //    if (voucherDetailListView.Items.Count >= 1)
                //    {
                //        SetTotalAmountInListView(voucherDetailListView);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"No item is selected to remove.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void creditAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void debitAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void voucherDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetDescriptionAccordingToVoucherDescription();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDescriptionAccordingToVoucherDescription()
        {
            string description = voucherDescriptionTextBox.Text;
            descriptionTextBox.Text = description;
        }

        private void editVoucherDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (voucherDetailListView.SelectedItems.Count > 0)
                //{
                //    ResetUpdateableRequisitionDetailItem();
                //    _isDetailUpdateMode = true;
                //    _updateableDetailItem = voucherDetailListView.SelectedItems[0];
                //    RequisitionVoucherDetail voucherDetail =
                //        voucherDetailListView.SelectedItems[0].Tag as RequisitionVoucherDetail;
                //    if (voucherDetail == null)
                //    {
                //        throw new UiException("Voucher detail item is not tagged.");
                //    }
                //    LoadAccountComboByAccountCodes();
                //    LoadRequisitionDetailInformationFromRequisition(voucherDetail.RequisitionDetails);
                //    LoadVoucherDetailInformation(voucherDetail);
                //    SetVoucherDetailToEditMode();
                //}
                //else
                //{
                //    MessageBox.Show(@"No item is selected to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVoucherDetailInformation(RequisitionVoucherDetail voucherDetail)
        {
            accountCodeComboBox.SelectedValue = voucherDetail.AccountCode;
            debitAmountTextBox.Text = voucherDetail.DrAmount == null ? string.Empty : voucherDetail.DrAmount.ToString();
            creditAmountTextBox.Text = voucherDetail.CrAmount == null ? string.Empty : voucherDetail.CrAmount.ToString();
            descriptionTextBox.Text = voucherDetail.Description;
            isNetCheckBox.Checked = voucherDetail.IsNet;
            var account = _chartOfAccountManager.GetByCode(voucherDetail.AccountCode);

            if (account.IsVatTaxAccount)
            {
                vatTaxGroupBox.Visible = true;
                vendorComboBox.SelectedValue = voucherDetail.VendorId;
                LoadVatTaxCategory(account.VatTaxTypeId);
                categoryComboBox.SelectedValue = voucherDetail.VatTaxCategoryId;
                percentageTextBox.Text = voucherDetail.Percentage;
            }
            else
            {
                DisableVatTaxGroupBox();

            }
        }

        //private void LoadVoucherDetailVoucherDetailListView(List<RequisitionVoucherDetail> voucherDetails)
        //{
        //    if (voucherDetails != null)
        //    {
        //        voucherDetailListView.Items.Clear();

        //        foreach (var requisitionVoucherDetail in voucherDetails)
        //        {
        //            Accounts_ChartOfAccounts chartOfAccounts = _chartOfAccountManager.GetByCode(requisitionVoucherDetail.AccountCode);
        //            string accountCodeAndDescription = chartOfAccounts.AccountCodeAndDescription;
        //            ListViewItem item = new ListViewItem(accountCodeAndDescription);
        //            item.SubItems.Add(requisitionVoucherDetail.Description);
        //            item.SubItems.Add(requisitionVoucherDetail.DrAmount != null
        //                ? requisitionVoucherDetail.DrAmount.Value.ToString("N")
        //                : string.Empty);
        //            item.SubItems.Add(requisitionVoucherDetail.CrAmount != null
        //                ? requisitionVoucherDetail.CrAmount.Value.ToString("N")
        //                : string.Empty);
        //            item.Tag = requisitionVoucherDetail;
        //            voucherDetailListView.Items.Add(item);
        //        }
        //    }
        //}

        private void LoadVoucherDetailVoucherDetailGridView(List<RequisitionVoucherDetail> voucherDetails)
        {
            if (voucherDetails != null)
            {
                voucherDetailDataGridView.Rows.Clear();

                foreach (var requisitionVoucherDetail in voucherDetails)
                {
                    Accounts_ChartOfAccounts chartOfAccounts = _chartOfAccountManager.GetByCode(requisitionVoucherDetail.AccountCode);
                    string accountCodeAndDescription = chartOfAccounts.AccountCodeAndDescription;

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(voucherDetailDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = accountCodeAndDescription;
                    row.Cells[3].Value = requisitionVoucherDetail.Description;
                    row.Cells[4].Value = requisitionVoucherDetail.DrAmount != null
                        ? requisitionVoucherDetail.DrAmount.Value.ToString("N")
                        : string.Empty;
                    row.Cells[5].Value = requisitionVoucherDetail.CrAmount != null
                        ? requisitionVoucherDetail.CrAmount.Value.ToString("N")
                        : string.Empty;
                    row.Tag = requisitionVoucherDetail;
                    voucherDetailDataGridView.Rows.Add(row);
                }
            }
        }

        private void SetVoucherDetailToEditMode()
        {
            addButton.Text = @"Change";
            _isDetailUpdateMode = true;
        }

        private void GenerateSerialNoInListView(ListView listView)
        {
            int serial = 1;
            foreach (ListViewItem item in listView.Items)
            {
                item.Text = serial.ToString();
                serial++;
            }
        }

        private void ClearCreditAmountTextBox()
        {
            creditAmountTextBox.Text = string.Empty;
        }

        private void ClearDebitAmountTextBox()
        {
            debitAmountTextBox.Text = string.Empty;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetUpdateableRequisitionDetailItem();
                LoadAccountComboByAccountCodes();
                ClearVoucherDetailForm();
                ResetChangeMode();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accountCodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isMultipleSelect = false;
                Accounts_SelectAccountsAssignedToUserUI allAccountSelectUi = new Accounts_SelectAccountsAssignedToUserUI(isMultipleSelect);
                allAccountSelectUi.ShowDialog();
                var selectedAccounts = allAccountSelectUi.SelectedAccounts;
                if (selectedAccounts.Count > 0)
                {
                    var selectedItems = new List<Accounts_ChartOfAccounts>()
                {
                    new Accounts_ChartOfAccounts{AccountCode =selectedAccounts[0].AccountCode}
                };
                    var accountList = GetAllAccountComboBoxItems();
                    if (accountList != null && accountList.Any())
                    {
                        accountList = accountList.Where(c => c.AccountCode != DefaultItem.Text).ToList();
                        if (accountList.Any(c => c.AccountCode == selectedAccounts[0].AccountCode))
                        {
                            LoadAccountCodeComboBox(accountList);
                        }
                        else
                        {
                            accountList.AddRange(selectedItems);
                            LoadAccountCodeComboBox(accountList);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Accounts_ChartOfAccounts> GetAllAccountComboBoxItems()
        {
            List<Accounts_ChartOfAccounts> accounts = new List<Accounts_ChartOfAccounts>();
            if (accountCodeComboBox.Items.Count > 0)
            {
                foreach (var item in accountCodeComboBox.Items)
                {
                    var account = item as Accounts_ChartOfAccounts;
                    if (account != null)
                    {
                        accounts.Add(account);
                    }
                }
            }
            return accounts;
        }

        private void ResetUpdateableRequisitionDetailItem()
        {
            if (_updateableDetailItem != null)
            {
                var voucherDetail = _updateableDetailItem.Tag as RequisitionVoucherDetail;

                RemoveRequisitionDetailItemsFromRequisitionDetailListView(voucherDetail.RequisitionDetails);
            }
        }

        private void RemoveRequisitionDetailItemsFromRequisitionDetailListView(ICollection<AdvanceRequisitionDetail> requisitionDetails)
        {
            if (requisitionDetails != null && requisitionDetails.Any())
            {

                var items = requisitionDetailsListView.Items;
                foreach (ListViewItem listViewItem in items)
                {
                    var requisitionDetail = listViewItem.Tag as AdvanceRequisitionDetail;

                    if (requisitionDetails.Any(c => requisitionDetail != null && c.Id == requisitionDetail.Id))
                    {
                        requisitionDetailsListView.Items.Remove(listViewItem);
                    }
                }
            }
        }

        private void accountCodeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var selectedAccount = accountCodeComboBox.SelectedItem as Accounts_ChartOfAccounts;
                string accountCode = selectedAccount.AccountCode;
                if (accountCode != DefaultItem.Text)
                {
                    Accounts_ChartOfAccounts chartOfAccounts = _chartOfAccountManager.GetByCode(accountCode);
                    int normalBalance = Convert.ToInt32(chartOfAccounts.NormalBalance);
                    if (normalBalance == (int)DebitCreditTypeEnum.Debit)
                    {
                        if (_isDetailUpdateMode && !amountTextBox.Text.Equals(string.Empty))
                        {
                            // change amount
                            debitAmountTextBox.Text = amountTextBox.Text;
                            creditAmountTextBox.Text = string.Empty;
                        }
                        else if (!_isDetailUpdateMode)
                        {
                            debitAmountTextBox.Text = amountTextBox.Text;
                            creditAmountTextBox.Text = string.Empty;
                            CalculateAutoBalance();
                        }
                        LoadVatTax(selectedAccount);
                    }
                    else if (normalBalance == (int)DebitCreditTypeEnum.Credit)
                    {
                        if (_isDetailUpdateMode && !amountTextBox.Text.Equals(string.Empty))
                        {
                            // change amount
                            debitAmountTextBox.Text = string.Empty;
                            creditAmountTextBox.Text = amountTextBox.Text;
                        }
                        else if (!_isDetailUpdateMode)
                        {
                            debitAmountTextBox.Text = string.Empty;
                            creditAmountTextBox.Text = amountTextBox.Text;
                            CalculateAutoBalance();
                        }
                        LoadVatTax(selectedAccount);
                    }
                    else
                    {
                        throw new UiException("Normal balance of this chart of account is neither debit nor credit type.");
                    }
                }
                else
                {
                    DisableVatTaxGroupBox();
                    debitAmountTextBox.Text = string.Empty;
                    creditAmountTextBox.Text = string.Empty;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadVatTax(Accounts_ChartOfAccounts account)
        {
            if (account.IsVatTaxAccount)
            {
                vatTaxGroupBox.Visible = true;
                LoadVatTaxCategory(account.VatTaxTypeId);
            }
            else
            {
                DisableVatTaxGroupBox();
            }
        }

        private void DisableVatTaxGroupBox()
        {
            percentageTextBox.Text = string.Empty;
            vendorComboBox.Text = DefaultItem.Text;
            categoryComboBox.Text = DefaultItem.Text;
            vatTaxGroupBox.Visible = false;
        }

        private void LoadVatTaxCategory(int? vatTaxTypeId)
        {
            if (vatTaxTypeId == null)
            {
                throw new UiException("Vat Tax category not found");
            }
            List<Accounts_NC_VatTaxCategory> categories = _accountsNcVatTaxCategoryManager.GetVatTaxCategoryByType((int)vatTaxTypeId).ToList();
            categories.Insert(0, new Accounts_NC_VatTaxCategory { Id = (int)DefaultItem.Value, Name = DefaultItem.Text });
            categoryComboBox.DataSource = null;
            categoryComboBox.DisplayMember = "Name";
            categoryComboBox.ValueMember = "Id";
            categoryComboBox.DataSource = categories;

        }

        private void categoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var category = categoryComboBox.SelectedItem as Accounts_NC_VatTaxCategory;
                if (category == null)
                {
                    throw new UiException("Category is not tagged.");
                }
                percentageTextBox.Text = category.VatTaxPercentageInText;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void voucherDetailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (voucherDetailDataGridView.SelectedRows.Count > 0)
                        {
                            ResetUpdateableRequisitionDetailItem();
                            _isDetailUpdateMode = true;
                            _updateableDetailItem = voucherDetailDataGridView.SelectedRows[0];
                            RequisitionVoucherDetail voucherDetail =
                                voucherDetailDataGridView.SelectedRows[0].Tag as RequisitionVoucherDetail;
                            if (voucherDetail == null)
                            {
                                throw new UiException("Voucher detail item is not tagged.");
                            }
                            LoadAccountComboByAccountCodes();
                            LoadRequisitionDetailInformationFromRequisition(voucherDetail.RequisitionDetails);
                            LoadVoucherDetailInformation(voucherDetail);
                            SetVoucherDetailToEditMode();
                        }
                        else
                        {
                            MessageBox.Show(@"No item is selected to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    if (e.ColumnIndex == 1)
                    {
                        if (voucherDetailDataGridView.SelectedRows.Count > 0)
                        {
                            _isRemoveMode = true;
                            int detailItem = voucherDetailDataGridView.SelectedRows[0].Index;
                            RequisitionVoucherDetail voucherDetail =
                                voucherDetailDataGridView.SelectedRows[0].Tag as RequisitionVoucherDetail;
                            if (voucherDetail == null)
                            {
                                throw new UiException("Voucher detail cannot be deleted.");
                            }
                            RemoveTotalAmountFromGridView(voucherDetailDataGridView);
                            voucherDetailDataGridView.Rows.RemoveAt(detailItem);
                            LoadRequisitionDetailInformationFromRequisition(voucherDetail.RequisitionDetails);
                            _isRemoveMode = false;
                            if (voucherDetailDataGridView.Rows.Count >= 1)
                            {
                                SetTotalAmountInGridView(voucherDetailDataGridView);
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"No item is selected to remove.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
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
