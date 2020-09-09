using System;
using System.Drawing;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class EmployeeLeaveEntryUI : Form
    {
        private readonly IEmployeeManager _employeeManager;
        private UserTable _selectedEmployee;
        private readonly IEmployeeLeaveManager _employeeLeaveManager;
        private string _userName;
        private EmployeeLeave _employeeLeave;
        private bool _isUpdateMode;
        private bool _isDeleteMode;
        private EmployeeLeave _updatebaleEmployeeLeave;

        public EmployeeLeaveEntryUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _employeeLeaveManager = new EmployeeLeaveManager();
        }

        private void EmployeeLeaveEntryUI_Load(object sender, EventArgs e)
        {
            try
            {
                _userName = Session.LoginUserName;

                if (_userName.ToLower().Equals("admin"))
                {
                    _isDeleteMode = true;
                    selectEmployeeButton.Enabled = true;
                    removeToolStripMenuItem.Visible = true;
                }
                else
                {
                    _isDeleteMode = false;
                    selectEmployeeButton.Enabled = false;
                    _selectedEmployee = _employeeManager.GetByUserName(_userName);
                    employeeNameTextBox.Text = _selectedEmployee.FullName;
                    employeeIdTextBox.Text = _selectedEmployee.EmployeeID;
                    designationTextBox.Text = _selectedEmployee.Admin_Rank.RankName;
                    LoadEmployeeLeaveListView(_selectedEmployee.UserName);
                }
                _isUpdateMode = false;
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
                _employeeLeave = new EmployeeLeave();
                _employeeLeave.EmployeeUsername = _selectedEmployee.UserName;

                if (!_isUpdateMode && !_isDeleteMode)
                {
                    _employeeLeave.FromDate = fromDateTimePicker.Value.Date;
                    _employeeLeave.ToDate = toDateTimePicker.Value.Date;
                    _employeeLeave.CreatedBy = Session.LoginUserName;
                    _employeeLeave.CreatedOn = DateTime.Now;
                    bool isInserted = _employeeLeaveManager.Insert(_employeeLeave);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Employee leave saved successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadEmployeeLeaveListView(_selectedEmployee.UserName);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(@"Employee leave save failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else if (_isDeleteMode)
                {
                    if (!_userName.ToLower().Equals("admin"))
                    {
                        MessageBox.Show(@"Only admin can access delete operation.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    _employeeLeave.Id = _updatebaleEmployeeLeave.Id;
                    _employeeLeave.CreatedBy = _updatebaleEmployeeLeave.CreatedBy;
                    _employeeLeave.CreatedOn = _updatebaleEmployeeLeave.CreatedOn;
                    _employeeLeave.LastModifiedBy = Session.LoginUserName;
                    _employeeLeave.LastModifiedOn = DateTime.Now;
                    _employeeLeave.FromDate = _updatebaleEmployeeLeave.FromDate;
                    _employeeLeave.ToDate = _updatebaleEmployeeLeave.ToDate;
                    if (_updatebaleEmployeeLeave.FromDate < DateTime.Now.Date)
                    {
                        MessageBox.Show(@"Ending of leave date can not be before today's date.", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
                        return;
                    }
                    _employeeLeave.IsDeleted = true;
                    bool isDeleted = _employeeLeaveManager.Edit(_employeeLeave);
                    if (isDeleted)
                    {
                        MessageBox.Show(@"Employee leave delete successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadEmployeeLeaveListView(_selectedEmployee.UserName);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(@"Employee leave delete failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _employeeLeave.Id = _updatebaleEmployeeLeave.Id;
                    _employeeLeave.CreatedBy = _updatebaleEmployeeLeave.CreatedBy;
                    _employeeLeave.CreatedOn = _updatebaleEmployeeLeave.CreatedOn;
                    _employeeLeave.LastModifiedBy = Session.LoginUserName;
                    _employeeLeave.LastModifiedOn = DateTime.Now;

                    if (_updatebaleEmployeeLeave.ToDate < DateTime.Now.Date)
                    {
                        MessageBox.Show(@"Ending of leave date can not be before today's date.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                    _employeeLeave.ToDate = toDateTimePicker.Value.Date;
                    _employeeLeave.FromDate = fromDateTimePicker.Value.Date;

                    bool isUpdated = _employeeLeaveManager.Edit(_employeeLeave);
                    if (isUpdated)
                    {
                        MessageBox.Show(@"Employee leave updated successfully.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadEmployeeLeaveListView(_selectedEmployee.UserName);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(@"Employee leave update failed.", @"Error!", MessageBoxButtons.OK,
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

        private void ClearForm()
        {
            fromDateTimePicker.Value = DateTime.Now;
            toDateTimePicker.Value = DateTime.Now;
            fromDateTimePicker.Checked = false;
            toDateTimePicker.Checked = false;
            saveButton.Text = @"Save";
            _isUpdateMode = false;
            _isDeleteMode = false;
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if (_selectedEmployee == null)
            {
                errorMessage += "Employee is not selected." + Environment.NewLine;
            }
            if (!fromDateTimePicker.Checked)
            {
                errorMessage += "From date is not selected." + Environment.NewLine;
            }
            if (!toDateTimePicker.Checked)
            {
                errorMessage += "To date is not selected." + Environment.NewLine;
            }
            if (fromDateTimePicker.Value.Date > toDateTimePicker.Value.Date)
            {
                errorMessage += "To date cannot go before from date." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            return true;
        }

        private void selectEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectEmployeeUi = new SelectEmployeeUI();
                selectEmployeeUi.ShowDialog();
                _selectedEmployee = selectEmployeeUi.SelectedEmployee;
                if (_selectedEmployee == null)
                {
                    return;
                }
                employeeNameTextBox.Text = _selectedEmployee.FullName;
                employeeIdTextBox.Text = _selectedEmployee.EmployeeID;
                designationTextBox.Text = _selectedEmployee.Admin_Rank.RankName;
                LoadEmployeeLeaveListView(_selectedEmployee.UserName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void LoadEmployeeLeaveListView(string userName)
        {
            var leaveList = _employeeLeaveManager.GetAllByEmployeeUserName(userName);
            int serial = 1;
            employeeLeaveDataGridView.Rows.Clear();
            if (leaveList == null)
            {
                return;
            }
            foreach (var leave in leaveList)
            {
                DataGridViewRow row=new DataGridViewRow();
                row.CreateCells(employeeLeaveDataGridView);
                row.Cells[0].Value = serial.ToString();
                row.Cells[1].Value = leave.FromDate.ToString("dd MMM, yyyy");
                row.Cells[2].Value = leave.ToDate.ToString("dd MMM, yyyy");
                row.Cells[3].Value = "Edit";
                row.Cells[4].Value = "Remove"; 
                row.Tag = leave;
                employeeLeaveDataGridView.Rows.Add(row);
                serial++;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (employeeLeaveListView.SelectedItems.Count > 0)
                //{
                //    var employeeLeave = employeeLeaveListView.SelectedItems[0].Tag as EmployeeLeave;
                //    if (employeeLeave != null)
                //    {
                //        _updatebaleEmployeeLeave = employeeLeave;
                //        _isUpdateMode = true;
                //        if (_updatebaleEmployeeLeave.FromDate < DateTime.Now.Date)
                //        {
                //            fromDateTimePicker.Enabled = false;
                //        }
                //        if (_updatebaleEmployeeLeave.ToDate < DateTime.Now.Date)
                //        {
                //            toDateTimePicker.Enabled = false;
                //        }
                //        fromDateTimePicker.Value = employeeLeave.FromDate;
                //        toDateTimePicker.Value = employeeLeave.ToDate;
                //        saveButton.Text = @"Update";
                //    }
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!_userName.ToLower().Equals("admin"))
                //{
                //    MessageBox.Show(@"Only admin can access delete operation.", @"Error!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);
                //    return;
                //}
                //if (employeeLeaveListView.SelectedItems.Count > 0)
                //{
                //    var employeeLeave = employeeLeaveListView.SelectedItems[0].Tag as EmployeeLeave;
                //    if (employeeLeave != null)
                //    {
                //        _updatebaleEmployeeLeave = employeeLeave;
                //        _isUpdateMode = false;
                //        _isDeleteMode = true;
                //        fromDateTimePicker.Value = employeeLeave.FromDate;
                //        toDateTimePicker.Value = employeeLeave.ToDate;
                //        saveButton.Text = @"Delete";
                //    }
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void employeeLeaveDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                   var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex==3)
                    {
                        if (employeeLeaveDataGridView.SelectedRows.Count > 0)
                        {
                            var employeeLeave = employeeLeaveDataGridView.SelectedRows[0].Tag as EmployeeLeave;
                            if (employeeLeave != null)
                            {
                                _updatebaleEmployeeLeave = employeeLeave;
                                _isUpdateMode = true;
                                if (_updatebaleEmployeeLeave.FromDate < DateTime.Now.Date)
                                {
                                    fromDateTimePicker.Enabled = false;
                                }
                                if (_updatebaleEmployeeLeave.ToDate < DateTime.Now.Date)
                                {
                                    toDateTimePicker.Enabled = false;
                                }
                                fromDateTimePicker.Value = employeeLeave.FromDate;
                                toDateTimePicker.Value = employeeLeave.ToDate;
                                saveButton.Text = @"Update";
                            }
                        }
                    }
                    else  if(e.ColumnIndex==4)
                    {
                        if (!_userName.ToLower().Equals("admin"))
                        {
                            MessageBox.Show(@"Only admin can access delete operation.", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                        if (employeeLeaveDataGridView.SelectedRows.Count > 0)
                        {
                            var employeeLeave = employeeLeaveDataGridView.SelectedRows[0].Tag as EmployeeLeave;
                            if (employeeLeave != null)
                            {
                                _updatebaleEmployeeLeave = employeeLeave;
                                _isUpdateMode = false;
                                _isDeleteMode = true;
                                fromDateTimePicker.Value = employeeLeave.FromDate;
                                toDateTimePicker.Value = employeeLeave.ToDate;
                                saveButton.Text = @"Delete";
                            }
                        } 
                    }
                    
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
