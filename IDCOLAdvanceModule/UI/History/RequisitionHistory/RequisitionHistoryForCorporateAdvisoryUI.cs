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
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;

namespace IDCOLAdvanceModule.UI.History.RequisitionHistory
{
    public partial class RequisitionHistoryForCorporateAdvisoryUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IRequisitionHistoryHeaderManager _requisitionHistoryHeaderManager;
        private ListViewItem _lastSelectedItem;
        public RequisitionHistoryForCorporateAdvisoryUI()
        {
            _employeeManager = new EmployeeManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionHistoryHeaderManager = new RequisitionHistoryManager();
            InitializeComponent();
        }
        public RequisitionHistoryForCorporateAdvisoryUI(AdvanceCorporateAdvisoryRequisitionHeader header)
            : this()
        {
            var
            requisitionHistories = _requisitionHistoryHeaderManager.GetAllByRequisitionHeaderId(header.Id);
            LoadhistoryGroupBox(requisitionHistories);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            SetFormToViewMode();
        }
        private void LoadhistoryGroupBox(ICollection<RequisitionHistoryHeader> requisitionHistories)
        {
            requisitionHistoryListView.Items.Clear();
            int serial = 1;
            requisitionHistories = requisitionHistories.OrderByDescending(c => c.LastModifiedOn).ToList();
            foreach (RequisitionHistoryHeader requisitionHistoryHeader in requisitionHistories)
            {
                ListViewItem item = new ListViewItem(serial.ToString());
                if (requisitionHistoryHeader.HistoryModeId == (long)HistoryModeEnum.Create)
                {
                    item.SubItems.Add(requisitionHistoryHeader.CreatedOn.ToString("dd/MM/yyyy"));
                }
                else
                {
                    item.SubItems.Add(requisitionHistoryHeader.LastModifiedOn == null ? string.Empty : requisitionHistoryHeader.LastModifiedOn.Value.ToString("dd/MM/yyyy"));
                }
                item.SubItems.Add(requisitionHistoryHeader.HistoryMode.Name);
                item.Tag = requisitionHistoryHeader;
                requisitionHistoryListView.Items.Add(item);
                serial++;
            }
        }
        private void SetFormToViewMode()
        {
            requisitionDateTime.Enabled = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
        }

        private void RequisitionHistoryForCorporateAdvisoryUI_Load(object sender, EventArgs e)
        {
            requisitionDateTime.Value = DateTime.Now;
            LoadEmployeeInformation();
            LoadCurrencyComboBox();
            Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
            LoadFirstItem();
            SetFormTitleAndText();
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
        private void LoadFirstItem()
        {
            requisitionHistoryListView.Items[0].Selected = true;
            requisitionHistoryListView.Select();
            var requisitionHistoryHeader =
               requisitionHistoryListView.SelectedItems[0].Tag as CorporateAdvisoryRequisitionHistoryHeader;
            LoadData(requisitionHistoryHeader);
        }

        private void LoadData(CorporateAdvisoryRequisitionHistoryHeader requisitionHistoryHeader)
        {
            var requisitionHeader = Mapper.Map<AdvanceCorporateAdvisoryRequisitionHeader>(requisitionHistoryHeader);
            LoadHeaderInformation(requisitionHeader);
            var historyDetails = requisitionHistoryHeader.CorporateAdvisoryRequisitionHistoryDetails.Select(c => (CorporateAdvisoryRequisitionHistoryDetail)c).ToList();
            LoadDetailsInformation(historyDetails);
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

        private void LoadDetailsInformation(ICollection<CorporateAdvisoryRequisitionHistoryDetail> details)
        {
            try
            {
                advanceDetailsListView.Items.Clear();
                List<CorporateAdvisoryRequisitionHistoryDetail> deletedDetails = new List<CorporateAdvisoryRequisitionHistoryDetail>();
                foreach (CorporateAdvisoryRequisitionHistoryDetail detail in details)
                {
                    var requisitionDetail = Mapper.Map<AdvanceCorporateAdvisoryRequisitionDetail>(detail);
                    if (detail.HistoryModeId == (long)HistoryModeEnum.Delete)
                    {
                        deletedDetails.Add(detail);
                    }
                    else
                    {
                        ListViewItem item = new ListViewItem(detail.Purpose);
                        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
                        item.SubItems.Add(detail.HistoryMode.Name);
                        item.Tag = detail;
                        advanceDetailsListView.Items.Add(item);
                    }
                }
                SetTotalAmountInListView(advanceDetailsListView);

                if (deletedDetails.Count > 0)
                {
                    ListViewItem deleteItem = new ListViewItem("Deleted Details");
                    deleteItem.ForeColor = Color.Red;
                    advanceDetailsListView.Items.Add(deleteItem);
                    foreach (CorporateAdvisoryRequisitionHistoryDetail detail in deletedDetails)
                    {
                        var requisitionDetail = Mapper.Map<CorporateAdvisoryRequisitionHistoryDetail>(detail);

                        ListViewItem item = new ListViewItem(detail.Purpose);
                        item.SubItems.Add(detail.NoOfUnit != null && detail.NoOfUnit > 0 ? detail.NoOfUnit.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.UnitCost != null && detail.UnitCost > 0 ? detail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(detail.ReceipientOrPayeeName) ? detail.ReceipientOrPayeeName : "N/A");
                        item.SubItems.Add(detail.HistoryMode.Name);
                        item.Tag = detail;
                        advanceDetailsListView.Items.Add(item);
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
            var details = GetAdvanceRequisitionDetailsFromListView(advanceDetailsListViewControl);

            var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
            var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
            var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;

            ListViewItem item = new ListViewItem();
            item.Text = string.Empty;
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(@"Total");
            item.SubItems.Add(totalAdvanceAmountInBDT.ToString("N"));
            item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            advanceDetailsListViewControl.Items.Add(item);
        }
        private ICollection<CorporateAdvisoryRequisitionHistoryDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        {
            var details = new List<CorporateAdvisoryRequisitionHistoryDetail>();
             if (advanceDetailsListViewControl.Items.Count > 0)
            {
                foreach (ListViewItem item in advanceDetailsListViewControl.Items)
                {
                    var detailItem = item.Tag as CorporateAdvisoryRequisitionHistoryDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
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
                int index = requisitionHistoryListView.SelectedItems[0].Index;
                if (index > 0)
                {
                    requisitionHistoryListView.Items[index - 1].Selected = true;
                    requisitionHistoryListView.Select();
                    var requisitionHistoryHeader =
                       requisitionHistoryListView.SelectedItems[0].Tag as CorporateAdvisoryRequisitionHistoryHeader;
                    LoadData(requisitionHistoryHeader);
                }
                else
                {
                    MessageBox.Show(@"Sorry, you have reached last history.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int index = requisitionHistoryListView.SelectedItems[0].Index;
                if (index < requisitionHistoryListView.Items.Count - 1)
                {
                    requisitionHistoryListView.Items[index + 1].Selected = true;
                    requisitionHistoryListView.Select();
                    var requisitionHistoryHeader =
                       requisitionHistoryListView.SelectedItems[0].Tag as CorporateAdvisoryRequisitionHistoryHeader;
                    LoadData(requisitionHistoryHeader);
                }
                else
                {
                    MessageBox.Show(@"Sorry, you have reached first history", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceDetailsListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = advanceDetailsListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, advanceDetailsListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceDetailsListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void advanceDetailsListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

        private void requisitionHistoryListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = requisitionHistoryListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, requisitionHistoryListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionHistoryListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void requisitionHistoryListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
