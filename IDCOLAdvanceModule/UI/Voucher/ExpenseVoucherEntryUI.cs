using Accounts.NerdCastle.NerdCastleUI.ModalUI;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.ViewModels;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Accounts.NerdCastle.NCBLL;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using AccountConfigurationManager = IDCOLAdvanceModule.BLL.AdvanceManager.AccountConfigurationManager;
using Accounts_NC_VatTaxCategory = Accounts.NerdCastle.NCDAO.Accounts_NC_VatTaxCategory;
using DefaultItem = IDCOLAdvanceModule.Utility.DefaultItem;

namespace IDCOLAdvanceModule.UI.Voucher
{
    public partial class ExpenseVoucherEntryUI : Form
    {
        private readonly IBankManager _bankManager;
        private readonly IBranchManager _branchManager;
        private readonly IChartOfAccountManager _chartOfAccountManager;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IExpenseVoucherHeaderManager _expenseVoucherHeaderManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly UserTable _requester;
        private readonly AdvanceExpenseHeader _expenseHeader;
        private bool _isUpdateMode;
        private bool _isDetailUpdateMode;
        private bool _isRemoveMode;
        private DataGridViewRow _updateableDetailItem;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IExpenseSourceOfFundManager _expenseSourceOfFundManager;
        private readonly IAccountConfigurationManager _accountConfigurationManager;
        private readonly IVoucherTypeManager _voucherTypeManager;
        private readonly ExpenseVoucherHeader _updateableVoucherHeader;
        private readonly RecipientVM _recipient;
        private readonly AdvancedFormMode _formMode;
        private readonly Accounts_NC_VatTaxCategoryManager _accountsNcVatTaxCategoryManager;
        private readonly Accounts_NC_VendorInfoManager _accountsNcVendorInfoManager;

        private ExpenseVoucherEntryUI()
        {
            InitializeComponent();
            _bankManager = new BankManager();
            _branchManager = new BranchManager();
            _chartOfAccountManager = new ChartOfAccountManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
            _employeeManager = new EmployeeManager();
            _expenseVoucherHeaderManager = new ExpenseVoucherHeaderManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _expenseSourceOfFundManager = new ExpenseSourceOfFundManager();
            _accountConfigurationManager = new AccountConfigurationManager();
            _voucherTypeManager = new VoucherTypeManager();
            _accountsNcVatTaxCategoryManager = new Accounts_NC_VatTaxCategoryManager();
            _accountsNcVendorInfoManager = new Accounts_NC_VendorInfoManager();

            _isDetailUpdateMode = false;
            _isUpdateMode = false;
            _isRemoveMode = false;
            _formMode = AdvancedFormMode.Create;
        }

        public ExpenseVoucherEntryUI(AdvanceExpenseHeader expenseHeader)
            : this()
        {
            _expenseHeader = expenseHeader;
            LoadExpenseDetailInformation(expenseHeader);
            _requester = _employeeManager.GetByUserName(expenseHeader.RequesterUserName);
        }

        public ExpenseVoucherEntryUI(AdvanceExpenseHeader expenseHeader, RecipientVM recipient)
            : this()
        {
            _expenseHeader = expenseHeader;
            _recipient = recipient;
            LoadRecipientPayeNameTextBox();
            LoadVoucherDescriptionFromHeaderPurpose();
            LoadExpenseDetailInformation(expenseHeader);
            _requester = _employeeManager.GetByUserName(expenseHeader.RequesterUserName);
        }

