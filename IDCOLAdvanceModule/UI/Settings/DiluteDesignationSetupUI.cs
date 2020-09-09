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
using IDCOLAdvanceModule.Model;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class DiluteDesignationSetupUI : Form
    {
        private readonly IDesignationManager _designationManager;
        private readonly IDiluteDesignationManager _diluteDesignationManager;

        public DiluteDesignationSetupUI()
        {
            InitializeComponent();
            _designationManager = new DesignationManager();
            _diluteDesignationManager = new DiluteDesignationManager();
        }

        private void DiluteDesignationSetupUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDiluteDesignations();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadDiluteDesignations()
        {
            LoadDesignationListView();
            LoadExistingDiluteDesignatons();
        }

        private void LoadExistingDiluteDesignatons()
        {
            var diluteDesignations = _diluteDesignationManager.GetAll();
            if (diluteDesignations != null && diluteDesignations.Any() && diluteDesignationSettingsListView.Items.Count > 0)
            {
                foreach (ListViewItem item in diluteDesignationSettingsListView.Items)
                {
                    var designation = item.Tag as Admin_Rank;

                    if (diluteDesignations.Any(c => c.DesignationId == designation.RankID))
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        private void LoadDesignationListView()
        {
            var designations = _designationManager.GetFiltered().OrderBy(c => c.RankName);
            diluteDesignationSettingsListView.Items.Clear();
            foreach (Admin_Rank designation in designations)
            {
                ListViewItem item = new ListViewItem();
                item.Text = designation.RankName;
                item.Tag = designation;
                diluteDesignationSettingsListView.Items.Add(item);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedDiluteDesignations = GetSelectedDiluteDesigations().ToList();

                if (selectedDiluteDesignations == null)
                {
                    selectedDiluteDesignations = new List<DiluteDesignation>();
                }

                bool isSaved = _diluteDesignationManager.Edit(selectedDiluteDesignations.ToList());

                if (isSaved)
                {
                    MessageBox.Show(@"Saved Successfully!", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private IEnumerable<DiluteDesignation> GetSelectedDiluteDesigations()
        {
            if (diluteDesignationSettingsListView.Items.Count > 0)
            {
                foreach (ListViewItem item in diluteDesignationSettingsListView.CheckedItems)
                {
                    var designation = item.Tag as Admin_Rank;

                    yield return new DiluteDesignation() { DesignationId = designation.RankID };
                }
            }
        }

        private void diluteDesignationSettingsListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaption,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = diluteDesignationSettingsListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, diluteDesignationSettingsListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void diluteDesignationSettingsListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void diluteDesignationSettingsListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
