using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Migrations;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class HeadOfDepartmentSetupUI : Form
    {
        private UserTable _selectedEmployee;
        private readonly IEmployeeManager _employeeManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly IHeadOfDepartmentManager _headOfDepartmentManager;
        private readonly IAdvance_VW_GetHeadOfDepartmentManager _advanceVwGetHeadOfDepartmentManager;
        private HeadOfDepartment _updateableHeadOfDepartment;
        private bool _isUpdateMode;
        private decimal _departmentId;

        public HeadOfDepartmentSetupUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _departmentManager = new DepartmentManager();
            _headOfDepartmentManager = new HeadOfDepartmentManager();
            _advanceVwGetHeadOfDepartmentManager = new AdvanceVwGetHeadOfDepartmentManager();
        }

        private void selectEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_departmentId != DefaultItem.Value && _departmentId != 0)
                {
                    SelectEmployeeUI selectEmployeeUi = new SelectEmployeeUI();
                    selectEmployeeUi.ShowDialog();
                    _selectedEmployee = selectEmployeeUi.SelectedEmployee;
                    if (_selectedEmployee != null)
                    {
                        employeeNameTextBox.Text = _selectedEmployee.FullName;
                    }
                }
                else
                {
                    MessageBox.Show(@"Please select a department first.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDepartmentComboBox()
        {
            List<Admin_Departments> departments = new List<Admin_Departments>
            {
                new Admin_Departments { DepartmentID = DefaultItem.Value, DepartmentName = DefaultItem.Text }
            };
            departments.AddRange(_departmentManager.GetAll());
            departmentComboBox.DisplayMember = "DepartmentName";
            departmentComboBox.ValueMember = "DepartmentID";
            departmentComboBox.DataSource = departments;
        }

        private void HeadOfDepartmentSetupUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDepartmentComboBox();
                LoadHeadOfDepartmentGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void departmentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                _departmentId = Convert.ToInt64(departmentComboBox.SelectedValue);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                if (!_isUpdateMode)
                {
                    HeadOfDepartment headOfDepartment = new HeadOfDepartment();
                    headOfDepartment.DepartmentId = _departmentId;
                    headOfDepartment.EmployeeUserName = _selectedEmployee.UserName;
                    bool isInserted = _headOfDepartmentManager.Insert(headOfDepartment);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Head of department setup successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ResetForm();
                        //LoadHeadOfDepartmentListView();
                        LoadHeadOfDepartmentGridView();
                    }
                    else
                    {
                        MessageBox.Show(@"Head of department setup failed.", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _updateableHeadOfDepartment.EmployeeUserName = _selectedEmployee.UserName;
                    bool isUpdated = _headOfDepartmentManager.Edit(_updateableHeadOfDepartment);
                    if (isUpdated)
                    {
                        MessageBox.Show(@"Head of department update successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ResetForm();
                        LoadHeadOfDepartmentGridView();
                    }
                    else
                    {
                        MessageBox.Show(@"Head of department update failed.", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if (_departmentId == DefaultItem.Value || _departmentId == 0)
            {
                errorMessage += "Department is not selected." + Environment.NewLine;
            }
            if (_selectedEmployee == null)
            {
                errorMessage += "Employee is not selected." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            else
            {
                return true;
            }
        }

        //private void LoadHeadOfDepartmentListView()
        //{
        //    headOfDepartmentListView.Items.Clear();
        //    ICollection<Advance_VW_GetHeadOfDepartment> headOfDepartmentList =
        //        _advanceVwGetHeadOfDepartmentManager.GetAll();
        //    foreach (Advance_VW_GetHeadOfDepartment headOfDepartment in headOfDepartmentList)
        //    {
        //        ListViewItem item = new ListViewItem(headOfDepartment.DepartmentName);
        //        item.SubItems.Add(headOfDepartment.HeadOfDepartmentFullName);
        //        item.Tag = headOfDepartment;
        //        headOfDepartmentListView.Items.Add(item);
        //    }
        //}
        private void LoadHeadOfDepartmentGridView()
        {
            headOfDepaertmentDataGridView.Rows.Clear();
            ICollection<Advance_VW_GetHeadOfDepartment> headOfDepartmentList =
                _advanceVwGetHeadOfDepartmentManager.GetAll();
            foreach (Advance_VW_GetHeadOfDepartment headOfDepartment in headOfDepartmentList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(headOfDepaertmentDataGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = headOfDepartment.DepartmentName;
                row.Cells[2].Value = headOfDepartment.HeadOfDepartmentFullName;
                
                row.Tag = headOfDepartment;
                headOfDepaertmentDataGridView.Rows.Add(row);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (headOfDepartmentListView.SelectedItems.Count > 0)
                //{
                //    _isUpdateMode = true;
                //    saveButton.Text = @"Update";
                //    resetButton.Visible = true;
                //    departmentComboBox.Enabled = false;
                //    Advance_VW_GetHeadOfDepartment headOfDepartment =
                //        headOfDepartmentListView.SelectedItems[0].Tag as Advance_VW_GetHeadOfDepartment;
                //    if (headOfDepartment != null)
                //    {
                //        _updateableHeadOfDepartment = new HeadOfDepartment { Id = headOfDepartment.HODId, DepartmentId = headOfDepartment.DepartmentID.Value, EmployeeUserName = headOfDepartment.HeadOfDepartmentUserName };
                //        _departmentId = Convert.ToDecimal(_updateableHeadOfDepartment.DepartmentId);
                //        _selectedEmployee = _employeeManager.GetByUserName(_updateableHeadOfDepartment.EmployeeUserName);
                //        departmentComboBox.SelectedValue = _departmentId;
                //        employeeNameTextBox.Text = _selectedEmployee.FullName;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetForm();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            departmentComboBox.SelectedIndex = 0;
            _departmentId = 0;
            _selectedEmployee = null;
            employeeNameTextBox.Text = String.Empty;
            saveButton.Text = @"Save";
            departmentComboBox.Enabled = true;
            resetButton.Visible = false;
        }

        private void headOfDepaertmentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (headOfDepaertmentDataGridView.SelectedRows.Count > 0)
                    {
                        _isUpdateMode = true;
                        saveButton.Text = @"Update";
                        resetButton.Visible = true;
                        departmentComboBox.Enabled = false;
                        Advance_VW_GetHeadOfDepartment headOfDepartment =
                            headOfDepaertmentDataGridView.SelectedRows[0].Tag as Advance_VW_GetHeadOfDepartment;
                        if (headOfDepartment != null)
                        {
                            _updateableHeadOfDepartment = new HeadOfDepartment { Id = headOfDepartment.HODId, DepartmentId = headOfDepartment.DepartmentID.Value, EmployeeUserName = headOfDepartment.HeadOfDepartmentUserName };
                            _departmentId = Convert.ToDecimal(_updateableHeadOfDepartment.DepartmentId);
                            _selectedEmployee = _employeeManager.GetByUserName(_updateableHeadOfDepartment.EmployeeUserName);
                            departmentComboBox.SelectedValue = _departmentId;
                            employeeNameTextBox.Text = _selectedEmployee.FullName;
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
