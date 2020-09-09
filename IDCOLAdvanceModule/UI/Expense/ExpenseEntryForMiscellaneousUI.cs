using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.Model.ViewModels;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.UI.Expense
{
    public partial class ExpenseEntryForMiscellaneousUI : Form
    {
        private readonly UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private IVendorManager _vendorManager;
        private readonly ICurrencyManager _currencyManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private bool _isUpdateMode;
        private bool _updateDetailMode;

        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly AdvanceMiscelleneousRequisitionHeader _requisitionHeader;
        private readonly AdvanceMiscelleaneousExpenseHeader _updateableExpenseHeader;
        private DataGridViewRow _updateDetailItem;
        private readonly ICollection<ExpenseFile> _expenseFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        private readonly ICollection<AdvanceMiscelleneousRequisitionHeader> _requisitionHeaders;
        private string _requisitionNo;

        public ExpenseEntryForMiscellaneousUI()
        {
            InitializeComponent();
            _expenseFiles = new List<ExpenseFile>();
            _vendorManager = new VendorManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _employeeManager = new EmployeeManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
            _requisitionHeaders = new List<AdvanceMiscelleneousRequisitionHeader>();
        }

        public ExpenseEntryForMiscellaneousUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public ExpenseEntryForMiscellaneousUI(ICollection<AdvanceMiscelleneousRequisitionHeader> requisitionHeaders)
            : this()
        {
            _requisitionHeaders = requisitionHeaders;
            DateTime fromDate = _requisitionHeaders.Min(c => c.FromDate);
            DateTime toDate = _requisitionHeaders.Max(c => c.ToDate);
            var advanceMiscelleneousRequisitionHeader = _requisitionHeaders.FirstOrDefault();
            if (advanceMiscelleneousRequisitionHeader != null)
            {
                long categoryId = advanceMiscelleneousRequisitionHeader.AdvanceCategoryId;
                _advanceCategory = _advanceRequisitionCategoryManager.GetById(categoryId);
                _selectedEmployee = _employeeManager.GetByUserName(advanceMiscelleneousRequisitionHeader.RequesterUserName);
            }
            if (_requisitionHeaders.Count == 1)
            {
                LoadHeaderInformationFromRequisition(advanceMiscelleneousRequisitionHeader);
            }
            else
            {
                LoadHeaderInformationFromRequisition(fromDate, toDate);
            }
            LoadDetailsInformationFromRequisition(_requisitionHeaders);
        }

        public ExpenseEntryForMiscellaneousUI(AdvanceMiscelleaneousExpenseHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableExpenseHeader = header;
            if (header.AdvanceMiscelleneousRequisitionHeaders != null &&
                header.AdvanceMiscelleneousRequisitionHeaders.Any())
            {
                foreach (AdvanceMiscelleneousRequisitionHeader requisitionHeader in header.AdvanceMiscelleneousRequisitionHeaders)
                {
                    _requisitionHeaders.Add(requisitionHeader);
                }
            }
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            LoadHeaderInformationInUpdateMode(header);
            LoadDetailsInformationInUpdateMode(header.AdvanceMiscelleaneousExpenseDetails.ToList());
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

        private void SetFormToUpdateMode()
        {
            _isUpdateMode = true;
            saveButton.Text = @"Update";
        }

        private void SetFormToViewMode()
        {
            expenseDateTime.Enabled = false;
            addButton.Visible = false;
            saveButton.Visible = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
            detailsAddGroupBox.Enabled = false;
            //detailsShowGroupBox.Enabled = false;
            // expenseDetailsListView.ContextMenuStrip = null;
        }

        private List<AdvanceMiscelleaneousExpenseDetail> GetMiscelleaneousExpenseDetail(
            ICollection<AdvanceMiscelleneousRequisitionDetail> details)
        {
            List<AdvanceMiscelleaneousExpenseDetail> detailList = new List<AdvanceMiscelleaneousExpenseDetail>();
            foreach (var detail in details)
            {
                AdvanceMiscelleaneousExpenseDetail expenseDetail = new AdvanceMiscelleaneousExpenseDetail();
                expenseDetail.Purpose = detail.Purpose;
                expenseDetail.NoOfUnit = detail.NoOfUnit;
                expenseDetail.UnitCost = detail.UnitCost;
                expenseDetail.AdvanceAmount = detail.AdvanceAmount;
                expenseDetail.ExpenseAmount = detail.AdvanceAmount;
                expenseDetail.Remarks = detail.Remarks;
                expenseDetail.AdvanceRequisitionDetailId = detail.Id;

                detailList.Add(expenseDetail);
            }
            return detailList;
        }

        private void LoadDetailsInformationFromRequisition(ICollection<AdvanceMiscelleneousRequisitionDetail> details)
        {
            //var expenseDetails = GetMiscelleaneousExpenseDetail(details);
            //foreach (AdvanceMiscelleaneousExpenseDetail detail in expenseDetails)
            //{
            //    ListViewItem item = new ListViewItem(_requisitionHeader.RequisitionNo);
            //    item.SubItems.Add(detail.Purpose);
            //    item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
            //    item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
            //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
            //    item.SubItems.Add(detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == "" ? 0 : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
            //    item.SubItems.Add(detail.GetReimbursementOrRefundAmount().ToString("N"));
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
            //    item.Tag = detail;
            //    expenseDetailsListView.Items.Add(item);
            //}

            //SetTotalAmountInListView(expenseDetailsListView);
        }

        //private void LoadDetailsInformationFromRequisition(ICollection<AdvanceMiscelleneousRequisitionHeader> requisitionHeaders)
        //{
        //    foreach (AdvanceMiscelleneousRequisitionHeader header in requisitionHeaders)
        //    {
        //        foreach (AdvanceMiscelleneousRequisitionDetail detail in header.AdvanceMiscelleneousRequisitionDetails)
        //        {
        //            ListViewItem item = new ListViewItem(header.RequisitionNo);
        //            item.SubItems.Add(detail.Purpose);
        //            item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //            item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //            item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
        //            item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
        //            item.SubItems.Add((detail.GetAdvanceAmountInBdt() - detail.GetAdvanceAmountInBdt()).ToString("N"));
        //            item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //            item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //            AdvanceMiscelleaneousExpenseDetail advancePettyCashExpenseDetail = new AdvanceMiscelleaneousExpenseDetail
        //            {
        //                AdvanceRequisitionDetailId = detail.Id,
        //                AdvanceAmount = detail.AdvanceAmount,
        //                ExpenseAmount = detail.AdvanceAmount,
        //                Purpose = detail.Purpose,
        //                Remarks = detail.Remarks,
        //                IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
        //                ReceipientOrPayeeName = detail.ReceipientOrPayeeName
        //            };
        //            item.Tag = advancePettyCashExpenseDetail;

        //            expenseDetailsListView.Items.Add(item);
        //        }
        //    }
        //    SetTotalAmountInListView(expenseDetailsListView);
        //}


        private void LoadDetailsInformationFromRequisition(ICollection<AdvanceMiscelleneousRequisitionHeader> requisitionHeaders)
        {
            foreach (AdvanceMiscelleneousRequisitionHeader header in requisitionHeaders)
            {
                foreach (AdvanceMiscelleneousRequisitionDetail detail in header.AdvanceMiscelleneousRequisitionDetails)
                {


                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseDetailsDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = header.RequisitionNo;
                    row.Cells[3].Value = detail.Purpose;
                    row.Cells[4].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                        ? detail.NoOfUnit.Value.ToString("N")
                        : "N/A";
                    row.Cells[5].Value = detail.UnitCost != null && detail.UnitCost > 0
                        ? detail.UnitCost.Value.ToString("N")
                        : "N/A";

                    row.Cells[6].Value = detail.GetAdvanceAmountInBdt().ToString("N");
                    row.Cells[7].Value = detail.GetAdvanceAmountInBdt().ToString("N");
                    row.Cells[8].Value = (detail.GetAdvanceAmountInBdt() - detail.GetAdvanceAmountInBdt()).ToString("N");
                    row.Cells[9].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                    row.Cells[10].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                        ? detail.ReceipientOrPayeeName
                        : "N/A";
                    AdvanceMiscelleaneousExpenseDetail advancePettyCashExpenseDetail = new AdvanceMiscelleaneousExpenseDetail
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
        private void LoadDetailsInformationInUpdateMode(ICollection<AdvanceMiscelleaneousExpenseDetail> expenseDetails)
        {
            foreach (var detail in expenseDetails)
            {
                ListViewItem item = new ListViewItem();
                if (detail.AdvanceMiscelleneousRequisitionDetail != null)
                {
                    item.Text =
                        detail.AdvanceMiscelleneousRequisitionDetail.AdvanceMiscelleneousRequisitionHeader.RequisitionNo;
                }
                else
                {
                    item.Text = string.Empty;
                }
                item.SubItems.Add(detail.Purpose);
                item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
                item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
                item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                item.SubItems.Add(GetReimburseAmount(detail.ExpenseAmount, detail.AdvanceAmount));
                item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
                item.Tag = detail;
                expenseDetailsDataGridView.Rows.Add(item);
            }

            SetTotalAmountInGridView(expenseDetailsDataGridView);
        }

        //private ICollection<AdvanceMiscelleaneousExpenseDetail> GetAdvanceExpenseDetailsFromListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = new List<AdvanceMiscelleaneousExpenseDetail>();
        //    if (advanceDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in advanceDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvanceMiscelleaneousExpenseDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private ICollection<AdvanceMiscelleaneousExpenseDetail> GetAdvanceExpenseDetailsFromGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = new List<AdvanceMiscelleaneousExpenseDetail>();
            if (advanceDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in advanceDetailsGridViewControl.Rows)
                {
                    var detailItem = row.Tag as AdvanceMiscelleaneousExpenseDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
        }
        private void LoadHeaderInformationFromRequisition(AdvanceMiscelleneousRequisitionHeader header)
        {
            purposeTextBox.Text = header.Purpose;
            placeOfEventTextBox.Text = header.PlaceOfEvent;
            //sourceOfFundTextBox.Text = header.SourceOfFund;
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            expenseDateTime.Value = header.RequisitionDate;
            expenseDateTime.Checked = true;
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

        private void LoadHeaderInformationInUpdateMode(AdvanceMiscelleaneousExpenseHeader header)
        {
            purposeTextBox.Text = header.Purpose;
            placeOfEventTextBox.Text = header.PlaceOfEvent;
            //sourceOfFundTextBox.Text = header.SourceOfFund;
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            expenseDateTime.Value = header.ExpenseEntryDate;
            expenseDateTime.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
        }

        private void ExpenseEntryForMiscellaneousUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseDateTime.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadCurrencyComboBox();
                LoadVatTaxTypeComboBox();
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
                SetFormTitleAndText();
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
            //        throw new UiException("You cannot remove this expense. You can only edit.");
            //    }
            //    RemoveTotalAmountFromListView();
            //    expenseDetailsListView.Items.RemoveAt(detailItem);
            //    if (expenseDetailsListView.Items.Count >= 1)
            //    {
            //        SetTotalAmountInListView(expenseDetailsListView);
            //    }
            //}
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
            //AdvanceMiscelleaneousExpenseDetail detail = null;
            //if (expenseDetailsListView.SelectedItems.Count > 0)
            //{
            //    ListViewItem item = expenseDetailsListView.SelectedItems[0];
            //    if (expenseDetailsListView.Items.Count == item.Index + 1)
            //    {
            //        throw new UiException("No item is selected to edit.");
            //    }
            //    detail = item.Tag as AdvanceMiscelleaneousExpenseDetail;
            //    if (detail == null)
            //    {
            //        throw new UiException("Item is not tagged.");
            //    }

            //    _requisitionNo = item.Text;
            //    particularsTextBox.Text = detail.Purpose;
            //    noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
            //    unitCostTextBox.Text = detail.UnitCost.ToString();
            //    remarksTextBox.Text = detail.Remarks;
            //    receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
            //    advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
            //    expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
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

        private void SetFormToDetailChangeMode(DataGridViewRow row)
        {
            if (row == null)
            {
                throw new UiException("There is no data found for changing.");
            }
            addButton.Text = @"Change";
            _updateDetailItem = row;
            _updateDetailMode = true;
            saveButton.Visible = false;
        }

        private decimal GetTotalAmount()
        {
            ICollection<AdvanceMiscelleaneousExpenseDetail> detailList =
                GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);

            if (detailList.Any())
            {
                return detailList.Sum(c => c.AdvanceAmount);
            }
            return 0;
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

        private bool ValidateAdd()
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(particularsTextBox.Text))
            {
                errorMessage += "Particulars is not provided." + Environment.NewLine;
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

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();

                AdvanceMiscelleaneousExpenseDetail detail = new AdvanceMiscelleaneousExpenseDetail();
                detail.Purpose = particularsTextBox.Text;

                if (!string.IsNullOrEmpty(unitCostTextBox.Text))
                {
                    detail.UnitCost = Convert.ToDecimal(unitCostTextBox.Text);
                }
                if (!string.IsNullOrEmpty(noOfUnitTextBox.Text))
                {
                    detail.NoOfUnit = Convert.ToDouble(noOfUnitTextBox.Text);
                }
                if (!string.IsNullOrEmpty(advanceAmountTextBox.Text))
                {
                    detail.AdvanceAmount = Convert.ToDecimal(advanceAmountTextBox.Text);
                }
                if (!string.IsNullOrEmpty(expenseAmountTextBox.Text))
                {
                    detail.ExpenseAmount = Convert.ToDecimal(expenseAmountTextBox.Text);
                }
                if (!string.IsNullOrEmpty(remarksTextBox.Text))
                {
                    detail.Remarks = remarksTextBox.Text;
                }
                if (!string.IsNullOrEmpty(receipientOrPayeeNameTextBox.Text))
                {
                    detail.ReceipientOrPayeeName = receipientOrPayeeNameTextBox.Text;
                }
                detail.IsThirdPartyReceipient = isThirdPartyReceipientCheckBox.Checked;
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
                if (_updateDetailMode)
                {
                    if (_updateDetailItem == null)
                    {
                        throw new UiException("There is no item found to update!");
                    }
                    var item = _updateDetailItem;
                    var updateItemDetail = item.Tag as AdvanceMiscelleaneousExpenseDetail;
                    _requisitionNo = (string) item.Cells[2].Value;
                    updateItemDetail.NoOfUnit = detail.NoOfUnit;
                    updateItemDetail.Purpose = detail.Purpose;
                    updateItemDetail.Remarks = detail.Remarks;
                    updateItemDetail.UnitCost = detail.UnitCost;
                    updateItemDetail.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;
                    updateItemDetail.AdvanceAmount = detail.AdvanceAmount;
                    updateItemDetail.ExpenseAmount = detail.ExpenseAmount;
                    updateItemDetail.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;

                    RemoveTotalAmountFromListView();
                    SetChangedGridViewItemByDetail(updateItemDetail, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    //IsRequisitionDetailsExist(detail);
                    var item = GetNewGridViewItemByDetail(detail);
                    RemoveTotalAmountFromListView();
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
        //    ICollection<AdvanceMiscelleaneousExpenseDetail> details = GetAdvanceExpenseDetailsFromListView(listView);

        //    List<RecipientWithReimbursementRefund> recipientWiseReimbursementRefund = new List<RecipientWithReimbursementRefund>();

        //    foreach (AdvanceMiscelleaneousExpenseDetail detail in details)
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
            ICollection<AdvanceMiscelleaneousExpenseDetail> details = GetAdvanceExpenseDetailsFromGridView(gridView);

            List<RecipientWithReimbursementRefund> recipientWiseReimbursementRefund = new List<RecipientWithReimbursementRefund>();

            foreach (AdvanceMiscelleaneousExpenseDetail detail in details)
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

        //    ListViewItem item = new ListViewItem();
        //    item.Text = string.Empty;
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(totalExpenseAmountInBDT.ToString("N"));
        //    item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
        //    item.SubItems.Add(GetReimburseAmount(totalExpenseAmountInBDT, totalAdvanceAmountInBDT));
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
            row.Cells[4].Value = @"Total";
            row.Cells[5].Value = totalExpenseAmountInBDT.ToString("N");
            row.Cells[6].Value = totalAdvanceAmountInBDT.ToString("N");
            row.Cells[7].Value = GetReimburseAmount(totalExpenseAmountInBDT, totalAdvanceAmountInBDT);

            row.Cells[4].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[5].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[6].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[7].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);

            row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

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

        private void RemoveTotalAmountFromListView()
        {
            if (expenseDetailsDataGridView.Rows.Count > 1)
            {
                int detailItem =
                        expenseDetailsDataGridView.Rows.Count - 1;
                expenseDetailsDataGridView.Rows.RemoveAt(detailItem);
            }
        }

        //private ListViewItem GetNewListViewItemByDetail(AdvanceMiscelleaneousExpenseDetail detail)
        //{
        //    ListViewItem item = new ListViewItem(_requisitionNo);
        //    item.SubItems.Add(detail.Purpose);
        //    item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.UnitCost != 0 ? detail.UnitCost.ToString() : "N/A");
        //    item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
        //    item.SubItems.Add(detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == "" ? 0 : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
        //    item.SubItems.Add(detail.GetReimbursementOrRefundAmount().ToString("N"));
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItemByDetail(AdvanceMiscelleaneousExpenseDetail detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(expenseDetailsDataGridView);


            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.Purpose;
            row.Cells[4].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A";
            row.Cells[5].Value = detail.UnitCost != 0 ? detail.UnitCost.ToString() : "N/A";
            row.Cells[6].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[7].Value = detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == "" ? 0 : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N");
            row.Cells[8].Value = detail.GetReimbursementOrRefundAmount().ToString("N");
            row.Cells[9].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[10].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";

            row.Tag = detail;
        
            return row;
        }

        //private ListViewItem SetChangedListViewItemByDetail(AdvanceMiscelleaneousExpenseDetail detail, ListViewItem item)
        //{
        //    item.Text = _requisitionNo;
        //    item.SubItems[1].Text = detail.Purpose;
        //    item.SubItems[2].Text = (detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //    item.SubItems[3].Text = (detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //    item.SubItems[4].Text = (detail.ExpenseAmount.ToString("N"));
        //    item.SubItems[5].Text = (detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == "" ? 0 : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
        //    string reimburse = detail.ExpenseAmount - detail.AdvanceAmount >= 0
        //        ? (detail.ExpenseAmount - detail.AdvanceAmount).ToString("N")
        //        : "(" + (detail.AdvanceAmount - detail.ExpenseAmount).ToString("N") + ")";
        //    item.SubItems[6].Text = reimburse;
        //    item.SubItems[7].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[8].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(AdvanceMiscelleaneousExpenseDetail detail, DataGridViewRow row)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.Purpose;
            row.Cells[4].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                ? detail.NoOfUnit.Value.ToString("N")
                : "N/A";
            row.Cells[5].Value = detail.UnitCost != null && detail.UnitCost > 0
                ? detail.UnitCost.Value.ToString("N")
                : "N/A";
            row.Cells[6].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[7].Value = detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == ""
                ? 0
                : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N");
            string reimburse = detail.ExpenseAmount - detail.AdvanceAmount >= 0
                ? (detail.ExpenseAmount - detail.AdvanceAmount).ToString("N")
                : "(" + (detail.AdvanceAmount - detail.ExpenseAmount).ToString("N") + ")";
            row.Cells[8].Value = reimburse;
            row.Cells[9].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[10].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";

            return row;
        }

        private string GetReimburseAmount(decimal expenseAmount, decimal advanceAmount)
        {
            var reimburse = (expenseAmount - advanceAmount);
            if (reimburse >= 0)
                return reimburse.ToString("N");
            return "(" + Math.Abs(reimburse) + ")";
        }

        private bool IsRequisitionDetailsExist(AdvanceMiscelleaneousExpenseDetail detail)
        {
            ICollection<AdvanceMiscelleaneousExpenseDetail> detailList = GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
            if (detailList.Any(c => c.Purpose.Equals(detail.Purpose)))
            {
                string errorMessage = "Purpose already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }

        private void ClearDetailControl()
        {
            vatTaxTypeComboBox.SelectedValue = DefaultItem.Value;
            particularsTextBox.Text = string.Empty;
            Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
            unitCostTextBox.Text = string.Empty;
            expensePayableTextBox.Text = string.Empty;
            advanceAmountTextBox.Text = string.Empty;
            expenseAmountTextBox.Text = string.Empty;
            remarksTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Checked = false;
            receipientOrPayeeNameTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Enabled = true;
        }

        private void ResetDetailChangeMode()
        {
            addButton.Text = @"Add";
            _requisitionNo = string.Empty;
            _updateDetailItem = null;
            _updateDetailMode = false;
            saveButton.Visible = true;
            ClearDetailControl();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                AdvanceMiscelleaneousExpenseHeader miscelleaneousExpenseHeader = new AdvanceMiscelleaneousExpenseHeader();
                if (_isUpdateMode)
                {
                    miscelleaneousExpenseHeader.Id = _updateableExpenseHeader.Id;
                    miscelleaneousExpenseHeader.ExpenseNo = _updateableExpenseHeader.ExpenseNo;
                    miscelleaneousExpenseHeader.SerialNo = _updateableExpenseHeader.SerialNo;
                    miscelleaneousExpenseHeader.CreatedBy = _updateableExpenseHeader.CreatedBy;
                    miscelleaneousExpenseHeader.CreatedOn = _updateableExpenseHeader.CreatedOn;
                    miscelleaneousExpenseHeader.LastModifiedBy = Session.LoginUserName;
                    miscelleaneousExpenseHeader.LastModifiedOn = DateTime.Now;
                }
                else
                {
                    miscelleaneousExpenseHeader.CreatedBy = Session.LoginUserName;
                    miscelleaneousExpenseHeader.CreatedOn = DateTime.Now;
                }
                miscelleaneousExpenseHeader.AdvanceCategoryId = _advanceCategory.Id;
                miscelleaneousExpenseHeader.Currency = currencyComboBox.Text;
                miscelleaneousExpenseHeader.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                miscelleaneousExpenseHeader.ExpenseEntryDate = expenseDateTime.Value;
                miscelleaneousExpenseHeader.RequesterUserName = _selectedEmployee.UserName;
                miscelleaneousExpenseHeader.RequesterDepartmentId = _selectedEmployee.DepartmentID;
                miscelleaneousExpenseHeader.RequesterRankId = _selectedEmployee.RankID;
                miscelleaneousExpenseHeader.RequesterSupervisorId = _selectedEmployee.SupervisorID;
                miscelleaneousExpenseHeader.PlaceOfEvent = placeOfEventTextBox.Text;
                miscelleaneousExpenseHeader.FromDate = fromDateTimePicker.Value;
                miscelleaneousExpenseHeader.ToDate = toDateTimePicker.Value;
                miscelleaneousExpenseHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                miscelleaneousExpenseHeader.Purpose = purposeTextBox.Text;
                miscelleaneousExpenseHeader.AdvanceMiscelleaneousExpenseDetails = GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
                miscelleaneousExpenseHeader.ExpenseFiles = _expenseFiles;
                if (_requisitionHeaders != null)
                {
                    miscelleaneousExpenseHeader.AdvanceMiscelleneousRequisitionHeaders = _requisitionHeaders;
                }
                if (!_isUpdateMode)
                {
                    bool isInserted = _advanceExpenseHeaderManager.Insert(miscelleaneousExpenseHeader);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Expnse requested successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Expense request failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool isUpdated = _advanceExpenseHeaderManager.Edit(miscelleaneousExpenseHeader);

                    if (isUpdated)
                    {
                        MessageBox.Show(@"Requested expense update successful.", @"Success", MessageBoxButtons.OK,
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
            if (!expenseDateTime.Checked)
            {
                errorMessage += "Requisition date is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(purposeTextBox.Text))
            {
                errorMessage += "Purpose is not provided." + Environment.NewLine;
            }
            if (_advanceCategory.Id == (long)AdvanceCategoryEnum.Event ||
                _advanceCategory.Id == (long)AdvanceCategoryEnum.TrainingAndWorkshop ||
                _advanceCategory.Id == (long)AdvanceCategoryEnum.Meeting)
            {
                if (string.IsNullOrEmpty(placeOfEventTextBox.Text))
                {
                    errorMessage += "Place of event is not provided." + Environment.NewLine;
                }
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

        private void noOfUnitTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void unitCostTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void expenseTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void unitCostTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                SetExpenseAmounts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void unitCostTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetExpenseAmounts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void noOfUnitTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetExpenseAmounts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetExpenseAmounts()
        {
            var expensePayable = GetExpensePayable();
            expensePayableTextBox.Text = expensePayable.ToString();
            if (expensePayable > 0)
            {
                expenseAmountTextBox.Text = expensePayable.ToString();
            }
        }

        private double GetExpensePayable()
        {
            double noOfUnit = 0;
            double unitCost = 0;

            if (!String.IsNullOrEmpty(noOfUnitTextBox.Text))
            {
                noOfUnit = Convert.ToDouble(noOfUnitTextBox.Text);
            }

            if (!String.IsNullOrEmpty(unitCostTextBox.Text))
            {
                unitCost = Convert.ToDouble(unitCostTextBox.Text);
            }

            return (noOfUnit * unitCost);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetDetailChangeMode();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        AdvanceMiscelleaneousExpenseDetail detail = null;
                        if (expenseDetailsDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow row = expenseDetailsDataGridView.SelectedRows[0];
                            if (expenseDetailsDataGridView.Rows.Count == row.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = row.Tag as AdvanceMiscelleaneousExpenseDetail;
                            if (detail == null)
                            {
                                throw new UiException("Item is not tagged.");
                            }

                            _requisitionNo = (string) row.Cells[2].Value;
                            particularsTextBox.Text = detail.Purpose;
                            noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
                            unitCostTextBox.Text = detail.UnitCost.ToString();
                            remarksTextBox.Text = detail.Remarks;
                            receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                            advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                            expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
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
                                throw new UiException("You cannot remove this expense. You can only edit.");
                            }
                            RemoveTotalAmountFromListView();
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
