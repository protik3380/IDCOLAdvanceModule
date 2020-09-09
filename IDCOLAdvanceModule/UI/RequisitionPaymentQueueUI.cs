using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.UI.Entry;
using IDCOLAdvanceModule.UI.Voucher;
using IDCOLAdvanceModule.UI._360View;
using IDCOLAdvanceModule.Utility;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class RequisitionPaymentQueueUI : Form
    {
        private readonly string _userName = Session.LoginUserName;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly IRequisitionVoucherHeaderManager _requisitionVoucherHeaderManager;

        public RequisitionPaymentQueueUI()
        {
            InitializeComponent();
            InitializeColumnSorter();
            voucherTabControl.SelectedTab = requisitionTabPage;
            _approvalProcessManager = new ApprovalProcessManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _departmentManager = new DepartmentManager();
            _requisitionVoucherHeaderManager = new RequisitionVoucherHeaderManager();
        }

        private void InitializeColumnSorter()
        {
            Utility.ListViewColumnSorter requisitionColumnSorter = new Utility.ListViewColumnSorter();
            // advanceRequisitionSearchListView.ListViewItemSorter = requisitionColumnSorter;
            Utility.ListViewColumnSorter paidRequisitionColumnSorter = new Utility.ListViewColumnSorter();
           // advancePaidRequisitionSearchListView.ListViewItemSorter = paidRequisitionColumnSorter;
        }

        private void LoadAdvanceRequisitionCategoryComboBox()
        {
            advanceRequisitionCategoryComboBox.DataSource = null;
            List<AdvanceCategory> categories = new List<AdvanceCategory>
            {
                new AdvanceCategory{Id = DefaultItem.Value, Name = DefaultItem.Text}
            };
            categories.AddRange(_advanceRequisitionCategoryManager.GetAll());
            advanceRequisitionCategoryComboBox.DisplayMember = "Name";
            advanceRequisitionCategoryComboBox.ValueMember = "Id";
            advanceRequisitionCategoryComboBox.DataSource = categories;
        }

        private void LoadDepartmentComboBox()
        {
            departmentComboBox.DataSource = null;
            List<Admin_Departments> departments = new List<Admin_Departments>
            {
                new Admin_Departments{DepartmentID = DefaultItem.Value, DepartmentName = DefaultItem.Text}
            };
            departments.AddRange(_departmentManager.GetAll());
            departmentComboBox.DisplayMember = "DepartmentName";
            departmentComboBox.ValueMember = "DepartmentID";
            departmentComboBox.DataSource = departments;
        }

        //private void LoadAdvanceUnPaidRequisitionSearchListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    advanceRequisitionSearchListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderBy(c => c.RequisitionDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(requisitionVm.RequisitionCategoryName);
        //            item.SubItems.Add(requisitionVm.EmployeeName);
        //            item.SubItems.Add(requisitionVm.EmployeeDepartmentName);
        //            item.SubItems.Add(requisitionVm.RequisitionDate != null
        //                ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.FromDate != null
        //                ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.ToDate != null
        //                ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N"));

        //            item.Tag = requisitionVm;
        //            advanceRequisitionSearchListView.Items.Add(item);
        //            serial++;
        //        }
        //    }     
        //}

        private void LoadAdvanceUnPaidRequisitionSearchGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
           advanceRequisitionSearchDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderBy(c => c.RequisitionDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(advanceRequisitionSearchDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "Pay";
                    row.Cells[2].Value = "View";
                    row.Cells[3].Value = serial.ToString();
                    row.Cells[4].Value = requisitionVm.RequisitionCategoryName;
                    row.Cells[5].Value = requisitionVm.EmployeeName;
                    row.Cells[6].Value = requisitionVm.EmployeeDepartmentName;
                    row.Cells[7].Value = requisitionVm.RequisitionDate != null
                        ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[8].Value = requisitionVm.FromDate != null
                        ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[9].Value = requisitionVm.ToDate != null
                        ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A";
                    row.Cells[10].Value = Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N");
                    row.Tag = requisitionVm;
                    advanceRequisitionSearchDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        //private void LoadAdvancePaidRequisitionSearchListView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        //{
        //    advancePaidRequisitionSearchListView.Items.Clear();
        //    if (requisitionList != null && requisitionList.Any())
        //    {
        //        requisitionList = requisitionList.OrderBy(c => c.AdvanceIssueDate).ToList();
        //        int serial = 1;
        //        foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(requisitionVm.RequisitionCategoryName);
        //            item.SubItems.Add(requisitionVm.EmployeeName);
        //            item.SubItems.Add(requisitionVm.EmployeeDepartmentName);
        //            item.SubItems.Add(requisitionVm.RequisitionDate != null
        //                ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.FromDate != null
        //                ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(requisitionVm.ToDate != null
        //                ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
        //                : "N/A");
        //            item.SubItems.Add(Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N"));

        //            item.Tag = requisitionVm;
        //            advancePaidRequisitionSearchListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadAdvancePaidRequisitionSearchGridView(ICollection<Advance_VW_GetAdvanceRequisition> requisitionList)
        {
            advancePaidRequisitionSearchDataGridView.Rows.Clear();
            if (requisitionList != null && requisitionList.Any())
            {
                requisitionList = requisitionList.OrderBy(c => c.AdvanceIssueDate).ToList();
                int serial = 1;
                foreach (Advance_VW_GetAdvanceRequisition requisitionVm in requisitionList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(advancePaidRequisitionSearchDataGridView);
                    row.Cells[0].Value = "360 View";
                    row.Cells[1].Value = "Payment Entry";
                    row.Cells[2].Value =serial.ToString();
                    row.Cells[3].Value =requisitionVm.RequisitionCategoryName;
                    row.Cells[4].Value = requisitionVm.EmployeeName;
                    row.Cells[5].Value = requisitionVm.EmployeeDepartmentName;
                    row.Cells[6].Value =requisitionVm.RequisitionDate != null
                        ? requisitionVm.RequisitionDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A" ;
                    row.Cells[7].Value =requisitionVm.FromDate != null
                        ? requisitionVm.FromDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A" ;
                    row.Cells[8].Value =requisitionVm.ToDate != null
                        ? requisitionVm.ToDate.Date.ToString("dd-MMM-yyyy")
                        : "N/A" ;
                    row.Cells[9].Value =Convert.ToDouble(requisitionVm.AdvanceAmountInBDT).ToString("N") ;
                   
                    row.Tag = requisitionVm;
                    advancePaidRequisitionSearchDataGridView.Rows.Add(row);
                    serial++;
                   
                }
            }
        }

        private void GoToRequisition360ViewUi()
        {
            if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
            {
                var requisition =
                    advanceRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (requisition == null)
                {
                    throw new UiException("Selected requisition has no data.");
                }
                var requisition360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
                requisition360ViewUi.Show();
            }
            else
            {
                MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void GoToPaidRequisition360ViewUi()
        {
            if (advancePaidRequisitionSearchDataGridView.SelectedRows.Count > 0)
            {
                var requisition =
                    advancePaidRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (requisition == null)
                {
                    throw new UiException("Selected requisition has no data.");
                }
                var requisition360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
                requisition360ViewUi.Show();
            }
            else
            {
                MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Search()
        {
            AdvanceRequisitionSearchCriteria criteria = new AdvanceRequisitionSearchCriteria();
            if (Convert.ToInt64(advanceRequisitionCategoryComboBox.SelectedValue) != DefaultItem.Value)
            {
                criteria.AdvanceCategoryId = Convert.ToInt64(departmentComboBox.SelectedValue);
            }
            if (Convert.ToInt64(departmentComboBox.SelectedValue) != DefaultItem.Value)
            {
                criteria.DepartmentId = departmentComboBox.SelectedValue as decimal?;
            }
            criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?)null;
            criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?)null;
            criteria.EmployeeName = employeeNameTextBox.Text;

            ICollection<Advance_VW_GetAdvanceRequisition> unPaidRequisitionList =
                _approvalProcessManager.GetUnPaidRequisitionForMember(criteria);
            LoadAdvanceUnPaidRequisitionSearchGridView(unPaidRequisitionList);

            ICollection<Advance_VW_GetAdvanceRequisition> paidRequisitionList =
                _approvalProcessManager.GetPaidRequisitionForMember(criteria);
            LoadAdvancePaidRequisitionSearchGridView(paidRequisitionList);
        }

        private void RequisitionPaymentQueue_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAdvanceRequisitionCategoryComboBox();
                LoadPaymentData();
                LoadDepartmentComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadPaymentData()
        {
            ICollection<Advance_VW_GetAdvanceRequisition> unPaidrequisitionList =
                _approvalProcessManager.GetUnPaidRequisitionForMember();
            LoadAdvanceUnPaidRequisitionSearchGridView(unPaidrequisitionList);

            ICollection<Advance_VW_GetAdvanceRequisition> paidRequisitionList =
                _approvalProcessManager.GetPaidRequisitionForMember();
            LoadAdvancePaidRequisitionSearchGridView(paidRequisitionList);
            LoadVoucher();
        }

        //private void LoadVoucher()
        //{
        //    List<RequisitionVoucherHeader> requisitionVoucherHeaders = new List<RequisitionVoucherHeader>();

        //    var isDraftSelected = draftRadioButton.Checked;
        //    if (isDraftSelected)
        //    {
        //        requisitionVoucherListView.ContextMenuStrip = draftVoucherContextMenu;
        //        requisitionVoucherHeaders = _requisitionVoucherHeaderManager.GetAllDraftVoucher().ToList();
        //    }
        //    else
        //    {
        //        requisitionVoucherListView.ContextMenuStrip = sentVoucherContextMenu;

        //        requisitionVoucherHeaders = _requisitionVoucherHeaderManager.GetAllSentVoucher().ToList();
        //    }
        //    LoadRequisitionVoucherRequisitionVoucherGridView(requisitionVoucherHeaders);
        //}

        private void LoadVoucher()
        {
            List<RequisitionVoucherHeader> requisitionVoucherHeaders = new List<RequisitionVoucherHeader>();

            var isDraftSelected = draftRadioButton.Checked;
            if (isDraftSelected)
            {
                //requisitionVoucherListView.ContextMenuStrip = draftVoucherContextMenu;
                requisitionVoucherHeaders = _requisitionVoucherHeaderManager.GetAllDraftVoucher().ToList();
                LoadDraftRequisitionVoucherGridView(requisitionVoucherHeaders);
            }
            else
            {
                //requisitionVoucherListView.ContextMenuStrip = sentVoucherContextMenu;  
                requisitionVoucherHeaders = _requisitionVoucherHeaderManager.GetAllSentVoucher().ToList();
                LoadSendRequisitionVoucherGridView(requisitionVoucherHeaders);
            }
            
        }

        //private void LoadRequisitionVoucherRequisitionVoucherListView(ICollection<RequisitionVoucherHeader> requisitionVoucherHeaders)
        //{
        //    requisitionVoucherListView.Items.Clear();
        //    if (requisitionVoucherHeaders != null && requisitionVoucherHeaders.Any())
        //    {
        //        requisitionVoucherHeaders = requisitionVoucherHeaders.OrderBy(c => c.VoucherEntryDate).ToList();
        //        int serial = 1;
        //        foreach (var voucher in requisitionVoucherHeaders)
        //        {
        //            ListViewItem item = new ListViewItem(serial.ToString());
        //            item.SubItems.Add(voucher.RequisitionHeader.RequisitionNo);
        //            item.SubItems.Add(voucher.VoucherDescription);
        //            item.SubItems.Add(voucher.GetTotalDrAmount() != null
        //                ? voucher.GetTotalDrAmount().Value.ToString("N")
        //                : null);
        //            item.SubItems.Add(voucher.GetTotalCrAmount() != null
        //                ? voucher.GetTotalCrAmount().Value.ToString("N")
        //                : null);
        //            item.Tag = voucher;
        //            requisitionVoucherListView.Items.Add(item);
        //            serial++;
        //        }
        //    }
        //}

        private void LoadDraftRequisitionVoucherGridView(ICollection<RequisitionVoucherHeader> requisitionVoucherHeaders)
        {
            requisitionVoucherDataGridView.Rows.Clear();
            if (requisitionVoucherHeaders != null && requisitionVoucherHeaders.Any())
            {
                requisitionVoucherHeaders = requisitionVoucherHeaders.OrderBy(c => c.VoucherEntryDate).ToList();
                int serial = 1;
                foreach (var voucher in requisitionVoucherHeaders)
                {
                    DataGridViewRow row=new DataGridViewRow();
                    row.CreateCells(requisitionVoucherDataGridView);

                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Preview and Send Voucher";
                    row.Cells[2].Value = serial.ToString();
                    row.Cells[3].Value = voucher.RequisitionHeader.RequisitionNo;
                    row.Cells[4].Value = voucher.VoucherDescription;
                    row.Cells[5].Value = voucher.GetTotalDrAmount() != null
                        ? voucher.GetTotalDrAmount().Value.ToString("N")
                        : null;
                    row.Cells[6].Value = voucher.GetTotalCrAmount() != null
                        ? voucher.GetTotalCrAmount().Value.ToString("N")
                        : null;
                    row.Tag = voucher;
                    requisitionVoucherDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void LoadSendRequisitionVoucherGridView(ICollection<RequisitionVoucherHeader> requisitionVoucherHeaders)
        {
            requisitionVoucherDataGridView.Rows.Clear();
            if (requisitionVoucherHeaders != null && requisitionVoucherHeaders.Any())
            {
                requisitionVoucherHeaders = requisitionVoucherHeaders.OrderBy(c => c.VoucherEntryDate).ToList();
                int serial = 1;
                foreach (var voucher in requisitionVoucherHeaders)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(requisitionVoucherDataGridView);
                     row.Cells[0]=new DataGridViewTextBoxCell();
                      row.Cells[1]=new DataGridViewTextBoxCell();

                      row.Cells[2].Value = serial.ToString();
                      row.Cells[3].Value = voucher.RequisitionHeader.RequisitionNo;
                      row.Cells[4].Value = voucher.VoucherDescription;
                      row.Cells[5].Value = voucher.GetTotalDrAmount() != null
                          ? voucher.GetTotalDrAmount().Value.ToString("N")
                          : null;
                      row.Cells[6].Value = voucher.GetTotalCrAmount() != null
                          ? voucher.GetTotalCrAmount().Value.ToString("N")
                          : null;
                    row.Tag = voucher;
                    requisitionVoucherDataGridView.Rows.Add(row);
                    serial++;
                }
            }
        }

        private void requisition360ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void advanceRequisitionSearchListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
            //    {
            //        payRequisitionButton.Visible = true;
            //        requisition360ViewButton.Visible = true;
            //    }
            //    else
            //    {
            //        payRequisitionButton.Visible = false;
            //        requisition360ViewButton.Visible = false;
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //}
        }

        private void requisition360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToRequisition360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void paymentEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PayAgainstRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void PayAgainstRequisition()
        {
            if (advancePaidRequisitionSearchDataGridView.SelectedRows.Count > 0)
            {
                var selectedRequisition =
                    advancePaidRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (selectedRequisition != null)
                {
                    AdvanceRequisitionHeader header =
                        _advanceRequisitionHeaderManager.GetById(selectedRequisition.HeaderId);
                    RecipientPayeeListUI recipientPayeeListUi = new RecipientPayeeListUI(header);
                    recipientPayeeListUi.ShowDialog();
                    LoadPaymentData();
                }
            }
            else
            {
                MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void paidRequisition360ViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoToPaidRequisition360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void requisitionPaymentEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                PayAgainstRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void payToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void payRequisitionButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenSelectPaymentDateUiForRequisition();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OpenSelectPaymentDateUiForRequisition()
        {
            //if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
            //{
            //    var selectedRequisition =
            //        advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
            //    if (selectedRequisition != null)
            //    {
            //        SelectPaymentDateUI selectPaymentDateUi = new SelectPaymentDateUI();
            //        selectPaymentDateUi.ShowDialog();
            //        if (selectPaymentDateUi.IsPayButtonClicked)
            //        {
            //            DateTime selectedPaymentDate = selectPaymentDateUi.paymentDateMetroDateTime.Value;
            //            bool isPaid = _advanceRequisitionHeaderManager.PayRequisition(selectedRequisition.HeaderId, Session.LoginUserName,
            //                selectedPaymentDate);
            //            if (isPaid)
            //            {
            //                MessageBox.Show(@"Payment date insertion successful. Please find it in the 'Paid Requisition' tab.", @"Success", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Information);
            //                LoadPaymentData();
            //            }
            //            else
            //            {
            //                MessageBox.Show(@"Payment date insertion failed.", @"Warning!", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
            //}
            if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
            {
                var selectedRequisition =
                    advanceRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                if (selectedRequisition != null)
                {
                    SelectPaymentDateUI selectPaymentDateUi = new SelectPaymentDateUI();
                    selectPaymentDateUi.ShowDialog();
                    if (selectPaymentDateUi.IsPayButtonClicked)
                    {
                        DateTime selectedPaymentDate = selectPaymentDateUi.paymentDateMetroDateTime.Value;
                        bool isPaid = _advanceRequisitionHeaderManager.PayRequisition(selectedRequisition.HeaderId, Session.LoginUserName,
                            selectedPaymentDate);
                        if (isPaid)
                        {
                            MessageBox.Show(@"Payment date insertion successful. Please find it in the 'Paid Requisition' tab.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            LoadPaymentData();
                        }
                        else
                        {
                            MessageBox.Show(@"Payment date insertion failed.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Please select a requisition.", @"Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void advancePaidRequisitionSearchListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //if (advancePaidRequisitionSearchListView.SelectedItems.Count > 0)
                //{
                //    paidRequisition360ViewButton.Visible = true;
                //    requisitionPaymentEntryButton.Visible = true;
                //}
                //else
                //{
                //    paidRequisition360ViewButton.Visible = false;
                //    requisitionPaymentEntryButton.Visible = false;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceRequisitionSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Utility.Utility.SortColumn(e, advanceRequisitionSearchListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancePaidRequisitionSearchListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //Utility.Utility.SortColumn(e, advancePaidRequisitionSearchListView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void paid360ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GoToPaidRequisition360ViewUi();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void editVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (requisitionVoucherListView.SelectedItems.Count > 0)
                //{
                //    var requisitionVoucherHeader =
                //        requisitionVoucherListView.SelectedItems[0].Tag as RequisitionVoucherHeader;
                //    if (requisitionVoucherHeader == null)
                //    {
                //        throw new UiException("Voucher header not tagged.");
                //    }
                //    RequisitionVoucherEntryUI requisitionVoucherEntryUi = new RequisitionVoucherEntryUI(requisitionVoucherHeader, AdvancedFormMode.Update);
                //    requisitionVoucherEntryUi.Show();
                //    Search();
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item", @"Error!", MessageBoxButtons.OK,
                //   MessageBoxIcon.Error);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void previewVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (requisitionVoucherListView.SelectedItems.Count > 0)
                //{
                //    var voucher = requisitionVoucherListView.SelectedItems[0].Tag as RequisitionVoucherHeader;
                //    AdvanceVoucherGenerateManager voucherGenerateManager = new AdvanceVoucherGenerateManager();
                //    var transactionVoucher = voucherGenerateManager.GetTransactionVoucherFromRequisitionVoucher(voucher);
                //    PreviewVoucherUI voucherUi = new PreviewVoucherUI(transactionVoucher, voucher);
                //    voucherUi.ShowDialog();
                //    LoadVoucher();
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void draftRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadVoucher();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        } 

      
       
        private void advanceRequisitionSearchDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
                {
                    payRequisitionButton.Visible = true;
                    requisition360ViewButton.Visible = true;
                }
                else
                {
                    payRequisitionButton.Visible = false;
                    requisition360ViewButton.Visible = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceRequisitionSearchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        GoToRequisition360ViewUi();
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        OpenSelectPaymentDateUiForRequisition();
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                advanceRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            if (requisition == null)
                            {
                                throw new UiException("Selected requisition has no data.");
                            }
                            RequisitionViewUI requisitionViewUi = new RequisitionViewUI(requisition.HeaderId);
                            requisitionViewUi.Show();
                        }
                        else
                        {
                            MessageBox.Show(@"Please select a requisition to view.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancePaidRequisitionSearchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        GoToPaidRequisition360ViewUi(); 
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        PayAgainstRequisition();  
                    }
                   
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void requisitionVoucherDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (requisitionVoucherDataGridView.SelectedRows.Count > 0)
                        {
                            var requisitionVoucherHeader =
                                requisitionVoucherDataGridView.SelectedRows[0].Tag as RequisitionVoucherHeader;
                            if (requisitionVoucherHeader == null)
                            {
                                throw new UiException("Voucher header not tagged.");
                            }
                            RequisitionVoucherEntryUI requisitionVoucherEntryUi = new RequisitionVoucherEntryUI(requisitionVoucherHeader, AdvancedFormMode.Update);
                            requisitionVoucherEntryUi.Show();
                            Search();
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item", @"Error!", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                        }  
                    }
                    else if (e.ColumnIndex == 1)
                    {
                        if (requisitionVoucherDataGridView.SelectedRows.Count > 0)
                        {
                            var voucher = requisitionVoucherDataGridView.SelectedRows[0].Tag as RequisitionVoucherHeader;
                            AdvanceVoucherGenerateManager voucherGenerateManager = new AdvanceVoucherGenerateManager();
                            var transactionVoucher = voucherGenerateManager.GetTransactionVoucherFromRequisitionVoucher(voucher);
                            PreviewVoucherUI voucherUi = new PreviewVoucherUI(transactionVoucher, voucher);
                            voucherUi.ShowDialog();
                            LoadVoucher();
                        }
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
