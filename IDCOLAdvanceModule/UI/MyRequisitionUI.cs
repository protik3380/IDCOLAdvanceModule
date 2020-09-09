using System.Drawing;
using IDCOLAdvanceModule.BLL;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI.Expense;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.UI.Entry;
using IDCOLAdvanceModule.UI.History.RequisitionHistory;
using IDCOLAdvanceModule.UI._360View;

namespace IDCOLAdvanceModule.UI
{
    public partial class MyRequisitionUI : Form
    {
        private readonly string _userName = Session.LoginUserName;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IRequisitionApprovalTicketManager _requisitionApprovalTicketManager;
        private readonly IRequisitionApprovalTrackerManager _requisitionApprovalTrackerManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;

        public MyRequisitionUI()
        {
            _requisitionApprovalTrackerManager = new RequisitionApprovalTrackerManager();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _requisitionApprovalTicketManager = new RequisitionApprovalTicketManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _approvalProcessManager = new ApprovalProcessManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            InitializeComponent();
            InitializeColumnSorter();
            requisitionsTabControl.SelectedTab = draftTabPage;
        }

        private void InitializeColumnSorter()
        {
            //Utility.ListViewColumnSorter draftColumnSorter = new Utility.ListViewColumnSorter();
            //draftAdvanceRequisitionListView.ListViewItemSorter = draftColumnSorter;
            //Utility.ListViewColumnSorter sentColumnSorter = new Utility.ListViewColumnSorter();
            //sentAdvanceRequisitionListView.ListViewItemSorter = sentColumnSorter;
            //Utility.ListViewColumnSorter approvedColumnSorter = new Utility.ListViewColumnSorter();
            //approvedAdvanceRequisitionListView.ListViewItemSorter = approvedColumnSorter;
            //Utility.ListViewColumnSorter revertedColumnSorter = new Utility.ListViewColumnSorter();
            //revertedAdvanceRequisitionListView.ListViewItemSorter = revertedColumnSorter;
            //Utility.ListViewColumnSorter rejectedColumnSorter = new Utility.ListViewColumnSorter();
            //rejectedAdvanceRequisitionListView.ListViewItemSorter = rejectedColumnSorter;
            //Utility.ListViewColumnSorter trashColumnSorter = new Utility.ListViewColumnSorter();
            //trashAdvanceRequisitionListView.ListViewItemSorter = trashColumnSorter;
        }

