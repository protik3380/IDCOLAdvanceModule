using System;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.ViewModels;
using IDCOLAdvanceModule.UI.Expense;

namespace IDCOLAdvanceModule.UI._360View
{
    public partial class Expense360ViewUI : Form
    {
        private readonly long _expenseHeaderId;
        private readonly Expense360ViewManager _expense360ViewManager;
        private Expense360ViewVM _expense360View;
        private readonly EmployeeManager _employeeManager;
        private readonly HeadOfDepartmentManager _headOfDepartmentManager;
        private readonly Requisition360ViewManager _requisition360ViewManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;

        private Expense360ViewUI()
        {
            InitializeComponent();
        }

        public Expense360ViewUI(long expenseHeaderId)
            : this()
        {
            _expenseHeaderId = expenseHeaderId;
            _expense360ViewManager = new Expense360ViewManager();
            _employeeManager = new EmployeeManager();
            _headOfDepartmentManager = new HeadOfDepartmentManager();
            _requisition360ViewManager = new Requisition360ViewManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
        }

        private void LoadExpense360ViewInformation()
        {
            _expense360View = _expense360ViewManager.GetExpense360View(_expenseHeaderId);
            if (_expense360View == null)
            {
                throw new Exception("No data found");
            }
            SetCategoryWiseFromText();
            ShowOrHideRequisitionDetailsButton();
            LoadEmployeeInformation(_expense360View);
            LoadApprovalStatusInformation(_expense360View);
            LoadExpenseTrackingHistory(_expense360View);
        }

        private void SetCategoryWiseFromText()
        {
            string title = string.Empty;
            if (_expense360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.LocalTravel)
            {
                title = @"360 View for Expense (Local Travel)";
            }
            else if (_expense360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.OversearTravel)
            {
                title = @"360 View for Expense (Overseas Travel)";
            }
            else if (_expense360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.PettyCash)
            {
                title = @"360 View for Expense (Petty Cash)";
            }
            else if (_expense360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.Others)
            {
                title = @"360 View for Expense (Others)";
            }
            else if (_expense360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.Event)
            {
                title = @"360 View for Expense (Event)";
            }
            else if (_expense360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.TrainingAndWorkshop)
            {
                title = @"360 View for Expense (Training)";
            }
            else if (_expense360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.Meeting)
            {
                title = @"360 View for Expense (Workshop)";
            }
            Text = title;
        }

        private void ShowOrHideRequisitionDetailsButton()
        {
            bool headerExists = _expense360View.AdvanceRequisitionHeaders != null;
            if (_expense360View.AdvanceRequisitionHeaders != null)
            {
                headerExists = _expense360View.AdvanceRequisitionHeaders.Count != 0;
            }
            showRequisitionButton.Visible = headerExists;
        }

        private void LoadEmployeeInformation(Expense360ViewVM expense360View)
        {
            expenseNoLabel.Text = expense360View.AdvanceExpenseHeader.ExpenseNo;
            employeeNameLabel.Text = expense360View.EmployeeName;
            designationLabel.Text = expense360View.Designation;
            employeeIdLabel.Text = expense360View.EmployeeId;
            departmentLabel.Text = expense360View.DepartmentName;
            fromDateLabel.Text = expense360View.FromDate;
            toDateLabel.Text = expense360View.ToDate;
            advanceRequisitionAmountLabel.Text = expense360View.AdvanceRequisitionAmount.ToString("N");
            expenseAmountLabel.Text = expense360View.ExpenseTotalAmount.ToString("N");
            reimburseOrRefundAmountLabel.Text = expense360View.ReimburseOrRefundAmountText;
        }

        private void Expense360ViewUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadExpense360ViewInformation();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showDetailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                var expenseHeader = _expense360View.AdvanceExpenseHeader;
                ExpenseViewUI expenseViewUi = new ExpenseViewUI(expenseHeader.Id);
                expenseViewUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadApprovalStatusInformation(Expense360ViewVM expense360Vm)
        {
            employeeDataGridView.Rows.Clear();
            requisitionStatusLabel.Text = expense360Vm.ExpenseStatus;
            currentApprovalLevelLabel.Text = expense360Vm.CurrentApprovalLevel;

            int serial = 1;
            if (expense360Vm.ApprovalLevelMemberList != null && expense360Vm.ApprovalLevelMemberList.Any())
            {
                foreach (var member in expense360Vm.ApprovalLevelMemberList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(employeeDataGridView);
                    row.Cells[0].Value = serial.ToString();
                    row.Cells[1].Value = member.EmployeeID;
                    row.Cells[2].Value = member.EmployeeFullName;
                    row.Cells[3].Value = member.RankName;
                    row.Tag = member;
                    employeeDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void LoadExpenseTrackingHistory(Expense360ViewVM expense360View)
        {
            expenseTrackingHistoryDataGridView.Rows.Clear();
            if (expense360View.ExpenseApprovalTrackers != null)
            {
                int serial = 1;
                foreach (var expenseTracker in expense360View.ExpenseApprovalTrackers)
                {
                    if (expenseTracker.ApprovalStatusId != (long)ApprovalStatusEnum.WaitingForApproval)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(expenseTrackingHistoryDataGridView);
                        row.Cells[0].Value = serial.ToString();
                        row.Cells[1].Value = expenseTracker.TicketNarration;
                        row.Cells[2].Value = expenseTracker.Remarks;
                        row.Tag = expenseTracker;
                        expenseTrackingHistoryDataGridView.Rows.Add(row);
                        serial++;
                    }
                }
            }
        }

        private void showRequisitionButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requisitions = _expense360View.AdvanceRequisitionHeaders;

                if (requisitions != null && requisitions.Any())
                {
                    if (requisitions.Count == 1)
                    {
                        var requisitionId = requisitions.FirstOrDefault().Id;
                        Requisition360ViewUI requisition360ViewUi = new Requisition360ViewUI(requisitionId);
                        requisition360ViewUi.ShowDialog();
                    }
                    else if (requisitions.Count > 0)
                    {
                        RequisitionListUI requisitionListUi = new RequisitionListUI(requisitions);
                        requisitionListUi.ShowDialog();
                    }
                }
                else
                {
                    throw new UiException("No requisition found against this expense.");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_expenseHeaderId > 0)
                {
                    AdvanceExpenseHeader expenseHeader =
                        _advanceExpenseHeaderManager.GetById(_expenseHeaderId);
                    if (expenseHeader == null)
                    {
                        throw new UiException("Expense header not found.");
                    }
                    ShowSupportingFilesUI showSupportingFilesUi = new ShowSupportingFilesUI(expenseHeader.ExpenseFiles);
                    showSupportingFilesUi.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"Expense not found.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
