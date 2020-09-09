using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.ViewModels;

namespace IDCOLAdvanceModule.UI.Voucher
{
    public partial class RecipientPayeeListUI : Form
    {
        private readonly AdvanceRequisitionHeader _requisitionHeader;
        private readonly AdvanceExpenseHeader _expenseHeader;
        private readonly UserTable _requester;
        private readonly IEmployeeManager _employeeManager;
        private readonly IRequisitionVoucherHeaderManager _requisitionVoucherHeaderManager;
        private readonly IExpenseVoucherHeaderManager _expenseVoucherHeaderManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;

        private RecipientPayeeListUI()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _requisitionVoucherHeaderManager = new RequisitionVoucherHeaderManager();
            _expenseVoucherHeaderManager = new ExpenseVoucherHeaderManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
        }

        public RecipientPayeeListUI(AdvanceRequisitionHeader requisitionHeader)
            : this()
        {
            _requisitionHeader = requisitionHeader;
            _requester = _employeeManager.GetByUserName(requisitionHeader.RequesterUserName);
        }

        public RecipientPayeeListUI(AdvanceExpenseHeader expenseHeader)
            : this()
        {
            _expenseHeader = expenseHeader;
            _requester = _employeeManager.GetByUserName(expenseHeader.RequesterUserName);
        }

