using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.UI.Entry
{
    public partial class RequisitionEntryForCorporateAdvisoryUI : Form
    {
        private UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private bool _isUpdateMode;
        private bool _updateDetailMode;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly AdvanceCorporateAdvisoryRequisitionHeader _updateableRequisitionHeader;
        private DataGridViewRow _updateDetailItem;
        private readonly ICollection<RequisitionFile> _requisitionFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        public RequisitionEntryForCorporateAdvisoryUI()
        {
            InitializeComponent();
            _requisitionFiles = new List<RequisitionFile>();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _employeeManager = new EmployeeManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
        }
        public RequisitionEntryForCorporateAdvisoryUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public RequisitionEntryForCorporateAdvisoryUI(AdvanceCorporateAdvisoryRequisitionHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableRequisitionHeader = header;
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            LoadHeaderInformation(header);
            LoadDetailsInformation(header.AdvanceCorporateAdvisoryRequisitionDetails.ToList());
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            _requisitionFiles = header.RequisitionFiles;
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

        private void SetCategoryWiseFormText()
        {
            if (_advanceCategory == null)
            {
                throw new UiException("Category not found.");
            }
            string title = " Advance Cash Requisition Entry Form (" + _advanceCategory.Name + ")";
            Text = title;
            titleLabel.Text = title;
        }

        private void LoadHeaderInformation(AdvanceCorporateAdvisoryRequisitionHeader header)
        {
            purposeTextBox.Text = header.Purpose;
            placeOfEventTextBox.Text = header.CorporateAdvisoryPlaceOfEvent;
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            requisitionDateTime.Value = header.RequisitionDate;
            requisitionDateTime.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
            noOfUnitHeaderTextBox.Text = header.NoOfUnit.ToString();
            unitCostHeaderTextBox.Text = header.UnitCost.ToString();
            totalRevenueTextBox.Text = header.TotalRevenue == null ? string.Empty : header.TotalRevenue.ToString();
            corporateAdvisoryRemarksTextBox.Text = header.AdvanceCorporateRemarks ?? string.Empty;
        }

        //private void LoadDetailsInformation(ICollection<AdvanceCorporateAdvisoryRequisitionDetail> details)
        //{
        //    foreach (AdvanceCorporateAdvisoryRequisitionDetail detail in details)
        //    {
        //        ListViewItem item = new ListViewItem(detail.Purpose);
        //        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //        item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //        item.Tag = detail;
        //        advanceDetailsListView.Items.Add(item);
        //    }

        //    SetTotalAmountInListView(advanceDetailsListView);
        //}

        private void LoadDetailsInformation(ICollection<AdvanceCorporateAdvisoryRequisitionDetail> details)
        {
            foreach (AdvanceCorporateAdvisoryRequisitionDetail detail in details)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(advanceDetailsDataGridView);

                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = "Remove";
                row.Cells[2].Value = detail.Purpose;
                row.Cells[3].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                    ? detail.NoOfUnit.Value.ToString("N")
                    : "N/A";
                row.Cells[4].Value = detail.UnitCost != null && detail.UnitCost > 0
                    ? detail.UnitCost.Value.ToString("N")
                    : "N/A";
                row.Cells[5].Value = detail.GetAdvanceAmountInBdt().ToString("N");
                row.Cells[6].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                row.Cells[7].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                    ? detail.ReceipientOrPayeeName
                    : "N/A";
                row.Tag = detail;
                advanceDetailsDataGridView.Rows.Add(row);
            }

            SetTotalAmountInGridView(advanceDetailsDataGridView);
        }
        private void SetUploadedFileNumberLabel()
        {
            if (_requisitionFiles != null)
            {
                if (_requisitionFiles.Count >= 1)
                {
                    uploadedFileNumberLabel.Visible = true;
                }
                uploadedFileNumberLabel.Text = @"(" + _requisitionFiles.Count(c => !c.IsDeleted) + @")";
            }
        }
        private void SetFormToUpdateMode()
        {
            _isUpdateMode = true;
            saveButton.Text = @"Update";
        }

        private void SetFormToViewMode()
        {
            requisitionDateTime.Enabled = false;
            addButton.Visible = false;
            saveButton.Visible = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
            detailsAddGroupBox.Enabled = false;
            //advanceDetailsListView.ContextMenuStrip = null;
        }

        private void RequisitionEntryForAdvanceCorporateUI_Load(object sender, EventArgs e)
        {
            try
            {
                requisitionDateTime.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadVatTaxTypeComboBox();
                LoadCurrencyComboBox();
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitHeaderTextBox);
                if (_updateableRequisitionHeader != null)
                {
                    LoadHeaderInformation(_updateableRequisitionHeader);
                }
                SetCategoryWiseFormText();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadEmployeeInformation()
        {
            try
            {
                if (_selectedEmployee != null)
                {
                    designationTextBox.Text = _selectedEmployee.Admin_Rank == null ? "N/A" : _selectedEmployee.Admin_Rank.RankName;
                    employeeNameTextBox.Text = _selectedEmployee.FullName;
                    employeeIdTextBox.Text = _selectedEmployee.EmployeeID;
                    departmentTextBox.Text = _selectedEmployee.Admin_Departments == null ? "N/A" : _selectedEmployee.Admin_Departments.DepartmentName;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();

                AdvanceCorporateAdvisoryRequisitionDetail detail = new AdvanceCorporateAdvisoryRequisitionDetail();
                detail.Purpose = particularsTextBox.Text;
                if (!string.IsNullOrEmpty(unitCostTextBox.Text))
                {
                    detail.UnitCost = Convert.ToDecimal(unitCostTextBox.Text);
                }
                if (!string.IsNullOrEmpty(noOfUnitTextBox.Text))
                {
                    detail.NoOfUnit = Convert.ToDouble(noOfUnitTextBox.Text);
                }
                detail.AdvanceAmount = Convert.ToDecimal(advanceAmountTextBox.Text);
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

                if (_updateDetailMode)
                {
                    if (_updateDetailItem == null)
                    {
                        throw new UiException("There is no item found to update!");
                    }
                    var item = _updateDetailItem;
                    var updateItemDetail = item.Tag as AdvanceCorporateAdvisoryRequisitionDetail;

                    updateItemDetail.Purpose = detail.Purpose;
                    updateItemDetail.NoOfUnit = detail.NoOfUnit;
                    updateItemDetail.UnitCost = detail.UnitCost;
                    updateItemDetail.Remarks = detail.Remarks;
                    updateItemDetail.AdvanceAmount = detail.AdvanceAmount;
                    updateItemDetail.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;
                    updateItemDetail.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;
                    updateItemDetail.VatTaxTypeId = detail.VatTaxTypeId;
                    RemoveTotalAmountFromGridView();
                    SetGridViewItemByDetail(updateItemDetail, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    var item = GettNewGridViewItemByDetail(detail);
                    RemoveTotalAmountFromGridView();
                    advanceDetailsDataGridView.Rows.Add(item);
                }

                SetTotalAmountInGridView(advanceDetailsDataGridView);
                ClearDetailControl();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateAdd()
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(particularsTextBox.Text))
            {
                errorMessage += "Particulars is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(advanceAmountTextBox.Text))
            {
                errorMessage += "Advance amount is not provided." + Environment.NewLine;
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

        //private static ListViewItem SetListViewItemByDetail(AdvanceCorporateAdvisoryRequisitionDetail detail, ListViewItem item)
        //{
        //    item.Text = detail.Purpose;
        //    item.SubItems[1].Text = detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A";
        //    item.SubItems[2].Text = detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A";
        //    item.SubItems[3].Text = detail.AdvanceAmount.ToString("N");
        //    item.SubItems[4].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[5].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}

        private static DataGridViewRow SetGridViewItemByDetail(AdvanceCorporateAdvisoryRequisitionDetail detail, DataGridViewRow row)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.Purpose;
            row.Cells[3].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                ? detail.NoOfUnit.Value.ToString("N")
                : "N/A";
            row.Cells[4].Value = detail.UnitCost != null && detail.UnitCost > 0
                ? detail.UnitCost.Value.ToString("N")
                : "N/A";
            row.Cells[5].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[6].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[7].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Tag = detail;
            return row;
        }

        // private void RemoveTotalAmountFromListView()
        //{
        //    if (advanceDetailsListView.Items.Count > 1)
        //    {
        //        int detailItem =
        //                advanceDetailsListView.Items.Count - 1;
        //        advanceDetailsListView.Items.RemoveAt(detailItem);
        //    }
        //}

        private void RemoveTotalAmountFromGridView()
        {
            if (advanceDetailsDataGridView.Rows.Count > 1)
            {
                int detailItem =
                        advanceDetailsDataGridView.Rows.Count - 1;
                advanceDetailsDataGridView.Rows.RemoveAt(detailItem);
            }
        }
        private void ResetDetailChangeMode()
        {
            addButton.Text = @"Add";
            _updateDetailItem = null;
            _updateDetailMode = false;
            saveButton.Visible = true;
            ClearDetailControl();
        }

        private void ClearDetailControl()
        {
            vatTaxTypeComboBox.SelectedValue = DefaultItem.Value;
            particularsTextBox.Text = string.Empty;
            Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
            unitCostTextBox.Text = string.Empty;
            advancePayableTextBox.Text = string.Empty;
            advanceAmountTextBox.Text = string.Empty;
            remarksTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Checked = false;
            receipientOrPayeeNameTextBox.Text = string.Empty;
            addButton.Text = @"Add";
        }

        //private static ListViewItem GettNewListViewItemByDetail(AdvanceCorporateAdvisoryRequisitionDetail detail)
        //{
        //    ListViewItem item = new ListViewItem(detail.Purpose);
        //    item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.Tag = detail;
        //    return item;
        //}

        private  DataGridViewRow GettNewGridViewItemByDetail(AdvanceCorporateAdvisoryRequisitionDetail detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(advanceDetailsDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.Purpose;
            row.Cells[3].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                ? detail.NoOfUnit.Value.ToString("N")
                : "N/A";
            row.Cells[4].Value = detail.UnitCost != null && detail.UnitCost > 0
                ? detail.UnitCost.Value.ToString("N")
                : "N/A";
            row.Cells[5].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[6].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[7].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Tag = detail;
            return row;
        }
        //private void SetTotalAmountInListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = GetAdvanceRequisitionDetailsFromListView(advanceDetailsListViewControl);

        //    var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
        //    var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
        //    var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;

        //    ListViewItem item = new ListViewItem();
        //    item.Text = string.Empty;
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
        //    item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
        //    advanceDetailsListViewControl.Items.Add(item);
        //}

        private void SetTotalAmountInGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = GetAdvanceRequisitionDetailsFromGridView(advanceDetailsGridViewControl);

            var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
            var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
            var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(advanceDetailsGridViewControl);

            row.Cells[0] = new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();


            row.Cells[4].Value = @"Total";
            row.Cells[5].Value = totalAdvanceAmountInBDT.ToString("N");
            row.Cells[4].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[5].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            advanceDetailsGridViewControl.Rows.Add(row);
        }

        //private ICollection<AdvanceCorporateAdvisoryRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = new List<AdvanceCorporateAdvisoryRequisitionDetail>();
        //    if (advanceDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in advanceDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvanceCorporateAdvisoryRequisitionDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private ICollection<AdvanceCorporateAdvisoryRequisitionDetail> GetAdvanceRequisitionDetailsFromGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = new List<AdvanceCorporateAdvisoryRequisitionDetail>();
            if (advanceDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in advanceDetailsGridViewControl.Rows)
                {
                    var detailItem = row.Tag as AdvanceCorporateAdvisoryRequisitionDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
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

        private void noOfUnitTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AdvanceAmountCalculation();
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

        private void unitCostTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AdvanceAmountCalculation();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void AdvanceAmountCalculation()
        {
            if (!string.IsNullOrEmpty(noOfUnitTextBox.Text) && !string.IsNullOrEmpty(unitCostTextBox.Text))
            {
                decimal noOfUnit = Convert.ToDecimal(noOfUnitTextBox.Text);
                decimal unitCost = Convert.ToDecimal(unitCostTextBox.Text);
                decimal advancePayable = noOfUnit * unitCost;
                advanceAmountTextBox.Text = advancePayable.ToString();
                advancePayableTextBox.Text = advancePayable.ToString();
            }
            else
            {
                advanceAmountTextBox.Text = string.Empty;
                advancePayableTextBox.Text = string.Empty;
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

        private void unitCostHeaderTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void unitCostHeaderTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TotalRevenueCalculation();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void noOfUnitHeaderTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void noOfUnitHeaderTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TotalRevenueCalculation();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TotalRevenueCalculation()
        {
            if (!string.IsNullOrEmpty(noOfUnitHeaderTextBox.Text) && !string.IsNullOrEmpty(unitCostHeaderTextBox.Text))
            {
                decimal noOfUnit = Convert.ToDecimal(noOfUnitHeaderTextBox.Text);
                decimal unitCost = Convert.ToDecimal(unitCostHeaderTextBox.Text);
                decimal totalRevenue = noOfUnit * unitCost;
                totalRevenueTextBox.Text = totalRevenue.ToString();
            }
            else
            {
                totalRevenueTextBox.Text = string.Empty;
            }
        }

        private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitHeaderTextBox);
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
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitHeaderTextBox);

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
                        string fileUploadLocation = Utility.Utility.RequisitionFileUploadLocation;
                        if (!Directory.Exists(fileUploadLocation))
                        {
                            Directory.CreateDirectory(fileUploadLocation);
                        }
                        string newLocationAndFileName = fileUploadLocation + newFileName;
                        File.Copy(imageLocation, newLocationAndFileName);
                        RequisitionFile file = new RequisitionFile
                        {
                            FileLocation = newLocationAndFileName,
                            UploadedBy = Session.LoginUserName,
                            UploadedOn = DateTime.Now
                        };
                        _requisitionFiles.Add(file);
                        SetUploadedFileNumberLabel();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void manageUploadedFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                ManageUploadedFilesUI manageUploadedFilesUi = new ManageUploadedFilesUI(_requisitionFiles, _isUpdateMode);
                manageUploadedFilesUi.ShowDialog();
                SetUploadedFileNumberLabel();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();

                AdvanceCorporateAdvisoryRequisitionHeader advanceRequisitionHeader = new AdvanceCorporateAdvisoryRequisitionHeader();
                if (_isUpdateMode)
                {
                    advanceRequisitionHeader.CreatedBy = _updateableRequisitionHeader.CreatedBy;
                    advanceRequisitionHeader.CreatedOn = _updateableRequisitionHeader.CreatedOn;
                    advanceRequisitionHeader.LastModifiedBy = Session.LoginUserName;
                    advanceRequisitionHeader.LastModifiedOn = DateTime.Now;
                    advanceRequisitionHeader.RequisitionNo = _updateableRequisitionHeader.RequisitionNo;
                    advanceRequisitionHeader.SerialNo = _updateableRequisitionHeader.SerialNo;
                }
                else
                {
                    advanceRequisitionHeader.CreatedBy = Session.LoginUserName;
                    advanceRequisitionHeader.CreatedOn = DateTime.Now;
                }

                advanceRequisitionHeader.AdvanceCategoryId = _advanceCategory.Id;
                advanceRequisitionHeader.Currency = currencyComboBox.Text;
                advanceRequisitionHeader.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                advanceRequisitionHeader.RequisitionDate = requisitionDateTime.Value;
                advanceRequisitionHeader.RequesterUserName = _selectedEmployee.UserName;
                advanceRequisitionHeader.RequesterDepartmentId = _selectedEmployee.DepartmentID;
                advanceRequisitionHeader.RequesterRankId = _selectedEmployee.RankID;
                advanceRequisitionHeader.RequesterSupervisorId = _selectedEmployee.SupervisorID;
                advanceRequisitionHeader.FromDate = fromDateTimePicker.Value;
                advanceRequisitionHeader.ToDate = toDateTimePicker.Value;
                advanceRequisitionHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                advanceRequisitionHeader.Purpose = purposeTextBox.Text;
                advanceRequisitionHeader.CorporateAdvisoryPlaceOfEvent = placeOfEventTextBox.Text;
                advanceRequisitionHeader.AdvanceCorporateAdvisoryRequisitionDetails = GetAdvanceRequisitionDetailsFromGridView(advanceDetailsDataGridView);
                advanceRequisitionHeader.RequisitionFiles = _requisitionFiles;
                advanceRequisitionHeader.TotalRevenue = totalRevenueTextBox.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(totalRevenueTextBox.Text);
                advanceRequisitionHeader.UnitCost = unitCostHeaderTextBox.Text == string.Empty
                    ? (decimal?)null
                    : Convert.ToDecimal(unitCostHeaderTextBox.Text);
                advanceRequisitionHeader.NoOfUnit = noOfUnitHeaderTextBox.Text == string.Empty
                    ? (double?)null
                    : Convert.ToDouble(noOfUnitHeaderTextBox.Text);
                advanceRequisitionHeader.AdvanceCorporateRemarks = corporateAdvisoryRemarksTextBox.Text == string.Empty
                    ? null
                    : corporateAdvisoryRemarksTextBox.Text;
                if (!ValidateCeilingAmount())
                {
                    DialogResult result = MessageBox.Show(@"You are about to exceed the ceiling amount (" + _advanceCategory.CeilingAmount + "). Do you want to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                if (!_isUpdateMode)
                {
                    bool isInserted = _advanceRequisitionHeaderManager.Insert(advanceRequisitionHeader);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Your advance entry has been saved as draft.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Advance request failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    advanceRequisitionHeader.Id = _updateableRequisitionHeader.Id;

                    bool isUpdated = _advanceRequisitionHeaderManager.Edit(advanceRequisitionHeader);

                    if (isUpdated)
                    {
                        MessageBox.Show(@"Requested advance updated successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Requested advance update failed.", @"Error!", MessageBoxButtons.OK,
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
            if (!requisitionDateTime.Checked)
            {
                errorMessage += "Requisition date is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(purposeTextBox.Text))
            {
                errorMessage += "Purpose is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(placeOfEventTextBox.Text))
            {
                errorMessage += "Place of event is not provided." + Environment.NewLine;
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
            if (advanceDetailsDataGridView.Rows.Count < 1)
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

        private bool ValidateCeilingAmount()
        {
            var details = GetAdvanceRequisitionDetailsFromGridView(advanceDetailsDataGridView);

            var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
            var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
            var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;

            if (_advanceCategory.IsCeilingApplicable)
            {
                decimal totalAmount;
                if (!string.IsNullOrEmpty(advanceAmountTextBox.Text))
                {
                    totalAmount = totalAdvanceAmountInBDT + Convert.ToDecimal(advanceAmountTextBox.Text);
                }
                else
                {
                    totalAmount = totalAdvanceAmountInBDT;
                }
                if (totalAmount > _advanceCategory.CeilingAmount)
                {
                    return false;
                }
            }

            return true;
        }

       

        private void EditDetailItem()
        {
            //AdvanceCorporateAdvisoryRequisitionDetail detail = null;
            //if (advanceDetailsListView.SelectedItems.Count > 0)
            //{
            //    ListViewItem item = advanceDetailsListView.SelectedItems[0];
            //    if (advanceDetailsListView.Items.Count == item.Index + 1)
            //    {
            //        throw new UiException("No item is selected to edit.");
            //    }
            //    detail = item.Tag as AdvanceCorporateAdvisoryRequisitionDetail;
            //    if (detail == null)
            //    {
            //        throw new UiException("Item is not tagged.");
            //    }
            //    particularsTextBox.Text = detail.Purpose;
            //    noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
            //    unitCostTextBox.Text = detail.UnitCost.ToString();
            //    remarksTextBox.Text = detail.Remarks;
            //    receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
            //    advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
            //    isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
            //    receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
            //    vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
            //    SetFormToDetailChangeMode(item);
            //}
            //else
            //{
            //    throw new UiException("No item is selected to edit.");
            //}
        }

        private void SetFormToDetailChangeMode(DataGridViewRow item)
        {
            if (item == null)
            {
                throw new Exception("There is no data found for changing!");
            }
            addButton.Text = @"Change";
            _updateDetailItem = item;
            _updateDetailMode = true;
            saveButton.Visible = false;
        }

       
        private void RemoveDetailItem()
        {
            //if (advanceDetailsListView.SelectedItems.Count > 0)
            //{
            //    int detailItem =
            //        advanceDetailsListView.SelectedItems[0].Index;
            //    if (advanceDetailsListView.Items.Count == detailItem + 1)
            //    {
            //        throw new UiException("No item is selected to remove.");
            //    }

            //    RemoveTotalAmountFromListView();
            //    advanceDetailsListView.Items.RemoveAt(detailItem);

            //    if (advanceDetailsListView.Items.Count >= 1)
            //    {
            //        SetTotalAmountInListView(advanceDetailsListView);
            //    }
            //}
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDetailControl();
                ResetDetailChangeMode();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void totalRevenueTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

       
        private void advanceDetailsListView_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void advanceDetailsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    AdvanceCorporateAdvisoryRequisitionDetail detail = null;
                    if (advanceDetailsDataGridView.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = advanceDetailsDataGridView.SelectedRows[0];
                        if (advanceDetailsDataGridView.Rows.Count == row.Index + 1)
                        {
                            throw new UiException("No item is selected to edit.");
                        }
                        detail = row.Tag as AdvanceCorporateAdvisoryRequisitionDetail;
                        if (detail == null)
                        {
                            throw new UiException("Item is not tagged.");
                        }
                        particularsTextBox.Text = detail.Purpose;
                        noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
                        unitCostTextBox.Text = detail.UnitCost.ToString();
                        remarksTextBox.Text = detail.Remarks;
                        receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                        advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                        isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
                        receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                        vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
                        SetFormToDetailChangeMode(row);
                    }
                    else
                    {
                        throw new UiException("No item is selected to edit.");
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (advanceDetailsDataGridView.SelectedRows.Count > 0)
                    {
                        int detailItem =
                            advanceDetailsDataGridView.SelectedRows[0].Index;
                        if (advanceDetailsDataGridView.Rows.Count == detailItem + 1)
                        {
                            throw new UiException("No item is selected to remove.");
                        }

                        RemoveTotalAmountFromGridView();
                        advanceDetailsDataGridView.Rows.RemoveAt(detailItem);

                        if (advanceDetailsDataGridView.Rows.Count >= 1)
                        {
                            SetTotalAmountInGridView(advanceDetailsDataGridView);
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
