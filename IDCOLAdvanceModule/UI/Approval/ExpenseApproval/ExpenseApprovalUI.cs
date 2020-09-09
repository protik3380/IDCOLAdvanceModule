using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using AutoMapper;
using IDCOLAdvanceModule.BLL;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.UI.Expense;
using IDCOLAdvanceModule.UI._360View;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Approval.ExpenseApproval
{
    public partial class ExpenseApprovalUI : Form
    {
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly ISourceOfFundManager _sourceOfFundManager;
        private readonly IRequisitionSourceOfFundManager _requisitionSourceOfFundManager;
        private readonly IExpenseSourceOfFundManager _expenseSourceOfFundManager;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;
        private readonly IExpenseApprovalTicketManager _expenseApprovalTicketManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IAdvance_VW_GetAdvanceExpenseDetailManager _advanceVwGetAdvanceExpenseDetailManager;
        private readonly IDiluteDesignationManager _diluteDesignationManager;
        private readonly IDesignationManager _designationManager;
        private readonly IAdvance_VW_GetExpenseSourceOfFundManager _advanceVwGetExpenseSourceOfFundManager;
        private readonly IAdvance_VW_GetExpenseSignatoryManager _advanceVwGetExpenseSignatoryManager;

        private AdvanceExpenseHeader _expenseHeader;
        private readonly AdvanceCategoryEnum _advanceCategoryEnum;
        private List<ExpenseSourceOfFund> _expenseSourceOfFunds;
        private List<Advance_VW_GetAdvanceRequisition> _unadjustedRequisitions;
        private bool _isSourceOfFundEntered;
        private bool _isUpdateMode;
        private DataGridViewRow _updateableDetailItem;
        private bool _isSourceOfFundVerified;
        private bool _isSourceOfFundSaved;
        private UserTable _selectedDiluteEmployee;
        private bool _isSourceOfFundPercentageOk;

        public ExpenseApprovalUI()
        {
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _employeeManager = new EmployeeManager();
            _sourceOfFundManager = new SourceOfFundManager();
            _requisitionSourceOfFundManager = new RequisitionSourceOfFundManager();
            _expenseSourceOfFundManager = new ExpenseSourceOfFundManager();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _expenseApprovalTicketManager = new ExpenseApprovalTicketManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _approvalProcessManager = new ApprovalProcessManager();
            _advanceVwGetAdvanceExpenseDetailManager = new AdvanceVwGetAdvanceExpenseDetailManager();
            _diluteDesignationManager = new DiluteDesignationManager();
            _designationManager = new DesignationManager();
            _advanceVwGetExpenseSourceOfFundManager = new AdvanceVwGetExpenseSourceOfFundManager();
            _advanceVwGetExpenseSignatoryManager = new AdvanceVwGetExpenseSignatoryManager();

            _expenseHeader = null;
            _expenseSourceOfFunds = new List<ExpenseSourceOfFund>();
            _unadjustedRequisitions = new List<Advance_VW_GetAdvanceRequisition>();
            _isSourceOfFundEntered = true;
            _isUpdateMode = false;
            _isSourceOfFundVerified = false;
            _isSourceOfFundSaved = true;
            _selectedDiluteEmployee = null;
            _isSourceOfFundPercentageOk = false;
            InitializeComponent();
        }

        public ExpenseApprovalUI(long headerId)
            : this()
        {
            _expenseHeader = _advanceExpenseHeaderManager.GetById(headerId);
            _advanceCategoryEnum = (AdvanceCategoryEnum)_expenseHeader.AdvanceCategoryId;
        }

        private void ExpenseApprovalUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSourceOfFundComboBox();
                LoadExpenseSourceOfFundListView();
                EnableUnadjustedRequisitionButton();
                EnableVerifySouceOfFundGroupBox();
                LoadVerifySouceOfFund();
                EnableSouceOfFundGroupBox();
                SetTitle();
                SetTextOfApproveButton();
                if (_expenseHeader.AdvanceRequisitionHeaders != null && _expenseHeader.AdvanceRequisitionHeaders.Count > 0)
                {
                    viewRequisitionSupportingFilesButton.Visible = true;
                }
                expenseReportViewer.RefreshReport();
                LoadExpenseDetailInReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExpenseDetailInReport()
        {
            if (_expenseHeader == null)
            {
                throw new UiException("Expense not found.");
            }
            expenseReportViewer.RefreshReport();
            expenseReportViewer.LocalReport.Refresh();
            expenseReportViewer.LocalReport.DataSources.Clear();
            string reportTitle = "Adjustment/Reimbursement ";
            string reportSource = string.Empty;
            ICollection<Advance_VW_GetAdvanceExpenseDetail> expenseDetails =
                _advanceVwGetAdvanceExpenseDetailManager.GetByHeader(_expenseHeader.Id);
            ICollection<Advance_VW_GetExpenseSourceOfFund> expenseSourceOfFunds =
                _advanceVwGetExpenseSourceOfFundManager.GetByExpense(_expenseHeader.Id);
            ICollection<Advance_VW_GetExpenseSignatory> expenseSignatories =
                _advanceVwGetExpenseSignatoryManager.GetByExpense(_expenseHeader.Id);

            if (_advanceCategoryEnum == AdvanceCategoryEnum.LocalTravel)
            {
                reportTitle += "(Local Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.LocalTravelExpense.rdlc";
            }
            else if (_advanceCategoryEnum == AdvanceCategoryEnum.OversearTravel)
            {
                reportTitle += "(Overseas Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.OverseasTravelExpense.rdlc";
            }
            else if (_advanceCategoryEnum == AdvanceCategoryEnum.CorporateAdvisory)
            {
                reportTitle += "(Corporate Advisory)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.CorporateAdvisoryExpense.rdlc";
            }
            else if (_advanceCategoryEnum == AdvanceCategoryEnum.Event || _advanceCategoryEnum == AdvanceCategoryEnum.TrainingAndWorkshop || _advanceCategoryEnum == AdvanceCategoryEnum.Meeting || _advanceCategoryEnum == AdvanceCategoryEnum.Procurement || _advanceCategoryEnum == AdvanceCategoryEnum.Others)
            {
                switch (_advanceCategoryEnum)
                {
                    case AdvanceCategoryEnum.Event:
                        reportTitle += "(Event)";
                        break;
                    case AdvanceCategoryEnum.TrainingAndWorkshop:
                        reportTitle += "(Training & Workshop)";
                        break;
                    case AdvanceCategoryEnum.Meeting:
                        reportTitle += "(Meeting)";
                        break;
                    case AdvanceCategoryEnum.Procurement:
                        reportTitle += "(Procurement)";
                        break;
                    case AdvanceCategoryEnum.Others:
                        reportTitle += "(Others)";
                        break;
                }
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.MiscellaneousExpense.rdlc";
            }
            else if (_advanceCategoryEnum == AdvanceCategoryEnum.PettyCash)
            {
                reportTitle += "(Petty Cash)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.PettyCashExpense.rdlc";
            }
            ReportDataSource ds = new ReportDataSource("DS_ExpenseDetail", expenseDetails);
            ReportDataSource ds2 = new ReportDataSource("DS_SourceOfFund", expenseSourceOfFunds);
            ReportDataSource ds3 = new ReportDataSource("DS_Signatory", expenseSignatories);
            ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM, yyyy hh:mm:ss tt"));
            ReportParameter title = new ReportParameter("Title", reportTitle);
            expenseReportViewer.ProcessingMode = ProcessingMode.Local;
            expenseReportViewer.LocalReport.ReportEmbeddedResource = reportSource;
            expenseReportViewer.LocalReport.DataSources.Add(ds);
            expenseReportViewer.LocalReport.DataSources.Add(ds2);
            expenseReportViewer.LocalReport.DataSources.Add(ds3);
            expenseReportViewer.LocalReport.SetParameters(printDate);
            expenseReportViewer.LocalReport.SetParameters(title);
            expenseReportViewer.LocalReport.Refresh();
            expenseReportViewer.RefreshReport();
        }

        private void LoadSourceOfFundComboBox()
        {
            var sourceOfFunds = _sourceOfFundManager.GetAll().ToList();
            sourceOfFundComboBox.DataSource = null;
            sourceOfFunds.Insert(0, new SourceOfFund() { Id = DefaultItem.Value, Name = DefaultItem.Text });

            sourceOfFundComboBox.DisplayMember = "Name";
            sourceOfFundComboBox.ValueMember = "Id";
            sourceOfFundComboBox.DataSource = sourceOfFunds;
        }

        private void LoadExpenseSourceOfFundListView()
        {
            if (_expenseHeader.IsSourceOfEntered)
            {
                LoadExpenseSourceOfFundFromDatabase();
            }
            else
            {
                if (_expenseHeader.AdvanceRequisitionHeaders != null && _expenseHeader.AdvanceRequisitionHeaders.Count > 0)
                {
                    List<RequisitionSourceOfFund> requisitionSourceOfFundList = new List<RequisitionSourceOfFund>();

                    foreach (AdvanceRequisitionHeader header in _expenseHeader.AdvanceRequisitionHeaders)
                    {
                        ICollection<RequisitionSourceOfFund> requisitionSourceOfFunds =
                            _requisitionSourceOfFundManager.GetAllByAdvanceRequisitionHeaderId(header.Id);
                        requisitionSourceOfFundList.AddRange(requisitionSourceOfFunds);
                    }
                    _expenseSourceOfFunds =
                        Mapper.Map<List<RequisitionSourceOfFund>, List<ExpenseSourceOfFund>>(requisitionSourceOfFundList);
                    foreach (ExpenseSourceOfFund expenseSourceOfFund in _expenseSourceOfFunds)
                    {
                        expenseSourceOfFund.AdvanceExpenseHeaderId = _expenseHeader.Id;
                    }
                }
            }

            ReloadExpenseSourceOfFundGridView();
        }
        private void GenerateSerialNumber(DataGridView gridView)
        {
            int serial = 1;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.Cells[2].Value = serial.ToString();
                serial++;
            }
        }
        private void LoadExpenseSourceOfFundFromDatabase()
        {
            _expenseSourceOfFunds = _expenseSourceOfFundManager.GetAllByExpenseHeaderId(_expenseHeader.Id).ToList();
        }

        //private void ReloadExpenseSourceOfFundListView()
        //{
        //    expenseSourceOfFundListView.Items.Clear();
        //    if (_expenseSourceOfFunds != null && _expenseSourceOfFunds.Any())
        //    {
        //        int serial = 1;
        //        foreach (var expenseSourceOfFund in _expenseSourceOfFunds)
        //        {
        //            ListViewItem item = new ListViewItem();
        //            item.Text = serial.ToString();
        //            item.SubItems.Add(expenseSourceOfFund.SourceOfFund.Name);
        //            item.SubItems.Add(expenseSourceOfFund.Percentage.ToString("N"));
        //            item.Tag = expenseSourceOfFund;
        //            expenseSourceOfFundListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void ReloadExpenseSourceOfFundGridView()
        {
            expenseSourceOfFundDataGridView.Rows.Clear();
            if (_expenseSourceOfFunds != null && _expenseSourceOfFunds.Any())
            {
                int serial = 1;
                foreach (var expenseSourceOfFund in _expenseSourceOfFunds)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(expenseSourceOfFundDataGridView);

                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    //row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = expenseSourceOfFund.SourceOfFund.Name;
                    row.Cells[4].Value = expenseSourceOfFund.Percentage.ToString("N");
                    row.Tag = expenseSourceOfFund;
                    expenseSourceOfFundDataGridView.Rows.Add(row);
                    serial++;
                }
                GenerateSerialNumber(expenseSourceOfFundDataGridView);
            }
        }

        private void EnableUnadjustedRequisitionButton()
        {
            _unadjustedRequisitions = _advanceVwGetAdvanceRequisitionManager.GetAllUnadjustedRequisitionsForRequisitionApproval(_expenseHeader.RequesterUserName, _expenseHeader.Id).ToList();
            if (_unadjustedRequisitions.Count > 0)
            {
                unadjustedRequisitionButton.Visible = true;
            }
        }

        private void EnableVerifySouceOfFundGroupBox()
        {
            if (!_expenseHeader.IsSourceOfFundVerified)
            {
                var ticket = _expenseApprovalTicketManager.GetByAdvanceExpenseHeaderIdWithNotApproved(_expenseHeader.Id);
                ApprovalLevel approvalLevel = ticket.ApprovalLevel;
                if (approvalLevel != null)
                {
                    if (approvalLevel.IsSourceOfFundVerify)
                    {
                        verifySourceOfFundGroupBox.Enabled = approvalLevel.IsSourceOfFundVerify;
                        verifySourceOfFundGroupBox.Visible = approvalLevel.IsSourceOfFundVerify;
                    }
                }
            }
        }

        private void LoadVerifySouceOfFund()
        {
            sourceOfFundStatusLabel.Text = _expenseHeader.IsSourceOfFundVerified ? "Verified" : "Not verified";
        }

        private void EnableSouceOfFundGroupBox()
        {
            var ticket = _expenseApprovalTicketManager.GetByAdvanceExpenseHeaderIdWithNotApproved(_expenseHeader.Id);
            ApprovalLevel approvalLevel = ticket.ApprovalLevel;
            if (approvalLevel != null)
            {
                if (approvalLevel.IsSourceOfFundEntry || approvalLevel.IsSourceOfFundVerify)
                {
                    inputSouceOfFundGroupBox.Visible = true;
                    saveSourceOfFundGoupBox.Visible = true;
                    if (approvalLevel.IsSourceOfFundEntry && !_expenseHeader.IsSourceOfEntered)
                    {
                        _isSourceOfFundEntered = false;
                    }
                }
            }
        }

        private void SetTitle()
        {
            var advanceCategory = _expenseHeader.AdvanceCategory;
            if (advanceCategory == null)
            {
                advanceCategory = _advanceRequisitionCategoryManager.GetById(_expenseHeader.AdvanceCategoryId);
            }
            titleLabel.Text += @" (" + advanceCategory.Name + @")";
            Text += @" (" + advanceCategory.Name + @")";
        }

        private void SetTextOfApproveButton()
        {
            var ticket = _expenseApprovalTicketManager.GetByExpenseHeaderId(_expenseHeader.Id);
            var approvalLevel = ticket.ApprovalLevel;
            if (!approvalLevel.IsApprovalAuthority)
            {
                approveButton.Text = @"Verify";
            }
        }

        private void view360Button_Click(object sender, EventArgs e)
        {
            try
            {
                var expense360ViewUi = new Expense360ViewUI(_expenseHeader.Id);
                expense360ViewUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void unadjustedRequisitionButton_Click(object sender, EventArgs e)
        {
            try
            {
                UnadjustedRequisitionUI unadjustedRequisitionUi = new UnadjustedRequisitionUI(_unadjustedRequisitions);
                unadjustedRequisitionUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewExpenseSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_expenseHeader != null)
                {
                    ShowSupportingFilesUI showSupportingFilesUi = new ShowSupportingFilesUI(_expenseHeader.ExpenseFiles);
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

        private void viewRequisitionSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_expenseHeader.AdvanceRequisitionHeaders != null && _expenseHeader.AdvanceRequisitionHeaders.Any())
                {
                    ICollection<RequisitionFile> files = _expenseHeader.AdvanceRequisitionHeaders.SelectMany(c => c.RequisitionFiles).ToList();
                    ShowSupportingFilesUI showSupportingFilesUi = new ShowSupportingFilesUI(files);
                    showSupportingFilesUi.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"Requisition(s) not found.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();
                ExpenseSourceOfFund expenseSourceOfFund = new ExpenseSourceOfFund();
                expenseSourceOfFund.SourceOfFundId = (long)sourceOfFundComboBox.SelectedValue;
                expenseSourceOfFund.SourceOfFund = new SourceOfFund() { Name = sourceOfFundComboBox.Text };
                expenseSourceOfFund.Percentage = Convert.ToDouble(percentageTextBox.Text);
                expenseSourceOfFund.CreatedBy = Session.LoginUserName;
                expenseSourceOfFund.CreatedOn = DateTime.Now;

                if (!_isUpdateMode)
                {
                    IsExpenseSourceOfFundExist(expenseSourceOfFund);
                    _expenseSourceOfFunds.Add(expenseSourceOfFund);
                }
                else
                {
                    if (_updateableDetailItem == null)
                    {
                        throw new UiException("There is no item found to update!");
                    }
                    var item = _updateableDetailItem;
                    var updateableExpenseSourceOfFund = item.Tag as ExpenseSourceOfFund;
                    if (updateableExpenseSourceOfFund == null)
                    {
                        throw new UiException("There is no item tagged with the detail item!");
                    }
                    updateableExpenseSourceOfFund.LastModifiedBy = Session.LoginUserName;
                    updateableExpenseSourceOfFund.LastModifiedOn = DateTime.Now;
                    updateableExpenseSourceOfFund.SourceOfFundId = expenseSourceOfFund.SourceOfFundId;
                    updateableExpenseSourceOfFund.SourceOfFund = expenseSourceOfFund.SourceOfFund;
                    updateableExpenseSourceOfFund.Percentage = expenseSourceOfFund.Percentage;
                    SetChangedGridViewItemByDetail(updateableExpenseSourceOfFund, _updateableDetailItem);
                    _isUpdateMode = false;
                    addButton.Text = @"Add";
                }

                ReloadExpenseSourceOfFundGridView();
                _isSourceOfFundVerified = false;
                ChangeSourceOfFundStatus();
                ClearInputs();
                _isSourceOfFundSaved = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateAdd()
        {
            string errorMessage = string.Empty;
            if ((long)sourceOfFundComboBox.SelectedValue == (long)DefaultItem.Value)
            {
                errorMessage += "Source of fund is not selected." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(percentageTextBox.Text))
            {
                errorMessage += "percentage is not provided." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            else
            {
                return true;
            }
        }

        private bool IsExpenseSourceOfFundExist(ExpenseSourceOfFund expenseSourceOfFund)
        {
            ICollection<ExpenseSourceOfFund> expenseSourceOfFunds = GetExpenseSourceOfFundGridViewItems();
            if (expenseSourceOfFunds.Any(c => c.SourceOfFundId == expenseSourceOfFund.SourceOfFundId))
            {
                string errorMessage = "Source of fund already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }
        //private List<ExpenseSourceOfFund> GetExpenseSourceOfFundListViewItems()
        //{
        //    var requisitionSourceOfFunds = new List<ExpenseSourceOfFund>();
        //    if (expenseSourceOfFundListView.Items.Count > 0)
        //    {
        //        foreach (ListViewItem item in expenseSourceOfFundListView.Items)
        //        {
        //            var requisitionSourceOfFund = item.Tag as ExpenseSourceOfFund;
        //            if (requisitionSourceOfFund != null)
        //            {
        //                requisitionSourceOfFunds.Add(requisitionSourceOfFund);
        //            }
        //        }
        //    }
        //    return requisitionSourceOfFunds;
        //}

        private List<ExpenseSourceOfFund> GetExpenseSourceOfFundGridViewItems()
        {
            var requisitionSourceOfFunds = new List<ExpenseSourceOfFund>();
            if (expenseSourceOfFundDataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in expenseSourceOfFundDataGridView.Rows)
                {
                    var requisitionSourceOfFund = row.Tag as ExpenseSourceOfFund;
                    if (requisitionSourceOfFund != null)
                    {
                        requisitionSourceOfFunds.Add(requisitionSourceOfFund);
                    }
                }
            }
            return requisitionSourceOfFunds;
        }

        //private ListViewItem SetChangedListViewItemByDetail(ExpenseSourceOfFund expenseSourceOfFund, ListViewItem item)
        //{
        //    item.SubItems[1].Text = sourceOfFundComboBox.SelectedText;
        //    item.SubItems[2].Text = expenseSourceOfFund.Percentage.ToString();
        //    item.Tag = expenseSourceOfFund;
        //    return item;
        //}

        private DataGridViewRow SetChangedGridViewItemByDetail(ExpenseSourceOfFund expenseSourceOfFund, DataGridViewRow row)
        {
            row.Cells[3].Value = sourceOfFundComboBox.SelectedText;
            row.Cells[4].Value = expenseSourceOfFund.Percentage.ToString();
            row.Tag = expenseSourceOfFund;
            return row;
        }
        private void ChangeSourceOfFundStatus()
        {
            sourceOfFundStatusLabel.Text = _isSourceOfFundVerified ? "Verified" : "Not Verified";
            verifyFundButton.Text = _isSourceOfFundVerified ? "Unverify Fund?" : "Verify Fund?";
        }

        private void ClearInputs()
        {
            sourceOfFundComboBox.SelectedValue = DefaultItem.Value;
            percentageTextBox.Text = string.Empty;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                addButton.Text = @"Add";
                ClearInputs();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<ExpenseSourceOfFund> expenseSourceOfFunds =
                                GetExpenseSourceOfFundGridViewItems();
                if (expenseSourceOfFunds != null)
                {
                    bool isSaved = false;
                    using (TransactionScope ts = new TransactionScope())
                    {
                        isSaved = _expenseSourceOfFundManager.Insert(expenseSourceOfFunds, _expenseHeader.Id);

                        if (isSaved)
                        {
                            MessageBox.Show(@"Source of fund saved.", @"Success!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            if (!_isSourceOfFundEntered)
                            {
                                LoadExpenseSourceOfFundFromDatabase();
                                ReloadExpenseSourceOfFundGridView();
                                _isSourceOfFundEntered = true;
                                _expenseHeader.IsSourceOfEntered = true;
                                _advanceExpenseHeaderManager.Edit(_expenseHeader);
                                _expenseHeader = _advanceExpenseHeaderManager.GetById(_expenseHeader.Id);
                            }
                            LoadExpenseSourceOfFundListView();
                            _isSourceOfFundSaved = true;

                        }
                        else
                        {
                            MessageBox.Show(@"Source of fund save failed.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                        ts.Complete();
                    }

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void verifyFundButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (verifyFundButton.Text.Equals(@"Verify Fund?"))
                {
                    DialogResult result = MessageBox.Show(@"Are you confirm to verify source of funds?",
                        @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        _expenseHeader.IsSourceOfFundVerified = true;
                        _isSourceOfFundVerified = true;
                        ChangeSourceOfFundStatus();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(@"Are you confirm to un verify source of funds?",
                        @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        _expenseHeader.IsSourceOfFundVerified = false;
                        _isSourceOfFundVerified = false;
                        ChangeSourceOfFundStatus();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dialuteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var designationList = _diluteDesignationManager.GetAll().Select(c => c.DesignationId).ToList();
                List<Admin_Rank> adminRanks = new List<Admin_Rank>();
                foreach (var designationId in designationList)
                {
                    var adminRank = _designationManager.GetById(designationId);
                    adminRanks.Add(adminRank);
                }
                SelectEmployeeUI selectEmployeeUi = new SelectEmployeeUI(adminRanks);
                selectEmployeeUi.ShowDialog();
                _selectedDiluteEmployee = selectEmployeeUi.SelectedEmployee;
                if (_selectedDiluteEmployee != null)
                {
                    diliteMemberLabel.Text = _selectedDiluteEmployee.FullName;
                }
                else
                {
                    dialuteCheckBox.Checked = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void revertBackButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateRevertBack();
                if (_expenseHeader.ExpenseApprovalTickets != null)
                {
                    ExpenseApprovalTicket ticket = _expenseHeader.ExpenseApprovalTickets.FirstOrDefault();
                    if (ticket != null)
                    {
                        ticket.Remarks = remarksTextBox.Text;
                        bool isApproved = _approvalProcessManager.Revert(ticket, Session.LoginUserName);
                        _expenseHeader.IsSourceOfFundVerified = false;
                        bool isRequsitionUpdated = _advanceExpenseHeaderManager.Edit(_expenseHeader);
                        if (isApproved)
                        {
                            MessageBox.Show(@"This request has been reverted successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                            return;
                        }
                        MessageBox.Show(@"This request cannot be reverted now.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateRevertBack()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(remarksTextBox.Text))
            {
                errorMessage += "Remarks is not provided." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }

            return true;
        }

        private void rejectButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(@"Do you want to reject this expense?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
                ValidateReject();
                if (_expenseHeader.ExpenseApprovalTickets != null)
                {
                    ExpenseApprovalTicket ticket = _expenseHeader.ExpenseApprovalTickets.FirstOrDefault();
                    if (ticket != null)
                    {
                        bool isRequsitionUpdated = false;
                        bool isRejected = false;
                        ticket.Remarks = remarksTextBox.Text;
                        isRejected = _approvalProcessManager.Reject(ticket, Session.LoginUserName);
                        if (isRejected)
                        {
                            MessageBox.Show(@"This request has been rejected successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                            return;
                        }
                        MessageBox.Show(@"This request cannot be rejected now.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateReject()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(remarksTextBox.Text))
            {
                errorMessage += "Remarks is not provided." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }

            return true;
        }

        private void approveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_expenseHeader.ExpenseApprovalTickets != null)
                {
                    ExpenseApprovalTicket ticket = _expenseHeader.ExpenseApprovalTickets.FirstOrDefault();
                    if (ticket != null)
                    {
                        ValidateApprove(ticket);

                        if (dialuteCheckBox.Checked && !diliteMemberLabel.Text.Equals("N/A"))
                        {
                            DestinationUserForTicket destinationUserForTicket = new DestinationUserForTicket()
                            {
                                ApprovalTicketId = ticket.Id,
                                ApprovalPanelId = ticket.ApprovalPanelId,
                                DestinationUserName = _selectedDiluteEmployee.UserName
                            };
                            ticket.DestinationUserForTickets.Add(destinationUserForTicket);
                        }
                        ticket.Remarks = string.IsNullOrEmpty(remarksTextBox.Text) ? null : remarksTextBox.Text;
                        bool isApproved = false;
                        ApprovalLevel approvalLevel = ticket.ApprovalLevel;
                        if (approvalLevel != null)
                        {
                            if (approvalLevel.IsSourceOfFundVerify || approvalLevel.IsSourceOfFundEntry)
                            {
                                isApproved = _approvalProcessManager.Approve(ticket, _expenseHeader, Session.LoginUserName);
                            }
                            else
                            {
                                isApproved = _approvalProcessManager.Approve(ticket, Session.LoginUserName);
                            }
                        }

                        if (isApproved)
                        {
                            MessageBox.Show(@"This request has been approved successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                            return;
                        }
                        MessageBox.Show(@"This request cannot be approved now.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateApprove(ExpenseApprovalTicket ticket)
        {
            CalculateSourceOfFundPercentage();
            ApprovalLevel approvalLevel = ticket.ApprovalLevel;
            string errorMessage = string.Empty;

            if (approvalLevel.IsSourceOfFundVerify || approvalLevel.IsSourceOfFundEntry)
            {
                if (!_isSourceOfFundPercentageOk)
                {
                    errorMessage = errorMessage + "Cumulative percentage of all source of funds must be 100%." +
                                   Environment.NewLine;
                }
                if (!_isSourceOfFundSaved || !_isSourceOfFundEntered)
                {
                    errorMessage = errorMessage + "Source of funds are not saved." +
                                   Environment.NewLine;
                }
                if (approvalLevel.IsSourceOfFundVerify && !_isSourceOfFundVerified)
                {
                    errorMessage = errorMessage + "Source of funds is not verified yet." +
                                   Environment.NewLine;
                }
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            else
            {
                return true;
            }
        }

        private void CalculateSourceOfFundPercentage()
        {
            if (expenseSourceOfFundDataGridView.Rows.Count > 0)
            {
                double totalPercentage = GetTotalPercentageAmount();
                if (totalPercentage == 100)
                {
                    _isSourceOfFundPercentageOk = true;
                    return;
                }
            }
            _isSourceOfFundPercentageOk = false;
        }

        private double GetTotalPercentageAmount()
        {
            double totalPercentage = 0;
            foreach (DataGridViewRow row in expenseSourceOfFundDataGridView.Rows)
            {
                var requisitionSourceOfFund = row.Tag as ExpenseSourceOfFund;
                if (requisitionSourceOfFund != null)
                {
                    totalPercentage = totalPercentage + requisitionSourceOfFund.Percentage;
                }
            }
            return totalPercentage;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseSourceOfFundListView.Items.Count > 0)
                //{
                //    var expenseSourceOfFund =
                //        expenseSourceOfFundListView.SelectedItems[0].Tag as ExpenseSourceOfFund;
                //    sourceOfFundComboBox.SelectedValue = expenseSourceOfFund.SourceOfFundId;
                //    percentageTextBox.Text = expenseSourceOfFund.Percentage.ToString();
                //    _updateableDetailItem = expenseSourceOfFundListView.SelectedItems[0];
                //    _isUpdateMode = true;
                //    addButton.Text = @"Update";
                //}
                //else
                //{
                //    MessageBox.Show(@"Please Choose an item to edit", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (expenseSourceOfFundListView.SelectedItems.Count > 0)
                //{
                //    ExpenseSourceOfFund requisitionSourceOfFund = expenseSourceOfFundListView.SelectedItems[0].Tag as ExpenseSourceOfFund;
                //    if (requisitionSourceOfFund != null)
                //    {
                //        var detailItem = expenseSourceOfFundListView.SelectedItems[0].Index;
                //        expenseSourceOfFundListView.Items.RemoveAt(detailItem);
                //        _expenseSourceOfFunds.Remove(requisitionSourceOfFund);
                //        _isSourceOfFundSaved = false;
                //        _isSourceOfFundVerified = false;
                //        ChangeSourceOfFundStatus();
                //    }
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sourceOfFundComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if ((long)sourceOfFundComboBox.SelectedValue != (long)DefaultItem.Value)
                {
                    var getTotalPercentage = GetTotalPercentageAmount();
                    var remainingPercentage = 100 - getTotalPercentage >= 0 ? 100 - getTotalPercentage : 0;
                    percentageTextBox.Text = remainingPercentage.ToString();
                }
                else
                {
                    percentageTextBox.Text = string.Empty;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            try
            {
                var approvalTicket = _expenseHeader.ExpenseApprovalTickets.FirstOrDefault();
                bool isForwarded = _approvalProcessManager.Forward(approvalTicket);

                if (isForwarded)
                {
                    DestinationUserForTicketManager destinationUserForTicketManagerObj = new DestinationUserForTicketManager();
                    var destinationUser =
                        destinationUserForTicketManagerObj.GetBy(approvalTicket.Id, approvalTicket.ApprovalLevelId,
                            approvalTicket.ApprovalPanelId).LastOrDefault();
                    var employee = _employeeManager.GetByUserName(destinationUser.DestinationUserName);
                    MessageBox.Show(@"Selected adjustment has been forwarded to  " + employee.FullName, @"Forwarding completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        @"Sorry, since there is no more next member in this level, you can't forward this adjustment.", @"Forward not possible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void expenseSourceOfFundDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (expenseSourceOfFundDataGridView.Rows.Count > 0)
                        {
                            var expenseSourceOfFund =
                                expenseSourceOfFundDataGridView.SelectedRows[0].Tag as ExpenseSourceOfFund;
                            sourceOfFundComboBox.SelectedValue = expenseSourceOfFund.SourceOfFundId;
                            percentageTextBox.Text = expenseSourceOfFund.Percentage.ToString();
                            _updateableDetailItem = expenseSourceOfFundDataGridView.SelectedRows[0];
                            _isUpdateMode = true;
                            addButton.Text = @"Update";
                        }
                        else
                        {
                            MessageBox.Show(@"Please Choose an item to edit", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        } 
                    }
                    if (e.ColumnIndex == 1)
                    {
                        if (expenseSourceOfFundDataGridView.SelectedRows.Count > 0)
                        {
                            ExpenseSourceOfFund requisitionSourceOfFund = expenseSourceOfFundDataGridView.SelectedRows[0].Tag as ExpenseSourceOfFund;
                            if (requisitionSourceOfFund != null)
                            {
                                var detailItem = expenseSourceOfFundDataGridView.SelectedRows[0].Index;
                                expenseSourceOfFundDataGridView.Rows.RemoveAt(detailItem);
                                _expenseSourceOfFunds.Remove(requisitionSourceOfFund);
                                _isSourceOfFundSaved = false;
                                _isSourceOfFundVerified = false;
                                ChangeSourceOfFundStatus();
                            }
                        } 
                        GenerateSerialNumber(expenseSourceOfFundDataGridView);
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
