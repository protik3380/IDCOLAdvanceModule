using System.Drawing.Text;
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
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;

namespace IDCOLAdvanceModule.UI.Entry
{
    public partial class RequisitionEntryForLocalTravelUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IEntitlementMappingSettingManager _entitlementManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly ICostItemManager _costItemManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private bool _isUpdateMode;
        private bool _isDetailUpdateMode;
        private DataGridViewRow _updateableDetailItem;
        private readonly AdvanceTravelRequisitionHeader _updateableRequisitionHeader;
        private readonly ICollection<RequisitionFile> _requisitionFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        private EntitlementMappingSettingVM _entitlementVm;
        //private string _justification;
        //private decimal _justificationAmount;

        private RequisitionEntryForLocalTravelUI()
        {
            InitializeComponent();
            _requisitionFiles = new List<RequisitionFile>();
            _employeeManager = new EmployeeManager();
            _entitlementManager = new EntitlementMappingSettingManager();
            _costItemManager = new CostItemManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _vatTaxTypeManager = new VatTaxTypeManager();

        }


        public RequisitionEntryForLocalTravelUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public RequisitionEntryForLocalTravelUI(AdvanceTravelRequisitionHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableRequisitionHeader = header;
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName.ToLower());
            LoadHeaderInformation(header);
            LoadDetailsInformation(header.AdvanceTravelRequisitionDetails.ToList());
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

        private void LoadHeaderInformation(AdvanceTravelRequisitionHeader header)
        {
            purposeOfTravelTextBox.Text = header.Purpose;
            placeOfVisitTextBox.Text = header.PlaceOfVisit;
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            requisitionDateTime.Value = header.RequisitionDate;
            requisitionDateTime.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
            yesRadioButton.Checked = header.IsSponsorFinanced;
            sponsorNameTextBox.Text = header.IsSponsorFinanced ? header.SponsorName : string.Empty;
        }

