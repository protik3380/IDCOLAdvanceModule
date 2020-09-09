using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.UI._360View;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Entry
{
    public partial class RequisitionViewUI : Form
    {
        private readonly AdvanceRequisitionHeader _requisitionHeader;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvance_VW_GetAdvanceRequisitionDetailManager _advanceVwGetAdvanceRequisitionDetailManager;
        private readonly IAdvance_VW_GetRequisitionSourceOfFundManager _advanceVwGetRequisitionSourceOfFundManager;
        private readonly IAdvance_VW_GetRequisitionSignatoryManager _advanceVwGetRequisitionSignatoryManager;

        private RequisitionViewUI()
        {
            InitializeComponent();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceVwGetAdvanceRequisitionDetailManager = new AdvanceVwGetAdvanceRequisitionDetailManager();
            _advanceVwGetRequisitionSourceOfFundManager = new AdvanceVwGetRequisitionSourceOfFundManager();
            _advanceVwGetRequisitionSignatoryManager = new AdvanceVwGetRequisitionSignatoryManager();
        }

        public RequisitionViewUI(long headerId)
            : this()
        {
            _requisitionHeader = _advanceRequisitionHeaderManager.GetById(headerId);
        }

        private void RequisitionViewUI_Load(object sender, EventArgs e)
        {
            try
            {
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

            if (_requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
            {
                reportTitle += "(Local Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.LocalTravelRequisition.rdlc";
            }
            else if (_requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
            {
                reportTitle += "(Overseas Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.OverseasTravelRequisition.rdlc";
            }
            else if (_requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.CorporateAdvisory)
            {
                reportTitle += "(Corporate Advisory)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.RequisitionView.CorporateAdvisoryRequisition.rdlc";
            }
            else if (_requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Event || _requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.TrainingAndWorkshop || _requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Meeting || _requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Procurement || _requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Others)
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
            else if (_requisitionHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.PettyCash)
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

        private void viewSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_requisitionHeader == null)
                {
                    throw new UiException("Requisition not found.");
                }
                ShowSupportingFilesUI showSupportingFilesUi = new ShowSupportingFilesUI(_requisitionHeader.RequisitionFiles);
                showSupportingFilesUi.ShowDialog();
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
                Requisition360ViewUI requisition360ViewUi = new Requisition360ViewUI(_requisitionHeader.Id);
                requisition360ViewUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
