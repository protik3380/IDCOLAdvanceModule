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
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.History.RequisitionHistory
{
    public partial class RequisitionHistoryForOverseasTravelUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly UserTable _selectedEmployee;
        private readonly AdvanceCategory _advanceCategory;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IRequisitionHistoryHeaderManager _requisitionHistoryHeaderManager;
        private ListViewItem _lastSelectedItem;
        private readonly IPlaceofVistManager _placeofVistManager;

        private RequisitionHistoryForOverseasTravelUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionHistoryHeaderManager = new RequisitionHistoryManager();
            _placeofVistManager = new PlaceOfVisitManager();
        }

        public RequisitionHistoryForOverseasTravelUI(UserTable employee, AdvanceCategory advanceCategory)
            : this()
        {
            _selectedEmployee = employee;
            _advanceCategory = advanceCategory;
        }

        public RequisitionHistoryForOverseasTravelUI(AdvanceOverseasTravelRequisitionHeader header)
            : this()
        {
            var
            requisitionHistories = _requisitionHistoryHeaderManager.GetAllByRequisitionHeaderId(header.Id);
            LoadhistoryGroupBox(requisitionHistories);
            _selectedEmployee = _employeeManager.GetByUserName(header.RequesterUserName);
            _advanceCategory = _advanceRequisitionCategoryManager.GetById(header.AdvanceCategoryId);
            SetFormToViewMode();
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

        private void LoadFirstItem()
        {
            requisitionHistoryListView.Items[0].Selected = true;
            requisitionHistoryListView.Select();
            var requisitionHistoryHeader =
               requisitionHistoryListView.SelectedItems[0].Tag as OverseasTravelRequisitionHistoryHeader;
            LoadData(requisitionHistoryHeader);
        }

        private void LoadData(OverseasTravelRequisitionHistoryHeader requisitionHistoryHeader)
        {
            var requisitionHeader = Mapper.Map<AdvanceOverseasTravelRequisitionHeader>(requisitionHistoryHeader);
            LoadHeaderInformation(requisitionHeader);
            var historyDetails = requisitionHistoryHeader.OverseasTravelRequisitionDetailHistories.Select(c => (OverseasTravelRequisitionHistoryDetail)c).ToList();
            LoadDetailsInformation(historyDetails);
        }

        private void SetFormToViewMode()
        {
            requisitionDateTime.Enabled = false;
            employeeInfoGroupBox.Enabled = false;
            headerInfoGroupBox.Enabled = false;
        }

        private void LoadHeaderInformation(AdvanceOverseasTravelRequisitionHeader header)
        {
            purposeOfTravelTextBox.Text = header.Purpose;
            fromDateTimePicker.Value = header.FromDate;
            fromDateTimePicker.Checked = true;
            toDateTimePicker.Value = header.ToDate;
            toDateTimePicker.Checked = true;
            requisitionDateTime.Value = header.RequisitionDate;
            requisitionDateTime.Checked = true;
            noOfDaysTextBox.Text = header.NoOfDays.ToString();
            yesRadioButton.Checked = header.IsOverseasSponsorFinanced;
            sponsorNameTextBox.Text = header.IsOverseasSponsorFinanced ? header.OverseasSponsorName.ToString() : string.Empty;
            countryNameTextBox.Text = header.CountryName == null ? string.Empty : header.CountryName;
            placeOfVisitComboBox.SelectedValue = header.PlaceOfVisitId;
        }

        private void LoadDetailsInformation(ICollection<OverseasTravelRequisitionHistoryDetail> details)
        {
            advanceDetailsListView.Items.Clear();
            List<OverseasTravelRequisitionHistoryDetail> deletedDetails = new List<OverseasTravelRequisitionHistoryDetail>();
            foreach (OverseasTravelRequisitionHistoryDetail detail in details)
            {
                var requisitionDetail = Mapper.Map<AdvanceOverseasTravelRequisitionDetail>(detail);
                if (detail.HistoryModeId == (long)HistoryModeEnum.Delete)
                {
                    deletedDetails.Add(detail);
                }
                else
                {
                    ListViewItem item = new ListViewItem(requisitionDetail.OverseasFromDate != null ? requisitionDetail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                    item.SubItems.Add(requisitionDetail.OverseasToDate != null ? requisitionDetail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                    item.SubItems.Add(requisitionDetail.Purpose);
                    item.SubItems.Add(requisitionDetail.UnitCost != 0 || requisitionDetail.UnitCost != null ? requisitionDetail.UnitCost.ToString() : "N/A");
                    item.SubItems.Add(requisitionDetail.NoOfUnit != 0 ? requisitionDetail.NoOfUnit.ToString() : "N/A");
                    item.SubItems.Add(requisitionDetail.GetAdvanceAmountInBDT(Convert.ToDecimal(requisitionDetail.ConversionRate)).ToString());
                    item.SubItems.Add(requisitionDetail.OverseasSponsorFinancedDetailAmount == null ? "N/A" : requisitionDetail.OverseasSponsorFinancedDetailAmount.ToString());
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
                foreach (OverseasTravelRequisitionHistoryDetail detail in deletedDetails)
                {
                    var requisitionDetail = Mapper.Map<AdvanceOverseasTravelRequisitionDetail>(detail);

                    ListViewItem item = new ListViewItem(requisitionDetail.OverseasFromDate != null ? requisitionDetail.OverseasFromDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                    item.SubItems.Add(requisitionDetail.OverseasToDate != null ? requisitionDetail.OverseasToDate.Value.Date.ToString("dd/MM/yyyy") : string.Empty);
                    item.SubItems.Add(requisitionDetail.Purpose);
                    item.SubItems.Add(requisitionDetail.UnitCost != 0 || requisitionDetail.UnitCost != null ? requisitionDetail.UnitCost.ToString() : "N/A");
                    item.SubItems.Add(requisitionDetail.NoOfUnit != 0 ? requisitionDetail.NoOfUnit.ToString() : "N/A");
                    item.SubItems.Add(requisitionDetail.GetAdvanceAmountInBDT(Convert.ToDecimal(requisitionDetail.ConversionRate)).ToString());
                    item.SubItems.Add(requisitionDetail.OverseasSponsorFinancedDetailAmount == null ? "N/A" : requisitionDetail.OverseasSponsorFinancedDetailAmount.ToString());
                    item.SubItems.Add(!string.IsNullOrEmpty(requisitionDetail.Remarks) ? requisitionDetail.Remarks : "N/A");
                    item.SubItems.Add(!string.IsNullOrEmpty(requisitionDetail.ReceipientOrPayeeName) ? requisitionDetail.ReceipientOrPayeeName : "N/A");
                    item.SubItems.Add(detail.HistoryMode.Name);
                    item.ForeColor = Color.Red;
                    item.Tag = detail;
                    advanceDetailsListView.Items.Add(item);
                }
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

        private void RequisitionHistoryForOverseasTravelUI_Load(object sender, EventArgs e)
        {
            requisitionDateTime.Value = DateTime.Now;
            LoadPlaceOfVisit();
            LoadEmployeeInformation();
            Utility.Utility.CalculateNoOfDays(fromDateTimePicker.Value, toDateTimePicker.Value, noOfDaysTextBox);
            LoadFirstItem();
            SetFormTitleAndText();
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

            var totalAdvanceAmountInBDT = details.Sum(c => c.AdvanceAmount * Convert.ToDecimal(c.ConversionRate));
            var totalSponsorAmountInBDT = details.Sum(c => c.OverseasSponsorFinancedDetailAmount) ?? 0;

            ListViewItem item = new ListViewItem();
            item.Text = string.Empty;
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(@"Total");
            item.SubItems.Add(totalAdvanceAmountInBDT.ToString());
            item.SubItems.Add(totalSponsorAmountInBDT.ToString());
            item.Font = new Font(Utility.Utility.FontName, 10, FontStyle.Bold);
            advanceDetailsListViewControl.Items.Add(item);
        }

        private List<AdvanceOverseasTravelRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        {
            var details = new List<AdvanceOverseasTravelRequisitionDetail>();
            if (advanceDetailsListViewControl.Items.Count > 0)
            {
                foreach (ListViewItem item in advanceDetailsListViewControl.Items)
                {
                    var detailItem = item.Tag as OverseasTravelRequisitionHistoryDetail;
                    if (detailItem != null)
                    {
                        var requisitionDetail = Mapper.Map<AdvanceOverseasTravelRequisitionDetail>(detailItem);
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
                                requisitionHistoryListView.SelectedItems[0].Tag as OverseasTravelRequisitionHistoryHeader;
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
                       requisitionHistoryListView.SelectedItems[0].Tag as OverseasTravelRequisitionHistoryHeader;
                    LoadData(requisitionHistoryHeader);
                }
                else
                {
                    MessageBox.Show(@"Sorry, you have reached last history.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                       requisitionHistoryListView.SelectedItems[0].Tag as OverseasTravelRequisitionHistoryHeader;
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
