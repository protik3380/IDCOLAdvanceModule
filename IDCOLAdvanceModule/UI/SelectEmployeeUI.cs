using System.Drawing;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.Utility;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectEmployeeUI : Form
    {
        private readonly IDesignationManager _designationManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly Admin_Rank _rank;
        private readonly decimal? _departmentId;
        private ICollection<Admin_Rank> _rankList;

        public SelectEmployeeUI()
        {
            InitializeComponent();
            _designationManager = new DesignationManager();
            _employeeManager = new EmployeeManager();
        }

        public SelectEmployeeUI(Admin_Rank rank)
            : this()
        {
            _rank = rank;
        }

        public SelectEmployeeUI(decimal? departmentId)
            : this()
        {
            _departmentId = departmentId;
            LoadEmployeeListView(null, null);
        }

        public SelectEmployeeUI(ICollection<Admin_Rank> ranks)
            : this()
        {
            _rankList = ranks;
        }

        public UserTable SelectedEmployee { get; set; }

        private void LoadDesignationComboBox()
        {
            designationComboBox.DataSource = null;
            List<Admin_Rank> designations = new List<Admin_Rank>
                {
                    new Admin_Rank{RankID = DefaultItem.Value, RankName = DefaultItem.Text}
                };

            if (_rankList == null)
            {
                _rankList = _designationManager.GetAll();
            }
            designations.AddRange(_rankList);
            designationComboBox.DisplayMember = "RankName";
            designationComboBox.ValueMember = "RankID";
            designationComboBox.DataSource = designations;
            if (_rank != null)
            {
                designationComboBox.SelectedItem = _rank;
            }
        }

        private void SelectEmployeeUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDesignationComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            try
            {
                employeeListView.Items.Clear();
                long designationId = Convert.ToInt64(designationComboBox.SelectedValue);
                string search = searchTextBox.Text;
                LoadEmployeeListView(designationId, search);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadEmployeeListView(long? designationId, string search)
        {
            List<UserTable> employeeList;
            if (designationId == DefaultItem.Value)
            {
                employeeList = _employeeManager.GetBy(_departmentId, _rankList, null, search).ToList();
            }
            else
            {
                employeeList = _employeeManager.GetBy(_departmentId, _rankList, designationId, search).ToList();
            }

            foreach (UserTable userTable in employeeList)
            {
                ListViewItem item = new ListViewItem(userTable.EmployeeID);
                item.SubItems.Add(userTable.FullName);
                item.SubItems.Add(userTable.Admin_Rank != null ? userTable.Admin_Rank.RankName : "N/A");
                item.SubItems.Add(userTable.BankAccountNumber);
                item.SubItems.Add(userTable.UserTable2 == null ? string.Empty : userTable.UserTable2.FullName);
                item.Tag = userTable;

                employeeListView.Items.Add(item);
            }
        }

        private void employeeListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (employeeListView.SelectedItems.Count > 0)
                {
                    SelectedEmployee = employeeListView.SelectedItems[0].Tag as UserTable;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(@"Please select an employee", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void employeeListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = employeeListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, employeeListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void employeeListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            try
            {
                e.DrawDefault = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void employeeListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            try
            {
                e.DrawDefault = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
