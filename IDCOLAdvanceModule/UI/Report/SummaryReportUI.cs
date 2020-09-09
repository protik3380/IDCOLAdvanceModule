using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.SearchModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace IDCOLAdvanceModule.UI.Report
{
    public partial class SummaryReportUI : Form
    {
        private readonly IAdvanceVwGetAgingReportManager _agingReportManager;

        public SummaryReportUI()
        {
            InitializeComponent();
            _agingReportManager = new AdvanceVwGetAgingReportManager();
        }
        
        private void showButton_Click(object sender, EventArgs e)
        {
            try
            {
                ReportSearchCriteria criteria = new ReportSearchCriteria();
                criteria.FromDate = fromDateTimePicker.Checked ? (DateTime?)fromDateTimePicker.Value : null;
                criteria.ToDate = toDateTimePicker.Checked ? (DateTime?)toDateTimePicker.Value : null;

                var summaryReport = _agingReportManager.GetSummaryReport(criteria);
                summaryReportViewer.LocalReport.DataSources.Clear();
                summaryReportViewer.LocalReport.Refresh();

                summaryReportViewer.Visible = true;
                summaryReportViewer.ProcessingMode = ProcessingMode.Local;
                summaryReportViewer.LocalReport.Refresh();
                ReportDataSource ds = new ReportDataSource("DS_SummaryReport", summaryReport);
                ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM, yyyy hh:mm:ss tt"));
                summaryReportViewer.LocalReport.ReportEmbeddedResource = @"IDCOLAdvanceModule.Rdlc.SummaryReport.rdlc";
                summaryReportViewer.LocalReport.DataSources.Add(ds);
                summaryReportViewer.LocalReport.SetParameters(printDate);
                summaryReportViewer.LocalReport.Refresh();
                summaryReportViewer.RefreshReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
