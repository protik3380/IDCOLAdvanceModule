using IDCOLAdvanceModule.BLL;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI._360View;
using IDCOLAdvanceModule.UI.Approval.ExpenseApproval;
using IDCOLAdvanceModule.UI.Approval.RequisitionApproval;
using IDCOLAdvanceModule.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IDCOLAdvanceModule.UI
{
    public partial class ApprovalQueueUI : Form
    {
        private readonly string _userName = Session.LoginUserName;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly IRequisitionApprovalTicketManager _requisitionApprovalTicketManager;
        private readonly IExpenseApprovalTicketManager _expenseApprovalTicketManager;

        public ApprovalQueueUI()
        {
            InitializeComponent();
            InitializeColumnSorter();
            approvalQueueTabControl.SelectedTab = requisitionTabPage;
            _approvalProcessManager = new ApprovalProcessManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _departmentManager = new DepartmentManager();
            _requisitionApprovalTicketManager = new RequisitionApprovalTicketManager();
            _expenseApprovalTicketManager = new ExpenseApprovalTicketManager();
        }

        private void InitializeColumnSorter()
        {
            //Utility.ListViewColumnSorter requisitionColumnSorter = new Utility.ListViewColumnSorter();
            ////requisitionListView.ListViewItemSorter = requisitionColumnSorter;
            //Utility.ListViewColumnSorter expenseColumnSorter = new Utility.ListViewColumnSorter();
            //expenseListView.ListViewItemSorter = expenseColumnSorter;
            //Utility.ListViewColumnSorter requisitionHistoryColumnSorter = new Utility.ListViewColumnSorter();
            ////requisitionHistoryListView.ListViewItemSorter = requisitionHistoryColumnSorter;
            //Utility.ListViewColumnSorter expenseHistoryColumnSorter = new Utility.ListViewColumnSorter();
            //expenseHistoryListView.ListViewItemSorter = expenseHistoryColumnSorter;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                AdvanceRequisitionSearchCriteria criteria = new AdvanceRequisitionSearchCriteria();
                if (Convert.ToInt64(advanceRequisitionCategoryComboBox.SelectedValue) != DefaultItem.Value)
                {
                    criteria.AdvanceCategoryId = Convert.ToInt64(departmentComboBox.SelectedValue);
                }
                if (Convert.ToInt64(departmentComboBox.SelectedValue) != DefaultItem.Value)
                {
                    criteria.DepartmentId = departmentComboBox.SelectedValue as decimal?;
                }
                criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?)null;
                criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?)null;
                criteria.EmployeeName = employeeNameTextBox.Text;

                ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                    _approvalProcessManager.GetWaitingForApprovalRequisitionsForMember(_userName, criteria);
                LoadRequisitionGridView(requisitionList);
                ICollection<Advance_VW_GetAdvanceExpense> expenseList =
                    _approvalProcessManager.GetWaitingForApprovalExpensesForMember(_userName, criteria);
                LoadExpenseGridView(expenseList);
                ICollection<Advance_VW_GetAdvanceRequisition> requisitionHistoryList =
                    _approvalProcessManager.GetRequisitionsByApprovedBy(_userName, criteria);
                LoadRequisitionHistoryGridView(requisitionHistoryList);
                ICollection<Advance_VW_GetAdvanceExpense> expenseHistoryList =
                    _approvalProcessManager.GetExpensesByApprovedBy(_userName, criteria);
                LoadExpenseHistoryGridView(expenseHistoryList);
                LoadDepartmentComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RequisitionApprovalQueueUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdvanceRequisitionCategoryComboBox();
                LoadAllListView();
                LoadDepartmentComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllListView()
        {
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                    _approvalProcessManager.GetWaitingForApprovalRequisitionsForMember(_userName);
            LoadRequisitionGridView(requisitionList);
            ICollection<Advance_VW_GetAdvanceExpense> expenseList =
                _approvalProcessManager.GetWaitingForApprovalExpensesForMember(_userName);
            LoadExpenseGridView(expenseList);
            ICollection<Advance_VW_GetAdvanceRequisition> requisitionHistoryList =
                _approvalProcessManager.GetRequisitionsByApprovedBy(_userName);
            LoadRequisitionHistoryGridView(requisitionHistoryList);
            ICollection<Advance_VW_GetAdvanceExpense> expenseHistoryList =
                _approvalProcessManager.GetExpensesByApprovedBy(_userName);
            LoadExpenseHistoryGridView(expenseHistoryList);
        }

        private void LoadAdvanceRequisitionCategoryComboBox()
        {
            advanceRequisitionCategoryComboBox.DataSource = null;
            List<AdvanceCategory> categories = new List<AdvanceCategory>
            {
                new AdvanceCategory{Id = DefaultItem.Value, Name = DefaultItem.Text}
            };
            categories.AddRange(_advanceRequisitionCategoryManager.GetAll());
            advanceRequisitionCategoryComboBox.DisplayMember = "Name";
            advanceRequisitionCategoryComboBox.ValueMember = "Id";
            advanceRequisitionCategoryComboBox.DataSource = categories;
        }

        private void LoadDepartmentComboBox()
        {
            departmentComboBox.DataSource = null;
            List<Admin_Departments> departments = new List<Admin_Departments>
            {
                new Admin_Departments{DepartmentID = DefaultItem.Value, DepartmentName = DefaultItem.Text}
            };
            departments.AddRange(_departmentManager.GetAll());
            departmentComboBox.DisplayMember = "DepartmentName";
            departmentComboBox.ValueMember = "DepartmentID";
            departmentComboBox.DataSource = departments;
        }

        //private void LoadRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    requisitionListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(requisitionVm.RequisitionNo);
        //            item.SubItems.Add(requisitionVm.RequisitionCategoryName);
        //            item.SubItems.Add(requisitionVm.EmployeeName);
        //            item.SubItems.Add(requisitionVm.EmployeeDepartmentName);
        //            item.SubItems.Add(requisitionVm.RequisitionDate != null
        //                ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.FromDate != null
        //                ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.ToDate != null
        //                ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N"));

        //            item.Tag = requisitionVm;
        //            item.BackColor = GetListViewItemBackColor(requisitionVm.AuthorizedOn);
        //            item.ForeColor = GetListViewItemFontColor(item.BackColor);
        //            requisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //        requisitionTabPage.Text = @"Requisition (" + requisitionList.Count + @")";
        //    }
        //    else
        //    {
        //        requisitionTabPage.Text = @"Requisition (0)";
        //    }
        //}

        private void LoadRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            requisitionDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(requisitionDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "Approve";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = requisitionVm.RequisitionNo;
                    row.Cells[4].Value = requisitionVm.RequisitionCategoryName;
                    row.Cells[5].Value = requisitionVm.EmployeeName;
                    row.Cells[6].Value = requisitionVm.EmployeeDepartmentName;
                    row.Cells[7].Value = requisitionVm.RequisitionDate != null
                        ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = requisitionVm.FromDate != null
                        ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = requisitionVm.ToDate != null
                        ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N");
                    row.Tag = requisitionVm;
                    //item.BackColor = GetListViewItemBackColor(requisitionVm.AuthorizedOn);
                    //item.ForeColor = GetListViewItemFontColor(item.BackColor);
                    requisitionDataGridView.Rows.Add(row);
                    serial++;
                }
                requisitionTabPage.Text = @"Requisition (" + requisitionList.Count + @")";
            }
            else
            {
                requisitionTabPage.Text = @"Requisition (0)";
            }
        }

        //private void LoadRequisitionHistoryListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    requisitionHistoryListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(requisitionVm.RequisitionNo);
        //            item.SubItems.Add(requisitionVm.RequisitionCategoryName);
        //            item.SubItems.Add(requisitionVm.EmployeeName);
        //            item.SubItems.Add(requisitionVm.EmployeeDepartmentName);
        //            item.SubItems.Add(requisitionVm.RequisitionDate != null
        //                ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.FromDate != null
        //                ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.ToDate != null
        //                ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N"));

        //            item.Tag = requisitionVm;
        //            requisitionHistoryListView.Items.Add(item);
        //            serial++;
        //        }
        //        requisitionHistoryTabPage.Text = @"Requisition History (" + requisitionList.Count + @")";
        //    }
        //    else
        //    {
        //        requisitionHistoryTabPage.Text = @"Requisition History (0)";
        //    }
        //}

        private void LoadRequisitionHistoryGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            requisitionHistoryDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderByDescending(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(requisitionHistoryDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = serial.ToString();
                    row.Cells[2].Value = requisitionVm.RequisitionNo;
                    row.Cells[3].Value = requisitionVm.RequisitionCategoryName;
                    row.Cells[4].Value =requisitionVm.EmployeeName ;
                    row.Cells[5].Value = requisitionVm.EmployeeDepartmentName;
                    row.Cells[6].Value = requisitionVm.RequisitionDate != null
                        ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = requisitionVm.FromDate != null
                        ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = requisitionVm.ToDate != null
                        ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N");
                    row.Tag = requisitionVm;
                    requisitionHistoryDataGridView.Rows.Add(row);
                    serial++;
                }
                requisitionHistoryTabPage.Text = @"Requisition History (" + requisitionList.Count + @")";
            }
            else
            {
                requisitionHistoryTabPage.Text = @"Requisition History (0)";
            }
        }


        private Color GetListViewItemBackColor(DateTime? authorizedDate)
        {
            var itemColor = new Color();

            if (authorizedDate != null)
            {
                if (CalculateNoOfDays(authorizedDate.Value.Date, DateTime.Now.Date) <= 2)
                {
                    itemColor = Color.Green;
                }
                else if (CalculateNoOfDays(authorizedDate.Value.Date, DateTime.Now.Date) >= 2 && CalculateNoOfDays(authorizedDate.Value.Date, DateTime.Now.Date) <= 5)
                {
                    itemColor = Color.Yellow;
                }
                else if (CalculateNoOfDays(authorizedDate.Value.Date, DateTime.Now.Date) > 5)
                {
                    itemColor = Color.Red;
                }
            }

            return itemColor;
        }

        private Color GetListViewItemFontColor(Color backColor)
        {
            var fontColor = new Color();
            if (backColor == Color.Green)
            {
                fontColor = Color.White;
            }
            else if (backColor == Color.Yellow)
            {
                fontColor = Color.Black;
            }
            else if (backColor == Color.Red)
            {
                fontColor = Color.White;
            }
            return fontColor;
        }

        private int CalculateNoOfDays(DateTime fromDate, DateTime toDate)
        {
            int noOfDays = (int)(toDate - fromDate).TotalDays;
            return noOfDays;
        }

        //private void LoadExpenseListView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        //{
        //    expenseListView.Items.Clear();
        //    if (expenseList != null && expenseList.Any())
        //    {
        //        expenseList = expenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(expenseVm.ExpenseNo);
        //            item.SubItems.Add(expenseVm.AdvanceCategoryName);
        //            item.SubItems.Add(expenseVm.EmployeeName);
        //            item.SubItems.Add(expenseVm.EmployeeDepartmentName);
        //            item.SubItems.Add(expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.FromDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.ToDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(expenseVm.ExpenseAmountInBDT.ToString("N"));
        //            item.Tag = expenseVm;
        //            item.BackColor = GetListViewItemBackColor(expenseVm.AuthorizedOn);
        //            item.ForeColor = GetListViewItemFontColor(item.BackColor);
        //            expenseListView.Items.Add(item);
        //            serial++;
        //        }
        //        expenseTabPage.Text = @"Adjustment/Reimbursement (" + expenseList.Count + @")";
        //    }
        //    else
        //    {
        //        expenseTabPage.Text = @"Adjustment/Reimbursement (0)";
        //    }
        //}

        private void LoadExpenseGridView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            expenseDataGridView.Rows.Clear();
            if (expenseList != null && expenseList.Any())
            {
                expenseList = expenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "Approve";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = expenseVm.ExpenseNo;
                    row.Cells[4].Value = expenseVm.AdvanceCategoryName;
                    row.Cells[5].Value = expenseVm.EmployeeName;
                    row.Cells[6].Value = expenseVm.EmployeeDepartmentName;
                    row.Cells[7].Value = expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy");
                    row.Cells[8].Value = expenseVm.FromDate.ToString("dd-MMM-yyyy");
                    row.Cells[9].Value = expenseVm.ToDate.ToString("dd-MMM-yyyy");
                    row.Cells[10].Value = expenseVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[11].Value =expenseVm.ExpenseAmountInBDT.ToString("N");
                    row.Tag = expenseVm;
                    expenseDataGridView.Rows.Add(row);
                    serial++;
                    //item.BackColor = GetListViewItemBackColor(expenseVm.AuthorizedOn);
                    //item.ForeColor = GetListViewItemFontColor(item.BackColor);
                    //serial++;
                }
                expenseTabPage.Text = @"Adjustment/Reimbursement (" + expenseList.Count + @")";
            }
            else
            {
                expenseTabPage.Text = @"Adjustment/Reimbursement (0)";
            }
        }

        //private void LoadExpenseHistoryListView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        //{
        //    expenseHistoryListView.Items.Clear();
        //    if (expenseList != null && expenseList.Any())
        //    {
        //        expenseList = expenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(expenseVm.ExpenseNo);
        //            item.SubItems.Add(expenseVm.AdvanceCategoryName);
        //            item.SubItems.Add(expenseVm.EmployeeName);
        //            item.SubItems.Add(expenseVm.EmployeeDepartmentName);
        //            item.SubItems.Add(expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.FromDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.ToDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(expenseVm.ExpenseAmountInBDT.ToString("N"));
        //            item.Tag = expenseVm;
        //            expenseHistoryListView.Items.Add(item);
        //            serial++;
        //        }
        //        expenseHistoryTabPage.Text = @"Adjustment/Reimbursement History (" + expenseList.Count + @")";
        //    }
        //    else
        //    {
        //        expenseHistoryTabPage.Text = @"Adjustment/Reimbursement History (0)";
        //    }
        //}

        private void LoadExpenseHistoryGridView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            expenseHistoryDataGridView.Rows.Clear();
            if (expenseList != null && expenseList.Any())
            {
                expenseList = expenseList.OrderByDescending(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseHistoryDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = serial.ToString();
                    row.Cells[2].Value = expenseVm.ExpenseNo;
                    row.Cells[3].Value = expenseVm.AdvanceCategoryName;
                    row.Cells[4].Value =expenseVm.EmployeeName ;
                    row.Cells[5].Value = expenseVm.EmployeeDepartmentName;
                    row.Cells[6].Value = expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy");
                    row.Cells[7].Value = expenseVm.FromDate.ToString("dd-MMM-yyyy");
                    row.Cells[8].Value = expenseVm.ToDate.ToString("dd-MMM-yyyy");
                    row.Cells[9].Value = expenseVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[10].Value = expenseVm.ExpenseAmountInBDT.ToString("N");
                    row.Tag = expenseVm;
                    expenseHistoryDataGridView.Rows.Add(row);
                    serial++;
                }
                expenseHistoryTabPage.Text = @"Adjustment/Reimbursement History (" + expenseList.Count + @")";
            }
            else
            {
                expenseHistoryTabPage.Text = @"Adjustment/Reimbursement History (0)";
            }
        }

        private void view360ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //GoToRequisition360ViewUi(requisitionListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void approveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //GoToRequisitionApproveUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoToRequisitionApproveUi()
        {
            try
            {
                if (requisitionDataGridView.SelectedRows.Count > 0)
                {
                    var requisition =
                        requisitionDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                    if (requisition != null)
                    {
                        RequisitionApprovalUI approvalUi =
                                new RequisitionApprovalUI(requisition.HeaderId);
                        approvalUi.ShowDialog();
                        ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                            _approvalProcessManager.GetWaitingForApprovalRequisitionsForMember(_userName);
                        LoadRequisitionGridView(requisitionList);
                        ICollection<Advance_VW_GetAdvanceRequisition> requisitionHistoryList =
                            _approvalProcessManager.GetRequisitionsByApprovedBy(_userName);
                        LoadRequisitionHistoryGridView(requisitionHistoryList);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please select a requisition to approve.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoToExpenseApproveUi()
        {
            if (expenseDataGridView.SelectedRows.Count > 0)
            {
                var expense = expenseDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                if (expense != null)
                {
                    ExpenseApprovalUI approvalUi = new ExpenseApprovalUI(expense.HeaderId);
                    approvalUi.ShowDialog();
                    ICollection<Advance_VW_GetAdvanceExpense> expenseList =
                        _approvalProcessManager.GetWaitingForApprovalExpensesForMember(_userName);
                    LoadExpenseGridView(expenseList);
                    ICollection<Advance_VW_GetAdvanceExpense> expenseHistoryList =
                        _approvalProcessManager.GetExpensesByApprovedBy(_userName);
                    LoadExpenseHistoryGridView(expenseHistoryList);
                }
            }
            else
            {
                MessageBox.Show(@"Please select an expense to approve.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        //private void GoToExpense360ViewUi(ListView listView)
        //{
        //    if (listView.SelectedItems.Count > 0)
        //    {
        //        var expense = listView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;

        //        if (expense == null)
        //        {
        //            throw new UiException("Selected expense has no data.");
        //        }

        //        Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expense.HeaderId);
        //        expense360ViewUi.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //    }
        //}

        private void GoToExpense360ViewUi(DataGridView gridView)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                var expense = gridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;

                if (expense == null)
                {
                    throw new UiException("Selected expense has no data.");
                }

                Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expense.HeaderId);
                expense360ViewUi.Show();
            }
            else
            {
                MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void requisitionListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (requisitionListView.SelectedItems.Count > 0)
                //{
                //    requisitionApproveButton.Visible = true;
                //    requisition360ViewButton.Visible = true;
                //}
                //else
                //{
                //    requisitionApproveButton.Visible = false;
                //    requisition360ViewButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void expenseListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (expenseListView.SelectedItems.Count > 0)
        //        {
        //            expense360ViewButton.Visible = true;
        //            expenseApproveButton.Visible = true;
        //        }
        //        else
        //        {
        //            expense360ViewButton.Visible = false;
        //            expenseApproveButton.Visible = false;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void requisitionApproveButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToRequisitionApproveUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void GoToRequisition360ViewUi(ListView listView)
        //{
        //    if (listView.SelectedItems.Count > 0)
        //    {
        //        var requisition =
        //            listView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
        //        if (requisition == null)
        //        {
        //            throw new UiException("Selected requisition has no data.");
        //        }
        //        var requisition360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
        //        requisition360ViewUi.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //    }
        //}

        private void GoToRequisition360ViewUi(DataGridView gridView)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                var requisition =
                    gridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (requisition == null)
                {
                    throw new UiException("Selected requisition has no data.");
                }
                var requisition360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
                requisition360ViewUi.Show();
            }
            else
            {
                MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void requisition360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                //GoToRequisition360ViewUi(requisitionListView);
                GoToRequisition360ViewUi(requisitionDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expense360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToExpense360ViewUi(expenseDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseApproveButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToExpenseApproveUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
               // Utility.Utility.SortColumn(e, requisitionListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void expenseListView_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    try
        //    {
        //        Utility.Utility.SortColumn(e, expenseListView);
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void requisitionHistoryListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Utility.Utility.SortColumn(e, requisitionHistoryListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void requisitionHistory360ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //GoToRequisition360ViewUi(requisitionHistoryListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionHistory360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToRequisition360ViewUi(requisitionHistoryDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseHistory360ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void expenseHistory360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToExpense360ViewUi(expenseDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void expenseHistoryListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (expenseHistoryListView.SelectedItems.Count > 0)
        //        {
        //            expenseHistory360ViewButton.Visible = true;
        //        }
        //        else
        //        {
        //            expenseHistory360ViewButton.Visible = false;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void requisitionHistoryListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (requisitionHistoryListView.SelectedItems.Count > 0)
                //{
                //    requisitionHistory360ViewButton.Visible = true;
                //}
                //else
                //{
                //    requisitionHistory360ViewButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionApproveAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (requisitionDataGridView.SelectedRows.Count > 0)
                {
                    List<RequisitionApprovalTicket> tickets = new List<RequisitionApprovalTicket>();
                    //foreach (ListViewItem item in requisitionListView.CheckedItems)
                    //{
                    //    var requisition = item.Tag as Advance_VW_GetAdvanceRequisition;
                    //    if (requisition != null)
                    //    {
                    //        var ticket =
                    //            _requisitionApprovalTicketManager.GetById((long)requisition.RequistionApprovalTicketId);
                    //        tickets.Add(ticket);
                    //    }
                    //}
                    foreach (DataGridViewRow row in requisitionDataGridView.SelectedRows)
                    {
                        var requisition = row.Tag as Advance_VW_GetAdvanceRequisition;
                        if (requisition != null)
                        {
                            var ticket =
                                _requisitionApprovalTicketManager.GetById((long)requisition.RequistionApprovalTicketId);
                            tickets.Add(ticket);
                        }
                    }
                    var isApproved = _approvalProcessManager.Approve(tickets, Session.LoginUserName.ToLower());
                    if (isApproved)
                    {
                        ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                            _approvalProcessManager.GetWaitingForApprovalRequisitionsForMember(_userName);
                        LoadRequisitionGridView(requisitionList);
                        MessageBox.Show(@"Requisition(s) approved sucessfully.", @"Success", MessageBoxButtons.OK,
                             MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(@"Requisition(s) faild to approve", @"Error!", MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please choose an item.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                //if (requisitionListView.CheckedItems.Count > 0)
                //{
                //    requisitionApproveAllButton.Visible = true;
                //}
                //else
                //{
                //    requisitionApproveAllButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void expenseListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        //{
        //    try
        //    {
        //        if (expenseListView.CheckedItems.Count > 0)
        //        {
        //            expenseApproveAllButton.Visible = true;
        //        }
        //        else
        //        {
        //            expenseApproveAllButton.Visible = false;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void expenseApproveAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (expenseDataGridView.SelectedRows.Count > 0)
                {
                    List<ExpenseApprovalTicket> tickets = new List<ExpenseApprovalTicket>();
                    //foreach (ListViewItem item in expenseListView.CheckedItems)
                    //{
                    //    var requisition = item.Tag as Advance_VW_GetAdvanceExpense;
                    //    if (requisition != null)
                    //    {
                    //        var ticket =
                    //            _expenseApprovalTicketManager.GetById((long)requisition.ExpenseApprovalTicketId);
                    //        tickets.Add(ticket);
                    //    }
                    //}

                    foreach (DataGridViewRow row in expenseDataGridView.Rows)
                    {
                        var requisition = row.Tag as Advance_VW_GetAdvanceExpense;
                        if (requisition != null)
                        {
                            var ticket =
                                _expenseApprovalTicketManager.GetById((long)requisition.ExpenseApprovalTicketId);
                            tickets.Add(ticket);
                        }
                    }
                    var isApproved = _approvalProcessManager.Approve(tickets, Session.LoginUserName.ToLower());
                    if (isApproved)
                    {
                        LoadAllListView();
                        MessageBox.Show(@"Requisition(s) approved sucessfully.", @"Success", MessageBoxButtons.OK,
                             MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(@"Requisition(s) faild to approve", @"Error!", MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please choose an item.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        GoToRequisition360ViewUi(requisitionDataGridView); 
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        GoToRequisitionApproveUi();  
                    }
                   
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionHistoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    GoToRequisition360ViewUi(requisitionHistoryDataGridView);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        GoToExpense360ViewUi(expenseDataGridView); 
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        GoToExpenseApproveUi(); 
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseHistoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        GoToExpense360ViewUi(expenseHistoryDataGridView);
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
