using System.Drawing;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI.Approval.ExpenseApproval;
using IDCOLAdvanceModule.UI.Approval.RequisitionApproval;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.UI.Entry;
using IDCOLAdvanceModule.UI.Expense;
using IDCOLAdvanceModule.UI.Voucher;
using IDCOLAdvanceModule.UI._360View;

namespace IDCOLAdvanceModule.UI
{
    public partial class PaymentQueueUI : Form
    {
        private readonly string _userName = Session.LoginUserName;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IDepartmentManager _departmentManager;

        public PaymentQueueUI()
        {
            InitializeComponent();
            InitializeColumnSorter();
            approvalQueueTabControl.SelectedTab = requisitionTabPage;
            _approvalProcessManager = new ApprovalProcessManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _departmentManager = new DepartmentManager();
        }

        private void InitializeColumnSorter()
        {
            Utility.ListViewColumnSorter requisitionColumnSorter = new Utility.ListViewColumnSorter();
            advanceRequisitionSearchListView.ListViewItemSorter = requisitionColumnSorter;
            Utility.ListViewColumnSorter paidRequisitionColumnSorter = new Utility.ListViewColumnSorter();
            advancePaidRequisitionSearchListView.ListViewItemSorter = paidRequisitionColumnSorter;
            Utility.ListViewColumnSorter expenseColumnSorter = new Utility.ListViewColumnSorter();
            advanceExpenseSearchListView.ListViewItemSorter = expenseColumnSorter;
            Utility.ListViewColumnSorter paidExpenseColumnSorter = new Utility.ListViewColumnSorter();
            advancePaidExpenseSearchListView.ListViewItemSorter = paidExpenseColumnSorter;
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

        private void LoadAdvanceUnpaidRequisitionSearchListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            advanceRequisitionSearchListView.Items.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
                {
                    ListViewItem item = new ListViewItem(serial.ToString());
                    item.SubItems.Add(requisitionVm.RequisitionCategoryName);
                    item.SubItems.Add(requisitionVm.EmployeeName);
                    item.SubItems.Add(requisitionVm.EmployeeDepartmentName);
                    item.SubItems.Add(requisitionVm.RequisitionDate != null
                        ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A");
                    item.SubItems.Add(requisitionVm.FromDate != null
                        ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A");
                    item.SubItems.Add(requisitionVm.ToDate != null
                        ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A");
                    item.SubItems.Add(requisitionVm.AdvanceAmount.ToString("N"));

                    item.Tag = requisitionVm;
                    advanceRequisitionSearchListView.Items.Add(item);
                    serial++;
                }
                requisitionTabPage.Text = @"Requisition (" + requisitionList.Count + @")";
            }
            else
            {
                requisitionTabPage.Text = @"Requisition (0)";
            }
        }

        private void LoadAdvancePaidRequisitionSearchListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            advancePaidRequisitionSearchListView.Items.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
                {
                    ListViewItem item = new ListViewItem(serial.ToString());
                    item.SubItems.Add(requisitionVm.RequisitionCategoryName);
                    item.SubItems.Add(requisitionVm.EmployeeName);
                    item.SubItems.Add(requisitionVm.EmployeeDepartmentName);
                    item.SubItems.Add(requisitionVm.RequisitionDate != null
                        ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A");
                    item.SubItems.Add(requisitionVm.FromDate != null
                        ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A");
                    item.SubItems.Add(requisitionVm.ToDate != null
                        ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A");
                    item.SubItems.Add(requisitionVm.AdvanceAmount.ToString("N"));

                    item.Tag = requisitionVm;
                    advancePaidRequisitionSearchListView.Items.Add(item);
                    serial++;
                }
                paidRequisitionTabPage.Text = @"Paid Requisition (" + requisitionList.Count + @")";
            }
            else
            {
                paidRequisitionTabPage.Text = @"Paid Requisition (0)";
            }
        }

        private void LoadAdvanceExpenseSearchListView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            advanceExpenseSearchListView.Items.Clear();
            if (expenseList != null && expenseList.Any())
            {
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
                {
                    ListViewItem item = new ListViewItem(serial.ToString());
                    item.SubItems.Add(expenseVm.AdvanceCategoryName);
                    item.SubItems.Add(expenseVm.EmployeeName);
                    item.SubItems.Add(expenseVm.EmployeeDepartmentName);
                    item.SubItems.Add(expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy"));
                    item.SubItems.Add(expenseVm.FromDate.ToString("dd-MMM-yyyy"));
                    item.SubItems.Add(expenseVm.ToDate.ToString("dd-MMM-yyyy"));
                    item.SubItems.Add(expenseVm.AdvanceAmount.ToString("N"));
                    item.SubItems.Add(expenseVm.ExpenseAmount.ToString("N"));
                    item.Tag = expenseVm;
                    advanceExpenseSearchListView.Items.Add(item);
                    serial++;
                }
                expenseTabPage.Text = @"Adjustment/Reimbursement (" + expenseList.Count + @")";
            }
            else
            {
                expenseTabPage.Text = @"Adjustment/Reimbursement (0)";
            }
        }

        private void LoadAdvancePaidExpenseSearchListView(ICollection<Advance_VW_GetAdvanceExpense> expenseList)
        {
            advancePaidExpenseSearchListView.Items.Clear();
            if (expenseList != null && expenseList.Any())
            {
                int serial = 1;
                foreach (Advance_VW_GetAdvanceExpense expenseVm in expenseList)
                {
                    ListViewItem item = new ListViewItem(serial.ToString());
                    item.SubItems.Add(expenseVm.AdvanceCategoryName);
                    item.SubItems.Add(expenseVm.EmployeeName);
                    item.SubItems.Add(expenseVm.EmployeeDepartmentName);
                    item.SubItems.Add(expenseVm.ExpenseEntryDate.ToString("dd-MMM-yyyy"));
                    item.SubItems.Add(expenseVm.FromDate.ToString("dd-MMM-yyyy"));
                    item.SubItems.Add(expenseVm.ToDate.ToString("dd-MMM-yyyy"));
                    item.SubItems.Add(expenseVm.AdvanceAmount.ToString("N"));
                    item.SubItems.Add(expenseVm.ExpenseAmount.ToString("N"));
                    item.Tag = expenseVm;
                    advancePaidExpenseSearchListView.Items.Add(item);
                    serial++;
                }
                paidExpenseTabPage.Text = @"Paid Adjustment/Reimbursement (" + expenseList.Count + @")";
            }
            else
            {
                paidExpenseTabPage.Text = @"Paid Adjustment/Reimbursement (0)";
            }
        }

        private void GoToRequisitionApproveUi()
        {
            try
            {
                //if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    if (requisition != null)
                //    {
                //        BaseAdvanceCategory category =
                //            _advanceRequisitionCategoryManager.GetById((long)requisition.AdvanceCategoryId).BaseAdvanceCategory;

                //        if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                //        {
                //            if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                //            {
                //                AdvanceTravelRequisitionHeader header =
                //                    (AdvanceTravelRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionApprovalForLocalTravelUI requisitionApprovalForLocalTravelUi =
                //                    new RequisitionApprovalForLocalTravelUI(header);
                //                requisitionApprovalForLocalTravelUi.ShowDialog();
                //            }
                //            else if (requisition.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                //            {
                //                AdvanceOverseasTravelRequisitionHeader header =
                //                    (AdvanceOverseasTravelRequisitionHeader)
                //                    _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionApprovalForOverseasTravelUI requisitionApprovalForTravelUi =
                //                    new RequisitionApprovalForOverseasTravelUI(header);
                //                requisitionApprovalForTravelUi.ShowDialog();
                //            }
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                //        {
                //            AdvancePettyCashRequisitionHeader header =
                //                (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionApprovalForPettyUI requisitionApprovalForPettyUi =
                //                new RequisitionApprovalForPettyUI(header);
                //            requisitionApprovalForPettyUi.ShowDialog();
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                //        {
                //            AdvanceMiscelleneousRequisitionHeader header =
                //                (AdvanceMiscelleneousRequisitionHeader)
                //                _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionApprovalForMiscellaneousUI requisitionApprovalForMiscellaneousUi =
                //                new RequisitionApprovalForMiscellaneousUI(header);
                //            requisitionApprovalForMiscellaneousUi.ShowDialog();
                //        }

                //        ICollection<Advance_VW_GetAdvanceRequisition> requisitionList =
                //            _approvalProcessManager.GetWaitingForApprovalRequisitionsForMember(_userName);
                //        LoadAdvanceUnpaidRequisitionSearchListView(requisitionList);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select a requisition to approve.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void GoToExpenseApproveUi()
        {
            if (advanceExpenseSearchListView.SelectedItems.Count > 0)
            {
                var expense = advanceExpenseSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                if (expense != null)
                {
                    //BaseAdvanceCategory category =
                    //    _advanceRequisitionCategoryManager.GetById(expense.AdvanceCategoryId).BaseAdvanceCategory;
                    //if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                    //{
                    //    if (expense.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                    //    {
                    //        AdvanceTravelExpenseHeader header =
                    //            (AdvanceTravelExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);
                    //        ExpenseApprovalForLocalTravelUI expenseApprovalForLocalTravelUi =
                    //            new ExpenseApprovalForLocalTravelUI(header);
                    //        expenseApprovalForLocalTravelUi.ShowDialog();
                    //    }
                    //    else if (expense.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                    //    {
                    //        AdvanceOverseasTravelExpenseHeader header =
                    //            (AdvanceOverseasTravelExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);
                    //        ExpenseApprovalForOverseasTravelUI overseasTravelUi =
                    //            new ExpenseApprovalForOverseasTravelUI(header);
                    //        overseasTravelUi.ShowDialog();
                    //    }
                    //}
                    //else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                    //{
                    //    AdvancePettyCashExpenseHeader header =
                    //        (AdvancePettyCashExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);
                    //    ExpenseApprovalForPettyUI expenseApprovalForPettyUi =
                    //        new ExpenseApprovalForPettyUI(header);
                    //    expenseApprovalForPettyUi.ShowDialog();
                    //}
                    //else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                    //{
                    //    AdvanceMiscelleaneousExpenseHeader header =
                    //        (AdvanceMiscelleaneousExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);
                    //    ExpenseApprovalForMiscellaneousUI expenseApprovalForMiscellaneousUi =
                    //        new ExpenseApprovalForMiscellaneousUI(header);
                    //    expenseApprovalForMiscellaneousUi.ShowDialog();
                    //}
                    //ICollection<Advance_VW_GetAdvanceExpense> expenseList =
                    //    _approvalProcessManager.GetWaitingForApprovalExpensesForMember(_userName);
                    //LoadAdvanceExpenseSearchListView(expenseList);
                }
            }
            else
            {
                MessageBox.Show(@"Please select an expense to approve.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void GoToExpense360ViewUi()
        {
            if (advanceExpenseSearchListView.SelectedItems.Count > 0)
            {
                var expense = advanceExpenseSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;

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

        private void GoToPaidExpense360ViewUi()
        {
            if (advancePaidExpenseSearchListView.SelectedItems.Count > 0)
            {
                var expense = advancePaidExpenseSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;

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

        private void GoToRequisition360ViewUi()
        {
            if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
            {
                var requisition =
                    advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
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

        private void GoToPaidRequisition360ViewUi()
        {
            if (advancePaidRequisitionSearchListView.SelectedItems.Count > 0)
            {
                var requisition =
                    advancePaidRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
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

                ICollection<Advance_VW_GetAdvanceRequisition> unPaidRequisitionList =
                    _approvalProcessManager.GetUnPaidRequisitionForMember(criteria);
                LoadAdvanceUnpaidRequisitionSearchListView(unPaidRequisitionList);

                ICollection<Advance_VW_GetAdvanceRequisition> paidRequisitionList =
                    _approvalProcessManager.GetPaidRequisitionForMember(criteria);
                LoadAdvancePaidRequisitionSearchListView(paidRequisitionList);

                ICollection<Advance_VW_GetAdvanceExpense> unPaidExpenseList =
                    _approvalProcessManager.GetUnpaidExpensesForMember(criteria);
                LoadAdvanceExpenseSearchListView(unPaidExpenseList);

                ICollection<Advance_VW_GetAdvanceExpense> paidExpensesList =
                    _approvalProcessManager.GetPaidExpensesForMember(criteria);
                LoadAdvancePaidExpenseSearchListView(paidExpensesList);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ApprovalQueueForPaymentEntryUI_Load(object sender, EventArgs e)
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
            ICollection<Advance_VW_GetAdvanceRequisition> unPaidrequisitionList =
                _approvalProcessManager.GetUnPaidRequisitionForMember();
            LoadAdvanceUnpaidRequisitionSearchListView(unPaidrequisitionList);

            ICollection<Advance_VW_GetAdvanceRequisition> paidRequisitionList =
                _approvalProcessManager.GetPaidRequisitionForMember();
            LoadAdvancePaidRequisitionSearchListView(paidRequisitionList);

            ICollection<Advance_VW_GetAdvanceExpense> unPaidExpenseList =
                _approvalProcessManager.GetUnpaidExpensesForMember();
            LoadAdvanceExpenseSearchListView(unPaidExpenseList);

            ICollection<Advance_VW_GetAdvanceExpense> paidExpensesList =
                _approvalProcessManager.GetPaidExpensesForMember();
            LoadAdvancePaidExpenseSearchListView(paidExpensesList);
        }

        private void view360ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GoToRequisition360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GoToRequisitionApproveUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approveExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GoToExpenseApproveUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void view360ExpenseToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void advanceRequisitionSearchListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
                {
                    requisition360ViewButton.Visible = true;
                    payRequisitionButton.Visible = true;
                }
                else
                {
                    requisition360ViewButton.Visible = false;
                    payRequisitionButton.Visible = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceExpenseSearchListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (advancePaidExpenseSearchListView.SelectedItems.Count > 0)
                {
                    paidExpense360ViewButton.Visible = true;
                    expensePaymentEntryButton.Visible = true;
                }
                else
                {
                    paidExpense360ViewButton.Visible = false;
                    expensePaymentEntryButton.Visible = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionApproveButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToRequisitionApproveUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisition360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToRequisition360ViewUi();
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

        private void expenseApproveButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToExpenseApproveUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void paymentEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                PayAgainstRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void PayAgainstRequisition()
        {
            if (advancePaidRequisitionSearchListView.SelectedItems.Count > 0)
            {
                var selectedRequisition =
                    advancePaidRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (selectedRequisition != null)
                {
                    AdvanceRequisitionHeader header =
                        _advanceRequisitionHeaderManager.GetById(selectedRequisition.HeaderId);
                    //RequisitionVoucherEntryUI requisitionVoucherEntryUi = new RequisitionVoucherEntryUI(header);
                    //requisitionVoucherEntryUi.ShowDialog();
                    LoadPaymentData();
                }
            }
            else
            {
                MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                GoToPaidRequisition360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void paidRequisition360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToPaidRequisition360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

        private void PayAgainstExpense()
        {
            if (advancePaidExpenseSearchListView.SelectedItems.Count > 0)
            {
                var selectedExpense =
                    advancePaidExpenseSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                if (selectedExpense != null)
                {
                    AdvanceExpenseHeader header =
                        _advanceExpenseHeaderManager.GetById(selectedExpense.HeaderId);
                    ExpenseVoucherEntryUI requisitionVoucherUi = new ExpenseVoucherEntryUI(header);
                    requisitionVoucherUi.ShowDialog();
                    LoadPaymentData();
                }
            }
            else
            {
                MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void expensePaymentEntryMetroButton_Click(object sender, EventArgs e)
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

        private void requisitionPaymentEntryMetroButton_Click(object sender, EventArgs e)
        {
            try
            {
                PayAgainstRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void payToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenSelectPaymentDateUiForRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void payRequisitionButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenSelectPaymentDateUiForRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OpenSelectPaymentDateUiForRequisition()
        {
            if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
            {
                var selectedRequisition =
                    advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (selectedRequisition != null)
                {
                    SelectPaymentDateUI selectPaymentDateUi = new SelectPaymentDateUI();
                    selectPaymentDateUi.ShowDialog();
                    if (selectPaymentDateUi.IsPayButtonClicked)
                    {
                        DateTime selectedPaymentDate = selectPaymentDateUi.paymentDateMetroDateTime.Value;
                        bool isPaid = _advanceRequisitionHeaderManager.PayRequisition(selectedRequisition.HeaderId, Session.LoginUserName,
                            selectedPaymentDate);
                        if (isPaid)
                        {
                            MessageBox.Show(@"Payment date insertion successful. Please find it in the 'Paid Requisition' tab.", @"Success", MessageBoxButtons.OK,
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

        private void advancePaidRequisitionSearchListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (advancePaidRequisitionSearchListView.SelectedItems.Count > 0)
                {
                    paidRequisition360ViewButton.Visible = true;
                    requisitionPaymentEntryMetroButton.Visible = true;
                }
                else
                {
                    paidRequisition360ViewButton.Visible = false;
                    requisitionPaymentEntryMetroButton.Visible = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceRequisitionSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                Utility.Utility.SortColumn(e, advanceRequisitionSearchListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceExpenseSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                Utility.Utility.SortColumn(e, advanceExpenseSearchListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancePaidRequisitionSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                Utility.Utility.SortColumn(e, advancePaidRequisitionSearchListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancePaidExpenseSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                Utility.Utility.SortColumn(e, advancePaidExpenseSearchListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
            {
                var selectedExpense =
                    advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
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

        private void expenseEntryToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void advanceRequisitionSearchListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaptionText,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = advanceRequisitionSearchListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, advanceRequisitionSearchListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceRequisitionSearchListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void advanceExpenseSearchListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = advanceExpenseSearchListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, advanceExpenseSearchListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceExpenseSearchListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void advancePaidRequisitionSearchListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = advancePaidRequisitionSearchListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, advancePaidRequisitionSearchListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancePaidRequisitionSearchListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void advancePaidExpenseSearchListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = advancePaidExpenseSearchListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, advancePaidExpenseSearchListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancePaidExpenseSearchListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void advanceRequisitionSearchListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

        private void advanceExpenseSearchListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

        private void advancePaidRequisitionSearchListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

        private void advancePaidExpenseSearchListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

        private void viewRequisitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
                {
                    var requisition =
                        advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                    if (requisition == null)
                    {
                        throw new UiException("Selected requisition has no data.");
                    }
                    RequisitionViewUI requisitionViewUi = new RequisitionViewUI(requisition.HeaderId);
                    requisitionViewUi.Show();
                }
                else
                {
                    MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (advanceExpenseSearchListView.SelectedItems.Count > 0)
                {
                    var expense = advanceExpenseSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;

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
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
