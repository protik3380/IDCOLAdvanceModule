using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class ApprovalLevelSetupUI : Form
    {
        private readonly IApprovalPanelManager _approvalPanelManager;
        private readonly IApprovalLevelManager _approvalLevelManager;
        private bool _isUpdateMode;
        private ApprovalLevel _updateableApprovalLevel;

        public ApprovalLevelSetupUI()
        {
            InitializeComponent();
            _approvalPanelManager = new ApprovalPanelManager();
            _approvalLevelManager = new ApprovalLevelManager();
            _isUpdateMode = false;
        }

        private void LoadApprovalPanelTypeComboBox()
        {
            approvalPanelComboBox.DataSource = null;
            List<ApprovalPanel> approvalPanels = _approvalPanelManager.GetAll().ToList();
            approvalPanels.Insert(0, new ApprovalPanel { Id = DefaultItem.Value, Name = DefaultItem.Text });
            approvalPanelComboBox.DataSource = approvalPanels;
            approvalPanelComboBox.DisplayMember = "Name";
            approvalPanelComboBox.ValueMember = "Id";
        }

        //private void LoadApprovalLevelListView()
        //{
        //    approvalLevelListView.Items.Clear();
        //    long panelId = (long)approvalPanelComboBox.SelectedValue;
        //    if (panelId != DefaultItem.Value)
        //    {
        //        List<ApprovalLevel> approvalLevels = _approvalLevelManager.GetByPanelId(panelId).OrderBy(c => c.LevelOrder).ToList();
        //        foreach (var approvalLevel in approvalLevels)
        //        {
        //            ListViewItem item = new ListViewItem();
        //            item.Text = approvalLevel.Name;
        //            item.SubItems.Add(approvalLevel.LevelOrder.ToString());
        //            item.Tag = approvalLevel;
        //            approvalLevelListView.Items.Add(item);
        //        }
        //    }
        //}
        private void LoadApprovalLevelGridView()
        {
            approvalLevelDataGridView.Rows.Clear();
            long panelId = (long)approvalPanelComboBox.SelectedValue;
            if (panelId != DefaultItem.Value)
            {
                List<ApprovalLevel> approvalLevels = _approvalLevelManager.GetByPanelId(panelId).OrderBy(c => c.LevelOrder).ToList();
                foreach (var approvalLevel in approvalLevels)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(approvalLevelDataGridView);
                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = approvalLevel.Name;
                    row.Cells[2].Value = approvalLevel.LevelOrder;
                    
                    row.Tag = approvalLevel;
                    approvalLevelDataGridView.Rows.Add(row);
                }
            }
        }

        private void ApprovalLevelSetupUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadApprovalPanelTypeComboBox();
                LoadApprovalLevelGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                ApprovalLevel approvalLevel = new ApprovalLevel();
                approvalLevel.ApprovalPanelId = (long)approvalPanelComboBox.SelectedValue;
                approvalLevel.Name = approvalLevelTextBox.Text;
                approvalLevel.LevelOrder = Convert.ToInt32(levelOrderTextBox.Text);
                approvalLevel.IsLineSupervisor = lineSupervisorCheckBox.Checked;
                approvalLevel.IsHeadOfDepartment = headOfDepartmentCheckBox.Checked;
                approvalLevel.IsSourceOfFundEntry = sourceOfFundEntryCheckBox.Checked;
                approvalLevel.IsSourceOfFundVerify = sourceOfFundVerifyCheckBox.Checked;
                approvalLevel.IsApprovalAuthority = approvalAuthorityCheckBox.Checked;
                if (!_isUpdateMode)
                {
                    approvalLevel.CreatedBy = Session.LoginUserName;
                    approvalLevel.CreatedOn = DateTime.Now;
                    bool isInserted = _approvalLevelManager.Insert(approvalLevel);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Approval level setup successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearApprovalLevelInformation();
                        LoadApprovalLevelGridView();
                    }
                    else
                    {
                        MessageBox.Show(@"Approval level setup failed.", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                    }
                }
                else
                {
                    approvalLevel.Id = _updateableApprovalLevel.Id;
                    approvalLevel.CreatedBy = _updateableApprovalLevel.CreatedBy;
                    approvalLevel.CreatedOn = _updateableApprovalLevel.CreatedOn;
                    approvalLevel.LastModifiedBy = Session.LoginUserName;
                    approvalLevel.LastModifiedOn = DateTime.Now;
                    bool isUpdated = _approvalLevelManager.Edit(approvalLevel);
                    if (isUpdated)
                    {
                        MessageBox.Show(@"Approval level setup updated successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearApprovalLevelInformation();
                        LoadApprovalLevelGridView();
                        _isUpdateMode = false;
                        addButton.Text = @"Add";
                    }
                    else
                    {
                        MessageBox.Show(@"Approval level setup failed.", @"Error!", MessageBoxButtons.OK,
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
            if ((long)approvalPanelComboBox.SelectedValue == DefaultItem.Value)
            {
                errorMessage += "Panel type is not selected." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(approvalLevelTextBox.Text))
            {
                errorMessage += "Level name is not provided." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(levelOrderTextBox.Text))
            {
                errorMessage += "Level order is not provided." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(levelOrderTextBox.Text) && Convert.ToInt32(levelOrderTextBox.Text) <= 0)
            {
                errorMessage += "Level order must be greater than 0." + Environment.NewLine;
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

        private void approvalPanelComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadApprovalLevelGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DisableUpDownButtons()
        {
            upButton.Enabled = false;
            downButton.Enabled = false;
        }

        private void EnabledUpDownButtons()
        {
            upButton.Enabled = true;
            downButton.Enabled = true;
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (approvalLevelListView.SelectedItems.Count > 0)
                //{
                //    var currentIndex = approvalLevelListView.SelectedItems[0].Index;
                //    var item = approvalLevelListView.Items[currentIndex];
                //    if (currentIndex < approvalLevelListView.Items.Count - 1)
                //    {
                //        approvalLevelListView.Items.RemoveAt(currentIndex);
                //        approvalLevelListView.Items.Insert(currentIndex + 1, item);
                //        currentIndex = currentIndex + 1;
                //    }
                //    var currentSubTypeItems = GetItemsFromListViewControl();
                //    if (!_approvalLevelManager.UpdateDisplaySerial(currentSubTypeItems))
                //    {
                //        MessageBox.Show(@"Update level order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //    LoadApprovalLevelListView();
                //    if (approvalLevelListView.Items.Count != currentIndex)
                //    {
                //        var selectItem = approvalLevelListView.Items[currentIndex];
                //        selectItem.Selected = true;
                //    }
                //    else
                //    {
                //        approvalLevelListView.SelectedItems.Clear();
                //    }
                //}
                if (approvalLevelDataGridView.SelectedRows.Count > 0)
                {   
                    var currentIndex = approvalLevelDataGridView.SelectedRows[0].Index;
                    var item = approvalLevelDataGridView.Rows[currentIndex];
                    if (currentIndex < approvalLevelDataGridView.Rows.Count - 1)
                    {
                        approvalLevelDataGridView.Rows.RemoveAt(currentIndex);
                        approvalLevelDataGridView.Rows.Insert(currentIndex + 1, item);
                        currentIndex = currentIndex + 1;
                    }
                    var currentSubTypeItems = GetItemsFromGridViewControl();
                    if (!_approvalLevelManager.UpdateDisplaySerial(currentSubTypeItems))
                    {
                        MessageBox.Show(@"Update level order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadApprovalLevelGridView();
                    if (approvalLevelDataGridView.Rows.Count != currentIndex)
                    {
                        var selectItem = approvalLevelDataGridView.Rows[currentIndex];
                        selectItem.Selected = true;
                    }
                    else
                    {
                        approvalLevelDataGridView.Rows.Clear();
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

        //private List<ApprovalLevel> GetItemsFromListViewControl()
        //{
        //    var list = approvalLevelDataGridView.Rows.Cast<ListViewItem>().Select(c => (ApprovalLevel)c.Tag).ToList();
        //    if (list != null && list.Any())
        //    {
        //        int count = 1;
        //        foreach (var approvalLevel in list)
        //        {
        //            approvalLevel.LevelOrder = count;
        //            count++;
        //        }
        //    }
        //    return list;
        //}
        private List<ApprovalLevel> GetItemsFromGridViewControl()
        {
            var list = approvalLevelDataGridView.Rows.Cast<DataGridViewRow>().Select(c => (ApprovalLevel)c.Tag).ToList();
            if (list != null && list.Any())
            {
                int count = 1;
                foreach (var approvalLevel in list)
                {
                    approvalLevel.LevelOrder = count;
                    count++;
                }
            }
            return list;
        }


        private void upButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (approvalLevelListView.SelectedItems.Count > 0)
                //{
                //    var currentIndex = approvalLevelListView.SelectedItems[0].Index;
                //    var item = approvalLevelListView.Items[currentIndex];
                //    if (currentIndex > 0)
                //    {
                //        approvalLevelListView.Items.RemoveAt(currentIndex);
                //        approvalLevelListView.Items.Insert(currentIndex - 1, item);
                //        currentIndex = currentIndex - 1;
                //    }
                //    var currentSubTypeItems = GetItemsFromListViewControl();
                //    if (!_approvalLevelManager.UpdateDisplaySerial(currentSubTypeItems))
                //    {
                //        MessageBox.Show(@"Update level order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //    LoadApprovalLevelListView();
                //    if (currentIndex != -1)
                //    {
                //        var selectItem = approvalLevelListView.Items[currentIndex];
                //        selectItem.Selected = true;
                //    }
                //    else
                //    {
                //        approvalLevelListView.SelectedItems.Clear();
                //    }
                //}
                if (approvalLevelDataGridView.SelectedRows.Count>0)
                {
                    var currentIndex = approvalLevelDataGridView.SelectedRows[0].Index;
                    var item = approvalLevelDataGridView.Rows[currentIndex];
                    if (currentIndex > 0)
                    {
                        approvalLevelDataGridView.Rows.RemoveAt(currentIndex);
                        approvalLevelDataGridView.Rows.Insert(currentIndex - 1, item);
                        currentIndex = currentIndex - 1;
                    }
                    var currentSubTypeItems = GetItemsFromGridViewControl();
                    if (!_approvalLevelManager.UpdateDisplaySerial(currentSubTypeItems))
                    {
                        MessageBox.Show(@"Update level order failed.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadApprovalLevelGridView();
                    if (currentIndex != -1)
                    {
                        var selectItem = approvalLevelDataGridView.Rows[currentIndex];
                        selectItem.Selected = true;
                    }
                    else
                    {
                        approvalLevelDataGridView.Rows.Clear();
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

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (approvalLevelListView.SelectedItems.Count > 0)
                //{
                //    var approvalLevel = approvalLevelListView.SelectedItems[0].Tag as ApprovalLevel;
                //    if (approvalLevel != null)
                //    {
                //        _isUpdateMode = true;
                //        _updateableApprovalLevel = approvalLevel;
                //        LoadApprovalLevelInformation(approvalLevel);
                //        addButton.Text = @"Update";
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Select an item to update.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadApprovalLevelInformation(ApprovalLevel approvalLevel)
        {
            approvalPanelComboBox.SelectedValue = approvalLevel.ApprovalPanelId;
            approvalLevelTextBox.Text = approvalLevel.Name;
            levelOrderTextBox.Text = approvalLevel.LevelOrder.ToString();
            lineSupervisorCheckBox.Checked = approvalLevel.IsLineSupervisor;
            headOfDepartmentCheckBox.Checked = approvalLevel.IsHeadOfDepartment;
            sourceOfFundEntryCheckBox.Checked = approvalLevel.IsSourceOfFundEntry;
            sourceOfFundVerifyCheckBox.Checked = approvalLevel.IsSourceOfFundVerify;
            approvalAuthorityCheckBox.Checked = approvalLevel.IsApprovalAuthority;
        }

        private void ClearApprovalLevelInformation()
        {
            approvalLevelTextBox.Text = string.Empty;
            levelOrderTextBox.Text = string.Empty;
            lineSupervisorCheckBox.Checked = false;
            headOfDepartmentCheckBox.Checked = false;
            sourceOfFundEntryCheckBox.Checked = false;
            sourceOfFundVerifyCheckBox.Checked = false;
            approvalAuthorityCheckBox.Checked = false;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearApprovalLevelInformation();
                _isUpdateMode = false;
                addButton.Text = @"Add";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       private void approvalLevelDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    var approvalLevel = senderGrid.Rows[e.RowIndex].Tag as ApprovalLevel;
                    if (approvalLevel != null)
                    {
                        _updateableApprovalLevel = approvalLevel;
                        _isUpdateMode = true;
                        LoadApprovalLevelInformation(approvalLevel);
                        addButton.Text = @"Update";
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
