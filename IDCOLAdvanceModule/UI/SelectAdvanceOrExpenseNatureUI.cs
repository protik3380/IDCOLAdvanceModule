using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.UI.Entry;
using IDCOLAdvanceModule.UI.Expense;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectAdvanceOrExpenseNatureUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly BaseAdvanceCategoryEnum _baseAdvanceCategoryEnum;
        private readonly UserTable _employee;
        private readonly EntryTypeEnum _entryTypeEnum;

        private SelectAdvanceOrExpenseNatureUI()
        {
            InitializeComponent();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
        }

        public SelectAdvanceOrExpenseNatureUI(BaseAdvanceCategoryEnum baseAdvanceCategoryEnum, UserTable employee, EntryTypeEnum entryTypeEnum)
            : this()
        {
            _baseAdvanceCategoryEnum = baseAdvanceCategoryEnum;
            _employee = employee;
            _entryTypeEnum = entryTypeEnum;
        }

        private void SelectTravelNatureUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTravelCategoryComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadTravelCategoryComboBox()
        {
            if (_employee.RankID != null)
            {
                ICollection<AdvanceCategory> advanceCategories =
                    _advanceRequisitionCategoryManager.GetBy((long)_baseAdvanceCategoryEnum, _employee.RankID.Value);
                travelNatureComboBox.DataSource = null;
                travelNatureComboBox.DisplayMember = "Name";
                travelNatureComboBox.ValueMember = "Id";
                travelNatureComboBox.DataSource = advanceCategories;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            try
            {
                long advanceCategoryId = Convert.ToInt64(travelNatureComboBox.SelectedValue);
                AdvanceCategory advanceCategory =
                    _advanceRequisitionCategoryManager.GetById(advanceCategoryId);

                if (advanceCategory == null)
                {
                    throw new UiException(
                        "You must select Advance Requisition to go further!, if there is no category displayed, please contact with admin");
                }
                if (_entryTypeEnum == EntryTypeEnum.Requisition)
                {
                    LoadAdvanceRequisitionEntryForm(advanceCategory, advanceCategoryId);
                }
                else if (_entryTypeEnum == EntryTypeEnum.Expense)
                {
                    LoadExpenseEntryForm(advanceCategory);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAdvanceRequisitionEntryForm(AdvanceCategory advanceCategory, long advanceCategoryId)
        {
            try
            {
                if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.PettyCash)
                {
                    RequisitionEntryForPettyUI requisitionEntryForPettyUi =
                        new RequisitionEntryForPettyUI(_employee, advanceCategory);
                    requisitionEntryForPettyUi.ShowDialog();
                    Close();
                }
                else if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.Miscellaneous)
                {
                    RequisitionEntryForMiscellaneousUI requisitionEntryForMiscellaneousUi =
                        new RequisitionEntryForMiscellaneousUI(_employee, advanceCategory);
                    requisitionEntryForMiscellaneousUi.ShowDialog();
                    Close();
                }
                else if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.Travel)
                {
                    if (advanceCategoryId == (long)AdvanceCategoryEnum.LocalTravel)
                    {
                        RequisitionEntryForLocalTravelUI requisitionEntryForLocalTravelUi =
                            new RequisitionEntryForLocalTravelUI(_employee, advanceCategory);
                        requisitionEntryForLocalTravelUi.ShowDialog();
                    }
                    else
                    {
                        RequisitionEntryForOverseasTravelUI requisitionEntryForTravelUi =
                            new RequisitionEntryForOverseasTravelUI(_employee, advanceCategory);
                        requisitionEntryForTravelUi.ShowDialog();
                    }
                    Close();
                }
                else if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.CorporateAdvisory)
                {
                    RequisitionEntryForCorporateAdvisoryUI requisitionEntryForCorporateAdvisoryUi =
                            new RequisitionEntryForCorporateAdvisoryUI(_employee, advanceCategory);
                    requisitionEntryForCorporateAdvisoryUi.ShowDialog();
                    Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExpenseEntryForm(AdvanceCategory advanceCategory)
        {
            try
            {
                if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.PettyCash)
                {
                    ExpenseEntryForPettyCashUI cashRequisitionForPettyUi =
                        new ExpenseEntryForPettyCashUI(_employee, advanceCategory);
                    cashRequisitionForPettyUi.ShowDialog();
                    Close();
                }
                else if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.Miscellaneous)
                {
                    ExpenseEntryForMiscellaneousUI cashRequisitionForMiscellaneousUi =
                            new ExpenseEntryForMiscellaneousUI(_employee, advanceCategory);
                    cashRequisitionForMiscellaneousUi.ShowDialog();
                    Close();
                }
                else if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.Travel)
                {
                    if (advanceCategory.Id == (long)AdvanceCategoryEnum.LocalTravel)
                    {
                        ExpenseEntryForLocalTravelUI requisitionEntryForLocalTravelUi =
                            new ExpenseEntryForLocalTravelUI(_employee, advanceCategory);
                        requisitionEntryForLocalTravelUi.ShowDialog();
                    }
                    else
                    {
                        ExpenseEntryForOverseasTravelUI requisitionEntryForTravelUi =
                            new ExpenseEntryForOverseasTravelUI(_employee, advanceCategory);
                        requisitionEntryForTravelUi.ShowDialog();
                    }
                    Close();
                }
                else if (_baseAdvanceCategoryEnum == BaseAdvanceCategoryEnum.CorporateAdvisory)
                {
                    ExpenseEntryForCorporateAdvisoryUI corporateAdvisoryUi =
                           new ExpenseEntryForCorporateAdvisoryUI(_employee, advanceCategory);
                    corporateAdvisoryUi.ShowDialog();
                    Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