        private void LoadRecipientPayeeGridView()
        {
            recipientPayeeDataGridView.Rows.Clear();
            if (_requisitionHeader != null)
            {
                List<RecipientVM> recipients = GetRecipientPayeeName(_requisitionHeader.AdvanceRequisitionDetails);
                foreach (RecipientVM recipient in recipients)
                {
                    DataGridViewRow row=new DataGridViewRow();
                    row.CreateCells(recipientPayeeDataGridView);
                    row.Cells[0].Value = "Go";
                    row.Cells[1].Value = recipient.Name;
                    row.Cells[2].Value = recipient.RequisitionVoucherHeader =
                        _requisitionVoucherHeaderManager.GetByHeaderIdAndRecipientName(_requisitionHeader.Id,
                            recipient.Name);
                    row.Tag = recipient;
                    recipientPayeeDataGridView.Rows.Add(row);
                }
            }
            else if (_expenseHeader != null)
            {
                List<RecipientVM> receipientPayeeName = GetRecipientPayeeName(_expenseHeader.AdvanceExpenseDetails);
                foreach (RecipientVM recipient in receipientPayeeName)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(recipientPayeeDataGridView);
                    row.Cells[0].Value = "Go";
                    row.Cells[1].Value = recipient.Name;
                    row.Cells[2].Value = _expenseVoucherHeaderManager.GetVoucherStatusByHeaderIdAndRecipientName(
                        _expenseHeader.Id, recipient.Name);
                    recipient.ExpenseVoucherHeader =
                        _expenseVoucherHeaderManager.GetByHeaderIdAndRecipientName(_expenseHeader.Id,
                            recipient.Name);
                    row.Tag = recipient;
                    recipientPayeeDataGridView.Rows.Add(row);
                }
            }
            else
            {
                throw new UiException("Header not found.");
            }
        }

        private List<RecipientVM> GetRecipientPayeeName(ICollection<AdvanceRequisitionDetail> requisitionDetails)
        {
            List<RecipientVM> recipientList = new List<RecipientVM>();
            foreach (AdvanceRequisitionDetail detail in requisitionDetails)
            {
                RecipientVM recipient = new RecipientVM();
                string name = detail.IsThirdPartyReceipient ? detail.ReceipientOrPayeeName : _requester.FullName;
                recipient.Name = name;
                recipient.IsThirdParty = detail.IsThirdPartyReceipient;
                recipientList.Add(recipient);
            }

            return recipientList.DistinctBy(c => c.Name).ToList();
        }

        private List<RecipientVM> GetRecipientPayeeName(ICollection<AdvanceExpenseDetail> expenseDetails)
        {
            List<RecipientVM> recipientList = new List<RecipientVM>();
            foreach (AdvanceExpenseDetail detail in expenseDetails)
            {
                RecipientVM recipient = new RecipientVM();
                string name = detail.IsThirdPartyReceipient ? detail.ReceipientOrPayeeName : _requester.FullName;
                recipient.Name = name;
                recipient.IsThirdParty = detail.IsThirdPartyReceipient;
                recipientList.Add(recipient);
            }

            return recipientList.DistinctBy(c => c.Name).ToList();
        }

        private void RecipientPayeeListUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadRecipientPayeeGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoToRequisitionOrExpensePaymentEntryUi()
        {
            RecipientVM recipient = recipientPayeeDataGridView.SelectedRows[0].Tag as RecipientVM;
            if (recipient == null)
            {
                throw new UiException("Recipient not found.");
            }
            if (_requisitionHeader != null)
            {
                if (recipient.RequisitionVoucherHeader == null && recipient.ExpenseVoucherHeader == null)
                {
                    RequisitionVoucherEntryUI requisitionVoucherEntryUi = new RequisitionVoucherEntryUI(_requisitionHeader, recipient);
                    requisitionVoucherEntryUi.ShowDialog();
                }
                if (recipient.RequisitionVoucherHeader != null)
                {
                    if (recipient.RequisitionVoucherHeader.VoucherStatusId == (long)VoucherStatusEnum.Draft)
                    {
                        RequisitionVoucherEntryUI requisitionVoucherEntryUi = new RequisitionVoucherEntryUI(recipient.RequisitionVoucherHeader, AdvancedFormMode.Update);
                        requisitionVoucherEntryUi.ShowDialog();
                    }
                    else if (recipient.RequisitionVoucherHeader.VoucherStatusId == (long)VoucherStatusEnum.Sent)
                    {
                        RequisitionVoucherEntryUI requisitionVoucherEntryUi = new RequisitionVoucherEntryUI(recipient.RequisitionVoucherHeader, AdvancedFormMode.View);
                        requisitionVoucherEntryUi.ShowDialog();
                    }
                }
            }
            else if (_expenseHeader != null)
            {
                if (_expenseHeader.AdvanceRequisitionHeaders != null)
                {
                    foreach (AdvanceRequisitionHeader advanceRequisitionHeader in _expenseHeader.AdvanceRequisitionHeaders)
                    {
                        List<string> recipientList =
                            _advanceRequisitionHeaderManager.GetRecipientName(advanceRequisitionHeader);
                        if (recipientList.Contains(recipient.Name))
                        {
                            RequisitionVoucherHeader voucherHeader = _requisitionVoucherHeaderManager.GetByHeaderIdAndRecipientName(
                            advanceRequisitionHeader.Id, recipient.Name);
                            if (voucherHeader == null)
                            {
                                throw new UiException("You have to enter requisition voucher before expense.");
                            }
                        }
                    }
                }
                if (recipient.RequisitionVoucherHeader == null && recipient.ExpenseVoucherHeader == null)
                {
                    ExpenseVoucherEntryUI expenseVoucherEntryUi = new ExpenseVoucherEntryUI(_expenseHeader, recipient);
                    expenseVoucherEntryUi.ShowDialog();
                }
                if (recipient.ExpenseVoucherHeader != null)
                {
                    if (recipient.ExpenseVoucherHeader.VoucherStatusId == (long)VoucherStatusEnum.Draft)
                    {
                        ExpenseVoucherEntryUI expenseVoucherEntryUi = new ExpenseVoucherEntryUI(recipient.ExpenseVoucherHeader, AdvancedFormMode.Update);
                        expenseVoucherEntryUi.ShowDialog();
                    }
                    else if (recipient.ExpenseVoucherHeader.VoucherStatusId == (long)VoucherStatusEnum.Sent)
                    {
                        ExpenseVoucherEntryUI expenseVoucherEntryUi = new ExpenseVoucherEntryUI(recipient.ExpenseVoucherHeader, AdvancedFormMode.View);
                        expenseVoucherEntryUi.ShowDialog();
                    }
                }
            }
            else
            {
                throw new UiException("Header not found.");
            }
            LoadRecipientPayeeGridView();
        }
        
        private void recipientPayeeDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (recipientPayeeDataGridView.SelectedRows.Count > 0)
                {
                    GoToRequisitionOrExpensePaymentEntryUi();
                }
                else
                {
                    MessageBox.Show(@"No item is selected.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recipientPayeeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (recipientPayeeDataGridView.SelectedRows.Count > 0)
                    {
                        GoToRequisitionOrExpensePaymentEntryUi();
                    }
                    else
                    {
                        MessageBox.Show(@"No item is selected.", @"Warning!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
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
