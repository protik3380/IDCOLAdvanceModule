using System;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectRequisitionOrExpenseEntryForUI : Form
    {
        private readonly BaseAdvanceCategoryEnum _baseCategoryEnum;
        private UserTable _employee;
        private readonly IEmployeeManager _employeeManager;
        private readonly EntryTypeEnum _entryTypeEnum;

        private SelectRequisitionOrExpenseEntryForUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
        }

        public SelectRequisitionOrExpenseEntryForUI(BaseAdvanceCategoryEnum baseCategoryEnum, EntryTypeEnum entryTypeEnum)
            : this()
        {
            _baseCategoryEnum = baseCategoryEnum;
            _entryTypeEnum = entryTypeEnum;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (otherRadioButton.Checked)
                {
                    SelectEmployeeUI selectEmployeeUi = new SelectEmployeeUI();
                    selectEmployeeUi.ShowDialog();
                    _employee = selectEmployeeUi.SelectedEmployee;
                }
                else if (ownRadioButton.Checked)
                {
                    string userName = Session.LoginUserName;
                    _employee = _employeeManager.GetByUserName(userName);
                }
                LoadRequisitionEntryForm();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadRequisitionEntryForm()
        {
            try
            {
                if (_employee == null)
                {
                    return;
                }
                if (_baseCategoryEnum == BaseAdvanceCategoryEnum.PettyCash)
                {
                    SelectAdvanceOrExpenseNatureUI selectAdvanceOrExpenseNatureUi = new SelectAdvanceOrExpenseNatureUI(_baseCategoryEnum, _employee, _entryTypeEnum);
                    selectAdvanceOrExpenseNatureUi.ShowDialog();
                    Close();
                }
                else if (_baseCategoryEnum == BaseAdvanceCategoryEnum.Miscellaneous)
                {
                    SelectAdvanceOrExpenseNatureUI selectAdvanceOrExpenseNatureUi = new SelectAdvanceOrExpenseNatureUI(_baseCategoryEnum, _employee, _entryTypeEnum);
                    selectAdvanceOrExpenseNatureUi.ShowDialog();
                    Close();
                }
                else if (_baseCategoryEnum == BaseAdvanceCategoryEnum.Travel)
                {
                    SelectAdvanceOrExpenseNatureUI selectAdvanceOrExpenseNatureUi = new SelectAdvanceOrExpenseNatureUI(_baseCategoryEnum, _employee, _entryTypeEnum);
                    selectAdvanceOrExpenseNatureUi.ShowDialog();
                    Close();
                }
                else if (_baseCategoryEnum == BaseAdvanceCategoryEnum.CorporateAdvisory)
                {
                    SelectAdvanceOrExpenseNatureUI selectAdvanceOrExpenseNatureUi = new SelectAdvanceOrExpenseNatureUI(_baseCategoryEnum, _employee, _entryTypeEnum);
                    selectAdvanceOrExpenseNatureUi.ShowDialog();
                    Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadExpenseEntryForm()
        {
            if (_employee == null)
            {
                return;
            }
            if (_baseCategoryEnum == BaseAdvanceCategoryEnum.PettyCash)
            {
                SelectAdvanceOrExpenseNatureUI selectAdvanceOrExpenseNatureUi =
                    new SelectAdvanceOrExpenseNatureUI(_baseCategoryEnum, _employee, _entryTypeEnum)
                    {
                        Text = @"Select Expense Nature",
                        advanceOrExpenseNatureLabel = { Text = @"Expense nature" }
                    };
                selectAdvanceOrExpenseNatureUi.ShowDialog();
                Close();
            }
            else if (_baseCategoryEnum == BaseAdvanceCategoryEnum.Miscellaneous)
            {
                SelectAdvanceOrExpenseNatureUI selectAdvanceOrExpenseNatureUi =
                    new SelectAdvanceOrExpenseNatureUI(_baseCategoryEnum, _employee, _entryTypeEnum)
                    {
                        Text = @"Select Expense Nature",
                        advanceOrExpenseNatureLabel = { Text = @"Expense nature" }
                    };
                selectAdvanceOrExpenseNatureUi.ShowDialog();
                Close();
            }
            else if (_baseCategoryEnum == BaseAdvanceCategoryEnum.Travel)
            {
                SelectAdvanceOrExpenseNatureUI selectAdvanceOrExpenseNatureUi =
                    new SelectAdvanceOrExpenseNatureUI(_baseCategoryEnum, _employee, _entryTypeEnum)
                    {
                        Text = @"Select Expense Nature",
                        advanceOrExpenseNatureLabel = { Text = @"Expense nature" }
                    };
                selectAdvanceOrExpenseNatureUi.ShowDialog();
                Close();
            }
        }
    }
}
