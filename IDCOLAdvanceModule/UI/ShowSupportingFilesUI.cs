using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class ShowSupportingFilesUI : Form
    {
        private readonly ICollection<RequisitionFile> _requisitionFiles;
        private readonly ICollection<ExpenseFile> _expenseFiles;

        public ShowSupportingFilesUI()
        {
            InitializeComponent();
        }

        public ShowSupportingFilesUI(ICollection<RequisitionFile> requisitionFiles)
            : this()
        {
            _requisitionFiles = requisitionFiles.Where(c => !c.IsDeleted).ToList();
            LoadUploadedFilesGridView(_requisitionFiles);
        }

        public ShowSupportingFilesUI(ICollection<ExpenseFile> expenseFiles)
            : this()
        {
            _expenseFiles = expenseFiles.Where(c => !c.IsDeleted).ToList();
            LoadUploadedFilesGridView(_expenseFiles);
        }

        //private void LoadUploadedFilesListView(ICollection<RequisitionFile> requisitionFiles)
        //{
        //    uploadedFilesListView.Items.Clear();
        //    foreach (RequisitionFile requisitionFile in requisitionFiles)
        //    {
        //        ListViewItem item = new ListViewItem(requisitionFile.FileName);
        //        item.Tag = requisitionFile;
        //        uploadedFilesListView.Items.Add(item);
        //    }
        //}

        private void LoadUploadedFilesGridView(ICollection<RequisitionFile> requisitionFiles)
        {
            supportingFileDataGridView.Rows.Clear();
            foreach (RequisitionFile requisitionFile in requisitionFiles)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(supportingFileDataGridView);

                row.Cells[0].Value = "Download";
                row.Cells[1].Value = requisitionFile.FileName;
                row.Tag = requisitionFile;
                supportingFileDataGridView.Rows.Add(row);
            }
        }

        //private void LoadUploadedFilesListView(ICollection<ExpenseFile> expenseFiles)
        //{
        //    uploadedFilesListView.Items.Clear();
        //    foreach (ExpenseFile expenseFile in expenseFiles)
        //    {
        //        ListViewItem item = new ListViewItem(expenseFile.FileName);
        //        item.Tag = expenseFile;
        //        uploadedFilesListView.Items.Add(item);
        //    }
        //}

        private void LoadUploadedFilesGridView(ICollection<ExpenseFile> expenseFiles)
        {
            supportingFileDataGridView.Rows.Clear();
            foreach (ExpenseFile expenseFile in expenseFiles)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(supportingFileDataGridView);

                row.Cells[0].Value = "Download";
                row.Cells[1].Value = expenseFile.FileName;
                row.Tag = expenseFile;
                supportingFileDataGridView.Rows.Add(row); 
               
            }
        }
        private void uploadedFilesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (uploadedFilesListView.SelectedItems.Count > 0)
                //{
                //    downloadButton.Visible = true;
                //}
                //else
                //{
                //    downloadButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadFileFromList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void DownloadFileFromList()
        //{
        //    if (uploadedFilesListView.SelectedItems.Count > 0)
        //    {
        //        if (_requisitionFiles != null)
        //        {
        //            var file = uploadedFilesListView.SelectedItems[0].Tag as RequisitionFile;
        //            if (file == null)
        //            {
        //                throw new UiException("File not found.");
        //            }
        //            SaveFileDialog saveFileDialog = new SaveFileDialog
        //            {
        //                Title = @"Select your destination folder",
        //                Filter = Utility.Utility.SupportedFileFormat,
        //                FileName = file.FileName,
        //                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        //                RestoreDirectory = true
        //            };
        //            DialogResult dialogResult = saveFileDialog.ShowDialog();
        //            if (dialogResult == DialogResult.OK)
        //            {
        //                File.Copy(file.FileLocation, saveFileDialog.FileName, true);
        //            }
        //        }
        //        if (_expenseFiles != null)
        //        {
        //            var file = uploadedFilesListView.SelectedItems[0].Tag as ExpenseFile;
        //            if (file == null)
        //            {
        //                throw new UiException("File not found.");
        //            }
        //            SaveFileDialog saveFileDialog = new SaveFileDialog
        //            {
        //                Title = @"Select your destination folder",
        //                Filter = Utility.Utility.SupportedFileFormat,
        //                FileName = file.FileName,
        //                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        //                RestoreDirectory = true
        //            };
        //            DialogResult dialogResult = saveFileDialog.ShowDialog();
        //            if (dialogResult == DialogResult.OK)
        //            {
        //                File.Copy(file.FileLocation, saveFileDialog.FileName, true);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"No file is selected to download.", @"Warning!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //    }
        //}

        private void DownloadFileFromList()
        {
            if (supportingFileDataGridView.SelectedRows.Count > 0)
            {
                if (_requisitionFiles != null)
                {
                    var file = supportingFileDataGridView.SelectedRows[0].Tag as RequisitionFile;
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
                    var file = supportingFileDataGridView.SelectedRows[0].Tag as ExpenseFile;
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
        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void downloadToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    var senderGrid = (DataGridView)sender;
            //    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            //    {
            //        if (e.ColumnIndex == 0)
            //        {
                       
            //        }
                   
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void supportingFileDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                  
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
