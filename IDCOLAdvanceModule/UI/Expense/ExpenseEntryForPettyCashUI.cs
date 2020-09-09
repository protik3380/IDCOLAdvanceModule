using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.ViewModels;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.UI.Expense
{
    public partial class ExpenseEntryForPettyCashUI : Form
    {
        private UserTable _selectedEmployee;
        private AdvanceCategory _advanceCategory;
        private DataGridViewRow _updateableDetailItem;
        private bool _isUpdateMode;
        private bool _isDetailUpdateMode;
        private ICollection<AdvanceRequisitionHeader> _advanceRequisitionHeaders;
        private readonly AdvancePettyCashExpenseHeader _updateableExpenseHeader;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly ICurrencyManager _currencyManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly ICollection<ExpenseFile> _expenseFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        private readonly AdvanceRequisitionHeader _advanceRequisitionHeader;
        private readonly ICollection<AdvancePettyCashRequisitionHeader> _requisitionHeaders;
        private string _requisitionNo;

        public ExpenseEntryForPettyCashUI()
        {
            InitializeComponent();
            _expenseFiles = new List<ExpenseFile>();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _currencyManager = new IDCOLAdvanceModule.BLL.MISManager.CurrencyManager();
            _employeeManager = new EmployeeManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
            _requisitionHeaders = new List<AdvancePettyCashRequisitionHeader>();
        }

        public ExpenseEntryForPettyCashUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public ExpenseEntryForPettyCashUI(AdvancePettyCashExpenseHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableExpenseHeader = header;
            if (header.AdvancePettyCashRequisitionHeaders != null &&
                header.AdvancePettyCashRequisitionHeaders.Any())
            {
                foreach (AdvancePettyCashRequisitionHeader requisitionHeader in header.AdvancePettyCashRequisitionHeaders)
                {
                    _requisitionHeaders.Add(requisitionHeader);
                }
            }
            _advanceRequisitionHeaders = header.AdvanceRequisitionHeaders;
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            LoadHeaderInformationFromExpense(header);
            LoadDetailsInformationFromExpense(header.AdvancePettyCashExpenseDetails.ToList());
            _expenseFiles = header.ExpenseFiles;
            SetUploadedFileNumberLabel();
            if (advancedFormMode == AdvancedFormMode.Update)
            {
                SetFormToUpdateMode();
            }
            else if (advancedFormMode == AdvancedFormMode.View)
            {
                SetFormToViewMode();
            }
        }

        public ExpenseEntryForPettyCashUI(ICollection<AdvancePettyCashRequisitionHeader> requisitionHeaders)
            : this()
        {
            _requisitionHeaders = requisitionHeaders;
            DateTime fromDate = _requisitionHeaders.Min(c => c.FromDate);
            DateTime toDate = _requisitionHeaders.Max(c => c.ToDate);
            var advancePettyCashRequisitionHeader = _requisitionHeaders.FirstOrDefault();
            if (advancePettyCashRequisitionHeader != null)
            {
                long categoryId = advancePettyCashRequisitionHeader.AdvanceCategoryId;
                _advanceCategory = _advanceRequisitionCategoryManager.GetById(categoryId);
                _selectedEmployee = _employeeManager.GetByUserName(advancePettyCashRequisitionHeader.RequesterUserName);
            }
            if (_requisitionHeaders.Count == 1)
            {
                LoadHeaderInformationFromRequisition(advancePettyCashRequisitionHeader);
            }
            else
            {
                LoadHeaderInformationFromRequisition(fromDate, toDate);
            }
            LoadDetailsInformationFromRequisition(_requisitionHeaders);
        }

        private void ExpenseEntryForPettyCashUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseDateTimePicker.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadCurrencyComboBox();
                LoadVatTaxTypeComboBox();
                SetFormTitleAndText();
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormTitleAndText()
        {
            if (_advanceCategory == null)
            {
                throw new UiException("Category not found.");
            }
            string title = "Adjustment/Reimbursement Entry Form (" + _advanceCategory.Name + ")";
            Text = title;
            titleLabel.Text = title;
        }

        private void LoadVatTaxTypeComboBox()
        {
            vatTaxTypeComboBox.DataSource = null;
            var vatTaxTypeList = new List<VatTaxType>
                {
                    new VatTaxType{Id = DefaultItem.Value, Name = "None"}
                };
            vatTaxTypeList.AddRange(_vatTaxTypeManager.GetAll());
            vatTaxTypeComboBox.ValueMember = "Id";
            vatTaxTypeComboBox.DisplayMember = "Name";
            vatTaxTypeComboBox.DataSource = vatTaxTypeList;
        }

        private void SetFormToViewMode()
        {
            expenseDateTimePicker.Enabled = false;
            addButton.Visible = false;
            saveButton.Visible = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
            detailsAddGroupBox.Enabled = false;
            //detailsShowGroupBox.Enabled = false;
            //expenseDetailsListView.ContextMenuStrip = null;
        }

        private void SetFormToUpdateMode()
        {
            _isUpdateMode = true;
            saveButton.Text = @"Update";
        }

        private void LoadHeaderInformationFromRequisition(AdvancePettyCashRequisitionHeader header)
        {
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            //requisitionDateTime.Value = header.RequisitionDate;
            expenseDateTimePicker.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
        }

        private void LoadHeaderInformationFromRequisition(DateTime fromDate, DateTime toDate)
        {
            fromDateTimePicker.Value = fromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = toDate;
            toDateTimePicker.Checked = true;
        }

        private void LoadHeaderInformationFromExpense(AdvancePettyCashExpenseHeader header)
        {
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            expenseDateTimePicker.Value = header.ExpenseEntryDate;
            expenseDateTimePicker.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
        }

        //private void LoadDetailsInformationFromRequisition(List<AdvancePettyCashRequisitionDetail> details)
        //{
        //    foreach (AdvancePettyCashRequisitionDetail detail in details)
        //    {
        //        ListViewItem item = new ListViewItem(_advanceRequisitionHeader.RequisitionNo);
        //        item.SubItems.Add(detail.Purpose);
        //        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //        item.SubItems.Add((detail.AdvanceAmount - detail.AdvanceAmount).ToString("N"));
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //        AdvancePettyCashExpenseDetail advancePettyCashExpenseDetail = new AdvancePettyCashExpenseDetail()
        //        {
        //            AdvanceRequisitionDetailId = detail.Id,
        //            AdvanceAmount = detail.AdvanceAmount,
        //            ExpenseAmount = detail.AdvanceAmount,
        //            Purpose = detail.Purpose,
        //            Remarks = detail.Remarks,
        //            IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
        //            ReceipientOrPayeeName = detail.ReceipientOrPayeeName
        //        };
        //        item.Tag = advancePettyCashExpenseDetail;

        //        expenseDetailsListView.Items.Add(item);
        //    }

        //    SetTotalAmountInListView(expenseDetailsListView);
        //}

        
        private void LoadDetailsInformationFromRequisition(ICollection<AdvancePettyCashRequisitionHeader> requisitionHeaders)
        {
            foreach (AdvancePettyCashRequisitionHeader header in requisitionHeaders)
            {
                foreach (AdvancePettyCashRequisitionDetail detail in header.AdvancePettyCashRequisitionDetails)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseDetailsDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = header.RequisitionNo;
                    row.Cells[3].Value = detail.Purpose;
                    row.Cells[4].Value = detail.AdvanceAmount.ToString("N");
                    row.Cells[5].Value = detail.AdvanceAmount.ToString("N");
                    row.Cells[6].Value = (detail.AdvanceAmount - detail.AdvanceAmount).ToString("N");
                    row.Cells[7].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                    row.Cells[8].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                        ? detail.ReceipientOrPayeeName
                        : "N/A";
                    AdvancePettyCashExpenseDetail advancePettyCashExpenseDetail = new AdvancePettyCashExpenseDetail()
                       {
                           AdvanceRequisitionDetailId = detail.Id,
                           AdvanceAmount = detail.AdvanceAmount,
                           ExpenseAmount = detail.AdvanceAmount,
                           Purpose = detail.Purpose,
                           Remarks = detail.Remarks,
                           IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
                           ReceipientOrPayeeName = detail.ReceipientOrPayeeName
                       };
                    row.Tag = advancePettyCashExpenseDetail;

                    expenseDetailsDataGridView.Rows.Add(row);
                }
            }
            SetTotalAmountInGridView(expenseDetailsDataGridView);
        }

        private void LoadDetailsInformationFromExpense(List<AdvancePettyCashExpenseDetail> details)
        {
            foreach (AdvancePettyCashExpenseDetail detail in details)
            {
                ListViewItem item = new ListViewItem();
                if (detail.AdvanceTravelRequisitionDetail != null)
                {
                    item.Text =
                        detail.AdvanceTravelRequisitionDetail.AdvancePettyCashRequisitionHeader.RequisitionNo;
                }
                else
                {
                    item.Text = string.Empty;
                }
                item.SubItems.Add(detail.Purpose);
                item.SubItems.Add(detail.GetExpenseAmountInBdt().ToString("N"));
                item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                var reimburse = detail.GetFormattedReimbursementAmount();
                item.SubItems.Add(reimburse);
                item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
                item.Tag = detail;

                expenseDetailsDataGridView.Rows.Add(item);
            }

            SetTotalAmountInGridView(expenseDetailsDataGridView);
        }

        private string GetReimburseAmount(decimal expenseAmount, decimal advanceAmount)
        {
            var reimburse = (expenseAmount - advanceAmount);
            if (reimburse >= 0)
                return reimburse.ToString("N");
            return "(" + Math.Abs(reimburse) + ")";
        }

        //private List<AdvancePettyCashExpenseDetail> GetAdvanceExpenseDetailsFromListView(ListView expenseDetailsListViewControl)
        //{
        //    var details = new List<AdvancePettyCashExpenseDetail>();
        //    if (expenseDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in expenseDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvancePettyCashExpenseDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private List<AdvancePettyCashExpenseDetail> GetAdvanceExpenseDetailsFromGridView(DataGridView expenseDetailsGridViewControl)
        {
            var details = new List<AdvancePettyCashExpenseDetail>();
            if (expenseDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in expenseDetailsGridViewControl.Rows)
                {
                    var detailItem = row.Tag as AdvancePettyCashExpenseDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
        }

        private void LoadCurrencyComboBox()
        {
            currencyComboBox.DataSource = null;

            ICollection<Solar_CurrencyInfo> currencyList =
                _currencyManager.GetAdvanceRequsitionCategoryWiseCurrencyInfo(_advanceCategory.Id);
            currencyComboBox.DisplayMember = "ShortName";
            currencyComboBox.ValueMember = "CurrencyID";
            currencyComboBox.DataSource = currencyList;
            if (currencyComboBox.Text.Equals("TK"))
            {
                conversionRateTextBox.Text = @"1";
                conversionRateTextBox.Enabled = false;
            }
            else
            {
                conversionRateTextBox.Text = string.Empty;
                conversionRateTextBox.Enabled = true;
            }
        }

        private void LoadEmployeeInformation()
        {
            if (_selectedEmployee != null)
            {
                designationTextBox.Text = _selectedEmployee.Admin_Rank == null ? "N/A" : _selectedEmployee.Admin_Rank.RankName;
                employeeNameTextBox.Text = _selectedEmployee.FullName;
                employeeIdTextBox.Text = _selectedEmployee.EmployeeID;
                departmentTextBox.Text = _selectedEmployee.Admin_Departments == null ? "N/A" : _selectedEmployee.Admin_Departments.DepartmentName;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EditDetailItem();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditDetailItem()
        {
            //AdvancePettyCashExpenseDetail detail = null;
            //if (expenseDetailsListView.SelectedItems.Count > 0)
            //{
            //    ListViewItem item = expenseDetailsListView.SelectedItems[0];
            //    if (expenseDetailsListView.Items.Count == item.Index + 1)
            //    {
            //        throw new UiException("No item is selected to edit.");
            //    }
            //    detail = item.Tag as AdvancePettyCashExpenseDetail;
            //    if (detail == null)
            //    {
            //        throw new UiException("Item is not tagged");
            //    }
            //    _updateableDetailItem = item;
            //    _requisitionNo = item.Text;
            //    purposeTextBox.Text = detail.Purpose;
            //    advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
            //    expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
            //    remarksTextBox.Text = detail.Remarks;
            //    isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
            //    receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
            //    vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
            //    if (detail.AdvanceRequisitionDetailId != null && detail.AdvanceRequisitionDetailId.Value > 0)
            //    {
            //        isThirdPartyReceipientCheckBox.Enabled = false;
            //    }
            //    SetFormToDetailChangeMode(item);
            //}
            //else
            //{
            //    throw new UiException("No item is selected to edit.");
            //}
        }

        private void SetFormToDetailChangeMode(DataGridViewRow detail)
        {
            if (detail == null)
            {
                throw new Exception("There is no data found for changing!");
            }

            _isDetailUpdateMode = true;
            addButton.Text = @"Change";
            _updateableDetailItem = detail;
            saveButton.Visible = false;
            AdvancePettyCashExpenseDetail expenseDetail = detail.Tag as AdvancePettyCashExpenseDetail;
            if (expenseDetail != null && expenseDetail.AdvanceRequisitionDetailId != null)
            {
                if (expenseDetail.AdvanceRequisitionDetail != null)
                {
                    if (expenseDetail.AdvanceRequisitionDetail.Id > 0)
                        advanceAmountTextBox.ReadOnly = true;
                }
                else if (expenseDetail.AdvanceRequisitionDetailId != null)
                {
                    if (expenseDetail.AdvanceRequisitionDetailId > 0)
                        advanceAmountTextBox.ReadOnly = true;
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();

                AdvancePettyCashExpenseDetail detail = new AdvancePettyCashExpenseDetail();
                detail.Purpose = purposeTextBox.Text;
                if (string.IsNullOrEmpty(advanceAmountTextBox.Text))
                {
                    detail.AdvanceAmount = 0;
                }
                else
                {
                    detail.AdvanceAmount = Convert.ToDecimal(advanceAmountTextBox.Text);
                }
                detail.ExpenseAmount = Convert.ToDecimal(expenseAmountTextBox.Text);
                if (!string.IsNullOrEmpty(remarksTextBox.Text))
                {
                    detail.Remarks = remarksTextBox.Text;
                }
                detail.IsThirdPartyReceipient = isThirdPartyReceipientCheckBox.Checked;
                if (!string.IsNullOrEmpty(receipientOrPayeeNameTextBox.Text))
                {
                    detail.ReceipientOrPayeeName = receipientOrPayeeNameTextBox.Text;
                }
                detail.VatTaxTypeId = (long)vatTaxTypeComboBox.SelectedValue == DefaultItem.Value
                   ? null
                   : (long?)vatTaxTypeComboBox.SelectedValue;

                if (!ValidateCeilingAmount())
                {
                    DialogResult result = MessageBox.Show(@"You are about to exceed the ceiling amount (" + _advanceCategory.CeilingAmount + @"). Do you want to continue?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                if (_isDetailUpdateMode)
                {
                    if (_updateableDetailItem == null)
                    {
                        throw new UiException("There is no item found to update!");
                    }
                    var item = _updateableDetailItem;
                    var _updateDetailItem = item.Tag as AdvancePettyCashExpenseDetail;
                    if (_updateDetailItem == null)
                    {
                        throw new UiException("There is no item tagged with the detail item!");
                    }
                    detail.AdvanceRequisitionDetail = _updateDetailItem.AdvanceRequisitionDetail;
                    _requisitionNo = (string) item.Cells[2].Value;
                    _updateDetailItem.Purpose = detail.Purpose;
                    _updateDetailItem.AdvanceAmount = detail.AdvanceAmount;
                    _updateDetailItem.ExpenseAmount = detail.ExpenseAmount;
                    _updateDetailItem.Remarks = detail.Remarks;
                    _updateDetailItem.NoOfUnit = detail.NoOfUnit;
                    _updateDetailItem.UnitCost = detail.UnitCost;
                    _updateDetailItem.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;
                    _updateDetailItem.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;

                    RemoveTotalAmountFromGridView();
                    detail.Id = _updateDetailItem.Id;
                    SetChangedGridViewItemByDetail(_updateDetailItem, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    //IsRequisitionDetailsExist(detail);
                    var item = GetNewGridViewItemByDetail(detail);

                    RemoveTotalAmountFromGridView();
                    expenseDetailsDataGridView.Rows.Add(item);
                }
                SetTotalAmountInGridView(expenseDetailsDataGridView);
                ClearDetailControl();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void SetRecipientWiseReimbursementRefund(ListView listView)
        //{
        //    ICollection<AdvancePettyCashExpenseDetail> details = GetAdvanceExpenseDetailsFromListView(listView);

        //    List<RecipientWithReimbursementRefund> recipientWiseReimbursementRefund = new List<RecipientWithReimbursementRefund>();

        //    foreach (AdvancePettyCashExpenseDetail detail in details)
        //    {
        //        recipientWiseReimbursementRefund.Add(new RecipientWithReimbursementRefund { Recipient = detail.ReceipientOrPayeeName, Amount = detail.GetReimbursementOrRefundAmount() });
        //    }

        //    var data = recipientWiseReimbursementRefund.GroupBy(c => c.Recipient).Select(d => new { Recipient = d.Key, Amount = GetReimbursementOrRefundAmountInString(d.Sum(e => e.Amount)) }).ToList();
        //    recipientListView.Items.Clear();
        //    foreach (var d in data)
        //    {
        //        ListViewItem item = new ListViewItem();
        //        item.Text = string.IsNullOrEmpty(d.Recipient) ? _selectedEmployee.FullName : d.Recipient;
        //        item.SubItems.Add(d.Amount);
        //        recipientListView.Items.Add(item);
        //    }
        //}

        private void SetRecipientWiseReimbursementRefund(DataGridView gridView)
        {
            ICollection<AdvancePettyCashExpenseDetail> details = GetAdvanceExpenseDetailsFromGridView(gridView);

            List<RecipientWithReimbursementRefund> recipientWiseReimbursementRefund = new List<RecipientWithReimbursementRefund>();

            foreach (AdvancePettyCashExpenseDetail detail in details)
            {
                recipientWiseReimbursementRefund.Add(new RecipientWithReimbursementRefund { Recipient = detail.ReceipientOrPayeeName, Amount = detail.GetReimbursementOrRefundAmount() });
            }

            var data = recipientWiseReimbursementRefund.GroupBy(c => c.Recipient).Select(d => new { Recipient = d.Key, Amount = GetReimbursementOrRefundAmountInString(d.Sum(e => e.Amount)) }).ToList();
            recipientListView.Items.Clear();
            foreach (var d in data)
            {
                ListViewItem item = new ListViewItem();
                item.Text = string.IsNullOrEmpty(d.Recipient) ? _selectedEmployee.FullName : d.Recipient;
                item.SubItems.Add(d.Amount);
                recipientListView.Items.Add(item);
            }
        }

        private string GetReimbursementOrRefundAmountInString(decimal amount)
        {
            if (amount >= 0)
                return amount.ToString("N");
            return "(" + Math.Abs(amount).ToString("N") + ")";
        }

        //private void SetTotalAmountInListView(ListView listView)
        //{
        //    var details = GetAdvanceExpenseDetailsFromListView(listView);

        //    var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
        //    var totalExpenseAmount = details.Sum(c => c.ExpenseAmount);

        //    //var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
        //    var totalAdvanceAmountInBDT = details.Sum(c => c.GetAdvanceAmountInBdt());
        //    var totalExpenseAmountInBDT = details.Sum(c => c.GetExpenseAmountInBdt());

        //    ListViewItem item = new ListViewItem(string.Empty);
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(totalExpenseAmountInBDT.ToString("N"));
        //    item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
        //    item.SubItems.Add(GetReimburseAmount(totalExpenseAmount, totalAdvanceAmount));
        //    item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
        //    listView.Items.Add(item);

        //    SetRecipientWiseReimbursementRefund(listView);
        //}

        private void SetTotalAmountInGridView(DataGridView gridView)
        {
            var details = GetAdvanceExpenseDetailsFromGridView(gridView);

            var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
            var totalExpenseAmount = details.Sum(c => c.ExpenseAmount);

            //var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
            var totalAdvanceAmountInBDT = details.Sum(c => c.GetAdvanceAmountInBdt());
            var totalExpenseAmountInBDT = details.Sum(c => c.GetExpenseAmountInBdt());

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gridView);

            row.Cells[0] = new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();

            row.Cells[3].Value = "Total";
            row.Cells[4].Value = totalExpenseAmountInBDT.ToString("N");
            row.Cells[5].Value = totalAdvanceAmountInBDT.ToString("N");
            row.Cells[6].Value = GetReimburseAmount(totalExpenseAmount, totalAdvanceAmount);

            row.Cells[3].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[4].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[5].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[6].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);

            row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridView.Rows.Add(row);

            SetRecipientWiseReimbursementRefund(gridView);
        }

        //private void RemoveTotalAmountFromListView()
        //{
        //    if (expenseDetailsListView.Items.Count > 1)
        //    {
        //        int detailItem =
        //                expenseDetailsListView.Items.Count - 1;
        //        expenseDetailsListView.Items.RemoveAt(detailItem);
        //    }
        //}

        private void RemoveTotalAmountFromGridView()
        {
            if (expenseDetailsDataGridView.Rows.Count > 1)
            {
                int detailItem =
                        expenseDetailsDataGridView.Rows.Count - 1;
                expenseDetailsDataGridView.Rows.RemoveAt(detailItem);
            }
        }

        //private ListViewItem GetNewListViewItemByDetail(AdvancePettyCashExpenseDetail detail)
        //{
        //    ListViewItem item = new ListViewItem(_requisitionNo);
        //    item.SubItems.Add(detail.Purpose);
        //    item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
        //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //    string reimburse = detail.ExpenseAmount - detail.AdvanceAmount > 0
        //        ? (detail.ExpenseAmount - detail.AdvanceAmount).ToString("N")
        //        : "(" + (detail.AdvanceAmount - detail.ExpenseAmount).ToString("N") + ")";
        //    item.SubItems.Add(reimburse);
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItemByDetail(AdvancePettyCashExpenseDetail detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(expenseDetailsDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.Purpose;
            row.Cells[4].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[5].Value = detail.AdvanceAmount.ToString("N");
            string reimburse = detail.ExpenseAmount - detail.AdvanceAmount > 0
              ? (detail.ExpenseAmount - detail.AdvanceAmount).ToString("N")
              : "(" + (detail.AdvanceAmount - detail.ExpenseAmount).ToString("N") + ")";

            row.Cells[6].Value = reimburse;
            row.Cells[7].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[8].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Tag = detail;
            return row;
        }

        private void ResetDetailChangeMode()
        {
            addButton.Text = @"Add";
            _requisitionNo = string.Empty;
            _isDetailUpdateMode = false;
            _updateableDetailItem = null;
            saveButton.Visible = true;
            ClearDetailControl();
        }

        private bool IsRequisitionDetailsExist(AdvancePettyCashExpenseDetail detail)
        {
            ICollection<AdvancePettyCashExpenseDetail> detailList = GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
            if (detailList.Any(c => c.Purpose.Equals(detail.Purpose)))
            {
                string errorMessage = "Purpose already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }

        //private ListViewItem SetChangedListViewItemByDetail(AdvancePettyCashExpenseDetail detail, ListViewItem item)
        //{
        //    item.Text = _requisitionNo;
        //    item.SubItems[1].Text = detail.Purpose;
        //    item.SubItems[2].Text = detail.ExpenseAmount.ToString("N");
        //    item.SubItems[3].Text = detail.AdvanceAmount.ToString("N");
        //    string reimburse = detail.ExpenseAmount - detail.AdvanceAmount > 0
        //        ? (detail.ExpenseAmount - detail.AdvanceAmount).ToString("N")
        //        : "(" + (detail.AdvanceAmount - detail.ExpenseAmount).ToString("N") + ")";
        //    item.SubItems[4].Text = reimburse;
        //    item.SubItems[5].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[6].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(AdvancePettyCashExpenseDetail detail, DataGridViewRow row)
        {

            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.Purpose;
            row.Cells[4].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[5].Value = detail.AdvanceAmount.ToString("N");
            string reimburse = detail.ExpenseAmount - detail.AdvanceAmount > 0
                ? (detail.ExpenseAmount - detail.AdvanceAmount).ToString("N")
                : "(" + (detail.AdvanceAmount - detail.ExpenseAmount).ToString("N") + ")";

            row.Cells[6].Value = reimburse;
            row.Cells[7].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[8].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";

            row.Tag = detail;
            return row;
        }

        private bool ValidateAdd()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(purposeTextBox.Text))
            {
                errorMessage += "Purpose of requisition is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(conversionRateTextBox.Text))
            {
                errorMessage += "Conversion rate is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(expenseAmountTextBox.Text))
            {
                errorMessage += "Expense amount is not provided." + Environment.NewLine;
            }
            if (isThirdPartyReceipientCheckBox.Checked && string.IsNullOrEmpty(receipientOrPayeeNameTextBox.Text))
            {
                errorMessage += "Receipient/Payee name is not provided." + Environment.NewLine;
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

        private bool ValidateCeilingAmount()
        {
            if (_advanceCategory.IsCeilingApplicable)
            {
                decimal totalAmount;
                if (!string.IsNullOrEmpty(advanceAmountTextBox.Text))
                {
                    totalAmount = GetTotalAmount() + Convert.ToDecimal(advanceAmountTextBox.Text);
                }
                else
                {
                    totalAmount = GetTotalAmount();
                }
                if (totalAmount > _advanceCategory.CeilingAmount)
                {
                    return false;
                }
            }

            return true;
        }

        private decimal GetTotalAmount()
        {
            ICollection<AdvancePettyCashExpenseDetail> detailList =
                GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);

            if (detailList.Any())
            {
                return detailList.Sum(c => c.AdvanceAmount);
            }
            return 0;
        }

        private decimal GetTotalReimburseAmount()
        {
            ICollection<AdvancePettyCashExpenseDetail> detailList =
                GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);

            if (detailList.Any())
            {
                var totalAdvanceAmount = detailList.Sum(c => c.AdvanceAmount);
                var totalExpenseAmount = detailList.Sum(c => c.ExpenseAmount);
                return totalExpenseAmount - totalAdvanceAmount;
            }
            return 0;
        }

        private void advanceAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ExpenseAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDetailControl();
                _isUpdateMode = false;
                addButton.Text = @"Add";
                saveButton.Visible = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDetailControl()
        {
            vatTaxTypeComboBox.SelectedValue = DefaultItem.Value;
            purposeTextBox.Text = string.Empty;
            advanceAmountTextBox.Text = string.Empty;
            expenseAmountTextBox.Text = string.Empty;
            remarksTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Checked = false;
            receipientOrPayeeNameTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Enabled = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();

                AdvancePettyCashExpenseHeader advanceExpenseHeader = new AdvancePettyCashExpenseHeader();
                if (_isUpdateMode)
                {
                    advanceExpenseHeader.Id = _updateableExpenseHeader.Id;
                    advanceExpenseHeader.ExpenseNo = _updateableExpenseHeader.ExpenseNo;
                    advanceExpenseHeader.SerialNo = _updateableExpenseHeader.SerialNo;
                    advanceExpenseHeader.CreatedBy = _updateableExpenseHeader.CreatedBy;
                    advanceExpenseHeader.CreatedOn = _updateableExpenseHeader.CreatedOn;
                    advanceExpenseHeader.LastModifiedBy = Session.LoginUserName;
                    advanceExpenseHeader.LastModifiedOn = DateTime.Now;
                }
                else
                {
                    advanceExpenseHeader.CreatedBy = Session.LoginUserName;
                    advanceExpenseHeader.CreatedOn = DateTime.Now;
                }
                advanceExpenseHeader.RequesterUserName = _selectedEmployee.UserName;
                advanceExpenseHeader.RequesterDepartmentId = _selectedEmployee.DepartmentID;
                advanceExpenseHeader.RequesterRankId = _selectedEmployee.RankID;
                advanceExpenseHeader.RequesterSupervisorId = _selectedEmployee.SupervisorID;
                advanceExpenseHeader.ExpenseEntryDate = expenseDateTimePicker.Value;
                advanceExpenseHeader.AdvancePettyCashExpenseDetails = GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
                advanceExpenseHeader.AdvanceCategoryId = _advanceCategory.Id;
                advanceExpenseHeader.FromDate = fromDateTimePicker.Value;
                advanceExpenseHeader.ToDate = toDateTimePicker.Value;
                advanceExpenseHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                advanceExpenseHeader.Currency = currencyComboBox.Text;
                advanceExpenseHeader.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                if (_requisitionHeaders != null)
                {
                    advanceExpenseHeader.AdvancePettyCashRequisitionHeaders = _requisitionHeaders;
                    //todo: further update advance requisition headers with saved expense header id
                }
                advanceExpenseHeader.ExpenseFiles = _expenseFiles;
                if (!ValidateCeilingAmount())
                {
                    DialogResult result = MessageBox.Show(@"You are about to exceed the ceiling amount (" + _advanceCategory.CeilingAmount + @"). Do you want to continue?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!_isUpdateMode)
                {
                    bool isInserted = _advanceExpenseHeaderManager.Insert(advanceExpenseHeader);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Expense inserted successfully.", @"Success", MessageBoxButtons.OK,
                             MessageBoxIcon.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Expense insertion failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    advanceExpenseHeader.Id = _updateableExpenseHeader.Id;
                    bool isUpdated = _advanceExpenseHeaderManager.Edit(advanceExpenseHeader);

                    if (isUpdated)
                    {
                        MessageBox.Show(@"Requested expense updated successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Requested expense update failed.", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if (!expenseDateTimePicker.Checked)
            {
                errorMessage += "Requisition date is not provided." + Environment.NewLine;
            }
            if (!fromDateTimePicker.Checked)
            {
                errorMessage += "From date is not provided." + Environment.NewLine;
            }
            if (!toDateTimePicker.Checked)
            {
                errorMessage += "To date is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(noOfDaysTextBox.Text))
            {
                errorMessage += "No. of day(s) is not provided." + Environment.NewLine;
            }
            if (expenseDetailsDataGridView.Rows.Count < 1)
            {
                errorMessage += "Could not proceed further! As you have not added any requisition detail. " + Environment.NewLine;
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
            try
            {
                RemoveDetailItem();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RemoveDetailItem()
        {
            //if (expenseDetailsListView.SelectedItems.Count > 0)
            //{
            //    int detailItem =
            //        expenseDetailsListView.SelectedItems[0].Index;
            //    if (expenseDetailsListView.Items.Count == detailItem + 1)
            //    {
            //        throw new UiException("No item is selected to remove.");
            //    }
            //    AdvanceExpenseDetail detail = expenseDetailsListView.SelectedItems[0].Tag as AdvanceExpenseDetail;
            //    if (detail.AdvanceRequisitionDetail != null || detail.AdvanceRequisitionDetailId != null)
            //    {
            //        throw new UiException("You cannot remove this expense. You can only edit this expense.");
            //    }
            //    RemoveTotalAmountFromListView();
            //    expenseDetailsListView.Items.RemoveAt(detailItem);
            //    if (expenseDetailsListView.Items.Count >= 1)
            //    {
            //        SetTotalAmountInListView(expenseDetailsListView);
            //    }
            //}
        }

        private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void isThirdPartyReceipientCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                receipientOrPayeeNameTextBox.ReadOnly = !isThirdPartyReceipientCheckBox.Checked;
                if (!isThirdPartyReceipientCheckBox.Checked)
                {
                    receipientOrPayeeNameTextBox.Text = string.Empty;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void browseFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Filter = Utility.Utility.SupportedFileFormat,
                    Multiselect = true
                };
                DialogResult dialogResult = fileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    foreach (string fileName in fileDialog.FileNames)
                    {
                        string imageLocation = fileName;
                        string originalFileName = Path.GetFileName(imageLocation);
                        string newFileName = Utility.Utility.GenerateFileName(_selectedEmployee.UserName, originalFileName);
                        string fileUploadLocation = Utility.Utility.ExpenseFileUploadLocation;
                        if (!Directory.Exists(fileUploadLocation))
                        {
                            Directory.CreateDirectory(fileUploadLocation);
                        }
                        string newLocationAndFileName = fileUploadLocation + newFileName;
                        File.Copy(imageLocation, newLocationAndFileName);
                        ExpenseFile file = new ExpenseFile
                        {
                            FileLocation = newLocationAndFileName,
                            UploadedBy = Session.LoginUserName,
                            UploadedOn = DateTime.Now
                        };
                        _expenseFiles.Add(file);
                        SetUploadedFileNumberLabel();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetUploadedFileNumberLabel()
        {
            if (_expenseFiles != null)
            {
                if (_expenseFiles.Count >= 1)
                {
                    uploadedFileNumberLabel.Visible = true;
                }
                uploadedFileNumberLabel.Text = @"(" + _expenseFiles.Count(c => !c.IsDeleted) + @")";
            }
        }

        private void manageUploadedFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                ManageUploadedFilesUI manageUploadedFilesUi = new ManageUploadedFilesUI(_expenseFiles, _isUpdateMode);
                manageUploadedFilesUi.ShowDialog();
                SetUploadedFileNumberLabel();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_requisitionHeaders != null && _requisitionHeaders.Any())
                {
                    ICollection<RequisitionFile> files = _requisitionHeaders.SelectMany(c => c.RequisitionFiles).ToList();
                    ShowSupportingFilesUI showSupportingFilesUi = new ShowSupportingFilesUI(files);
                    showSupportingFilesUi.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"Requisition(s) not found.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseDetailsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (expenseDetailsListView.SelectedItems.Count > 0)
                //{
                //    editButton.Visible = true;
                //    removeButton.Visible = true;
                //}
                //else
                //{
                //    editButton.Visible = false;
                //    removeButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveDetailItem();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                EditDetailItem();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseDetailsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                EditDetailItem();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void recipientListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = recipientListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, recipientListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recipientListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            try
            {
                e.DrawDefault = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recipientListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            try
            {
                e.DrawDefault = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseDetailsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        AdvancePettyCashExpenseDetail detail = null;
                        if (expenseDetailsDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow row = expenseDetailsDataGridView.SelectedRows[0];
                            if (expenseDetailsDataGridView.Rows.Count == row.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = row.Tag as AdvancePettyCashExpenseDetail;
                            if (detail == null)
                            {
                                throw new UiException("Item is not tagged");
                            }
                            _updateableDetailItem = row;
                            _requisitionNo = (string) row.Cells[2].Value;
                            purposeTextBox.Text = detail.Purpose;
                            advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                            expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
                            remarksTextBox.Text = detail.Remarks;
                            isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
                            receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                            vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
                            if (detail.AdvanceRequisitionDetailId != null && detail.AdvanceRequisitionDetailId.Value > 0)
                            {
                                isThirdPartyReceipientCheckBox.Enabled = false;
                            }
                            SetFormToDetailChangeMode(row);
                        }
                        else
                        {
                            throw new UiException("No item is selected to edit.");
                        }
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        if (expenseDetailsDataGridView.SelectedRows.Count > 0)
                        {
                            int detailItem =
                                expenseDetailsDataGridView.SelectedRows[0].Index;
                            if (expenseDetailsDataGridView.Rows.Count == detailItem + 1)
                            {
                                throw new UiException("No item is selected to remove.");
                            }
                            AdvanceExpenseDetail detail = expenseDetailsDataGridView.SelectedRows[0].Tag as AdvanceExpenseDetail;
                            if (detail.AdvanceRequisitionDetail != null || detail.AdvanceRequisitionDetailId != null)
                            {
                                throw new UiException("You cannot remove this expense. You can only edit this expense.");
                            }
                            RemoveTotalAmountFromGridView();
                            expenseDetailsDataGridView.Rows.RemoveAt(detailItem);
                            if (expenseDetailsDataGridView.Rows.Count >= 1)
                            {
                                SetTotalAmountInGridView(expenseDetailsDataGridView);
                            }
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
