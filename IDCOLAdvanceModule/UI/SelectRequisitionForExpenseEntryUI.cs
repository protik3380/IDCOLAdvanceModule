using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI.Expense;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectRequisitionForExpenseEntryUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly BaseAdvanceCategory _baseAdvanceCategory;
        private readonly string _userName = Session.LoginUserName;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private Utility.ListViewColumnSorter _requisitionColumnSorter;

        private SelectRequisitionForExpenseEntryUI()
        {
            InitializeComponent();
            InitializeColumnSorter();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
        }

        private void InitializeColumnSorter()
        {
            _requisitionColumnSorter = new Utility.ListViewColumnSorter();
            //approvedRequisitionListView.ListViewItemSorter = _requisitionColumnSorter;
        }

        public SelectRequisitionForExpenseEntryUI(BaseAdvanceCategory baseAdvanceCategory)
            : this()
        {
            _baseAdvanceCategory = baseAdvanceCategory;
        }

        private void LoadAdvanceCategoryComboBox()
        {
            List<AdvanceCategory> categoryList = new List<AdvanceCategory> { new AdvanceCategory { Id = DefaultItem.Value, Name = DefaultItem.Text } };
            if (_baseAdvanceCategory == null)
            {
                categoryList.AddRange(_advanceRequisitionCategoryManager.GetAll().ToList());
            }
            else
            {
                categoryList.AddRange(_advanceRequisitionCategoryManager.GetBy(_baseAdvanceCategory.Id));
            }
            advanceRequisitionCategoryComboBox.DataSource = null;
            advanceRequisitionCategoryComboBox.DisplayMember = "Name";
            advanceRequisitionCategoryComboBox.ValueMember = "Id";
            advanceRequisitionCategoryComboBox.DataSource = categoryList;
        }

        private void SelectRequisitionForExpenseEntryUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdvanceCategoryComboBox();
                SearchRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SearchRequisition()
        {
            var criteria = new AdvanceRequisitionSearchCriteria();
            if (_baseAdvanceCategory != null)
            {
                criteria.BaseAdvanceCategoryId = _baseAdvanceCategory.Id;
            }
            if (Convert.ToInt64(advanceRequisitionCategoryComboBox.SelectedValue) != DefaultItem.Value)
            {
                criteria.AdvanceCategoryId = Convert.ToInt32(advanceRequisitionCategoryComboBox.SelectedValue.ToString());
            }
            criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?)null;
            criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?)null;
            ICollection<Advance_VW_GetAdvanceRequisition> approvedRequisitionsList =
                _advanceVwGetAdvanceRequisitionManager.GetRequisitionByExpendetureEntry(criteria, _userName);
            LoadApprovedRequisitionGridView(approvedRequisitionsList);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                SearchRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void LoadApprovedRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> approvedRequisitionsList)
        //{
        //    approvedRequisitionListView.Items.Clear();
        //    if (approvedRequisitionsList != null && approvedRequisitionsList.Any())
        //    {
        //        approvedRequisitionsList = approvedRequisitionsList.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in approvedRequisitionsList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.RequisitionNo);
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.RequisitionCategoryName);
        //            item.SubItems.Add(criteriaVm.RequisitionDate != null
        //                ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(Convert.ToDouble(criteriaVm.AdvanceAmountInBDT).ToString("N"));

        //            item.Tag = criteriaVm;
        //            approvedRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //    //else
        //    //{
        //    //    MessageBox.Show(@"No data found", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    //}
        //}

        private void LoadApprovedRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> approvedRequisitionsList)
        {
            approvedRequisitionDataGridView.Rows.Clear();
            if (approvedRequisitionsList != null && approvedRequisitionsList.Any())
            {
                approvedRequisitionsList = approvedRequisitionsList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in approvedRequisitionsList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(approvedRequisitionDataGridView);

                    row.Cells[0].Value = "Expense Entry";
                    row.Cells[1].Value = false;
                    row.Cells[2].Value = criteriaVm.RequisitionNo;
                    row.Cells[3].Value = criteriaVm.EmployeeName;
                    row.Cells[4].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[5].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[6].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = Convert.ToDouble(criteriaVm.AdvanceAmountInBDT).ToString("N");
                    row.Tag = criteriaVm;
                    approvedRequisitionDataGridView.Rows.Add(row);
                    serial++;
                }
            }
            //else
            //{
            //    MessageBox.Show(@"No data found", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void approvedRequisitionListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                LoadCategoryWiseExpenseEntryUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void expenseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadCategoryWiseExpenseEntryUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryWiseExpenseEntryUi()
        {
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList = new List<Advance_VW_GetAdvanceRequisition>();

            foreach (DataGridViewRow row in approvedRequisitionDataGridView.SelectedRows)
            {
                if (row.Cells[1].Selected)
                {
                    DataGridViewRow i = (DataGridViewRow)row;
                    Advance_VW_GetAdvanceRequisition requisition = i.Tag as Advance_VW_GetAdvanceRequisition;
                    if (requisition == null)
                    {
                        throw new UiException("Requisition view is not tagged. Please contact to admin.");
                    }
                    requisitionList.Add(requisition);
                }
            }

            //ListView.CheckedListViewItemCollection items = approvedRequisitionListView.CheckedItems;
            //foreach (var item in items)
            //{
            //    ListViewItem i = (ListViewItem)item;
            //    Advance_VW_GetAdvanceRequisition requisition = i.Tag as Advance_VW_GetAdvanceRequisition;
            //    if (requisition == null)
            //    {
            //        throw new UiException("No requisition was tagged.");
            //    }
            //    requisitionList.Add(requisition);
            //}

            // Previous code for list view
            if (!requisitionList.Any())
            {
                throw new UiException("No requisition was found in the collection.");
            }
            List<long> advanceIdList = requisitionList.Select(c => c.AdvanceCategoryId).Distinct().ToList();
            if (advanceIdList.Count > 1)
            {
                throw new UiException("You cannot insert adjustment/reimbursement for two different category requisition.");
            }
            long advanceCategoryId = requisitionList.FirstOrDefault().AdvanceCategoryId;
            BaseAdvanceCategory category =
                _advanceRequisitionCategoryManager.GetById(advanceCategoryId).BaseAdvanceCategory;
            if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
            {
                if ((long)AdvanceCategoryEnum.LocalTravel == advanceCategoryId)
                {
                    ICollection<AdvanceTravelRequisitionHeader> requisitionHeaders = new List<AdvanceTravelRequisitionHeader>();
                    foreach (Advance_VW_GetAdvanceRequisition requisition in requisitionList)
                    {
                        AdvanceTravelRequisitionHeader header =
                            (AdvanceTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        if (_advanceExpenseHeaderManager.IsExpenseAlreadyEntryForRequisition(header.Id))
                        {
                            throw new Exception("Expense entry is already done for the requisition " + header.RequisitionNo + ".");
                        }
                        requisitionHeaders.Add(header);
                    }
                    ExpenseEntryForLocalTravelUI expenseEntryForLocalTravelUi = new ExpenseEntryForLocalTravelUI(requisitionHeaders);
                    expenseEntryForLocalTravelUi.ShowDialog();
                }
                else if ((long)AdvanceCategoryEnum.OversearTravel == advanceCategoryId)
                {
                    ICollection<AdvanceOverseasTravelRequisitionHeader> requisitionHeaders = new List<AdvanceOverseasTravelRequisitionHeader>();
                    foreach (Advance_VW_GetAdvanceRequisition requisition in requisitionList)
                    {
                        AdvanceOverseasTravelRequisitionHeader header =
                            (AdvanceOverseasTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        if (_advanceExpenseHeaderManager.IsExpenseAlreadyEntryForRequisition(header.Id))
                        {
                            throw new Exception("Expense entry is already done for the requisition " + header.RequisitionNo + ".");
                        }
                        requisitionHeaders.Add(header);
                    }
                    ExpenseEntryForOverseasTravelUI expenseEntryForOverseasTravelUi = new ExpenseEntryForOverseasTravelUI(requisitionHeaders);
                    expenseEntryForOverseasTravelUi.ShowDialog();
                }
            }
            else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
            {
                ICollection<AdvancePettyCashRequisitionHeader> requisitionHeaders = new List<AdvancePettyCashRequisitionHeader>();
                foreach (Advance_VW_GetAdvanceRequisition requisition in requisitionList)
                {
                    AdvancePettyCashRequisitionHeader header =
                        (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                    if (_advanceExpenseHeaderManager.IsExpenseAlreadyEntryForRequisition(header.Id))
                    {
                        throw new Exception("Expense entry is already done for the requisition " + header.RequisitionNo + ".");
                    }
                    requisitionHeaders.Add(header);
                }
                ExpenseEntryForPettyCashUI expenseEntryForPettyCashUi = new ExpenseEntryForPettyCashUI(requisitionHeaders);
                expenseEntryForPettyCashUi.ShowDialog();
            }
            else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
            {
                if ((long)AdvanceCategoryEnum.CorporateAdvisory == advanceCategoryId)
                {
                    ICollection<AdvanceCorporateAdvisoryRequisitionHeader> requisitionHeaders = new List<AdvanceCorporateAdvisoryRequisitionHeader>();
                    foreach (Advance_VW_GetAdvanceRequisition requisition in requisitionList)
                    {
                        AdvanceCorporateAdvisoryRequisitionHeader header =
                            (AdvanceCorporateAdvisoryRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        if (_advanceExpenseHeaderManager.IsExpenseAlreadyEntryForRequisition(header.Id))
                        {
                            throw new Exception("Expense entry is already done for the requisition " + header.RequisitionNo + ".");
                        }
                        requisitionHeaders.Add(header);
                    }
                    ExpenseEntryForCorporateAdvisoryUI expenseEntryForMiscellaneousUi = new ExpenseEntryForCorporateAdvisoryUI(requisitionHeaders);
                    expenseEntryForMiscellaneousUi.ShowDialog();
                }
                else
                {
                    ICollection<AdvanceMiscelleneousRequisitionHeader> requisitionHeaders = new List<AdvanceMiscelleneousRequisitionHeader>();
                    foreach (Advance_VW_GetAdvanceRequisition requisition in requisitionList)
                    {
                        AdvanceMiscelleneousRequisitionHeader header =
                            (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        if (_advanceExpenseHeaderManager.IsExpenseAlreadyEntryForRequisition(header.Id))
                        {
                            throw new Exception("Expense entry is already done for the requisition " + header.RequisitionNo + ".");
                        }
                        requisitionHeaders.Add(header);
                    }
                    ExpenseEntryForMiscellaneousUI expenseEntryForMiscellaneousUi = new ExpenseEntryForMiscellaneousUI(requisitionHeaders);
                    expenseEntryForMiscellaneousUi.ShowDialog();
                }
            }
        }

        private void expenseEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (approvedRequisitionListView.CheckedItems.Count > 0)
                //{
                //    LoadCategoryWiseExpenseEntryUi();
                //}
                //foreach (DataGridViewRow row in approvedRequisitionDataGridView.Rows)
                //{
                //    if ((bool)row.Cells[1].Value == true)
                //    {
                //        LoadCategoryWiseExpenseEntryUi();
                //    }
                //    else
                //    {
                //        MessageBox.Show(@"Select atleast one requisition.", @"Warning!", MessageBoxButtons.OK,
                //            MessageBoxIcon.Error);
                //    }
                //}
                if (approvedRequisitionDataGridView.SelectedRows.Count > 0)
                {
                    LoadCategoryWiseExpenseEntryUi();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvedRequisitionListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                // Utility.Utility.SortColumn(e, approvedRequisitionListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvedRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    try
                    {
                        LoadCategoryWiseExpenseEntryUi();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }


            }
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                e.RowIndex >= 0)
            {

                toggleCellCheck(e.RowIndex, e.ColumnIndex);

            }
        }
        private void toggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)approvedRequisitionDataGridView[column, row].EditedFormattedValue;
            approvedRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

    }
}
