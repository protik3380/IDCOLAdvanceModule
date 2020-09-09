using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.UI._360View;
using Microsoft.Reporting.WinForms;

namespace IDCOLAdvanceModule.UI.Expense
{
    public partial class ExpenseViewUI : Form
    {
        private readonly AdvanceExpenseHeader _expenseHeader;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IAdvance_VW_GetAdvanceExpenseDetailManager _advanceVwGetAdvanceExpenseDetailManager;
        private readonly IAdvance_VW_GetExpenseSourceOfFundManager _advanceVwGetExpenseSourceOfFundManager;
        private readonly IAdvance_VW_GetExpenseSignatoryManager _advanceVwGetExpenseSignatoryManager;

        private ExpenseViewUI()
        {
            InitializeComponent();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _advanceVwGetAdvanceExpenseDetailManager = new AdvanceVwGetAdvanceExpenseDetailManager();
            _advanceVwGetExpenseSourceOfFundManager = new AdvanceVwGetExpenseSourceOfFundManager();
            _advanceVwGetExpenseSignatoryManager = new AdvanceVwGetExpenseSignatoryManager();
        }

        public ExpenseViewUI(long headerId)
            : this()
        {
            _expenseHeader = _advanceExpenseHeaderManager.GetById(headerId);
        }

        private void ExpenseViewUI_Load(object sender, EventArgs e)
        {
            try
            {
                expenseReportViewer.RefreshReport();
                LoadExpenseDetailInReport();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExpenseDetailInReport()
        {
            if (_expenseHeader == null)
            {
                throw new UiException("Expense not found.");
            }
            expenseReportViewer.RefreshReport();
            expenseReportViewer.LocalReport.Refresh();
            expenseReportViewer.LocalReport.DataSources.Clear();
            string reportTitle = "Adjustment/Reimbursement ";
            string reportSource = string.Empty;
            ICollection<Advance_VW_GetAdvanceExpenseDetail> expenseDetails =
                _advanceVwGetAdvanceExpenseDetailManager.GetByHeader(_expenseHeader.Id);
            ICollection<Advance_VW_GetExpenseSourceOfFund> expenseSourceOfFunds =
                _advanceVwGetExpenseSourceOfFundManager.GetByExpense(_expenseHeader.Id);
            ICollection<Advance_VW_GetExpenseSignatory> expenseSignatories =
                _advanceVwGetExpenseSignatoryManager.GetByExpense(_expenseHeader.Id);

            if (_expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
            {
                reportTitle += "(Local Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.LocalTravelExpense.rdlc";
            }
            else if (_expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.OversearTravel)
            {
                reportTitle += "(Overseas Travel)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.OverseasTravelExpense.rdlc";
            }
            else if (_expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.CorporateAdvisory)
            {
                reportTitle += "(Corporate Advisory)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.CorporateAdvisoryExpense.rdlc";
            }
            else if (_expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Event || _expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.TrainingAndWorkshop || _expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Meeting || _expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Procurement || _expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.Others)
            {
                switch (_expenseHeader.AdvanceCategoryId)
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
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.MiscellaneousExpense.rdlc";
            }
            else if (_expenseHeader.AdvanceCategoryId == (long)AdvanceCategoryEnum.PettyCash)
            {
                reportTitle += "(Petty Cash)";
                reportSource = @"IDCOLAdvanceModule.Rdlc.ExpenseView.PettyCashExpense.rdlc";
            }
            ReportDataSource ds = new ReportDataSource("DS_ExpenseDetail", expenseDetails);
            ReportDataSource ds2 = new ReportDataSource("DS_SourceOfFund", expenseSourceOfFunds);
            ReportDataSource ds3 = new ReportDataSource("DS_Signatory", expenseSignatories);
            ReportParameter printDate = new ReportParameter("PrintDate", DateTime.Now.ToString("dd MMM, yyyy hh:mm:ss tt"));
            ReportParameter title = new ReportParameter("Title", reportTitle);
            expenseReportViewer.ProcessingMode = ProcessingMode.Local;
            expenseReportViewer.LocalReport.ReportEmbeddedResource = reportSource;
            expenseReportViewer.LocalReport.DataSources.Add(ds);
            expenseReportViewer.LocalReport.DataSources.Add(ds2);
            expenseReportViewer.LocalReport.DataSources.Add(ds3);
            expenseReportViewer.LocalReport.SetParameters(printDate);
            expenseReportViewer.LocalReport.SetParameters(title);
            expenseReportViewer.LocalReport.Refresh();
            expenseReportViewer.RefreshReport();
        }

        private void viewSupportingFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_expenseHeader == null)
                {
                    throw new UiException("Expense not found.");
                }
                ShowSupportingFilesUI showSupportingFilesUi = new ShowSupportingFilesUI(_expenseHeader.ExpenseFiles);
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
                Expense360ViewUI expense360ViewUi = new Expense360ViewUI(_expenseHeader.Id);
                expense360ViewUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
