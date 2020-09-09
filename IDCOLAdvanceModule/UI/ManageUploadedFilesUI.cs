using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class ManageUploadedFilesUI : Form
    {
        private readonly ICollection<RequisitionFile> _requisitionFiles;
        private readonly ICollection<ExpenseFile> _expenseFiles;
        private readonly bool _isUpdateMode;

        public ManageUploadedFilesUI()
        {
            InitializeComponent();
        }

        public ManageUploadedFilesUI(ICollection<RequisitionFile> requisitionFiles, bool isUpdateMode)
            : this()
        {
            _requisitionFiles = requisitionFiles;
            _isUpdateMode = isUpdateMode;
            LoadUploadedFilesGridView(_requisitionFiles);
        }

        public ManageUploadedFilesUI(ICollection<ExpenseFile> expenseFiles, bool isUpdateMode)
            : this()
        {
            _expenseFiles = expenseFiles;
            _isUpdateMode = isUpdateMode;
            LoadUploadedFilesGridView(_expenseFiles);
        }

        //private void LoadUploadedFilesListView(ICollection<RequisitionFile> requisitionFiles)
        //{
        //    uploadedFilesListView.Items.Clear();
        //    foreach (RequisitionFile requisitionFile in requisitionFiles)
        //    {
        //        if (!requisitionFile.IsDeleted)
        //        {
        //            ListViewItem item = new ListViewItem(requisitionFile.FileName);
        //            item.Tag = requisitionFile;
        //            uploadedFilesListView.Items.Add(item);
        //        }
        //    }
        //}

        private void LoadUploadedFilesGridView(ICollection<RequisitionFile> requisitionFiles)
        {
            uploadedFilesDataGridView.Rows.Clear();
            foreach (RequisitionFile requisitionFile in requisitionFiles)
            {
                if (!requisitionFile.IsDeleted)
                {
                    DataGridViewRow row=new DataGridViewRow();
                    row.CreateCells(uploadedFilesDataGridView);

                    row.Cells[0].Value = "Download";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = requisitionFile.FileName;
                    row.Tag = requisitionFile;
                    uploadedFilesDataGridView.Rows.Add(row);
                }
            }
        }

        //private void LoadUploadedFilesListView(ICollection<ExpenseFile> expenseFiles)
        //{
        //    uploadedFilesListView.Items.Clear();
        //    foreach (ExpenseFile expenseFile in expenseFiles)
        //    {
        //        if (!expenseFile.IsDeleted)
        //        {
        //            ListViewItem item = new ListViewItem(expenseFile.FileName);
        //            item.Tag = expenseFile;
        //            uploadedFilesListView.Items.Add(item);
        //        }
        //    }
        //}

        private void LoadUploadedFilesGridView(ICollection<ExpenseFile> expenseFiles)
        {
            uploadedFilesDataGridView.Rows.Clear();
            foreach (ExpenseFile expenseFile in expenseFiles)
            {
                if (!expenseFile.IsDeleted)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(uploadedFilesDataGridView);

                    row.Cells[0].Value = "Download";
                    row.Cells[1].Value = "Remove";
                    row.Cells[2].Value = expenseFile.FileName;
                    row.Tag = expenseFile;
                    uploadedFilesDataGridView.Rows.Add(row);
                }
            }
        }
       

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveFileFromList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveFileFromList()
        {
            if (uploadedFilesDataGridView.SelectedRows.Count > 0)
            {
                if (_requisitionFiles != null)
                {
                    var file = uploadedFilesDataGridView.SelectedRows[0].Tag as RequisitionFile;
                    if (!_isUpdateMode)
                    {
                        _requisitionFiles.Remove(file);
                    }
                    else if (file != null)
                    {
                        file.IsDeleted = true;
                        file.DeletedBy = Session.LoginUserName;
                        file.DeletedOn = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show(@"File not found to remove.", @"Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                if (_expenseFiles != null)
                {
                    var file = uploadedFilesDataGridView.SelectedRows[0].Tag as ExpenseFile;
                    if (!_isUpdateMode)
                    {
                        _expenseFiles.Remove(file);
                    }
                    else if (file != null)
                    {
                        file.IsDeleted = true;
                        file.DeletedBy = Session.LoginUserName;
                        file.DeletedOn = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show(@"File not found to remove.", @"Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                //uploadedFilesDataGridView.SelectedRows[0].Remove();
                if (uploadedFilesDataGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewCell oneCell in uploadedFilesDataGridView.SelectedCells)
                    {
                        if (oneCell.Selected)
                            uploadedFilesDataGridView.Rows.RemoveAt(oneCell.RowIndex);
                    }
                }
            }
            else
            {
                MessageBox.Show(@"No file is selected to remove.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveFileFromList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void DownloadFileFromList()
        {
            if (uploadedFilesDataGridView.SelectedRows.Count > 0)
            {
                if (_requisitionFiles != null)
                {
                    var file = uploadedFilesDataGridView.SelectedRows[0].Tag as RequisitionFile;
                    if (file == null)
                    {
                        throw new UiException("File not found.");
                    }
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Title = @"Select your destination folder",
                        Filter = Utility.Utility.SupportedFileFormat,
                        FileName = file.FileName,
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        RestoreDirectory = true
                    };
                    DialogResult dialogResult = saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        File.Copy(file.FileLocation, saveFileDialog.FileName, true);
                    }
                }
                if (_expenseFiles != null)
                {
                    var file = uploadedFilesDataGridView.SelectedRows[0].Tag as ExpenseFile;
                    if (file == null)
                    {
                        throw new UiException("File not found.");
                    }
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Title = @"Select your destination folder",
                        Filter = Utility.Utility.SupportedFileFormat,
                        FileName = file.FileName,
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        RestoreDirectory = true
                    };
                    DialogResult dialogResult = saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        File.Copy(file.FileLocation, saveFileDialog.FileName, true);
                    }
                }
            }
            else
            {
                MessageBox.Show(@"No file is selected to download.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadFileFromList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uploadedFilesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        DownloadFileFromList(); 
                    }
                    if (e.ColumnIndex == 1)
                    {
                        RemoveFileFromList();
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
