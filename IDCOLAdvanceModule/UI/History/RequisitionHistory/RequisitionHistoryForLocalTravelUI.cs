using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
using MetroFramework;

namespace IDCOLAdvanceModule.UI.History.RequisitionHistory
{
    public partial class RequisitionHistoryForLocalTravelUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly ICurrencyManager _currencyManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IRequisitionHistoryHeaderManager _requisitionHistoryHeaderManager;
        private ListViewItem _lastSelectedItem;

        private RequisitionHistoryForLocalTravelUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionHistoryHeaderManager = new RequisitionHistoryManager();
        }

        public RequisitionHistoryForLocalTravelUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public RequisitionHistoryForLocalTravelUI(AdvanceTravelRequisitionHeader header)
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

        private void LoadFirstItem()
        {
            requisitionHistoryListView.Items[0].Selected = true;
            requisitionHistoryListView.Select();
            var requisitionHistoryHeader =
               requisitionHistoryListView.SelectedItems[0].Tag as TravelRequisitionHistoryHeader;
            LoadData(requisitionHistoryHeader);
        }

        private void LoadData(TravelRequisitionHistoryHeader requisitionHistoryHeader)
        {
            var requisitionHeader = Mapper.Map<AdvanceTravelRequisitionHeader>(requisitionHistoryHeader);
            LoadHeaderInformation(requisitionHeader);
            var historyDetails = requisitionHistoryHeader.TravelRequisitionDetailHistories.Select(c => (TravelRequisitionHistoryDetail)c).ToList();
            LoadDetailsInformation(historyDetails);
        }

        private void SetFormToViewMode()
        {
            requisitionDateTime.Enabled = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
        }

        private void LoadHeaderInformation(AdvanceTravelRequisitionHeader header)
        {
            purposeTextBox.Text = header.Purpose;
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
            sponsorNameTextBox.Text = header.IsSponsorFinanced ? header.SponsorName.ToString() : string.Empty;
            
        }

        private void LoadDetailsInformation(ICollection<TravelRequisitionHistoryDetail> details)
        {
            try
            {
                advanceDetailsListView.Items.Clear();
                List<TravelRequisitionHistoryDetail> deletedDetails = new List<TravelRequisitionHistoryDetail>();
                foreach (TravelRequisitionHistoryDetail detail in details)
                {
                    var requisitionDetail = Mapper.Map<AdvanceTravelRequisitionDetail>(detail);
                    if (detail.HistoryModeId == (long)HistoryModeEnum.Delete)
                    {
                        deletedDetails.Add(detail);
                    }
                    else
                    {
                        ListViewItem item = new ListViewItem(requisitionDetail.FromDate != null ? requisitionDetail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                        item.SubItems.Add(requisitionDetail.ToDate != null ? requisitionDetail.ToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                        item.SubItems.Add(requisitionDetail.Purpose);
                        item.SubItems.Add(requisitionDetail.UnitCost != null ? requisitionDetail.UnitCost.Value.ToString("N") : "N/A");
                        item.SubItems.Add(requisitionDetail.NoOfUnit != null ? requisitionDetail.NoOfUnit.Value.ToString("N") : "N/A");
                        item.SubItems.Add(requisitionDetail.AdvanceAmount.ToString("N"));
                        item.SubItems.Add(requisitionDetail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString("N"));
                        item.SubItems.Add(requisitionDetail.TravelSponsorFinancedDetailAmount != null ? requisitionDetail.TravelSponsorFinancedDetailAmount.Value.ToString("N") : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(requisitionDetail.Remarks) ? requisitionDetail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(requisitionDetail.ReceipientOrPayeeName) ? requisitionDetail.ReceipientOrPayeeName : "N/A");
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
                    foreach (TravelRequisitionHistoryDetail detail in deletedDetails)
                    {
                        var requisitionDetail = Mapper.Map<AdvanceTravelRequisitionDetail>(detail);

                        ListViewItem item = new ListViewItem(requisitionDetail.FromDate != null ? requisitionDetail.FromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                        item.SubItems.Add(requisitionDetail.ToDate != null ? requisitionDetail.ToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                        item.SubItems.Add(requisitionDetail.Purpose);
                        item.SubItems.Add(requisitionDetail.UnitCost != null ? requisitionDetail.UnitCost.ToString() : "N/A");
                        item.SubItems.Add(requisitionDetail.NoOfUnit != null ? requisitionDetail.NoOfUnit.ToString() : "N/A");
                        item.SubItems.Add(requisitionDetail.AdvanceAmount.ToString());
                        item.SubItems.Add(requisitionDetail.GetAdvanceAmountInBDT(Convert.ToDecimal(conversionRateTextBox.Text)).ToString());
                        item.SubItems.Add(detail.TravelSponsorFinancedDetailAmount != null ? detail.TravelSponsorFinancedDetailAmount.ToString() : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(requisitionDetail.Remarks) ? requisitionDetail.Remarks : "N/A");
                        item.SubItems.Add(!string.IsNullOrEmpty(requisitionDetail.ReceipientOrPayeeName) ? requisitionDetail.ReceipientOrPayeeName : "N/A");
                        item.SubItems.Add(detail.HistoryMode.Name);
                        item.ForeColor = Color.Red;
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

        private void RequisitionHistoryForLocalTravelUI_Load(object sender, EventArgs e)
        {
            requisitionDateTime.Value = DateTime.Now;
            LoadEmployeeInformation();
            LoadCurrencyComboBox();
            Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
            LoadFirstItem();
            SetFormTitleAndText();
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

        private void SetTotalAmountInListView(ListView advanceDetailsListViewControl)
        {
            var details = GetAdvanceRequisitionDetailsFromListView(advanceDetailsListViewControl);

            var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
            var conversionRate = Convert.ToDouble(conversionRateTextBox.Text);
            var totalAdvanceAmountInBDT = totalAdvanceAmount * (decimal)conversionRate;

            ListViewItem item = new ListViewItem();
            item.Text = string.Empty;
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(@"Total");
            item.SubItems.Add(totalAdvanceAmount.ToString());
            item.SubItems.Add(totalAdvanceAmountInBDT.ToString());
            item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            advanceDetailsListViewControl.Items.Add(item);
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

        private List<AdvanceTravelRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        {
            var details = new List<AdvanceTravelRequisitionDetail>();
            if (advanceDetailsListViewControl.Items.Count > 0)
            {
                foreach (ListViewItem item in advanceDetailsListViewControl.Items)
                {
                    var detailItem = item.Tag as TravelRequisitionHistoryDetail;
                    if (detailItem != null)
                    {
                        var requisitionDetail = Mapper.Map<AdvanceTravelRequisitionDetail>(detailItem);
                        details.Add(requisitionDetail);
                    }
                }
            }
            return details;
        }

        private void requisitionHistoryListView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var requisitionHistoryHeader =
                                requisitionHistoryListView.SelectedItems[0].Tag as TravelRequisitionHistoryHeader;
                LoadData(requisitionHistoryHeader);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionHistoryListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                _lastSelectedItem = e.Item;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionHistoryListView_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (requisitionHistoryListView.GetItemAt(e.X, e.Y) == null && _lastSelectedItem != null)
                {
                    requisitionHistoryListView.Items[_lastSelectedItem.Index].Selected = true;
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
                int index = requisitionHistoryListView.SelectedItems[0].Index;
                if (index > 0)
                {
                    requisitionHistoryListView.Items[index - 1].Selected = true;
                    requisitionHistoryListView.Select();
                    var requisitionHistoryHeader =
                       requisitionHistoryListView.SelectedItems[0].Tag as TravelRequisitionHistoryHeader;
                    LoadData(requisitionHistoryHeader);
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
                int index = requisitionHistoryListView.SelectedItems[0].Index;
                if (index < requisitionHistoryListView.Items.Count - 1)
                {
                    requisitionHistoryListView.Items[index + 1].Selected = true;
                    requisitionHistoryListView.Select();
                    var requisitionHistoryHeader =
                       requisitionHistoryListView.SelectedItems[0].Tag as TravelRequisitionHistoryHeader;
                    LoadData(requisitionHistoryHeader);
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
