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
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Utility;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Report
{
    public partial class TimeLagRequisitionReportUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IApprovalPanelManager _approvalPanelManager;
        private readonly IApprovalLevelManager _approvalLevelManager;
        private readonly IAdvance_VW_GetTimeLagForRequisitionManager _advanceVwGetTimeLagForRequisitionManager;

        public TimeLagRequisitionReportUI()
        {
            InitializeComponent();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _approvalPanelManager = new ApprovalPanelManager();
            _approvalLevelManager = new ApprovalLevelManager();
            _advanceVwGetTimeLagForRequisitionManager = new AdvanceVwGetTimeLagForRequisitionManager();
        }

        private void LoadCategoryComboBox()
        {
            categoryComboBox.DataSource = null;
            List<AdvanceCategory> advanceCategories = new List<AdvanceCategory>
            {
                new AdvanceCategory { Id=DefaultItem.Value, Name = DefaultItem.Text }
            };
            advanceCategories.AddRange(_advanceRequisitionCategoryManager.GetAll());
            categoryComboBox.DisplayMember = "Name";
            categoryComboBox.ValueMember = "Id";
            categoryComboBox.DataSource = advanceCategories;
        }

        private void LoadApprovalPanelComboBox()
        {
            panelComboBox.DataSource = null;
            List<ApprovalPanel> approvalPanels = new List<ApprovalPanel>
            {
                new ApprovalPanel { Id = DefaultItem.Value, Name = DefaultItem.Text }
            };
            approvalPanels.AddRange(_approvalPanelManager.GetAll().ToList());
            panelComboBox.DisplayMember = "Name";
            panelComboBox.ValueMember = "Id";
            panelComboBox.DataSource = approvalPanels;
        }

        private void LoadApprovalLevelComboBox(long? panelId = null)
        {
            levelComboBox.DataSource = null;
            List<ApprovalLevel> approvalLevels = new List<ApprovalLevel>
            {
                new ApprovalLevel { Id = DefaultItem.Value, Name = DefaultItem.Text }
            };
            if (panelId != null)
            {
                approvalLevels.AddRange(_approvalLevelManager.GetByPanelId(panelId.Value));
            }
            levelComboBox.DisplayMember = "Name";
            levelComboBox.ValueMember = "Id";
            levelComboBox.DataSource = approvalLevels;
        }

        private void TimeLagRequisitionReportUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategoryComboBox();
                LoadApprovalPanelComboBox();
                LoadApprovalLevelComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            try
            {
                timeLagReportViewer.RefreshReport();
                timeLagReportViewer.LocalReport.Refresh();
                timeLagReportViewer.LocalReport.DataSources.Clear();
                ReportSearchCriteria criteria = new ReportSearchCriteria();
                criteria.CategoryId = Convert.ToInt64(categoryComboBox.SelectedValue) != DefaultItem.Value
                    ? (long?)categoryComboBox.SelectedValue
                    : null;
                criteria.EmployeeName = !string.IsNullOrEmpty(employeeNameTextBox.Text) ? employeeNameTextBox.Text : null;
                criteria.EmployeeId = !string.IsNullOrEmpty(employeeIdTextBox.Text) ? employeeIdTextBox.Text : null;
                criteria.RequisitionNo = !string.IsNullOrEmpty(requisitionNoTextBox.Text) ? requisitionNoTextBox.Text : null;
                criteria.ApprovalPanelId = Convert.ToInt64(panelComboBox.SelectedValue) == DefaultItem.Value ? (long?)null : Convert.ToInt64(panelComboBox.SelectedValue);
                criteria.ApprovalLevelId = Convert.ToInt64(levelComboBox.SelectedValue) == DefaultItem.Value ? (long?)null : Convert.ToInt64(levelComboBox.SelectedValue);

                string category = Convert.ToInt64(categoryComboBox.SelectedValue) != DefaultItem.Value
                    ? categoryComboBox.Text
                    : "N/A";
                string requisitionNo = !string.IsNullOrEmpty(requisitionNoTextBox.Text)
                    ? requisitionNoTextBox.Text
                    : "N/A";
                string employeeId = !string.IsNullOrEmpty(employeeIdTextBox.Text)
                    ? employeeIdTextBox.Text
                    : "N/A";
                string employeeName = !string.IsNullOrEmpty(employeeNameTextBox.Text)
                    ? employeeNameTextBox.Text
                    : "N/A";
                string panelName = Convert.ToInt64(panelComboBox.SelectedValue) != DefaultItem.Value
                    ? panelComboBox.Text
                    : "N/A";
                string levelName = Convert.ToInt64(levelComboBox.SelectedValue) != DefaultItem.Value
                    ? levelComboBox.Text
                    : "N/A";

                ICollection<Advance_VW_GetTimeLagForRequisition> reportData =
                    _advanceVwGetTimeLagForRequisitionManager.GetByCriteria(criteria);
                ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM, yyyy hh:mm:ss tt"));
                ReportParameter categoryParameter = new ReportParameter("Category", category);
                ReportParameter requisitionNoParameter = new ReportParameter("RequisitionNo", requisitionNo);
                ReportParameter employeeIdParameter = new ReportParameter("EmployeeId", employeeId);
                ReportParameter employeeNameParameter = new ReportParameter("EmployeeName", employeeName);
                ReportParameter panelNameParameter = new ReportParameter("PanelName", panelName);
                ReportParameter levelNameParameter = new ReportParameter("LevelName", levelName);
                ReportDataSource ds = new ReportDataSource("DS_TimeLagRequisition", reportData);
                timeLagReportViewer.ProcessingMode = ProcessingMode.Local;
                timeLagReportViewer.LocalReport.ReportEmbeddedResource = @"IDCOLAdvanceModule.Rdlc.TimeLagRequisitionReport.rdlc";
                timeLagReportViewer.LocalReport.DataSources.Add(ds);
                timeLagReportViewer.LocalReport.SetParameters(printDate);
                timeLagReportViewer.LocalReport.SetParameters(categoryParameter);
                timeLagReportViewer.LocalReport.SetParameters(requisitionNoParameter);
                timeLagReportViewer.LocalReport.SetParameters(employeeIdParameter);
                timeLagReportViewer.LocalReport.SetParameters(employeeNameParameter);
                timeLagReportViewer.LocalReport.SetParameters(panelNameParameter);
                timeLagReportViewer.LocalReport.SetParameters(levelNameParameter);
                timeLagReportViewer.LocalReport.Refresh();
                timeLagReportViewer.RefreshReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panelComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                long? panelId = Convert.ToInt64(panelComboBox.SelectedValue) == DefaultItem.Value ? (long?) null : Convert.ToInt64(panelComboBox.SelectedValue);
                LoadApprovalLevelComboBox(panelId);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
