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
using IDCOLAdvanceModule.UI.Settings;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.Model.ViewModels;

namespace IDCOLAdvanceModule.UI.Expense
{
    public partial class ExpenseEntryForOverseasTravelUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly ICostItemManager _costItemManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly AdvanceOverseasTravelRequisitionHeader _advanceRequisitionHeader;
        private bool _isUpdateMode;
        private bool _isViewMode;
        private bool _isDetailUpdateMode;
        private DataGridViewRow _updateableDetailItem;
        private readonly AdvanceOverseasTravelExpenseHeader _updateableExpenseHeader;
        private readonly OverseasTravellingAllowanceManager _overseasTravellingAllowanceManager;
        private readonly IPlaceofVistManager _placeofVistManager;
        private List<CurrencyConversionRateDetail> _currencyConversionRateDetails;
        private readonly ICurrencyConversionRateDetailManager _currencyConversionRateDetailManager;
        private readonly ICollection<ExpenseFile> _expenseFiles;
        private readonly IVatTaxTypeManager _vatTaxTypeManager;
        private readonly ICollection<AdvanceOverseasTravelRequisitionHeader> _requisitionHeaders;
        private string _requisitionNo;
        public ExpenseEntryForOverseasTravelUI()
        {
            InitializeComponent();
            _expenseFiles = new List<ExpenseFile>();
            _employeeManager = new EmployeeManager();
            _costItemManager = new CostItemManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _overseasTravellingAllowanceManager = new OverseasTravellingAllowanceManager();
            _placeofVistManager = new PlaceOfVisitManager();
            _currencyConversionRateDetailManager = new CurrencyConversionRateDetailManager();
            _currencyConversionRateDetails = new List<CurrencyConversionRateDetail>();
            _vatTaxTypeManager = new VatTaxTypeManager();
            _requisitionHeaders = new List<AdvanceOverseasTravelRequisitionHeader>();
            LoadTravelCostItemDropDown();
            LoadVatTaxTypeComboBox();
            LoadPlaceOfVisit();
        }

        public ExpenseEntryForOverseasTravelUI(ICollection<AdvanceOverseasTravelRequisitionHeader> requisitionHeaders)
            : this()
        {
            _requisitionHeaders = requisitionHeaders;
            DateTime fromDate = _requisitionHeaders.Min(c => c.FromDate);
            DateTime toDate = _requisitionHeaders.Max(c => c.ToDate);
            var advanceOverseasTravelRequisitionHeader = _requisitionHeaders.FirstOrDefault();
            if (advanceOverseasTravelRequisitionHeader != null)
            {
                long categoryId = advanceOverseasTravelRequisitionHeader.AdvanceCategoryId;
                _advanceCategory = _advanceRequisitionCategoryManager.GetById(categoryId);
                _selectedEmployee = _employeeManager.GetByUserName(advanceOverseasTravelRequisitionHeader.RequesterUserName);
            }
            if (_requisitionHeaders.Count == 1)
            {
                LoadHeaderInformationFromRequisition(advanceOverseasTravelRequisitionHeader);
            }
            else
            {
                LoadHeaderInformationFromRequisition(fromDate, toDate);
            }
            LoadDetailsInformationFromRequisition(_requisitionHeaders);
        }

        public ExpenseEntryForOverseasTravelUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public ExpenseEntryForOverseasTravelUI(AdvanceOverseasTravelExpenseHeader header,
            AdvancedFormMode advancedFormMode)
            : this()
        {
            _updateableExpenseHeader = header;
            if (header.AdvanceOverseasTravelRequisitionHeaders != null &&
                header.AdvanceOverseasTravelRequisitionHeaders.Any())
            {
                foreach (AdvanceOverseasTravelRequisitionHeader requisitionHeader in header.AdvanceOverseasTravelRequisitionHeaders)
                {
                    _requisitionHeaders.Add(requisitionHeader);
                }
            }
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            LoadHeaderInformation(header);
            LoadCurrencyConversationDetails();
            LoadDetailsInformationFromExpense(header.AdvanceOverseasTravelExpenseDetails.ToList());
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

        private void LoadCurrencyConversationDetails()
        {
            if (_updateableExpenseHeader != null && _updateableExpenseHeader.Id > 0)
            {
                _currencyConversionRateDetails =
                    _currencyConversionRateDetailManager.GetByExpense(_updateableExpenseHeader.Id).ToList();
            }
        }

        private void LoadDetailsInformationFromRequisition(List<AdvanceOverseasTravelRequisitionDetail> details)
        {
            foreach (AdvanceOverseasTravelRequisitionDetail detail in details)
            {
                //ListViewItem item = new ListViewItem(_advanceRequisitionHeader.RequisitionNo);
                //item.SubItems.Add(detail.AdvanceRequisitionHeader.FromDate.Date.ToString("dd/MM/yyyy"));
                //item.SubItems.Add(detail.AdvanceRequisitionHeader.ToDate.Date.ToString("dd/MM/yyyy"));
                //item.SubItems.Add(detail.NoOfUnit != null ? detail.NoOfUnit.ToString() : string.Empty);
                //var overseasTravelCostItem = _costItemManager.GetById((long)detail.OverseasTravelCostItemId);
                //detail.OverseasTravelCostItem = overseasTravelCostItem;
                //item.SubItems.Add(detail.OverseasTravelCostItem.Name);
                //item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                //item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                //item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                //item.SubItems.Add(detail.Currency);
                //item.SubItems.Add(detail.ConversionRate.ToString("N"));
                //var advanceAmountInBDT = detail.AdvanceAmount *
                //                         (decimal)detail.ConversionRate;
                //var expenseAmountInBDT = advanceAmountInBDT;
                //var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount ?? 0;
                //var actualExpenseInBDT = expenseAmountInBDT - sponsorAmountInBDT;
                //item.SubItems.Add(expenseAmountInBDT.ToString("N"));
                //item.SubItems.Add(advanceAmountInBDT.ToString("N"));
                //item.SubItems.Add(detail.OverseasSponsorFinancedDetailAmount == null ? "N/A" : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N"));
                //item.SubItems.Add(actualExpenseInBDT.ToString("N"));
                //item.SubItems.Add(GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT));
                //item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                //item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
                //AdvanceOverseasTravelExpenseDetail advanceOverseasTravelExpenseDetail = new AdvanceOverseasTravelExpenseDetail
                //{
                //    OverseasFromDate = detail.OverseasFromDate,
                //    OverseasToDate = detail.OverseasToDate,
                //    AdvanceRequisitionDetailId = detail.Id,
                //    NoOfUnit = detail.NoOfUnit,
                //    AdvanceAmount = detail.AdvanceAmount,
                //    ExpenseAmount = detail.AdvanceAmount,
                //    Purpose = detail.OverseasTravelCostItem.Name,
                //    Remarks = detail.Remarks,
                //    OverseasTravelCostItemId = detail.OverseasTravelCostItemId,
                //    UnitCost = detail.UnitCost,
                //    IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
                //    ReceipientOrPayeeName = detail.ReceipientOrPayeeName,
                //    ConversionRate = detail.ConversionRate,
                //    Currency = detail.Currency,
                //    OverseasSponsorFinancedDetailAmount = detail.OverseasSponsorFinancedDetailAmount
                //};
                //item.Tag = advanceOverseasTravelExpenseDetail;

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(expenseDetailsDataGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = "Remove";
                row.Cells[2].Value = _advanceRequisitionHeader.RequisitionNo;
                row.Cells[3].Value = detail.AdvanceRequisitionHeader.FromDate.Date.ToString("dd/MM/yyyy");
                row.Cells[4].Value = detail.AdvanceRequisitionHeader.ToDate.Date.ToString("dd/MM/yyyy");
                row.Cells[5].Value = detail.NoOfUnit != null ? detail.NoOfUnit.ToString() : string.Empty;
                var overseasTravelCostItem = _costItemManager.GetById((long)detail.OverseasTravelCostItemId);
                detail.OverseasTravelCostItem = overseasTravelCostItem;
                row.Cells[6].Value = detail.OverseasTravelCostItem.Name;
                row.Cells[7].Value = detail.UnitCost != null && detail.UnitCost > 0
                    ? detail.UnitCost.Value.ToString("N")
                    : "N/A";
                row.Cells[8].Value = detail.AdvanceAmount.ToString("N");
                row.Cells[9].Value = detail.AdvanceAmount.ToString("N");
                row.Cells[10].Value = detail.Currency;
                row.Cells[11].Value = detail.ConversionRate.ToString("N");

                var advanceAmountInBDT = detail.AdvanceAmount *
                                        (decimal)detail.ConversionRate;
                var expenseAmountInBDT = advanceAmountInBDT;
                var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount ?? 0;
                var actualExpenseInBDT = expenseAmountInBDT - sponsorAmountInBDT;

                row.Cells[12].Value = expenseAmountInBDT.ToString("N");
                row.Cells[13].Value = advanceAmountInBDT.ToString("N");
                row.Cells[14].Value = detail.OverseasSponsorFinancedDetailAmount == null
                    ? "N/A"
                    : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N");
                row.Cells[15].Value = actualExpenseInBDT.ToString("N");
                row.Cells[16].Value = GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT);
                row.Cells[17].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                row.Cells[18].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                    ? detail.ReceipientOrPayeeName
                    : "N/A";

                AdvanceOverseasTravelExpenseDetail advanceOverseasTravelExpenseDetail = new AdvanceOverseasTravelExpenseDetail
                {
                    OverseasFromDate = detail.OverseasFromDate,
                    OverseasToDate = detail.OverseasToDate,
                    AdvanceRequisitionDetailId = detail.Id,
                    NoOfUnit = detail.NoOfUnit,
                    AdvanceAmount = detail.AdvanceAmount,
                    ExpenseAmount = detail.AdvanceAmount,
                    Purpose = detail.OverseasTravelCostItem.Name,
                    Remarks = detail.Remarks,
                    OverseasTravelCostItemId = detail.OverseasTravelCostItemId,
                    UnitCost = detail.UnitCost,
                    IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
                    ReceipientOrPayeeName = detail.ReceipientOrPayeeName,
                    ConversionRate = detail.ConversionRate,
                    Currency = detail.Currency,
                    OverseasSponsorFinancedDetailAmount = detail.OverseasSponsorFinancedDetailAmount
                };
                row.Tag = advanceOverseasTravelExpenseDetail;

                expenseDetailsDataGridView.Rows.Add(row);
            }

            SetTotalAmountInGridView(expenseDetailsDataGridView);
        }

        private void LoadDetailsInformationFromRequisition(ICollection<AdvanceOverseasTravelRequisitionHeader> requisitionHeaders)
        {
            foreach (AdvanceOverseasTravelRequisitionHeader header in requisitionHeaders)
            {
                foreach (AdvanceOverseasTravelRequisitionDetail detail in header.AdvanceOverseasTravelRequisitionDetails)
                {
                    //ListViewItem item = new ListViewItem(header.RequisitionNo);
                    //item.SubItems.Add(detail.AdvanceRequisitionHeader.FromDate.Date.ToString("dd/MM/yyyy"));
                    //item.SubItems.Add(detail.AdvanceRequisitionHeader.ToDate.Date.ToString("dd/MM/yyyy"));
                    //item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty);
                    //var overseasTravelCostItem = _costItemManager.GetById((long)detail.OverseasTravelCostItemId);
                    //detail.OverseasTravelCostItem = overseasTravelCostItem;
                    //item.SubItems.Add(detail.OverseasTravelCostItem.Name);
                    //item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                    //item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                    //item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                    //item.SubItems.Add(detail.Currency);
                    //item.SubItems.Add(detail.ConversionRate.ToString("N"));
                    //var advanceAmountInBDT = detail.GetAdvanceAmountInBdt();
                    //var expenseAmountInBDT = detail.GetAdvanceAmountInBdt();
                    //var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount ?? 0;
                    //var actualExpenseInBDT = expenseAmountInBDT - sponsorAmountInBDT;
                    //item.SubItems.Add(expenseAmountInBDT.ToString("N"));
                    //item.SubItems.Add(advanceAmountInBDT.ToString("N"));
                    //item.SubItems.Add(detail.OverseasSponsorFinancedDetailAmount == null ? "N/A" : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N"));
                    //item.SubItems.Add(actualExpenseInBDT.ToString("N"));
                    //item.SubItems.Add(GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT));
                    //item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                    //item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
                    //item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseDetailsDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = header.RequisitionNo;
                    row.Cells[3].Value = detail.AdvanceRequisitionHeader.FromDate.Date.ToString("dd/MM/yyyy");
                    row.Cells[4].Value = detail.AdvanceRequisitionHeader.ToDate.Date.ToString("dd/MM/yyyy");
                    row.Cells[5].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty;

                    var overseasTravelCostItem = _costItemManager.GetById((long)detail.OverseasTravelCostItemId);
                    detail.OverseasTravelCostItem = overseasTravelCostItem;

                    row.Cells[6].Value = detail.OverseasTravelCostItem.Name;
                    row.Cells[7].Value = detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A";
                    row.Cells[8].Value = detail.AdvanceAmount.ToString("N");
                    row.Cells[9].Value = detail.AdvanceAmount.ToString("N");
                    row.Cells[10].Value = detail.Currency;
                    row.Cells[11].Value = detail.ConversionRate.ToString("N");

                    var advanceAmountInBDT = detail.GetAdvanceAmountInBdt();
                    var expenseAmountInBDT = detail.GetAdvanceAmountInBdt();
                    var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount ?? 0;
                    var actualExpenseInBDT = expenseAmountInBDT - sponsorAmountInBDT;

                    row.Cells[12].Value = expenseAmountInBDT.ToString("N");
                    row.Cells[13].Value = advanceAmountInBDT.ToString("N");
                    row.Cells[14].Value = detail.OverseasSponsorFinancedDetailAmount == null ? "N/A" : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N");
                    row.Cells[15].Value = actualExpenseInBDT.ToString("N");
                    row.Cells[16].Value = GetReimburseAmount(actualExpenseInBDT, advanceAmountInBDT);
                    row.Cells[17].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
                    row.Cells[18].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
                    row.Cells[18].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
                    AdvanceOverseasTravelExpenseDetail advanceTravelExpenseDetail = new AdvanceOverseasTravelExpenseDetail
                    {
                        OverseasFromDate = detail.OverseasFromDate,
                        OverseasToDate = detail.OverseasToDate,
                        AdvanceRequisitionDetailId = detail.Id,
                        NoOfUnit = detail.NoOfUnit,
                        AdvanceAmount = detail.AdvanceAmount,
                        ExpenseAmount = detail.AdvanceAmount,
                        Purpose = detail.OverseasTravelCostItem.Name,
                        Remarks = detail.Remarks,
                        OverseasTravelCostItemId = detail.OverseasTravelCostItemId,
                        UnitCost = detail.UnitCost,
                        IsThirdPartyReceipient = detail.IsThirdPartyReceipient,
                        ReceipientOrPayeeName = detail.ReceipientOrPayeeName,
                        ConversionRate = detail.ConversionRate,
                        Currency = detail.Currency,
                        OverseasSponsorFinancedDetailAmount = detail.OverseasSponsorFinancedDetailAmount,
                        Justification = detail.Justification
                    };
                    row.Tag = advanceTravelExpenseDetail;

                    expenseDetailsDataGridView.Rows.Add(row);
                }
            }
            SetTotalAmountInGridView(expenseDetailsDataGridView);
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

        private void LoadDetailsInformationFromExpense(ICollection<AdvanceOverseasTravelExpenseDetail> details)
        {
            try
            {
                expenseDetailsDataGridView.Rows.Clear();

                foreach (AdvanceOverseasTravelExpenseDetail detail in details)
                {
                    DataGridViewRow row = GetNewGridViewItemByDetail(detail);
                    row.Tag = detail;
                    expenseDetailsDataGridView.Rows.Add(row);
                }

                SetTotalAmountInGridView(expenseDetailsDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHeaderInformationFromRequisition(AdvanceOverseasTravelRequisitionHeader header)
        {
            fromHeaderDateTimePicker.Value = header.FromDate;
            fromHeaderDateTimePicker.Checked = true;
            toHeaderDateTimePicker.Value = header.ToDate;
            toHeaderDateTimePicker.Checked = true;
            expenseDateTimePicker.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            placeOfVisitComboBox.SelectedValue = header.PlaceOfVisitId;
            purposeOfTravelTextBox.Text = header.Purpose;
            countryNameTextBox.Text = header.CountryName;
            yesRadioButton.Checked = header.IsOverseasSponsorFinanced;
            sponsorNameTextBox.Text = header.OverseasSponsorName;
        }

        private void LoadHeaderInformationFromRequisition(DateTime fromDate, DateTime toDate)
        {
            fromHeaderDateTimePicker.Value = fromDate;
            fromHeaderDateTimePicker.Checked = true;
            toHeaderDateTimePicker.Value = toDate;
            toHeaderDateTimePicker.Checked = true;
        }

        private void LoadHeaderInformation(AdvanceOverseasTravelExpenseHeader header)
        {
            purposeOfTravelTextBox.Text = header.Purpose;
            placeOfVisitComboBox.SelectedValue = header.PlaceOfVisitId;
            fromHeaderDateTimePicker.Value = header.FromDate;
            fromHeaderDateTimePicker.Checked = true;
            toHeaderDateTimePicker.Value = header.ToDate;
            toHeaderDateTimePicker.Checked = true;
            expenseDateTimePicker.Value = header.ExpenseEntryDate;
            expenseDateTimePicker.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            sponsorNameTextBox.Text = header.OverseasSponsorName;
            countryNameTextBox.Text = header.CountryName;
            yesRadioButton.Checked = header.IsOverseasSponsorFinanced;
        }

        private void ExpenseEntryForOverseasTravelUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseDateTimePicker.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadCurrencyComboBox();
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value,
                    noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value,
                    noOfUnitTextBox);
                SetDefaultFromDetailDateAndToDetailDate();
                SetFormTitleAndText();
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

        private void SetFormToViewMode()
        {
            _isViewMode = true;
            expenseDateTimePicker.Enabled = false;
            addButton.Visible = false;
            saveButton.Visible = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
            detailsAddGroupBox.Enabled = false;
            //detailsShowGroupBox.Enabled = false;
            //expenseDetailsListView.ContextMenuStrip = null;
        }

        private void LoadPlaceOfVisit()
        {
            var placeOfVisits = _placeofVistManager.GetAll().ToList();
            if (placeOfVisits.Any())
            {
                placeOfVisits.Insert(0, new PlaceOfVisit() { Id = DefaultItem.Value, Name = DefaultItem.Text });
                placeOfVisitComboBox.DataSource = null;
                placeOfVisitComboBox.DisplayMember = "Name";
                placeOfVisitComboBox.ValueMember = "Id";
                placeOfVisitComboBox.DataSource = placeOfVisits;

            }
            if (_isUpdateMode || _isViewMode)
            {
                placeOfVisitComboBox.SelectedValue = _updateableExpenseHeader.PlaceOfVisitId;
            }
            else
            {
                if (_advanceRequisitionHeader != null)
                {
                    placeOfVisitComboBox.SelectedValue = _advanceRequisitionHeader.PlaceOfVisitId;
                }
            }
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
                designationTextBox.Text = _selectedEmployee.Admin_Rank == null
                    ? "N/A"
                    : _selectedEmployee.Admin_Rank.RankName;
                employeeNameTextBox.Text = _selectedEmployee.FullName;
                employeeIdTextBox.Text = _selectedEmployee.EmployeeID;
                departmentTextBox.Text = _selectedEmployee.Admin_Departments == null
                    ? "N/A"
                    : _selectedEmployee.Admin_Departments.DepartmentName;
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

        private void travelCostItemComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                SetEntitleAmount();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                travelCostItemComboBox.SelectedIndex = 0;
            }
        }

        private void SetEntitleAmount()
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
                        expenseAmountTextBox.Text = (overseasAllowance.EntitlementAmount * Convert.ToDecimal(noOfUnitTextBox.Text)).ToString();
                    }
                    else
                    {
                        entitlementTextBox.Text = @"Fully Entitled";
                        expenseAmountTextBox.Text = string.Empty;
                    }

                }
                else
                {
                    entitlementTextBox.Text = string.Empty;
                    expenseAmountTextBox.Text = string.Empty;
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
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value,
                                noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value,
                                noOfUnitTextBox);
                SetDefaultFromDetailDateAndToDetailDate();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void toDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value,
                                noOfDaysTextBox);
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value,
                                noOfUnitTextBox);
                SetDefaultFromDetailDateAndToDetailDate();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private decimal GetTotalAmount()
        {
            ICollection<AdvanceOverseasTravelExpenseDetail> detailList =
                GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);

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
                AdvanceOverseasTravelExpenseDetail detail = new AdvanceOverseasTravelExpenseDetail();
                if (!string.IsNullOrEmpty(noOfUnitTextBox.Text))
                {
                    detail.NoOfUnit = Convert.ToDouble(noOfUnitTextBox.Text);
                }
                detail.OverseasSponsorFinancedDetailAmount = sponsorDetailAmountTextBox.Text == string.Empty
                    ? (Decimal?)null
                    : Convert.ToDecimal(sponsorDetailAmountTextBox.Text);
                detail.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                detail.Currency = currencyComboBox.Text;
                detail.OverseasTravelCostItemId = Convert.ToInt64(travelCostItemComboBox.SelectedValue);
                if (!string.IsNullOrEmpty(entitlementTextBox.Text) && !entitlementTextBox.Text.Equals(@"Fully Entitled"))
                {
                    detail.UnitCost = Convert.ToDecimal(entitlementTextBox.Text);
                }
                detail.AdvanceAmount = !string.IsNullOrEmpty(advanceAmountTextBox.Text)
                    ? Convert.ToDecimal(advanceAmountTextBox.Text)
                    : 0;
                if (!string.IsNullOrEmpty(remarksTextBox.Text))
                {
                    detail.Remarks = remarksTextBox.Text;
                }
                detail.OverseasFromDate = fromDetailDateTimePicker.Value;
                detail.OverseasToDate = toDetailDateTimePicker.Value;
                if (!string.IsNullOrEmpty(expenseAmountTextBox.Text))
                {
                    detail.ExpenseAmount = Convert.ToDecimal(expenseAmountTextBox.Text);
                }
                detail.IsThirdPartyReceipient = isThirdPartyReceipientCheckBox.Checked;
                if (!string.IsNullOrEmpty(receipientOrPayeeNameTextBox.Text))
                {
                    detail.ReceipientOrPayeeName = receipientOrPayeeNameTextBox.Text;
                }
                detail.Purpose = travelCostItemComboBox.Text;
                if (!ValidateCeilingAmount())
                {
                    DialogResult result = MessageBox.Show(@"You are about to exceed the ceiling amount (" + _advanceCategory.CeilingAmount +
                        @"). Do you want to continue?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                    var _updateableDetail = item.Tag as AdvanceOverseasTravelExpenseDetail;

                    if (_updateableDetail == null)
                    {
                        throw new UiException("There is no item tagged with the detail item!");
                    }
                    _requisitionNo = (string) item.Cells[2].Value;
                    _updateableDetail.OverseasFromDate = detail.OverseasFromDate;
                    _updateableDetail.OverseasToDate = detail.OverseasToDate;
                    _updateableDetail.NoOfUnit = detail.NoOfUnit;
                    _updateableDetail.UnitCost = detail.UnitCost;
                    _updateableDetail.Remarks = detail.Remarks;
                    _updateableDetail.IsThirdPartyReceipient = detail.IsThirdPartyReceipient;
                    _updateableDetail.OverseasTravelCostItemId = detail.OverseasTravelCostItemId;
                    _updateableDetail.ReceipientOrPayeeName = detail.ReceipientOrPayeeName;
                    _updateableDetail.AdvanceAmount = detail.AdvanceAmount;
                    _updateableDetail.ExpenseAmount = detail.ExpenseAmount;
                    _updateableDetail.OverseasSponsorFinancedDetailAmount = detail.OverseasSponsorFinancedDetailAmount;
                    detail.AdvanceRequisitionDetailId = _updateableDetail.AdvanceRequisitionDetailId;
                    detail.Id = _updateableDetail.Id;
                    _updateableDetail.Purpose = detail.Purpose;
                    detail.AdvanceExpenseHeaderId = _updateableDetail.AdvanceExpenseHeaderId;

                    decimal justificationAmount = Convert.ToDecimal(expenseAmountTextBox.Text);
                    decimal entilementAmount = 0;
                    if (_updateableDetail.UnitCost != null && _updateableDetail.NoOfUnit != null)
                    {
                        entilementAmount = _updateableDetail.UnitCost.Value * (decimal)_updateableDetail.NoOfUnit.Value;
                    }
                    if (entilementAmount != 0)
                    {
                        if (justificationAmount > entilementAmount)
                        {
                            if (!string.IsNullOrEmpty(_updateableDetail.Justification))
                            {
                                JustificationEntryUI justification = new JustificationEntryUI(_updateableDetail.Justification, entilementAmount);
                                justification.ShowDialog();
                                _updateableDetail.Justification = justification.Info;
                                if (_updateableDetail.Justification == null)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                JustificationEntryUI justification = new JustificationEntryUI(entilementAmount);
                                justification.ShowDialog();
                                _updateableDetail.Justification = justification.Info;
                                if (_updateableDetail.Justification == null)
                                {
                                    return;
                                }
                            }

                        }
                        else if (justificationAmount == entilementAmount || entilementAmount > justificationAmount)
                        {
                            _updateableDetail.Justification = null;
                        }
                    }
                    RemoveTotalAmountFromGridView();
                    SetChangedGridViewItemByDetail(_updateableDetail, item);
                    ResetDetailChangeMode();
                }
                else
                {
                    //IsRequisitionDetailsExist(detail);
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
                ResetCostItemComboBox();
                ClearDetailControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void SetRecipientWiseReimbursementRefund(ListView listView)
        //{
        //    ICollection<AdvanceOverseasTravelExpenseDetail> details = GetAdvanceExpenseDetailsFromListView(listView);

        //    List<RecipientWithReimbursementRefund> recipientWiseReimbursementRefund = new List<RecipientWithReimbursementRefund>();

        //    foreach (AdvanceOverseasTravelExpenseDetail detail in details)
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
            ICollection<AdvanceOverseasTravelExpenseDetail> details = GetAdvanceExpenseDetailsFromGridView(gridView);

            List<RecipientWithReimbursementRefund> recipientWiseReimbursementRefund = new List<RecipientWithReimbursementRefund>();

            foreach (AdvanceOverseasTravelExpenseDetail detail in details)
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

        //    var totalAdvanceAmountInBDT = details.Sum(c => c.GetAdvanceAmountInBdt());
        //    var totalExpenseAmountInBDT = details.Sum(c => c.GetExpenseAmountInBdt());
        //    var totalSponsorAmountInBDT = details.Sum(c => c.OverseasSponsorFinancedDetailAmount) ?? 0;
        //    var actualExpenseInBDT = totalExpenseAmountInBDT - totalSponsorAmountInBDT;

        //    ListViewItem item = new ListViewItem();
        //    item.Text = string.Empty;
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(string.Empty);
        //    item.SubItems.Add(@"Total");
        //    item.SubItems.Add(totalExpenseAmountInBDT.ToString("N"));
        //    item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
        //    item.SubItems.Add(totalSponsorAmountInBDT.ToString("N"));
        //    item.SubItems.Add(GetFormatedAmount(actualExpenseInBDT));
        //    item.SubItems.Add(GetReimburseAmount(actualExpenseInBDT, totalAdvanceAmountInBDT));
        //    item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
        //    listView.Items.Add(item);

        //    SetRecipientWiseReimbursementRefund(listView);
        //}




        private void SetTotalAmountInGridView(DataGridView gridView)
        {
            var details = GetAdvanceExpenseDetailsFromGridView(gridView);

            var totalAdvanceAmountInBDT = details.Sum(c => c.GetAdvanceAmountInBdt());
            var totalExpenseAmountInBDT = details.Sum(c => c.GetExpenseAmountInBdt());
            var totalSponsorAmountInBDT = details.Sum(c => c.OverseasSponsorFinancedDetailAmount) ?? 0;
            var actualExpenseInBDT = totalExpenseAmountInBDT - totalSponsorAmountInBDT;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gridView);
            row.Cells[0] = new DataGridViewTextBoxCell();
            row.Cells[1] = new DataGridViewTextBoxCell();

            row.Cells[11].Value = "Total";
            row.Cells[12].Value = totalExpenseAmountInBDT.ToString("N");
            row.Cells[13].Value = totalAdvanceAmountInBDT.ToString("N");
            row.Cells[14].Value = totalSponsorAmountInBDT.ToString("N");
            row.Cells[15].Value = GetFormatedAmount(actualExpenseInBDT);
            row.Cells[16].Value = GetReimburseAmount(actualExpenseInBDT, totalAdvanceAmountInBDT);

            row.Cells[11].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[12].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[13].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[14].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[15].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            row.Cells[16].Style.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);

            row.Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[15].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[16].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridView.Rows.Add(row);

            SetRecipientWiseReimbursementRefund(gridView);
        }

        private string GetFormatedAmount(Decimal amount)
        {
            if (amount >= 0)
                return amount.ToString("N");
            return "(" + Math.Abs(amount) + ")";
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

        //private ListViewItem GetNewListViewItemByDetail(AdvanceOverseasTravelExpenseDetail detail)
        //{
        //    var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(detail.ConversionRate);
        //    var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(detail.ConversionRate);
        //    var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount == null
        //        ? 0
        //        : detail.OverseasSponsorFinancedDetailAmount;
        //    var actualExpenseInBDT
        //        = expenseAmountInBDT - sponsorAmountInBDT;
        //    ListViewItem item =
        //        new ListViewItem(_requisitionNo);
        //    item.SubItems.Add(detail.OverseasFromDate != null
        //       ? detail.OverseasFromDate.Value.ToString("dd/MM/yyyy")
        //       : string.Empty);
        //    item.SubItems.Add(detail.OverseasToDate != null
        //       ? detail.OverseasToDate.Value.ToString("dd/MM/yyyy")
        //       : string.Empty);
        //    item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty);
        //    item.SubItems.Add(detail.Purpose);
        //    item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
        //    item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
        //    item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
        //    item.SubItems.Add(detail.Currency);
        //    item.SubItems.Add(detail.ConversionRate.ToString("N"));
        //    item.SubItems.Add(expenseAmountInBDT.ToString("N"));
        //    item.SubItems.Add(advanceAmountInBDT.ToString("N"));
        //    item.SubItems.Add(sponsorAmountInBDT == 0 ? "N/A" : sponsorAmountInBDT.Value.ToString("N"));
        //    item.SubItems.Add(actualExpenseInBDT.Value.ToString("N"));
        //    item.SubItems.Add(GetReimburseAmount(Convert.ToDecimal(actualExpenseInBDT), advanceAmountInBDT));
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
        //    item.SubItems.Add(!string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A");

        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow GetNewGridViewItemByDetail(AdvanceOverseasTravelExpenseDetail detail)
        {
            var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(detail.ConversionRate);
            var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(detail.ConversionRate);
            var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount == null
                ? 0
                : detail.OverseasSponsorFinancedDetailAmount;
            var actualExpenseInBDT
                = expenseAmountInBDT - sponsorAmountInBDT;
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(expenseDetailsDataGridView);

            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.OverseasFromDate != null
                ? detail.OverseasFromDate.Value.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[4].Value = detail.OverseasToDate != null
                ? detail.OverseasToDate.Value.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[5].Value = detail.NoOfUnit != null && detail.NoOfUnit > 0
                ? detail.NoOfUnit.Value.ToString("N")
                : string.Empty;
            row.Cells[6].Value = detail.Purpose;
            row.Cells[7].Value = detail.UnitCost != null && detail.UnitCost > 0
                ? detail.UnitCost.Value.ToString("N")
                : "N/A";
            row.Cells[8].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[9].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[10].Value = detail.Currency;
            row.Cells[11].Value = detail.ConversionRate.ToString("N");
            row.Cells[12].Value = expenseAmountInBDT.ToString("N");
            row.Cells[13].Value = advanceAmountInBDT.ToString("N");
            row.Cells[14].Value = sponsorAmountInBDT == 0 ? "N/A" : sponsorAmountInBDT.Value.ToString("N");
            row.Cells[15].Value = actualExpenseInBDT.Value.ToString("N");
            row.Cells[16].Value = GetReimburseAmount(Convert.ToDecimal(actualExpenseInBDT), advanceAmountInBDT);
            row.Cells[17].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[18].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName)
                ? detail.ReceipientOrPayeeName
                : "N/A";
            row.Cells[19].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
        
            row.Tag = detail;
            return row;
        }

        //private ListViewItem SetChangedListViewItemByDetail(AdvanceOverseasTravelExpenseDetail detail, ListViewItem item)
        //{
        //    var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount == null
        //        ? 0
        //        : detail.OverseasSponsorFinancedDetailAmount;
        //    var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(detail.ConversionRate);
        //    var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(detail.ConversionRate);
        //    var actualExpenseInBDT = expenseAmountInBDT - sponsorAmountInBDT;

        //    item.Text = _requisitionNo;
        //    item.SubItems[1].Text = detail.OverseasFromDate != null
        //        ? detail.OverseasFromDate.Value.ToString("dd/MM/yyyy")
        //        : string.Empty;
        //    item.SubItems[2].Text = detail.OverseasToDate != null
        //        ? detail.OverseasToDate.Value.ToString("dd/MM/yyyy")
        //        : string.Empty;
        //    item.SubItems[3].Text = detail.NoOfUnit != null ? detail.NoOfUnit.ToString() : string.Empty;
        //    item.SubItems[4].Text = travelCostItemComboBox.Text;
        //    item.SubItems[5].Text = detail.UnitCost != 0 ? detail.UnitCost.ToString() : "N/A";
        //    item.SubItems[6].Text = detail.ExpenseAmount.ToString("N");
        //    item.SubItems[7].Text = detail.AdvanceAmount.ToString("N");
        //    item.SubItems[8].Text = detail.Currency;
        //    item.SubItems[9].Text = detail.ConversionRate.ToString("N");
        //    item.SubItems[10].Text = expenseAmountInBDT.ToString("N");

        //    item.SubItems[11].Text = advanceAmountInBDT.ToString("N");
        //    item.SubItems[12].Text = detail.OverseasSponsorFinancedDetailAmount == null
        //        ? "N/A"
        //        : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N");
        //    item.SubItems[13].Text = GetFormatedAmount(Convert.ToDecimal(actualExpenseInBDT));
        //    item.SubItems[14].Text = GetReimburseAmount(Convert.ToDecimal(actualExpenseInBDT), advanceAmountInBDT);

        //    item.SubItems[15].Text = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
        //    item.SubItems[16].Text = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
        //    item.SubItems[17].Text = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";
        //    item.Tag = detail;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(AdvanceOverseasTravelExpenseDetail detail, DataGridViewRow row)
        {
            var sponsorAmountInBDT = detail.OverseasSponsorFinancedDetailAmount == null
                ? 0
                : detail.OverseasSponsorFinancedDetailAmount;
            var expenseAmountInBDT = detail.ExpenseAmount * Convert.ToDecimal(detail.ConversionRate);
            var advanceAmountInBDT = detail.AdvanceAmount * Convert.ToDecimal(detail.ConversionRate);
            var actualExpenseInBDT = expenseAmountInBDT - sponsorAmountInBDT;

            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[2].Value = _requisitionNo;
            row.Cells[3].Value = detail.OverseasFromDate != null
                ? detail.OverseasFromDate.Value.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[4].Value = detail.OverseasToDate != null
                ? detail.OverseasToDate.Value.ToString("dd/MM/yyyy")
                : string.Empty;
            row.Cells[5].Value = detail.NoOfUnit != null ? detail.NoOfUnit.ToString() : string.Empty;
            row.Cells[6].Value = travelCostItemComboBox.Text;
            row.Cells[7].Value = detail.UnitCost != 0 ? detail.UnitCost.ToString() : "N/A";
            row.Cells[8].Value = detail.ExpenseAmount.ToString("N");
            row.Cells[9].Value = detail.AdvanceAmount.ToString("N");
            row.Cells[10].Value = detail.Currency;
            row.Cells[11].Value = detail.ConversionRate.ToString("N");
            row.Cells[12].Value = expenseAmountInBDT.ToString("N");
            row.Cells[13].Value = advanceAmountInBDT.ToString("N");
            row.Cells[14].Value = detail.OverseasSponsorFinancedDetailAmount == null
                ? "N/A"
                : detail.OverseasSponsorFinancedDetailAmount.Value.ToString("N");
            row.Cells[15].Value = GetFormatedAmount(Convert.ToDecimal(actualExpenseInBDT));
            row.Cells[16].Value = GetReimburseAmount(Convert.ToDecimal(actualExpenseInBDT), advanceAmountInBDT);
            row.Cells[17].Value = !string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A";
            row.Cells[18].Value = !string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A";
            row.Cells[19].Value = !string.IsNullOrEmpty(detail.Justification) ? detail.Justification : "N/A";

            row.Tag = detail;
            return row;
        }

        private void ClearDetailControls()
        {
            vatTaxTypeComboBox.SelectedValue = DefaultItem.Value;
            advanceAmountTextBox.Text = string.Empty;
            Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value,
                noOfUnitTextBox);
            entitlementTextBox.Text = string.Empty;
            remarksTextBox.Text = string.Empty;
            expenseAmountTextBox.Text = string.Empty;
            isThirdPartyReceipientCheckBox.Checked = false;
            receipientOrPayeeNameTextBox.Text = string.Empty;
            sponsorDetailAmountTextBox.Text = string.Empty;
            conversionRateTextBox.Text = string.Empty;
            travelCostItemComboBox.Enabled = true;
            isThirdPartyReceipientCheckBox.Enabled = true;
        }

        private void ResetCostItemComboBox()
        {
            travelCostItemComboBox.SelectedIndex = 0;
        }

        private bool IsRequisitionDetailsExist(AdvanceOverseasTravelExpenseDetail detail)
        {
            ICollection<AdvanceOverseasTravelExpenseDetail> detailList =
                GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
            if (detailList.Any(c => c.OverseasTravelCostItemId == detail.OverseasTravelCostItemId))
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
                AdvanceOverseasTravelExpenseHeader advanceExpenseHeader = new AdvanceOverseasTravelExpenseHeader();
                if (_isUpdateMode)
                {
                    advanceExpenseHeader.Id = _updateableExpenseHeader.Id;
                    advanceExpenseHeader.SerialNo = _updateableExpenseHeader.SerialNo;
                    advanceExpenseHeader.ExpenseNo = _updateableExpenseHeader.ExpenseNo;
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
                advanceExpenseHeader.PlaceOfVisitId = (long)placeOfVisitComboBox.SelectedValue;
                advanceExpenseHeader.IsOverseasSponsorFinanced = yesRadioButton.Checked;
                if (!string.IsNullOrEmpty(sponsorNameTextBox.Text))
                {
                    advanceExpenseHeader.OverseasSponsorName = sponsorNameTextBox.Text;
                }
                advanceExpenseHeader.AdvanceOverseasTravelExpenseDetails =
                    GetAdvanceExpenseDetailsFromGridView(expenseDetailsDataGridView);
                advanceExpenseHeader.ExpenseEntryDate = expenseDateTimePicker.Value;
                advanceExpenseHeader.AdvanceCategoryId = _advanceCategory.Id;
                advanceExpenseHeader.FromDate = fromHeaderDateTimePicker.Value;
                advanceExpenseHeader.ToDate = toHeaderDateTimePicker.Value;
                advanceExpenseHeader.NoOfDays = Convert.ToDouble(noOfDaysTextBox.Text);
                advanceExpenseHeader.CurrencyConversionRateDetails = _currencyConversionRateDetails;
                advanceExpenseHeader.CountryName = countryNameTextBox.Text == string.Empty ? null : countryNameTextBox.Text;
                if (_requisitionHeaders != null)
                {
                    advanceExpenseHeader.AdvanceOverseasTravelRequisitionHeaders = _requisitionHeaders;
                }
                advanceExpenseHeader.ExpenseFiles = _expenseFiles;
                if (!ValidateCeilingAmount())
                {
                    DialogResult result = MessageBox.Show(@"You are about to exceed the ceiling amount (" + _advanceCategory.CeilingAmount +
                        @"). Do you want to continue?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool isUpdated = _advanceExpenseHeaderManager.Edit(advanceExpenseHeader);

                    if (isUpdated)
                    {
                        MessageBox.Show(@"Requested expense updated successfully.", @"Success",
                            MessageBoxButtons.OK,
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

        private void selectEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                SelectEmployeeUI selectEmployeeUi = new SelectEmployeeUI();
                selectEmployeeUi.ShowDialog();
                _selectedEmployee = selectEmployeeUi.SelectedEmployee;
                if (_selectedEmployee != null)
                {
                    designationTextBox.Text = _selectedEmployee.Admin_Rank == null
                        ? "N/A"
                        : _selectedEmployee.Admin_Rank.RankName;
                    employeeNameTextBox.Text = _selectedEmployee.FullName;
                    employeeIdTextBox.Text = _selectedEmployee.EmployeeID;
                    departmentTextBox.Text = _selectedEmployee.Admin_Departments == null
                        ? "N/A"
                        : _selectedEmployee.Admin_Departments.DepartmentName;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //private List<AdvanceOverseasTravelExpenseDetail> GetAdvanceExpenseDetailsFromListView(
        //    ListView expenseDetailsListViewControl)
        //{
        //    var details = new List<AdvanceOverseasTravelExpenseDetail>();
        //    if (expenseDetailsListViewControl.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in expenseDetailsListViewControl.Items)
        //        {
        //            var detailItem = item.Tag as AdvanceOverseasTravelExpenseDetail;
        //            if (detailItem != null)
        //            {
        //                details.Add(detailItem);
        //            }
        //        }
        //    }
        //    return details;
        //}

        private List<AdvanceOverseasTravelExpenseDetail> GetAdvanceExpenseDetailsFromGridView(
          DataGridView expenseDetailsGridViewControl)
        {
            var details = new List<AdvanceOverseasTravelExpenseDetail>();
            if (expenseDetailsGridViewControl.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in expenseDetailsGridViewControl.Rows)
                {
                    var detailItem = row.Tag as AdvanceOverseasTravelExpenseDetail;
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
            if (string.IsNullOrEmpty(conversionRateTextBox.Text))
            {
                errorMessage += "Conversion rate is not provided." + Environment.NewLine;
            }
            if (isThirdPartyReceipientCheckBox.Checked && string.IsNullOrEmpty(receipientOrPayeeNameTextBox.Text))
            {
                errorMessage += "Receipient/Payee name is not provided." + Environment.NewLine;
            }
            if (expenseAmountTextBox.Text == string.Empty)
            {
                errorMessage += "Expense amount is not provided." + Environment.NewLine;
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
            if (!expenseDateTimePicker.Checked)
            {
                errorMessage += "Expense date is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(purposeOfTravelTextBox.Text))
            {
                errorMessage += "Purpose of travel is not provided." + Environment.NewLine;
            }
            if ((long)placeOfVisitComboBox.SelectedValue == DefaultItem.Value)
            {
                errorMessage += "Place of visit is not selected." + Environment.NewLine;
            }
            if (!fromHeaderDateTimePicker.Checked)
            {
                errorMessage += "Header from date is not provided." + Environment.NewLine;
            }
            if (!toHeaderDateTimePicker.Checked)
            {
                errorMessage += "Header to date is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(noOfDaysTextBox.Text))
            {
                errorMessage += "No. of day(s) is not provided." + Environment.NewLine;
            }
            if (expenseDetailsDataGridView.Rows.Count < 1)
            {
                errorMessage += "Could not proceed further! As you have not added any Expense detail." +
                                Environment.NewLine;
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
                SetDefaultExpenseAmount();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetDefaultExpenseAmount()
        {
            if (!string.IsNullOrEmpty(entitlementTextBox.Text) && !string.IsNullOrEmpty(noOfUnitTextBox.Text) && !entitlementTextBox.Text.Equals("Fully Entitled"))
            {
                decimal entitlement = Convert.ToDecimal(entitlementTextBox.Text);
                decimal noOnUnit = Convert.ToDecimal(noOfUnitTextBox.Text);

                decimal expenseAmount = entitlement * noOnUnit;

                expenseAmountTextBox.Text = expenseAmount.ToString();
            }
            else
            {
                expenseAmountTextBox.Text = string.Empty;
            }
        }

        private string GetReimburseAmount(decimal expenseAmount, decimal advanceAmount)
        {
            var reimburse = (expenseAmount - advanceAmount);
            if (reimburse >= 0)
                return reimburse.ToString("N");
            return "(" + Math.Abs(reimburse) + ")";
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
            //AdvanceOverseasTravelExpenseDetail detail = null;
            //if (expenseDetailsListView.SelectedItems.Count > 0)
            //{
            //    ListViewItem item = expenseDetailsListView.SelectedItems[0];
            //    if (expenseDetailsListView.Items.Count == item.Index + 1)
            //    {
            //        throw new UiException("No item is selected to edit.");
            //    }
            //    detail = item.Tag as AdvanceOverseasTravelExpenseDetail;
            //    if (detail == null)
            //    {
            //        throw new UiException("Item is not tagged");
            //    }
            //    _requisitionNo = item.Text;
            //    travelCostItemComboBox.SelectedValue = detail.OverseasTravelCostItemId;
            //    entitlementTextBox.Text = detail.UnitCost.ToString();
            //    noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
            //    remarksTextBox.Text = detail.Remarks;
            //    advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
            //    expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
            //    fromDetailDateTimePicker.Value = detail.OverseasFromDate != null
            //        ? detail.OverseasFromDate.Value
            //        : DateTime.Today;
            //    toDetailDateTimePicker.Value = detail.OverseasToDate != null
            //        ? detail.OverseasToDate.Value
            //        : DateTime.Today;
            //    isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
            //    receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
            //    vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
            //    currencyComboBox.Text = detail.Currency;
            //    sponsorDetailAmountTextBox.Text = detail.OverseasSponsorFinancedDetailAmount == null
            //        ? string.Empty
            //        : detail.OverseasSponsorFinancedDetailAmount.ToString();
            //    conversionRateTextBox.Text = detail.ConversionRate.ToString();
            //    expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
            //    if (detail.AdvanceRequisitionDetailId != null && detail.AdvanceRequisitionDetailId.Value > 0)
            //    {
            //        travelCostItemComboBox.Enabled = false;
            //        isThirdPartyReceipientCheckBox.Enabled = false;
            //    }
            //    SetFormToDetailChangeMode(item);
            //}
            //else
            //{
            //    throw new UiException("No item is selected to edit!");
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
            try
            {
                ResetDetailChangeMode();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ResetDetailChangeMode()
        {
            addButton.Text = @"Add";
            _requisitionNo = string.Empty;
            _updateableDetailItem = null;
            _isDetailUpdateMode = false;
            saveButton.Visible = true;
            ClearDetailControls();
            ResetCostItemComboBox();
        }

        private void fromDetailDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDetailDateTimePicker.Value, toDetailDateTimePicker.Value,
                                noOfUnitTextBox);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void toDetailDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.Utility.CalculateNoOfDays(fromDetailDateTimePicker.Value, toDetailDateTimePicker.Value,
                                noOfUnitTextBox);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetDefaultFromDetailDateAndToDetailDate()
        {
            fromDetailDateTimePicker.Value = fromHeaderDateTimePicker.Value;
            toDetailDateTimePicker.Value = toHeaderDateTimePicker.Value;
        }

        private void metroTileConversionRate_Click(object sender, EventArgs e)
        {
            try
            {
                CurrencyConversionRateDetailUI currencyConversionRateDetailUi =
                new CurrencyConversionRateDetailUI(_currencyConversionRateDetails);
                currencyConversionRateDetailUi.ShowDialog();
                _currencyConversionRateDetails = currencyConversionRateDetailUi.CurrencyConversionRateDetails;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void sponsorDetailAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void SetAdvanceAmount()
        {
            var conversionRate = conversionRateTextBox.Text == string.Empty
                   ? 0
                   : Convert.ToDecimal(conversionRateTextBox.Text);
            var advanceAmount = advanceAmountTextBox.Text == string.Empty
            ? 0
            : Convert.ToDecimal(advanceAmountTextBox.Text);

            var advanceAmounInBDT = conversionRate * advanceAmount;
            advanceAmountInBDTTextBox.Text = (advanceAmounInBDT) == 0 ? string.Empty : advanceAmounInBDT.ToString();
        }

        private void SetExpenseAmount()
        {
            var conversionRate = conversionRateTextBox.Text == string.Empty
                   ? 0
                   : Convert.ToDecimal(conversionRateTextBox.Text);
            var expenseAmount = expenseAmountTextBox.Text == string.Empty
            ? 0
            : Convert.ToDecimal(expenseAmountTextBox.Text);
            var expenseAmounInBDT = conversionRate * expenseAmount;
            expenseAmountInBDTTextBox.Text = (expenseAmounInBDT) == 0 ? string.Empty : expenseAmounInBDT.ToString();
        }

        private void conversionRateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetAdvanceAmount();
                SetExpenseAmount();
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

        private void expenseAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetExpenseAmount();
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
                        AdvanceOverseasTravelExpenseDetail detail = null;
                        if (expenseDetailsDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow row = expenseDetailsDataGridView.SelectedRows[0];
                            if (expenseDetailsDataGridView.Rows.Count == row.Index + 1)
                            {
                                throw new UiException("No item is selected to edit.");
                            }
                            detail = row.Tag as AdvanceOverseasTravelExpenseDetail;
                            if (detail == null)
                            {
                                throw new UiException("Item is not tagged");
                            }
                            _requisitionNo = (string)row.Cells[2].Value;
                            travelCostItemComboBox.SelectedValue = detail.OverseasTravelCostItemId;
                            entitlementTextBox.Text = detail.UnitCost.ToString();
                            noOfUnitTextBox.Text = detail.NoOfUnit.ToString();
                            remarksTextBox.Text = detail.Remarks;
                            advanceAmountTextBox.Text = detail.AdvanceAmount.ToString();
                            expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
                            fromDetailDateTimePicker.Value = detail.OverseasFromDate != null
                                ? detail.OverseasFromDate.Value
                                : DateTime.Today;
                            toDetailDateTimePicker.Value = detail.OverseasToDate != null
                                ? detail.OverseasToDate.Value
                                : DateTime.Today;
                            isThirdPartyReceipientCheckBox.Checked = detail.IsThirdPartyReceipient;
                            receipientOrPayeeNameTextBox.Text = detail.ReceipientOrPayeeName;
                            vatTaxTypeComboBox.SelectedValue = detail.VatTaxTypeId ?? DefaultItem.Value;
                            currencyComboBox.Text = detail.Currency;
                            sponsorDetailAmountTextBox.Text = detail.OverseasSponsorFinancedDetailAmount == null
                                ? string.Empty
                                : detail.OverseasSponsorFinancedDetailAmount.ToString();
                            conversionRateTextBox.Text = detail.ConversionRate.ToString();
                            expenseAmountTextBox.Text = detail.ExpenseAmount.ToString();
                            if (detail.AdvanceRequisitionDetailId != null && detail.AdvanceRequisitionDetailId.Value > 0)
                            {
                                travelCostItemComboBox.Enabled = false;
                                isThirdPartyReceipientCheckBox.Enabled = false;
                            }
                            SetFormToDetailChangeMode(row);
                        }
                        else
                        {
                            throw new UiException("No item is selected to edit!");
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

