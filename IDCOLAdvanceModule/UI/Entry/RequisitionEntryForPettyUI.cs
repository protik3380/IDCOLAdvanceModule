using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Entry
{
    public partial class RequisitionEntryForPettyUI : Form
    {
        private UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly ICurrencyManager _currencyManager;
        private readonly IEmployeeManager _employeeManager;
        private bool _isUpdateMode;
        private bool _isDetailUpdateMode;
        private DataGridViewRow _updateableDetailItem;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly AdvancePettyCashRequisitionHeader _updateableRequisitionHeader;
        private IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly ICollection<RequisitionFile> _requisitionFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;

        private RequisitionEntryForPettyUI()
        {
            InitializeComponent();
            _requisitionFiles = new List<RequisitionFile>();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _employeeManager = new EmployeeManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
        }

        public RequisitionEntryForPettyUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public RequisitionEntryForPettyUI(AdvancePettyCashRequisitionHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableRequisitionHeader = header;
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            LoadHeaderInformation(header);
            LoadDetailsInformation(header.AdvancePettyCashRequisitionDetails.ToList());
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

        private void LoadHeaderInformation(AdvancePettyCashRequisitionHeader header)
        {
            try
            {
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
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDetailsInformation(ICollection<AdvancePettyCashRequisitionDetail> details)
        {
            try
            {
                foreach (AdvancePettyCashRequisitionDetail detail in details)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(advanceDetailDataGridView);

                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = detail.Purpose;
                    row.Cells[3].Value = detail.AdvanceAmount.ToString("N");
                    row.Cells[4].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                    row.Cells[5].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                        ? detail.ReceipientOrPayeeName
                        : "N/A";
                    row.Tag = detail;

                    advanceDetailDataGridView.Rows.Add(row);
                }

                SetTotalAmountInGridView(advanceDetailDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
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

        private void selectEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                SelectEmployeeUI selectEmployeeUi = new SelectEmployeeUI();
                selectEmployeeUi.ShowDialog();
                _selectedEmployee = selectEmployeeUi.SelectedEmployee;
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

        private decimal GetTotalAmount()
        {
            ICollection<AdvancePettyCashRequisitionDetail> detailList =
                GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);

            if (detailList.Any())
            {
                return detailList.Sum(c => c.AdvanceAmount);
            }
            return 0;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();

                AdvancePettyCashRequisitionDetail detail = new AdvancePettyCashRequisitionDetail();
                detail.Purpose = purposeTextBox.Text;
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
                if (_isDetailUpdateMode)
                {
                    if (_updateableDetailItem == null)
                    {
                        throw new UiException("There is no item found to update.");
                    }
                    var item = _updateableDetailItem;
                    var updateDetailItem = item.Tag as AdvancePettyCashRequisitionDetail;
                    if (updateDetailItem == null)
                    {
                        throw new UiException("There is no item tagged with the detail item.");
                    }
                    updateDetailItem.Purpose = detail.Purpose;
                    updateDetailItem.AdvanceAmount = detail.AdvanceAmount;
                    updateDetailItem.Remarks = detail.Remarks;
                    updateDetailItem.NoOfUnit = detail.NoOfUnit;
                    updateDetailItem.UnitCost = detail.UnitCost;
                    updateDetailItem.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;
                    updateDetailItem.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;
                    updateDetailItem.Id = detail.Id;
                    updateDetailItem.VatTaxTypeId = detail.VatTaxTypeId;
                    RemoveTotalAmountFromGridView();
                    SetChangeGridViewItemByDetail(updateDetailItem, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    //IsRequisitionDetailsExist(detail);
                    var item = GetNewGridViewItemByDetail(detail);

                    RemoveTotalAmountFromGridView();
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

        //private void SetTotalAmountInListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);

        //    var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
        //    var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
        //    var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;

        //    ListViewItem item = new ListViewItem();
        //    item.Text = @"Total";
        //    item.SubItems.Add(totalAdvanceAmount.ToString("N"));
        //    item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
        //    advanceDetailsListViewControl.Rows.Add(item);
        //}
        private void SetTotalAmountInGridView(DataGridView advanceDetailsDataGridView)
        {
            var details = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);

            var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
            var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
            var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;


            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(advanceDetailsDataGridView);

            //advanceDetailsDataGridView.Columns[0].ty
            //DataGridViewButtonCell cell = new DataGridViewButtonCell{Style = new DataGridViewCellStyle{BackColor = Color.Crimson}};
            row.Cells[0] = new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();

            row.Cells[2].Value = "Total";
            row.Cells[3].Value = totalAdvanceAmount.ToString("N");

            row.Cells[2].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[3].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);

            row.Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            advanceDetailsDataGridView.Rows.Add(row);
        }

        //private void RemoveTotalAmountFromListView()
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
            if (advanceDetailDataGridView.Rows.Count > 1)
            {
                int detailItem =
                        advanceDetailDataGridView.Rows.Count - 1;
                advanceDetailDataGridView.Rows.RemoveAt(detailItem);
            }
        }

        private void ClearDetailControl()
        {
            vatTaxTypeComboBox.SelectedValue = DefaultItem.Value;
            purposeTextBox.Text = string.Empty;
            advanceAmountTextBox.Text = string.Empty;
            remarksTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Checked = false;
            receipientOrPayeeNameTextBox.Text = string.Empty;
            addButton.Text = @"Add";
        }

        private void ResetDetailChangeMode()
        {
            addButton.Text = @"Add";
            _isDetailUpdateMode = false;
            _updateableDetailItem = null;
            saveButton.Visible = true;
            ClearDetailControl();
        }

        //private ListViewItem SetChangeListViewItemByDetail(AdvancePettyCashRequisitionDetail detail, ListViewItem item)
        //{
        //    item.Text = detail.Purpose;
        //    item.SubItems[1].Text = detail.AdvanceAmount.ToString("N");
        //    item.SubItems[2].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[3].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangeGridViewItemByDetail(AdvancePettyCashRequisitionDetail detail, DataGridViewRow row)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.Purpose;
            row.Cells[3].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[4].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[5].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Tag = detail;
            return row;
        }

        //private static ListViewItem GettNewListViewItemByDetail(AdvancePettyCashRequisitionDetail detail)
        //{
        //    ListViewItem item = new ListViewItem(detail.Purpose);
        //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItemByDetail(AdvancePettyCashRequisitionDetail detail)
        {

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(advanceDetailDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.Purpose;
            row.Cells[3].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[4].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[5].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Tag = detail;
            return row;
        }

        private bool IsRequisitionDetailsExist(AdvancePettyCashRequisitionDetail detail)
        {
            ICollection<AdvancePettyCashRequisitionDetail> detailList = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);
            if (detailList.Any(c => c.Purpose.Equals(detail.Purpose)))
            {
                string errorMessage = "Purpose already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();

                AdvancePettyCashRequisitionHeader advanceRequisitionHeader = new AdvancePettyCashRequisitionHeader();
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

                advanceRequisitionHeader.RequesterUserName = _selectedEmployee.UserName;
                advanceRequisitionHeader.RequesterDepartmentId = _selectedEmployee.DepartmentID;
                advanceRequisitionHeader.RequesterRankId = _selectedEmployee.RankID;
                advanceRequisitionHeader.RequesterSupervisorId = _selectedEmployee.SupervisorID;
                advanceRequisitionHeader.RequisitionDate = requisitionDateTime.Value;
                advanceRequisitionHeader.AdvancePettyCashRequisitionDetails = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);
                advanceRequisitionHeader.AdvanceCategoryId = _advanceCategory.Id;
                advanceRequisitionHeader.FromDate = fromDateTimePicker.Value;
                advanceRequisitionHeader.ToDate = toDateTimePicker.Value;
                advanceRequisitionHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                advanceRequisitionHeader.Currency = currencyComboBox.Text;
                advanceRequisitionHeader.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                advanceRequisitionHeader.RequisitionFiles = _requisitionFiles;
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
                            MessageBoxIcon.Warning);
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

        //private List<AdvancePettyCashRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = new List<AdvancePettyCashRequisitionDetail>();
        //    if (advanceDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in advanceDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvancePettyCashRequisitionDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private List<AdvancePettyCashRequisitionDetail> GetAdvanceRequisitionDetailsFromGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = new List<AdvancePettyCashRequisitionDetail>();
            if (advanceDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in advanceDetailsGridViewControl.Rows)
                {
                    var detailItem = item.Tag as AdvancePettyCashRequisitionDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
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

            //    RemoveTotalAmountFromGridView();
            //    advanceDetailsListView.Items.RemoveAt(detailItem);

            //    if (advanceDetailsListView.Items.Count >= 1)
            //    {
            //        SetTotalAmountInGridView(advanceDetailDataGridView);
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
            //AdvancePettyCashRequisitionDetail detail = null;
            //if (advanceDetailsListView.Items.Count > 0)
            //{
            //    ListViewItem item = advanceDetailsListView.SelectedItems[0];
            //    if (advanceDetailsListView.Items.Count == item.Index + 1)
            //    {
            //        throw new UiException("No item is selected to edit.");
            //    }
            //    detail = item.Tag as AdvancePettyCashRequisitionDetail;
            //    if (detail == null)
            //    {
            //        throw new UiException("Item is not tagged.");
            //    }
            //    purposeTextBox.Text = detail.Purpose;
            //    advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
            //    remarksTextBox.Text = detail.Remarks;
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
        }

        private void AdvanceCashRequisitionForPettyUI_Load(object sender, EventArgs e)
        {
            try
            {
                requisitionDateTime.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadCurrencyComboBox();
                LoadVatTaxTypeComboBox();
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
                SetCategoryWiseFormText();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.Utility.MakeTextBoxToTakeNumbersOnly(sender, e);
        }

        private void noOfDaysTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.Utility.MakeTextBoxToTakeNumbersOnly(sender, e);
        }

        private void conversionRateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.Utility.MakeTextBoxToTakeNumbersOnly(sender, e);
        }

        private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
        }

        private void toDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
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
                _isUpdateMode = false;
                addButton.Text = @"Add";
                saveButton.Visible = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void isThirdPartyReceipientCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            receipientOrPayeeNameTextBox.ReadOnly = !isThirdPartyReceipientCheckBox.Checked;
            if (!isThirdPartyReceipientCheckBox.Checked)
            {
                receipientOrPayeeNameTextBox.Text = string.Empty;
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

        private void advanceDetailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        AdvancePettyCashRequisitionDetail detail = null;
                        if (advanceDetailDataGridView.Rows.Count > 0)
                        {
                            DataGridViewRow row = advanceDetailDataGridView.SelectedRows[0];
                            if (advanceDetailDataGridView.Rows.Count == row.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = row.Tag as AdvancePettyCashRequisitionDetail;
                            if (detail == null)
                            {
                                throw new UiException("Item is not tagged.");
                            }
                            purposeTextBox.Text = detail.Purpose;
                            advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                            remarksTextBox.Text = detail.Remarks;
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
                        if (advanceDetailDataGridView.SelectedRows.Count > 0)
                        {
                            int detailItem =
                                advanceDetailDataGridView.SelectedRows[0].Index;
                            if (advanceDetailDataGridView.Rows.Count == detailItem + 1)
                            {
                                throw new UiException("No item is selected to remove.");
                            }

                            RemoveTotalAmountFromGridView();
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
