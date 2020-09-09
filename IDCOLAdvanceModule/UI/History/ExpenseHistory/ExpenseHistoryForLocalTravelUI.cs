using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.History.ExpenseHistory
{
    public partial class ExpenseHistoryForLocalTravelUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IExpenseHistoryHeaderManager _expenseHistoryHeaderManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private ListViewItem _lastSelectedItem;
        private AdvanceExpenseHeader _expenseHeader;
        private IAdvanceRequisitionDetailRepository _advanceRequisitionDetailRepository;
        public ExpenseHistoryForLocalTravelUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _expenseHistoryHeaderManager = new ExpenseHistoryManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceRequisitionDetailRepository = new AdvanceRequisitionDetailRepository();
        }

        public ExpenseHistoryForLocalTravelUI(long expenseHeaderId) : this()
        {
            var
            expenseHistories = _expenseHistoryHeaderManager.GetAllBy(expenseHeaderId);
            LoadHistoryGroupBox(expenseHistories);
            _expenseHeader = _advanceExpenseHeaderManager.GetById(expenseHeaderId);
            _selectedEmployee = _employeeManager.GetByUserName(_expenseHeader.RequesterUserName);
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(_expenseHeader.AdvanceCategoryId);
        }

        private void LoadHistoryGroupBox(ICollection<ExpenseHistoryHeader> expenseHistories)
        {
            expenseHistoryListView.Items.Clear();
            int serial = 1;
            expenseHistories = expenseHistories.OrderByDescending(c => c.LastModifiedOn).ToList();
            foreach (ExpenseHistoryHeader expenseHistoryHeader in expenseHistories)
            {
                ListViewItem item = new ListViewItem(serial.ToString());
                if (expenseHistoryHeader.HistoryModeId == (long)HistoryModeEnum.Create)
                {
                    item.SubItems.Add(expenseHistoryHeader.CreatedOn.ToString("dd/MM/yyyy"));
                }
                else
                {
                    item.SubItems.Add(expenseHistoryHeader.LastModifiedOn == null ? string.Empty : expenseHistoryHeader.LastModifiedOn.Value.ToString("dd/MM/yyyy"));
                }
                item.SubItems.Add(expenseHistoryHeader.HistoryMode.Name);
                item.Tag = expenseHistoryHeader;
                expenseHistoryListView.Items.Add(item);
                serial++;
            }
        }

        private void ExpenseHistoryForTravelUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseDateTimePicker.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadCurrencyComboBox();
                Utility.Utility.CalculateNoOfDays(fromHeaderDateTimePicker.Value, toHeaderDateTimePicker.Value, noOfDaysTextBox);
                LoadFirstItem();
                SetFormTitleAndText();
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

        private void LoadFirstItem()
        {
            expenseHistoryListView.Items[0].Selected = true;
            expenseHistoryListView.Select();
            var expenseHistoryHeader =
               expenseHistoryListView.SelectedItems[0].Tag as TravelExpenseHistoryHeader;
            LoadData(expenseHistoryHeader);
        }

        private void LoadData(TravelExpenseHistoryHeader expenseHistoryHeader)
        {
            var expenseHeader = Mapper.Map<AdvanceTravelExpenseHeader>(expenseHistoryHeader);
            LoadHeaderInformation(expenseHeader);
            var historyDetails = expenseHistoryHeader.TravelExpenseHistoryDetails.Select(c => (TravelExpenseHistoryDetail)c).ToList();
            LoadDetailsInformation(historyDetails);
        }

        private void LoadHeaderInformation(AdvanceTravelExpenseHeader header)
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

        private void LoadDetailsInformation(ICollection<TravelExpenseHistoryDetail> details)
        {
            try
            {
                expenseDetailsListView.Items.Clear();
                List<TravelExpenseHistoryDetail> deletedDetails = new List<TravelExpenseHistoryDetail>();
                foreach (TravelExpenseHistoryDetail detail in details)
                {
                    var expenseDetail = Mapper.Map<AdvanceTravelExpenseDetail>(detail);
                    if (detail.HistoryModeId == (long)HistoryModeEnum.Delete)
                    {
                        deletedDetails.Add(detail);
                    }
                    else
                    {
                        string requisitionNo = "N/A";
                        if (expenseDetail.AdvanceRequisitionDetailId != null)
                        {
                            requisitionNo =
                                _advanceRequisitionDetailRepository.GetFirstOrDefaultBy(
                                    c => c.Id == expenseDetail.AdvanceRequisitionDetailId,
                                    c => c.AdvanceRequisitionHeader).AdvanceRequisitionHeader.RequisitionNo;
                        }
                        ListViewItem item = new ListViewItem(requisitionNo);
                        item.SubItems.Add(expenseDetail.FromDate.Value.ToString("dd/MM/yyyy"));
                        item.SubItems.Add(expenseDetail.ToDate.Value.ToString("dd/MM/yyyy"));
                        item.SubItems.Add(expenseDetail.NoOfUnit != null && expenseDetail.NoOfUnit > 0 ? expenseDetail.NoOfUnit.Value.ToString("N") : string.Empty);
                        item.SubItems.Add(expenseDetail.Purpose);
                        item.SubItems.Add(expenseDetail.UnitCost != null && expenseDetail.UnitCost > 0 ? expenseDetail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(expenseDetail.ExpenseAmount.ToString("N"));
                        item.SubItems.Add(expenseDetail.AdvanceAmount.ToString("N"));
                        item.SubItems.Add(expenseDetail.GetSponsorAmount().ToString("N"));
                        item.SubItems.Add(Utility.Utility.GetFormatedAmount(expenseDetail.GetTotalActualExpenseAmount()));
                        item.SubItems.Add(expenseDetail.GetFormattedReimbursementAmount());
                        item.SubItems.Add(!string.IsNullOrEmpty(expenseDetail.Remarks) ? expenseDetail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(expenseDetail.ReceipientOrPayeeName) ? expenseDetail.ReceipientOrPayeeName : "N/A");
                        item.SubItems.Add(detail.HistoryMode.Name);
                        item.Tag = detail;
                        expenseDetailsListView.Items.Add(item);
                    }
                }
                SetTotalAmountInListView(expenseDetailsListView);

                if (deletedDetails.Count > 0)
                {

                    ListViewItem deleteItem = new ListViewItem("Deleted Details");
                    deleteItem.ForeColor = Color.Red;
                    expenseDetailsListView.Items.Add(deleteItem);
                    foreach (TravelExpenseHistoryDetail detail in deletedDetails)
                    {

                        var expenseDetail = Mapper.Map<AdvanceTravelExpenseDetail>(detail);
                        string requisitionNo = "N/A";
                        if (expenseDetail.AdvanceRequisitionDetailId != null)
                        {
                            requisitionNo =
                                _advanceRequisitionDetailRepository.GetFirstOrDefaultBy(
                                    c => c.Id == expenseDetail.AdvanceRequisitionDetailId,
                                    c => c.AdvanceRequisitionHeader).AdvanceRequisitionHeader.RequisitionNo;
                        }
                        ListViewItem item = new ListViewItem(requisitionNo);
                        item.SubItems.Add(expenseDetail.FromDate.Value.ToString("dd/MM/yyyy"));
                        item.SubItems.Add(expenseDetail.ToDate.Value.ToString("dd/MM/yyyy"));
                        item.SubItems.Add(expenseDetail.NoOfUnit != null && expenseDetail.NoOfUnit > 0 ? expenseDetail.NoOfUnit.Value.ToString("N") : string.Empty);
                        item.SubItems.Add(expenseDetail.Purpose);
                        item.SubItems.Add(expenseDetail.UnitCost != null && expenseDetail.UnitCost > 0 ? expenseDetail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(expenseDetail.ExpenseAmount.ToString("N"));
                        item.SubItems.Add(expenseDetail.AdvanceAmount.ToString("N"));
                        item.SubItems.Add(expenseDetail.GetSponsorAmount().ToString("N"));
                        item.SubItems.Add(Utility.Utility.GetFormatedAmount(expenseDetail.GetTotalActualExpenseAmount()));
                        item.SubItems.Add(expenseDetail.GetFormattedReimbursementAmount());
                        item.SubItems.Add(!string.IsNullOrEmpty(expenseDetail.Remarks) ? expenseDetail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(expenseDetail.ReceipientOrPayeeName) ? expenseDetail.ReceipientOrPayeeName : "N/A");
                        item.SubItems.Add(detail.HistoryMode.Name);
                        item.Tag = detail;
                        expenseDetailsListView.Items.Add(item);
                    }
                }
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
            string title = "Requisition History (" + _advanceCategory.Name + ")";
            Text = title;
            titleLabel.Text = title;
        }

        private void SetTotalAmountInListView(ListView expenseDetailsListViewControl)
        {
            var details = GetAdvanceExpenseDetailsFromListView(expenseDetailsListViewControl);

            var totalAdvanceAmountInBDT = details.Sum(c => c.AdvanceAmount);
            var totalExpenseAmountInBDT = details.Sum(c => c.ExpenseAmount);
            var totalSponsorAmountInBDT = details.Sum(c => c.TravelSponsorFinancedDetailAmount) ?? 0;
            var actualExpenseInBDT = totalExpenseAmountInBDT - totalSponsorAmountInBDT;

            ListViewItem item = new ListViewItem();
            item.Text = string.Empty;
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(@"Total");
            item.SubItems.Add(totalExpenseAmountInBDT.ToString("N"));
            item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
            item.SubItems.Add(totalSponsorAmountInBDT.ToString("N"));
            item.SubItems.Add(actualExpenseInBDT.ToString("N"));
            item.SubItems.Add(Utility.Utility.GetFormatedAmount(details.Sum(c=>c.GetReimbursementOrRefundAmount())));
            item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            expenseDetailsListViewControl.Items.Add(item);
        }

        private List<AdvanceTravelExpenseDetail> GetAdvanceExpenseDetailsFromListView(ListView expenseDetailsListViewControl)
        {
            var details = new List<AdvanceTravelExpenseDetail>();
            if (expenseDetailsListViewControl.Items.Count > 0)
            {
                foreach (ListViewItem item in expenseDetailsListViewControl.Items)
                {
                    var detailItem = item.Tag as TravelExpenseHistoryDetail;
                    if (detailItem != null)
                    {
                        var requisitionDetail = Mapper.Map<AdvanceTravelExpenseDetail>(detailItem);
                        details.Add(requisitionDetail);
                    }
                }
            }
            return details;
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = expenseHistoryListView.SelectedItems[0].Index;
                if (index < expenseHistoryListView.Items.Count - 1)
                {
                    expenseHistoryListView.Items[index + 1].Selected = true;
                    expenseHistoryListView.Select();
                    var expenseHistoryHeader =
                       expenseHistoryListView.SelectedItems[0].Tag as TravelExpenseHistoryHeader;
                    LoadData(expenseHistoryHeader);
                }
                else
                {
                    MessageBox.Show(@"Sorry, you have reached first history.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = expenseHistoryListView.SelectedItems[0].Index;
                if (index > 0)
                {
                    expenseHistoryListView.Items[index - 1].Selected = true;
                    expenseHistoryListView.Select();
                    var expenseHistoryHeader =
                       expenseHistoryListView.SelectedItems[0].Tag as TravelExpenseHistoryHeader;
                    LoadData(expenseHistoryHeader);
                }
                else
                {
                    MessageBox.Show(@"Sorry, you have reached last history.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseDetailsListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = expenseDetailsListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, expenseDetailsListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseDetailsListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void expenseDetailsListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
    }
}
