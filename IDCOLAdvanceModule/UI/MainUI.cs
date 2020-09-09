using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.UI.Report;
using IDCOLAdvanceModule.UI.Settings;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;

namespace IDCOLAdvanceModule.UI
{
    public partial class MainUI : Form
    {
        private bool _isLogoutButtonClicked;

        public MainUI()
        {
            InitializeComponent();
        }

        private void travelMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi =
               new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.Travel, EntryTypeEnum.Requisition)
               {
                   Text = @"Select Requisition For",
                   requisitionOrExpenseForLabel = { Text = @"Requisition For" }
               };
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pettyCashMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi =
                  new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.PettyCash, EntryTypeEnum.Requisition)
                  {
                      Text = @"Select Requisition For",
                      requisitionOrExpenseForLabel = { Text = @"Requisition For" }
                  };
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void miscellaneousMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi =
                  new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.Miscellaneous, EntryTypeEnum.Requisition)
                  {
                      Text = @"Select Requisition For",
                      requisitionOrExpenseForLabel = { Text = @"Requisition For" }
                  };
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceCategorySettingsMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                AdvanceCategorySettingsUI advanceCategorySettingsUi = new AdvanceCategorySettingsUI();
                advanceCategorySettingsUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void employeeCategorySettingsMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeCategorySettingsUI employeeCategorySettingsUi = new EmployeeCategorySettingsUI();
                employeeCategorySettingsUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionSearchMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                RequisitionSearchUI requisitionSearchUi = new RequisitionSearchUI();
                requisitionSearchUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionCategoryWiseCostItemSettingsMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                RequisitionCategoryWiseCostItemSettingsUI requisitionCategoryWiseCostItemSettingsUi = new RequisitionCategoryWiseCostItemSettingsUI();
                requisitionCategoryWiseCostItemSettingsUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void entitlementMappingSettingsMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                EntitlementMappingSettingsUI entitlementMappingSettingsUi = new EntitlementMappingSettingsUI();
                entitlementMappingSettingsUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void travelRequisitionEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi = new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.Travel, EntryTypeEnum.Requisition);
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pettyCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi = new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.PettyCash, EntryTypeEnum.Requisition);
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void miscellaneousRequisitionEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi = new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.Miscellaneous, EntryTypeEnum.Requisition);
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceCategorySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdvanceCategorySettingsUI advanceCategorySettingsUi = new AdvanceCategorySettingsUI();
                advanceCategorySettingsUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void employeeCategorySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeCategorySettingsUI employeeCategorySettingsUi = new EmployeeCategorySettingsUI();
                employeeCategorySettingsUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RequisitionSearchUI requisitionSearchUi = new RequisitionSearchUI();
                requisitionSearchUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvalQueueMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                ApprovalQueueUI requisitionApprovalQueueUi = new ApprovalQueueUI();
                requisitionApprovalQueueUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvalPanelTypeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ApprovalPanelTypeSetupUI approvalPanelTypeSetupUi = new ApprovalPanelTypeSetupUI();
                approvalPanelTypeSetupUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvalPanelSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ApprovalPanelSetupUI approvalPanelSetupUi = new ApprovalPanelSetupUI();
                approvalPanelSetupUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvalLevelSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ApprovalLevelSetupUI approvalLevelSetupUi = new ApprovalLevelSetupUI();
                approvalLevelSetupUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvalLevelWiseMemberSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ApprovalLevelWiseMemberSettingsUI approvalLevelWiseMemberSettingsUi = new ApprovalLevelWiseMemberSettingsUI();
                approvalLevelWiseMemberSettingsUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionCategoryWisePanelSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RequisitionCategoryWisePanelSettingsUI requisitionCategoryWisePanelSettingsUi = new RequisitionCategoryWisePanelSettingsUI();
                requisitionCategoryWisePanelSettingsUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void locationGroupSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LocationGroupSetupUI locationGroupSetup = new LocationGroupSetupUI();
                locationGroupSetup.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void myRequisitionMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                MyRequisitionUI myRequisitionUi = new MyRequisitionUI();
                myRequisitionUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void overseasTravelCostItemSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExecutiveOverseasTravelingAllowanceUI overseasTravelCostItemSettingUi = new ExecutiveOverseasTravelingAllowanceUI();
                overseasTravelCostItemSettingUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void headOfDepartmentSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HeadOfDepartmentSetupUI headOfDepartmentSetupUi = new HeadOfDepartmentSetupUI();
                headOfDepartmentSetupUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _isLogoutButtonClicked = true;
                Session.LoginUserID = 0;
                Session.LoginUserName = "";
                this.Hide();
                List<Form> openForms = new List<Form>();

                foreach (Form f in Application.OpenForms)
                {
                    openForms.Add(f);
                }
                foreach (Form f in openForms)
                {
                    if (f.Name != "LoginUI")
                    {
                        f.Close();
                    }
                }
                LoginUI aUi = new LoginUI();
                aUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MainUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!_isLogoutButtonClicked)
                {
                    Application.Exit();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void travelExpenseMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectExpenseNatureUI selectExpenseNatureUi = new SelectExpenseNatureUI(BaseAdvanceCategoryEnum.Travel);
                selectExpenseNatureUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pettyCashExpenseMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectExpenseNatureUI selectExpenseNatureUi = new SelectExpenseNatureUI(BaseAdvanceCategoryEnum.PettyCash);
                selectExpenseNatureUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void micellaneousExpenseMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectExpenseNatureUI selectExpenseNatureUi = new SelectExpenseNatureUI(BaseAdvanceCategoryEnum.Miscellaneous);
                selectExpenseNatureUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionExpenseSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExpenseSearchUI searchRequisitionExpenseEntryUi = new ExpenseSearchUI();
                searchRequisitionExpenseEntryUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void myExpenseMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                MyExpenseUI myExpense = new MyExpenseUI();
                myExpense.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            try
            {
                loginUserNameLabel.Text = @"You are logged in as " + Session.LoginUserName;
                loginUserNameLabel.Location = new Point(loginUserNameLabel.Location.X + 10, loginUserNameLabel.Location.Y);

                if (loginUserNameLabel.Location.X > this.Width)
                {
                    loginUserNameLabel.Location = new Point(0 - loginUserNameLabel.Width, loginUserNameLabel.Location.Y);
                }
                SetNotificationButtonText();

                notificationToolTip.SetToolTip(notificationButton, "Notification");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetNotificationButtonText()
        {
            ApplicationNotificationManager _applicationNotificationManager = new ApplicationNotificationManager();
            int count = _applicationNotificationManager.GetUnReadNotificationBy(Session.LoginUserName).Count;
            if (count > 0)
            {
                notificationButton.ForeColor = Color.Red;
            }
            else
            {
                notificationButton.ForeColor = Color.Black;
            }
            notificationButton.Text = count.ToString();
        }

        private void sourceOfFundSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SourceOfFundSetupUI sourceOfFundSetupUi = new SourceOfFundSetupUI();
                sourceOfFundSetupUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void rejectedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RejectedRequisitionExpenseReportUI rejectedRequisitionExpenseReportUi = new RejectedRequisitionExpenseReportUI();
                rejectedRequisitionExpenseReportUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void agingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdvanceAgingReportUI advanceAgingReportUi = new AdvanceAgingReportUI();
                advanceAgingReportUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pendingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PendingRequisitionExpenseReportUI pendingRequisitionExpenseReportUi = new PendingRequisitionExpenseReportUI();
                pendingRequisitionExpenseReportUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void summaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SummaryReportUI summaryReportUi = new SummaryReportUI();
                summaryReportUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void reqPaymentEntryMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                RequisitionPaymentQueueUI requisitionPaymentQueueUi = new RequisitionPaymentQueueUI();
                requisitionPaymentQueueUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void diluteDesignationSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DiluteDesignationSetupUI diluteDesignationSetupUi = new DiluteDesignationSetupUI();
                diluteDesignationSetupUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void expenseMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                ExpensePaymentQueueUI paymentQueueUi = new ExpensePaymentQueueUI();
                paymentQueueUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void accountConfigarationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AccountConfigurationSettingsUI configurationSettingsUi = new AccountConfigurationSettingsUI();
                configurationSettingsUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void generalAccountConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GeneralAccountConfigurationSettingsUI configurationSettingsUi = new GeneralAccountConfigurationSettingsUI();
                configurationSettingsUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TimeLagRequisitionReportUI timeLagRequisitionReportUi = new TimeLagRequisitionReportUI();
                timeLagRequisitionReportUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void adjustmentReimbursementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TimeLagExpenseReportUI timeLagExpenseReportUi = new TimeLagExpenseReportUI();
                timeLagExpenseReportUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void notificationButton_Click(object sender, EventArgs e)
        {
            try
            {
                NotificationUI notificationUi = new NotificationUI();
                notificationUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void entitlementMappingSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EntitlementMappingSettingsUI entitlementMappingSettingsUi = new EntitlementMappingSettingsUI();
                entitlementMappingSettingsUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void corporateAdvisoryMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi =
                  new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.CorporateAdvisory, EntryTypeEnum.Requisition)
                  {
                      Text = @"Select Requisition For",
                      requisitionOrExpenseForLabel = { Text = @"Requisition For" }
                  };
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void corporateAdvisoryExpenseMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                SelectExpenseNatureUI selectExpenseNatureUi = new SelectExpenseNatureUI(BaseAdvanceCategoryEnum.CorporateAdvisory);
                selectExpenseNatureUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void corporateAdvisoryRequisitionEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi =
                  new SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum.CorporateAdvisory, EntryTypeEnum.Requisition)
                  {
                      Text = @"Select Requisition For",
                      requisitionOrExpenseForLabel = { Text = @"Requisition For" }
                  };
                selectRequisitionOrExpenseEntryForUi.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void travelReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TravelReportUI travelReportUi = new TravelReportUI();
                travelReportUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void employeeLeaveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeLeaveEntryUI employeeLeave = new EmployeeLeaveEntryUI();
                employeeLeave.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void expenseSearchMetroTile_Click(object sender, EventArgs e)
        {
            try
            {
                ExpenseSearchUI expenseSearchUi = new ExpenseSearchUI();
                expenseSearchUi.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MainUI_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                SetNotificationButtonText();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        private void notificationUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                SetNotificationButtonText();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void notificationToolTip_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
