using System;
using System.Drawing;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class ApprovalPanelTypeSetupUI : Form
    {
        private readonly IApprovalPanelTypeManager _approvalPanelTypeManager;
        private bool _isUpdateMode;
        private ApprovalPanelType _updateableApprovalPanelType;

        public ApprovalPanelTypeSetupUI()
        {
            InitializeComponent();
            _approvalPanelTypeManager = new ApprovalPanelTypeManager();
            _isUpdateMode = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                ApprovalPanelType approvalPanelType = new ApprovalPanelType();
                approvalPanelType.Name = panelTypeNameTextBox.Text;
                if (!_isUpdateMode)
                {
                    bool isInserted = _approvalPanelTypeManager.Insert(approvalPanelType);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Approval panel type setup successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        panelTypeNameTextBox.Text = "";
                        LoadAllApprovalPanelTypes();
                    }
                    else
                    {
                        MessageBox.Show(@"Approval panel type setup failed.", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                    }
                }
                else
                {
                    approvalPanelType.Id = _updateableApprovalPanelType.Id;
                    approvalPanelType.CreatedOn = _updateableApprovalPanelType.CreatedOn;
                    bool isUpdated = _approvalPanelTypeManager.Edit(approvalPanelType);
                    if (isUpdated)
                    {
                        MessageBox.Show(@"Approval panel type setup updated successful.", @"Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        panelTypeNameTextBox.Text = "";
                        LoadAllApprovalPanelTypes();
                        _isUpdateMode = false;
                        saveButton.Text = @"Save";
                    }
                    else
                    {
                        MessageBox.Show(@"Approval panel type setup failed.", @"Error!", MessageBoxButtons.OK,
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
            if (string.IsNullOrEmpty(panelTypeNameTextBox.Text))
            {
                errorMessage += "Panel type name is not provided." + Environment.NewLine;
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

        //private void LoadAllApprovalPanelTypes()
        //{
        //    var approvalPanelTypes = _approvalPanelTypeManager.GetAll();
        //    int serial = 1;
        //    approvalPanelTypeListView.Items.Clear();
        //    foreach (var approvalPanelType in approvalPanelTypes)
        //    {
        //        ListViewItem item = new ListViewItem(serial.ToString());
        //        item.SubItems.Add(approvalPanelType.Name);

        //        item.Tag = approvalPanelType;
        //        approvalPanelTypeListView.Items.Add(item);
        //        serial++;
        //    }
        //}

        private void LoadAllApprovalPanelTypes()
        {
            var approvalPanelTypes = _approvalPanelTypeManager.GetAll();
            int serial = 1;
            approvalPeanelTypeGridView.Rows.Clear();
            foreach (var approvalPanelType in approvalPanelTypes)
            {
               DataGridViewRow row=new DataGridViewRow();
                row.CreateCells(approvalPeanelTypeGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = serial.ToString();
                row.Cells[2].Value = approvalPanelType.Name;
              row.Tag = approvalPanelType;
                approvalPeanelTypeGridView.Rows.Add(row);
                serial++;
            }
        }

        private void ApprovalPanelTypeSetupUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllApprovalPanelTypes();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void editToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (approvalPanelTypeListView.SelectedItems.Count > 0)
        //        {
        //            _isUpdateMode = true;
        //            var approvalPanelType = approvalPanelTypeListView.SelectedItems[0].Tag as ApprovalPanelType;
        //            if (approvalPanelType != null)
        //            {
        //                _updateableApprovalPanelType = approvalPanelType;
        //                panelTypeNameTextBox.Text = approvalPanelType.Name;
        //                saveButton.Text = @"Update";
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show(@"Please select an item to edit.", @"Warning", MessageBoxButtons.OK,
        //               MessageBoxIcon.Warning);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

       private void approveanelTypeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (approvalPeanelTypeGridView.SelectedRows.Count > 0)
                    {
                        _isUpdateMode = true;
                        var approvalPanelType = approvalPeanelTypeGridView.SelectedRows[0].Tag as ApprovalPanelType;
                        if (approvalPanelType != null)
                        {
                            _updateableApprovalPanelType = approvalPanelType;
                            panelTypeNameTextBox.Text = approvalPanelType.Name;
                            saveButton.Text = @"Update";
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please select an item to edit.", @"Warning", MessageBoxButtons.OK,
                           MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       private void resetButton_Click(object sender, EventArgs e)
       {
           try
           {
               Clear();
               _isUpdateMode = false;
               saveButton.Text = @"Save";
           }
           catch (Exception exception)
           {
               MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }

        private void Clear()
        {
            panelTypeNameTextBox.Text = "";
        }
    }
}
