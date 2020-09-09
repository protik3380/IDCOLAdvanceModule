using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectDesignationUI : Form
    {
        private readonly IDiluteDesignationManager _diluteDesignationManager;
        private readonly IDesignationManager _designationManager;
        public UserTable SelectedEmployee { get; set; }

        public SelectDesignationUI()
        {
            _diluteDesignationManager = new DiluteDesignationManager();
            _designationManager = new DesignationManager();
            InitializeComponent();
        }

        private void SelectDesignation_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDesignationListView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadDesignationListView()
        {
            var designations = _diluteDesignationManager.GetAll().ToList();
            designationListView.Items.Clear();
            foreach (var designation in designations)
            {
                var rankAdmin = _designationManager.GetById(designation.DesignationId);
                ListViewItem item = new ListViewItem();
                item.Text = rankAdmin.RankName;
                item.Tag = rankAdmin;
                designationListView.Items.Add(item);
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (designationListView.CheckedItems.Count > 0)
                {
                    List<Admin_Rank> designations = new List<Admin_Rank>();
                    foreach (ListViewItem item in designationListView.CheckedItems)
                    {
                        var designation = item.Tag as Admin_Rank;
                        if (designation != null)
                        {
                            designations.Add(designation);
                        }
                    }
                    SelectEmployeeUI selectEmployeeUi = new SelectEmployeeUI(designations);
                    selectEmployeeUi.ShowDialog();
                    SelectedEmployee = selectEmployeeUi.SelectedEmployee;
                    Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void designationListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = designationListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, designationListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void designationListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void designationListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
