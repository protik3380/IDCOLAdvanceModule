using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Utility;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Report
{
    public partial class PendingRequisitionExpenseReportUI : Form
    {
        private readonly IAdvance_VW_GetPendingRequisitionReportManager _advanceVwGetPendingRequisitionReportManager;
        private readonly IAdvance_VW_GetPendingExpenseReportManager _advanceVwGetPendingExpenseReportManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IDepartmentManager _departmentManager;

        public PendingRequisitionExpenseReportUI()
        {
            _advanceVwGetPendingRequisitionReportManager = new AdvanceVwGetPendingRequisitionReportManager();
            _advanceVwGetPendingExpenseReportManager = new AdvanceVwGetPendingExpenseReportManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _departmentManager = new DepartmentManager();
            InitializeComponent();
        }

        private void PendingRequisitionExpenseReportUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadEntryTypeComboBox();
                LoadDepartmentComboBox();
                LoadCategoryComboBox();
                pendingReportViewer.RefreshReport();
                fromDateTimePicker.Checked = false;
                toDateTimePicker.Checked = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadEntryTypeComboBox()
        {
            entryTypeComboBox.DataSource = null;
            entryTypeComboBox.DataSource = Enum.GetNames(typeof(EntryTypeEnum));
        }

        private void LoadDepartmentComboBox()
        {
            departmentComboBox.DataSource = null;
            List<Admin_Departments> departments = new List<Admin_Departments>
            {
                new Admin_Departments { DepartmentID = DefaultItem.Value, DepartmentName = DefaultItem.Text }
            };
            departments.AddRange(_departmentManager.GetAll());
            departmentComboBox.DisplayMember = "DepartmentName";
            departmentComboBox.ValueMember = "DepartmentID";
            departmentComboBox.DataSource = departments;
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

        private void showButton_Click(object sender, EventArgs e)
        {
            try
            {
                ReportSearchCriteria criteria = new ReportSearchCriteria();
                criteria.CategoryId = Convert.ToInt64(categoryComboBox.SelectedValue) != DefaultItem.Value
                    ? (long?)categoryComboBox.SelectedValue
                    : null;
                criteria.DepartmentId = Convert.ToDecimal(departmentComboBox.SelectedValue) != DefaultItem.Value
                    ? (decimal?)departmentComboBox.SelectedValue
                    : null;
                criteria.EmployeeName = !string.IsNullOrEmpty(employeeNameTextBox.Text) ? employeeNameTextBox.Text : null;
                criteria.EmployeeId = !string.IsNullOrEmpty(employeeIdTextBox.Text) ? employeeIdTextBox.Text : null;
                criteria.FromDate = fromDateTimePicker.Checked ? (DateTime?)fromDateTimePicker.Value : null;
                criteria.ToDate = toDateTimePicker.Checked ? (DateTime?)toDateTimePicker.Value : null;
                string category = Convert.ToInt64(categoryComboBox.SelectedValue) != DefaultItem.Value
                    ? categoryComboBox.Text
                    : "N/A";
                string department = Convert.ToInt64(departmentComboBox.SelectedValue) != DefaultItem.Value
                    ? departmentComboBox.Text
                    : "N/A";
                string employeeId = !string.IsNullOrEmpty(employeeIdTextBox.Text)
                    ? employeeIdTextBox.Text
                    : "N/A";
                string employeeName = !string.IsNullOrEmpty(employeeNameTextBox.Text)
                    ? employeeNameTextBox.Text
                    : "N/A";
                string fromDate = criteria.FromDate != null ? criteria.FromDate.Value.ToString("dd MMM, yyyy") : "N/A";
                string toDate = criteria.ToDate != null ? criteria.ToDate.Value.ToString("dd MMM, yyyy") : "N/A";
                ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM, yyyy hh:mm:ss tt"));
                ReportParameter categoryParameter = new ReportParameter("Category", category);
                ReportParameter departmentPaarameter = new ReportParameter("Department", department);
                ReportParameter employeeIdParameter = new ReportParameter("EmployeeId", employeeId);
                ReportParameter employeeNameParameter = new ReportParameter("EmployeeName", employeeName);
                ReportParameter fromDateParameter = new ReportParameter("FromDate", fromDate);
                ReportParameter toDateParameter = new ReportParameter("ToDate", toDate);
                string entryType = entryTypeComboBox.Text;
                pendingReportViewer.RefreshReport();
                pendingReportViewer.LocalReport.Refresh();
                pendingReportViewer.LocalReport.DataSources.Clear();
                if (entryType == EntryTypeEnum.Requisition.ToString())
                {
                    ICollection<Advance_VW_GetPendingRequisitionReport> pendingRequisitionList =
                        _advanceVwGetPendingRequisitionReportManager.GetPendingRequisitionReport(criteria);
                    ReportDataSource ds = new ReportDataSource("DS_PendingRequisitionReport", pendingRequisitionList);
                    pendingReportViewer.ProcessingMode = ProcessingMode.Local;
                    pendingReportViewer.LocalReport.ReportEmbeddedResource = @"IDCOLAdvanceModule.Rdlc.PendingRequisitionReport.rdlc";
                    pendingReportViewer.LocalReport.DataSources.Add(ds);
                    pendingReportViewer.LocalReport.SetParameters(printDate);
                    pendingReportViewer.LocalReport.SetParameters(categoryParameter);
                    pendingReportViewer.LocalReport.SetParameters(departmentPaarameter);
                    pendingReportViewer.LocalReport.SetParameters(employeeIdParameter);
                    pendingReportViewer.LocalReport.SetParameters(employeeNameParameter);
                    pendingReportViewer.LocalReport.SetParameters(fromDateParameter);
                    pendingReportViewer.LocalReport.SetParameters(toDateParameter);
                }
                else if (entryType == EntryTypeEnum.Expense.ToString())
                {
                    ICollection<Advance_VW_GetPendingExpenseReport> pendingExpenseList =
                        _advanceVwGetPendingExpenseReportManager.GetPendingExpenseReport(criteria);
                    ReportDataSource ds = new ReportDataSource("DS_PendingExpenseReport", pendingExpenseList);
                    pendingReportViewer.ProcessingMode = ProcessingMode.Local;
                    pendingReportViewer.LocalReport.ReportEmbeddedResource = @"IDCOLAdvanceModule.Rdlc.PendingExpenseReport.rdlc";
                    pendingReportViewer.LocalReport.DataSources.Add(ds);
                    pendingReportViewer.LocalReport.SetParameters(printDate);
                    pendingReportViewer.LocalReport.SetParameters(categoryParameter);
                    pendingReportViewer.LocalReport.SetParameters(departmentPaarameter);
                    pendingReportViewer.LocalReport.SetParameters(employeeIdParameter);
                    pendingReportViewer.LocalReport.SetParameters(employeeNameParameter);
                    pendingReportViewer.LocalReport.SetParameters(fromDateParameter);
                    pendingReportViewer.LocalReport.SetParameters(toDateParameter);
                }
                pendingReportViewer.LocalReport.Refresh();
                pendingReportViewer.RefreshReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
