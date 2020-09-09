using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.ViewModels;
using IDCOLAdvanceModule.UI.Settings;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.UI.Expense
{
    public partial class ExpenseEntryForLocalTravelUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly UserTable _selectedEmployee;
        private readonly ICostItemManager _costItemManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IEntitlementMappingSettingManager _entitlementManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private bool _isUpdateMode;
        private bool _isDetailUpdateMode;
        private DataGridViewRow _updateableDetailItem;
        private AdvanceTravelRequisitionHeader _updateableRequisitionHeader;
        private readonly AdvanceTravelExpenseHeader _updateableExpenseHeader;
        private readonly AdvanceRequisitionHeader _advanceRequisitionHeader;
        private readonly ICollection<ExpenseFile> _expenseFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        private readonly ICollection<AdvanceTravelRequisitionHeader> _requisitionHeaders;
        private string _requisitionNo;
        private EntitlementMappingSettingVM _entitlementVm;
        //private string _justification;
        //private decimal _justificationAmount;


        public ExpenseEntryForLocalTravelUI()
        {
            InitializeComponent();
            _expenseFiles = new List<ExpenseFile>();
            _employeeManager = new EmployeeManager();
            _entitlementManager = new EntitlementMappingSettingManager();
            _costItemManager = new CostItemManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _vatTaxTypeManager = new VatTaxTypeManager();
            _requisitionHeaders = new List<AdvanceTravelRequisitionHeader>();
        }

        public ExpenseEntryForLocalTravelUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public ExpenseEntryForLocalTravelUI(AdvanceTravelExpenseHeader header, AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableExpenseHeader = header;
            if (header.AdvanceTravelRequisitionHeaders != null &&
                header.AdvanceTravelRequisitionHeaders.Any())
            {
                foreach (AdvanceTravelRequisitionHeader requisitionHeader in header.AdvanceTravelRequisitionHeaders)
                {
                    _requisitionHeaders.Add(requisitionHeader);
                }
            }
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            LoadHeaderInformationFromExpense(header);
            LoadDetailsInformationFromExpense(header.AdvanceTravelExpenseDetails.ToList());
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

        public ExpenseEntryForLocalTravelUI(ICollection<AdvanceTravelRequisitionHeader> requisitionHeaders)
            : this()
        {
            _requisitionHeaders = requisitionHeaders;
            DateTime fromDate = _requisitionHeaders.Min(c => c.FromDate);
            DateTime toDate = _requisitionHeaders.Max(c => c.ToDate);
            var advanceTravelRequisitionHeader = _requisitionHeaders.FirstOrDefault();
            if (advanceTravelRequisitionHeader != null)
            {
                long categoryId = advanceTravelRequisitionHeader.AdvanceCategoryId;
                _advanceCategory = _advanceRequisitionCategoryManager.GetById(categoryId);
                _selectedEmployee = _employeeManager.GetByUserName(advanceTravelRequisitionHeader.RequesterUserName);
            }
            if (_requisitionHeaders.Count == 1)
            {
                LoadHeaderInformationFromRequisition(advanceTravelRequisitionHeader);
            }
            else
            {
                LoadHeaderInformationFromRequisition(fromDate, toDate);
            }

            LoadDetailsInformationFromRequisition(_requisitionHeaders);
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

        private void LoadHeaderInformationFromExpense(AdvanceTravelExpenseHeader header)
        {
            fromHeaderDateTimePicker.Value = header.FromDate;
            fromHeaderDateTimePicker.Checked = true;
            toHeaderDateTimePicker.Value = header.ToDate;
            toHeaderDateTimePicker.Checked = true;
            expenseDateTimePicker.Value = header.ExpenseEntryDate;
            expenseDateTimePicker.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            placeOfVisitTextBox.Text = header.PlaceOfVisit;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
            purposeOfTravelTextBox.Text = header.Purpose;
            yesRadioButton.Checked = header.IsSponsorFinanced;
            if (header.IsSponsorFinanced)
            {
                sponsorNameTextBox.Text = header.SponsorName;
            }
        }

        private void LoadTravelCostItemDropDown()
        {
            try
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

        private void LoadHeaderInformationFromRequisition(AdvanceTravelRequisitionHeader header)
        {
            fromHeaderDateTimePicker.Value = header.FromDate;
            fromHeaderDateTimePicker.Checked = true;
            toHeaderDateTimePicker.Value = header.ToDate;
            toHeaderDateTimePicker.Checked = true;
            purposeOfTravelTextBox.Text = header.Purpose;
            placeOfVisitTextBox.Text = header.PlaceOfVisit;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
            yesRadioButton.Checked = header.IsSponsorFinanced;
            if (header.IsSponsorFinanced)
            {
                sponsorNameTextBox.Text = header.SponsorName;
            }
        }

        private void LoadHeaderInformationFromRequisition(DateTime fromDate, DateTime toDate)
        {
            fromHeaderDateTimePicker.Value = fromDate;
            fromHeaderDateTimePicker.Checked = true;
            toHeaderDateTimePicker.Value = toDate;
            toHeaderDateTimePicker.Checked = true;
        }

        //private void LoadDetailsInformationFromRequisition(List<AdvanceTravelRequisitionDetail> details)
        //{
        //    try
        //    {
        //        foreach (AdvanceTravelRequisitionDetail detail in details)
        //        {
        //            ListViewItem item = new ListViewItem(_advanceRequisitionHeader.RequisitionNo);
        //            item.SubItems.Add(detail.AdvanceRequisitionHeader.FromDate.Date.ToString("dd/MM/yyyy"));
        //            item.SubItems.Add(detail.AdvanceRequisitionHeader.ToDate.Date.ToString("dd/MM/yyyy"));
        //            item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty);
        //            var travelCostItem = _costItemManager.GetById((long)detail.TravelCostItemId);
        //            detail.TravelCostItem = travelCostItem;
        //            item.SubItems.Add(detail.TravelCostItem.Name);
        //            item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //            item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //            item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //            item.SubItems.Add((detail.AdvanceAmount - detail.AdvanceAmount).ToString("N"));
        //            item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //            item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //            AdvanceTravelExpenseDetail advancePettyCashExpenseDetail = new AdvanceTravelExpenseDetail
        //            {
        //                FromDate = detail.FromDate.Value,
        //                ToDate = detail.ToDate.Value,
        //                AdvanceRequisitionDetailId = detail.Id,
        //                NoOfUnit = detail.NoOfUnit,
        //                AdvanceAmount = detail.AdvanceAmount,
        //                ExpenseAmount = detail.AdvanceAmount,
        //                Purpose = detail.Purpose,
        //                Remarks = detail.Remarks,
        //                TravelCostItemId = detail.TravelCostItemId,
        //                IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
        //                ReceipientOrPayeeName = detail.ReceipientOrPayeeName
        //            };
        //            item.Tag = advancePettyCashExpenseDetail;

        //            expenseDetailsListView.Items.Add(item);
        //        }

        //        SetTotalAmountInListView(expenseDetailsListView);
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void LoadDetailsInformationFromRequisition(ICollection<AdvanceTravelRequisitionHeader> requisitionHeaders)
        //{
        //    foreach (AdvanceTravelRequisitionHeader header in requisitionHeaders)
        //    {
        //        foreach (AdvanceTravelRequisitionDetail detail in header.AdvanceTravelRequisitionDetails)
        //        {
        //            var expenseAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(header.ConversionRate);
        //            var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(header.ConversionRate);
        //            var sponsorAmountInBDT = detail.TravelSponsorFinancedDetailAmount ?? 0;
        //            var actualExpenseInBDT
        //                = expenseAmountInBDT - sponsorAmountInBDT;
        //            ListViewItem item = new ListViewItem(header.RequisitionNo);
        //            item.SubItems.Add(detail.AdvanceRequisitionHeader.FromDate.Date.ToString("dd/MM/yyyy"));
        //            item.SubItems.Add(detail.AdvanceRequisitionHeader.ToDate.Date.ToString("dd/MM/yyyy"));
        //            item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty);
        //            var travelCostItem = _costItemManager.GetById((long)detail.TravelCostItemId);
        //            detail.TravelCostItem = travelCostItem;
        //            item.SubItems.Add(detail.TravelCostItem.Name);
        //            item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");

        //            item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //            item.SubItems.Add(expenseAmountInBDT.ToString("N"));
        //            item.SubItems.Add(detail.TravelSponsorFinancedDetailAmount == null ? "N/A" : detail.TravelSponsorFinancedDetailAmount.Value.ToString("N"));
        //            item.SubItems.Add(Utility.Utility.GetFormatedAmount(actualExpenseInBDT));
        //            item.SubItems.Add(GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT));
        //            item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //            item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //            item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");
        //            AdvanceTravelExpenseDetail advanceTravelExpenseDetail = new AdvanceTravelExpenseDetail
        //            {
        //                FromDate = detail.FromDate.Value,
        //                ToDate = detail.ToDate.Value,
        //                AdvanceRequisitionDetailId = detail.Id,
        //                NoOfUnit = detail.NoOfUnit,
        //                AdvanceAmount = detail.AdvanceAmount,
        //                ExpenseAmount = detail.AdvanceAmount,
        //                Purpose = detail.Purpose,
        //                Remarks = detail.Remarks,
        //                TravelCostItemId = detail.TravelCostItemId,
        //                IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
        //                ReceipientOrPayeeName = detail.ReceipientOrPayeeName,
        //                TravelSponsorFinancedDetailAmount = detail.TravelSponsorFinancedDetailAmount,
        //                Justification = detail.Justification
        //            };
        //            item.Tag = advanceTravelExpenseDetail;

        //            expenseDetailsListView.Items.Add(item);
        //        }
        //    }
        //    SetTotalAmountInListView(expenseDetailsListView);
        //}

        private void LoadDetailsInformationFromRequisition(ICollection<AdvanceTravelRequisitionHeader> requisitionHeaders)
        {
            foreach (AdvanceTravelRequisitionHeader header in requisitionHeaders)
            {
                foreach (AdvanceTravelRequisitionDetail detail in header.AdvanceTravelRequisitionDetails)
                {
                    var expenseAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(header.ConversionRate);
                    var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(header.ConversionRate);
                    var sponsorAmountInBDT = detail.TravelSponsorFinancedDetailAmount ?? 0;
                    var actualExpenseInBDT
                        = expenseAmountInBDT - sponsorAmountInBDT;

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseDetailsDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = header.RequisitionNo;
                    row.Cells[3].Value = detail.AdvanceRequisitionHeader.FromDate.Date.ToString("dd/MM/yyyy");
                    row.Cells[4].Value = detail.AdvanceRequisitionHeader.ToDate.Date.ToString("dd/MM/yyyy");
                    row.Cells[5].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                        ? detail.NoOfUnit.Value.ToString("N")
                        : string.Empty;
                    var travelCostItem = _costItemManager.GetById((long)detail.TravelCostItemId);
                    detail.TravelCostItem = travelCostItem;
                    row.Cells[6].Value = detail.TravelCostItem.Name;
                    row.Cells[7].Value = detail.UnitCost != null && detail.UnitCost > 0
                        ? detail.UnitCost.Value.ToString("N")
                        : "N/A";
                    row.Cells[8].Value = detail.AdvanceAmount.ToString("N");
                    row.Cells[9].Value = expenseAmountInBDT.ToString("N");
                    row.Cells[10].Value = detail.TravelSponsorFinancedDetailAmount == null
                        ? "N/A"
                        : detail.TravelSponsorFinancedDetailAmount.Value.ToString("N");
                    row.Cells[11].Value = Utility.Utility.GetFormatedAmount(actualExpenseInBDT);
                    row.Cells[12].Value = GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT);
                    row.Cells[13].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                    row.Cells[14].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                        ? detail.ReceipientOrPayeeName
                        : "N/A";
                    row.Cells[15].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";

                    AdvanceTravelExpenseDetail advanceTravelExpenseDetail = new AdvanceTravelExpenseDetail
                    {
                        FromDate = detail.FromDate.Value,
                        ToDate = detail.ToDate.Value,
                        AdvanceRequisitionDetailId = detail.Id,
                        NoOfUnit = detail.NoOfUnit,
                        AdvanceAmount = detail.AdvanceAmount,
                        ExpenseAmount = detail.AdvanceAmount,
                        Purpose = detail.Purpose,
                        Remarks = detail.Remarks,
                        TravelCostItemId = detail.TravelCostItemId,
                        IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
                        ReceipientOrPayeeName = detail.ReceipientOrPayeeName,
                        TravelSponsorFinancedDetailAmount = detail.TravelSponsorFinancedDetailAmount,
                        Justification = detail.Justification
                    };
                    row.Tag = advanceTravelExpenseDetail;

                    expenseDetailsDataGridView.Rows.Add(row);
                }
            }
            SetTotalAmountInGridView(expenseDetailsDataGridView);
        }

        //private void LoadDetailsInformationFromExpense(List<AdvanceTravelExpenseDetail> details)
        //{
        //    foreach (AdvanceTravelExpenseDetail detail in details)
        //    {
        //        string requisitionNo = string.Empty;
        //        if (detail.AdvanceRequisitionDetailId != null)
        //            requisitionNo = detail.AdvanceTravelRequisitionDetail.AdvanceTravelRequisitionHeader.RequisitionNo;
        //        ListViewItem item = new ListViewItem(requisitionNo);
        //        item.SubItems.Add(detail.FromDate.Value.ToString("dd/MM/yyyy"));
        //        item.SubItems.Add(detail.ToDate.Value.ToString("dd/MM/yyyy"));
        //        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty);
        //        item.SubItems.Add(detail.Purpose);
        //        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //        item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
        //        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //        item.SubItems.Add(detail.GetSponsorAmount().ToString("N"));
        //        item.SubItems.Add(Utility.Utility.GetFormatedAmount(detail.GetTotalActualExpenseAmount()));
        //        item.SubItems.Add(detail.GetFormattedReimbursementAmount());
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //        item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");
        //        item.Tag = detail;

        //        expenseDetailsListView.Items.Add(item);
        //    }

        //    SetTotalAmountInListView(expenseDetailsListView);
        //}

        private void LoadDetailsInformationFromExpense(List<AdvanceTravelExpenseDetail> details)
        {
            foreach (AdvanceTravelExpenseDetail detail in details)
            {
                string requisitionNo = string.Empty;
                if (detail.AdvanceRequisitionDetailId != null)
                    requisitionNo = detail.AdvanceTravelRequisitionDetail.AdvanceTravelRequisitionHeader.RequisitionNo;



                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(expenseDetailsDataGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = "Remove";
                row.Cells[2].Value = requisitionNo;
                row.Cells[3].Value = detail.FromDate.Value.ToString("dd/MM/yyyy");
                row.Cells[4].Value = detail.ToDate.Value.ToString("dd/MM/yyyy");
                row.Cells[5].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                    ? detail.NoOfUnit.Value.ToString("N")
                    : string.Empty;
                row.Cells[6].Value = detail.Purpose;
                row.Cells[7].Value = detail.UnitCost != null && detail.UnitCost > 0
                    ? detail.UnitCost.Value.ToString("N")
                    : "N/A";
                row.Cells[8].Value = detail.AdvanceAmount.ToString("N");
                row.Cells[9].Value = detail.ExpenseAmount.ToString("N");
                row.Cells[10].Value = detail.GetSponsorAmount().ToString("N");
                row.Cells[11].Value = Utility.Utility.GetFormatedAmount(detail.GetTotalActualExpenseAmount());
                row.Cells[12].Value = detail.GetFormattedReimbursementAmount();
                row.Cells[13].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                row.Cells[14].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
                row.Cells[15].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
                row.Tag = detail;

                expenseDetailsDataGridView.Rows.Add(row);
            }

            SetTotalAmountInGridView(expenseDetailsDataGridView);
        }

        private decimal GetTotalAmount()
        {
            ICollection<AdvanceTravelExpenseDetail> detailList =
                GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);

            if (detailList.Any())
            {
                return detailList.Sum(c => c.AdvanceAmount);
            }
            return 0;
        }

        private string GetReimburseAmount(decimal expenseAmount, decimal advanceAmount)
        {
            var reimburse = (expenseAmount - advanceAmount);
            if (reimburse >= 0)
                return reimburse.ToString("N");
            return "(" + Math.Abs(reimburse).ToString("N") + ")";
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

        //private List<AdvanceTravelExpenseDetail> GetAdvanceExpenseDetailsFromListView(ListView listView)
        //{
        //    var details = new List<AdvanceTravelExpenseDetail>();
        //    if (listView.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in listView.Items)
        //        {
        //            var detailItem = item.Tag as AdvanceTravelExpenseDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private List<AdvanceTravelExpenseDetail> GetAdvanceExpenseDetailsFromGridView(DataGridView gridView)
        {
            var details = new List<AdvanceTravelExpenseDetail>();
            if (gridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in gridView.Rows)
                {
                    var detailItem = row.Tag as AdvanceTravelExpenseDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
        }

        private void ExpenseEntryForLocalTravelUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseDateTimePicker.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadTravelCostItemDropDown();
                LoadCurrencyComboBox();
                LoadVatTaxTypeComboBox();
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value, noOfDaysTextBox);
                SetDefaultFromDetailDateAndToDetailDate();
                SetFormTitleAndText();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void fromHeaderDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value, noOfUnitTextBox);
                SetDefaultFromDetailDateAndToDetailDate();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toHeaderDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value, noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value, noOfUnitTextBox);
                SetDefaultFromDetailDateAndToDetailDate();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultFromDetailDateAndToDetailDate()
        {
            fromDetailDateTimePicker.Value = fromHeaderDateTimePicker.Value;
            toDetailDateTimePicker.Value = toHeaderDateTimePicker.Value;
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
            //AdvanceTravelExpenseDetail detail = null;
            //if (expenseDetailsListView.SelectedItems.Count > 0)
            //{
            //    ListViewItem item = expenseDetailsListView.SelectedItems[0];
            //    if (expenseDetailsListView.Items.Count == item.Index + 1)
            //    {
            //        throw new UiException("No item is selected to edit.");
            //    }
            //    detail = item.Tag as AdvanceTravelExpenseDetail;
            //    if (detail == null)
            //    {
            //        throw new UiException("Item is not tagged");
            //    }
            //    _requisitionNo = item.Text;
            //    _updateableDetailItem = item;
            //    fromDetailDateTimePicker.Value = detail.FromDate.Value;
            //    toDetailDateTimePicker.Value = detail.ToDate.Value;
            //    noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
            //    remarksTextBox.Text = detail.Remarks;
            //    travelCostItemComboBox.SelectedValue = detail.TravelCostItemId;

            //    SetEntitlementAmount();

            //    advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
            //    expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
            //    remarksTextBox.Text = detail.Remarks;
            //    isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
            //    receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
            //    vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
            //    sponsorAmountTextBox.Text = detail.TravelSponsorFinancedDetailAmount == null
            //        ? string.Empty
            //        : detail.TravelSponsorFinancedDetailAmount.ToString();
            //    if (detail.AdvanceRequisitionDetailId != null && detail.AdvanceRequisitionDetailId.Value > 0)
            //    {
            //        travelCostItemComboBox.Enabled = false;
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
                throw new UiException("There is no data found for changing!");
            }

            _isDetailUpdateMode = true;
            addButton.Text = @"Change";
            _updateableDetailItem = detail;
            saveButton.Visible = false;
            AdvanceTravelExpenseDetail expenseDetail = detail.Tag as AdvanceTravelExpenseDetail;
            if (expenseDetail != null && expenseDetail.AdvanceRequisitionDetailId != null)
            {
                if (expenseDetail.AdvanceRequisitionDetail != null)
                {
                    SetFormToNonUpdateableMode();
                }
            }
        }

        private void SetFormToNonUpdateableMode()
        {
            remarksTextBox.Enabled = false;
            travelCostItemComboBox.Enabled = false;
            entitlementTextBox.Enabled = false;
            advanceAmountTextBox.Enabled = false;
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
            travelCostItemComboBox.SelectedValue = DefaultItem.Value;
            advanceAmountTextBox.Text = string.Empty;
            expenseAmountTextBox.Text = string.Empty;
            remarksTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Checked = false;
            receipientOrPayeeNameTextBox.Text = string.Empty;
            travelCostItemComboBox.Enabled = true;
            isThirdPartyReceipientCheckBox.Enabled = true;
            sponsorAmountTextBox.Text = string.Empty;
            entitlementTextBox.Text = string.Empty;
            addButton.Text = @"Add";
        }

        private bool ValidateAdd()
        {
            string errorMessage = string.Empty;
            if (!fromDetailDateTimePicker.Checked)
            {
                errorMessage += "From date is not selected." + Environment.NewLine;
            }
            if (fromDetailDateTimePicker.Value.Date < fromHeaderDateTimePicker.Value.Date)
            {
                errorMessage += "From date of details cannot go before to date of header." + Environment.NewLine;
            }
            if (fromDetailDateTimePicker.Value.Date > toHeaderDateTimePicker.Value.Date)
            {
                errorMessage += "From date of details cannot exceeds to date of header." + Environment.NewLine;
            }
            if (!toDetailDateTimePicker.Checked)
            {
                errorMessage += "To date is not selected." + Environment.NewLine;
            }
            if (toDetailDateTimePicker.Value.Date > toHeaderDateTimePicker.Value.Date)
            {
                errorMessage += "To date of details cannot exceed to date of header." + Environment.NewLine;
            }
            if (toDetailDateTimePicker.Value.Date < fromHeaderDateTimePicker.Value.Date)
            {
                errorMessage += "To date of details cannot go before from date of header." + Environment.NewLine;
            }
            if ((long)travelCostItemComboBox.SelectedValue == -1)
            {
                errorMessage += "Travel cost item is not selected." + Environment.NewLine;
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
                decimal conversionRate = Convert.ToDecimal(conversionRateTextBox.Text);
                AdvanceTravelExpenseDetail detail = new AdvanceTravelExpenseDetail();
                detail.FromDate = fromDetailDateTimePicker.Value;
                detail.ToDate = toDetailDateTimePicker.Value;
                if (!string.IsNullOrEmpty(noOfUnitTextBox.Text))
                {
                    detail.NoOfUnit = Convert.ToDouble(noOfUnitTextBox.Text);
                }
                detail.Purpose = travelCostItemComboBox.Text;
                detail.TravelCostItemId = Convert.ToInt64(travelCostItemComboBox.SelectedValue);
                if (!string.IsNullOrEmpty(entitlementTextBox.Text) && !entitlementTextBox.Text.Equals(@"Fully Entitled"))
                {
                    detail.UnitCost = Convert.ToDecimal(entitlementTextBox.Text);
                }
                if (!string.IsNullOrEmpty(advanceAmountTextBox.Text))
                {
                    detail.AdvanceAmount = Convert.ToDecimal(advanceAmountTextBox.Text) * conversionRate;
                }
                if (!string.IsNullOrEmpty(remarksTextBox.Text))
                {
                    detail.Remarks = remarksTextBox.Text;
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
                detail.TravelSponsorFinancedDetailAmount = sponsorAmountTextBox.Text == string.Empty
                    ? (decimal?)null
                    : Convert.ToDecimal(sponsorAmountTextBox.Text);
                if (!ValidateCeilingAmount())
                {
                    DialogResult result = MessageBox.Show(@"You are about to exceed the ceiling amount (" + _advanceCategory.CeilingAmount + "). Do you want to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                    var updateDetailItem = item.Tag as AdvanceTravelExpenseDetail;
                    if (updateDetailItem == null)
                    {
                        throw new UiException("There is no item tagged with the detail item.");
                    }
                    _requisitionNo = (string) item.Cells[2].Value;
                    detail.AdvanceRequisitionDetail = updateDetailItem.AdvanceRequisitionDetail;
                    updateDetailItem.TravelCostItemId = Convert.ToInt64(travelCostItemComboBox.SelectedValue);
                    updateDetailItem.Purpose = detail.Purpose;
                    updateDetailItem.AdvanceAmount = detail.AdvanceAmount;
                    updateDetailItem.ExpenseAmount = detail.ExpenseAmount;
                    updateDetailItem.Remarks = detail.Remarks;
                    updateDetailItem.NoOfUnit = detail.NoOfUnit;
                    updateDetailItem.UnitCost = detail.UnitCost;

                    decimal justificationAmount = Convert.ToDecimal(expenseAmountTextBox.Text);
                    decimal entilementAmount = 0;
                    if (updateDetailItem.UnitCost != null && updateDetailItem.NoOfUnit != null)
                    {
                        entilementAmount = updateDetailItem.UnitCost.Value * (decimal)updateDetailItem.NoOfUnit.Value;
                    }
                    if (entilementAmount != 0)
                    {
                        if (justificationAmount > entilementAmount)
                        {
                            if (!string.IsNullOrEmpty(updateDetailItem.Justification))
                            {
                                JustificationEntryUI justification = new JustificationEntryUI(updateDetailItem.Justification, entilementAmount);
                                justification.ShowDialog();
                                updateDetailItem.Justification = justification.Info;
                                if (updateDetailItem.Justification == null)
                                {

                                    return;
                                }
                            }
                            else
                            {
                                JustificationEntryUI justification = new JustificationEntryUI(entilementAmount);
                                justification.ShowDialog();
                                updateDetailItem.Justification = justification.Info;
                                if (updateDetailItem.Justification == null)
                                {
                                    return;
                                }
                            }

                        }
                        else if (justificationAmount == entilementAmount || entilementAmount > justificationAmount)
                        {
                            updateDetailItem.Justification = null;
                        }
                    }
                    RemoveTotalAmountFromGridView();
                    SetChangedGridViewItemByDetail(updateDetailItem, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    decimal justificationAmount = Convert.ToDecimal(expenseAmountTextBox.Text);
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
                                expenseAmountTextBox.Text = entilementAmount.ToString();
                                return;
                            }
                        }
                    }
                    var item = GetNewGridViewItemByDetail(detail);
                    RemoveTotalAmountFromGridView();
                    expenseDetailsDataGridView.Rows.Add(item);
                }
                SetTotalAmountInGridView(expenseDetailsDataGridView);
                ClearDetailControl();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetRecipientWiseReimbursementRefund(DataGridView gridView)
        {
            ICollection<AdvanceTravelExpenseDetail> details = GetAdvanceExpenseDetailsFromGridView(gridView);

            List<RecipientWithReimbursementRefund> recipientWiseReimbursementRefund = new List<RecipientWithReimbursementRefund>();

            foreach (AdvanceTravelExpenseDetail detail in details)
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

        //    var totalAdvanceAmountInBDT = details.Sum(c => c.AdvanceAmount);
        //    var totalExpenseAmountInBDT = details.Sum(c => c.ExpenseAmount);
        //    var totalSponsorAmountInBDT = details.Sum(c => c.TravelSponsorFinancedDetailAmount) ?? 0;
        //    var actualExpenseInBDT = totalExpenseAmountInBDT - totalSponsorAmountInBDT;

        //    ListViewItem item = new ListViewItem();
        //    item.Text = string.Empty;
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(totalExpenseAmountInBDT.ToString("N"));
        //    item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
        //    item.SubItems.Add(totalSponsorAmountInBDT.ToString("N"));
        //    item.SubItems.Add(actualExpenseInBDT.ToString("N"));
        //    item.SubItems.Add(GetReimburseAmount(actualExpenseInBDT, totalAdvanceAmountInBDT));
        //    item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
        //    listView.Items.Add(item);
        //    SetRecipientWiseReimbursementRefund(listView);
        //}


        private void SetTotalAmountInGridView(DataGridView gridView)
        {
            var details = GetAdvanceExpenseDetailsFromGridView(gridView);

            var totalAdvanceAmountInBDT = details.Sum(c => c.AdvanceAmount);
            var totalExpenseAmountInBDT = details.Sum(c => c.ExpenseAmount);
            var totalSponsorAmountInBDT = details.Sum(c => c.TravelSponsorFinancedDetailAmount) ?? 0;
            var actualExpenseInBDT = totalExpenseAmountInBDT - totalSponsorAmountInBDT;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gridView);
            row.Cells[0] = new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();

            row.Cells[8].Value = "Total";
            row.Cells[9].Value = totalExpenseAmountInBDT.ToString("N");
            row.Cells[10].Value = totalAdvanceAmountInBDT.ToString("N");
            row.Cells[11].Value = totalSponsorAmountInBDT.ToString("N");
            row.Cells[12].Value = actualExpenseInBDT.ToString("N");
            row.Cells[12].Value = GetReimburseAmount(actualExpenseInBDT, totalAdvanceAmountInBDT);

            row.Cells[8].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            row.Cells[9].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            row.Cells[10].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            row.Cells[11].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            row.Cells[12].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            row.Cells[13].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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


        //private ListViewItem SetChangedListViewItemByDetail(AdvanceTravelExpenseDetail detail, ListViewItem item)
        //{
        //    var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(conversionRateTextBox.Text);
        //    var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(conversionRateTextBox.Text);
        //    var sponsorAmountInBDT = detail.TravelSponsorFinancedDetailAmount ?? 0;
        //    var actualExpenseInBDT
        //        = expenseAmountInBDT - sponsorAmountInBDT;

        //    item.Text = _requisitionNo;
        //    item.SubItems[1].Text = detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
        //    item.SubItems[2].Text = detail.ToDate != null ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
        //    item.SubItems[3].Text = detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A";
        //    item.SubItems[4].Text = detail.Purpose;
        //    item.SubItems[5].Text = detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A";
        //    item.SubItems[6].Text = detail.ExpenseAmount.ToString("N");
        //    item.SubItems[7].Text = detail.AdvanceAmount.ToString("N");
        //    item.SubItems[8].Text = sponsorAmountInBDT.ToString("N");
        //    item.SubItems[9].Text = actualExpenseInBDT.ToString("N");
        //    item.SubItems[10].Text = GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT);
        //    item.SubItems[11].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[12].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.SubItems[13].Text = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(AdvanceTravelExpenseDetail detail, DataGridViewRow row)
        {
            var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(conversionRateTextBox.Text);
            var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(conversionRateTextBox.Text);
            var sponsorAmountInBDT = detail.TravelSponsorFinancedDetailAmount ?? 0;
            var actualExpenseInBDT
                = expenseAmountInBDT - sponsorAmountInBDT;
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.FromDate != null ? detail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
            row.Cells[4].Value = detail.ToDate != null ? detail.ToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty;
            row.Cells[5].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A";
            row.Cells[6].Value = detail.Purpose;
            row.Cells[7].Value = detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A";

            row.Cells[8].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[9].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[10].Value = sponsorAmountInBDT.ToString("N");
            row.Cells[11].Value = actualExpenseInBDT.ToString("N");
            row.Cells[12].Value = GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT);
            row.Cells[13].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[14].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
            row.Cells[15].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
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

        private bool IsRequisitionDetailsExist(AdvanceTravelExpenseDetail detail)
        {
            ICollection<AdvanceTravelExpenseDetail> detailList = GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
            if (detailList.Any(c => c.TravelCostItemId == detail.TravelCostItemId))
            {
                string errorMessage = "Cost item already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }

        //private ListViewItem GetNewListViewItemByDetail(AdvanceTravelExpenseDetail detail)
        //{
        //    var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(conversionRateTextBox.Text);
        //    var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(conversionRateTextBox.Text);
        //    var sponsorAmountInBDT = detail.TravelSponsorFinancedDetailAmount ?? 0;
        //    var actualExpenseInBDT
        //        = expenseAmountInBDT - sponsorAmountInBDT;

        //    ListViewItem item = new ListViewItem(_requisitionNo);
        //    item.SubItems.Add(detail.FromDate != null ? detail.FromDate.Value.ToString("dd/MM/yyyy") : string.Empty);
        //    item.SubItems.Add(detail.ToDate != null ? detail.ToDate.Value.ToString("dd/MM/yyyy") : string.Empty);
        //    item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.Purpose);
        //    item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
        //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //    item.SubItems.Add(detail.TravelSponsorFinancedDetailAmount == null ? "N/A" : detail.TravelSponsorFinancedDetailAmount.Value.ToString("N"));
        //    item.SubItems.Add(actualExpenseInBDT.ToString("N"));
        //    item.SubItems.Add(GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT));
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItemByDetail(AdvanceTravelExpenseDetail detail)
        {
            var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(conversionRateTextBox.Text);
            var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(conversionRateTextBox.Text);
            var sponsorAmountInBDT = detail.TravelSponsorFinancedDetailAmount ?? 0;
            var actualExpenseInBDT
                = expenseAmountInBDT - sponsorAmountInBDT;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(expenseDetailsDataGridView);
            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.FromDate != null ? detail.FromDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            row.Cells[4].Value = detail.ToDate != null ? detail.ToDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            row.Cells[5].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0? detail.NoOfUnit.Value.ToString("N"): "N/A";
            row.Cells[6].Value = detail.Purpose;
            row.Cells[7].Value = detail.UnitCost != null && detail.UnitCost > 0
                ? detail.UnitCost.Value.ToString("N")
                : "N/A";
            row.Cells[8].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[9].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[10].Value = detail.TravelSponsorFinancedDetailAmount == null ? "N/A" : detail.TravelSponsorFinancedDetailAmount.Value.ToString("N");
            row.Cells[11].Value = actualExpenseInBDT.ToString("N");
            row.Cells[12].Value = GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT);
            row.Cells[13].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[14].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
            row.Cells[15].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
            row.Tag = detail;
            return row;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                AdvanceTravelExpenseHeader advanceExpenseHeader = new AdvanceTravelExpenseHeader();
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
                advanceExpenseHeader.Purpose = purposeOfTravelTextBox.Text;
                advanceExpenseHeader.PlaceOfVisit = placeOfVisitTextBox.Text;
                advanceExpenseHeader.IsSponsorFinanced = yesRadioButton.Checked;
                if (!string.IsNullOrEmpty(sponsorNameTextBox.Text))
                {
                    advanceExpenseHeader.SponsorName = sponsorNameTextBox.Text;
                }
                advanceExpenseHeader.ExpenseEntryDate = expenseDateTimePicker.Value;
                advanceExpenseHeader.AdvanceTravelExpenseDetails = GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
                advanceExpenseHeader.AdvanceCategoryId = _advanceCategory.Id;
                advanceExpenseHeader.FromDate = fromHeaderDateTimePicker.Value;
                advanceExpenseHeader.ToDate = toHeaderDateTimePicker.Value;
                advanceExpenseHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                advanceExpenseHeader.Currency = currencyComboBox.Text;
                advanceExpenseHeader.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                if (_requisitionHeaders != null)
                {
                    advanceExpenseHeader.AdvanceTravelRequisitionHeaders = _requisitionHeaders;
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
                        MessageBox.Show(@"Expense requested successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Expense request failed.", @"Error!", MessageBoxButtons.OK,
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if (!expenseDateTimePicker.Checked)
            {
                errorMessage += "Expense entry date is not provided." + Environment.NewLine;
            }
            if (!fromHeaderDateTimePicker.Checked)
            {
                errorMessage += "From date is not provided." + Environment.NewLine;
            }
            if (!toHeaderDateTimePicker.Checked)
            {
                errorMessage += "To date is not provided." + Environment.NewLine;
            }
            if (toHeaderDateTimePicker.Value.Date < toDetailDateTimePicker.Value.Date)
            {
                errorMessage += "To date of details cannot exceed to date of header." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(noOfDaysTextBox.Text))
            {
                errorMessage += "No. of day(s) is not provided." + Environment.NewLine;
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
            if (expenseDetailsDataGridView.Rows.Count < 1)
            {
                errorMessage += "Could not proceed further! As you have not added any requisition detail. " + Environment.NewLine;
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

        private void noOfUnitTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetDefaultExpenseAmount();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultExpenseAmount()
        {
            if (!string.IsNullOrEmpty(entitlementTextBox.Text) && !string.IsNullOrEmpty(noOfUnitTextBox.Text) && !entitlementTextBox.Text.Equals("Fully Entitled"))
            {
                decimal entitlementAmount = Convert.ToDecimal(entitlementTextBox.Text);
                decimal noOfUnit = Convert.ToDecimal(noOfUnitTextBox.Text);
                decimal expenseAmount = entitlementAmount * noOfUnit;
                expenseAmountTextBox.Text = expenseAmount.ToString();
            }
            if (string.IsNullOrEmpty(noOfUnitTextBox.Text))
            {
                expenseAmountTextBox.Text = string.Empty;
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
                        expenseAmountTextBox.Text = _entitlementVm.EntitlementAmount == null
                            ? string.Empty
                            : (_entitlementVm.EntitlementAmount * Convert.ToDecimal(noOfUnitTextBox.Text)).ToString();
                    }
                    else
                    {
                        entitlementTextBox.Text = @"Fully Entitled";
                        expenseAmountTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    ClearDetailControls();
                }
                SetDefaultExpenseAmount();
            }
            else
            {
                ClearDetailControls();
            }
        }

        private void ClearDetailControls()
        {
            expenseAmountTextBox.Text = string.Empty;
            Utility.Utility.CalculateNoOfDays(fromDetailDateTimePicker.Value, toDetailDateTimePicker.Value, noOfUnitTextBox);
            entitlementTextBox.Text = string.Empty;
        }

        private void expenseAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void yesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            sponsorNameTextBox.Enabled = yesRadioButton.Checked;
            if (!yesRadioButton.Checked)
            {
                sponsorNameTextBox.Text = string.Empty;
                ;
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
                        AdvanceTravelExpenseDetail detail = null;
                        if (expenseDetailsDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow row = expenseDetailsDataGridView.SelectedRows[0];
                            if (expenseDetailsDataGridView.Rows.Count == row.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = row.Tag as AdvanceTravelExpenseDetail;
                            if (detail == null)
                            {
                                throw new UiException("Item is not tagged");
                            }
                            _requisitionNo = (string)row.Cells[2].Value;
                            _updateableDetailItem = row;
                            fromDetailDateTimePicker.Value = detail.FromDate.Value;
                            toDetailDateTimePicker.Value = detail.ToDate.Value;
                            noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
                            remarksTextBox.Text = detail.Remarks;
                            travelCostItemComboBox.SelectedValue = detail.TravelCostItemId;

                            SetEntitlementAmount();

                            advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                            expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
                            remarksTextBox.Text = detail.Remarks;
                            isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
                            receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                            vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
                            sponsorAmountTextBox.Text = detail.TravelSponsorFinancedDetailAmount == null
                                ? string.Empty
                                : detail.TravelSponsorFinancedDetailAmount.ToString();
                            if (detail.AdvanceRequisitionDetailId != null && detail.AdvanceRequisitionDetailId.Value > 0)
                            {
                                travelCostItemComboBox.Enabled = false;
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
