using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class ApprovalLevelWiseMemberSettingsUI : Form
    {
        private readonly IDesignationManager _designationManager;
        private readonly IApprovalPanelManager _approvalPanelManager;
        private readonly IApprovalLevelManager _approvalLevelManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IApprovalLevelMemberManager _approvalLevelMemberManager;
        private readonly IAdvance_VW_GetApprovalLevelMemberManager _advanceVwGetApprovalLevelMemberManager;
        private List<Advance_VW_GetApprovalLevelMember> _selectedMembers;

        public ApprovalLevelWiseMemberSettingsUI()
        {
            InitializeComponent();
            _designationManager = new DesignationManager();
            _approvalPanelManager = new ApprovalPanelManager();
            _approvalLevelManager = new ApprovalLevelManager();
            _employeeManager = new EmployeeManager();
            _approvalLevelMemberManager = new ApprovalLevelMemberManager();
            _advanceVwGetApprovalLevelMemberManager = new AdvanceVwGetApprovalLevelMemberManager();
        }

        private void LoadDesignationComboBox()
        {
            designationComboBox.DataSource = null;
            List<Admin_Rank> designationList = new List<Admin_Rank>
            {
                new Admin_Rank { RankID = DefaultItem.Value, RankName = DefaultItem.Text }
            };
            designationList.AddRange(_designationManager.GetFiltered());
            designationComboBox.DisplayMember = "RankName";
            designationComboBox.ValueMember = "RankID";
            designationComboBox.DataSource = designationList;
        }

        private void LoadApprovalPanelComboBox()
        {
            approvalPanelComboBox.DataSource = null;
            List<ApprovalPanel> approvalPanels = new List<ApprovalPanel>
            {
                new ApprovalPanel { Id = DefaultItem.Value, Name = DefaultItem.Text }
            };
            approvalPanels.AddRange(_approvalPanelManager.GetAll());
            approvalPanelComboBox.DisplayMember = "Name";
            approvalPanelComboBox.ValueMember = "Id";
            approvalPanelComboBox.DataSource = approvalPanels;
        }

        private void LoadApprovalLevelComboBox()
        {
            approvalLevelComboBox.DataSource = null;
            long approvalPanelId = Convert.ToInt64(approvalPanelComboBox.SelectedValue);
            List<ApprovalLevel> approvalLevels = new List<ApprovalLevel>
            {
                new ApprovalLevel{ Id = DefaultItem.Value, Name = DefaultItem.Text }
            };
            if (approvalPanelId != DefaultItem.Value)
            {
                approvalLevels.AddRange(_approvalLevelManager.GetByPanelId(approvalPanelId));
            }
            approvalLevelComboBox.DisplayMember = "Name";
            approvalLevelComboBox.ValueMember = "Id";
            approvalLevelComboBox.DataSource = approvalLevels;
        }

        //private void LoadSelectedEmployeeListView()
        //{
        //    selectedEmployeeListView.Items.Clear();
        //    if (Convert.ToInt64(approvalLevelComboBox.SelectedValue) != DefaultItem.Value)
        //    {
        //        long approvalLevelId = Convert.ToInt64(approvalLevelComboBox.SelectedValue);
        //        _selectedMembers = _advanceVwGetApprovalLevelMemberManager.GetByApprovalLevel(approvalLevelId).OrderBy(c => c.PriorityOrder).ToList();
        //        foreach (Advance_VW_GetApprovalLevelMember advanceVwGetApprovalLevelMember in _selectedMembers)
        //        {
        //            ListViewItem item = new ListViewItem(advanceVwGetApprovalLevelMember.EmployeeID);
        //            item.SubItems.Add(advanceVwGetApprovalLevelMember.EmployeeFullName);
        //            item.SubItems.Add(advanceVwGetApprovalLevelMember.RankName);
        //            item.SubItems.Add(advanceVwGetApprovalLevelMember.PriorityOrder.ToString());
        //            item.Tag = advanceVwGetApprovalLevelMember as Advance_VW_GetApprovalLevelMember;
        //            selectedEmployeeListView.Items.Add(item);
        //        }
        //    }
        //}
        private void LoadSelectedEmployeeGridView()
        {
            selectEmployeeDataGridView.Rows.Clear();
            if (Convert.ToInt64(approvalLevelComboBox.SelectedValue) != DefaultItem.Value)
            {
                long approvalLevelId = Convert.ToInt64(approvalLevelComboBox.SelectedValue);
                _selectedMembers = _advanceVwGetApprovalLevelMemberManager.GetByApprovalLevel(approvalLevelId).OrderBy(c => c.PriorityOrder).ToList();
                foreach (Advance_VW_GetApprovalLevelMember advanceVwGetApprovalLevelMember in _selectedMembers)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(selectEmployeeDataGridView);
                    row.Cells[0].Value = "Remove";
                    row.Cells[1].Value = advanceVwGetApprovalLevelMember.EmployeeID;
                    row.Cells[2].Value = advanceVwGetApprovalLevelMember.EmployeeFullName;
                    row.Cells[3].Value = advanceVwGetApprovalLevelMember.RankName;
                    row.Cells[4].Value = advanceVwGetApprovalLevelMember.PriorityOrder.ToString();
                    
                    row.Tag = advanceVwGetApprovalLevelMember as Advance_VW_GetApprovalLevelMember;
                    selectEmployeeDataGridView.Rows.Add(row);
                }
            }
        }

        //private void LoadEmployeeListView()
        //{
        //    employeeListView.Items.Clear();
        //    long designationId = Convert.ToInt64(designationComboBox.SelectedValue);
        //    if (designationId != DefaultItem.Value)
        //    {
        //        List<UserTable> employees = _employeeManager.GetByDesignationId(designationId).ToList();

        //        foreach (UserTable userTable in employees)
        //        {
        //            bool isChecked = _selectedMembers != null && _selectedMembers.Any(c => c.EmployeeUserName == userTable.UserName);
        //            ListViewItem item = new ListViewItem(userTable.EmployeeID);
        //            item.SubItems.Add(userTable.FullName);
        //            item.Tag = userTable;
        //            item.Checked = isChecked;
        //            employeeListView.Items.Add(item);
        //        }
        //    }
        //}

        private void LoadEmployeeGridView()
        {
            employeeDataGridView.Rows.Clear();
            long designationId = Convert.ToInt64(designationComboBox.SelectedValue);
            if (designationId != DefaultItem.Value)
            {
                List<UserTable> employees = _employeeManager.GetByDesignationId(designationId).ToList();

                foreach (UserTable userTable in employees)
                {
                    bool isChecked = _selectedMembers != null && _selectedMembers.Any(c => c.EmployeeUserName == userTable.UserName);
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(employeeDataGridView);
                    row.Cells[0].Value = isChecked;
                    row.Cells[1].Value = userTable.EmployeeID;
                    row.Cells[2].Value = userTable.FullName;
                    row.Tag = userTable;
                    employeeDataGridView.Rows.Add(row);
                }
            }
        }

        private void ApprovalLevelWiseMemberSettingsUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDesignationComboBox();
                LoadApprovalPanelComboBox();
                LoadApprovalLevelComboBox();
                designationComboBox.Enabled = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void approvalPanelComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadApprovalLevelComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void designationComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //LoadEmployeeListView();
                LoadEmployeeGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void designationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                designationComboBox.Enabled = designationCheckBox.Checked;
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
                List<ApprovalLevelMember> approvalLevelMembers = new List<ApprovalLevelMember>();
                int priority = _approvalLevelMemberManager.GetMaxValueInPriorityOrder(Convert.ToInt64(approvalLevelComboBox.SelectedValue));
                foreach (DataGridViewRow row in employeeDataGridView.Rows)
                {
                    //row.Cells["checkColumn"].Value =true;
                    if ((bool)row.Cells[0].Value)
                    {
                        UserTable employee = row.Tag as UserTable;
                        if (employee != null)
                        {
                            ApprovalLevelMember approvalLevelMember = new ApprovalLevelMember();
                            approvalLevelMember.ApprovalLevelId = Convert.ToInt64(approvalLevelComboBox.SelectedValue);
                            ApprovalLevel approvalLevel =
                                _approvalLevelManager.GetById(Convert.ToInt64(approvalLevelComboBox.SelectedValue));
                            if (approvalLevel.IsLineSupervisor || approvalLevel.IsHeadOfDepartment)
                            {
                                priority = 1;
                            }
                            priority = priority + 1;
                            approvalLevelMember.EmployeeUserName = employee.UserName;
                            approvalLevelMember.CreatedBy = Session.LoginUserName;
                            approvalLevelMember.CreatedOn = DateTime.Now;
                            approvalLevelMember.PriorityOrder = priority;
                            approvalLevelMembers.Add(approvalLevelMember);
                        }
                    }
                }
                if (approvalLevelMembers.Any())
                {
                    bool isInserted = _approvalLevelMemberManager.CheckAndInsert(approvalLevelMembers,
                     Convert.ToInt64(designationComboBox.SelectedValue), _selectedMembers);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Approval level member mapping successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        _approvalLevelMemberManager.ReArrangePriorityOrder(Convert.ToInt64(approvalLevelComboBox.SelectedValue));
                        //LoadSelectedEmployeeListView();
                        //LoadEmployeeListView();
                        LoadSelectedEmployeeGridView();
                        LoadEmployeeGridView();
                    }
                    else
                    {
                        MessageBox.Show(@"Approval level member mapping failed.", @"Error!", MessageBoxButtons.OK,
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
            if (Convert.ToInt64(approvalLevelComboBox.SelectedValue) == DefaultItem.Value)
            {
                errorMessage += "Approval level is not selected." + Environment.NewLine;
            }
            if (employeeDataGridView.SelectedRows.Count == 0)
            {
                errorMessage += "No employee is selected." + Environment.NewLine;
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

        private void approvalLevelComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadSelectedEmployeeGridView();
                LoadEmployeeGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (selectedEmployeeListView.SelectedItems.Count > 0)
            //    {
            //        Advance_VW_GetApprovalLevelMember member = selectedEmployeeListView.SelectedItems[0].Tag as Advance_VW_GetApprovalLevelMember;
            //        if (member != null)
            //        {
            //            ApprovalLevelMember memberToRemove = new ApprovalLevelMember
            //            {
            //                Id = member.ApprovalLevelMemberId,
            //                ApprovalLevelId = member.ApprovalLevelId,
            //                EmployeeUserName = member.EmployeeUserName,
            //                CreatedBy = member.CreatedBy,
            //                CreatedOn = member.CreatedOn,
            //                LastModifiedBy = Session.LoginUserName,
            //                LastModifiedOn = DateTime.Now,
            //                PriorityOrder = member.PriorityOrder,
            //                IsDeleted = true
            //            };
            //            bool isDeleted = _approvalLevelMemberManager.Edit(memberToRemove);
            //            if (isDeleted)
            //            {
            //                MessageBox.Show(@"Approval level member removed successfully.", @"Success", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Information);
            //                _approvalLevelMemberManager.ReArrangePriorityOrder(Convert.ToInt64(approvalLevelComboBox.SelectedValue));
            //                LoadSelectedEmployeeListView();
            //                LoadEmployeeListView();
            //            }
            //            else
            //            {
            //                MessageBox.Show(@"Approval level member remove failed.", @"Error!", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Error);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(@"Please select a member to remove.", @"Warning!", MessageBoxButtons.OK,
            //            MessageBoxIcon.Warning);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (selectedEmployeeListView.SelectedItems.Count > 0)
                //{
                //    var currentIndex = selectedEmployeeListView.SelectedItems[0].Index;
                //    var item = selectedEmployeeListView.Items[currentIndex];
                //    if (currentIndex > 0)
                //    {
                //        selectedEmployeeListView.Items.RemoveAt(currentIndex);
                //        selectedEmployeeListView.Items.Insert(currentIndex - 1, item);
                //        currentIndex = currentIndex - 1;
                //    }
                //    var currentSubTypeItems = GetItemsFromListViewControl();
                //    if (!_approvalLevelMemberManager.UpdatePrioritySerial(currentSubTypeItems))
                //    {
                //        MessageBox.Show(@"Update Priority order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //    LoadSelectedEmployeeListView();
                //    if (currentIndex != -1)
                //    {
                //        var selectItem = selectedEmployeeListView.Items[currentIndex];
                //        selectItem.Selected = true;
                //    }
                //    else
                //    {
                //        selectedEmployeeListView.SelectedItems.Clear();
                //    }
                //}
                if (selectEmployeeDataGridView.SelectedRows.Count > 0)
                {
                    var currentIndex = selectEmployeeDataGridView.SelectedRows[0].Index;
                    var item = selectEmployeeDataGridView.Rows[currentIndex];
                    if (currentIndex > 0)
                    {
                        selectEmployeeDataGridView.Rows.RemoveAt(currentIndex);
                        selectEmployeeDataGridView.Rows.Insert(currentIndex - 1, item);
                        currentIndex = currentIndex - 1;
                    }
                    var currentSubTypeItems = GetItemsFromListViewControl();
                    if (!_approvalLevelMemberManager.UpdatePrioritySerial(currentSubTypeItems))
                    {
                        MessageBox.Show(@"Update Priority order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadSelectedEmployeeGridView();
                    if (currentIndex != -1)
                    {
                        var selectItem = selectEmployeeDataGridView.Rows[currentIndex];
                        selectItem.Selected = true;
                    }
                    else
                    {
                        selectEmployeeDataGridView.SelectedRows.Clear();
                    }
                }
                else
                {
                    MessageBox.Show(@"Select an item first.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ApprovalLevelMember> GetItemsFromListViewControl()
        {

            List<Advance_VW_GetApprovalLevelMember> list = selectEmployeeDataGridView.Rows.Cast<DataGridViewRow>().Select(c => (Advance_VW_GetApprovalLevelMember)c.Tag).ToList();

            List<ApprovalLevelMember> approvalLevelMembers = list.Select(c => new ApprovalLevelMember()
            {
                Id = c.ApprovalLevelMemberId,
                ApprovalLevelId = c.ApprovalLevelId,
                EmployeeUserName = c.EmployeeUserName,
                CreatedBy = c.CreatedBy,
                CreatedOn = c.CreatedOn,
                LastModifiedBy = Session.LoginUserName,
                LastModifiedOn = DateTime.Now,
                PriorityOrder = c.PriorityOrder,
                IsDeleted = c.IsDeleted
            }).ToList();
            if (approvalLevelMembers.Any())
            {
                int count = 1;
                ApprovalLevel approvalLevel =
                    _approvalLevelManager.GetById(Convert.ToInt64(approvalLevelComboBox.SelectedValue));
                if (approvalLevel.IsLineSupervisor || approvalLevel.IsHeadOfDepartment)
                {
                    count = 2;
                }

                foreach (var approvalLevelMember in approvalLevelMembers)
                {
                    approvalLevelMember.PriorityOrder = count;
                    count++;
                }
            }
            return approvalLevelMembers;
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (selectedEmployeeListView.SelectedItems.Count > 0)
                //{
                //    var currentIndex = selectedEmployeeListView.SelectedItems[0].Index;
                //    var item = selectedEmployeeListView.Items[currentIndex];
                //    if (currentIndex < selectedEmployeeListView.Items.Count - 1)
                //    {
                //        selectedEmployeeListView.Items.RemoveAt(currentIndex);
                //        selectedEmployeeListView.Items.Insert(currentIndex + 1, item);
                //        currentIndex = currentIndex + 1;
                //    }
                //    var currentSubTypeItems = GetItemsFromListViewControl();
                //    if (!_approvalLevelMemberManager.UpdatePrioritySerial(currentSubTypeItems))
                //    {
                //        MessageBox.Show(@"Update priority order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //    LoadSelectedEmployeeListView();
                //    if (selectedEmployeeListView.Items.Count != currentIndex)
                //    {
                //        var selectItem = selectedEmployeeListView.Items[currentIndex];
                //        selectItem.Selected = true;
                //    }
                //    else
                //    {
                //        selectedEmployeeListView.SelectedItems.Clear();
                //    }
                //}
                if (selectEmployeeDataGridView.SelectedRows.Count > 0)
                {
                    var currentIndex = selectEmployeeDataGridView.SelectedRows[0].Index;
                    var item = selectEmployeeDataGridView.Rows[currentIndex];
                    if (currentIndex < selectEmployeeDataGridView.Rows.Count - 1)
                    {
                        selectEmployeeDataGridView.Rows.RemoveAt(currentIndex);
                        selectEmployeeDataGridView.Rows.Insert(currentIndex + 1, item);
                        currentIndex = currentIndex + 1;
                    }
                    var currentSubTypeItems = GetItemsFromListViewControl();
                    if (!_approvalLevelMemberManager.UpdatePrioritySerial(currentSubTypeItems))
                    {
                        MessageBox.Show(@"Update priority order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadSelectedEmployeeGridView();
                    if (selectEmployeeDataGridView.Rows.Count != currentIndex)
                    {
                        var selectItem = selectEmployeeDataGridView.Rows[currentIndex];
                        selectItem.Selected = true;
                    }
                    else
                    {
                        selectEmployeeDataGridView.SelectedRows.Clear();
                    }
                }
                else
                {
                    MessageBox.Show(@"Select an item first.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void employeeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ToggleCellCheck(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

     
        private void ToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)employeeDataGridView[column, row].EditedFormattedValue;
            employeeDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void selectEmployeeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (selectEmployeeDataGridView.SelectedRows.Count > 0)
                    {
                        Advance_VW_GetApprovalLevelMember member = selectEmployeeDataGridView.SelectedRows[0].Tag as Advance_VW_GetApprovalLevelMember;
                        if (member != null)
                        {
                            ApprovalLevelMember memberToRemove = new ApprovalLevelMember
                            {
                                Id = member.ApprovalLevelMemberId,
                                ApprovalLevelId = member.ApprovalLevelId,
                                EmployeeUserName = member.EmployeeUserName,
                                CreatedBy = member.CreatedBy,
                                CreatedOn = member.CreatedOn,
                                LastModifiedBy = Session.LoginUserName,
                                LastModifiedOn = DateTime.Now,
                                PriorityOrder = member.PriorityOrder,
                                IsDeleted = true
                            };
                            bool isDeleted = _approvalLevelMemberManager.Edit(memberToRemove);
                            if (isDeleted)
                            {
                                MessageBox.Show(@"Approval level member removed successfully.", @"Success", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                                _approvalLevelMemberManager.ReArrangePriorityOrder(Convert.ToInt64(approvalLevelComboBox.SelectedValue));
                                LoadSelectedEmployeeGridView();
                                LoadEmployeeGridView();
                            }
                            else
                            {
                                MessageBox.Show(@"Approval level member remove failed.", @"Error!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please select a member to remove.", @"Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
