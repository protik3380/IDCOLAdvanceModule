using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI.Expense;
using IDCOLAdvanceModule.UI._360View;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using IDCOLAdvanceModule.UI.History.ExpenseHistory;

namespace IDCOLAdvanceModule.UI
{
    public partial class MyExpenseUI : Form
    {
        private readonly string _userName = Session.LoginUserName;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;
        private readonly IAdvance_VW_GetAdvanceExpenseManager _advanceVwGetAdvanceExpenseManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IRequisitionApprovalTicketManager _requisitionApprovalTicketManager;
        private readonly IRequisitionApprovalTrackerManager _requisitionApprovalTrackerManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IApprovalProcessManager _approvalProcessManager;

        public MyExpenseUI()
        {
            _advanceVwGetAdvanceExpenseManager = new AdvanceVwGetAdvanceExpenseManager();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionApprovalTicketManager = new RequisitionApprovalTicketManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _approvalProcessManager = new ApprovalProcessManager();
            _requisitionApprovalTrackerManager = new RequisitionApprovalTrackerManager();
            InitializeComponent();
            InitializeColumnSorter();
            ExpenseTabControl.SelectedTab = draftTabPage;
        }

        private void InitializeColumnSorter()
        {
            //Utility.ListViewColumnSorter draftColumnSorter = new Utility.ListViewColumnSorter();
            //draftAdvanceExpenseListView.ListViewItemSorter = draftColumnSorter;
            //Utility.ListViewColumnSorter sentColumnSorter = new Utility.ListViewColumnSorter();
            //sentAdvanceExpenseListView.ListViewItemSorter = sentColumnSorter;
            //Utility.ListViewColumnSorter approvedColumnSorter = new Utility.ListViewColumnSorter();
            //approvedAdvanceExpenseListView.ListViewItemSorter = approvedColumnSorter;
            //Utility.ListViewColumnSorter revertedColumnSorter = new Utility.ListViewColumnSorter();
            //revertedAdvanceExpenseListView.ListViewItemSorter = revertedColumnSorter;
            //Utility.ListViewColumnSorter rejectedColumnSorter = new Utility.ListViewColumnSorter();
            //rejectedAdvanceExpenseListView.ListViewItemSorter = rejectedColumnSorter;
            //Utility.ListViewColumnSorter trashColumnSorter = new Utility.ListViewColumnSorter();
            //trashAdvanceExpenseListView.ListViewItemSorter = trashColumnSorter;
        }

