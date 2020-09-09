using System;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.UI.Entry;

namespace IDCOLAdvanceModule.UI._360View
{
    public partial class Requisition360ViewUI : Form
    {
        private readonly long _requisitionHeaderId;
        private readonly Requisition360ViewManager _requisition360ViewManager;
        private Requisition360ViewVM _requisition360View;
        private EmployeeManager _employeeManager;
        private HeadOfDepartmentManager _headOfDepartmentManager;
        private readonly BaseAdvanceCategoryEnum _baseAdvanceCategoryEnum;
        private AdvanceCategory _advanceCategory;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;

        public Requisition360ViewUI(long requisitionHeaderId)
        {
            InitializeComponent();
            _requisitionHeaderId = requisitionHeaderId;
            _requisition360ViewManager = new Requisition360ViewManager();
            _employeeManager = new EmployeeManager();
            _headOfDepartmentManager = new HeadOfDepartmentManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
        }

        private void LoadRequisition360ViewInformation()
        {
            try
            {
                _requisition360View = _requisition360ViewManager.GetRequisition360View(_requisitionHeaderId);
                if (_requisition360View == null)
                {
                    throw new Exception("No data found.");
                }
                SetCategoryWiseFormText();
                LoadEmployeeInformation(_requisition360View);
                LoadApprovalStatusInformation(_requisition360View);
                LoadRequisitionTrackingHistory(_requisition360View);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetCategoryWiseFormText()
        {
            string title = string.Empty;
            if (_requisition360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.LocalTravel)
            {
                title = @"360 View for Requisition (Local Travel)";
            }
            else if (_requisition360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.OversearTravel)
            {
                title = @"360 View for Requisition (Overseas Travel)";
            }
            else if (_requisition360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.PettyCash)
            {
                title = @"360 View for Requisition (Petty Cash)";
            }
            else if (_requisition360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.Others)
            {
                title = @"360 View for Requisition (Others Cash)";
            }
            else if (_requisition360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.Event)
            {
                title = @"360 View for Requisition (Event)";
            }
            else if (_requisition360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.TrainingAndWorkshop)
            {
                title = @"360 View for Requisition (Training)";
            }
            else if (_requisition360View.AdvanceCategory.Id == (long)AdvanceCategoryEnum.Meeting)
            {
                title = @"360 View for Requisition (Workshop)";
            }
            Text = title;
        }

        private void LoadEmployeeInformation(Requisition360ViewVM requisitionEmployee)
        {
            try
            {
                requisitionNoLabel.Text = requisitionEmployee.AdvanceRequisitionHeader.RequisitionNo;
                employeeNameLabel.Text = requisitionEmployee.EmployeeName;
                designationLabel.Text = requisitionEmployee.Designation;
                employeeIdLabel.Text = requisitionEmployee.EmployeeId;
                departmentLabel.Text = requisitionEmployee.DepartmentName;
                fromDateLabel.Text = requisitionEmployee.FromDate;
                toDateLabel.Text = requisitionEmployee.ToDate;
                advanceRequisitionAmountLabel.Text = requisitionEmployee.AdvanceRequisitionAmount.ToString("N");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Requisition360ViewUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadRequisition360ViewInformation();
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
                var requisitionHeader = _requisition360View.AdvanceRequisitionHeader;
                RequisitionViewUI requisitionViewUi = new RequisitionViewUI(requisitionHeader.Id);
                requisitionViewUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadApprovalStatusInformation(Requisition360ViewVM requisitionApprovalStatus)
        {
            employeeDataGridView.Rows.Clear();
            requisitionStatusLabel.Text = requisitionApprovalStatus.RequisitionStatus;
            currentApprovalLevelLabel.Text = requisitionApprovalStatus.CurrentApprovalLevel;
            requisitionCategoryLabel.Text = requisitionApprovalStatus.RequisitionCategoryName;

            int serial = 1;
            if (requisitionApprovalStatus.ApprovalLevelMemberList != null &&
                requisitionApprovalStatus.ApprovalLevelMemberList.Any())
            {

                foreach (var member in requisitionApprovalStatus.ApprovalLevelMemberList)
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

        private void LoadRequisitionTrackingHistory(Requisition360ViewVM requisitionTrackingHistory)
        {
            requisitionTrackingHistoryDataGridView.Rows.Clear();
            if (requisitionTrackingHistory.RequisitionApprovalTrackers != null)
            {
                int serial = 1;
                foreach (var requisitionApprovalTracker in requisitionTrackingHistory.RequisitionApprovalTrackers)
                {
                    if (requisitionApprovalTracker.ApprovalStatusId != (long)ApprovalStatusEnum.WaitingForApproval)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(requisitionTrackingHistoryDataGridView);
                        row.Cells[0].Value = serial.ToString();
                        row.Cells[1].Value = requisitionApprovalTracker.TicketNarration;
                        row.Cells[2].Value = requisitionApprovalTracker.Remarks;
                        row.Tag = requisitionApprovalTracker;
                        requisitionTrackingHistoryDataGridView.Rows.Add(row);
                        serial++;
                    }
                }
            }
        }

        private void viewSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_requisitionHeaderId > 0)
                {
                    AdvanceRequisitionHeader requisitionHeader =
                        _advanceRequisitionHeaderManager.GetById(_requisitionHeaderId);
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
    }
}