        private void LoadDetailsInformation(ICollection<AdvanceTravelRequisitionDetail> details)
        {
            //advanceDetailsListView.Items.Clear();

            //foreach (AdvanceTravelRequisitionDetail detail in details)
            //{
            //    ListViewItem item = new ListViewItem(detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
            //    item.SubItems.Add(detail.ToDate != null ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
            //    item.SubItems.Add(detail.Purpose);
            //    item.SubItems.Add(detail.UnitCost != null ? detail.UnitCost.Value.ToString("N") : "N/A");
            //    item.SubItems.Add(detail.NoOfUnit != null ? detail.NoOfUnit.Value.ToString("N") : "N/A");
            //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
            //    item.SubItems.Add(detail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
            //    item.SubItems.Add(detail.TravelSponsorFinancedDetailAmount != null ? detail.TravelSponsorFinancedDetailAmount.Value.ToString("N") : "N/A");
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
            //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");
            //    item.Tag = detail;
            //    advanceDetailsListView.Items.Add(item);
            //}

            //SetTotalAmountInListView(advanceDetailsListView);

            advanceDetailDataGridView.Rows.Clear();

            foreach (AdvanceTravelRequisitionDetail detail in details)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(advanceDetailDataGridView);

                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = "Remove";
                row.Cells[2].Value =
                    detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
                row.Cells[3].Value = detail.ToDate != null
                    ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy")
                    : string.Empty;
                row.Cells[4].Value = detail.Purpose;
                row.Cells[5].Value = detail.UnitCost != null
                    ? detail.UnitCost.Value.ToString("N")
                    : "N/A";
                row.Cells[6].Value = detail.NoOfUnit != null
                    ? detail.NoOfUnit.Value.ToString("N")
                    : "N/A";
                row.Cells[7].Value = detail.AdvanceAmount.ToString("N");
                row.Cells[8].Value = (detail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
                row.Cells[9].Value = detail.TravelSponsorFinancedDetailAmount != null ? detail.TravelSponsorFinancedDetailAmount.Value.ToString("N") : "N/A";
                row.Cells[10].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                row.Cells[11].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                    ? detail.ReceipientOrPayeeName
                    : "N/A";
                row.Cells[12].Value = !string.IsNullOrEmpty(detail.Justification)
                    ? detail.Justification
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
            try
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
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdvanceRequisitionEntryForOverseasTravelUI_Load(object sender, EventArgs e)
        {
            try
            {
                requisitionDateTime.Value = DateTime.Now;
                LoadTravelCostItemDropDown();
                LoadVatTaxTypeComboBox();
                LoadEmployeeInformation();
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
            }
        }

        private void SetEntitlementAmount()
        {
            var selectedTravelCostItem = travelCostItemComboBox.SelectedItem as CostItem;
            if (selectedTravelCostItem != null && selectedTravelCostItem.Id > 0)
            {
                _entitlementVm = _entitlementManager.GetEntitlementSettingByCriteria((long)_selectedEmployee.RankID, _advanceCategory.Id, selectedTravelCostItem.Id);

                if (_entitlementVm != null)
                {
                    if (!_entitlementVm.IsFullAmountEntitlement)
                    {
                        entitlementTextBox.Text = _entitlementVm.EntitlementAmount.ToString();
                        advanceAmountTextBox.Text = _entitlementVm.EntitlementAmount == null
                            ? string.Empty
                            : (_entitlementVm.EntitlementAmount * Convert.ToDecimal(noOfUnitTextBox.Text)).ToString();
                    }
                    else
                    {
                        entitlementTextBox.Text = @"Fully Entitled";
                        advanceAmountTextBox.Text = string.Empty;
                    }

                }
                else
                {
                    ClearDetailControls();
                }

            }
            else
            {
                ClearDetailControls();
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
            ICollection<AdvanceTravelRequisitionDetail> detailList =
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
                decimal conversionRate = Convert.ToDecimal(conversionRateTextBox.Text);
                AdvanceTravelRequisitionDetail detail = new AdvanceTravelRequisitionDetail();
                if (!string.IsNullOrEmpty(noOfUnitTextBox.Text))
                {
                    detail.NoOfUnit = Convert.ToDouble(noOfUnitTextBox.Text);
                }
                detail.FromDate = fromDetailDateTimePicker.Value;
                detail.ToDate = toDetailDateTimePicker.Value;
                detail.Purpose = travelCostItemComboBox.Text;
                detail.TravelCostItemId = Convert.ToInt64(travelCostItemComboBox.SelectedValue);

                if (!string.IsNullOrEmpty(entitlementTextBox.Text) && !entitlementTextBox.Text.Equals(@"Fully Entitled"))
                {
                    detail.UnitCost = Convert.ToDecimal(entitlementTextBox.Text);
                }

                detail.AdvanceAmount = Convert.ToDecimal(advanceAmountTextBox.Text) * conversionRate;

                if (!string.IsNullOrEmpty(remarksTextBox.Text))
                {
                    detail.Remarks = remarksTextBox.Text;
                }
                detail.IsThirdPartyReceipient = isThirdPartyReceipientCheckBox.Checked;
                if (!string.IsNullOrEmpty(receipientOrPayeeNameTextBox.Text))
                {
                    detail.ReceipientOrPayeeName = receipientOrPayeeNameTextBox.Text;
                }
                detail.TravelSponsorFinancedDetailAmount = sponsorDetailAmountTextBox.Text == string.Empty
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
                    var updateableDetail = item.Tag as AdvanceTravelRequisitionDetail;

                    if (updateableDetail == null)
                    {
                        throw new UiException("There is no item tagged with the detail item!");
                    }

                    updateableDetail.FromDate = detail.FromDate;
                    updateableDetail.ToDate = detail.ToDate;
                    updateableDetail.NoOfUnit = detail.NoOfUnit;
                    updateableDetail.UnitCost = detail.UnitCost;
                    updateableDetail.Purpose = detail.Purpose;
                    updateableDetail.Remarks = detail.Remarks;
                    updateableDetail.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;
                    updateableDetail.TravelCostItemId = detail.TravelCostItemId;
                    updateableDetail.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;
                    updateableDetail.AdvanceAmount = detail.AdvanceAmount;
                    updateableDetail.TravelSponsorFinancedDetailAmount = detail.TravelSponsorFinancedDetailAmount;
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
                                    advanceAmountTextBox.Text = entilementAmount.ToString();
                                    return;
                                }
                            }

                        }
                        else if (justificationAmount == entilementAmount || entilementAmount > justificationAmount)
                        {
                            updateableDetail.Justification = null;
                        }
                    }
                    RemoveTotalAmountFromGridView();
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

