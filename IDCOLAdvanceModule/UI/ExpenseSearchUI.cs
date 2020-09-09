using System.Drawing;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI.Expense;
using IDCOLAdvanceModule.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.UI._360View;

namespace IDCOLAdvanceModule.UI
{
    public partial class ExpenseSearchUI : Form
    {
        private UserTable _employee;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IAdvance_VW_GetAdvanceExpenseManager _advanceVwGetAdvanceExpenseManager;
        private readonly IDesignationManager _designationManager;
        private readonly ICurrencyManager _currencyManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IExpenseApprovalTicketManager _expenseApprovalTicketManager;
        private readonly IEmployeeManager _employeeManager;

        public ExpenseSearchUI()
        {
            InitializeComponent();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceVwGetAdvanceExpenseManager = new AdvanceVwGetAdvanceExpenseManager();
            _designationManager = new DesignationManager();
            _currencyManager = new BLL.MISManager.CurrencyManager();
            _departmentManager = new DepartmentManager();
            _approvalProcessManager = new ApprovalProcessManager();
            _expenseApprovalTicketManager = new ExpenseApprovalTicketManager();
            _employeeManager = new EmployeeManager();
        }

        private void LoadAdvanceCategoryComboBox()
        {
            List<AdvanceCategory> categoryList = _advanceRequisitionCategoryManager.GetAll().ToList();
            categoryList.Insert(0, new AdvanceCategory { Id = DefaultItem.Value, Name = DefaultItem.Text });
            advanceRequisitionCategoryComboBox.DataSource = null;
            advanceRequisitionCategoryComboBox.DisplayMember = "Name";
            advanceRequisitionCategoryComboBox.ValueMember = "Id";
            advanceRequisitionCategoryComboBox.DataSource = categoryList;
        }

        private void LoadDepartmentComboBox()
        {
            var departmentList = _departmentManager.GetAll().ToList();
            if (departmentList.Any())
            {
                departmentList.Insert(0, new Admin_Departments { DepartmentID = DefaultItem.Value, DepartmentName = DefaultItem.Text });
                departmentComboBox.DataSource = null;
                departmentComboBox.DisplayMember = "DepartmentName";
                departmentComboBox.ValueMember = "DepartmentID";
                departmentComboBox.DataSource = departmentList;
            }
        }

        private void LoadDesignationComboBox()
        {
            var designationList = _designationManager.GetAll().ToList();
            designationList.Insert(0, new Admin_Rank { RankID = DefaultItem.Value, RankName = DefaultItem.Text });
            designationComboBox.DataSource = null;
            designationComboBox.DisplayMember = "RankName";
            designationComboBox.ValueMember = "RankID";
            designationComboBox.DataSource = designationList;
        }

        private void LoadCurrencyComboBox()
        {
            List<Solar_CurrencyInfo> currencyList =
                _currencyManager.GetAllForAdvanceRequisitionAndExpense().ToList();
            currencyList.Insert(0, new Solar_CurrencyInfo { CurrencyID = DefaultItem.Value, ShortName = DefaultItem.Text });
            currencyComboBox.DataSource = null;
            currencyComboBox.DisplayMember = "ShortName";
            currencyComboBox.ValueMember = "CurrencyID";
            currencyComboBox.DataSource = currencyList;
        }

        private void LoadDesignationByDepartment()
        {
            if (int.Parse(departmentComboBox.SelectedValue.ToString()) != DefaultItem.Value)
            {
                var selectedDepartmentId = (decimal)departmentComboBox.SelectedValue;
                var designationList = _designationManager.GetByDepartmentId((long)selectedDepartmentId).ToList();
                if (designationList.Any())
                {
                    designationList.Insert(0, new Admin_Rank { RankID = DefaultItem.Value, RankName = DefaultItem.Text });
                    designationComboBox.DataSource = null;
                    designationComboBox.DisplayMember = "RankName";
                    designationComboBox.ValueMember = "RankID";
                    designationComboBox.DataSource = designationList;
                }
            }
            else
            {
                var designationList = new List<Admin_Rank>();
                designationList.Insert(0, new Admin_Rank { RankID = DefaultItem.Value, RankName = DefaultItem.Text });
                designationComboBox.DataSource = null;
                designationComboBox.DisplayMember = "RankName";
                designationComboBox.ValueMember = "RankID";
                designationComboBox.DataSource = designationList;
            }
        }

        private void SearchRequisitionExpenseEntryUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdvanceCategoryComboBox();
                LoadDepartmentComboBox();
                LoadDesignationComboBox();
                LoadCurrencyComboBox();
                if (Session.LoginUserName.ToLower().Equals("admin"))
                {
                    moveToolStripMenuItem.Visible = true;
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
                AdvanceExpenseSearchCriteria criteria = new AdvanceExpenseSearchCriteria();
                if (int.Parse(departmentComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    criteria.DepartmentId = (decimal?)departmentComboBox.SelectedValue;
                }
                if (int.Parse(designationComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    criteria.RankId = (decimal?)designationComboBox.SelectedValue;
                }
                if (_employee != null)
                {
                    criteria.RequesterUserName = _employee.UserName;
                }
                if (int.Parse(advanceRequisitionCategoryComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    criteria.AdvanceCategoryId = Convert.ToInt32(advanceRequisitionCategoryComboBox.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(remarksTextBox.Text))
                {
                    criteria.Remarks = remarksTextBox.Text;
                }
                criteria.CurrencyName = currencyComboBox.Text;
                criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?)null;
                criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?)null;
                criteria.RequesterUserName = _employee != null ? _employee.UserName : null;
                var expensesList = _advanceVwGetAdvanceExpenseManager.GetBySearchCriteria(criteria);
                expenseSearchDataGridView.Rows.Clear();
                if (expensesList != null && expensesList.Any())
                {
                    int serial = 1;
                    foreach (var expense in expensesList)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(expenseSearchDataGridView);
                        row.Cells[0].Value = "360 View";
                        row.Cells[1].Value = "Edit";
                        row.Cells[2].Value = "View";
                        row.Cells[3].Value = "Move";
                        row.Cells[4].Value = serial.ToString();
                        row.Cells[5].Value = expense.ExpenseNo;
                        row.Cells[6].Value = expense.EmployeeName;
                        row.Cells[7].Value = expense.AdvanceCategoryName;
                        row.Cells[8].Value = expense.ExpenseEntryDate.ToString("dd MMM, yyyy");
                        row.Cells[9].Value = expense.ExpenseAmountInBDT.ToString("N");
                        row.Cells[10].Value =expense.AdvanceAmountInBDT.ToString("N");
                        row.Cells[11].Value =
                            GetReimbursementOrRefundAmountInString(
                                (decimal) (expense.ExpenseAmountInBDT - expense.AdvanceAmountInBDT));
                        row.Tag = expense;
                        expenseSearchDataGridView.Rows.Add(row);
                        serial++;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        expenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    if (expense != null)
                //    {
                //        BaseAdvanceCategory category =
                //            _advanceRequisitionCategoryManager.GetById(expense.AdvanceCategoryId).BaseAdvanceCategory;

                //        if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                //        {
                //            if ((long)AdvanceCategoryEnum.LocalTravel == expense.AdvanceCategoryId)
                //            {
                //                AdvanceTravelExpenseHeader header =
                //                    (AdvanceTravelExpenseHeader)
                //                        _advanceExpenseHeaderManager.GetById(expense.HeaderId);

                //                ExpenseEntryForLocalTravelUI expenseEntryForLocalTravelUi = new ExpenseEntryForLocalTravelUI(
                //                    header, AdvancedFormMode.Update);
                //                expenseEntryForLocalTravelUi.Show();
                //            }
                //            else if ((long)AdvanceCategoryEnum.OversearTravel == expense.AdvanceCategoryId)
                //            {
                //                AdvanceOverseasTravelExpenseHeader header =
                //                    (AdvanceOverseasTravelExpenseHeader)
                //                        _advanceExpenseHeaderManager.GetById(expense.HeaderId);

                //                ExpenseEntryForOverseasTravelUI expenseEntryForOverseasTravelUi = new ExpenseEntryForOverseasTravelUI(
                //                    header, AdvancedFormMode.Update);
                //                expenseEntryForOverseasTravelUi.Show();
                //            }
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                //        {
                //            AdvancePettyCashExpenseHeader header =
                //            (AdvancePettyCashExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);

                //            ExpenseEntryForPettyCashUI pettyUi = new ExpenseEntryForPettyCashUI(header, AdvancedFormMode.Update);
                //            pettyUi.Show();
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                //        {
                //            AdvanceMiscelleaneousExpenseHeader header =
                //            (AdvanceMiscelleaneousExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);

                //            ExpenseEntryForMiscellaneousUI miscellaneousUi = new ExpenseEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
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

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        expenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    if (expense != null)
                //    {
                //        ExpenseViewUI expenseViewUi = new ExpenseViewUI(expense.HeaderId);
                //        expenseViewUi.Show();
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void view360ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        expenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    if (expense != null)
                //    {
                //        Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expense.HeaderId);
                //        expense360ViewUi.Show();
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void selectEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedDesignation = designationComboBox.SelectedItem;
                var selectEmployeeUi = new SelectEmployeeUI((Admin_Rank)selectedDesignation);
                selectEmployeeUi.ShowDialog();
                UserTable selectedEmployee = selectEmployeeUi.SelectedEmployee;
                employeeTextBox.Text = selectedEmployee != null ? selectedEmployee.FullName : string.Empty;
                _employee = selectedEmployee;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void departmentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadDesignationByDepartment();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string GetReimbursementOrRefundAmountInString(decimal amount)
        {
            if (amount >= 0)
                return amount.ToString("N");
            return "(" + Math.Abs(amount).ToString("N") + ")";
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseListView.SelectedItems.Count > 0)
                //{
                //    var expense =
                //        expenseListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceExpense;
                //    if (expense == null)
                //    {
                //        throw new UiException("Expense is not tagged in listview.");
                //    }
                //    if (expense.ExpenseApprovalTicketId == null)
                //    {
                //        throw new UiException(
                //            "Sorry! You cannot move this adjustment, because the requester has not sent it for approval yet.");
                //    }
                //    var ticket = _expenseApprovalTicketManager.GetByExpenseHeaderId(expense.HeaderId);
                //    var isMoved = _approvalProcessManager.Move(ticket, Session.LoginUserName);
                //    if (isMoved)
                //    {
                //        DestinationUserForTicketManager destinationUserForTicketManagerObj =
                //            new DestinationUserForTicketManager();
                //        var destinationUser =
                //            destinationUserForTicketManagerObj.GetBy(ticket.Id, ticket.ApprovalLevelId,
                //                ticket.ApprovalPanelId).LastOrDefault();
                //        var employee = _employeeManager.GetByUserName(destinationUser.DestinationUserName);
                //        MessageBox.Show(@"Selected adjustment has been moved to  " + employee.FullName,
                //            @"Movement completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //    else
                //    {
                //        MessageBox.Show(@"Sorry! This adjustment cannot be moved.", @"Warning!", MessageBoxButtons.OK,
                //            MessageBoxIcon.Warning);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an adjustment to move.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void expenseSearchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (expenseSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                expenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            if (expense != null)
                            {
                                Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expense.HeaderId);
                                expense360ViewUi.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        if (expenseSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                expenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            if (expense != null)
                            {
                                BaseAdvanceCategory category =
                                    _advanceRequisitionCategoryManager.GetById(expense.AdvanceCategoryId).BaseAdvanceCategory;

                                if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                                {
                                    if ((long)AdvanceCategoryEnum.LocalTravel == expense.AdvanceCategoryId)
                                    {
                                        AdvanceTravelExpenseHeader header =
                                            (AdvanceTravelExpenseHeader)
                                                _advanceExpenseHeaderManager.GetById(expense.HeaderId);

                                        ExpenseEntryForLocalTravelUI expenseEntryForLocalTravelUi = new ExpenseEntryForLocalTravelUI(
                                            header, AdvancedFormMode.Update);
                                        expenseEntryForLocalTravelUi.Show();
                                    }
                                    else if ((long)AdvanceCategoryEnum.OversearTravel == expense.AdvanceCategoryId)
                                    {
                                        AdvanceOverseasTravelExpenseHeader header =
                                            (AdvanceOverseasTravelExpenseHeader)
                                                _advanceExpenseHeaderManager.GetById(expense.HeaderId);

                                        ExpenseEntryForOverseasTravelUI expenseEntryForOverseasTravelUi = new ExpenseEntryForOverseasTravelUI(
                                            header, AdvancedFormMode.Update);
                                        expenseEntryForOverseasTravelUi.Show();
                                    }
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                                {
                                    AdvancePettyCashExpenseHeader header =
                                    (AdvancePettyCashExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);

                                    ExpenseEntryForPettyCashUI pettyUi = new ExpenseEntryForPettyCashUI(header, AdvancedFormMode.Update);
                                    pettyUi.Show();
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                                {
                                    AdvanceMiscelleaneousExpenseHeader header =
                                    (AdvanceMiscelleaneousExpenseHeader)_advanceExpenseHeaderManager.GetById(expense.HeaderId);

                                    ExpenseEntryForMiscellaneousUI miscellaneousUi = new ExpenseEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
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

                    else if (e.ColumnIndex == 2)
                    {
                        if (expenseSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                expenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            if (expense != null)
                            {
                                ExpenseViewUI expenseViewUi = new ExpenseViewUI(expense.HeaderId);
                                expenseViewUi.Show();
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
                        if (expenseSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var expense =
                                expenseSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceExpense;
                            if (expense == null)
                            {
                                throw new UiException("Expense is not tagged in listview.");
                            }
                            if (expense.ExpenseApprovalTicketId == null)
                            {
                                throw new UiException(
                                    "Sorry! You cannot move this adjustment, because the requester has not sent it for approval yet.");
                            }
                            var ticket = _expenseApprovalTicketManager.GetByExpenseHeaderId(expense.HeaderId);
                            var isMoved = _approvalProcessManager.Move(ticket, Session.LoginUserName);
                            if (isMoved)
                            {
                                DestinationUserForTicketManager destinationUserForTicketManagerObj =
                                    new DestinationUserForTicketManager();
                                var destinationUser =
                                    destinationUserForTicketManagerObj.GetBy(ticket.Id, ticket.ApprovalLevelId,
                                        ticket.ApprovalPanelId).LastOrDefault();
                                var employee = _employeeManager.GetByUserName(destinationUser.DestinationUserName);
                                MessageBox.Show(@"Selected adjustment has been moved to  " + employee.FullName,
                                    @"Movement completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(@"Sorry! This adjustment cannot be moved.", @"Warning!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an adjustment to move.", @"Warning!", MessageBoxButtons.OK,
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
    }
}
