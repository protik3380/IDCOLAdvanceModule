using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI.Approval.ExpenseApproval;
using IDCOLAdvanceModule.UI.Approval.RequisitionApproval;
using IDCOLAdvanceModule.UI.Expense;
using IDCOLAdvanceModule.UI.Voucher;
using IDCOLAdvanceModule.UI._360View;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class ExpensePaymentQueueUI : Form
    {
        private readonly string _userName = Session.LoginUserName;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly IExpenseVoucherHeaderManager _expenseVoucherHeader;

        public ExpensePaymentQueueUI()
        {
            InitializeComponent();
            InitializeColumnSorter();
            voucherTabControl.SelectedTab = expenseTabPage;
            _approvalProcessManager = new ApprovalProcessManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _departmentManager = new DepartmentManager();
            _expenseVoucherHeader = new ExpenseVoucherHeaderManager();
        }

        private void InitializeColumnSorter()
        {
            Utility.ListViewColumnSorter expenseColumnSorter = new Utility.ListViewColumnSorter();
            //advanceExpenseSearchListView.ListViewItemSorter = expenseColumnSorter;
            Utility.ListViewColumnSorter paidExpenseColumnSorter = new Utility.ListViewColumnSorter();
            //advancePaidExpenseSearchListView.ListViewItemSorter = paidExpenseColumnSorter;
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

        //private void LoadAdvanceExpenseSearchListView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        //{
        //    advanceExpenseSearchListView.Items.Clear();
        //    if (expenseList != null && expenseList.Any())
        //    {
        //        expenseList = expenseList.OrderBy(c => c.ExpenseEntryDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(expenseVm.AdvanceCategoryName);
        //            item.SubItems.Add(expenseVm.EmployeeName);
        //            item.SubItems.Add(expenseVm.EmployeeDepartmentName);
        //            item.SubItems.Add(expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.FromDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.ToDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(expenseVm.ExpenseAmountInBDT.ToString("N"));
        //            item.Tag = expenseVm;
        //            advanceExpenseSearchListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadAdvanceExpenseSearchGridView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            advanceExpenseSearchDataGridView.Rows.Clear();
            if (expenseList != null && expenseList.Any())
            {
                expenseList = expenseList.OrderBy(c => c.ExpenseEntryDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(advanceExpenseSearchDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "Pay";
                    row.Cells[2].Value = "View";
                    row.Cells[3].Value = serial.ToString();
                    row.Cells[4].Value = expenseVm.AdvanceCategoryName;
                    row.Cells[5].Value = expenseVm.EmployeeName;
                    row.Cells[6].Value = expenseVm.EmployeeDepartmentName;
                    row.Cells[7].Value = expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy");
                    row.Cells[8].Value = expenseVm.FromDate.ToString("dd-MMM-yyyy");
                    row.Cells[9].Value = expenseVm.ToDate.ToString("dd-MMM-yyyy");
                    row.Cells[10].Value = expenseVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[11].Value = expenseVm.ExpenseAmountInBDT.ToString("N");
                    row.Tag = expenseVm;
                    advanceExpenseSearchDataGridView.Rows.Add(row);
                    serial++; 
                }
            }
        }

        //private void LoadAdvancePaidExpenseSearchListView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        //{
        //    advancePaidExpenseSearchListView.Items.Clear();
        //    if (expenseList != null && expenseList.Any())
        //    {
        //        expenseList = expenseList.OrderBy(c => c.ExpenseIssueDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(expenseVm.AdvanceCategoryName);
        //            item.SubItems.Add(expenseVm.EmployeeName);
        //            item.SubItems.Add(expenseVm.EmployeeDepartmentName);
        //            item.SubItems.Add(expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.FromDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.ToDate.ToString("dd-MMM-yyyy"));
        //            item.SubItems.Add(expenseVm.AdvanceAmountInBDT.ToString("N"));
        //            item.SubItems.Add(expenseVm.ExpenseAmountInBDT.ToString("N"));
        //            item.Tag = expenseVm;
        //            advancePaidExpenseSearchListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadAdvancePaidExpenseSearchGridView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            advancePaidExpenseSearchDataGridView.Rows.Clear();
            if (expenseList != null && expenseList.Any())
            {
                expenseList = expenseList.OrderBy(c => c.ExpenseIssueDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(advancePaidExpenseSearchDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "Payment Entry";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = expenseVm.AdvanceCategoryName;
                    row.Cells[4].Value = expenseVm.EmployeeName;
                    row.Cells[5].Value = expenseVm.EmployeeDepartmentName;
                    row.Cells[6].Value = expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy");
                    row.Cells[7].Value = expenseVm.FromDate.ToString("dd-MMM-yyyy");
                    row.Cells[8].Value = expenseVm.ToDate.ToString("dd-MMM-yyyy");
                    row.Cells[9].Value = expenseVm.AdvanceAmountInBDT.ToString("N");
                    row.Cells[10].Value = expenseVm.ExpenseAmountInBDT.ToString("N");
                    row.Tag = expenseVm;
                    advancePaidExpenseSearchDataGridView.Rows.Add(row);
                    serial++; 

                   
                }
            }
        }

        private void GoToExpense360ViewUi()
        {
            try
            {
                if (advanceExpenseSearchDataGridView.SelectedRows.Count > 0)
                {
                    var expense = advanceExpenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;

                    if (expense == null)
                    {
                        throw new UiException("Selected expense has no data.");
                    }

                    Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expense.HeaderId);
                    expense360ViewUi.Show();
                }
                else
                {
                    MessageBox.Show(@"Please select a requesition.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void GoToPaidExpense360ViewUi()
        {
            try
            {
                if (advancePaidExpenseSearchDataGridView.SelectedRows.Count > 0)
                {
                    var expense = advancePaidExpenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;

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
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Search()
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
            criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?) null;
            criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?) null;
            criteria.EmployeeName = employeeNameTextBox.Text;

            ICollection<Advance_VW_GetAdvanceExpense> unPaidExpenseList =
                _approvalProcessManager.GetUnpaidExpensesForMember(criteria);
            LoadAdvanceExpenseSearchGridView(unPaidExpenseList);

            ICollection<Advance_VW_GetAdvanceExpense> paidExpensesList =
                _approvalProcessManager.GetPaidExpensesForMember(criteria);
            LoadAdvancePaidExpenseSearchGridView(paidExpensesList);
        }

        private void ExpensePaymentQueueUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdvanceRequisitionCategoryComboBox();
                LoadPaymentData();
                LoadDepartmentComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadPaymentData()
        {
            ICollection<Advance_VW_GetAdvanceExpense> unPaidExpenseList =
                _approvalProcessManager.GetUnpaidExpensesForMember();
            LoadAdvanceExpenseSearchGridView(unPaidExpenseList);

            ICollection<Advance_VW_GetAdvanceExpense> paidExpensesList =
                _approvalProcessManager.GetPaidExpensesForMember();
            LoadAdvancePaidExpenseSearchGridView(paidExpensesList);
            LoadVoucher();
        }

        private void LoadVoucher()
        {
            ICollection<ExpenseVoucherHeader> expenseVoucherHeaders =
                _expenseVoucherHeader.GetAll();
            var isDraftSelected = draftRadioButton.Checked;
            if (isDraftSelected)
            {
               // expenseVoucherListView.ContextMenuStrip = draftVoucherContextMenu;
                expenseVoucherHeaders =
                    _expenseVoucherHeader.GetAllDraftVoucher();
                LoadDraftExpenseVoucherRequisitionVoucherGridView(expenseVoucherHeaders);
            }
            else
            {
                //expenseVoucherListView.ContextMenuStrip = sentVoucherContextMenu;

                expenseVoucherHeaders =
                  _expenseVoucherHeader.GetAllSentVoucher();
                LoadSendExpenseVoucherRequisitionVoucherGridView(expenseVoucherHeaders);
            }
            
        }

        //private void LoadExpenseVoucherRequisitionVoucherListView(ICollection<ExpenseVoucherHeader> expenseVoucherHeaders)
        //{
        //    expenseVoucherListView.Items.Clear();
        //    if (expenseVoucherHeaders != null && expenseVoucherHeaders.Any())
        //    {
        //        expenseVoucherHeaders = expenseVoucherHeaders.OrderBy(c => c.VoucherEntryDate).ToList();
        //        int serial = 1;
        //        foreach (var voucher in expenseVoucherHeaders)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(voucher.ExpenseHeader.ExpenseNo);
        //            item.SubItems.Add(voucher.VoucherDescription);
        //            item.SubItems.Add(voucher.GetTotalDrAmount() != null
        //                ? voucher.GetTotalDrAmount().Value.ToString("N")
        //                : null);
        //            item.SubItems.Add(voucher.GetTotalCrAmount() != null
        //                ? voucher.GetTotalCrAmount().Value.ToString("N")
        //                : null);
        //            item.Tag = voucher;
        //            expenseVoucherListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadDraftExpenseVoucherRequisitionVoucherGridView(ICollection<ExpenseVoucherHeader> expenseVoucherHeaders)
        {
            expenseVoucherDataGridView.Rows.Clear();
            if (expenseVoucherHeaders != null && expenseVoucherHeaders.Any())
            {
                expenseVoucherHeaders = expenseVoucherHeaders.OrderBy(c => c.VoucherEntryDate).ToList();
                int serial = 1;
                foreach (var voucher in expenseVoucherHeaders)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseVoucherDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "Preview and Send Voucher";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = voucher.ExpenseHeader.ExpenseNo;
                    row.Cells[4].Value = voucher.VoucherDescription ;
                    row.Cells[5].Value = voucher.GetTotalDrAmount() != null
                        ? voucher.GetTotalDrAmount().Value.ToString("N")
                        : null;
                    row.Cells[6].Value = voucher.GetTotalCrAmount() != null
                        ? voucher.GetTotalCrAmount().Value.ToString("N")
                        : null;

                    row.Tag = voucher;
                    expenseVoucherDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void LoadSendExpenseVoucherRequisitionVoucherGridView(ICollection<ExpenseVoucherHeader> expenseVoucherHeaders)
        {
            expenseVoucherDataGridView.Rows.Clear();
            if (expenseVoucherHeaders != null && expenseVoucherHeaders.Any())
            {
                expenseVoucherHeaders = expenseVoucherHeaders.OrderBy(c => c.VoucherEntryDate).ToList();
                int serial = 1;
                foreach (var voucher in expenseVoucherHeaders)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseVoucherDataGridView);
                    row.Cells[0]=new DataGridViewTextBoxCell();
                    row.Cells[1] = new DataGridViewTextBoxCell();
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = voucher.ExpenseHeader.ExpenseNo;
                    row.Cells[4].Value = voucher.VoucherDescription;
                    row.Cells[5].Value = voucher.GetTotalDrAmount() != null
                        ? voucher.GetTotalDrAmount().Value.ToString("N")
                        : null;
                    row.Cells[6].Value = voucher.GetTotalCrAmount() != null
                        ? voucher.GetTotalCrAmount().Value.ToString("N")
                        : null;

                    row.Tag = voucher;
                    expenseVoucherDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void view360ExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void advanceExpenseSearchListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (advanceExpenseSearchListView.SelectedItems.Count > 0)
                //{
                //    payExpenseButton.Visible = true;
                //    expense360ViewButton.Visible = true;
                //}
                //else
                //{
                //    payExpenseButton.Visible = false;
                //    expense360ViewButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void expense360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToExpense360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void paid360ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void paidExpense360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToPaidExpense360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void paymentEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PayAgainstExpense()
        {
            if (advancePaidExpenseSearchDataGridView.SelectedRows.Count > 0)
            {
                var selectedExpense =
                    advancePaidExpenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                if (selectedExpense != null)
                {
                    AdvanceExpenseHeader header =
                        _advanceExpenseHeaderManager.GetById(selectedExpense.HeaderId);
                    RecipientPayeeListUI recipientPayeeListUi = new RecipientPayeeListUI(header);
                    recipientPayeeListUi.ShowDialog();
                    LoadPaymentData();
                }
            }
            else
            {
                MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        
        //private void advanceExpenseSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    try
        //    {
        //        Utility.Utility.SortColumn(e, advanceExpenseSearchListView);
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        //private void advancePaidExpenseSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    try
        //    {
        //        Utility.Utility.SortColumn(e, advancePaidExpenseSearchListView);
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        private void payExpenseButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenSelectPaymentDateUiForExpense();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OpenSelectPaymentDateUiForExpense()
        {
            if (advanceExpenseSearchDataGridView.SelectedRows.Count > 0)
            {
                var selectedExpense =
                    advanceExpenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                if (selectedExpense != null)
                {
                    SelectPaymentDateUI selectPaymentDateUi = new SelectPaymentDateUI();
                    selectPaymentDateUi.ShowDialog();
                    if (selectPaymentDateUi.IsPayButtonClicked)
                    {
                        DateTime selectedPaymentDate = selectPaymentDateUi.paymentDateMetroDateTime.Value;
                        bool isPaid = _advanceExpenseHeaderManager.PayExpense(selectedExpense.HeaderId, Session.LoginUserName,
                            selectedPaymentDate);
                        if (isPaid)
                        {
                            MessageBox.Show(@"Payment date insertion successful. Please find it in the 'Paid Adjustment/Reimbursement' tab.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            LoadPaymentData();
                        }
                        else
                        {
                            MessageBox.Show(@"Payment date insertion failed.", @"Warning!", MessageBoxButtons.OK,
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

        private void expensePaymentEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                PayAgainstExpense();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void payToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //private void advancePaidExpenseSearchListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (advancePaidExpenseSearchListView.SelectedItems.Count > 0)
        //        {
        //            paidExpense360ViewButton.Visible = true;
        //            expensePaymentEntryButton.Visible = true;
        //        }
        //        else
        //        {
        //            paidExpense360ViewButton.Visible = false;
        //            expensePaymentEntryButton.Visible = false;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        private void editVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseVoucherListView.SelectedItems.Count > 0)
                //{
                //    var requisitionVoucherHeader =
                //        expenseVoucherListView.SelectedItems[0].Tag as ExpenseVoucherHeader;
                //    if (requisitionVoucherHeader == null)
                //    {
                //        throw new UiException("Voucher header not tagged.");
                //    }
                //    ExpenseVoucherEntryUI expenseVoucherEntryUi = new ExpenseVoucherEntryUI(requisitionVoucherHeader, AdvancedFormMode.Update);
                //    expenseVoucherEntryUi.Show();
                //    Search();
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item", @"Error!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void previewVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseVoucherListView.SelectedItems.Count > 0)
                //{
                //    var voucher = expenseVoucherListView.SelectedItems[0].Tag as ExpenseVoucherHeader;
                //    AdvanceVoucherGenerateManager voucherGenerateManager = new AdvanceVoucherGenerateManager();
                //    var transactionVoucher = voucherGenerateManager.GetTransactionVoucherFromExpenseVoucher(voucher);
                //    PreviewVoucherUI voucherUi = new PreviewVoucherUI(transactionVoucher, voucher);
                //    voucherUi.ShowDialog();
                //    LoadVoucher();
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void draftRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadVoucher();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void advanceExpenseSearchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        GoToExpense360ViewUi(); 
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        OpenSelectPaymentDateUiForExpense();  
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        if (advanceExpenseSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var expense = advanceExpenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;

                            if (expense == null)
                            {
                                throw new UiException("Selected adjustment has no data.");
                            }

                            ExpenseViewUI expenseViewUi = new ExpenseViewUI(expense.HeaderId);
                            expenseViewUi.Show();
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an adjustment.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        } 
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancePaidExpenseSearchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        GoToPaidExpense360ViewUi();
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        PayAgainstExpense();  
                    }
                  
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseVoucherDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (expenseVoucherDataGridView.SelectedRows.Count > 0)
                        {
                            var requisitionVoucherHeader =
                                expenseVoucherDataGridView.SelectedRows[0].Tag as ExpenseVoucherHeader;
                            if (requisitionVoucherHeader == null)
                            {
                                throw new UiException("Voucher header not tagged.");
                            }
                            ExpenseVoucherEntryUI expenseVoucherEntryUi = new ExpenseVoucherEntryUI(requisitionVoucherHeader, AdvancedFormMode.Update);
                            expenseVoucherEntryUi.Show();
                            Search();
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }  
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        if (expenseVoucherDataGridView.SelectedRows.Count > 0)
                        {
                            var voucher = expenseVoucherDataGridView.SelectedRows[0].Tag as ExpenseVoucherHeader;
                            AdvanceVoucherGenerateManager voucherGenerateManager = new AdvanceVoucherGenerateManager();
                            var transactionVoucher = voucherGenerateManager.GetTransactionVoucherFromExpenseVoucher(voucher);
                            PreviewVoucherUI voucherUi = new PreviewVoucherUI(transactionVoucher, voucher);
                            voucherUi.ShowDialog();
                            LoadVoucher();
                        } 
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
