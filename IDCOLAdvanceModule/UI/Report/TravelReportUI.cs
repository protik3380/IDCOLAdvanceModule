using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule.SP;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Utility;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Report
{
    public partial class TravelReportUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly AdvanceSPManager _advanceSpManager;

        public TravelReportUI()
        {
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _departmentManager = new DepartmentManager();
            _advanceSpManager = new AdvanceSPManager();
            InitializeComponent();
        }

        private void TravelReportUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDepartmentComboBox();
                LoadCategoryComboBox();
                travelReportViewer.RefreshReport();
                fromDateTimePicker.Value = DateTime.Now;
                toDateTimePicker.Value = DateTime.Now;
                fromDateTimePicker.Checked = false;
                toDateTimePicker.Checked = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            List<AdvanceCategory> advanceCategories = new List<AdvanceCategory>();
            advanceCategories.AddRange(_advanceRequisitionCategoryManager.GetBy((long)BaseAdvanceCategoryEnum.Travel));
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
                ReportParameter departmentParameter = new ReportParameter("Department", department);
                ReportParameter employeeIdParameter = new ReportParameter("EmployeeId", employeeId);
                ReportParameter employeeNameParameter = new ReportParameter("EmployeeName", employeeName);
                ReportParameter fromDateParameter = new ReportParameter("FromDate", fromDate);
                ReportParameter toDateParameter = new ReportParameter("ToDate", toDate);
                travelReportViewer.RefreshReport();
                travelReportViewer.LocalReport.Refresh();
                travelReportViewer.LocalReport.DataSources.Clear();
                if (criteria.CategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                {
                    ICollection<Advance_SP_GetLocalTravelReport> reportData =
                        _advanceSpManager.GetLocalTravelReport(criteria);
                    ReportDataSource ds = new ReportDataSource("DS_LocalTravel", reportData);
                    travelReportViewer.ProcessingMode = ProcessingMode.Local;
                    travelReportViewer.LocalReport.ReportEmbeddedResource = @"IDCOLAdvanceModule.Rdlc.LocalTravelReport.rdlc";
                    travelReportViewer.LocalReport.DataSources.Add(ds);
                    travelReportViewer.LocalReport.SetParameters(printDate);
                    travelReportViewer.LocalReport.SetParameters(departmentParameter);
                    travelReportViewer.LocalReport.SetParameters(employeeIdParameter);
                    travelReportViewer.LocalReport.SetParameters(employeeNameParameter);
                    travelReportViewer.LocalReport.SetParameters(fromDateParameter);
                    travelReportViewer.LocalReport.SetParameters(toDateParameter);
                }
                else if (criteria.CategoryId == (long)AdvanceCategoryEnum.OversearTravel)
                {
                    ICollection<Advance_SP_GetOverseasTravelReport> reportData =
                        _advanceSpManager.GetOverseasTravelReport(criteria);
                    ReportDataSource ds = new ReportDataSource("DS_OverseasTravel", reportData);
                    travelReportViewer.ProcessingMode = ProcessingMode.Local;
                    travelReportViewer.LocalReport.ReportEmbeddedResource = @"IDCOLAdvanceModule.Rdlc.OverseasTravelReport.rdlc";
                    travelReportViewer.LocalReport.DataSources.Add(ds);
                    travelReportViewer.LocalReport.SetParameters(printDate);
                    travelReportViewer.LocalReport.SetParameters(departmentParameter);
                    travelReportViewer.LocalReport.SetParameters(employeeIdParameter);
                    travelReportViewer.LocalReport.SetParameters(employeeNameParameter);
                    travelReportViewer.LocalReport.SetParameters(fromDateParameter);
                    travelReportViewer.LocalReport.SetParameters(toDateParameter);
                }
                else
                {
                    throw new UiException("Category not sent properly.");
                }
                travelReportViewer.LocalReport.Refresh();
                travelReportViewer.RefreshReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
