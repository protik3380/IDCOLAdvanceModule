using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Accounts;
using Accounts.NerdCastle.NCDAO;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Voucher
{
    public partial class PreviewVoucherUI : Form
    {
        private readonly TransactionVoucher _transactionVoucher;
        private readonly IBankManager _bankManager;
        private readonly IBranchManager _branchManager;
        private readonly AdvanceVoucherGenerateManager _advanceVoucherGenerateManager;
        private readonly RequisitionVoucherHeader _requisitionVoucherHeader;
        private readonly ExpenseVoucherHeader _expenseVoucherHeader;
        private readonly IRequisitionVoucherHeaderManager _requisitionVoucherHeaderManager;
        private readonly IExpenseVoucherHeaderManager _expenseVoucherHeaderManager;

        private PreviewVoucherUI()
        {
            _bankManager = new BankManager();
            _branchManager = new BranchManager();
            _advanceVoucherGenerateManager = new AdvanceVoucherGenerateManager();
            InitializeComponent();
            _requisitionVoucherHeader = null;
            _expenseVoucherHeader = null;
            _requisitionVoucherHeaderManager = new RequisitionVoucherHeaderManager();
            _expenseVoucherHeaderManager = new ExpenseVoucherHeaderManager();
        }

        public PreviewVoucherUI(TransactionVoucher transactionVoucher)
            : this()
        {
            this._transactionVoucher = transactionVoucher ?? new TransactionVoucher();
            LoadVoucherGridView();
            FillCurrencyComboBox();
            FillBankNameComboBox();
            FillVoucherHeaderInformation();
        }

        public PreviewVoucherUI(TransactionVoucher transactionVoucher, RequisitionVoucherHeader requisitionVoucherHeader)
            : this(transactionVoucher)
        {
            _requisitionVoucherHeader = requisitionVoucherHeader;
        }

        public PreviewVoucherUI(TransactionVoucher transactionVoucher, ExpenseVoucherHeader expenseVoucherHeader)
            : this(transactionVoucher)
        {
            _expenseVoucherHeader = expenseVoucherHeader;
        }

        private void FillVoucherHeaderInformation()
        {
            voucherOwnerTextBox.Text = _transactionVoucher.VoucherOwner;
            voucherDescriptionTextBox.Text = _transactionVoucher.VoucherDesc;
            VoucherEntryDateTimePicker.Value = _transactionVoucher.EntryDate;
            if (_transactionVoucher.EntryDate == VoucherEntryDateTimePicker.Value)
            {
                VoucherEntryDateTimePicker.Checked = true;
            }
            if (_transactionVoucher.BankId != null)
            {
                bankComboBox.SelectedIndex = bankComboBox.FindStringExact(_transactionVoucher.BankName);

            }

            if (_transactionVoucher.BranchId != null)
            {
                LoadBankBranches();
                branchComboBox.SelectedIndex = branchComboBox.FindStringExact(_transactionVoucher.BranchName);
            }

            chequeNoTextBox.Text = _transactionVoucher.ChequeNo;
            chequeDateDateTimePicker.Value = (DateTime)(_transactionVoucher.ChequeDate ?? DateTime.Today);
            chequeDateDateTimePicker.Checked = true;
            currencyComboBox.Text = _transactionVoucher.CurrencyCode;
            conversionRateTextBox.Text = _transactionVoucher.ConversionRate.ToString();
        }

        private void LoadVoucherGridView()
        {
            previewVoucherDataGridView.AutoGenerateColumns = false;
            previewVoucherDataGridView.DataSource = null;
            previewVoucherDataGridView.DataSource = _transactionVoucher.TransactionDetails;
        }

        private void previewVoucherDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (previewVoucherDataGridView.CurrentRow != null)
                    {
                        var detail = previewVoucherDataGridView.CurrentRow.DataBoundItem as TransactionDetail;
                        LoadVoucherGridView();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void previewVoucherButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Validate()) return;
                InitializeVoucherHeaderData();
                if (CheckIfAccountCodeIsNotSelectedForAnyItem(_transactionVoucher))
                {
                    throw new Exception("Please provide account & description for all items");
                }
                PreviewVoucherDetailUI voucherDetailUi = new PreviewVoucherDetailUI(_transactionVoucher);
                voucherDetailUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckIfAccountCodeIsNotSelectedForAnyItem(TransactionVoucher voucher)
        {
            if (voucher != null && voucher.TransactionDetails != null)
            {
                return voucher.TransactionDetails.Any(transactionDetail => string.IsNullOrEmpty(transactionDetail.AccountCode) || string.IsNullOrEmpty(transactionDetail.Description));
            }
            return false;
        }

        private void InitializeVoucherHeaderData()
        {
            _transactionVoucher.VoucherOwner = voucherOwnerTextBox.Text;
            _transactionVoucher.VoucherDesc = voucherDescriptionTextBox.Text;
            _transactionVoucher.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
            _transactionVoucher.CurrencyCode = currencyComboBox.Text;
            _transactionVoucher.EntryDate = VoucherEntryDateTimePicker.Value;
            _transactionVoucher.EntryNo = _transactionVoucher.GetEntryNo();
            _transactionVoucher.FiscalPeriod =
               Accounts.FiscalYear.FiscalPeriod(_transactionVoucher.EntryDate.Month).ToString();
            _transactionVoucher.FiscalYear = _transactionVoucher.EntryDate.Year.ToString();

            if (chequeNoTextBox.Text != "")
            {
                _transactionVoucher.ChequeNo = chequeNoTextBox.Text;
            }
            else
            {
                _transactionVoucher.ChequeNo = null;
            }

            if (chequeDateDateTimePicker.Checked)
            {
                _transactionVoucher.ChequeDate = chequeDateDateTimePicker.Value;
            }

            if (bankComboBox.Text != "N/A" && bankComboBox.Text != "" && bankComboBox.SelectedValue != DefaultItem.Value.ToString())
            {
                _transactionVoucher.BankId = Convert.ToInt32(bankComboBox.SelectedValue) == -1 ? (int?)null : Convert.ToInt32(bankComboBox.SelectedValue);
            }

            if (branchComboBox.Text != "N/A" && branchComboBox.Text != "" && branchComboBox.SelectedValue != DefaultItem.Value.ToString())
            {
                _transactionVoucher.BranchId = Convert.ToInt32(branchComboBox.SelectedValue) == -1 ? (int?)null : Convert.ToInt32(branchComboBox.SelectedValue);
            }
        }

        private bool Validate()
        {
            if (voucherOwnerTextBox.Text == string.Empty)
            {
                MessageBox.Show(@"Please provide recipient or payee information.", @"Error!", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return true;
            }
            if (voucherDescriptionTextBox.Text == string.Empty)
            {
                MessageBox.Show(@"Please provide voucher description.", @"Error!", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
            if (conversionRateTextBox.Text == string.Empty)
            {
                MessageBox.Show(@"Please provide conversion rate.", @"Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (chequeNoTextBox.Text == string.Empty)
            {
                DialogResult result = MessageBox.Show(@"No Cheque No Provided, proceed?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return true;
                }
            }
            if (!chequeDateDateTimePicker.Checked)
            {
                DialogResult result = MessageBox.Show(@"No Cheque Date Selected, proceed?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return true;
                }
            }
            if (bankComboBox.Text == string.Empty || bankComboBox.SelectedValue == DefaultItem.Value.ToString())
            {
                DialogResult result = MessageBox.Show(@"No Bank Selected, proceed?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return true;
                }
            }
            if (branchComboBox.Text == string.Empty || branchComboBox.SelectedValue == DefaultItem.Value.ToString())
            {
                DialogResult result = MessageBox.Show(@"No Branch Selected, proceed?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return true;
                }
            }
            if (!VoucherEntryDateTimePicker.Checked)
            {
                MessageBox.Show(@"Please provide voucher entry date", @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            _transactionVoucher.EntryDate = VoucherEntryDateTimePicker.Value;
            var activefiscalYear = Accounts.FiscalYear.GetActiveYear();
            if (_transactionVoucher.EntryDate.Date < activefiscalYear.DateFrom.Date ||
                _transactionVoucher.EntryDate.Date > activefiscalYear.DateTo.Date)
            {
                MessageBox.Show(@"Please select voucher entry date within active fiscal year date between " + activefiscalYear.DateFrom.Date.ToString("dd-MMM-yyyy") + @" and " + activefiscalYear.DateTo.Date.ToString("dd-MMM-yyyy"), "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private void sendVoucherButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Validate()) return;
                InitializeVoucherHeaderData();
                if (CheckIfAccountCodeIsNotSelectedForAnyItem(_transactionVoucher))
                {
                    throw new Exception("Please provide account code for all items");
                }

                // Transaction scope is removed from here for DTC Issue..
                string message = "";

                bool isInitiated = false;
                isInitiated = _advanceVoucherGenerateManager.InitiateVoucher(_transactionVoucher);
                if (!isInitiated)
                {
                    MessageBox.Show(@"Voucher generation failed!", @"Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;

                }

                message = "Voucher Created Successfully! ";
                Accounts_NC_Integration integration = null;
                if (_requisitionVoucherHeader != null)
                {
                    integration = _advanceVoucherGenerateManager.GetIntegrationInstance(_transactionVoucher, _requisitionVoucherHeader);
                }
                else if (_expenseVoucherHeader != null)
                {
                    integration = _advanceVoucherGenerateManager.GetIntegrationInstance(_transactionVoucher, _expenseVoucherHeader);
                }
                bool isIntegrationInserted = _advanceVoucherGenerateManager.InsertIntegration(integration);

                if (!isIntegrationInserted)
                {
                    message += "Integration Data Generation failed!";
                }
                if (isIntegrationInserted)
                {
                    if (_expenseVoucherHeader != null)
                    {
                        _expenseVoucherHeader.VoucherStatusId = (long)VoucherStatusEnum.Sent;
                        _expenseVoucherHeader.SentDate = DateTime.Now;
                        _expenseVoucherHeaderManager.Edit(_expenseVoucherHeader);
                    }
                    if (_requisitionVoucherHeader != null)
                    {
                        _requisitionVoucherHeader.VoucherStatusId = (long)VoucherStatusEnum.Sent;
                        _requisitionVoucherHeader.SentDate = DateTime.Now;
                        _requisitionVoucherHeaderManager.Edit(_requisitionVoucherHeader);
                    }
                }
                MessageBox.Show(message, @"Voucher Sending Status", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void conversionRateTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void FillCurrencyComboBox()
        {
            var manager = new Accounts.BLL.CurrencyManager();
            List<Accounts.Currency> currencies = manager.GetAllCurrencies();

            if (currencies != null)
            {
                currencyComboBox.DataSource = currencies;
                currencyComboBox.ValueMember = "ShortName";
                currencyComboBox.DisplayMember = "ShortName";
                currencyComboBox.SelectedValue = "TK";
            }
            else
            {
                MessageBox.Show(@"There was an error getting the currency information.\r\nPlease try again.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void FillBankNameComboBox()
        {
            List<Loan_Bank> banks = new List<Loan_Bank>();
            banks.Insert(0, new Loan_Bank() { Bank_ID = (int)DefaultItem.Value, Bank_Name = "Select.." });

            bankComboBox.DataSource = null;
            if (_transactionVoucher.BankId != null)
            {
                banks.AddRange(_bankManager.GetAll().ToList());
                bankComboBox.DisplayMember = "Bank_Name";
                bankComboBox.ValueMember = "Bank_ID";
                bankComboBox.DataSource = banks;
                bankComboBox.SelectedValue = _transactionVoucher.BankId;
            }
            bankComboBox.DisplayMember = "Bank_Name";
            bankComboBox.ValueMember = "Bank_ID";
            bankComboBox.DataSource = banks;
        }

        private void LoadBankBranches()
        {
            List<Loan_Bank_Branch> bankBranches = new List<Loan_Bank_Branch>();
            bankBranches.Insert(0, new Loan_Bank_Branch() { Branch_ID = DefaultItem.Value, Branch_Name = "Select..." });

            if (_transactionVoucher.BankId != null)
                bankBranches = _branchManager.GetByBankId((long)_transactionVoucher.BankId).ToList();

            if (bankBranches != null)
            {
                bankBranches.Insert(0, new Loan_Bank_Branch() { Branch_ID = DefaultItem.Value, Branch_Name = "Select..." });
                branchComboBox.DataSource = bankBranches;
                branchComboBox.DisplayMember = "Branch_Name";
                branchComboBox.ValueMember = "Branch_ID";
                if (bankBranches.Count > 0)
                {
                    branchComboBox.SelectedIndex = 0;
                }
                else
                {
                    branchComboBox.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show(@"There was an error getting the branches.\r\nPlease try again.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bankComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBankBranches();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void currencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currencyComboBox.SelectedValue.Equals("TK"))
                {
                    conversionRateTextBox.Text = @"1";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
