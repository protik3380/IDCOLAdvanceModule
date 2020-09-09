using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
using MetroFramework;
using System.IO;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.UI.Entry
{
    public partial class RequisitionEntryForMiscellaneousUI : Form
    {
        private UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private bool _isUpdateMode;
        private bool _updateDetailMode;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly AdvanceMiscelleneousRequisitionHeader _updateableRequisitionHeader;
        private DataGridViewRow _updateDetailItem;
        private readonly ICollection<RequisitionFile> _requisitionFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;

        private RequisitionEntryForMiscellaneousUI()
        {
            InitializeComponent();
            _requisitionFiles = new List<RequisitionFile>();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _employeeManager = new EmployeeManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
        }

        public RequisitionEntryForMiscellaneousUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public RequisitionEntryForMiscellaneousUI(AdvanceMiscelleneousRequisitionHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableRequisitionHeader = header;
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            LoadHeaderInformation(header);
            LoadDetailsInformation(header.AdvanceMiscelleneousRequisitionDetails.ToList());
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

        private void SetFormToViewMode()
        {
            requisitionDateTime.Enabled = false;
            addButton.Visible = false;
            saveButton.Visible = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
            detailsAddGroupBox.Enabled = false;
            //detailsShowGroupBox.Enabled = false;
            //advanceDetailsListView.ContextMenuStrip = null;
        }

        private void LoadHeaderInformation(AdvanceMiscelleneousRequisitionHeader header)
        {
            purposeTextBox.Text = header.Purpose;
            placeOfEventTextBox.Text = header.PlaceOfEvent;
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            requisitionDateTime.Value = header.RequisitionDate;
            requisitionDateTime.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
        }

        //private void LoadDetailsInformation(ICollection<AdvanceMiscelleneousRequisitionDetail> details)
        //{
        //    foreach (AdvanceMiscelleneousRequisitionDetail detail in details)
        //    {
        //        ListViewItem item = new ListViewItem(detail.Purpose);
        //        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //        item.SubItems.Add(detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == "" ? 0 : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //        item.Tag = detail;
        //        advanceDetailsListView.Items.Add(item);
        //    }

        //    SetTotalAmountInListView(advanceDetailsListView);
        //}

        private void LoadDetailsInformation(ICollection<AdvanceMiscelleneousRequisitionDetail> details)
        {
            foreach (AdvanceMiscelleneousRequisitionDetail detail in details)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(advanceDetailDataGridView);
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
                row.Cells[6].Value =
                    detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == ""
                        ? 0
                        : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N");
                row.Cells[7].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                row.Cells[8].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                    ? detail.ReceipientOrPayeeName
                    : "N/A";
                row.Tag = detail;
                advanceDetailDataGridView.Rows.Add(row);
            }

            SetTotalAmountInGridView(advanceDetailDataGridView);
        }

        private void SetFormToUpdateMode()
        {
            _isUpdateMode = true;
            saveButton.Text = @"Update";
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

        private void selectEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                SelectEmployeeUI selectEmployeeUi = new SelectEmployeeUI();
                selectEmployeeUi.ShowDialog();
                _selectedEmployee = selectEmployeeUi.SelectedEmployee;
                if (_selectedEmployee != null)
                {
                    particularsTextBox.Text = _selectedEmployee.Admin_Rank == null ? "N/A" : _selectedEmployee.Admin_Rank.RankName;
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

        private void AdvanceCashRequisitionForMiscellaneousUI_Load(object sender, EventArgs e)
        {
            try
            {
                requisitionDateTime.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadVatTaxTypeComboBox();
                LoadCurrencyComboBox();
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
                SetCategoryWiseFormText();
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();

                AdvanceMiscelleneousRequisitionHeader advanceRequisitionHeader = new AdvanceMiscelleneousRequisitionHeader();
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
                //advanceRequisitionHeader.Purpose = purposeOfTravelTextBox.Text;
                advanceRequisitionHeader.FromDate = fromDateTimePicker.Value;
                advanceRequisitionHeader.ToDate = toDateTimePicker.Value;
                advanceRequisitionHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                advanceRequisitionHeader.Purpose = purposeTextBox.Text;
                advanceRequisitionHeader.PlaceOfEvent = placeOfEventTextBox.Text;
                advanceRequisitionHeader.AdvanceMiscelleneousRequisitionDetails = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);
                advanceRequisitionHeader.RequisitionFiles = _requisitionFiles;

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

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();

                AdvanceMiscelleneousRequisitionDetail detail = new AdvanceMiscelleneousRequisitionDetail();
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
                    var updateItemDetail = item.Tag as AdvanceMiscelleneousRequisitionDetail;

                    updateItemDetail.NoOfUnit = detail.NoOfUnit;
                    updateItemDetail.Purpose = detail.Purpose;
                    updateItemDetail.Remarks = detail.Remarks;
                    updateItemDetail.UnitCost = detail.UnitCost;
                    updateItemDetail.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;
                    updateItemDetail.AdvanceAmount = detail.AdvanceAmount;
                    updateItemDetail.Id = detail.Id;
                    updateItemDetail.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;
                    updateItemDetail.MiscelleneousCostItem = detail.MiscelleneousCostItem;
                    updateItemDetail.VatTaxTypeId = detail.VatTaxTypeId;
                    RemoveTotalAmountFromListView();
                    SetGridViewItemByDetail(updateItemDetail, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    //IsRequisitionDetailsExist(detail);
                    var item = GettNewGridViewItemByDetail(detail);
                    RemoveTotalAmountFromListView();
                    advanceDetailDataGridView.Rows.Add(item);
                }

                SetTotalAmountInGridView(advanceDetailDataGridView);
                ClearDetailControl();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RemoveTotalAmountFromListView()
        {
            if (advanceDetailDataGridView.Rows.Count > 1)
            {
                int detailItem =
                        advanceDetailDataGridView.Rows.Count - 1;
                advanceDetailDataGridView.Rows.RemoveAt(detailItem);
            }
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
            row.Cells[0]= new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();

            row.Cells[4].Value = @"Total";
            row.Cells[5].Value = totalAdvanceAmountInBDT.ToString("N");

            row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[5].Style.Alignment =DataGridViewContentAlignment.MiddleRight;
            row.Cells[4].Style.Font =new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[5].Style.Font =new Font(Utility.Utility.FontName, 10, FontStyle.Bold);

            advanceDetailsGridViewControl.Rows.Add(row);
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

        private void ResetDetailChangeMode()
        {
            addButton.Text = @"Add";
            _updateDetailItem = null;
            _updateDetailMode = false;
            saveButton.Visible = true;
            ClearDetailControl();
        }

        //private static ListViewItem GettNewListViewItemByDetail(AdvanceMiscelleneousRequisitionDetail detail)
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

        private DataGridViewRow GettNewGridViewItemByDetail(AdvanceMiscelleneousRequisitionDetail detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(advanceDetailDataGridView);
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
            //row.Cells[6].Value =
            //    detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == ""
            //        ? 0
            //        : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N");
            row.Cells[6].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[7].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Tag = detail;
            return row;
        }

        //private static ListViewItem SetListViewItemByDetail(AdvanceMiscelleneousRequisitionDetail detail, ListViewItem item)
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
        private static DataGridViewRow SetGridViewItemByDetail(AdvanceMiscelleneousRequisitionDetail detail, DataGridViewRow row)
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
            //row.Cells[6].Value =
            //    detail.GetAdvanceAmountInBDT(conversionRateTextBox.Text == ""
            //        ? 0
            //        : Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N");
            row.Cells[6].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[7].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Tag = detail;
            return row;
        }

        private bool IsRequisitionDetailsExist(AdvanceMiscelleneousRequisitionDetail detail)
        {
            ICollection<AdvanceMiscelleneousRequisitionDetail> detailList = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);
            if (detailList.Any(c => c.Purpose.Equals(detail.Purpose)))
            {
                string errorMessage = "Purpose already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }


        //private void EditDetailItem()
        //{
        //    AdvanceMiscelleneousRequisitionDetail detail = null;
        //    if (advanceDetailsListView.SelectedItems.Count > 0)
        //    {
        //        ListViewItem item = advanceDetailsListView.SelectedItems[0];
        //        if (advanceDetailsListView.Items.Count == item.Index + 1)
        //        {
        //            throw new UiException("No item is selected to edit.");
        //        }
        //        detail = item.Tag as AdvanceMiscelleneousRequisitionDetail;
        //        if (detail == null)
        //        {
        //            throw new UiException("Item is not tagged.");
        //        }
        //        particularsTextBox.Text = detail.Purpose;
        //        noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
        //        unitCostTextBox.Text = detail.UnitCost.ToString();
        //        remarksTextBox.Text = detail.Remarks;
        //        receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
        //        advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
        //        isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
        //        receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
        //        vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
        //        SetFormToDetailChangeMode(item);
        //    }
        //    else
        //    {
        //        throw new UiException("No item is selected to edit.");
        //    }
        //}

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

        private decimal GetTotalAmount()
        {
            ICollection<AdvanceMiscelleneousRequisitionDetail> detailList =
                GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);

            if (detailList.Any())
            {
                return detailList.Sum(c => c.AdvanceAmount);
            }
            return 0;
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

        private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
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
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private ICollection<AdvanceMiscelleneousRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = new List<AdvanceMiscelleneousRequisitionDetail>();
        //    if (advanceDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in advanceDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvanceMiscelleneousRequisitionDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}
        private ICollection<AdvanceMiscelleneousRequisitionDetail> GetAdvanceRequisitionDetailsFromGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = new List<AdvanceMiscelleneousRequisitionDetail>();
            if (advanceDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in advanceDetailsGridViewControl.Rows)
                {
                    var detailItem = row.Tag as AdvanceMiscelleneousRequisitionDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
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
            if (advanceDetailDataGridView.Rows.Count < 1)
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


        //private void RemoveDetailItem()
        //{
        //    if (advanceDetailsListView.SelectedItems.Count > 0)
        //    {
        //        int detailItem =
        //            advanceDetailsListView.SelectedItems[0].Index;
        //        if (advanceDetailsListView.Items.Count == detailItem + 1)
        //        {
        //            throw new UiException("No item is selected to remove.");
        //        }

        //        RemoveTotalAmountFromListView();
        //        advanceDetailsListView.Items.RemoveAt(detailItem);

        //        if (advanceDetailsListView.Items.Count >= 1)
        //        {
        //            SetTotalAmountInListView(advanceDetailsListView);
        //        }
        //    }
        //}

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

        //private void advanceDetailsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (advanceDetailDataGridView.SelectedRows.Count > 0)
        //        {
        //            editButton.Visible = true;
        //            removeButton.Visible = true;
        //        }
        //        else
        //        {
        //            editButton.Visible = false;
        //            removeButton.Visible = false;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private void advanceDetailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        AdvanceMiscelleneousRequisitionDetail detail = null;
                        if (advanceDetailDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow item = advanceDetailDataGridView.SelectedRows[0];
                            if (advanceDetailDataGridView.Rows.Count == item.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = item.Tag as AdvanceMiscelleneousRequisitionDetail;
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
                            SetFormToDetailChangeMode(item);
                        }
                        else
                        {
                            throw new UiException("No item is selected to edit.");
                        }
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        if (advanceDetailDataGridView.SelectedRows.Count > 0)
                        {
                            int detailItem =
                                advanceDetailDataGridView.SelectedRows[0].Index;
                            if (advanceDetailDataGridView.Rows.Count == detailItem + 1)
                            {
                                throw new UiException("No item is selected to remove.");
                            }

                            RemoveTotalAmountFromListView();
                            advanceDetailDataGridView.Rows.RemoveAt(detailItem);

                            if (advanceDetailDataGridView.Rows.Count >= 1)
                            {
                                SetTotalAmountInGridView(advanceDetailDataGridView);
                            }
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