        private void MyRequisitionUI_Load(object sender, EventArgs e)
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
            ICollection<Advance_VW_GetAdvanceRequisition> draftRequisitions =
                    _approvalProcessManager.GetDraftRequisitionsForMember(_userName);
            LoadDraftAdvanceRequisitionGridView(draftRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> sentRequisitions =
                _approvalProcessManager.GetSentRequisitionsForMember(_userName);
            LoadSentAdvanceRequisitionGridView(sentRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> approvedRequisitions =
                _approvalProcessManager.GetApprovedRequisitionsForMember(_userName);
            LoadApprovedAdvanceRequisitionGridView(approvedRequisitions);

            ICollection<Advance_VW_GetAdvanceRequisition> paidRequisitions =
                _approvalProcessManager.GetPaidRequisitionsForMember(_userName);
            LoadPaidAdvanceRequisitionGridView(paidRequisitions);

            ICollection<Advance_VW_GetAdvanceRequisition> receivedRequisitions =
                    _approvalProcessManager.GetReceivedRequisitionsForMember(_userName);
            LoadReceivedAdvanceRequisitionGridView(receivedRequisitions);

            ICollection<Advance_VW_GetAdvanceRequisition> revertedRequisitions =
                _approvalProcessManager.GetRevertedRequisitionsForMember(_userName);
            LoadRevertedAdvanceRequisitionGridView(revertedRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> rejectedRequisitions =
                _approvalProcessManager.GetRejectedRequisitionsForMember(_userName);
            LoadRejectedAdvanceRequisitionGridView(rejectedRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> removedRequisitions =
                _approvalProcessManager.GetRemovedRequisitionsForMember(_userName);
            LoadRemovedAdvanceRequisitionGridView(removedRequisitions);
        }

        //private void LoadRemovedAdvanceRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> removedRequisitions)
        //{
        //    trashAdvanceRequisitionListView.Items.Clear();
        //    if (removedRequisitions != null && removedRequisitions.Any())
        //    {
        //        removedRequisitions = removedRequisitions.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in removedRequisitions)
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
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.Value.ToString("N"));

        //            item.Tag = criteriaVm;
        //            trashAdvanceRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //        trashTabPage.Text = @"Trash (" + removedRequisitions.Count + @")";
        //    }
        //    else
        //    {
        //        trashTabPage.Text = @"Trash (0)";
        //    }
        //}

        private void LoadRemovedAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> removedRequisitions)
        {
            trashAdvanceRequisitionDataGridView.Rows.Clear();
            if (removedRequisitions != null && removedRequisitions.Any())
            {
                removedRequisitions = removedRequisitions.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in removedRequisitions)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(trashAdvanceRequisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.RequisitionNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[6].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    trashAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;
                }
                trashTabPage.Text = @"Trash (" + removedRequisitions.Count + @")";
            }
            else
            {
                trashTabPage.Text = @"Trash (0)";
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                SearchRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchRequisition()
        {
            EmployeeRequisitionSearchCriteria criteria = new EmployeeRequisitionSearchCriteria();
            criteria.IsRequisitionForLoggedInUser = ownRadioButton.Checked;
            if (Convert.ToInt64(advanceRequisitionCategoryComboBox.SelectedValue.ToString()) != DefaultItem.Value)
            {
                criteria.AdvanceCategoryId = Convert.ToInt32(advanceRequisitionCategoryComboBox.SelectedValue.ToString());
            }
            criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?)null;
            criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?)null;
            ICollection<Advance_VW_GetAdvanceRequisition> draftRequisitions = _approvalProcessManager.GetDraftRequisitionsForMember(_userName, criteria);
            LoadDraftAdvanceRequisitionGridView(draftRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> sentRequisitions = _approvalProcessManager.GetSentRequisitionsForMember(_userName, criteria);
            LoadSentAdvanceRequisitionGridView(sentRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> approvedRequisitions = _approvalProcessManager.GetApprovedRequisitionsForMember(_userName, criteria);
            LoadApprovedAdvanceRequisitionGridView(approvedRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> paidRequisitions =
                _approvalProcessManager.GetPaidRequisitionsForMember(_userName, criteria);
            LoadPaidAdvanceRequisitionGridView(paidRequisitions);

            ICollection<Advance_VW_GetAdvanceRequisition> receivedRequisitions =
                _approvalProcessManager.GetReceivedRequisitionsForMember(_userName, criteria);
            LoadReceivedAdvanceRequisitionGridView(receivedRequisitions);

            ICollection<Advance_VW_GetAdvanceRequisition> revertedRequisitions = _approvalProcessManager.GetRevertedRequisitionsForMember(_userName, criteria);
            LoadRevertedAdvanceRequisitionGridView(revertedRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> rejectedRequisitions = _approvalProcessManager.GetRejectedRequisitionsForMember(_userName, criteria);
            LoadRejectedAdvanceRequisitionGridView(rejectedRequisitions);
            ICollection<Advance_VW_GetAdvanceRequisition> removedRequisitions = _approvalProcessManager.GetRemovedRequisitions(_userName, criteria);
            LoadRemovedAdvanceRequisitionGridView(removedRequisitions);
        }

        //private void LoadRevertedAdvanceRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> revertedRequisitions)
        //{
        //    revertedAdvanceRequisitionListView.Items.Clear();
        //    if (revertedRequisitions != null && revertedRequisitions.Any())
        //    {
        //        revertedRequisitions = revertedRequisitions.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in revertedRequisitions)
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
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.Value.ToString("N"));

        //            item.Tag = criteriaVm;
        //            revertedAdvanceRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //        revertedTabPage.Text = @"Reverted (" + revertedRequisitions.Count + @")";
        //    }
        //    else
        //    {
        //        revertedTabPage.Text = @"Reverted trash(0)";
        //    }
        //}
        private void LoadRevertedAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> revertedRequisitions)
        {
            revertAdvanceRequisitionDataGridView.Rows.Clear();
            if (revertedRequisitions != null && revertedRequisitions.Any())
            {
                revertedRequisitions = revertedRequisitions.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in revertedRequisitions)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(revertAdvanceRequisitionDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = "Edit";
                    row.Cells[3].Value = false;
                    row.Cells[4].Value = serial.ToString();
                    row.Cells[5].Value = criteriaVm.RequisitionNo;
                    row.Cells[6].Value = criteriaVm.EmployeeName;
                    row.Cells[7].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[8].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[11].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    revertAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;
                }
                revertedTabPage.Text = @"Reverted (" + revertedRequisitions.Count + @")";
            }
            else
            {
                revertedTabPage.Text = @"Reverted trash(0)";
            }
        }

        //private void LoadDraftAdvanceRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    draftAdvanceRequisitionListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
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
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.Value.ToString("N"));

        //            item.Tag = criteriaVm;
        //            draftAdvanceRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //        draftTabPage.Text = @"Draft (" + requisitionList.Count + @")";
        //    }
        //    else
        //    {
        //        draftTabPage.Text = @"Draft (0)";
        //    }
        //}

