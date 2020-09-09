using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class AdvanceCategorySettingsUI : Form
    {
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private ICollection<AdvanceCategory> _advanceCategories;

        public AdvanceCategorySettingsUI()
        {
            InitializeComponent();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
        }

        private void LoadAllAdvanceCategories()
        {
            _advanceCategories = _advanceRequisitionCategoryManager.GetAll();
        }

        private void LoadAdvanceCategoryComboBox()
        {
            advanceCategoryComboBox.DataSource = null;
            advanceCategoryComboBox.DisplayMember = "Name";
            advanceCategoryComboBox.ValueMember = "Id";
            advanceCategoryComboBox.DataSource = _advanceCategories;
        }

        private void LoadAdvanceCategoriesGridView()
        {
            advancedCategoryDataGridView.Rows.Clear();
            foreach (AdvanceCategory advanceCategory in _advanceCategories)
            {
                DataGridViewRow row = new DataGridViewRow();
                 row.CreateCells(advancedCategoryDataGridView);  
                row.Cells[0].Value = (advanceCategory.Name);
                if (advanceCategory.IsCeilingApplicable)
                {
                    row.Cells[1].Value = (advanceCategory.CeilingAmount.ToString());
                }
                else
                {
                    row.Cells[1].Value = ("N/A");
                }

                advancedCategoryDataGridView.Rows.Add(row);
            }
        }

        private void AdvanceCategorySettingsUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllAdvanceCategories();
                LoadAdvanceCategoryComboBox();
                LoadAdvanceCategoriesGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ceilingApplicableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ceilingAmountTextBox.Enabled = ceilingApplicableCheckBox.Checked;
                saveButton.Enabled = ceilingApplicableCheckBox.Checked;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();

                long advanceCategoryId = Convert.ToInt64(advanceCategoryComboBox.SelectedValue);
                AdvanceCategory advanceCategory = _advanceRequisitionCategoryManager.GetById(advanceCategoryId);
                advanceCategory.IsCeilingApplicable = ceilingApplicableCheckBox.Checked;
                advanceCategory.CeilingAmount = Convert.ToDecimal(ceilingAmountTextBox.Text);
                bool isUpdated = _advanceRequisitionCategoryManager.Edit(advanceCategory);
                if (!isUpdated)
                {
                    MessageBox.Show(@"Ceiling amount update failed for " + advanceCategory.Name + @" category.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show(@"Ceiling amount updated for " + advanceCategory.Name + @" category.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAllAdvanceCategories();
                LoadAdvanceCategoriesGridView();
                ceilingApplicableCheckBox.Checked = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ceilingAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Utility.Utility.MakeTextBoxToTakeNumbersOnly(sender, e);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateSave()
        {
            if (ceilingApplicableCheckBox.Checked && string.IsNullOrEmpty(ceilingAmountTextBox.Text))
            {
                throw new UiException("Ceiling amount is not provided.");
            }
            return true;
        }
    }
}