        private void LoadAdvanceRequisitionCategoryComboBox()
        {
            List<AdvanceCategory> categoryList = _advanceRequisitionCategoryManager.GetAll().ToList();
            categoryList.Insert(0, new AdvanceCategory { Id = DefaultItem.Value, Name = DefaultItem.Text });
            advanceRequisitionCategoryComboBox.DataSource = null;
            advanceRequisitionCategoryComboBox.DisplayMember = "Name";
            advanceRequisitionCategoryComboBox.ValueMember = "Id";
            advanceRequisitionCategoryComboBox.DataSource = categoryList;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                SearchExpense();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchExpense()
        {
            EmployeeRequisitionSearchCriteria criteria = new EmployeeRequisitionSearchCriteria();
            criteria.IsRequisitionForLoggedInUser = true;
            if (Convert.ToInt64(advanceRequisitionCategoryComboBox.SelectedValue.ToString()) != DefaultItem.Value)
            {
                criteria.AdvanceCategoryId =
                    Convert.ToInt32(advanceRequisitionCategoryComboBox.SelectedValue.ToString());
            }

            criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?)null;
            criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?)null;
            ICollection<Advance_VW_GetAdvanceExpense> draftExpenses =
                _approvalProcessManager.GetDraftExpensesForMember(_userName, criteria);
            LoadDraftAdvanceExpenseGridView(draftExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> sentExpenses =
              _approvalProcessManager.GetSentExpensesForMember(_userName, criteria);
            LoadSentAdvanceExpenseGridView(sentExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> approvedExpenses =
              _approvalProcessManager.GetApprovedExpensesForMember(_userName, criteria);
            LoadApprovedAdvanceExpenseGridView(approvedExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> paidExpenses =
                _approvalProcessManager.GetPaidExpensesForMember(_userName, criteria);
            LoadPaidAdvanceExpenseGridView(paidExpenses);

            ICollection<Advance_VW_GetAdvanceExpense> receivedExpenses =
                _approvalProcessManager.GetReceivedExpensesForMember(_userName, criteria);
            LoadReceivedAdvanceExpenseGridView(receivedExpenses);

            ICollection<Advance_VW_GetAdvanceExpense> revertedExpenses =
              _approvalProcessManager.GetRevertedExpensesForMember(_userName, criteria);
            LoadRevertAdvanceExpenseGridView(revertedExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> rejectedExpenses =
              _approvalProcessManager.GetRejectedExpensesForMember(_userName, criteria);
            LoadRejectedAdvanceExpenseGridView(rejectedExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> removeExpenses =
              _approvalProcessManager.GetRemovedExpenses(_userName, criteria);
            LoadRemovedAdvanceExpenseGridView(removeExpenses);
        }

        private void EmployeeExpenseUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdvanceRequisitionCategoryComboBox();
                LoadGridData();
               
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGridData()
        {
            ICollection<Advance_VW_GetAdvanceExpense> draftExpenses =
                    _approvalProcessManager.GetDraftExpensesForMember(_userName);
            LoadDraftAdvanceExpenseGridView(draftExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> sentExpenses =
                _approvalProcessManager.GetSentExpensesForMember(_userName);
            LoadSentAdvanceExpenseGridView(sentExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> approvedExpenses =
                _approvalProcessManager.GetApprovedExpensesForMember(_userName);
            LoadApprovedAdvanceExpenseGridView(approvedExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> paidExpenses =
                _approvalProcessManager.GetPaidExpensesForMember(_userName);
            LoadPaidAdvanceExpenseGridView(paidExpenses);

            ICollection<Advance_VW_GetAdvanceExpense> receivedExpenses =
                _approvalProcessManager.GetReceivedExpensesForMember(_userName);
            LoadReceivedAdvanceExpenseGridView(receivedExpenses);

            ICollection<Advance_VW_GetAdvanceExpense> revertedExpenses =
                _approvalProcessManager.GetRevertedExpensesForMember(_userName);
            LoadRevertAdvanceExpenseGridView(revertedExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> rejectedExpenses =
                _approvalProcessManager.GetRejectedExpensesForMember(_userName);
            LoadRejectedAdvanceExpenseGridView(rejectedExpenses);
            ICollection<Advance_VW_GetAdvanceExpense> removedExpenses =
                _approvalProcessManager.GetRemovedExpensesForMember(_userName);
            LoadRemovedAdvanceExpenseGridView(removedExpenses);
        }

        //private void LoadRemovedAdvanceExpenseListView(ICollection<Advance_VW_GetAdvanceExpense> removeExpenses)
        //{
        //    trashAdvanceExpenseListView.Items.Clear();
        //    if (removeExpenses != null && removeExpenses.Any())
        //    {
        //        removeExpenses = removeExpenses.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense criteriaVm in removeExpenses)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.ExpenseNo);
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.AdvanceCategoryName);
        //            item.SubItems.Add(criteriaVm.ExpenseEntryDate != null
        //                ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(criteriaVm.ExpenseAmountInBDT.ToString("N"));

        //            item.Tag = criteriaVm;
        //            trashAdvanceExpenseListView.Items.Add(item);
        //            serial++;
        //        }
        //        trashTabPage.Text = @"Trash (" + removeExpenses.Count + @")";
        //    }
        //    else
        //    {
        //        trashTabPage.Text = @"Trash (0)";
        //    }
        //}

        private void LoadRemovedAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> removeExpenses)
        {
            trashAdvanceExpenseDataGridView.Rows.Clear();
            if (removeExpenses != null && removeExpenses.Any())
            {
                removeExpenses = removeExpenses.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in removeExpenses)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(trashAdvanceExpenseDataGridView);


                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.ExpenseNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[6].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[10].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");

                    row.Tag = criteriaVm;
                    trashAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;
                }
                trashTabPage.Text = @"Trash (" + removeExpenses.Count + @")";
            }
            else
            {
                trashTabPage.Text = @"Trash (0)";
            }
        }

        //private void LoadDraftAdvanceExpenseListView(ICollection<Advance_VW_GetAdvanceExpense> draftExpenseList)
        //{
        //    draftAdvanceExpenseListView.Items.Clear();
        //    if (draftExpenseList != null && draftExpenseList.Any())
        //    {
        //        draftExpenseList = draftExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense criteriaVm in draftExpenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.ExpenseNo);
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.AdvanceCategoryName);
        //            item.SubItems.Add(criteriaVm.ExpenseEntryDate != null
        //                ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(criteriaVm.ExpenseAmountInBDT.ToString("N"));

        //            item.Tag = criteriaVm;
        //            draftAdvanceExpenseListView.Items.Add(item);
        //            serial++;
        //        }
        //        draftTabPage.Text = @"Draft (" + draftExpenseList.Count + @")";
        //    }
        //    else
        //    {
        //        draftTabPage.Text = @"Draft (0)";
        //    }
        //}

        private void LoadDraftAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> draftExpenseList)
        {
            draftAdvanceExpenseDataGridView.Rows.Clear();
            if (draftExpenseList != null && draftExpenseList.Any())
            {
                draftExpenseList = draftExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in draftExpenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(draftAdvanceExpenseDataGridView);


                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = "Edit";
                    row.Cells[3].Value = "Remove";
                    row.Cells[4].Value = false;
                    row.Cells[5].Value = serial.ToString();
                    row.Cells[6].Value = criteriaVm.ExpenseNo;
                    row.Cells[7].Value = criteriaVm.EmployeeName;
                    row.Cells[8].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[9].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[11].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[12].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[13].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");

                    row.Tag = criteriaVm;
                    draftAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;
                }
                draftTabPage.Text = @"Draft (" + draftExpenseList.Count + @")";
            }
            else
            {
                draftTabPage.Text = @"Draft (0)";
            }
        }

        //private void LoadApprovedAdvanceExpenseListView(ICollection<Advance_VW_GetAdvanceExpense> approvedExpenseList)
        //{
        //    approvedAdvanceExpenseListView.Items.Clear();
        //    if (approvedExpenseList != null && approvedExpenseList.Any())
        //    {
        //        approvedExpenseList = approvedExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense criteriaVm in approvedExpenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.ExpenseNo);
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.AdvanceCategoryName);
        //            item.SubItems.Add(criteriaVm.ExpenseEntryDate != null
        //                ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(criteriaVm.ExpenseAmountInBDT.ToString("N"));