        private void LoadDraftAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            draftAdvanceRequisitionDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(draftAdvanceRequisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = "Edit";
                    row.Cells[3].Value = "Remove";
                    row.Cells[4].Value = false;
                    row.Cells[5].Value = serial.ToString();
                    row.Cells[6].Value = criteriaVm.RequisitionNo;
                    row.Cells[7].Value = criteriaVm.EmployeeName;
                    row.Cells[8].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[9].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[11].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[12].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    draftAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;
                }
                draftTabPage.Text = @"Draft (" + requisitionList.Count + @")";
            }
            else
            {
                draftTabPage.Text = @"Draft (0)";
            }
        }

        //private void LoadSentAdvanceRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    sentAdvanceRequisitionListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
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
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.Value.ToString("N"));

        //            item.Tag = criteriaVm;
        //            sentAdvanceRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //        sentTabPage.Text = @"Sent (" + requisitionList.Count + @")";
        //    }
        //    else
        //    {
        //        sentTabPage.Text = @"Sent (0)";
        //    }
        //}

        private void LoadSentAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            sentAdvanceRequisitionDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(sentAdvanceRequisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.RequisitionNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[6].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    sentAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;
                }
                sentTabPage.Text = @"Sent (" + requisitionList.Count + @")";
            }
            else
            {
                sentTabPage.Text = @"Sent (0)";
            }
        }

        //private void LoadApprovedAdvanceRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    approvedAdvanceRequisitionListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
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
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.Value.ToString("N"));

        //            item.Tag = criteriaVm;
        //            approvedAdvanceRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //        approvedTabPage.Text = @"Approved (" + requisitionList.Count + @")";
        //    }
        //    else
        //    {
        //        approvedTabPage.Text = @"Approved (0)";
        //    }
        //}


        private void LoadApprovedAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            approvedAdvanceRequisitionDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(approvedAdvanceRequisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    //row.Cells[2].Value = "Expense Entry";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.RequisitionNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[6].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    approvedAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;

                }
                approvedTabPage.Text = @"Approved (" + requisitionList.Count + @")";
            }
            else
            {
                approvedTabPage.Text = @"Approved (0)";
            }
        }

        private void LoadReceivedAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            receivedAdvanceRequisitionDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(receivedAdvanceRequisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = "Expense Entry";
                    row.Cells[3].Value = serial.ToString();
                    row.Cells[4].Value = criteriaVm.RequisitionNo;
                    row.Cells[5].Value = criteriaVm.EmployeeName;
                    row.Cells[6].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[7].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    receivedAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;

                }
                receivedTabPage.Text = @"Received (" + requisitionList.Count + @")";
            }
            else
            {
                receivedTabPage.Text = @"Received (0)";
            }
        }

        private void LoadPaidAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            paidAdvanceRequisitionDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(paidAdvanceRequisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = "Received";
                    row.Cells[3].Value = serial.ToString();
                    row.Cells[4].Value = criteriaVm.RequisitionNo;
                    row.Cells[5].Value = criteriaVm.EmployeeName;
                    row.Cells[6].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[7].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    paidAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;

                }
                paidTabPage.Text = @"Paid (" + requisitionList.Count + @")";
            }
            else
            {
                paidTabPage.Text = @"Paid (0)";
            }
        }
        //private void LoadRejectedAdvanceRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    rejectedAdvanceRequisitionListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
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
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.Value.ToString("N"));

        //            item.Tag = criteriaVm;
        //            rejectedAdvanceRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //        rejectedTabPage.Text = @"Rejected (" + requisitionList.Count + @")";
        //    }
        //    else
        //    {
        //        rejectedTabPage.Text = @"Rejected (0)";
        //    }
        //}

        private void LoadRejectedAdvanceRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            rejectedAdvanceRequisitionDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitionList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(rejectedAdvanceRequisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "History";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = criteriaVm.RequisitionNo;
                    row.Cells[4].Value = criteriaVm.EmployeeName;
                    row.Cells[5].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[6].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = (criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A");
                    row.Cells[9].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                    row.Tag = criteriaVm;
                    rejectedAdvanceRequisitionDataGridView.Rows.Add(row);
                    serial++;
                }
                rejectedTabPage.Text = @"Rejected (" + requisitionList.Count + @")";
            }
            else
            {
                rejectedTabPage.Text = @"Rejected (0)";
            }
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

        private void sendApprovalButton_Click(object sender, EventArgs e)
        {
            try
            {
                int checkedRequisition = 0;
                foreach (DataGridViewRow row in draftAdvanceRequisitionDataGridView.Rows)
                {
                    bool isChecked = (bool)row.Cells[4].EditedFormattedValue;

                    if (isChecked)
                    {
                        checkedRequisition++;
                    }
                }
                if (checkedRequisition > 0)
                {
                    DialogResult result = MessageBox.Show(@"Are you sure you want to send all checked requisitions for approval?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    ICollection<AdvanceRequisitionHeader> requisitionHeaders = new List<AdvanceRequisitionHeader>();
                    //foreach (ListViewItem checkedItem in draftAdvanceRequisitionListView.CheckedItems)
                    //{
                    //    var requisition = checkedItem.Tag as Advance_VW_GetAdvanceRequisition;
                    //    if (requisition != null)
                    //    {
                    //        AdvanceRequisitionHeader header = _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                    //        if (header != null)
                    //        {
                    //            requisitionHeaders.Add(header);
                    //        }
                    //    }
                    //}

                    foreach (DataGridViewRow checkedItem in draftAdvanceRequisitionDataGridView.Rows)
                    {
                        if ((bool)checkedItem.Cells[4].EditedFormattedValue)
                        {
                            var requisition = checkedItem.Tag as Advance_VW_GetAdvanceRequisition;
                            if (requisition != null)
                            {
                                AdvanceRequisitionHeader header = _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                                if (header != null)
                                {
                                    requisitionHeaders.Add(header);
                                }
                            }
                        }

                    }
                    var ticket = _approvalProcessManager.SendRequisitionForApproval(requisitionHeaders, _userName);
                    if (ticket != null && ticket.Any())
                    {
                        MessageBox.Show(@"Requisition is sent for approval. You can find this requisition now in Sent tab.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    SearchRequisition();
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

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (draftAdvanceRequisitionDataGridView.Rows.Count > 0)
                {
                    if (selectAllButton.Text.Equals("Select All"))
                    {
                        selectAllButton.Text = @"Deselect All";
                        Utility.Utility.SelectOrUnSelectAllGridItem(draftAdvanceRequisitionDataGridView, true);
                    }
                    else
                    {
                        selectAllButton.Text = @"Select All";
                        Utility.Utility.SelectOrUnSelectAllGridItem(draftAdvanceRequisitionDataGridView, false);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void selectAllInRevertmetroButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (revertedAdvanceRequisitionListView.Items.Count > 0)
        //        {
        //            if (selectAllInRevertmetroButton.Text.Equals("Select All"))
        //            {
        //                selectAllInRevertmetroButton.Text = @"Unselect All";
        //                Utility.Utility.SellectOrUnSelectAllListItem(revertedAdvanceRequisitionListView, true);
        //            }
        //            else
        //            {
        //                selectAllInRevertmetroButton.Text = @"Select All";
        //                Utility.Utility.SellectOrUnSelectAllListItem(revertedAdvanceRequisitionListView, false);
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void selectAllInRevertmetroButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (revertAdvanceRequisitionDataGridView.Rows.Count > 0)
                {
                    if (selectAllInRevertmetroButton.Text.Equals("Select All"))
                    {
                        selectAllInRevertmetroButton.Text = @"Unselect All";
                        RevertSelectOrUnSelectAllGridItem(rejectedAdvanceRequisitionDataGridView, true);
                    }
                    else
                    {
                        selectAllInRevertmetroButton.Text = @"Select All";
                        RevertSelectOrUnSelectAllGridItem(revertAdvanceRequisitionDataGridView, false);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RevertSelectOrUnSelectAllGridItem(DataGridView gridView, bool isChecked)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.Cells[3].Value = isChecked;
            }

        }
        private void SendForApprovalInRevertButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (revertedAdvanceRequisitionListView.CheckedItems.Count > 0)
                //{
                //    DialogResult result = MessageBox.Show(@"Are you sure you want to send all checked requisitions for approval?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //    if (result == DialogResult.No)
                //    {
                //        return;
                //    }
                //    ICollection<AdvanceRequisitionHeader> requisitionHeaders = new List<AdvanceRequisitionHeader>();
                //    foreach (ListViewItem checkedItem in revertedAdvanceRequisitionListView.CheckedItems)
                //    {
                //        var requisition = checkedItem.Tag as Advance_VW_GetAdvanceRequisition;
                //        if (requisition != null)
                //        {
                //            AdvanceRequisitionHeader header = _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                //            if (header != null)
                //            {
                //                requisitionHeaders.Add(header);
                //            }
                //        }
                //    }
                //    _approvalProcessManager.SendRevertedRequisitionsForApproval(requisitionHeaders, _userName);
                //    SearchRequisition();
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select a requisition.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                int checkedRequisition = 0;
                foreach (DataGridViewRow row in revertAdvanceRequisitionDataGridView.Rows)
                {
                    bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                    if (isChecked)
                    {
                        checkedRequisition++;
                    }
                }

                if (checkedRequisition > 0)
                {
                    DialogResult result = MessageBox.Show(@"Are you sure you want to send all checked requisitions for approval?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    ICollection<AdvanceRequisitionHeader> requisitionHeaders = new List<AdvanceRequisitionHeader>();
                    foreach (DataGridViewRow checkedItem in revertAdvanceRequisitionDataGridView.Rows)
                    {
                        if ((bool)checkedItem.Cells[3].EditedFormattedValue)
                        {
                            var requisition = checkedItem.Tag as Advance_VW_GetAdvanceRequisition;
                            if (requisition != null)
                            {
                                AdvanceRequisitionHeader header = _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                                if (header != null)
                                {
                                    requisitionHeaders.Add(header);
                                }
                            }
                        }

                    }
                    _approvalProcessManager.SendRevertedRequisitionsForApproval(requisitionHeaders, _userName);
                    SearchRequisition();
                }
                else
                {
                    MessageBox.Show(@"Please select a requisition.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void ShowRequisition360View(ListView listView)
        //{
        //    if (listView != null && listView.SelectedItems.Count > 0)
        //    {
        //        var selectedItem = listView.SelectedItems[0];

        //        var requisition = selectedItem.Tag as Advance_VW_GetAdvanceRequisition;
        //        if (requisition == null)
        //        {
        //            throw new Exception("Selected requisition has no data");
        //        }

        //        Requisition360ViewUI expense360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
        //        expense360ViewUi.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //    }
        //}

        //private void ShowRequisition360View(DataGridView gridView)
        //{
        //    if (gridView != null && gridView.SelectedRows.Count > 0)
        //    {
        //        var selectedItem = gridView.SelectedRows[0];

        //        var requisition = selectedItem.Tag as Advance_VW_GetAdvanceRequisition;
        //        if (requisition == null)
        //        {
        //            throw new Exception("Selected requisition has no data");
        //        }

        //        Requisition360ViewUI expense360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
        //        expense360ViewUi.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //    }
        //}

        private void ShowRequisition360View(DataGridView gridView)
        {
            if (gridView != null && gridView.SelectedRows.Count > 0)
            {
                var selectedItem = gridView.SelectedRows[0];

                var requisition = selectedItem.Tag as Advance_VW_GetAdvanceRequisition;
                if (requisition == null)
                {
                    throw new Exception("Selected requisition has no data");
                }

                Requisition360ViewUI expense360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
                expense360ViewUi.Show();
            }
            else
            {
                MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void view360SentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowRequisition360View(sentAdvanceRequisitionDataGridView);
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
                //if (draftAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        draftAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    AdvanceCategory advanceCategory =
                //        _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId);

                //    string title = "Advance Cash Requisition Entry Form (" + advanceCategory.Name + ")";
                //    if (requisition != null)
                //    {
                //        BaseAdvanceCategory category =
                //            _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                //        if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                //        {
                //            if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                //            {
                //                AdvanceTravelRequisitionHeader header =
                //                    (AdvanceTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionEntryForLocalTravelUI localTravelUi = new RequisitionEntryForLocalTravelUI(header, AdvancedFormMode.Update);
                //                localTravelUi.Text = title;
                //                localTravelUi.titleLabel.Text = title;
                //                localTravelUi.Show();
                //            }
                //            else if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                //            {
                //                AdvanceOverseasTravelRequisitionHeader header =
                //                    (AdvanceOverseasTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionEntryForOverseasTravelUI travelUi = new RequisitionEntryForOverseasTravelUI(header, AdvancedFormMode.Update);
                //                travelUi.Text = title;
                //                travelUi.titleLabel.Text = title;
                //                travelUi.Show();
                //            }
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                //        {
                //            AdvancePettyCashRequisitionHeader header =
                //            (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForPettyUI pettyUi = new RequisitionEntryForPettyUI(header, AdvancedFormMode.Update);
                //            pettyUi.Text = title;
                //            pettyUi.titleLabel.Text = title;
                //            pettyUi.Show();
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.CorporateAdvisory)
                //        {
                //            AdvanceCorporateAdvisoryRequisitionHeader header =
                //                (AdvanceCorporateAdvisoryRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForCorporateAdvisoryUI corporateAdvisoryUi = new RequisitionEntryForCorporateAdvisoryUI(header, AdvancedFormMode.Update);
                //            corporateAdvisoryUi.Text = title;
                //            corporateAdvisoryUi.titleLabel.Text = title;
                //            corporateAdvisoryUi.Show();
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                //        {
                //            AdvanceMiscelleneousRequisitionHeader header =
                //                (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForMiscellaneousUI miscellaneousUi = new RequisitionEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
                //            miscellaneousUi.Text = title;
                //            miscellaneousUi.titleLabel.Text = title;
                //            miscellaneousUi.Show();
                //        }
                //    }
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

        private void view360DraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowRequisition360View(draftAdvanceRequisitionDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editRevertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (revertedAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        revertedAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    AdvanceCategory advanceCategory =
                //        _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId);

                //    string title = " Advance Cash Requisition Entry Form (" + advanceCategory.Name + ")";
                //    if (requisition != null)
                //    {
                //        BaseAdvanceCategory category =
                //            _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                //        if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                //        {
                //            if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                //            {
                //                AdvanceTravelRequisitionHeader header =
                //                    (AdvanceTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionEntryForLocalTravelUI localTravelUi = new RequisitionEntryForLocalTravelUI(header, AdvancedFormMode.Update);
                //                localTravelUi.Text = title;
                //                localTravelUi.titleLabel.Text = title;
                //                localTravelUi.Show();
                //            }
                //            else if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                //            {
                //                AdvanceOverseasTravelRequisitionHeader header =
                //                    (AdvanceOverseasTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionEntryForOverseasTravelUI localTravelUi = new RequisitionEntryForOverseasTravelUI(header, AdvancedFormMode.Update);
                //                localTravelUi.Text = title;
                //                localTravelUi.titleLabel.Text = title;
                //                localTravelUi.Show();
                //            }
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                //        {
                //            AdvancePettyCashRequisitionHeader header =
                //            (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForPettyUI pettyUi = new RequisitionEntryForPettyUI(header, AdvancedFormMode.Update);
                //            pettyUi.Text = title;
                //            pettyUi.titleLabel.Text = title;
                //            pettyUi.Show();
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.CorporateAdvisory)
                //        {
                //            AdvanceCorporateAdvisoryRequisitionHeader header =
                //                (AdvanceCorporateAdvisoryRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForCorporateAdvisoryUI corporateAdvisoryUi = new RequisitionEntryForCorporateAdvisoryUI(header, AdvancedFormMode.Update);
                //            corporateAdvisoryUi.Text = title;
                //            corporateAdvisoryUi.titleLabel.Text = title;
                //            corporateAdvisoryUi.Show();
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                //        {
                //            AdvanceMiscelleneousRequisitionHeader header =
                //            (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForMiscellaneousUI miscellaneousUi = new RequisitionEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
                //            miscellaneousUi.Text = title;
                //            miscellaneousUi.titleLabel.Text = title;
                //            miscellaneousUi.Show();
                //        }
                //    }
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

        private void view360RevertedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //  ShowRequisition360View(revertedAdvanceRequisitionListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void view360ApprovedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ShowRequisition360View(approvedAdvanceRequisitionListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void approvedAdvanceRequisitionListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (approvedAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    approved360ViewButton.Visible = true;
                //    expenseEntryButton.Visible = true;
                //}
                //else
                //{
                //    approved360ViewButton.Visible = false;
                //    expenseEntryButton.Visible = false;
                //}
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
                ShowRequisition360View(approvedAdvanceRequisitionDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                LoadCategoryWiseExpenseEntryUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryWiseExpenseEntryUi()
        {
            //ListViewItem item = approvedAdvanceRequisitionListView.SelectedItems[0];

            DataGridViewRow row = receivedAdvanceRequisitionDataGridView.SelectedRows[0];
            Advance_VW_GetAdvanceRequisition requisition = row.Tag as Advance_VW_GetAdvanceRequisition;
            if (requisition != null)
            {
                if (_advanceExpenseHeaderManager.IsExpenseAlreadyEntryForRequisition(requisition.HeaderId))
                {
                    throw new UiException("Expense entry already done for this requisition.");
                }
                AdvanceCategory advanceCategory =
                    _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId);
                string title = "Expense Entry Form (" + advanceCategory.Name + ")";
                BaseAdvanceCategory category =
                    _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                {
                    if ((long)AdvanceCategoryEnum.LocalTravel == requisition.AdvanceCategoryId)
                    {
                        AdvanceTravelRequisitionHeader header =
                            (AdvanceTravelRequisitionHeader)
                            _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        ICollection<AdvanceTravelRequisitionHeader> requisitionHeaders = new List<AdvanceTravelRequisitionHeader> { header };
                        ExpenseEntryForLocalTravelUI expenseEntryForLocalTravelUi =
                            new ExpenseEntryForLocalTravelUI(requisitionHeaders);
                        expenseEntryForLocalTravelUi.Text = title;
                        expenseEntryForLocalTravelUi.titleLabel.Text = title;
                        expenseEntryForLocalTravelUi.Show();
                    }
                    else if ((long)AdvanceCategoryEnum.OversearTravel == requisition.AdvanceCategoryId)
                    {
                        AdvanceOverseasTravelRequisitionHeader header =
                            (AdvanceOverseasTravelRequisitionHeader)
                            _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        ICollection<AdvanceOverseasTravelRequisitionHeader> requisitionHeaders = new List<AdvanceOverseasTravelRequisitionHeader> { header };
                        ExpenseEntryForOverseasTravelUI expenseEntryForOverseasTravelUi =
                            new ExpenseEntryForOverseasTravelUI(requisitionHeaders);
                        expenseEntryForOverseasTravelUi.Text = title;
                        expenseEntryForOverseasTravelUi.titleLabel.Text = title;
                        expenseEntryForOverseasTravelUi.Show();
                    }
                }
                else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                {
                    AdvancePettyCashRequisitionHeader header =
                        (AdvancePettyCashRequisitionHeader)
                        _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                    ICollection<AdvancePettyCashRequisitionHeader> requisitionHeaders = new List<AdvancePettyCashRequisitionHeader> { header };
                    ExpenseEntryForPettyCashUI expenseEntryForPettyCashUi =
                        new ExpenseEntryForPettyCashUI(requisitionHeaders);
                    expenseEntryForPettyCashUi.Text = title;
                    expenseEntryForPettyCashUi.titleLabel.Text = title;
                    expenseEntryForPettyCashUi.Show();
                }
                else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                {
                    if ((long)AdvanceCategoryEnum.CorporateAdvisory == requisition.AdvanceCategoryId)
                    {
                        AdvanceCorporateAdvisoryRequisitionHeader header =
                        (AdvanceCorporateAdvisoryRequisitionHeader)
                        _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        ICollection<AdvanceCorporateAdvisoryRequisitionHeader> requisitionHeaders = new List<AdvanceCorporateAdvisoryRequisitionHeader> { header };
                        ExpenseEntryForCorporateAdvisoryUI expenseEntryForMiscellaneousUi =
                            new ExpenseEntryForCorporateAdvisoryUI(requisitionHeaders);
                        expenseEntryForMiscellaneousUi.Text = title;
                        expenseEntryForMiscellaneousUi.titleLabel.Text = title;
                        expenseEntryForMiscellaneousUi.Show();
                    }
                    else
                    {
                        AdvanceMiscelleneousRequisitionHeader header =
                         (AdvanceMiscelleneousRequisitionHeader)
                         _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        ICollection<AdvanceMiscelleneousRequisitionHeader> requisitionHeaders = new List<AdvanceMiscelleneousRequisitionHeader> { header };
                        ExpenseEntryForMiscellaneousUI expenseEntryForMiscellaneousUi =
                            new ExpenseEntryForMiscellaneousUI(requisitionHeaders);
                        expenseEntryForMiscellaneousUi.Text = title;
                        expenseEntryForMiscellaneousUi.titleLabel.Text = title;
                        expenseEntryForMiscellaneousUi.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Error occured.", @"Error!", MessageBoxButtons.OK);
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sent360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                ShowRequisition360View(sentAdvanceRequisitionDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sentAdvanceRequisitionListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (sentAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    sent360ViewButton.Visible = true;
                //}
                //else
                //{
                //    sent360ViewButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rejectedAdvanceRequisitionListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (rejectedAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    rejected360ViewButton.Visible = true;
                //}
                //else
                //{
                //    rejected360ViewButton.Visible = false;
                //}
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
                ShowRequisition360View(rejectedAdvanceRequisitionDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowRequisitionHistory(Advance_VW_GetAdvanceRequisition requisition)
        {
            if (requisition != null)
            {
                BaseAdvanceCategory category =
                    _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                {
                    if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                    {
                        AdvanceTravelRequisitionHeader header =
                            (AdvanceTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        RequisitionHistoryForLocalTravelUI requisitionHistoryForLocalTravelUi = new RequisitionHistoryForLocalTravelUI(header);
                        requisitionHistoryForLocalTravelUi.Show();
                    }
                    else if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                    {
                        AdvanceOverseasTravelRequisitionHeader header =
                            (AdvanceOverseasTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        RequisitionHistoryForOverseasTravelUI historyUi = new RequisitionHistoryForOverseasTravelUI(header);
                        historyUi.Show();
                    }
                }
                else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                {
                    AdvancePettyCashRequisitionHeader header =
                        (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                    RequisitionHistoryForPettyCashUI historyUi = new RequisitionHistoryForPettyCashUI(header);
                    historyUi.Show();
                }
                else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                {
                    if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.CorporateAdvisory)
                    {
                        AdvanceCorporateAdvisoryRequisitionHeader header =
                            (AdvanceCorporateAdvisoryRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        RequisitionHistoryForCorporateAdvisoryUI historyUi = new RequisitionHistoryForCorporateAdvisoryUI(header);
                        historyUi.Show();
                    }
                    else
                    {
                        AdvanceMiscelleneousRequisitionHeader header =
                            (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        RequisitionHistoryForMiscellaneousUI historyUi = new RequisitionHistoryForMiscellaneousUI(header);
                        historyUi.Show();
                    }
                }
            }
        }

        private void requisitionHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (draftAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        draftAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    ShowRequisitionHistory(requisition);
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

        private void requisitionHistorySentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (sentAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        sentAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    ShowRequisitionHistory(requisition);
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

        private void requisitionHistoryRevertedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (revertedAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        revertedAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    ShowRequisitionHistory(requisition);
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

        private void requisitionHistoryApprovedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (approvedAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        approvedAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    ShowRequisitionHistory(requisition);
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

        private void requisitionHistoryRejectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (rejectedAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        rejectedAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;

                //    ShowRequisitionHistory(requisition);
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

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    RemoveRequisition();
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        //private void RemoveRequisition()
        //{
        //    if (draftAdvanceRequisitionListView.CheckedItems.Count > 0)
        //    {
        //        DialogResult result = MessageBox.Show(@"Are you sure you want to remove all checked requisition for approval?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        //        if (result == DialogResult.No)
        //        {
        //            return;
        //        }
        //        ICollection<AdvanceRequisitionHeader> requisitionHeaders = new List<AdvanceRequisitionHeader>();
        //        foreach (ListViewItem checkedItem in draftAdvanceRequisitionListView.CheckedItems)
        //        {
        //            var requisition = checkedItem.Tag as Advance_VW_GetAdvanceRequisition;
        //            if (requisition != null)
        //            {
        //                AdvanceRequisitionHeader header = _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
        //                if (header != null)
        //                {
        //                    requisitionHeaders.Add(header);
        //                }
        //            }
        //        }
        //        var isRemoved = _approvalProcessManager.RemoveRequisition(requisitionHeaders);
        //        if (isRemoved)
        //        {
        //            MessageBox.Show(@"Requisition is removed. You can find this requisition now in Trash tab", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        SearchRequisition();
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        private void RemoveRequisition()
        {
            int checkedRequisition = 0;
            foreach (DataGridViewRow row in draftAdvanceRequisitionDataGridView.Rows)
            {
                bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                if (isChecked)
                {
                    checkedRequisition++;
                }
            }
            if (checkedRequisition > 0)
            {
                DialogResult result = MessageBox.Show(@"Are you sure you want to remove all checked requisition for approval?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
                ICollection<AdvanceRequisitionHeader> requisitionHeaders = new List<AdvanceRequisitionHeader>();
                foreach (DataGridViewRow checkedItem in draftAdvanceRequisitionDataGridView.Rows)
                {
                    var requisition = checkedItem.Tag as Advance_VW_GetAdvanceRequisition;
                    if (requisition != null)
                    {
                        AdvanceRequisitionHeader header = _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);
                        if (header != null)
                        {
                            requisitionHeaders.Add(header);
                        }
                    }
                }
                var isRemoved = _approvalProcessManager.RemoveRequisition(requisitionHeaders);
                if (isRemoved)
                {
                    MessageBox.Show(@"Requisition is removed. You can find this requisition now in Trash tab", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SearchRequisition();
            }
            else
            {
                MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void requisitionHistoryRemovedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (trashAdvanceRequisitionListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        trashAdvanceRequisitionListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;

                //    ShowRequisitionHistory(requisition);
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

        private void requisition360ViewRemovedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ShowRequisition360View(trashAdvanceRequisitionListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void view360RejectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ShowRequisition360View(rejectedAdvanceRequisitionListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void draftAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(draftAdvanceRequisitionDataGridView);
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        if (draftAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                draftAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            ShowRequisitionHistory(requisition);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }

                    else if (e.ColumnIndex == 2)
                    {
                        if (draftAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                draftAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            AdvanceCategory advanceCategory =
                                _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId);

                            string title = "Advance Cash Requisition Entry Form (" + advanceCategory.Name + ")";
                            if (requisition != null)
                            {
                                BaseAdvanceCategory category =
                                    _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                                if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                                {
                                    if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                                    {
                                        AdvanceTravelRequisitionHeader header =
                                            (AdvanceTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                        RequisitionEntryForLocalTravelUI localTravelUi = new RequisitionEntryForLocalTravelUI(header, AdvancedFormMode.Update);
                                        localTravelUi.Text = title;
                                        localTravelUi.titleLabel.Text = title;
                                        localTravelUi.Show();
                                    }
                                    else if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                                    {
                                        AdvanceOverseasTravelRequisitionHeader header =
                                            (AdvanceOverseasTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                        RequisitionEntryForOverseasTravelUI travelUi = new RequisitionEntryForOverseasTravelUI(header, AdvancedFormMode.Update);
                                        travelUi.Text = title;
                                        travelUi.titleLabel.Text = title;
                                        travelUi.Show();
                                    }
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                                {
                                    AdvancePettyCashRequisitionHeader header =
                                    (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForPettyUI pettyUi = new RequisitionEntryForPettyUI(header, AdvancedFormMode.Update);
                                    pettyUi.Text = title;
                                    pettyUi.titleLabel.Text = title;
                                    pettyUi.Show();
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.CorporateAdvisory)
                                {
                                    AdvanceCorporateAdvisoryRequisitionHeader header =
                                        (AdvanceCorporateAdvisoryRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForCorporateAdvisoryUI corporateAdvisoryUi = new RequisitionEntryForCorporateAdvisoryUI(header, AdvancedFormMode.Update);
                                    corporateAdvisoryUi.Text = title;
                                    corporateAdvisoryUi.titleLabel.Text = title;
                                    corporateAdvisoryUi.Show();
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                                {
                                    AdvanceMiscelleneousRequisitionHeader header =
                                        (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForMiscellaneousUI miscellaneousUi = new RequisitionEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
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
                    else if (e.ColumnIndex == 3)
                    {
                        RemoveRequisition();
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

        private void DraftToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)draftAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            draftAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void sentAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(sentAdvanceRequisitionDataGridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (sentAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                sentAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            ShowRequisitionHistory(requisition);
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
        private void SentToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)sentAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            sentAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void approvedAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(approvedAdvanceRequisitionDataGridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {

                        if (approvedAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                approvedAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            ShowRequisitionHistory(requisition);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    //else if (e.ColumnIndex == 2)
                    //{
                    //    LoadCategoryWiseExpenseEntryUi();
                    //}
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

        private void ApprovedToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)approvedAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            approvedAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void PaidToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)paidAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            paidAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void ReceivedToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)receivedAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            receivedAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void RevertToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)revertAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            revertAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void RejectToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)rejectedAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            rejectedAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void RemoveToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)trashAdvanceRequisitionDataGridView[column, row].EditedFormattedValue;
            trashAdvanceRequisitionDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }
        private void revertAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(revertAdvanceRequisitionDataGridView);

                    }

                    else if (e.ColumnIndex == 1)
                    {

                        if (revertAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                revertAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            ShowRequisitionHistory(requisition);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        if (revertAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                revertAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            AdvanceCategory advanceCategory =
                                _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId);

                            string title = " Advance Cash Requisition Entry Form (" + advanceCategory.Name + ")";
                            if (requisition != null)
                            {
                                BaseAdvanceCategory category =
                                    _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                                if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                                {
                                    if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                                    {
                                        AdvanceTravelRequisitionHeader header =
                                            (AdvanceTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                        RequisitionEntryForLocalTravelUI localTravelUi = new RequisitionEntryForLocalTravelUI(header, AdvancedFormMode.Update);
                                        localTravelUi.Text = title;
                                        localTravelUi.titleLabel.Text = title;
                                        localTravelUi.Show();
                                    }
                                    else if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                                    {
                                        AdvanceOverseasTravelRequisitionHeader header =
                                            (AdvanceOverseasTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                        RequisitionEntryForOverseasTravelUI localTravelUi = new RequisitionEntryForOverseasTravelUI(header, AdvancedFormMode.Update);
                                        localTravelUi.Text = title;
                                        localTravelUi.titleLabel.Text = title;
                                        localTravelUi.Show();
                                    }
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                                {
                                    AdvancePettyCashRequisitionHeader header =
                                    (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForPettyUI pettyUi = new RequisitionEntryForPettyUI(header, AdvancedFormMode.Update);
                                    pettyUi.Text = title;
                                    pettyUi.titleLabel.Text = title;
                                    pettyUi.Show();
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.CorporateAdvisory)
                                {
                                    AdvanceCorporateAdvisoryRequisitionHeader header =
                                        (AdvanceCorporateAdvisoryRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForCorporateAdvisoryUI corporateAdvisoryUi = new RequisitionEntryForCorporateAdvisoryUI(header, AdvancedFormMode.Update);
                                    corporateAdvisoryUi.Text = title;
                                    corporateAdvisoryUi.titleLabel.Text = title;
                                    corporateAdvisoryUi.Show();
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                                {
                                    AdvanceMiscelleneousRequisitionHeader header =
                                    (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForMiscellaneousUI miscellaneousUi = new RequisitionEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
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
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                   e.RowIndex >= 0)
                {
                    RevertToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rejectedAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(rejectedAdvanceRequisitionDataGridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (rejectedAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                rejectedAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;

                            ShowRequisitionHistory(requisition);
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
                    RejectToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trashAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(trashAdvanceRequisitionDataGridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {
                        if (trashAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                trashAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;

                            ShowRequisitionHistory(requisition);
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

        private void paidAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(paidAdvanceRequisitionDataGridView);

                    }

                    else if (e.ColumnIndex == 1)
                    {

                        if (paidAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                paidAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            ShowRequisitionHistory(requisition);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        MarkRequisitionAsReceived();
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



        private void MarkRequisitionAsReceived()
        {

            if (paidAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
            {
                var selectedRequisition =
                    paidAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (selectedRequisition != null)
                {
                    SelectReceiveDateUI selectReceiveDateUi = new SelectReceiveDateUI();
                    selectReceiveDateUi.ShowDialog();
                    if (selectReceiveDateUi.IsReceiveButtonClicked)
                    {
                        DateTime selectedReceiveDate = selectReceiveDateUi.receiveDateMetroDateTime.Value;
                        bool isReceived = _advanceRequisitionHeaderManager.RequisitionPayReceived(selectedRequisition.HeaderId, Session.LoginUserName,
                            selectedReceiveDate);
                        if (isReceived)
                        {
                            MessageBox.Show(@"Receive date insertion successful. Please find it in the 'Received Requisition' tab.", @"Success", MessageBoxButtons.OK,
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
        private void receivedAdvanceRequisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(receivedAdvanceRequisitionDataGridView);
                    }

                    else if (e.ColumnIndex == 1)
                    {

                        if (receivedAdvanceRequisitionDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                receivedAdvanceRequisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            ShowRequisitionHistory(requisition);
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        LoadCategoryWiseExpenseEntryUi();
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