using System;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectExpenseNatureUI : Form
    {
        private readonly IBaseAdvanceRequisitionCategoryManager _baseAdvanceRequisitionCategoryManager;
        private readonly BaseAdvanceCategoryEnum _baseAdvanceCategoryEnum;
        private readonly BaseAdvanceCategory _baseAdvanceCategory;
        
        private SelectExpenseNatureUI()
        {
            _baseAdvanceRequisitionCategoryManager = new BaseAdvanceRequisitionCategoryManager();
            InitializeComponent();
        }

        public SelectExpenseNatureUI(BaseAdvanceCategoryEnum baseAdvanceCategoryEnum)
            : this()
        {
            _baseAdvanceCategoryEnum = baseAdvanceCategoryEnum;
            BaseAdvanceCategory baseAdvanceCategory = _baseAdvanceRequisitionCategoryManager.GetById((long)baseAdvanceCategoryEnum);
            _baseAdvanceCategory = baseAdvanceCategory;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (adjustmentRadioButton.Checked)
                {
                    SelectRequisitionForExpenseEntryUI selectRequisitionForExpenseEntryUi = new SelectRequisitionForExpenseEntryUI(_baseAdvanceCategory);
                    selectRequisitionForExpenseEntryUi.ShowDialog();
                }
                else if (newExpenseEntryRadioButton.Checked)
                {
                    SelectRequisitionOrExpenseEntryForUI selectRequisitionOrExpenseEntryForUi =
                        new SelectRequisitionOrExpenseEntryForUI(_baseAdvanceCategoryEnum, EntryTypeEnum.Expense)
                        {
                            Text = @"Select adjustment/reibmursement For",
                            requisitionOrExpenseForLabel = { Text = @"Adjustment/reibmursement For" }
                        };
                    selectRequisitionOrExpenseEntryForUi.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
