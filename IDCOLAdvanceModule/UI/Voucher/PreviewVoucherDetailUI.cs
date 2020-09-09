using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Accounts;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Voucher
{
    public partial class PreviewVoucherDetailUI : Form
    {
        private TransactionVoucher transactionVoucher;

        public PreviewVoucherDetailUI(TransactionVoucher transactionVoucher)
        {
            InitializeComponent();
            this.transactionVoucher = transactionVoucher ?? new TransactionVoucher();
        }

        private void PreviewVoucherDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.RefreshReport();
                var transactionVouchers = new List<TransactionVoucher>() { transactionVoucher };
                ReportDataSource rds = new ReportDataSource("DS_Voucher", transactionVoucher.TransactionDetails);
                ReportDataSource rds1 = new ReportDataSource("DS_VoucherTransaction", transactionVouchers);
                //ReportParameter p1 = new ReportParameter("FromDate", report.FromDate.Date.ToString());

                //ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM yyyy hh:mm tt"));
                reportViewer1.LocalReport.ReportEmbeddedResource = "IDCOLAdvanceModule.Rdlc.PreviewVoucherReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.DataSources.Add(rds1);

                //reportViewer1.LocalReport.SetParameters(printDate);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
