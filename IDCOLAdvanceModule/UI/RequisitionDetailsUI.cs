using System;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class RequisitionDetailsUI : Form
    {
        private readonly List<AdvanceRequisitionDetail> _details;
        private double _conversionRate;

        public RequisitionDetailsUI(List<AdvanceRequisitionDetail> details, double conversionRate)
        {
            InitializeComponent();
            this._details = details;
            this._conversionRate = conversionRate;
        }

        private void LoadDetailsInformation()
        {
            advanceDetailsListView.Items.Clear();

            foreach (AdvanceRequisitionDetail detail in _details)
            {
                ListViewItem item = new ListViewItem(detail.Purpose);
                item.SubItems.Add(detail.UnitCost != 0 ? detail.UnitCost.ToString() : "N/A");
                item.SubItems.Add(detail.NoOfUnit != 0 ? detail.NoOfUnit.ToString() : "N/A");
                item.SubItems.Add(detail.AdvanceAmount.ToString("N"));
                item.SubItems.Add(detail.GetAdvanceAmountInBdt().ToString("N"));
                item.SubItems.Add(!string.IsNullOrEmpty(detail.Remarks) ? detail.Remarks : "N/A");
                item.Tag = detail;
                advanceDetailsListView.Items.Add(item);
            }

            SetTotalAmountInListView(advanceDetailsListView);
        }

        private void SetTotalAmountInListView(ListView advanceDetailsListViewControl)
        {
            var details = GetAdvanceRequisitionDetailsFromListView(advanceDetailsListViewControl);

            var totalAdvanceAmount = details.Sum(c => c.AdvanceAmount);
            var totalAdvanceAmountInBdt = details.Sum(c => c.GetAdvanceAmountInBdt());

            ListViewItem item = new ListViewItem();
            item.Text = string.Empty;
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(@"Total");
            item.SubItems.Add(totalAdvanceAmount.ToString("N"));
            item.SubItems.Add(totalAdvanceAmountInBdt.ToString("N"));
            item.Font = new Font(item.Font, FontStyle.Bold);
            advanceDetailsListViewControl.Items.Add(item);
        }

        private List<AdvanceRequisitionDetail> GetAdvanceRequisitionDetailsFromListView(ListView advanceDetailsListViewControl)
        {

            var details = new List<AdvanceRequisitionDetail>();
            if (advanceDetailsListViewControl.Items.Count > 0)
            {
                foreach (ListViewItem item in advanceDetailsListViewControl.Items)
                {
                    var detailItem = item.Tag as AdvanceRequisitionDetail;
                    if (detailItem != null)
                    {
                        details.Add(detailItem);
                    }
                }
            }
            return details;
        }

        private void AdvanceRequisitionDetailsUI_Load(object sender, System.EventArgs e)
        {
            try
            {
                LoadDetailsInformation();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void advanceDetailsListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                e.Graphics.DrawRectangle(SystemPens.ActiveCaptionText,
                      new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

                string text = advanceDetailsListView.Columns[e.ColumnIndex].Text;
                TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                      | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(e.Graphics, text, advanceDetailsListView.Font, e.Bounds, Color.Black, cFlag);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advanceDetailsListView_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void advanceDetailsListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
