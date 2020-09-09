using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.UI.Settings;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace IDCOLAdvanceModule.UI.Entry
{
    public partial class RequisitionEntryForOverseasTravelUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly ICostItemManager _costItemManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private bool _isUpdateMode;
        private bool _isViewMode;
        private bool _isDetailUpdateMode;
        private DataGridViewRow _updateableDetailItem;
        private readonly AdvanceOverseasTravelRequisitionHeader _updateableRequisitionHeader;
        private readonly OverseasTravellingAllowanceManager _overseasTravellingAllowanceManager;
        private readonly IPlaceofVistManager _placeofVistManager;
        private long? _placeOfVisitId;
        private readonly ICollection<RequisitionFile> _requisitionFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;


        private RequisitionEntryForOverseasTravelUI()
        {
            InitializeComponent();
            _requisitionFiles = new List<RequisitionFile>();
            _employeeManager = new EmployeeManager();
            _costItemManager = new CostItemManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _overseasTravellingAllowanceManager = new OverseasTravellingAllowanceManager();
            _placeofVistManager = new PlaceOfVisitManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
        }

        public RequisitionEntryForOverseasTravelUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public RequisitionEntryForOverseasTravelUI(AdvanceOverseasTravelRequisitionHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableRequisitionHeader = header;
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            LoadHeaderInformation(header);
            _placeOfVisitId = header.PlaceOfVisitId;
            LoadDetailsInformation(header.AdvanceOverseasTravelRequisitionDetails.ToList());
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
            _isViewMode = true;
            requisitionDateTime.Enabled = false;
            addButton.Visible = false;
            saveButton.Visible = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
            detailsAddGroupBox.Enabled = false;
            //detailsShowGroupBox.Enabled = false;
            //advanceDetailsListView.ContextMenuStrip = null;
        }

        private void LoadPlaceOfVisit()
        {
            var placeOfVisits = _placeofVistManager.GetAll().ToList();
            if (placeOfVisits.Any())
            {
                placeOfVisits.Insert(0, new PlaceOfVisit() { Id = DefaultItem.Value, Name = DefaultItem.Text });
                placeOfVisitComboBox.DataSource = placeOfVisits;
                placeOfVisitComboBox.DisplayMember = "Name";
                placeOfVisitComboBox.ValueMember = "Id";
            }
            if (_isUpdateMode || _isViewMode)
            {
                placeOfVisitComboBox.SelectedValue = _updateableRequisitionHeader.PlaceOfVisitId;
            }
        }

        private void LoadHeaderInformation(AdvanceOverseasTravelRequisitionHeader header)
        {
            try
            {
                purposeOfTravelTextBox.Text = header.Purpose;
                placeOfVisitComboBox.SelectedValue = header.PlaceOfVisitId;
                fromDateTimePicker.Value = header.FromDate;
                fromDateTimePicker.Checked = true;
                toDateTimePicker.Value = header.ToDate;
                toDateTimePicker.Checked = true;
                requisitionDateTime.Value = header.RequisitionDate;
                requisitionDateTime.Checked = true;
                noOfDaysTextBox.Text = header.NoOfDays.ToString();
                countryNameTextBox.Text = header.CountryName;
                if (header.IsOverseasSponsorFinanced)
                {
                    yesRadioButton.Checked = true;
                    sponsorNameTextBox.Text = header.OverseasSponsorName;
                }
                else
                {
                    noRadioButton.Checked = true;
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDetailsInformation(ICollection<AdvanceOverseasTravelRequisitionDetail> details)
        {
            //advanceDetailsListView.Items.Clear();
            //foreach (AdvanceOverseasTravelRequisitionDetail detail in details)
            //{
            //    ListViewItem item = new ListViewItem(detail.OverseasFromDate != null ? detail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
            //    item.SubItems.Add(detail.OverseasToDate != null ? detail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
            //    item.SubItems.Add(detail.Purpose);
            //    item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
            //    item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
            //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
            //    item.SubItems.Add(detail.Currency);
            //    item.SubItems.Add(detail.ConversionRate.ToString("N"));
            //    item.SubItems.Add(detail.GetAdvanceAmountInBDT(Convert.ToDecimal(detail.ConversionRate)).ToString("N"));
            //    item.SubItems.Add(detail.OverseasSponsorFinancedDetailAmount == null ? "N/A" : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N"));
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");
            //    item.Tag = detail;
            //    advanceDetailsListView.Items.Add(item);
            //}

            //SetTotalAmountInListView(advanceDetailsListView);


            advanceDetailsDataGridView.Rows.Clear();
            foreach (AdvanceOverseasTravelRequisitionDetail detail in details)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(advanceDetailsDataGridView);

                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = "Remove";
                row.Cells[2].Value = detail.OverseasFromDate != null
                    ? detail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy")
                    : string.Empty;
                row.Cells[3].Value = detail.OverseasToDate != null
                    ? detail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy")
                    : string.Empty;
                row.Cells[4].Value = detail.Purpose;
                row.Cells[5].Value = detail.UnitCost != null && detail.UnitCost > 0
                    ? detail.UnitCost.Value.ToString("N")
                    : "N/A";
                row.Cells[6].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                    ? detail.NoOfUnit.Value.ToString("N")
                    : "N/A";

                row.Cells[7].Value = detail.AdvanceAmount.ToString("N");
                row.Cells[8].Value = detail.Currency;
                row.Cells[9].Value = detail.ConversionRate.ToString("N");
                row.Cells[10].Value = detail.GetAdvanceAmountInBDT(Convert.ToDecimal(detail.ConversionRate))
                    .ToString("N");
                row.Cells[11].Value = detail.OverseasSponsorFinancedDetailAmount == null
                    ? "N/A"
                    : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N");
                row.Cells[12].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                row.Cells[13].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                    ? detail.ReceipientOrPayeeName
                    : "N/A";
                row.Cells[14].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
                row.Tag = detail;
                advanceDetailsDataGridView.Rows.Add(row);
            }

            SetTotalAmountInGridView(advanceDetailsDataGridView);
        }

        private void SetFormToUpdateMode()
        {
            _isUpdateMode = true;
            saveButton.Text = @"Update";
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

        private void LoadCurrencyComboBox()
        {
            currencyComboBox.DataSource = null;

            ICollection<Solar_CurrencyInfo> currencyList =
                _currencyManager.GetAllForAdvanceRequisitionAndExpense();
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
                if (string.IsNullOrEmpty(conversionRateTextBox.Text))
                    conversionRateTextBox.Text = string.Empty;
                conversionRateTextBox.Enabled = true;
            }
        }

        private void LoadTravelCostItemDropDown()
        {
            var travelCostItems = _costItemManager.GetAll().ToList();
            if (travelCostItems.Any())
            {
                travelCostItems.Insert(0, new CostItem { Id = DefaultItem.Value, Name = DefaultItem.Text });
                travelCostItemComboBox.DataSource = travelCostItems;
                travelCostItemComboBox.DisplayMember = "Name";
                travelCostItemComboBox.ValueMember = "Id";
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

        private void currencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void AdvanceRequisitionEntryForOverseasTravelUI_Load(object sender, EventArgs e)
        {
            try
            {
                requisitionDateTime.Value = DateTime.Now;
                LoadTravelCostItemDropDown();
                LoadEmployeeInformation();
                LoadCurrencyComboBox();
                LoadVatTaxTypeComboBox();
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfUnitTextBox);
                LoadPlaceOfVisit();
                SetCategoryWiseFormText();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void travelCostItemComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                SetEntitlementAmount();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                travelCostItemComboBox.SelectedIndex = 0;
                entitlementTextBox.Text = string.Empty;
            }
        }

        private void SetEntitlementAmount()
        {
            var selectedTravelCostItem = travelCostItemComboBox.SelectedItem as CostItem;
            long placeOfVisitId = (long)placeOfVisitComboBox.SelectedValue;
            var employee = _employeeManager.GetByUserName(Session.LoginUserName);
            if (selectedTravelCostItem != null && selectedTravelCostItem.Id > 0 && placeOfVisitId > 0)
            {
                var overseasAllowance = _overseasTravellingAllowanceManager.GetEntitlementAmountByPlaceOfVisitId(placeOfVisitId, selectedTravelCostItem.Id, employee.Admin_Rank.RankID);
                if (overseasAllowance != null)
                {
                    if (!overseasAllowance.IsFullAmountEntitlement)
                    {
                        entitlementTextBox.Text = overseasAllowance.EntitlementAmount.ToString();
                        advanceAmountTextBox.Text = (overseasAllowance.EntitlementAmount * Convert.ToDecimal(noOfUnitTextBox.Text)).ToString();
                    }
                    else
                    {
                        entitlementTextBox.Text = @"Fully Entitled";
                        advanceAmountTextBox.Text = string.Empty;
                    }

                }
                else
                {
                    entitlementTextBox.Text = string.Empty;
                    advanceAmountTextBox.Text = string.Empty;
                }
            }
            else
            {
                string errorMsg = string.Empty;
                if (placeOfVisitId == (long)DefaultItem.Value)
                {
                    errorMsg = errorMsg + "Please choose place of visit" + Environment.NewLine;
                }
                ClearDetailControls();
                throw new UiException(errorMsg);
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

        private decimal GetTotalAmount()
        {
            ICollection<AdvanceOverseasTravelRequisitionDetail> detailList =
                GetAdvanceRequisitionDetailsFromGridView(advanceDetailsDataGridView);

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
                AdvanceOverseasTravelRequisitionDetail detail = new AdvanceOverseasTravelRequisitionDetail();
                if (!string.IsNullOrEmpty(noOfUnitTextBox.Text))
                {
                    detail.NoOfUnit = Convert.ToDouble(noOfUnitTextBox.Text);
                }
                detail.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                detail.Currency = currencyComboBox.Text;
                detail.OverseasFromDate = fromDetailDateTimePicker.Value;
                detail.OverseasToDate = toDetailDateTimePicker.Value;
                detail.Purpose = travelCostItemComboBox.Text;
                detail.OverseasTravelCostItemId = Convert.ToInt64(travelCostItemComboBox.SelectedValue);
                if (!string.IsNullOrEmpty(entitlementTextBox.Text) && !entitlementTextBox.Text.Equals(@"Fully Entitled"))
                {
                    detail.UnitCost = Convert.ToDecimal(entitlementTextBox.Text);
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
                detail.OverseasSponsorFinancedDetailAmount = sponsorDetailAmountTextBox.Text == string.Empty
                   ? (decimal?)null
                   : Convert.ToDecimal(sponsorDetailAmountTextBox.Text);
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
                    var updateableDetail = item.Tag as AdvanceOverseasTravelRequisitionDetail;

                    if (updateableDetail == null)
                    {
                        throw new UiException("There is no item tagged with the detail item!");
                    }
                    updateableDetail.OverseasFromDate = detail.OverseasFromDate;
                    updateableDetail.OverseasToDate = detail.OverseasToDate;
                    updateableDetail.NoOfUnit = detail.NoOfUnit;
                    updateableDetail.UnitCost = detail.UnitCost;
                    updateableDetail.Purpose = detail.Purpose;
                    updateableDetail.Remarks = detail.Remarks;
                    updateableDetail.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;
                    updateableDetail.OverseasTravelCostItemId = detail.OverseasTravelCostItemId;
                    updateableDetail.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;
                    updateableDetail.AdvanceAmount = detail.AdvanceAmount;
                    updateableDetail.OverseasSponsorFinancedDetailAmount = detail.OverseasSponsorFinancedDetailAmount;
                    updateableDetail.Currency = detail.Currency;
                    updateableDetail.ConversionRate = detail.ConversionRate;
                    updateableDetail.VatTaxTypeId = detail.VatTaxTypeId;

                    decimal justificationAmount = Convert.ToDecimal(advanceAmountTextBox.Text);
                    decimal entilementAmount = 0;
                    if (updateableDetail.UnitCost != null && updateableDetail.NoOfUnit != null)
                    {
                        entilementAmount = updateableDetail.UnitCost.Value * (decimal)updateableDetail.NoOfUnit.Value;
                    }
                    if (entilementAmount != 0)
                    {
                        if (justificationAmount > entilementAmount)
                        {
                            if (!string.IsNullOrEmpty(updateableDetail.Justification))
                            {
                                JustificationEntryUI justification = new JustificationEntryUI(updateableDetail.Justification, entilementAmount);
                                justification.ShowDialog();
                                updateableDetail.Justification = justification.Info;
                                if (updateableDetail.Justification == null)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                JustificationEntryUI justification = new JustificationEntryUI(entilementAmount);
                                justification.ShowDialog();
                                updateableDetail.Justification = justification.Info;
                                if (updateableDetail.Justification == null)
                                {
                                    return;
                                }
                            }


                        }
                        else if (justificationAmount == entilementAmount || entilementAmount > justificationAmount)
                        {
                            updateableDetail.Justification = null;
                        }
                    }
                    RemoveTotalAmountFromListView();
                    SetChangedGridViewItemByDetail(updateableDetail, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    //IsRequisitionDetailsExist(detail);
                    decimal justificationAmount = Convert.ToDecimal(advanceAmountTextBox.Text);
                    decimal entilementAmount = 0;
                    if (detail.UnitCost != null && detail.NoOfUnit != null)
                    {
                        entilementAmount = detail.UnitCost.Value * (decimal)detail.NoOfUnit.Value;
                    }
                    if (entilementAmount != 0)
                    {
                        if (justificationAmount > entilementAmount)
                        {
                            JustificationEntryUI justification = new JustificationEntryUI(entilementAmount);
                            justification.ShowDialog();
                            detail.Justification = justification.Info;
                            if (detail.Justification == null)
                            {
                                advanceAmountTextBox.Text = entilementAmount.ToString();
                                return;
                            }
                        }
                    }
                    var item = GetNewGridViewItemByDetail(detail);
                    RemoveTotalAmountFromListView();
                    advanceDetailsDataGridView.Rows.Add(item);
                }

                SetTotalAmountInGridView(advanceDetailsDataGridView);
                ResetCostItemComboBox();
                ClearDetailControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void SetTotalAmountInListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = GetAdvanceRequisitionDetailsFromListView(advanceDetailsListViewControl);

        //    var totalAdvanceAmountInBDT = details.Sum(c => c.AdvanceAmount * Convert.ToDecimal(c.ConversionRate));
        //    var totalSponsorAmountInBDT = details.Sum(c => c.OverseasSponsorFinancedDetailAmount) ?? 0;

        //    ListViewItem item = new ListViewItem();
        //    item.Text = string.Empty;
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
        //    item.SubItems.Add(totalSponsorAmountInBDT.ToString("N"));
        //    item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
        //    advanceDetailsListViewControl.Items.Add(item);
        //}

        private void SetTotalAmountInGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = GetAdvanceRequisitionDetailsFromGridView(advanceDetailsGridViewControl);

            var totalAdvanceAmountInBDT = details.Sum(c => c.AdvanceAmount * Convert.ToDecimal(c.ConversionRate));
            var totalSponsorAmountInBDT = details.Sum(c => c.OverseasSponsorFinancedDetailAmount) ?? 0;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(advanceDetailsGridViewControl);
            row.Cells[0]=new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();

            row.Cells[9].Value = (@"Total");
            row.Cells[10].Value = totalAdvanceAmountInBDT.ToString("N");
            row.Cells[11].Value = totalSponsorAmountInBDT.ToString("N");
            row.Cells[9].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[10].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[11].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            advanceDetailsGridViewControl.Rows.Add(row);
        }

        private void RemoveTotalAmountFromListView()
        {
            if (advanceDetailsDataGridView.Rows.Count > 1)
            {
                int detailItem =
                        advanceDetailsDataGridView.Rows.Count - 1;
                advanceDetailsDataGridView.Rows.RemoveAt(detailItem);
            }
        }

        //private ListViewItem GetNewListViewItemByDetail(AdvanceOverseasTravelRequisitionDetail detail)
        //{
        //    ListViewItem item = new ListViewItem(detail.OverseasFromDate != null ? detail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
        //    item.SubItems.Add(detail.OverseasToDate != null ? detail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
        //    item.SubItems.Add(detail.Purpose);
        //    item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //    item.SubItems.Add(detail.Currency);
        //    item.SubItems.Add(detail.ConversionRate.ToString("N"));
        //    item.SubItems.Add(detail.GetAdvanceAmountInBDT(Convert.ToDecimal(detail.ConversionRate)).ToString("N"));
        //    item.SubItems.Add(detail.OverseasSponsorFinancedDetailAmount != null
        //        ? detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N")
        //        : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItemByDetail(AdvanceOverseasTravelRequisitionDetail detail)
        {

            DataGridViewRow row=new DataGridViewRow();
            row.CreateCells(advanceDetailsDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.OverseasFromDate != null
                ? detail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[3].Value = detail.OverseasToDate != null
                ? detail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[4].Value = detail.Purpose;
            row.Cells[5].Value = detail.UnitCost != null && detail.UnitCost > 0
                ? detail.UnitCost.Value.ToString("N")
                : "N/A";
            row.Cells[6].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                ? detail.NoOfUnit.Value.ToString("N")
                : "N/A";

            row.Cells[7].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[8].Value = detail.Currency;
            row.Cells[9].Value = detail.ConversionRate.ToString("N");
            row.Cells[10].Value = detail.GetAdvanceAmountInBDT(Convert.ToDecimal(detail.ConversionRate))
                .ToString("N");
            row.Cells[11].Value = detail.OverseasSponsorFinancedDetailAmount == null
                ? "N/A"
                : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N");
            row.Cells[12].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[13].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Cells[14].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
            row.Tag = detail;
            return row;
        }

        //private ListViewItem SetChangedListViewItemByDetail(AdvanceOverseasTravelRequisitionDetail detail, ListViewItem item)
        //{
        //    item.Text = (detail.OverseasFromDate != null ? detail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
        //    item.SubItems[1].Text = detail.OverseasToDate != null ? detail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
        //    item.SubItems[2].Text = detail.Purpose;
        //    item.SubItems[3].Text = detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A";
        //    item.SubItems[4].Text = detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A";
        //    item.SubItems[5].Text = detail.AdvanceAmount.ToString("N");
        //    item.SubItems[6].Text = detail.Currency;
        //    item.SubItems[7].Text = detail.ConversionRate.ToString("N");
        //    item.SubItems[8].Text = detail.GetAdvanceAmountInBDT(Convert.ToDecimal(detail.ConversionRate)).ToString("N");
        //    item.SubItems[9].Text = detail.OverseasSponsorFinancedDetailAmount != null
        //        ? detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N")
        //        : "N/A";
        //    item.SubItems[10].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[11].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.SubItems[12].Text = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(AdvanceOverseasTravelRequisitionDetail detail, DataGridViewRow row)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.OverseasFromDate != null
                ? detail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[3].Value = detail.OverseasToDate != null
                ? detail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[4].Value = detail.Purpose;
            row.Cells[5].Value = detail.UnitCost != null && detail.UnitCost > 0
                ? detail.UnitCost.Value.ToString("N")
                : "N/A";
            row.Cells[6].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                ? detail.NoOfUnit.Value.ToString("N")
                : "N/A";

            row.Cells[7].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[8].Value = detail.Currency;
            row.Cells[9].Value = detail.ConversionRate.ToString("N");
            row.Cells[10].Value = detail.GetAdvanceAmountInBDT(Convert.ToDecimal(detail.ConversionRate))
                .ToString("N");
            row.Cells[11].Value = detail.OverseasSponsorFinancedDetailAmount == null
                ? "N/A"
                : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N");
            row.Cells[12].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[13].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Cells[14].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
            row.Tag = detail;
            return row;
        }

        private void ClearDetailControls()
        {
            vatTaxTypeComboBox.SelectedValue = DefaultItem.Value;
            advanceAmountTextBox.Text = string.Empty;
            Utility.Utility.CalculateNoOfDays(fromDetailDateTimePicker.Value, toDetailDateTimePicker.Value, noOfUnitTextBox);
            entitlementTextBox.Text = string.Empty;
            remarksTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Checked = false;
            receipientOrPayeeNameTextBox.Text = string.Empty;
            currencyComboBox.SelectedIndex = 0;
            sponsorDetailAmountTextBox.Text = string.Empty;
            conversionRateTextBox.Text = string.Empty;
        }

        private void ResetCostItemComboBox()
        {
            travelCostItemComboBox.SelectedIndex = 0;
        }

        private bool IsRequisitionDetailsExist(AdvanceOverseasTravelRequisitionDetail detail)
        {
            ICollection<AdvanceOverseasTravelRequisitionDetail> detailList = GetAdvanceRequisitionDetailsFromGridView(advanceDetailsDataGridView);
            if (detailList.Any(c => c.OverseasTravelCostItemId == detail.OverseasTravelCostItemId))
            {
                string errorMessage = "Cost item already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }

        private void yesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                sponsorNameTextBox.Enabled = yesRadioButton.Checked;
                if (!yesRadioButton.Checked)
                {
                    sponsorNameTextBox.Text = string.Empty;
                }
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
                AdvanceOverseasTravelRequisitionHeader advanceRequisitionHeader = new AdvanceOverseasTravelRequisitionHeader();
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
                advanceRequisitionHeader.Purpose = purposeOfTravelTextBox.Text;
                advanceRequisitionHeader.PlaceOfVisitId = (long)placeOfVisitComboBox.SelectedValue;
                advanceRequisitionHeader.IsOverseasSponsorFinanced = yesRadioButton.Checked;
                if (!string.IsNullOrEmpty(sponsorNameTextBox.Text))
                {
                    advanceRequisitionHeader.OverseasSponsorName = sponsorNameTextBox.Text;
                }
                advanceRequisitionHeader.AdvanceOverseasTravelRequisitionDetails = GetAdvanceRequisitionDetailsFromGridView(advanceDetailsDataGridView);
                advanceRequisitionHeader.RequisitionDate = requisitionDateTime.Value;
                advanceRequisitionHeader.AdvanceCategoryId = _advanceCategory.Id;
                advanceRequisitionHeader.FromDate = fromDateTimePicker.Value;
                advanceRequisitionHeader.ToDate = toDateTimePicker.Value;
                advanceRequisitionHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                advanceRequisitionHeader.CountryName = countryNameTextBox.Text == string.Empty ? null : countryNameTextBox.Text;
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

            //    RemoveTotalAmountFromListView();
            //    advanceDetailsListView.Items.RemoveAt(detailItem);

            //    if (advanceDetailsListView.Items.Count >= 1)
            //    {
            //        SetTotalAmountInListView(advanceDetailsListView);
            //    }
            //}
        }

        //private List<AdvanceOverseasTravelRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = new List<AdvanceOverseasTravelRequisitionDetail>();
        //    if (advanceDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in advanceDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvanceOverseasTravelRequisitionDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private List<AdvanceOverseasTravelRequisitionDetail> GetAdvanceRequisitionDetailsFromGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = new List<AdvanceOverseasTravelRequisitionDetail>();
            if (advanceDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in advanceDetailsGridViewControl.Rows)
                {
                    var detailItem = row.Tag as AdvanceOverseasTravelRequisitionDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
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

        private void noOfDaysTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private bool ValidateAdd()
        {
            string errorMessage = string.Empty;
            if (!fromDetailDateTimePicker.Checked)
            {
                errorMessage += "From date is not selected." + Environment.NewLine;
            }
            if (fromDetailDateTimePicker.Value.Date < fromDateTimePicker.Value.Date)
            {
                errorMessage += "From date of details cannot go before from date of header." + Environment.NewLine;
            }
            if (!toDetailDateTimePicker.Checked)
            {
                errorMessage += "To date is not selected." + Environment.NewLine;
            }
            if (toDetailDateTimePicker.Value.Date > toDateTimePicker.Value.Date)
            {
                errorMessage += "To date of details cannot exceed to date of header." + Environment.NewLine;
            }
            if ((long)travelCostItemComboBox.SelectedValue == -1)
            {
                errorMessage += "Travel cost item is not selected." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(advanceAmountTextBox.Text))
            {
                errorMessage += "Advance amount is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(conversionRateTextBox.Text))
            {
                errorMessage += "Conversion Rate is not provided." + Environment.NewLine;
            }
            if (isThirdPartyReceipientCheckBox.Checked && string.IsNullOrEmpty(receipientOrPayeeNameTextBox.Text))
            {
                errorMessage += "Receipient/Payee name is not provided." + Environment.NewLine;
            }
            if (Convert.ToDecimal(currencyComboBox.SelectedValue) == Convert.ToDecimal(DefaultItem.Value))
            {
                errorMessage += "Currency is not selected." + Environment.NewLine;
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
            if (string.IsNullOrEmpty(purposeOfTravelTextBox.Text))
            {
                errorMessage += "Purpose of travel is not provided." + Environment.NewLine;
            }
            if ((long)placeOfVisitComboBox.SelectedValue == DefaultItem.Value)
            {
                errorMessage += "Place of visit is not selected." + Environment.NewLine;
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
                errorMessage += "Could not proceed further! As you have not added any requisition detail." + Environment.NewLine;
            }
            if (yesRadioButton.Checked && string.IsNullOrEmpty(sponsorNameTextBox.Text))
            {
                errorMessage += "Sponsor name is not provided." + Environment.NewLine;
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

       
        //private void EditDetailItem()
        //{
        //    AdvanceOverseasTravelRequisitionDetail detail = null;
        //    if (advanceDetailsListView.SelectedItems.Count > 0)
        //    {
        //        ListViewItem item = advanceDetailsListView.SelectedItems[0];
        //        if (advanceDetailsListView.Items.Count == item.Index + 1)
        //        {
        //            throw new UiException("No item is selected to edit.");
        //        }
        //        detail = item.Tag as AdvanceOverseasTravelRequisitionDetail;
        //        if (detail == null)
        //        {
        //            throw new UiException("Item is not tagged");
        //        }
        //        fromDetailDateTimePicker.Value = detail.OverseasFromDate.Value;
        //        toDetailDateTimePicker.Value = detail.OverseasToDate.Value;
        //        travelCostItemComboBox.Text = detail.Purpose;
        //        entitlementTextBox.Text = detail.UnitCost != null && detail.UnitCost > 0
        //            ? detail.UnitCost.Value.ToString()
        //            : string.Empty;
        //        noOfUnitTextBox.Text = detail.NoOfUnit != null && detail.NoOfUnit > 0
        //            ? detail.NoOfUnit.Value.ToString()
        //            : string.Empty;
        //        remarksTextBox.Text = detail.Remarks;
        //        SetEntitlementAmount();
        //        advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
        //        isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
        //        receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
        //        sponsorDetailAmountTextBox.Text = detail.OverseasSponsorFinancedDetailAmount != null
        //            ? detail.OverseasSponsorFinancedDetailAmount.Value.ToString()
        //            : string.Empty;
        //        conversionRateTextBox.Text = detail.ConversionRate.ToString();
        //        currencyComboBox.Text = detail.Currency;
        //        vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
        //        SetFormToDetailChangeMode(item);
        //    }
        //    else
        //    {
        //        throw new UiException("Not selected an item to edit!");
        //    }
        //}

        private void SetFormToDetailChangeMode(DataGridViewRow detail)
        {
            if (detail == null)
            {
                throw new Exception("There is no data found for changing!");
            }
            _updateableDetailItem = detail;
            addButton.Text = @"Change";
            _isDetailUpdateMode = true;
            saveButton.Visible = false;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetDetailChangeMode();
        }

        private void ResetDetailChangeMode()
        {
            addButton.Text = @"Add";
            _updateableDetailItem = null;
            _isDetailUpdateMode = false;
            saveButton.Visible = true;
            ClearDetailControls();
            ResetCostItemComboBox();
        }

        private void noOfUnitTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(entitlementTextBox.Text) && !string.IsNullOrEmpty(noOfUnitTextBox.Text) && !entitlementTextBox.Text.Equals("Fully Entitled"))
                {
                    decimal entitlement = Convert.ToDecimal(entitlementTextBox.Text);
                    decimal noOnUnit = Convert.ToDecimal(noOfUnitTextBox.Text);

                    decimal advanceAmount = entitlement * noOnUnit;

                    advanceAmountTextBox.Text = advanceAmount.ToString();
                }
                if (string.IsNullOrEmpty(noOfUnitTextBox.Text))
                {
                    advanceAmountTextBox.Text = string.Empty;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void placeOfVisitComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var placeOfvisitId = (long)placeOfVisitComboBox.SelectedValue;
                if (_placeOfVisitId != null)
                {
                    if (_placeOfVisitId != placeOfvisitId)
                    {
                        var details = GetAdvanceRequisitionDetailsFromGridView(advanceDetailsDataGridView);
                        if (details.Count > 0)
                        {
                            MessageBox.Show(@"You have aready added detail item for another place of visit. Please remove all place of visit and add new detail again.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            placeOfVisitComboBox.SelectedValue = _placeOfVisitId;
                        }
                    }
                }
                else
                {
                    _placeOfVisitId = placeOfvisitId;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void conversionRateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetAdvanceAmount();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetAdvanceAmount()
        {
            var conversionRate = conversionRateTextBox.Text == string.Empty
                   ? 0
                   : Convert.ToDecimal(conversionRateTextBox.Text);
            var advanceAmount = advanceAmountTextBox.Text == string.Empty
            ? 0
            : Convert.ToDecimal(advanceAmountTextBox.Text);
            advanceAmountInBDTTextBox.Text = (conversionRate * advanceAmount).ToString();
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

        private void yesRadioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            sponsorNameTextBox.Enabled = yesRadioButton.Checked;
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

        private void sponsorDetailAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void fromDetailDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDetailDateTimePicker.Value, toDetailDateTimePicker.Value, noOfUnitTextBox);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toDetailDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDetailDateTimePicker.Value, toDetailDateTimePicker.Value, noOfUnitTextBox);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetAdvanceAmount();
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
                        AdvanceOverseasTravelRequisitionDetail detail = null;
                        if (advanceDetailsDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow row = advanceDetailsDataGridView.SelectedRows[0];
                            if (advanceDetailsDataGridView.Rows.Count == row.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = row.Tag as AdvanceOverseasTravelRequisitionDetail;
                            if (detail == null)
                            {
                                throw new UiException("Item is not tagged");
                            }
                            fromDetailDateTimePicker.Value = detail.OverseasFromDate.Value;
                            toDetailDateTimePicker.Value = detail.OverseasToDate.Value;
                            travelCostItemComboBox.Text = detail.Purpose;
                            entitlementTextBox.Text = detail.UnitCost != null && detail.UnitCost > 0
                                ? detail.UnitCost.Value.ToString()
                                : string.Empty;
                            noOfUnitTextBox.Text = detail.NoOfUnit != null && detail.NoOfUnit > 0
                                ? detail.NoOfUnit.Value.ToString()
                                : string.Empty;
                            remarksTextBox.Text = detail.Remarks;
                            SetEntitlementAmount();
                            advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                            isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
                            receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                            sponsorDetailAmountTextBox.Text = detail.OverseasSponsorFinancedDetailAmount != null
                                ? detail.OverseasSponsorFinancedDetailAmount.Value.ToString()
                                : string.Empty;
                            conversionRateTextBox.Text = detail.ConversionRate.ToString();
                            currencyComboBox.Text = detail.Currency;
                            vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
                            SetFormToDetailChangeMode(row);
                        }
                        else
                        {
                            throw new UiException("Not selected an item to edit!");
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

                            RemoveTotalAmountFromListView();
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }





        //private void advanceDetailsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        EditDetailItem();
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


    }
}
