using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.UI.Approval.ExpenseApproval;
using IDCOLAdvanceModule.UI.Approval.RequisitionApproval;
using IDCOLAdvanceModule.UI._360View;

namespace IDCOLAdvanceModule.UI
{
    public partial class NotificationUI : Form
    {
        private readonly IApplicationNotificationManager _applicationNotificationManager;
        private readonly IAdvance_VW_GetApprovalLevelMemberManager _advanceVwGetApprovalLevelMemberManager;
        public NotificationUI()
        {
            _applicationNotificationManager = new ApplicationNotificationManager();
            _advanceVwGetApprovalLevelMemberManager = new AdvanceVwGetApprovalLevelMemberManager();
            InitializeComponent();
        }

        private void NotificationUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadNotification();
                metroTabControl1.SelectedTab = notificationTabPage;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNotification()
        {
            var unReadNotifications = _applicationNotificationManager.GetUnReadNotificationBy(Session.LoginUserName);
            var readNotifications = _applicationNotificationManager.GetReadNotificationBy(Session.LoginUserName);
            LoadUnreadNotification(unReadNotifications);
            LoadReadNotification(readNotifications);
        }

        private void LoadUnreadNotification(ICollection<ApplicationNotification> unreadNotifications)
        {
            notificationDataGridView.Rows.Clear();
            int serial = 1;
            unreadNotifications = unreadNotifications.OrderByDescending(c => c.NotificationDate).ToList();
            foreach (ApplicationNotification notification in unreadNotifications)
            {

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(notificationDataGridView);
                row.Cells[0].Value = "Go";
                row.Cells[1].Value = false;
                row.Cells[2].Value = serial.ToString();
                row.Cells[3].Value = notification.Message;
                row.Cells[4].Value = notification.NotificationDate.ToString("dd MMM, yyyy hh:mm tt");
                row.Tag = notification;
                notificationDataGridView.Rows.Add(row);
                serial++;
            }
        }

        private void LoadReadNotification(ICollection<ApplicationNotification> readNotifications)
        {
            historyDataGridView.Rows.Clear();
            int serial = 1;
            readNotifications = readNotifications.OrderByDescending(c => c.NotificationDate).ToList();
            foreach (ApplicationNotification notification in readNotifications)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(historyDataGridView);
                row.Cells[0].Value = "360 View";
                row.Cells[1].Value = serial.ToString();
                row.Cells[2].Value = notification.Message;
                row.Cells[3].Value = notification.NotificationDate.ToString("dd MMM, yyyy hh:mm tt");
                row.Tag = notification;
                historyDataGridView.Rows.Add(row);
                serial++;
            }
        }

        private void Go(ApplicationNotification notification)
        {
            if (notification.TicketStatusId == (long)ApprovalStatusEnum.WaitingForApproval)
            {
                bool isValid = _advanceVwGetApprovalLevelMemberManager.IsValidForMember(notification, Session.LoginUserName);
                if (isValid)
                {
                    if (notification.NotificationTypeId == (long)NotificaitonTypeEnum.Requisition)
                    {
                        var requisitionNotification = notification as RequisitionNotification;
                        if (requisitionNotification == null)
                            throw new UiException("Type casting problem");
                        RequisitionApprovalUI approvalUi = new RequisitionApprovalUI(requisitionNotification.RequisitionId);
                        approvalUi.ShowDialog();
                    }
                    else
                    {
                        var expenseNotification = notification as ExpenseNotification;
                        if (expenseNotification == null)
                            throw new UiException("Type casting problem");
                        ExpenseApprovalUI approvalUi = new ExpenseApprovalUI(expenseNotification.ExpenseHeaderId);
                        approvalUi.ShowDialog();
                    }
                }
                else
                {
                    SetMarkAsRead(new List<ApplicationNotification>() { notification });
                    throw new UiException("This is already approved from your level.");
                }
            }
            else
            {
                Load360View(notification);
            }
            SetMarkAsRead(new List<ApplicationNotification>() { notification });
        }

        private void markAsReadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (notificationDataGridView.SelectedRows.Count > 0)
                {
                    List<ApplicationNotification> applicationNotifications = new List<ApplicationNotification>();
                    foreach (DataGridViewRow row in notificationDataGridView.Rows)
                    {
                        if ((bool)row.Cells[1].EditedFormattedValue)
                        {
                            var notification = row.Tag as ApplicationNotification;
                            if (notification == null)
                            {
                                throw new UiException("Item is not tagged");
                            }
                            applicationNotifications.Add(notification);
                        }
                        //var notification = row.Tag as ApplicationNotification;
                        //if (notification == null)
                        //{
                        //    throw new UiException("Item is not tagged");
                        //}
                        //applicationNotifications.Add(notification);
                    }
                    SetMarkAsRead(applicationNotifications);
                }
                else
                {
                    MessageBox.Show(@"Please choose an item.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetMarkAsRead(List<ApplicationNotification> applicationNotifications)
        {
            var isUpdated = _applicationNotificationManager.SetMarkAsRead(applicationNotifications);
            if (isUpdated)
            {
                LoadNotification();
            }
            else
            {
                MessageBox.Show(@"Something went worng.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Load360View(ApplicationNotification notification)
        {
            if (notification.NotificationTypeId == (long)NotificaitonTypeEnum.Requisition)
            {
                var requisitionNotification = notification as RequisitionNotification;
                if (requisitionNotification == null)
                {
                    throw new UiException("Requisition notification tagged item not found.");
                }
                Requisition360ViewUI requisition360ViewUi = new Requisition360ViewUI(requisitionNotification.RequisitionId);
                requisition360ViewUi.ShowDialog();
            }
            else
            {
                var expenseNotification = notification as ExpenseNotification;
                if (expenseNotification == null)
                {
                    throw new UiException("Requisition notification tagged item not found.");
                }
                Expense360ViewUI expense360ViewUi = new Expense360ViewUI(expenseNotification.ExpenseHeaderId);
                expense360ViewUi.ShowDialog();
            }
        }

        public void SelectOrUnSelectAllGridItemForNotification(DataGridView gridView, bool isChecked)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.Cells[1].Value = isChecked;
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectAllButton.Text.Equals("Select All"))
                {
                    selectAllButton.Text = @"Deselect All";
                    SelectOrUnSelectAllGridItemForNotification(notificationDataGridView, true);
                }
                else
                {
                    selectAllButton.Text = @"Select All";
                    SelectOrUnSelectAllGridItemForNotification(notificationDataGridView, false);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notificationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (notificationDataGridView.SelectedRows.Count > 0)
                        {
                            var notification = notificationDataGridView.SelectedRows[0].Tag as ApplicationNotification;
                            if (notification == null)
                            {
                                throw new UiException("Item is not tagged.");
                            }
                            Go(notification);
                        }
                        else
                        {
                            MessageBox.Show(@"Please choose an item.", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    ToggleCellCheck(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)notificationDataGridView[column, row].EditedFormattedValue;
            notificationDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void historyDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        var notification = historyDataGridView.SelectedRows[0].Tag as ApplicationNotification;
                        if (notification == null)
                        {
                            throw new UiException("Notification tagged item not found.");
                        }
                        Load360View(notification);
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
