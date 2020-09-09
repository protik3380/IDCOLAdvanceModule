using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class SourceOfFundSetupUI : Form
    {
        private readonly ISourceOfFundManager _sourceOfFundManager;
        private bool _isUpdatemode;
        private SourceOfFund _updateableSourceOfFund;

        public SourceOfFundSetupUI()
        {
            _sourceOfFundManager = new SourceOfFundManager();
            InitializeComponent();
        }

        //private void LoadSourceOfFundListView()
        //{
        //    sourceOfFundListView.Items.Clear();
        //    ICollection<SourceOfFund> sourceOfFundList = _sourceOfFundManager.GetAll();
        //    int serial = 1;
        //    foreach (SourceOfFund sourceOfFund in sourceOfFundList)
        //    {
        //        ListViewItem item = new ListViewItem(serial.ToString());
        //        item.SubItems.Add(sourceOfFund.Name);
        //        item.Tag = sourceOfFund;
        //        sourceOfFundListView.Items.Add(item);
        //        serial++;
        //    }
        //}

        private void LoadSourceOfFundGridView()
        {
            sourceOfFundDataGridView.Rows.Clear();
            ICollection<SourceOfFund> sourceOfFundList = _sourceOfFundManager.GetAll();
            int serial = 1;
            foreach (SourceOfFund sourceOfFund in sourceOfFundList)
            {
                DataGridViewRow row=new DataGridViewRow();
                row.CreateCells(sourceOfFundDataGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = serial.ToString();
                row.Cells[2].Value = sourceOfFund.Name;
                
                row.Tag = sourceOfFund;
                sourceOfFundDataGridView.Rows.Add(row);
                serial++;
            }
        }

        private void SourceOfFundSetupUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSourceOfFundGridView();
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
                if (!_isUpdatemode)
                {
                    SourceOfFund sourceOfFund = new SourceOfFund();
                    sourceOfFund.Name = nameTextBox.Text;
                    sourceOfFund.CreatedBy = Session.LoginUserName;
                    sourceOfFund.CreatedOn = DateTime.Now;
                    bool isInserted = _sourceOfFundManager.Insert(sourceOfFund);
                    if (isInserted)
                    {
                        MessageBox.Show(@"Source of fund saved successfully.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        LoadSourceOfFundGridView();
                        ResetFormToSaveMode();
                    }
                    else
                    {
                        MessageBox.Show(@"Source of fund save failed.", @"Error!", MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }
                }
                else
                {
                    SourceOfFund sourceOfFund = _updateableSourceOfFund;
                    sourceOfFund.Name = nameTextBox.Text;
                    sourceOfFund.LastModifiedBy = Session.LoginUserName;
                    sourceOfFund.LastModifiedOn = DateTime.Now;
                    bool isUpdated = _sourceOfFundManager.Edit(sourceOfFund);
                    if (isUpdated)
                    {
                        MessageBox.Show(@"Source of fund updated successfully.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        LoadSourceOfFundGridView();
                        ResetFormToSaveMode();
                    }
                    else
                    {
                        MessageBox.Show(@"Source of fund update failed.", @"Error!", MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                errorMessage += "Source of fund is not provided." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            return true;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormToSaveMode();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetFormToSaveMode()
        {
            nameTextBox.Text = string.Empty;
            saveButton.Text = @"Save";
        }
        
        private void sourceOfFundDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (sourceOfFundDataGridView.SelectedRows.Count > 0)
                    {
                        _isUpdatemode = true;
                        saveButton.Text = @"Update";
                        DataGridViewRow row = sourceOfFundDataGridView.SelectedRows[0];
                        _updateableSourceOfFund = row.Tag as SourceOfFund;
                        if (_updateableSourceOfFund != null)
                        {
                            nameTextBox.Text = _updateableSourceOfFund.Name;
                        }
                    }
                    else
                    {
                        throw new UiException("No item is selected to edit.");
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
