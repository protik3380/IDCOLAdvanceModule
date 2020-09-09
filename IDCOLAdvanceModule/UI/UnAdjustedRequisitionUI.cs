using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.UI._360View;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class UnadjustedRequisitionUI : Form
    {
        public UnadjustedRequisitionUI()
        {
            InitializeComponent();
            InitializeColumnSorter();
        }

        private void InitializeColumnSorter()
        {
            //Utility.ListViewColumnSorter requisitionColumnSorter = new Utility.ListViewColumnSorter();
            //advanceRequisitionListView.ListViewItemSorter = requisitionColumnSorter;
        }

        public UnadjustedRequisitionUI(ICollection<Advance_VW_GetAdvanceRequisition> requisitions)
            : this()
        {
            LoadRequisitionGridView(requisitions);
        }

        //private void LoadRequisitionListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitions)
        //{
        //    advanceRequisitionListView.Items.Clear();
        //    if (requisitions != null && requisitions.Any())
        //    {
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitions)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(criteriaVm.EmployeeName);
        //            item.SubItems.Add(criteriaVm.RequisitionCategoryName);
        //            item.SubItems.Add(criteriaVm.RequisitionDate != null
        //                ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.FromDate != null
        //                ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.ToDate != null
        //                ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(criteriaVm.AdvanceAmountInBDT.Value.ToString("N"));

        //            item.Tag = criteriaVm;
        //            advanceRequisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadRequisitionGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitions)
        {
            requisitionDataGridView.Rows.Clear();
            if (requisitions != null && requisitions.Any())
            {
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition criteriaVm in requisitions)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(requisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = serial.ToString();
                    row.Cells[2].Value = criteriaVm.EmployeeName;
                    row.Cells[3].Value = criteriaVm.RequisitionCategoryName;
                    row.Cells[4].Value = criteriaVm.RequisitionDate != null
                        ? criteriaVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[5].Value = criteriaVm.FromDate != null
                        ? criteriaVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[6].Value = criteriaVm.ToDate != null
                        ? criteriaVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                   
                    row.Tag = criteriaVm;
                    requisitionDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ShowRequisition360View(advanceRequisitionListView);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //}
        }

        private void ShowRequisition360View(DataGridView gridView)
        {
            try
            {
                if (gridView != null && gridView.SelectedRows.Count > 0)
                {
                    var selectedItem = gridView.SelectedRows[0];

                    var requisition = selectedItem.Tag as Advance_VW_GetAdvanceRequisition;
                    if (requisition == null)
                    {
                        throw new Exception("Selected requisition has no data");
                    }

                    Requisition360ViewUI expense360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
                    expense360ViewUi.Show();
                }
                else
                {
                    MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        ShowRequisition360View(requisitionDataGridView);
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
