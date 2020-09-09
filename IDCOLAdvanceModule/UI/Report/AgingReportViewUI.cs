using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IDCOLAdvanceModule.UI.Report
{
    public partial class AgingReportViewUI : Form
    {
        private readonly ICollection<Advance_VW_GetAgingReport> _agingReports;
        private readonly string _selectedReportType;
        public AgingReportViewUI()
        {
            InitializeComponent();
        }

        public AgingReportViewUI(ICollection<Advance_VW_GetAgingReport> agingReports, string selectedReportType)
            : this()
        {
            this._agingReports = agingReports;
            this._selectedReportType = selectedReportType;
        }

        private void LoadReport()
        {
            advanceAgingReportViewer.LocalReport.DataSources.Clear();
            advanceAgingReportViewer.LocalReport.Refresh();

            advanceAgingReportViewer.Visible = true;
            advanceAgingReportViewer.ProcessingMode = ProcessingMode.Local;
            advanceAgingReportViewer.LocalReport.Refresh();
            ReportDataSource ds = new ReportDataSource("DS_AgingReportData", _agingReports);
            ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM, yyyy hh:mm:ss tt"));
            advanceAgingReportViewer.LocalReport.ReportEmbeddedResource = GetReportPath(_selectedReportType);
            advanceAgingReportViewer.LocalReport.DataSources.Add(ds);
            advanceAgingReportViewer.LocalReport.SetParameters(printDate);
            advanceAgingReportViewer.LocalReport.Refresh();
            advanceAgingReportViewer.RefreshReport();
        }

        private string GetReportPath(string selectedReportType)
        {
            string reportEmbeddedSource = @"IDCOLAdvanceModule.Rdlc.AdvanceAgingReport.rdlc";
            if (selectedReportType.Equals("Drildown"))
            {
                reportEmbeddedSource = @"IDCOLAdvanceModule.Rdlc.AdvanceAgingReportDrilDown.rdlc";
            }

            return reportEmbeddedSource;
        }

        private void AgingReportViewUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
