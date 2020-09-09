using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.UI._360View;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class RequisitionListUI : Form
    {
        private readonly ICollection<Advance_VW_GetAdvanceRequisition> _requisitionVmList;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;

        private RequisitionListUI()
        {
            InitializeComponent();
            InitializeColumnSorter();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _requisitionVmList = new List<Advance_VW_GetAdvanceRequisition>();
        }

        private void InitializeColumnSorter()
        {
            //Utility.ListViewColumnSorter requisitionColumnSorter = new Utility.ListViewColumnSorter();
            //requisitionListView.ListViewItemSorter = requisitionColumnSorter;
        }

        public RequisitionListUI(ICollection<Advance_VW_GetAdvanceRequisition> requisitionVmList)
            : this()
        {
            _requisitionVmList = requisitionVmList;
        }

        public RequisitionListUI(ICollection<AdvanceRequisitionHeader> headers)
            : this()
        {
            foreach (AdvanceRequisitionHeader requisitionHeader in headers)
            {
                Advance_VW_GetAdvanceRequisition requisitionVm =
                    _advanceVwGetAdvanceRequisitionManager.GetByRequisitionHeaderId(requisitionHeader.Id);
                _requisitionVmList.Add(requisitionVm);
            }
        }

        private void RequisitionListUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadRequisitionGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void LoadRequisitionListView()
        //{
        //    requisitionListView.Items.Clear();
        //    if (_requisitionVmList != null && _requisitionVmList.Any())
        //    {
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition requisition in _requisitionVmList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(requisition.RequisitionNo);
        //            item.SubItems.Add(requisition.EmployeeName);
        //            item.SubItems.Add(requisition.RequisitionCategoryName);
        //            item.SubItems.Add(requisition.RequisitionDate != null
        //                ? requisition.RequisitionDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisition.FromDate != null
        //                ? requisition.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisition.ToDate != null
        //                ? requisition.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisition.AdvanceAmount.ToString("N"));

        //            item.Tag = requisition;
        //            requisitionListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadRequisitionGridView()
        {
            requisitionDataGridView.Rows.Clear();
            if (_requisitionVmList != null && _requisitionVmList.Any())
            {
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition requisition in _requisitionVmList)
                {
                    DataGridViewRow row=new DataGridViewRow();
                    row.CreateCells(requisitionDataGridView);

                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = serial.ToString();
                    row.Cells[2].Value = requisition.RequisitionNo;
                    row.Cells[3].Value = requisition.EmployeeName;
                    row.Cells[4].Value = requisition.RequisitionCategoryName;
                    row.Cells[5].Value = requisition.RequisitionDate != null
                        ? requisition.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[6].Value = requisition.FromDate != null
                        ? requisition.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[7].Value = requisition.ToDate != null
                        ? requisition.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = requisition.AdvanceAmount.ToString("N");
                    row.Tag = requisition;
                    requisitionDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }



        private void show360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                ShowRequisition360View(requisitionDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void ShowRequisition360View(ListView listView)
        //{
        //    if (listView != null && listView.SelectedItems.Count > 0)
        //    {
        //        var selectedItem = listView.SelectedItems[0];

        //        var requisition = selectedItem.Tag as Advance_VW_GetAdvanceRequisition;
        //        if (requisition == null)
        //        {
        //            throw new Exception("Selected requisition has no data");
        //        }

        //        Requisition360ViewUI expense360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
        //        expense360ViewUi.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //    }
        //}

        private void ShowRequisition360View(DataGridView gridView)
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
