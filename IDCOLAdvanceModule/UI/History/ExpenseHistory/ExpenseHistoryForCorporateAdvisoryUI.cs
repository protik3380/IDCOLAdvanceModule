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

namespace IDCOLAdvanceModule.UI.History.ExpenseHistory
{
    public partial class ExpenseHistoryForCorporateAdvisoryUI : Form
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
        public ExpenseHistoryForCorporateAdvisoryUI()
        {
            _employeeManager = new EmployeeManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _expenseHistoryHeaderManager = new ExpenseHistoryManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceRequisitionDetailRepository = new AdvanceRequisitionDetailRepository();
            InitializeComponent();
        }
        public ExpenseHistoryForCorporateAdvisoryUI(long expenseHeaderId)
            : this()
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

        private void ExpenseHistoryForCorporateAdvisoryUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseDateTimePicker.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadCurrencyComboBox();
                Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
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
               expenseHistoryListView.SelectedItems[0].Tag as CorporateAdvisoryExpenseHistoryHeader;
            LoadData(expenseHistoryHeader);
        }
        private void LoadData(CorporateAdvisoryExpenseHistoryHeader expenseHistoryHeader)
        {
            var expenseHeader = Mapper.Map<AdvanceCorporateAdvisoryExpenseHeader>(expenseHistoryHeader);
            LoadHeaderInformation(expenseHeader);
            var historyDetails = expenseHistoryHeader.CorporateAdvisoryExpenseHistoryDetails.Select(c => (CorporateAdvisoryExpenseHistoryDetail)c).ToList();
            LoadDetailsInformation(historyDetails);
        }

        private void LoadHeaderInformation(AdvanceCorporateAdvisoryExpenseHeader header)
        {
            purposeTextBox.Text = header.Purpose;
            placeOfEventTextBox.Text = header.CorporateAdvisoryPlaceOfEvent;
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            expenseDateTimePicker.Value = header.ExpenseEntryDate;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            currencyComboBox.Text = header.Currency;
            conversionRateTextBox.Text = header.ConversionRate.ToString();
            noOfUnitHeaderTextBox.Text = header.NoOfUnit.ToString();
            unitCostHeaderTextBox.Text = header.UnitCost.ToString();
            totalRevenueTextBox.Text = header.TotalRevenue.ToString();
            corporateAdvisoryRemarksTextBox.Text = header.AdvanceCorporateRemarks ?? string.Empty;
        }

        private void LoadDetailsInformation(ICollection<CorporateAdvisoryExpenseHistoryDetail> details)
        {
            try
            {
                expenseDetailsListView.Items.Clear();
                List<CorporateAdvisoryExpenseHistoryDetail> deletedDetails = new List<CorporateAdvisoryExpenseHistoryDetail>();
                foreach (CorporateAdvisoryExpenseHistoryDetail detail in details)
                {
                    var expenseDetail = Mapper.Map<AdvanceCorporateAdvisoryExpenseDetail>(detail);
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
                        item.SubItems.Add(detail.Purpose);
                        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
                        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                        item.SubItems.Add(detail.GetFormattedReimbursementAmount());
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
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
                    foreach (CorporateAdvisoryExpenseHistoryDetail detail in deletedDetails)
                    {

                        var expenseDetail = Mapper.Map<AdvanceCorporateAdvisoryExpenseDetail>(detail);
                        string requisitionNo = "N/A";
                        if (expenseDetail.AdvanceRequisitionDetailId != null)
                        {
                            requisitionNo =
                                _advanceRequisitionDetailRepository.GetFirstOrDefaultBy(
                                    c => c.Id == expenseDetail.AdvanceRequisitionDetailId,
                                    c => c.AdvanceRequisitionHeader).AdvanceRequisitionHeader.RequisitionNo;
                        }
                        ListViewItem item = new ListViewItem(requisitionNo);
                        item.SubItems.Add(detail.Purpose);
                        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
                        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                        item.SubItems.Add(detail.GetFormattedReimbursementAmount());
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
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
        private void SetTotalAmountInListView(ListView advanceDetailsListViewControl)
        {
            var details = GetAdvanceExpenseDetailsFromListView(advanceDetailsListViewControl);

            var totalAdvanceAmount = details.Sum(c => c.GetAdvanceAmountInBdt());
            var totalExpenseAmount = details.Sum(c => c.GetExpenseAmountInBdt());
            var totalReimbursementAmount = details.Sum(c => c.GetReimbursementOrRefundAmount());
            var formatedReimbursementAmount = totalReimbursementAmount >= 0 ? totalReimbursementAmount.ToString() : "(" + Math.Abs(totalReimbursementAmount) + ")";


            ListViewItem item = new ListViewItem();
            item.Text = string.Empty;
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(@"Total");
            item.SubItems.Add(totalExpenseAmount.ToString());
            item.SubItems.Add(totalAdvanceAmount.ToString());
            item.SubItems.Add(formatedReimbursementAmount);
            item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            advanceDetailsListViewControl.Items.Add(item);
        }

        private List<CorporateAdvisoryExpenseHistoryDetail> GetAdvanceExpenseDetailsFromListView(ListView expenseDetailsListViewControl)
        {
            var details = new List<CorporateAdvisoryExpenseHistoryDetail>();
            if (expenseDetailsListViewControl.Items.Count > 0)
            {
                foreach (ListViewItem item in expenseDetailsListViewControl.Items)
                {
                    var detailItem = item.Tag as CorporateAdvisoryExpenseHistoryDetail;
                    if (detailItem != null)
                    {
                        var expenseDetail = Mapper.Map<CorporateAdvisoryExpenseHistoryDetail>(detailItem);
                        details.Add(expenseDetail);
                    }
                }
            }
            return details;
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
                       expenseHistoryListView.SelectedItems[0].Tag as CorporateAdvisoryExpenseHistoryHeader;
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
                       expenseHistoryListView.SelectedItems[0].Tag as CorporateAdvisoryExpenseHistoryHeader;
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

        private void expenseHistoryListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = expenseHistoryListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, expenseHistoryListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseHistoryListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

        private void expenseHistoryListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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