        //            item.Tag = criteriaVm;
        //            approvedAdvanceExpenseListView.Items.Add(item);
        //            serial++;
        //        }
        //        approvedTabPage.Text = @"Approved (" + approvedExpenseList.Count + @")";
        //    }
        //    else
        //    {
        //        approvedTabPage.Text = @"Approved (0)";
        //    }
        //}
        private void LoadPaidAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            paidAdvanceExpenseDataGridView.Rows.Clear();
            if (expenseList != null && expenseList.Any())
            {
                expenseList = expenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in expenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(paidAdvanceExpenseDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = "Received";
                    row.Cells[3].Value = serial.ToString();
                    row.Cells[4].Value = criteriaVm.ExpenseNo;
                    row.Cells[5].Value = criteriaVm.EmployeeName;
                    row.Cells[6].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[7].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[11].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");

                    row.Tag = criteriaVm;
                    paidAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;
                }
                paidTabPage.Text = @"Paid (" + expenseList.Count + @")";
            }
            else
            {
                paidTabPage.Text = @"Paid (0)";
            }
        }

        private void LoadApprovedAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> approvedExpenseList)
        {
            approvedAdvanceExpenseDataGridView.Rows.Clear();
            if (approvedExpenseList != null && approvedExpenseList.Any())
            {
                approvedExpenseList = approvedExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in approvedExpenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(approvedAdvanceExpenseDataGridView);


                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.ExpenseNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[6].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[10].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");

                    row.Tag = criteriaVm;
                    approvedAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;
                }
                approvedTabPage.Text = @"Approved (" + approvedExpenseList.Count + @")";
            }
            else
            {
                approvedTabPage.Text = @"Approved (0)";
            }
        }

        private void LoadReceivedAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            receivedAdvanceExpenseDataGridView.Rows.Clear();
            if (expenseList != null && expenseList.Any())
            {
                expenseList = expenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in expenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(receivedAdvanceExpenseDataGridView);


                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.ExpenseNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[6].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[10].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");

                    row.Tag = criteriaVm;
                    receivedAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;
                }
                receivedTabPage.Text = @"Received (" + expenseList.Count + @")";
            }
            else
            {
                receivedTabPage.Text = @"Received (0)";
            }
        }
        //private void LoadSentAdvanceExpenseListView(ICollection<Advance_VW_GetAdvanceExpense> sentExpenseList)
        //{
        //    sentAdvanceExpenseListView.Items.Clear();
        //    if (sentExpenseList != null && sentExpenseList.Any())
        //    {
        //        sentExpenseList = sentExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense criteriaVm in sentExpenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.ExpenseNo);
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.AdvanceCategoryName);
        //            item.SubItems.Add(criteriaVm.ExpenseEntryDate != null
        //                ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(criteriaVm.ExpenseAmountInBDT.ToString("N"));

        //            item.Tag = criteriaVm;
        //            sentAdvanceExpenseListView.Items.Add(item);
        //            serial++;
        //        }
        //        sentTabPage.Text = @"Sent (" + sentExpenseList.Count + @")";
        //    }
        //    else
        //    {
        //        sentTabPage.Text = @"Sent (0)";
        //    }
        //}


        private void LoadSentAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> sentExpenseList)
        {
            sentAdvanceExpenseDataGridView.Rows.Clear();
            if (sentExpenseList != null && sentExpenseList.Any())
            {
                sentExpenseList = sentExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in sentExpenseList)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(sentAdvanceExpenseDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.ExpenseNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[6].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[10].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");


                    row.Tag = criteriaVm;
                    sentAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;
                }
                sentTabPage.Text = @"Sent (" + sentExpenseList.Count + @")";
            }
            else
            {
                sentTabPage.Text = @"Sent (0)";
            }
        }
        //private void LoadRevertAdvanceExpenseListView(ICollection<Advance_VW_GetAdvanceExpense> revertedExpenseList)
        //{
        //    revertedAdvanceExpenseListView.Items.Clear();
        //    if (revertedExpenseList != null && revertedExpenseList.Any())
        //    {
        //        revertedExpenseList = revertedExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense criteriaVm in revertedExpenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.ExpenseNo);
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.AdvanceCategoryName);
        //            item.SubItems.Add(criteriaVm.ExpenseEntryDate != null
        //                ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(criteriaVm.ExpenseAmountInBDT.ToString("N"));

        //            item.Tag = criteriaVm;
        //            revertedAdvanceExpenseListView.Items.Add(item);
        //            serial++;
        //        }
        //        revertedTabPage.Text = @"Reverted (" + revertedExpenseList.Count + @")";
        //    }
        //    else
        //    {
        //        revertedTabPage.Text = @"Reverted (0)";
        //    }
        //}

        private void LoadRevertAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> revertedExpenseList)
        {
            revertedAdvanceExpenseDataGridView.Rows.Clear();
            if (revertedExpenseList != null && revertedExpenseList.Any())
            {
                revertedExpenseList = revertedExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in revertedExpenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(revertedAdvanceExpenseDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = "Edit";
                    row.Cells[3].Value = false;
                    row.Cells[4].Value = serial.ToString();
                    row.Cells[5].Value = criteriaVm.ExpenseNo;
                    row.Cells[6].Value = criteriaVm.EmployeeName;
                    row.Cells[7].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[8].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[11].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[12].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");


                    row.Tag = criteriaVm;
                    revertedAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;

                }
                revertedTabPage.Text = @"Reverted (" + revertedExpenseList.Count + @")";
            }
            else
            {
                revertedTabPage.Text = @"Reverted (0)";
            }
        }

        //private void LoadRejectedAdvanceExpenseListView(ICollection<Advance_VW_GetAdvanceExpense> rejectedExpenseList)
        //{
        //    rejectedAdvanceExpenseListView.Items.Clear();
        //    if (rejectedExpenseList != null && rejectedExpenseList.Any())
        //    {
        //        rejectedExpenseList = rejectedExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense criteriaVm in rejectedExpenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.ExpenseNo);
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.AdvanceCategoryName);
        //            item.SubItems.Add(criteriaVm.ExpenseEntryDate != null
        //                ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(criteriaVm.ExpenseAmountInBDT.ToString("N"));

        //            item.Tag = criteriaVm;
        //            rejectedAdvanceExpenseListView.Items.Add(item);
        //            serial++;
        //        }
        //        rejectedTabPage.Text = @"Rejected (" + rejectedExpenseList.Count + @")";
        //    }
        //    else
        //    {
        //        rejectedTabPage.Text = @"Rejected (0)";
        //    }
        //}


        private void LoadRejectedAdvanceExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> rejectedExpenseList)
        {
            rejectedAdvanceExpenseDataGridView.Rows.Clear();
            if (rejectedExpenseList != null && rejectedExpenseList.Any())
            {
                rejectedExpenseList = rejectedExpenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense criteriaVm in rejectedExpenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(rejectedAdvanceExpenseDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.ExpenseNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.AdvanceCategoryName;
                    row.Cells[6].Value = criteriaVm.ExpenseEntryDate != null
                        ? criteriaVm.ExpenseEntryDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[10].Value = criteriaVm.ExpenseAmountInBDT.ToString("N");


                    row.Tag = criteriaVm;
                    rejectedAdvanceExpenseDataGridView.Rows.Add(row);
                    serial++;
                }
                rejectedTabPage.Text = @"Rejected (" + rejectedExpenseList.Count + @")";
            }
            else
            {
                rejectedTabPage.Text = @"Rejected (0)";
            }
        }
        private void SendForApprovalInRevertButton_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;

                foreach (DataGridViewRow row in revertedAdvanceExpenseDataGridView.Rows)
                {
                    bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                    if (isChecked)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    DialogResult result = MessageBox.Show(@"Are you sure you want to send all checked adjustments for approval?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    ICollection<AdvanceExpenseHeader> expenseHeaders = new List<AdvanceExpenseHeader>();
                    foreach (DataGridViewRow checkedItem in revertedAdvanceExpenseDataGridView.Rows)
                    {
                        if ((bool)checkedItem.Cells[3].EditedFormattedValue)
                        {
                            var expense = checkedItem.Tag as Advance_VW_GetAdvanceExpense;
                            if (expense != null)
                            {
                                AdvanceExpenseHeader header = _advanceExpenseHeaderManager.GetById(expense.HeaderId);
                                if (header != null)
                                {
                                    expenseHeaders.Add(header);
                                }
                            }
                        }

                    }
                    bool isSentForApproval = _approvalProcessManager.SendRevertedExpensesForApproval(expenseHeaders, _userName);
                    SearchExpense();
                    if (isSentForApproval)
                    {
                        MessageBox.Show(@"Expense is sent for Approval. You can find this expense now in Sent tab", @"Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(@"Something went worng.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectAllInRevertmetroButton_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;

                foreach (DataGridViewRow row in revertedAdvanceExpenseDataGridView.Rows)
                {
                    bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                    if (isChecked)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    if (selectAllInRevertmetroButton.Text.Equals("Select All"))
                    {
                        selectAllInRevertmetroButton.Text = @"Unselect All";
                        RevertSelectOrUnSelectAllGridItem(revertedAdvanceExpenseDataGridView, true);
                    }
                    else
                    {
                        selectAllInRevertmetroButton.Text = @"Select All";
                        RevertSelectOrUnSelectAllGridItem(revertedAdvanceExpenseDataGridView, false);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RevertSelectOrUnSelectAllGridItem(DataGridView gridView, bool isChecked)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.Cells[3].Value = isChecked;
            }

        }
        private void selectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;

                foreach (DataGridViewRow row in draftAdvanceExpenseDataGridView.Rows)
                {
                    row.Cells[4].Value = true;

                    count++;
                }
                if (count > 0)
                {
                    if (selectAllButton.Text.Equals("Select All"))
                    {
                        selectAllButton.Text = @"Deselect All";
                        Utility.Utility.SelectOrUnSelectAllGridItem(draftAdvanceExpenseDataGridView, true);
                    }
                    else
                    {
                        selectAllButton.Text = @"Select All";
                        Utility.Utility.SelectOrUnSelectAllGridItem(draftAdvanceExpenseDataGridView, false);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendApprovalButton_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;

                foreach (DataGridViewRow row in draftAdvanceExpenseDataGridView.Rows)
                {
                    bool isChecked = (bool)row.Cells[4].EditedFormattedValue;

                    if (isChecked)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    DialogResult result = MessageBox.Show(@"Are you sure you want to send all checked adjustments for approval?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    ICollection<AdvanceExpenseHeader> expenseHeaders = new List<AdvanceExpenseHeader>();
                    foreach (DataGridViewRow checkedItem in draftAdvanceExpenseDataGridView.Rows)
                    {
                        if ((bool)checkedItem.Cells[4].EditedFormattedValue)
                        {
                            var expense = checkedItem.Tag as Advance_VW_GetAdvanceExpense;
                            if (expense != null)
                            {
                                AdvanceExpenseHeader header = _advanceExpenseHeaderManager.GetById(expense.HeaderId);
                                if (header != null)
                                {
                                    expenseHeaders.Add(header);
                                }
                            }
                        }

                    }
                    var ticket = _approvalProcessManager.SendExpenseForApproval(expenseHeaders, _userName);
                    if (ticket != null && ticket.Any())
                    {
                        MessageBox.Show(@"Expense is sent for Approval. You can find this expense now in Sent tab.", @"Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    SearchExpense();
                }
                else
                {
                    MessageBox.Show(@"Please select an expense.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expense360ViewDraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ListView listView = draftAdvanceExpenseListView;
                //ShowExpense360View(listView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editDraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ListView listView = draftAdvanceExpenseListView;
                //ShowEditExpenseForm(listView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editRevertedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ListView listView = revertedAdvanceExpenseListView;
                //ShowEditExpenseForm(listView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expense360ViewRevertedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ListView listView = revertedAdvanceExpenseListView;
                //ShowExpense360View(listView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expense360ViewSentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ListView listView = sentAdvanceExpenseListView;
                //ShowExpense360View(listView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expense360ViewApprovedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ListView listView = approvedAdvanceExpenseListView;
                //ShowExpense360View(listView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void ShowExpense360View(ListView listView)
        //{
        //    if (listView != null && listView.SelectedItems.Count > 0)
        //    {
        //        var selectedItem = listView.SelectedItems[0];

        //        var expenseHeader = selectedItem.Tag as Advance_VW_GetAdvanceExpense;

        //        if (expenseHeader != null)
        //        {
        //            Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expenseHeader.HeaderId);
        //            expense360ViewUi.Show();
        //        }
        //    }
        //}
        private void ShowExpense360View(DataGridView gridView)
        {
            if (gridView != null && gridView.SelectedRows.Count > 0)
            {
                var selectedItem = gridView.SelectedRows[0];

                var expenseHeader = selectedItem.Tag as Advance_VW_GetAdvanceExpense;

                if (expenseHeader != null)
                {
                    Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expenseHeader.HeaderId);
                    expense360ViewUi.Show();
                }
            }
        }

        private void ShowEditExpenseForm(DataGridView gridView)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                var requisition =
                    gridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                if (requisition != null)
                {
                    AdvanceCategory advanceCategory = _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId);
                    BaseAdvanceCategory category =
                        _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;
                    string title = "Expense Entry Form (" + advanceCategory.Name + ")";

                    if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                    {
                        if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                        {
                            AdvanceTravelExpenseHeader header =
                                (AdvanceTravelExpenseHeader)_advanceExpenseHeaderManager.GetById(requisition.HeaderId);

                            ExpenseEntryForLocalTravelUI localTravelUi = new ExpenseEntryForLocalTravelUI(header,
                                AdvancedFormMode.Update);
                            localTravelUi.Text = title;
                            localTravelUi.titleLabel.Text = title;
                            localTravelUi.Show();
                        }
                        else if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                        {
                            AdvanceOverseasTravelExpenseHeader header =
                                (AdvanceOverseasTravelExpenseHeader)_advanceExpenseHeaderManager.GetById(requisition.HeaderId);

                            ExpenseEntryForOverseasTravelUI travelUi = new ExpenseEntryForOverseasTravelUI(header,
                                AdvancedFormMode.Update);
                            travelUi.Text = title;
                            travelUi.titleLabel.Text = title;
                            travelUi.Show();
                        }
                    }
                    else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                    {
                        AdvancePettyCashExpenseHeader header =
                            (AdvancePettyCashExpenseHeader)_advanceExpenseHeaderManager.GetById(requisition.HeaderId);

                        ExpenseEntryForPettyCashUI pettyUi = new ExpenseEntryForPettyCashUI(header, AdvancedFormMode.Update);
                        pettyUi.Text = title;
                        pettyUi.titleLabel.Text = title;
                        pettyUi.Show();
                    }
                    else if (category.Id == (long)BaseAdvanceCategoryEnum.CorporateAdvisory)
                    {
                        AdvanceCorporateAdvisoryExpenseHeader header =
                       (AdvanceCorporateAdvisoryExpenseHeader)_advanceExpenseHeaderManager.GetById(requisition.HeaderId);

                        ExpenseEntryForCorporateAdvisoryUI corporateAdvisoryUi = new ExpenseEntryForCorporateAdvisoryUI(header,
                            AdvancedFormMode.Update);
                        corporateAdvisoryUi.Text = title;
                        corporateAdvisoryUi.titleLabel.Text = title;
                        corporateAdvisoryUi.Show();
                    }
                    else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                    {
                        AdvanceMiscelleaneousExpenseHeader header =
                           (AdvanceMiscelleaneousExpenseHeader)_advanceExpenseHeaderManager.GetById(requisition.HeaderId);

                        ExpenseEntryForMiscellaneousUI miscellaneousUi = new ExpenseEntryForMiscellaneousUI(header,
                            AdvancedFormMode.Update);
                        miscellaneousUi.Text = title;
                        miscellaneousUi.titleLabel.Text = title;
                        miscellaneousUi.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void sent360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView gridView = sentAdvanceExpenseDataGridView;
                ShowExpense360View(gridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void approved360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView gridView = approvedAdvanceExpenseDataGridView;
                ShowExpense360View(gridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rejected360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView gridView = rejectedAdvanceExpenseDataGridView;
                ShowExpense360View(gridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expense360ViewRejectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ListView listView = rejectedAdvanceExpenseListView;
                //ShowExpense360View(listView);
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
                RemoveExpense();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void RemoveExpense()
        //{
        //    if (draftAdvanceExpenseListView.CheckedItems.Count > 0)
        //    {
        //        DialogResult result = MessageBox.Show(@"Are you sure you want to remove all checked expense(s)?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        //        if (result == DialogResult.No)
        //        {
        //            return;
        //        }
        //        ICollection<AdvanceExpenseHeader> expenseHeaders = new List<AdvanceExpenseHeader>();
        //        foreach (ListViewItem checkedItem in draftAdvanceExpenseListView.CheckedItems)
        //        {
        //            var expense = checkedItem.Tag as Advance_VW_GetAdvanceExpense;
        //            if (expense != null)
        //            {
        //                AdvanceExpenseHeader header = _advanceExpenseHeaderManager.GetById(expense.HeaderId);
        //                if (header != null)
        //                {
        //                    expenseHeaders.Add(header);
        //                }
        //            }
        //        }
        //        var isRemoved = _approvalProcessManager.RemoveExpense(expenseHeaders);
        //        if (isRemoved)
        //        {
        //            MessageBox.Show(@"Expense is removed. You can find this expense now in trash tab.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        SearchExpense();
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"Please select a expense.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        private void RemoveExpense()
        {
            int draftChecked = 0;

            foreach (DataGridViewRow row in draftAdvanceExpenseDataGridView.Rows)
            {
                bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                if (isChecked)
                {
                    draftChecked++;
                }
            }
            if (draftChecked > 0)
            {
                DialogResult result = MessageBox.Show(@"Are you sure you want to remove all checked expense(s)?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
                ICollection<AdvanceExpenseHeader> expenseHeaders = new List<AdvanceExpenseHeader>();
                foreach (DataGridViewRow checkedItem in draftAdvanceExpenseDataGridView.Rows)
                {
                    var expense = checkedItem.Tag as Advance_VW_GetAdvanceExpense;
                    if (expense != null)
                    {
                        AdvanceExpenseHeader header = _advanceExpenseHeaderManager.GetById(expense.HeaderId);
                        if (header != null)
                        {
                            expenseHeaders.Add(header);
                        }
                    }
                }
                var isRemoved = _approvalProcessManager.RemoveExpense(expenseHeaders);
                if (isRemoved)
                {
                    MessageBox.Show(@"Expense is removed. You can find this expense now in trash tab.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SearchExpense();
            }
            else
            {
                MessageBox.Show(@"Please select a expense.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //RemoveExpense();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trashed360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView gridView = trashAdvanceExpenseDataGridView;
                ShowExpense360View(gridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expense360ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                //ListView listView = trashAdvanceExpenseListView;
                //ShowExpense360View(listView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trashAdvanceExpenseListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (trashAdvanceExpenseListView.SelectedItems.Count > 0)
                //{
                //    trashed360ViewButton.Visible = true;
                //}
                //else
                //{
                //    trashed360ViewButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void draftAdvanceExpenseListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                // Utility.Utility.SortColumn(e, draftAdvanceExpenseListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sentAdvanceExpenseListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                // Utility.Utility.SortColumn(e, sentAdvanceExpenseListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void approvedAdvanceExpenseListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                // Utility.Utility.SortColumn(e, approvedAdvanceExpenseListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void revertedAdvanceExpenseListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Utility.Utility.SortColumn(e, revertedAdvanceExpenseListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rejectedAdvanceExpenseListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                // Utility.Utility.SortColumn(e, rejectedAdvanceExpenseListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trashAdvanceExpenseListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Utility.Utility.SortColumn(e, trashAdvanceExpenseListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void draftExpenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (draftAdvanceExpenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        draftAdvanceExpenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    ShowExpenseHistory(expense);
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowExpenseHistory(Advance_VW_GetAdvanceExpense expense)
        {
            if (expense != null)
            {
                BaseAdvanceCategory category =
                    _advanceRequisitionCategoryManager.GetById(expense.AdvanceCategoryId).BaseAdvanceCategory;

                if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                {
                    if (expense.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                    {
                        ExpenseHistoryForLocalTravelUI historyUi = new ExpenseHistoryForLocalTravelUI(expense.HeaderId);
                        historyUi.Show();
                    }
                    else if (expense.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                    {
                        ExpenseHistoryForOverseasTravelUI historyUi = new ExpenseHistoryForOverseasTravelUI(expense.HeaderId);
                        historyUi.Show();
                    }
                }
                else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                {
                    ExpenseHistoryForPettyCashUI historyUi = new ExpenseHistoryForPettyCashUI(expense.HeaderId);
                    historyUi.Show();
                }
                else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                {
                    if (expense.AdvanceCategoryId == (long)AdvanceCategoryEnum.CorporateAdvisory)
                    {
                        ExpenseHistoryForCorporateAdvisoryUI historyUi = new ExpenseHistoryForCorporateAdvisoryUI(expense.HeaderId);
                        historyUi.Show();
                    }
                    else
                    {
                        ExpenseHistoryForMiscellaneousUI historyUi = new ExpenseHistoryForMiscellaneousUI(expense.HeaderId);
                        historyUi.Show();
                    }


                }
            }
        }

        private void sentExpenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (sentAdvanceExpenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        sentAdvanceExpenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    ShowExpenseHistory(expense);
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void revertExpenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (revertedAdvanceExpenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        revertedAdvanceExpenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    ShowExpenseHistory(expense);
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void approvedExpenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (approvedAdvanceExpenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        approvedAdvanceExpenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    ShowExpenseHistory(expense);
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rejetcedExpenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (rejectedAdvanceExpenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        rejectedAdvanceExpenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    ShowExpenseHistory(expense);
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trashExpenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (trashAdvanceExpenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        trashAdvanceExpenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    ShowExpenseHistory(expense);
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SentToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)sentAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            sentAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void DraftToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)draftAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            draftAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void ApprovedToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)approvedAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            approvedAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void PaidToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)paidAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            paidAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void ReceivedToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)receivedAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            receivedAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void RevertedToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)revertedAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            revertedAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void RejectedToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)rejectedAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            rejectedAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void RemoveToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)trashAdvanceExpenseDataGridView[column, row].EditedFormattedValue;
            trashAdvanceExpenseDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void draftAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = draftAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (draftAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                draftAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        DataGridView gridView = draftAdvanceExpenseDataGridView;
                        ShowEditExpenseForm(gridView);
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        RemoveExpense();
                    }
                }

                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                    e.RowIndex >= 0)
                {
                    DraftToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sentAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = sentAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (sentAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                sentAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                    e.RowIndex >= 0)
                {
                    SentToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void approvedAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = approvedAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (approvedAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                approvedAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                    e.RowIndex >= 0)
                {
                    ApprovedToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void revertedAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = rejectedAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (revertedAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                revertedAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        DataGridView gridView = revertedAdvanceExpenseDataGridView;
                        ShowEditExpenseForm(gridView);
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                    e.RowIndex >= 0)
                {
                    RevertedToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rejectedAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = rejectedAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (rejectedAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                rejectedAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                    e.RowIndex >= 0)
                {
                    RejectedToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trashAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = trashAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (trashAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                trashAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                    e.RowIndex >= 0)
                {
                    RemoveToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void paidAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = paidAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (paidAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                paidAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        MarkExpenseAsReceived();
                        LoadGridData();
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                         e.RowIndex >= 0)
                {
                    PaidToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MarkExpenseAsReceived()
        {

            if (paidAdvanceExpenseDataGridView.SelectedRows.Count > 0)
            {
                var selectedExpense =
                    paidAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                if (selectedExpense != null)
                {
                    SelectReceiveDateUI selectReceiveDateUi = new SelectReceiveDateUI();
                    selectReceiveDateUi.ShowDialog();
                    if (selectReceiveDateUi.IsReceiveButtonClicked)
                    {
                        DateTime selectedReceiveDate = selectReceiveDateUi.receiveDateMetroDateTime.Value;
                        bool isReceived = _advanceExpenseHeaderManager.ExpensePayReceived(selectedExpense.HeaderId, Session.LoginUserName,selectedReceiveDate);
                        if (isReceived)
                        {
                            MessageBox.Show(@"Receive date insertion successful. Please find it in the 'Received' tab.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(@"Receive date insertion failed.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void receivedAdvanceExpenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataGridView gridView = receivedAdvanceExpenseDataGridView;
                        ShowExpense360View(gridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (receivedAdvanceExpenseDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                receivedAdvanceExpenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            ShowExpenseHistory(expense);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                         e.RowIndex >= 0)
                {
                    ReceivedToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