        public ExpenseVoucherEntryUI(ExpenseVoucherHeader expenseVoucherHeader, AdvancedFormMode formMode)
            : this()
        {
            this._formMode = formMode;
            _updateableVoucherHeader = expenseVoucherHeader;
            _recipient = new RecipientVM { Name = expenseVoucherHeader.RecipientName };
            LoadRecipientPayeNameTextBox();
            _isUpdateMode = formMode == AdvancedFormMode.Update;
            _expenseHeader = _advanceExpenseHeaderManager.GetById(expenseVoucherHeader.ExpenseHeader.Id);
            _requester = _employeeManager.GetByUserName(expenseVoucherHeader.ExpenseHeader.RequesterUserName);
            //AdvanceRequisitionHeader requisitionHeader =
            //    _advanceRequisitionHeaderManager.GetById(expenseVoucherHeader.RequisitionHeaderId);
            //LoadRequisitionDetailInformationFromRequisition(requisitionHeader);

            LoadVoucherDetailVoucherDetailGridView(expenseVoucherHeader.ExpenseVoucherDetails.ToList());
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

        private void ExpensePaymentEntryUI_Load(object sender, EventArgs e)
        {
            try
            {
                voucherDateDateTime.Value = DateTime.Now;
                LoadExpenseBasicInformation();
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
        private void LoadVoucherHeader(ExpenseVoucherHeader expenseVoucherHeader)
        {
            if (expenseVoucherHeader.BankId != null)
            {
                var bank = _bankManager.GetById((long)expenseVoucherHeader.BankId);
                if (bank != null)
                {
                    bankComboBox.Text = bank.Bank_Name;
                }
            }
            if (expenseVoucherHeader.BranchId != null)
            {
                var branch = _branchManager.GetById((long)expenseVoucherHeader.BranchId);
                if (branch != null)
                {
                    branchComboBox.Text = branch.Branch_Name;
                }
            }
            if (expenseVoucherHeader.VoucherTypeId != null)
            {
                var voucherType = _voucherTypeManager.Get(c => c.VouTypeId == expenseVoucherHeader.VoucherTypeId).FirstOrDefault();

                if (voucherType != null) voucherTypeComboBox.Text = voucherType.VouType;
            }
            recipientPayeeTextBox.Text = _recipient.Name;
            chequeTextBox.Text = expenseVoucherHeader.ChequeNo;
            voucherDescriptionTextBox.Text = expenseVoucherHeader.VoucherDescription;
        }

        //private void LoadVoucherDetailVoucherDetailListView(List<ExpenseVoucherDetail> voucherDetails)
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

        private void LoadVoucherDetailVoucherDetailGridView(List<ExpenseVoucherDetail> voucherDetails)
        {
            if (voucherDetails != null)
            {
                voucherDetailDataGridView.Rows.Clear();

                foreach (var requisitionVoucherDetail in voucherDetails)
                {
                    Accounts_ChartOfAccounts chartOfAccounts = _chartOfAccountManager.GetByCode(requisitionVoucherDetail.AccountCode);
                    string accountCodeAndDescription = chartOfAccounts.AccountCodeAndDescription;
                    DataGridViewRow row=new DataGridViewRow();
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

        private void LoadRecipientPayeNameTextBox()
        {
            recipientPayeeTextBox.Text = _recipient.Name;
        }

        private void LoadVoucherDescriptionFromHeaderPurpose()
        {
            voucherDescriptionTextBox.Text = _expenseHeader.Purpose;
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
            DataGridViewRow row=new DataGridViewRow();
            row.CreateCells(gridView);
            row.Cells[0]=new DataGridViewTextBoxCell();
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

        private void LoadVoucherTypeComboBox()
        {
            var voucherTypes = _voucherTypeManager.GetOperationalTypes().ToList();
            if (voucherTypes == null)
            {
                voucherTypes = new List<Accounts_VoucherTypes>();
            }

            voucherTypes.Insert(0, new Accounts_VoucherTypes() { VouTypeId = (short)DefaultItem.Value, VouType = DefaultItem.Text });

            voucherTypeComboBox.DataSource = null;
            voucherTypeComboBox.DisplayMember = "VouType";
            voucherTypeComboBox.ValueMember = "VouTypeId";
            voucherTypeComboBox.DataSource = voucherTypes;

            voucherTypeComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            if (_formMode == AdvancedFormMode.Update)
            {
                voucherTypeComboBox.SelectedValue = (short)_updateableVoucherHeader.VoucherTypeId;
            }
        }

        private void LoadExpenseBasicInformation()
        {
            if (_expenseHeader == null)
            {
                throw new UiException("Adjustment/Reimbursement not found.");
            }
            expenseNoTextBox.Text = _expenseHeader.ExpenseNo;
            AdvanceCategory category = _advanceRequisitionCategoryManager.GetById(_expenseHeader.AdvanceCategoryId);
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
            fromDateTextBox.Text = _expenseHeader.FromDate.ToString("dd-MMM-yyyy");
            toDateTextBox.Text = _expenseHeader.ToDate.ToString("dd-MMM-yyyy");
            advanceAmountTextBox.Text = _expenseHeader.GetTotalAdvanceAmount().ToString("N");
            expenseAmountTextBox.Text = _expenseHeader.GetTotalExpenseAmount().ToString("N");
            reimbursementRefunsAmountTextBox.Text = _expenseHeader.GetFormattedReimbursementAmount();
            purposeTextBox.Text = _expenseHeader.Purpose;
            if (_expenseHeader.ExpenseIssueDate == null)
            {
                throw new UiException("Advance is not issued yet.");
            }
            advanceIssueDateTextBox.Text = _expenseHeader.ExpenseIssueDate.Value.ToString("dd-MMM-yyyy");
            ICollection<ExpenseSourceOfFund> expenseSourceOfFunds = _expenseSourceOfFundManager.GetAllByExpenseHeaderId(_expenseHeader.Id);
            LoadExpenseSourceOfFundListView(expenseSourceOfFunds);
        }

        private void LoadExpenseSourceOfFundListView(ICollection<ExpenseSourceOfFund> expenseSourceOfFunds)
        {
            expenseSourceOfFundListView.Items.Clear();
            if (expenseSourceOfFunds != null && expenseSourceOfFunds.Any())
            {
                int serial = 1;
                foreach (var requisitionSourceOfFund in expenseSourceOfFunds)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = serial.ToString();
                    item.SubItems.Add(requisitionSourceOfFund.SourceOfFund.Name);
                    item.SubItems.Add(requisitionSourceOfFund.Percentage.ToString("N"));
                    item.Tag = requisitionSourceOfFund;
                    expenseSourceOfFundListView.Items.Add(item);
                    serial++;
                }
            }
        }

        private void LoadExpenseDetailInformation(AdvanceExpenseHeader header)
        {
            ICollection<AdvanceExpenseDetail> details;
            if (_recipient == null)
            {
                details = header.AdvanceExpenseDetails.Where(c => c.ExpenseVoucherDetailId == null).ToList();
            }
            else
            {
                if (!_recipient.IsThirdParty)
                {
                    details = header.AdvanceExpenseDetails.Where(c => c.ExpenseVoucherDetailId == null && string.IsNullOrEmpty(c.ReceipientOrPayeeName)).ToList();
                }
                else
                {
                    details = header.AdvanceExpenseDetails.Where(c => c.ExpenseVoucherDetailId == null && c.ReceipientOrPayeeName != null && c.ReceipientOrPayeeName.Equals(_recipient.Name)).ToList();
                }
            }
            expenseDetailsListView.Items.Clear();

            //ICollection<AdvanceExpenseDetail> details = header.AdvanceExpenseDetails.Where(c => c.ExpenseVoucherDetailId == null).ToList();
            //expenseDetailsListView.Items.Clear();

            foreach (AdvanceExpenseDetail detail in details)
            {
                ListViewItem item = new ListViewItem(string.Empty);
                item.SubItems.Add(detail.Purpose);
                item.SubItems.Add(detail.GetTotalActualExpenseAmount().ToString("N"));
                //item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                //    ? detail.ReceipientOrPayeeName :
                //    "N/A");
                item.SubItems.Add(detail.VatTaxTypeId != null
                    ? _vatTaxTypeManager.GetById(detail.VatTaxTypeId.Value).Name
                    : "N/A");
                item.Tag = detail;
                expenseDetailsListView.Items.Add(item);
            }
            GenerateSerialNoInListView(expenseDetailsListView);
        }

        private void LoadExpenseDetailInformation(ICollection<AdvanceExpenseDetail> details)
        {
            foreach (AdvanceExpenseDetail detail in details)
            {
                ListViewItem item = new ListViewItem(string.Empty);
                item.SubItems.Add(detail.Purpose);
                item.SubItems.Add(detail.GetExpenseAmountInBdt().ToString("N"));
                //item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                //    ? detail.ReceipientOrPayeeName :
                //    "N/A");
                item.SubItems.Add(detail.VatTaxTypeId != null
                    ? _vatTaxTypeManager.GetById(detail.VatTaxTypeId.Value).Name
                    : "N/A");
                item.Checked = true;
                item.Tag = detail;
                expenseDetailsListView.Items.Add(item);
            }
            GenerateSerialNoInListView(expenseDetailsListView);
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

        private void bankComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                long bankId = Convert.ToInt64(bankComboBox.SelectedValue);
                LoadBranchComboBox(bankId);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void RemoveAddedItemsFromExpenseDetailsListView()
        {
            ListView.CheckedListViewItemCollection items = expenseDetailsListView.CheckedItems;
            foreach (ListViewItem item in items)
            {
                expenseDetailsListView.Items.Remove(item);
            }
        }

        private void ResetUpdateableExpenseDetailItem()
        {
            if (_updateableDetailItem != null)
            {
                var voucherDetail = _updateableDetailItem.Tag as ExpenseVoucherDetail;

                RemoveExpenseDetailItemsFromExpenseDetailListView(voucherDetail.ExpenseDetails);
            }
        }

        private void RemoveExpenseDetailItemsFromExpenseDetailListView(ICollection<AdvanceExpenseDetail> expenseDetails)
        {
            if (expenseDetails != null && expenseDetails.Any())
            {
                var items = expenseDetailsListView.Items;
                foreach (ListViewItem listViewItem in items)
                {
                    var expenseDetail = listViewItem.Tag as AdvanceExpenseDetail;

                    if (expenseDetails.Any(c => expenseDetail != null && c.Id == expenseDetail.Id))
                    {
                        expenseDetailsListView.Items.Remove(listViewItem);
                    }
                }
            }
        }

        private void expenseDetailsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (expenseDetailsListView.CheckedItems.Count > 0)
                {
                    LoadAccountComboByAccountCodes();
                    ListView.CheckedListViewItemCollection items = expenseDetailsListView.CheckedItems;
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
                else if (expenseDetailsListView.CheckedItems.Count == 0 && _isUpdateMode)
                {
                    ListView.CheckedListViewItemCollection items = expenseDetailsListView.CheckedItems;
                    //recipientPayeeTextBox.Text = GetReceipientPayeeName(items);
                    amountTextBox.Text = GetTotalAmount(items).ToString("0.00");
                }
                else if (expenseDetailsListView.CheckedItems.Count == 0 && !_isRemoveMode)
                {
                    amountTextBox.Text = string.Empty;
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
            var categoryId = _expenseHeader.AdvanceCategoryId;
            if (expenseDetailsListView.Items.Count > 0)
            {
                LoadExpenseAccountCodesBy(categoryId);
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

        private void LoadExpenseAccountCodesBy(long categoryId)
        {
            var accountList = _accountConfigurationManager.GetChartOfAccountBy(categoryId,
                (long)AccountTypeEnum.ExpenseAccount);

            LoadAccountCodeComboBox(accountList);
        }

        private string GetReceipientPayeeName(ListView.CheckedListViewItemCollection items)
        {
            string allReceipientPayee = string.Empty;
            foreach (ListViewItem item in items)
            {
                AdvanceExpenseDetail detail = item.Tag as AdvanceExpenseDetail;
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

        private void SetVoucherDetailToEditMode()
        {
            _isDetailUpdateMode = true;
            addButton.Text = @"Change";
        }

        private void voucherDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string description = voucherDescriptionTextBox.Text;
                descriptionTextBox.Text = description;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeVoucherDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (voucherDetailListView.SelectedItems.Count > 0)
                //{
                //    _isRemoveMode = true;
                //    int detailItem = voucherDetailListView.SelectedItems[0].Index;
                //    ExpenseVoucherDetail voucherDetail =
                //        voucherDetailListView.SelectedItems[0].Tag as ExpenseVoucherDetail;
                //    if (voucherDetail == null)
                //    {
                //        throw new UiException("Voucher detail cannot be deleted.");
                //    }
                //    if (voucherDetail.Description.Equals("Advance Adjustment"))
                //    {
                //        throw new UiException("Voucher detail cannot be deleted.");
                //    }
                //    RemoveTotalAmountFromGridView(voucherDetailDataGridView);
                //    voucherDetailListView.Items.RemoveAt(detailItem);
                //    RemoveVoucherAdjustment(voucherDetail);
                //    LoadExpenseDetailInformation(voucherDetail.ExpenseDetails);
                //    _isRemoveMode = false;
                //    if (voucherDetailListView.Items.Count >= 1)
                //    {
                //        SetTotalAmountInGridView(voucherDetailDataGridView);
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveVoucherAdjustment(ExpenseVoucherDetail voucherDetail)
        {
            var expenseVoucherDetailsForAdvances =
                            _expenseVoucherHeaderManager.GetVoucherDetailsForRelatedRequisition(voucherDetail.ExpenseDetails);
            if (expenseVoucherDetailsForAdvances != null && expenseVoucherDetailsForAdvances.Any())
            {
                var expenseVoucherDetail =
                GetVoucherDetailsFromGridView().Where(c => c.Description.Equals("Advance Adjustment")).ToList();
                //foreach (ListViewItem item in voucherDetailListView.Items)
                //{
                //    ExpenseVoucherDetail detail = item.Tag as ExpenseVoucherDetail;
                //    if (detail != null)
                //    {
                //        if (detail.Description.Equals("Advance Adjustment"))
                //        {
                //            foreach (ExpenseVoucherDetail voucherDetailsForAdvance in expenseVoucherDetailsForAdvances)
                //            {
                //                if (voucherDetailsForAdvance.AccountCode.Equals(detail.AccountCode) &&
                //                    voucherDetailsForAdvance.CrAmount == detail.CrAmount &&
                //                    voucherDetailsForAdvance.DrAmount == detail.DrAmount &&
                //                    voucherDetailsForAdvance.IsNet == detail.IsNet)
                //                {
                //                    voucherDetailListView.Items.RemoveAt(item.Index);
                //                }
                //            }
                //        }
                //    }
                //}

                foreach (DataGridViewRow  row in voucherDetailDataGridView.Rows)
                {
                    ExpenseVoucherDetail detail = row.Tag as ExpenseVoucherDetail;
                    if (detail != null)
                    {
                        if (detail.Description.Equals("Advance Adjustment"))
                        {
                            foreach (ExpenseVoucherDetail voucherDetailsForAdvance in expenseVoucherDetailsForAdvances)
                            {
                                if (voucherDetailsForAdvance.AccountCode.Equals(detail.AccountCode) &&
                                    voucherDetailsForAdvance.CrAmount == detail.CrAmount &&
                                    voucherDetailsForAdvance.DrAmount == detail.DrAmount &&
                                    voucherDetailsForAdvance.IsNet == detail.IsNet)
                                {
                                    voucherDetailDataGridView.Rows.RemoveAt(row.Index);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();

                ExpenseVoucherDetail detail = new ExpenseVoucherDetail();
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
                detail.ExpenseDetails = GetCheckedExpenseDetails();
                detail.IsNet = isNetCheckBox.Checked;
                detail.VendorId = (int)vendorComboBox.SelectedValue == (int)DefaultItem.Value ? (int?)null : (int)vendorComboBox.SelectedValue;
                detail.VatTaxCategoryId = categoryComboBox.SelectedValue == null || (int)categoryComboBox.SelectedValue == (int)DefaultItem.Value ? (int?)null : (int)categoryComboBox.SelectedValue;
                detail.Percentage = percentageTextBox.Text == string.Empty ? null : percentageTextBox.Text;
                Accounts_ChartOfAccounts chartOfAccounts = _chartOfAccountManager.GetByCode(detail.AccountCode);
                RemoveTotalAmountFromGridView(voucherDetailDataGridView);
                string accountCodeAndDescription = chartOfAccounts.AccountCodeAndDescription;
                if (!_isDetailUpdateMode)
                {
                    var item = GetNewGridViewItem(detail, accountCodeAndDescription);
                    voucherDetailDataGridView.Rows.Add(item);
                    if (detail.ExpenseDetails != null && detail.ExpenseDetails.Any())
                    {
                        var expenseVoucherDetailsForAdvances =
                            _expenseVoucherHeaderManager.GetVoucherDetailsForRelatedRequisition(detail.ExpenseDetails);
                        if (expenseVoucherDetailsForAdvances != null && expenseVoucherDetailsForAdvances.Any())
                        {
                            var accountCodeList = expenseVoucherDetailsForAdvances.Select(d => d.AccountCode);
                            var coaList =
                                _chartOfAccountManager.Get(
                                    c => accountCodeList
                                            .Contains(c.AccountCode)).ToList();
                            foreach (var advanceVoucherDetail in expenseVoucherDetailsForAdvances)
                            {
                                var coa = coaList.FirstOrDefault(c => c.AccountCode == advanceVoucherDetail.AccountCode);
                                voucherDetailDataGridView.Rows.Add(GetNewGridViewItem(advanceVoucherDetail, coa.AccountCodeAndDescription));
                            }
                        }
                    }
                }
                else
                {
                    var updateableDetail = _updateableDetailItem.Tag as ExpenseVoucherDetail;
                    updateableDetail.AccountCode = detail.AccountCode;
                    updateableDetail.CrAmount = detail.CrAmount;
                    updateableDetail.DrAmount = detail.DrAmount;
                    updateableDetail.ExpenseDetails = detail.ExpenseDetails;
                    updateableDetail.Description = detail.Description;
                    updateableDetail.IsNet = detail.IsNet;
                    updateableDetail.Percentage = detail.Percentage;
                    updateableDetail.VatTaxCategoryId = detail.VatTaxCategoryId;
                    updateableDetail.VendorId = detail.VendorId;
                    SetChangedGridViewItemByDetail(updateableDetail, _updateableDetailItem, accountCodeAndDescription);
                    ResetChangeMode();
                }
                amountTextBox.Text = string.Empty;
                RemoveAddedItemsFromExpenseDetailsListView();
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

        private void SetDescriptionAccordingToVoucherDescription()
        {
            string description = voucherDescriptionTextBox.Text;
            descriptionTextBox.Text = description;
        }

        private void ResetChangeMode()
        {
            addButton.Text = @"Add";
            _isDetailUpdateMode = false;
            ResetUpdateableExpenseDetailItem();
            _updateableDetailItem = null;
        }

        private bool IsUpdateableItemIsAlreadySetNet()
        {
            if (_isDetailUpdateMode && isNetCheckBox.Checked)
            {
                var expenseDetail = _updateableDetailItem.Tag as ExpenseVoucherDetail;
                return expenseDetail.IsNet;
            }
            return false;
        }

        //private ListViewItem SetChangedListViewItemByDetail(ExpenseVoucherDetail detail, ListViewItem item, string accountCodeAndDescription)
        //{
        //    item.Text = accountCodeAndDescription;
        //    item.SubItems[1].Text = detail.Description;
        //    item.SubItems[2].Text = detail.DrAmount != null && detail.DrAmount > 0 ? detail.DrAmount.Value.ToString("N") : string.Empty;
        //    item.SubItems[3].Text = detail.CrAmount != null && detail.CrAmount > 0 ? detail.CrAmount.Value.ToString("N") : string.Empty;
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(ExpenseVoucherDetail detail, DataGridViewRow row, string accountCodeAndDescription)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = accountCodeAndDescription;
            row.Cells[3].Value =  detail.Description;
            row.Cells[4].Value = detail.DrAmount != null && detail.DrAmount > 0 ? detail.DrAmount.Value.ToString("N") : string.Empty;
            row.Cells[5].Value = detail.CrAmount != null && detail.CrAmount > 0 ? detail.CrAmount.Value.ToString("N") : string.Empty;
            
            row.Tag = detail;
            return row;
        }

        //private ListViewItem GetNewListViewItem(ExpenseVoucherDetail detail, string accountCodeAndDescription)
        //{
        //    ListViewItem item = new ListViewItem(accountCodeAndDescription);
        //    item.SubItems.Add(detail.Description);
        //    item.SubItems.Add(detail.DrAmount != null && detail.DrAmount > 0 ? detail.DrAmount.Value.ToString("N") : string.Empty);
        //    item.SubItems.Add(detail.CrAmount != null && detail.CrAmount > 0 ? detail.CrAmount.Value.ToString("N") : string.Empty);
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItem(ExpenseVoucherDetail detail, string accountCodeAndDescription)
        {
            DataGridViewRow row=new DataGridViewRow();
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

        private bool ValidateAdd()
        {
            string errorMessage = string.Empty;
            var isNetValue = isNetCheckBox.Checked ? 1 : 0;

            if (voucherTypeComboBox.Text == DefaultItem.Text)
            {
                errorMessage += " Voucher Type must be selected. " + Environment.NewLine;
            }
            if (accountCodeComboBox.SelectedValue.Equals(DefaultItem.Text))
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

        private ICollection<AdvanceExpenseDetail> GetCheckedExpenseDetails()
        {
            ListView.CheckedListViewItemCollection items = expenseDetailsListView.CheckedItems;
            ICollection<AdvanceExpenseDetail> detailList = new List<AdvanceExpenseDetail>();
            foreach (ListViewItem item in items)
            {
                AdvanceExpenseDetail detail = item.Tag as AdvanceExpenseDetail;
                detailList.Add(detail);
            }
            return detailList;
        }

        private void editVoucherDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (voucherDetailListView.SelectedItems.Count > 0)
                //{
                //    ResetUpdateableExpenseDetailItem();
                //    _isDetailUpdateMode = true;
                //    _updateableDetailItem = voucherDetailListView.SelectedItems[0];
                //    ExpenseVoucherDetail voucherDetail =
                //        voucherDetailListView.SelectedItems[0].Tag as ExpenseVoucherDetail;
                //    if (voucherDetail == null)
                //    {
                //        throw new UiException("Voucher detail cannot be edited.");
                //    }
                //    //if (voucherDetail.ExpenseDetails == null || !voucherDetail.ExpenseDetails.Any())
                //    //{
                //    //    throw new UiException("Voucher detail cannot be edited.");
                //    //}
                //    LoadAccountComboByAccountCodes();
                //    LoadExpenseDetailInformation(voucherDetail.ExpenseDetails);
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
        private void LoadVoucherDetailInformation(ExpenseVoucherDetail voucherDetail)
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                ExpenseVoucherHeader voucherHeader = new ExpenseVoucherHeader();
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
                //voucherHeader.ExpenseVoucherDetails = GetVoucherDetailsFromListView();
                voucherHeader.ExpenseVoucherDetails = GetVoucherDetailsFromGridView();
                voucherHeader.CreatedBy = Session.LoginUserName;
                voucherHeader.CreatedOn = DateTime.Now;
                voucherHeader.ExpenseHeaderId = _expenseHeader.Id;
                voucherHeader.Currency = "TK";
                voucherHeader.ConversionRate = 1;

                if (!_isUpdateMode)
                {
                    bool isInserted = _expenseVoucherHeaderManager.Insert(voucherHeader);
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
                    
                    bool isInserted = _expenseVoucherHeaderManager.Edit(voucherHeader);
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (expenseDetailsListView.Items.Count > 0)
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

        private int GetIsNetCount()
        {
            //var voucherDetails = GetVoucherDetailsFromListView();
            var voucherDetails = GetVoucherDetailsFromGridView();
            var isNetCount = voucherDetails.Where(c => c.IsNet == true).Select(c => c.IsNet).Count();
            return isNetCount;
        }

        private decimal GetTotalDebitAmount()
        {
            decimal debitAmount = 0;
            //foreach (ListViewItem item in voucherDetailListView.Items)
            //{
            //    ExpenseVoucherDetail detail = item.Tag as ExpenseVoucherDetail;
            //    if (detail != null && detail.DrAmount != null)
            //    {
            //        debitAmount += detail.DrAmount.Value;
            //    }
            //}

            foreach (DataGridViewRow row in voucherDetailDataGridView.Rows)
            {
                ExpenseVoucherDetail detail = row.Tag as ExpenseVoucherDetail;
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
            //    ExpenseVoucherDetail detail = item.Tag as ExpenseVoucherDetail;
            //    if (detail != null && detail.CrAmount != null)
            //    {
            //        creditAmount += detail.CrAmount.Value;
            //    }
            //}

            foreach (DataGridViewRow row in voucherDetailDataGridView.Rows)
            {
                ExpenseVoucherDetail detail = row.Tag as ExpenseVoucherDetail;
                if (detail != null && detail.CrAmount != null)
                {
                    creditAmount += detail.CrAmount.Value;
                }
            }
            return creditAmount;
        }

        //private List<ExpenseVoucherDetail> GetVoucherDetailsFromListView()
        //{
        //    List<ExpenseVoucherDetail> voucherDetails = new List<ExpenseVoucherDetail>();
        //    foreach (ListViewItem item in voucherDetailListView.Items)
        //    {
        //        ExpenseVoucherDetail detail = item.Tag as ExpenseVoucherDetail;
        //        if (detail != null)
        //            voucherDetails.Add(detail);
        //    }
        //    return voucherDetails;
        //}

        private List<ExpenseVoucherDetail> GetVoucherDetailsFromGridView()
        {
            List<ExpenseVoucherDetail> voucherDetails = new List<ExpenseVoucherDetail>();
            foreach (DataGridViewRow row in voucherDetailDataGridView.Rows)
            {
                ExpenseVoucherDetail detail = row.Tag as ExpenseVoucherDetail;
                if (detail != null)
                    voucherDetails.Add(detail);
            }
            return voucherDetails;
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

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetUpdateableExpenseDetailItem();
                LoadAccountComboByAccountCodes();
                ClearVoucherDetailForm();
                ResetChangeMode();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accountCodeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var selectedAccount = accountCodeComboBox.SelectedItem as Accounts_ChartOfAccounts;
                string accountCode = selectedAccount.AccountCode;

                if (accountCode != DefaultItem.Text && !String.IsNullOrEmpty(accountCode))
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal? GetAmount()
        {
            var debitAmount = debitAmountTextBox.Text == string.Empty ? 0 : Convert.ToDecimal(debitAmountTextBox.Text);
            var creditAmount = creditAmountTextBox.Text == string.Empty ? 0 : Convert.ToDecimal(creditAmountTextBox.Text);
            if (debitAmount == 0 && creditAmount == 0)
            {
                return null;
            }
            if (debitAmount > creditAmount)
            {
                return debitAmount;
            }
            if (creditAmount > debitAmount)
            {
                return creditAmount;
            }
            return null;
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
                            ResetUpdateableExpenseDetailItem();
                            _isDetailUpdateMode = true;
                            _updateableDetailItem = voucherDetailDataGridView.SelectedRows[0];
                            ExpenseVoucherDetail voucherDetail =
                                voucherDetailDataGridView.SelectedRows[0].Tag as ExpenseVoucherDetail;
                            if (voucherDetail == null)
                            {
                                throw new UiException("Voucher detail cannot be edited.");
                            }
                            //if (voucherDetail.ExpenseDetails == null || !voucherDetail.ExpenseDetails.Any())
                            //{
                            //    throw new UiException("Voucher detail cannot be edited.");
                            //}
                            LoadAccountComboByAccountCodes();
                            LoadExpenseDetailInformation(voucherDetail.ExpenseDetails);
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
                            ExpenseVoucherDetail voucherDetail =
                                voucherDetailDataGridView.SelectedRows[0].Tag as ExpenseVoucherDetail;
                            if (voucherDetail == null)
                            {
                                throw new UiException("Voucher detail cannot be deleted.");
                            }
                            if (voucherDetail.Description.Equals("Advance Adjustment"))
                            {
                                throw new UiException("Voucher detail cannot be deleted.");
                            }
                            RemoveTotalAmountFromGridView(voucherDetailDataGridView);
                            voucherDetailDataGridView.Rows.RemoveAt(detailItem);
                            RemoveVoucherAdjustment(voucherDetail);
                            LoadExpenseDetailInformation(voucherDetail.ExpenseDetails);
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
