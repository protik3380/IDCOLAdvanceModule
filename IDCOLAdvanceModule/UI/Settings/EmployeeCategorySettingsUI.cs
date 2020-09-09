using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class EmployeeCategorySettingsUI : Form
    {
        private readonly IEmployeeCategoryManager _employeeCategoryManager;
        private readonly IEmployeeCategorySettingManager _employeeCategorySettingManager;
        private readonly ICollection<Admin_Rank> _designations;

        public EmployeeCategorySettingsUI()
        {
            InitializeComponent();
            IDesignationManager designationManager = new DesignationManager();
            _employeeCategoryManager = new EmployeeCategoryManager();
            _employeeCategorySettingManager = new EmployeeCategorySettingManager();
            _designations = designationManager.GetFiltered();
        }

        private void LoadDesignationGridView()
        {
            //employeeCategorySettingsListView.Items.Clear();
            //foreach (Admin_Rank designation in _designations)
            //{
            //    ListViewItem item = new ListViewItem();
            //    item.Text = designation.RankName;
            //    item.Tag = designation;
            //    employeeCategorySettingsListView.Items.Add(item);
            //}
            employeeCategorySettingsDataGridView.Rows.Clear();
            foreach (Admin_Rank designation in _designations)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(employeeCategorySettingsDataGridView);
                row.Cells[1].Value = designation.RankName;
                row.Tag = designation;
                employeeCategorySettingsDataGridView.Rows.Add(row);
            }
        }

        private void LoadCategoryDropdown()
        {
            var employeeCategories = new List<EmployeeCategory> { new EmployeeCategory { Id = DefaultItem.Value, Name = DefaultItem.Text } };
            if (employeeCategories.Any())
            {
                employeeCategories.AddRange(_employeeCategoryManager.GetAll());
                employeeCategoryComboBox.DataSource = null;
                employeeCategoryComboBox.DisplayMember = "Name";
                employeeCategoryComboBox.ValueMember = "Id";
                employeeCategoryComboBox.DataSource = employeeCategories;
            }
        }

        private void EmployeeCategorySettingsUI_Load(object sender, System.EventArgs e)
        {
            try
            {
                LoadDesignationGridView();
                LoadCategoryDropdown();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                var selectedEmployeeCategoryId = (long)employeeCategoryComboBox.SelectedValue;
                if (selectedEmployeeCategoryId > 0)
                {
                    var employeeCategorySettings = new List<EmployeeCategorySetting>();
                    int count = 0;

                    foreach (DataGridViewRow row in employeeCategorySettingsDataGridView.Rows)
                    {
                        bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                        if (isChecked)
                        {
                            count++;
                        }
                    }

                    if (count>0)
                    {
                        foreach (DataGridViewRow viewRow in employeeCategorySettingsDataGridView.Rows)
                        {
                            if ((bool)viewRow.Cells[0].EditedFormattedValue)
                            {
                                var designation = viewRow.Tag as Admin_Rank;
                                var setting = new EmployeeCategorySetting();
                                setting.EmployeeCategoryId = selectedEmployeeCategoryId;
                                if (designation != null) setting.AdminRankId = (long)designation.RankID;
                                setting.AdminRank = designation;
                                employeeCategorySettings.Add(setting);  
                            }
                        }
                        var employeeCategory = new EmployeeCategory
                        {
                            Id = selectedEmployeeCategoryId,
                            Name = employeeCategoryComboBox.Text,
                            EmployeeCategorySettings = employeeCategorySettings
                        };
                        bool isInserted = _employeeCategoryManager.Edit(employeeCategory);
                        if (isInserted)
                        {
                            MessageBox.Show(@"Settings saved successfully.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(@"Settings save failed.", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please select at least one item to save.", @"Warning!",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please select an employee category.", @"Warning!",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void categoryComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            try
            {
                if (int.Parse(employeeCategoryComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    long selectedCategoryId = (long)employeeCategoryComboBox.SelectedValue;
                    LoadDesignationBySelectedCategory(selectedCategoryId);
                }
                else
                {
                    LoadDesignationGridView();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDesignationBySelectedCategory(long selectedCategoryId)
        {
            //employeeCategorySettingsListView.Items.Clear();
            //var selectedDesignation = _employeeCategorySettingManager.GetByEmployeeCategoryId(selectedCategoryId);
            //foreach (Admin_Rank designation in _designations)
            //{
            //    ListViewItem item = new ListViewItem();
            //    item.Text = designation.RankName;
            //    item.Tag = designation;
            //    if (selectedDesignation.Select(c => c.AdminRankId).Contains(designation.RankID))
            //    {
            //        item.Checked = true;
            //    }
            //    employeeCategorySettingsListView.Items.Add(item);
            //}
            employeeCategorySettingsDataGridView.Rows.Clear();
            var selectedDesignation = _employeeCategorySettingManager.GetByEmployeeCategoryId(selectedCategoryId);
            foreach (Admin_Rank designation in _designations)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(employeeCategorySettingsDataGridView);
                row.Cells[1].Value = designation.RankName;
                row.Tag = designation;
                if (selectedDesignation.Select(c => c.AdminRankId).Contains(designation.RankID))
                {
                    row.Cells[0].Value = true;
                }
                employeeCategorySettingsDataGridView.Rows.Add(row);
            }
        }

        private void toggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)employeeCategorySettingsDataGridView[column, row].EditedFormattedValue;
            employeeCategorySettingsDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void employeeCategorySettingsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                toggleCellCheck(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
