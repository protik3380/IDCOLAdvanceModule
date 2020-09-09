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
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.History.ExpenseHistory
{
    public partial class ExpenseHistoryForOverseasTravelUI : Form
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
        private readonly IPlaceofVistManager _placeofVistManager;
        public ExpenseHistoryForOverseasTravelUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _expenseHistoryHeaderManager = new ExpenseHistoryManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceRequisitionDetailRepository = new AdvanceRequisitionDetailRepository();
            _placeofVistManager = new PlaceOfVisitManager();
        }

        public ExpenseHistoryForOverseasTravelUI(long expenseHeaderId)
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

        private void ExpenseHistoryForOverseasTravelUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseDateTimePicker.Value = DateTime.Now;
                LoadEmployeeInformation();
                LoadPlaceOfVisit();
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
        }

        private void LoadFirstItem()
        {
            expenseHistoryListView.Items[0].Selected = true;
            expenseHistoryListView.Select();
            var expenseHistoryHeader =
               expenseHistoryListView.SelectedItems[0].Tag as OverseasTravelExpenseHistoryHeader;
            LoadData(expenseHistoryHeader);
        }

        private void LoadData(OverseasTravelExpenseHistoryHeader expenseHistoryHeader)
        {
            var expenseHeader = Mapper.Map<AdvanceOverseasTravelExpenseHeader>(expenseHistoryHeader);
            LoadHeaderInformation(expenseHeader);
            var historyDetails = expenseHistoryHeader.OverseasTravelExpenseHistoryDetails.Select(c => (OverseasTravelExpenseHistoryDetail)c).ToList();
            LoadDetailsInformation(historyDetails);
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
            placeOfVisitComboBox.Text = _placeofVistManager.GetById(header.PlaceOfVisitId).Name;
        }

        private void LoadDetailsInformation(ICollection<OverseasTravelExpenseHistoryDetail> details)
        {
            try
            {
                expenseDetailsListView.Items.Clear();
                List<OverseasTravelExpenseHistoryDetail> deletedDetails = new List<OverseasTravelExpenseHistoryDetail>();
                foreach (OverseasTravelExpenseHistoryDetail detail in details)
                {
                    var expenseDetail = Mapper.Map<AdvanceOverseasTravelExpenseDetail>(detail);
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
                        item.SubItems.Add(detail.OverseasFromDate != null
               ? detail.OverseasFromDate.Value.ToString("dd/MM/yyyy")
               : string.Empty);
                        item.SubItems.Add(detail.OverseasToDate != null
                           ? detail.OverseasToDate.Value.ToString("dd/MM/yyyy")
                           : string.Empty);
                        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty);
                        item.SubItems.Add(detail.Purpose);
                        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
                        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                        item.SubItems.Add(detail.Currency);
                        item.SubItems.Add(detail.ConversionRate.ToString("N"));
                        item.SubItems.Add(detail.GetExpenseAmountInBdt().ToString("N"));
                        item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                        item.SubItems.Add(detail.GetSponsorAmount() == 0 ? "N/A" : detail.GetSponsorAmount().ToString("N"));
                        item.SubItems.Add(detail.GetTotalActualExpenseAmount().ToString("N"));
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
                    foreach (OverseasTravelExpenseHistoryDetail detail in deletedDetails)
                    {
                        var expenseDetail = Mapper.Map<AdvanceOverseasTravelExpenseDetail>(detail);
                        string requisitionNo = "N/A";
                        if (expenseDetail.AdvanceRequisitionDetailId != null)
                        {
                            requisitionNo =
                                _advanceRequisitionDetailRepository.GetFirstOrDefaultBy(
                                    c => c.Id == expenseDetail.AdvanceRequisitionDetailId,
                                    c => c.AdvanceRequisitionHeader).AdvanceRequisitionHeader.RequisitionNo;
                        }
                        ListViewItem item = new ListViewItem(requisitionNo);
                        item.SubItems.Add(detail.OverseasFromDate != null
                           ? detail.OverseasFromDate.Value.ToString("dd/MM/yyyy")
                           : string.Empty);
                        item.SubItems.Add(detail.OverseasToDate != null
                           ? detail.OverseasToDate.Value.ToString("dd/MM/yyyy")
                           : string.Empty);
                        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : string.Empty);
                        item.SubItems.Add(detail.Purpose);
                        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.ExpenseAmount.ToString("N"));
                        item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                        item.SubItems.Add(detail.Currency);
                        item.SubItems.Add(detail.ConversionRate.ToString("N"));
                        item.SubItems.Add(detail.GetExpenseAmountInBdt().ToString("N"));
                        item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                        item.SubItems.Add(detail.GetSponsorAmount() == 0 ? "N/A" : detail.GetSponsorAmount().ToString("N"));
                        item.SubItems.Add(detail.GetTotalActualExpenseAmount().ToString("N"));
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

            var totalAdvanceAmount = details.Sum(c => c.GetAdvanceAmountInBdt());
            var totalExpenseAmount = details.Sum(c => c.GetExpenseAmountInBdt());
            var totalSponsorAmount = details.Sum(c => c.GetSponsorAmount());
            var totalActualExpenseAmount = details.Sum(c => c.GetTotalActualExpenseAmount());
            var totalReimbursementAmount = details.Sum(c => c.GetReimbursementOrRefundAmount());
            var formatedReimbursementAmount = totalReimbursementAmount >= 0 ? totalReimbursementAmount.ToString() : "(" + Math.Abs(totalReimbursementAmount) + ")";
            
            ListViewItem item = new ListViewItem();
            item.Text = string.Empty;
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(@"Total");
            item.SubItems.Add(totalExpenseAmount.ToString("N"));
            item.SubItems.Add(totalAdvanceAmount.ToString("N"));
            item.SubItems.Add(totalSponsorAmount.ToString("N"));
            item.SubItems.Add(totalActualExpenseAmount.ToString("N"));
            item.SubItems.Add(formatedReimbursementAmount);
            item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            expenseDetailsListViewControl.Items.Add(item);
        }

        private List<AdvanceOverseasTravelExpenseDetail> GetAdvanceExpenseDetailsFromListView(ListView expenseDetailsListViewControl)
        {
            var details = new List<AdvanceOverseasTravelExpenseDetail>();
            if (expenseDetailsListViewControl.Items.Count > 0)
            {
                foreach (ListViewItem item in expenseDetailsListViewControl.Items)
                {
                    var detailItem = item.Tag as OverseasTravelExpenseHistoryDetail;
                    if (detailItem != null)
                    {
                        var requisitionDetail = Mapper.Map<AdvanceOverseasTravelExpenseDetail>(detailItem);
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
                       expenseHistoryListView.SelectedItems[0].Tag as OverseasTravelExpenseHistoryHeader;
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
                       expenseHistoryListView.SelectedItems[0].Tag as OverseasTravelExpenseHistoryHeader;
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
