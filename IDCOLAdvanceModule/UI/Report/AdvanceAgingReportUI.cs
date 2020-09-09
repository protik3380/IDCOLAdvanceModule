using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IDCOLAdvanceModule.UI.Report
{
    public partial class AdvanceAgingReportUI : Form
    {
        private readonly IAdvanceVwGetAgingReportManager _agingReportManager;
        private readonly IAdvance_VW_GetRejectedRequisitionReportManager
            _advanceVwGetRejectedRequisitionReportManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IDepartmentManager _departmentManager;

        public AdvanceAgingReportUI()
        {
            InitializeComponent();
            _agingReportManager = new AdvanceVwGetAgingReportManager();
            _advanceVwGetRejectedRequisitionReportManager = new AdvanceVwGetRejectedRequisitionReportManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _departmentManager = new DepartmentManager();
        }

        private void AdvanceAgingReportUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDepartmentComboBox();
                LoadCategoryComboBox();
                reportTypeComboBox.SelectedIndex = 0;
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
                if (adjustedRadioButton.Checked)
                {
                    criteria.IsAdjusted = true;
                }
                else if (unadjustedRadioButton.Checked)
                {
                    criteria.IsAdjusted = false;
                }
                else
                {
                    criteria.IsAdjusted = null;
                }
                string selectedReportType = reportTypeComboBox.Text;
                var agingReports = _agingReportManager.GetAgingReport(criteria);
                var agingReportViewUi = new AgingReportViewUI(agingReports, selectedReportType);
                agingReportViewUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