                    RemoveTotalAmountFromGridView();
                    advanceDetailDataGridView.Rows.Add(item);
                }
                SetTotalAmountInGridView(advanceDetailDataGridView);
                ClearDetailControls();
                ResetCostItemComboBox();
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

        //    var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
        //    var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
        //    var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;

        //    ListViewItem item = new ListViewItem();
        //    item.Text = string.Empty;
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(totalAdvanceAmount.ToString("N"));
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

            row.Cells[6].Value = @"Total";
            row.Cells[7].Value = totalAdvanceAmount.ToString("N");
            row.Cells[8].Value = totalAdvanceAmountInBDT.ToString("N");
            row.Cells[6].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[6].Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            row.Cells[7].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[8].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[7].Style.Alignment =
                 DataGridViewContentAlignment.MiddleRight;
            row.Cells[8].Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            advanceDetailsGridViewControl.Rows.Add(row);
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

        //private ListViewItem GetNewListViewItemByDetail(AdvanceTravelRequisitionDetail detail)
        //{
        //    ListViewItem item = new ListViewItem(detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
        //    item.SubItems.Add(detail.ToDate != null ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
        //    item.SubItems.Add(detail.Purpose);
        //    item.SubItems.Add(detail.UnitCost != null ? detail.UnitCost.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.NoOfUnit != null ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //    item.SubItems.Add(detail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
        //    item.SubItems.Add(detail.TravelSponsorFinancedDetailAmount != null
        //        ? detail.TravelSponsorFinancedDetailAmount.Value.ToString("N")
        //        : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItemByDetail(AdvanceTravelRequisitionDetail detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(advanceDetailDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
            row.Cells[3].Value = detail.ToDate != null ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy")
                    : string.Empty;
            row.Cells[4].Value = detail.Purpose;
            row.Cells[5].Value = detail.UnitCost != null ?
                    detail.UnitCost.Value.ToString("N")
                    : "N/A";
            row.Cells[6].Value = detail.NoOfUnit != null
                    ? detail.NoOfUnit.Value.ToString("N")
                    : "N/A";
            row.Cells[7].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[8].Value = (detail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
            row.Cells[9].Value = detail.TravelSponsorFinancedDetailAmount != null ? detail.TravelSponsorFinancedDetailAmount.Value.ToString("N") : "N/A";
            row.Cells[10].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[11].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                     ? detail.ReceipientOrPayeeName
                     : "N/A";
            row.Cells[12].Value = !string.IsNullOrEmpty(detail.Justification)
                     ? detail.Justification
                     : "N/A";
            row.Tag = detail;
            return row;
        }

        //private ListViewItem SetChangedListViewItemByDetail(AdvanceTravelRequisitionDetail detail, ListViewItem item)
        //{
        //    item.Text = detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
        //    item.SubItems[1].Text = detail.ToDate != null ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
        //    item.SubItems[2].Text = detail.Purpose;
        //    item.SubItems[3].Text = detail.UnitCost != null ? detail.UnitCost.ToString() : "N/A";
        //    item.SubItems[4].Text = detail.NoOfUnit != null ? detail.NoOfUnit.ToString() : "N/A";
        //    item.SubItems[5].Text = detail.AdvanceAmount.ToString("N");
        //    item.SubItems[6].Text = detail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N");
        //    item.SubItems[7].Text = detail.TravelSponsorFinancedDetailAmount != null
        //        ? detail.TravelSponsorFinancedDetailAmount.Value.ToString("N")
        //        : "N/A";
        //    item.SubItems[8].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[9].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.SubItems[10].Text = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}
        private DataGridViewRow SetChangedGridViewItemByDetail(AdvanceTravelRequisitionDetail detail, DataGridViewRow row)
        {
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
            row.Cells[3].Value = detail.ToDate != null ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy")
                    : string.Empty;
            row.Cells[4].Value = detail.Purpose;
            row.Cells[5].Value = detail.UnitCost != null ?
                    detail.UnitCost.Value.ToString("N")
                    : "N/A";
            row.Cells[6].Value = detail.NoOfUnit != null
                    ? detail.NoOfUnit.Value.ToString("N")
                    : "N/A";
            row.Cells[7].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[8].Value = (detail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
            row.Cells[9].Value = detail.TravelSponsorFinancedDetailAmount != null ? detail.TravelSponsorFinancedDetailAmount.Value.ToString("N") : "N/A";
            row.Cells[10].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[11].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                     ? detail.ReceipientOrPayeeName
                     : "N/A";
            row.Cells[12].Value = !string.IsNullOrEmpty(detail.Justification)
                     ? detail.Justification
                     : "N/A";
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
            sponsorDetailAmountTextBox.Text = string.Empty;
        }

        private void ResetCostItemComboBox()
        {
            travelCostItemComboBox.SelectedIndex = 0;
        }

        private bool IsRequisitionDetailsExist(AdvanceTravelRequisitionDetail detail)
        {
            ICollection<AdvanceTravelRequisitionDetail> detailList = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);
            if (detailList.Any(c => c.TravelCostItemId == detail.TravelCostItemId))
            {
                string errorMessage = "Cost item already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }

        private void yesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            sponsorNameTextBox.Enabled = yesRadioButton.Checked;

            if (!yesRadioButton.Checked)
            {
                sponsorNameTextBox.Text = string.Empty;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                AdvanceTravelRequisitionHeader advanceRequisitionHeader = new AdvanceTravelRequisitionHeader();
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
                advanceRequisitionHeader.RequesterRankId = _selectedEmployee.RankID;
                advanceRequisitionHeader.RequesterSupervisorId = _selectedEmployee.SupervisorID;
                advanceRequisitionHeader.RequesterDepartmentId = _selectedEmployee.DepartmentID;
                advanceRequisitionHeader.Purpose = purposeOfTravelTextBox.Text;
                advanceRequisitionHeader.PlaceOfVisit = placeOfVisitTextBox.Text;
                advanceRequisitionHeader.IsSponsorFinanced = yesRadioButton.Checked;
                if (!string.IsNullOrEmpty(sponsorNameTextBox.Text))
                {
                    advanceRequisitionHeader.SponsorName = sponsorNameTextBox.Text;
                }
                advanceRequisitionHeader.Currency = currencyComboBox.Text;
                advanceRequisitionHeader.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                advanceRequisitionHeader.AdvanceTravelRequisitionDetails = GetAdvanceRequisitionDetailsFromGridView(advanceDetailDataGridView);
                advanceRequisitionHeader.RequisitionDate = requisitionDateTime.Value;
                advanceRequisitionHeader.AdvanceCategoryId = _advanceCategory.Id;
                advanceRequisitionHeader.FromDate = fromDateTimePicker.Value;
                advanceRequisitionHeader.ToDate = toDateTimePicker.Value;
                advanceRequisitionHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
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
                // RemoveDetailItem();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //private List<AdvanceTravelRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        //{
        //    var details = new List<AdvanceTravelRequisitionDetail>();
        //    if (advanceDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in advanceDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvanceTravelRequisitionDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private List<AdvanceTravelRequisitionDetail> GetAdvanceRequisitionDetailsFromGridView(DataGridView advanceDetailsGridViewControl)
        {
            var details = new List<AdvanceTravelRequisitionDetail>();
            if (advanceDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in advanceDetailsGridViewControl.Rows)
                {
                    var detailItem = row.Tag as AdvanceTravelRequisitionDetail;
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                errorMessage += "from date of details cannot go before to date of header." + Environment.NewLine;
            }
            if (!toDetailDateTimePicker.Checked)
            {
                errorMessage += "To date is not selected." + Environment.NewLine;
            }
            if (toDetailDateTimePicker.Value.Date > toDateTimePicker.Value.Date)
            {
                errorMessage += "To date of details cannot exceed to date of header." + Environment.NewLine;
            }
            if ((long)travelCostItemComboBox.SelectedValue == DefaultItem.Value)
            {
                errorMessage += "Travel cost item is not selected." + Environment.NewLine;
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
            if (string.IsNullOrEmpty(purposeOfTravelTextBox.Text))
            {
                errorMessage += "Purpose of travel is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(placeOfVisitTextBox.Text))
            {
                errorMessage += "Place of visit is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(conversionRateTextBox.Text))
            {
                errorMessage += "Conversion rate is not provided." + Environment.NewLine;
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
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //AdvanceTravelRequisitionDetail detail = null;
            //if (advanceDetailsListView.SelectedItems.Count > 0)
            //{
            //    ListViewItem item = advanceDetailsListView.SelectedItems[0];
            //    if (advanceDetailsListView.Items.Count == item.Index + 1)
            //    {
            //        throw new UiException("No item is selected to edit.");
            //    }
            //    detail = item.Tag as AdvanceTravelRequisitionDetail;
            //    if (detail == null)
            //    {
            //        throw new UiException("Item is not tagged");
            //    }
            //    travelCostItemComboBox.Text = detail.Purpose;
            //    fromDetailDateTimePicker.Value = detail.FromDate.Value;
            //    toDetailDateTimePicker.Value = detail.ToDate.Value;
            //    entitlementTextBox.Text = detail.UnitCost == null ? string.Empty : Math.Round(detail.UnitCost.Value).ToString();
            //    noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
            //    remarksTextBox.Text = detail.Remarks;

            //    SetEntitlementAmount();

            //    advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
            //    isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
            //    receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
            //    sponsorDetailAmountTextBox.Text = detail.TravelSponsorFinancedDetailAmount.Equals("N/A")
            //        ? string.Empty
            //        : detail.TravelSponsorFinancedDetailAmount.ToString();
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

        private void sponsorHeaderAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                //RemoveDetailItem();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceDetailsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (advanceDetailDataGridView.SelectedRows.Count > 0)
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
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        AdvanceTravelRequisitionDetail detail = null;
                        if (advanceDetailDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow item = advanceDetailDataGridView.SelectedRows[0];
                            if (advanceDetailDataGridView.Rows.Count == item.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = item.Tag as AdvanceTravelRequisitionDetail;
                            if (detail == null)
                            {
                                throw new UiException("Item is not tagged");
                            }
                            travelCostItemComboBox.Text = detail.Purpose;
                            fromDetailDateTimePicker.Value = detail.FromDate.Value;
                            toDetailDateTimePicker.Value = detail.ToDate.Value;
                            entitlementTextBox.Text = detail.UnitCost == null ? string.Empty : Math.Round(detail.UnitCost.Value).ToString();
                            noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
                            remarksTextBox.Text = detail.Remarks;

                            SetEntitlementAmount();

                            advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                            isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
                            receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                            sponsorDetailAmountTextBox.Text = detail.TravelSponsorFinancedDetailAmount.Equals("N/A")
                                ? string.Empty
                                : detail.TravelSponsorFinancedDetailAmount.ToString();
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
