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
    public partial class ApprovalPanelSetupUI : Form
    {
        private readonly IApprovalPanelTypeManager _approvalPanelTypeManager;
        private readonly IApprovalPanelManager _approvalPanelManager;
        private bool _isUpdateMode;
        private ApprovalPanel _updatebaleApprovalPanel;

        public ApprovalPanelSetupUI()
        {
            InitializeComponent();
            _approvalPanelTypeManager = new ApprovalPanelTypeManager();
            _approvalPanelManager = new ApprovalPanelManager();
            _isUpdateMode = false;
        }

        private void LoadPanelTypeComboBox()
        {
            panelTypeComboBox.DataSource = null;
            List<ApprovalPanelType> panelTypeList = _approvalPanelTypeManager.GetAll().ToList();
            panelTypeList.Insert(0, new ApprovalPanelType { Id = DefaultItem.Value, Name = DefaultItem.Text });
            panelTypeComboBox.DataSource = panelTypeList;
            panelTypeComboBox.DisplayMember = "Name";
            panelTypeComboBox.ValueMember = "Id";
        }

        private void ApprovalPanelSetupUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPanelTypeComboBox();
                LoadApprovePanelGridView();
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
                ApprovalPanel panel = new ApprovalPanel();
                panel.ApprovalPanelTypeId = (long)panelTypeComboBox.SelectedValue;
                panel.Name = panelNameTextBox.Text;
                if (!_isUpdateMode)
                {
                    bool isInserted = _approvalPanelManager.Insert(panel);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Approval panel setup successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearInputs();
                        //LoadApprovalPanelListView();
                        LoadApprovePanelGridView();
                    }
                    else
                    {
                        MessageBox.Show(@"Approval panel setup failed.", @"Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    panel.Id = _updatebaleApprovalPanel.Id;
                    panel.CreatedOn = _updatebaleApprovalPanel.CreatedOn;
                    panel.CreatedBy = _updatebaleApprovalPanel.CreatedBy;
                    bool isUpdated = _approvalPanelManager.Edit(panel);
                    if (isUpdated)
                    {
                        MessageBox.Show(@"Approval panel setup updated successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearInputs();
                        _isUpdateMode = false;
                        //LoadApprovalPanelListView();
                        LoadApprovePanelGridView();
                        saveButton.Text = @"Save";
                    }
                    else
                    {
                        MessageBox.Show(@"Approval panel setup failed.", @"Error!", MessageBoxButtons.OK,
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

        private void ClearInputs()
        {
            panelTypeComboBox.SelectedValue = DefaultItem.Value;
            panelNameTextBox.Text = string.Empty;
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if ((long)panelTypeComboBox.SelectedValue == DefaultItem.Value)
            {
                errorMessage += "Panel type is not selected." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(panelNameTextBox.Text))
            {
                errorMessage += "Panel name is not provided." + Environment.NewLine;
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

        //private void LoadApprovalPanelListView()
        //{
        //    var approvalPanels = _approvalPanelManager.GetAll();
        //    int serial = 1;
        //    approvalPanelListView.Items.Clear();
        //    foreach (var approvalPanel in approvalPanels)
        //    {
        //        ListViewItem item = new ListViewItem(serial.ToString());
        //        item.SubItems.Add(approvalPanel.Name);
        //        item.SubItems.Add(approvalPanel.ApprovalPanelType.Name);
        //        item.Tag = approvalPanel;
        //        approvalPanelListView.Items.Add(item);
        //        serial++;
        //    }
        //}

        private void LoadApprovePanelGridView()
        {
            var approvalPanels = _approvalPanelManager.GetAll();
            int serial = 1;
            approvePanelGridView.Rows.Clear();
            foreach (var approvalPanel in approvalPanels)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(approvePanelGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = serial.ToString();
                row.Cells[2].Value = approvalPanel.Name;
                row.Cells[3].Value = approvalPanel.ApprovalPanelType.Name;
                
                row.Tag = approvalPanel;
                approvePanelGridView.Rows.Add(row);
                serial++;

            }
        }

        //private void editToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (approvalPanelListView.SelectedItems.Count > 0)
        //        {
        //            var approvalPanel = approvalPanelListView.SelectedItems[0].Tag as ApprovalPanel;
        //            if (approvalPanel != null)
        //            {
        //                _updatebaleApprovalPanel = approvalPanel;
        //                _isUpdateMode = true;
        //                LoadApprovalPanelInformation(approvalPanel);
        //                saveButton.Text = @"Update";
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        private void LoadApprovalPanelInformation(ApprovalPanel approvalPanel)
        {
            panelTypeComboBox.SelectedValue = approvalPanel.ApprovalPanelTypeId;
            panelNameTextBox.Text = approvalPanel.Name;
        }

        private void ClearApprovalPanelInformation()
        {
            panelTypeComboBox.SelectedValue = DefaultItem.Value;
            panelNameTextBox.Text = "";
        }

        private void approvePanelGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {               
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    var approvalPanel = senderGrid.Rows[e.RowIndex].Tag as ApprovalPanel;
                    if (approvalPanel!=null)
                    {
                        _updatebaleApprovalPanel = approvalPanel;
                        _isUpdateMode = true;
                        LoadApprovalPanelInformation(approvalPanel);
                        saveButton.Text = @"Update";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearApprovalPanelType();
                _isUpdateMode = false;
                saveButton.Text = @"Save";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearApprovalPanelType()
        {
            panelNameTextBox.Text = "";
            panelTypeComboBox.SelectedIndex = 0;
        }

       
    }
}
