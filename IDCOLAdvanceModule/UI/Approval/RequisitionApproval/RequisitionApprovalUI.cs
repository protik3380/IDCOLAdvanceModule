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
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.UI.Entry;
using IDCOLAdvanceModule.UI._360View;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Approval.RequisitionApproval
{
    public partial class RequisitionApprovalUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly ISourceOfFundManager _sourceOfFundManager;
        private readonly IRequisitionSourceOfFundManager _requisitionSourceOfFundManager;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IRequisitionApprovalTicketManager _requisitionApprovalTicketManager;
        private readonly IApprovalLevelManager _approvalLevelManager;
        private readonly IAdvance_VW_GetAdvanceRequisitionDetailManager _advanceVwGetAdvanceRequisitionDetailManager;
        private readonly IDiluteDesignationManager _diluteDesignationManager;
        private readonly IDesignationManager _designationManager;
        private readonly IAdvance_VW_GetRequisitionSourceOfFundManager _advanceVwGetRequisitionSourceOfFundManager;
        private readonly IAdvance_VW_GetRequisitionSignatoryManager _advanceVwGetRequisitionSignatoryManager;

        private readonly AdvanceCategoryEnum _advanceCategoryEnum;
        private readonly AdvanceRequisitionHeader _requisitionHeader;
        private List<RequisitionSourceOfFund> _requisitionSourceOfFunds;
        List<Advance_VW_GetAdvanceRequisition> _unadjustedRequisition;
        private bool _isSourceOfFundVerified;
        private bool _isSourceOfFundPercentageOk;
        private bool _isSourceOfFundSaved;
        private UserTable _selectedDiluteEmployee;
        private bool _isUpdateMode;
        private DataGridViewRow _updateableDetailItem;


        public RequisitionApprovalUI()
        {
            _employeeManager = new EmployeeManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _sourceOfFundManager = new SourceOfFundManager();
            _requisitionSourceOfFundManager = new RequisitionSourceOfFundManager();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _approvalProcessManager = new ApprovalProcessManager();
            _requisitionApprovalTicketManager = new RequisitionApprovalTicketManager();
            _approvalLevelManager = new ApprovalLevelManager();
            _advanceVwGetAdvanceRequisitionDetailManager = new AdvanceVwGetAdvanceRequisitionDetailManager();
            _diluteDesignationManager = new DiluteDesignationManager();
            _designationManager = new DesignationManager();
            _advanceVwGetRequisitionSourceOfFundManager = new AdvanceVwGetRequisitionSourceOfFundManager();
            _advanceVwGetRequisitionSignatoryManager = new AdvanceVwGetRequisitionSignatoryManager();

            _requisitionHeader = null;
            _requisitionSourceOfFunds = new List<RequisitionSourceOfFund>();
            _unadjustedRequisition = new List<Advance_VW_GetAdvanceRequisition>();
            _isSourceOfFundVerified = false;
            _isSourceOfFundPercentageOk = false;
            _isSourceOfFundSaved = true;
            _selectedDiluteEmployee = null;
            _isUpdateMode = false;

            InitializeComponent();
        }

        public RequisitionApprovalUI(long headerId)
            : this()
        {
            _requisitionHeader = _advanceRequisitionHeaderManager.GetById(headerId);
            _advanceCategoryEnum = (AdvanceCategoryEnum)_requisitionHeader.AdvanceCategoryId;
        }

        private void RequisitionApprovalUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSourceOfFundComboBox();
                LoadRequisitionSourceOfFundGridView();
                EnableunadjustedRequisitionButton();
                EnableVerifySouceOfFundGroupBox();
                LoadVerifySouceOfFund();
                EnableSouceOfFundGroupBox();
                SetTitle();
                SetTextOfApproveButton();
                requisitionReportViewer.RefreshReport();
                LoadRequisitionDetailInReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRequisitionDetailInReport()
        {
            if (_requisitionHeader == null)
            {
                throw new UiException("Requisition not found.");
            }
            requisitionReportViewer.RefreshReport();
            requisitionReportViewer.LocalReport.Refresh();
            requisitionReportViewer.LocalReport.DataSources.Clear();
            string reportTitle = "Requisition ";
            string reportSource = string.Empty;
            ICollection<Advance_VW_GetAdvanceRequisitionDetail> requisitionWithDetails =
                _advanceVwGetAdvanceRequisitionDetailManager.GetByHeader(_requisitionHeader.Id);
            ICollection<Advance_VW_GetRequisitionSourceOfFund> requisitionSourceOfFunds =
                _advanceVwGetRequisitionSourceOfFundManager.GetByRequisition(_requisitionHeader.Id);
            ICollection<Advance_VW_GetRequisitionSignatory> requisitionSignatories =
                _advanceVwGetRequisitionSignatoryManager.GetByRequisition(_requisitionHeader.Id);

            if (_advanceCategoryEnum == AdvanceCategoryEnum.LocalTravel)
            {
                reportTitle += "(Local Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.LocalTravelRequisition.rdlc";
            }
            if (_advanceCategoryEnum == AdvanceCategoryEnum.OversearTravel)
            {
                reportTitle += "(Overseas Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.OverseasTravelRequisition.rdlc";
            }
            else if (_advanceCategoryEnum == AdvanceCategoryEnum.CorporateAdvisory)
            {
                reportTitle += "(Corporate Advisory)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.CorporateAdvisoryRequisition.rdlc";
            }
            else if (_advanceCategoryEnum == AdvanceCategoryEnum.Event || _advanceCategoryEnum == AdvanceCategoryEnum.TrainingAndWorkshop || _advanceCategoryEnum == AdvanceCategoryEnum.Meeting || _advanceCategoryEnum == AdvanceCategoryEnum.Procurement || _advanceCategoryEnum == AdvanceCategoryEnum.Others)
            {
                switch (_requisitionHeader.AdvanceCategoryId)
                {
                    case (long)AdvanceCategoryEnum.Event:
                        reportTitle += "(Event)";
                        break;
                    case (long)AdvanceCategoryEnum.TrainingAndWorkshop:
                        reportTitle += "(Training & Workshop)";
                        break;
                    case (long)AdvanceCategoryEnum.Meeting:
                        reportTitle += "(Meeting)";
                        break;
                    case (long)AdvanceCategoryEnum.Procurement:
                        reportTitle += "(Procurement)";
                        break;
                    case (long)AdvanceCategoryEnum.Others:
                        reportTitle += "(Others)";
                        break;
                }
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.MiscellaneousRequisition.rdlc";
            }
            else if (_advanceCategoryEnum == AdvanceCategoryEnum.PettyCash)
            {
                reportTitle += "(Petty Cash)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.PettyCashRequisition.rdlc";
            }
            ReportDataSource ds = new ReportDataSource("DS_RequisitionDetail", requisitionWithDetails);
            ReportDataSource ds2 = new ReportDataSource("DS_SourceOfFund", requisitionSourceOfFunds);
            ReportDataSource ds3 = new ReportDataSource("DS_Signatory", requisitionSignatories);
            ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM, yyyy hh:mm:ss tt"));
            ReportParameter title = new ReportParameter("Title", reportTitle);
            requisitionReportViewer.ProcessingMode = ProcessingMode.Local;
            requisitionReportViewer.LocalReport.ReportEmbeddedResource = reportSource;
            requisitionReportViewer.LocalReport.DataSources.Add(ds);
            requisitionReportViewer.LocalReport.DataSources.Add(ds2);
            requisitionReportViewer.LocalReport.DataSources.Add(ds3);
            requisitionReportViewer.LocalReport.SetParameters(printDate);
            requisitionReportViewer.LocalReport.SetParameters(title);
            requisitionReportViewer.LocalReport.Refresh();
            requisitionReportViewer.RefreshReport();
        }

        private void LoadSourceOfFundComboBox()
        {
            var sourceOfFunds = _sourceOfFundManager.GetAll().ToList();
            sourceOfFundComboBox.DataSource = null;
            sourceOfFunds.Insert(0, new SourceOfFund { Id = DefaultItem.Value, Name = DefaultItem.Text });

            sourceOfFundComboBox.DisplayMember = "Name";
            sourceOfFundComboBox.ValueMember = "Id";
            sourceOfFundComboBox.DataSource = sourceOfFunds;
        }

        private void LoadRequisitionSourceOfFundGridView()
        {
            _requisitionSourceOfFunds = _requisitionSourceOfFundManager.GetAllByAdvanceRequisitionHeaderId(_requisitionHeader.Id).ToList();
            ReloadRequisitionSourceOfFundGridView();
        }

        //private void ReloadRequisitionSourceOfFundListView()
        //{
        //    requisitionSourceOfFundListView.Items.Clear();
        //    if (_requisitionSourceOfFunds != null && _requisitionSourceOfFunds.Any())
        //    {
        //        int serial = 1;
        //        foreach (var requisitionSourceOfFund in _requisitionSourceOfFunds)
        //        {
        //            ListViewItem item = new ListViewItem();
        //            item.Text = serial.ToString();
        //            item.SubItems.Add(requisitionSourceOfFund.SourceOfFund.Name);
        //            item.SubItems.Add(requisitionSourceOfFund.Percentage.ToString("N"));
        //            item.Tag = requisitionSourceOfFund;
        //            requisitionSourceOfFundListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void ReloadRequisitionSourceOfFundGridView()
        {
            requisitionSourceOfFundDataGridView.Rows.Clear();
            if (_requisitionSourceOfFunds != null && _requisitionSourceOfFunds.Any())
            {
                int serial = 1;
                foreach (var requisitionSourceOfFund in _requisitionSourceOfFunds)
                {
                    DataGridViewRow row=new DataGridViewRow();
                    row.CreateCells(requisitionSourceOfFundDataGridView);

                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    //row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = requisitionSourceOfFund.SourceOfFund.Name;
                    row.Cells[4].Value = requisitionSourceOfFund.Percentage.ToString("N");
                  
                    row.Tag = requisitionSourceOfFund;
                    requisitionSourceOfFundDataGridView.Rows.Add(row);
                    serial++;
                }
                GenerateSerialNumber(requisitionSourceOfFundDataGridView);
            }
        }

        private void EnableunadjustedRequisitionButton()
        {
            _unadjustedRequisition = _advanceVwGetAdvanceRequisitionManager.GetAllUnadjustedRequisitionsForRequisitionApproval(_requisitionHeader.RequesterUserName, _requisitionHeader.Id).ToList();
            if (_unadjustedRequisition.Count > 0)
            {
                unadjustedRequisitionButton.Visible = true;
            }
        }

        private void EnableVerifySouceOfFundGroupBox()
        {
            if (!_requisitionHeader.IsSourceOfFundVerified)
            {
                var ticket = _requisitionApprovalTicketManager.GetByAdvanceRequisitionHeaderIdWithNotApproved(_requisitionHeader.Id);
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
            sourceOfFundStatusLabel.Text = _requisitionHeader.IsSourceOfFundVerified ? "Verified" : "Not verified";
        }

        private void EnableSouceOfFundGroupBox()
        {
            var ticket = _requisitionApprovalTicketManager.GetByAdvanceRequisitionHeaderIdWithNotApproved(_requisitionHeader.Id);
            ApprovalLevel approvalLevel = ticket.ApprovalLevel;
            if (approvalLevel != null)
            {
                if (approvalLevel.IsSourceOfFundEntry || approvalLevel.IsSourceOfFundVerify)
                {
                    inputSourceOfFundGroupBox.Visible = true;
                    saveSourceOfFundGroupBox.Visible = true;
                }
            }
        }

        private void SetTitle()
        {
            var advanceCategory = _requisitionHeader.AdvanceCategory;
            if (advanceCategory == null)
            {
                advanceCategory = _advanceRequisitionCategoryManager.GetById(_requisitionHeader.AdvanceCategoryId);
            }
            titleLabel.Text += @" (" + advanceCategory.Name + @")";
            Text += @" (" + advanceCategory.Name + @")";
        }

        private void SetTextOfApproveButton()
        {
            var ticket = _requisitionApprovalTicketManager.GetByAdvanceRequisitionHeaderId(_requisitionHeader.Id);
            var approvalLevel = ticket.ApprovalLevel;
            if (!approvalLevel.IsApprovalAuthority)
            {
                approveButton.Text = @"Verify";
            }
        }

        private void viewSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_requisitionHeader != null)
                {
                    AdvanceRequisitionHeader requisitionHeader =
                        _advanceRequisitionHeaderManager.GetById(_requisitionHeader.Id);
                    if (requisitionHeader == null)
                    {
                        throw new UiException("Requisition header not found.");
                    }
                    ShowSupportingFilesUI showSupportingFilesUi = new ShowSupportingFilesUI(requisitionHeader.RequisitionFiles);
                    showSupportingFilesUi.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"Requisition not found.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                UnadjustedRequisitionUI adjustedRequisitionUi = new UnadjustedRequisitionUI(_unadjustedRequisition);
                adjustedRequisitionUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void view360Button_Click(object sender, EventArgs e)
        {
            try
            {
                var requisition360ViewUi = new Requisition360ViewUI(_requisitionHeader.Id);
                requisition360ViewUi.Show();
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
                        _requisitionHeader.IsSourceOfFundVerified = true;
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
                        _requisitionHeader.IsSourceOfFundVerified = false;
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

        private void ChangeSourceOfFundStatus()
        {
            sourceOfFundStatusLabel.Text = _isSourceOfFundVerified ? "Verified" : "Not Verified";
            verifyFundButton.Text = _isSourceOfFundVerified ? "Un verify Fund?" : "Verify Fund?";
        }

        private void dialuteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (dialuteCheckBox.Checked)
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
                else
                {
                    diliteMemberLabel.Text = @"N/A";
                    _selectedDiluteEmployee = null;
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
                if (_requisitionHeader.RequisitionApprovalTickets != null)
                {
                    RequisitionApprovalTicket ticket = _requisitionHeader.RequisitionApprovalTickets.FirstOrDefault();
                    if (ticket != null)
                    {
                        ticket.Remarks = remarksTextBox.Text;
                        bool isApproved, isRequsitionUpdated = false;
                        //using (var ts = new TransactionScope())
                        //{
                        isApproved = _approvalProcessManager.Revert(ticket, Session.LoginUserName);

                        _requisitionHeader.IsSourceOfFundVerified = false;
                        _requisitionHeader.IsFundAvailable = false;
                        isRequsitionUpdated = _advanceRequisitionHeaderManager.Edit(_requisitionHeader);
                        //    ts.Complete();
                        //}
                        if (isApproved)
                        {
                            MessageBox.Show(@"This request has been reverted successfully.", @"Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DialogResult result = MessageBox.Show(@"Do you want to reject this requisition?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
                ValidateReject();
                if (_requisitionHeader.RequisitionApprovalTickets != null)
                {
                    RequisitionApprovalTicket ticket = _requisitionHeader.RequisitionApprovalTickets.FirstOrDefault();
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
                if (_requisitionHeader.RequisitionApprovalTickets != null)
                {
                    RequisitionApprovalTicket ticket = _requisitionHeader.RequisitionApprovalTickets.FirstOrDefault();
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
                        bool isApproved = false;
                        bool isRequisitionSourceOfFundInserted = false;
                        if (!string.IsNullOrEmpty(remarksTextBox.Text))
                        {
                            ticket.Remarks = remarksTextBox.Text;
                        }
                        else
                        {
                            ticket.Remarks = null;
                        }

                        ApprovalLevel approvalLevel = ticket.ApprovalLevel;
                        if (approvalLevel != null)
                        {
                            if (approvalLevel.IsSourceOfFundVerify || approvalLevel.IsSourceOfFundEntry)
                            {
                                isApproved = _approvalProcessManager.Approve(ticket, _requisitionHeader, Session.LoginUserName);
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

        private bool ValidateApprove(RequisitionApprovalTicket ticket)
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
                if (!_isSourceOfFundSaved)
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
            if (requisitionSourceOfFundDataGridView.Rows.Count > 0)
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
            foreach (DataGridViewRow row in requisitionSourceOfFundDataGridView.Rows)
            {
                var requisitionSourceOfFund = row.Tag as RequisitionSourceOfFund;
                if (requisitionSourceOfFund != null)
                {
                    totalPercentage = totalPercentage + requisitionSourceOfFund.Percentage;
                }
            }
            return totalPercentage;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<RequisitionSourceOfFund> requisitionSourceOfFunds = GetRequisitionSourceOfFundGridViewItems();
                bool isRequisitionSourceOfFundInserted = false;
                if (requisitionSourceOfFunds != null)
                {
                    isRequisitionSourceOfFundInserted =
                        _requisitionSourceOfFundManager.Insert(requisitionSourceOfFunds, _requisitionHeader.Id);
                    if (isRequisitionSourceOfFundInserted)
                    {
                        MessageBox.Show(@"Successfully saved.", @"Success!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadRequisitionSourceOfFundGridView();
                        _isSourceOfFundSaved = true;
                    }
                    else
                    {
                        MessageBox.Show(@"Failed to save.", @"Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please add an item to save.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<RequisitionSourceOfFund> GetRequisitionSourceOfFundGridViewItems()
        {
            var requisitionSourceOfFunds = new List<RequisitionSourceOfFund>();
            if (requisitionSourceOfFundDataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in requisitionSourceOfFundDataGridView.Rows)
                {
                    var requisitionSourceOfFund = row.Tag as RequisitionSourceOfFund;
                    if (requisitionSourceOfFund != null)
                    {
                        requisitionSourceOfFunds.Add(requisitionSourceOfFund);
                    }
                }
            }
            return requisitionSourceOfFunds;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();
                RequisitionSourceOfFund requisitionSourceOfFund = new RequisitionSourceOfFund();
                requisitionSourceOfFund.SourceOfFundId = (long)sourceOfFundComboBox.SelectedValue;
                requisitionSourceOfFund.SourceOfFund = new SourceOfFund() { Name = sourceOfFundComboBox.Text };
                requisitionSourceOfFund.Percentage = Convert.ToDouble(percentageTextBox.Text);
                requisitionSourceOfFund.CreatedBy = Session.LoginUserName;
                requisitionSourceOfFund.CreatedOn = DateTime.Now;

                if (!_isUpdateMode)
                {
                    IsRequisitionSourceOfFundExist(requisitionSourceOfFund);
                    _requisitionSourceOfFunds.Add(requisitionSourceOfFund);
                }
                else
                {
                    if (_updateableDetailItem == null)
                    {
                        throw new UiException("There is no item found to update!");
                    }
                    var item = _updateableDetailItem;
                    var updateableRequisitionSourceOfFund = item.Tag as RequisitionSourceOfFund;
                    if (updateableRequisitionSourceOfFund == null)
                    {
                        throw new UiException("There is no item tagged with the detail item!");
                    }
                    updateableRequisitionSourceOfFund.LastModifiedBy = Session.LoginUserName;
                    updateableRequisitionSourceOfFund.LastModifiedOn = DateTime.Now;
                    updateableRequisitionSourceOfFund.SourceOfFundId = requisitionSourceOfFund.SourceOfFundId;
                    updateableRequisitionSourceOfFund.SourceOfFund = requisitionSourceOfFund.SourceOfFund;
                    updateableRequisitionSourceOfFund.Percentage = requisitionSourceOfFund.Percentage;
                    SetChangedGridViewItemByDetail(updateableRequisitionSourceOfFund, _updateableDetailItem);
                    _isUpdateMode = false;
                    addButton.Text = @"Add";
                }

                ReloadRequisitionSourceOfFundGridView();
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
                errorMessage += "Percentage is not provided." + Environment.NewLine;
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

        private bool IsRequisitionSourceOfFundExist(RequisitionSourceOfFund requisitionSourceOfFund)
        {
            ICollection<RequisitionSourceOfFund> requisitionSourceOfFunds = GetRequisitionSourceOfFundGridViewItems();
            if (requisitionSourceOfFunds.Any(c => c.SourceOfFundId == requisitionSourceOfFund.SourceOfFundId))
            {
                string errorMessage = "Source of fund already added.";
                throw new UiException(errorMessage);
            }
            return false;
        }

        //private ListViewItem SetChangedListViewItemByDetail(RequisitionSourceOfFund requisitionSourceOfFund, ListViewItem item)
        //{
        //    item.SubItems[1].Text = sourceOfFundComboBox.SelectedText;
        //    item.SubItems[2].Text = requisitionSourceOfFund.Percentage.ToString("N");
        //    item.Tag = requisitionSourceOfFund;
        //    return item;
        //}
        private DataGridViewRow SetChangedGridViewItemByDetail(RequisitionSourceOfFund requisitionSourceOfFund, DataGridViewRow row)
        {
            row.Cells[3].Value = sourceOfFundComboBox.SelectedText;
            row.Cells[4].Value = requisitionSourceOfFund.Percentage.ToString("N");
            row.Tag = requisitionSourceOfFund;
            return row;
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
                var approvalTicket = _requisitionHeader.RequisitionApprovalTickets.FirstOrDefault();
                bool isForwarded = _approvalProcessManager.Forward(approvalTicket); 

                if (isForwarded)
                {
                    DestinationUserForTicketManager destinationUserForTicketManagerObj = new DestinationUserForTicketManager();
                    var destinationUser =
                        destinationUserForTicketManagerObj.GetBy(approvalTicket.Id, approvalTicket.ApprovalLevelId,
                            approvalTicket.ApprovalPanelId).LastOrDefault();
                    var employee = _employeeManager.GetByUserName(destinationUser.DestinationUserName);
                    MessageBox.Show(@"Selected requisition has been forwarded to  " + employee.FullName, @"Forwarding completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        @"Sorry, since there is no more next member in this level, you can't forward this requisition.", @"Forward not possible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void requisitionSourceOfFundDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (requisitionSourceOfFundDataGridView.Rows.Count > 0)
                        {
                            var requisitionSourceOfFund =
                                requisitionSourceOfFundDataGridView.SelectedRows[0].Tag as RequisitionSourceOfFund;
                            if (requisitionSourceOfFund == null)
                            {
                                throw new UiException("Requisition Source of Fund is not tagged");
                            }
                            sourceOfFundComboBox.SelectedValue = requisitionSourceOfFund.SourceOfFundId;
                            percentageTextBox.Text = requisitionSourceOfFund.Percentage.ToString();
                            _updateableDetailItem = requisitionSourceOfFundDataGridView.SelectedRows[0];
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
                        if (requisitionSourceOfFundDataGridView.SelectedRows.Count > 0)
                        {
                            RequisitionSourceOfFund requisitionSourceOfFund = requisitionSourceOfFundDataGridView.SelectedRows[0].Tag as RequisitionSourceOfFund;
                            if (requisitionSourceOfFund != null)
                            {
                                var detailItem = requisitionSourceOfFundDataGridView.SelectedRows[0].Index;
                                requisitionSourceOfFundDataGridView.Rows.RemoveAt(detailItem);
                                _requisitionSourceOfFunds.Remove(requisitionSourceOfFund);
                                _isSourceOfFundSaved = false;
                                _isSourceOfFundVerified = false;
                                ChangeSourceOfFundStatus();
                            }
                        }
                        GenerateSerialNumber(requisitionSourceOfFundDataGridView);
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
