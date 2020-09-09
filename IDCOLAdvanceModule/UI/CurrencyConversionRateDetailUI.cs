using System.Drawing;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Utility;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CurrencyManager = IDCOLAdvanceModule.BLL.MISManager.CurrencyManager;

namespace IDCOLAdvanceModule.UI
{
    public partial class CurrencyConversionRateDetailUI : Form
    {
        private readonly ICurrencyManager _currencyManager;
        private readonly ICurrencyConversionRateDetailManager _currencyConversionRateDetailManager;
        public List<CurrencyConversionRateDetail> CurrencyConversionRateDetails { get; set; }
        private DataGridViewRow _upCurrencyConversionRateDetail;
        private bool _isUpdateMode;

        public CurrencyConversionRateDetailUI()
        {
            InitializeComponent();
            _currencyManager = new CurrencyManager();
            _currencyConversionRateDetailManager = new CurrencyConversionRateDetailManager();
        }

        public CurrencyConversionRateDetailUI(List<CurrencyConversionRateDetail> currencyConversionRateDetails)
            : this()
        {
            LoadCurrencyConversionRateDetailGridView(currencyConversionRateDetails);
        }

        private void LoadCurrencyConversionRateDetailGridView(
           List<CurrencyConversionRateDetail> currencyConversionRateDetails)
        {
            if (currencyConversionRateDetails != null)
            {
                int serial = 1;
                currencyConversionDataGridView.Rows.Clear();
                foreach (CurrencyConversionRateDetail detail in currencyConversionRateDetails)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(currencyConversionDataGridView);

                    row.Cells[0].Value = "Edit";
                    row.Cells[1].Value = "Remove";
                    row.Cells[3].Value = detail.FromCurrency;
                    row.Cells[4].Value = detail.ToCurrency;
                    row.Cells[5].Value = detail.ConversionRate.ToString();

                    row.Tag = detail;
                    currencyConversionDataGridView.Rows.Add(row);
                    serial++;
                }
                GenerateSerialNumber(currencyConversionDataGridView);
            }
        }

        private void CurrencyConversionRateDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFromCurrencyComboBox();
                LoadToCurrencyComboBox();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFromCurrencyComboBox()
        {
            fromCurrencyComboBox.DataSource = null;
            List<Solar_CurrencyInfo> formCurrencyList = _currencyManager.GetAll().ToList();
            formCurrencyList.Insert(0, new Solar_CurrencyInfo { CurrencyID = DefaultItem.Value, ShortName = DefaultItem.Text });
            fromCurrencyComboBox.DataSource = formCurrencyList;
            fromCurrencyComboBox.DisplayMember = "ShortName";
            fromCurrencyComboBox.ValueMember = "CurrencyID";
        }

        private void LoadToCurrencyComboBox()
        {
            toCurrencyComboBox.DataSource = null;
            List<Solar_CurrencyInfo> toCurrencyList = _currencyManager.GetAll().ToList();
            toCurrencyList.Insert(0, new Solar_CurrencyInfo { CurrencyID = DefaultItem.Value, ShortName = DefaultItem.Text });
            toCurrencyComboBox.DataSource = toCurrencyList;
            toCurrencyComboBox.DisplayMember = "ShortName";
            toCurrencyComboBox.ValueMember = "CurrencyID";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();
                CurrencyConversionRateDetail currencyDetail = new CurrencyConversionRateDetail();
                currencyDetail.FromCurrency = fromCurrencyComboBox.Text;
                currencyDetail.ToCurrency = toCurrencyComboBox.Text;
                currencyDetail.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                if (!_isUpdateMode)
                {
                    currencyDetail.FromCurrency = fromCurrencyComboBox.Text;
                    currencyDetail.ToCurrency = toCurrencyComboBox.Text;
                    currencyDetail.ConversionRate = Convert.ToDouble(conversionRateTextBox.Text);
                    AddCurrencyConversionRateDetailInGridView(currencyDetail);
                    ClearInputs();
                }
                else
                {
                    var item = _upCurrencyConversionRateDetail;
                    var updatableDetail = item.Tag as CurrencyConversionRateDetail;
                    updatableDetail.FromCurrency = currencyDetail.FromCurrency;
                    updatableDetail.ToCurrency = currencyDetail.ToCurrency;
                    updatableDetail.ConversionRate = currencyDetail.ConversionRate;
                    SetCurrencyConversionRateDetailInGridView(updatableDetail, item);
                    ClearInputs();
                    addButton.Text = @"Add";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private ListViewItem SetCurrencyConversionRateDetailInListView(CurrencyConversionRateDetail currencyDetail, ListViewItem item)
        //{
        //    item.Text = serial.ToString();
        //    item.SubItems[1].Text = currencyDetail.FromCurrency;
        //    item.SubItems[2].Text = currencyDetail.ToCurrency;
        //    item.SubItems[3].Text = currencyDetail.ConversionRate.ToString();
        //    item.Tag = currencyDetail;
        //    return item;
        //}

        private DataGridViewRow SetCurrencyConversionRateDetailInGridView(CurrencyConversionRateDetail currencyDetail, DataGridViewRow row)
        {

            row.Cells[0].Value = "Edit";
            row.Cells[1].Value = "Remove";
            row.Cells[3].Value = currencyDetail.FromCurrency;
            row.Cells[4].Value = currencyDetail.ToCurrency;
            row.Cells[5].Value = currencyDetail.ConversionRate.ToString();

            row.Tag = currencyDetail;
            return row;
        }

        //public void AddCurrencyConversionRateDetailInListView(CurrencyConversionRateDetail detail)
        //{
        //    try
        //    {
        //        List<ListViewItem> listViewItems = GetCurrencyConversionRateDetailInListView();
        //        currencyConversionListView.Items.Clear();
        //        foreach (ListViewItem item in listViewItems)
        //        {
        //            var currency = item.Tag as CurrencyConversionRateDetail;
        //            item.Text = serial.ToString();
        //            item.SubItems.Add(currency.FromCurrency);
        //            item.SubItems.Add(currency.ToCurrency);
        //            item.SubItems.Add(currency.ConversionRate.ToString());
        //            item.Tag = currency;
        //            currencyConversionListView.Items.Add(item);
        //            serial++;
        //        }
        //        ListViewItem newItem = new ListViewItem(serial.ToString());
        //        newItem.SubItems.Add(detail.FromCurrency);
        //        newItem.SubItems.Add(detail.ToCurrency);
        //        newItem.SubItems.Add(detail.ConversionRate.ToString());
        //        newItem.Tag = detail;
        //        currencyConversionListView.Items.Add(newItem);
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}


        public void AddCurrencyConversionRateDetailInGridView(CurrencyConversionRateDetail detail)
        {
            try
            {
                DataGridViewRow newrow = new DataGridViewRow();
                newrow.CreateCells(currencyConversionDataGridView);

                newrow.Cells[0].Value = "Edit";
                newrow.Cells[1].Value = "Remove";
                newrow.Cells[3].Value = detail.FromCurrency;
                newrow.Cells[4].Value = detail.ToCurrency;
                newrow.Cells[5].Value = detail.ConversionRate.ToString();

                newrow.Tag = detail;
                currencyConversionDataGridView.Rows.Add(newrow);
                GenerateSerialNumber(currencyConversionDataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        //public List<ListViewItem> GetCurrencyConversionRateDetailInListView()
        //{
        //    int serial = 1;
        //    List<ListViewItem> listViewItems = new List<ListViewItem>();
        //    foreach (ListViewItem item in currencyConversionListView.Items)
        //    {
        //        listViewItems.Add(item);
        //    }
        //    return listViewItems;
        //}

        private void ClearInputs()
        {
            fromCurrencyComboBox.SelectedIndex = 0;
            toCurrencyComboBox.SelectedIndex = 0;
            conversionRateTextBox.Text = string.Empty;
        }

        private bool ValidateAdd()
        {
            string errorMessage = string.Empty;
            if (Convert.ToDecimal(fromCurrencyComboBox.SelectedValue) == Convert.ToDecimal(DefaultItem.Value))
            {
                errorMessage += "From Currency is not selected." + Environment.NewLine;
            }
            if (Convert.ToDecimal(toCurrencyComboBox.SelectedValue) == Convert.ToDecimal(DefaultItem.Value))
            {
                errorMessage += "To Currency is not selected." + Environment.NewLine;
            }
            if (Convert.ToDecimal(toCurrencyComboBox.SelectedValue) == Convert.ToDecimal(fromCurrencyComboBox.SelectedValue) && Convert.ToDecimal(toCurrencyComboBox.SelectedValue) != Convert.ToDecimal(DefaultItem.Value) && Convert.ToDecimal(fromCurrencyComboBox.SelectedValue) != Convert.ToDecimal(DefaultItem.Value))
            {
                errorMessage += "Same Currency conversion is not possible." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(conversionRateTextBox.Text))
            {
                errorMessage += "Conversion rate is not provided." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            else
            {
                return true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                CurrencyConversionRateDetails = new List<CurrencyConversionRateDetail>();
                //foreach (ListViewItem item in currencyConversionListView.Items)
                //{
                //    CurrencyConversionRateDetail currencyConversionRateDetails = item.Tag as CurrencyConversionRateDetail;
                //    CurrencyConversionRateDetails.Add(currencyConversionRateDetails);
                //}
                foreach (DataGridViewRow row in currencyConversionDataGridView.Rows)
                {
                    CurrencyConversionRateDetail currencyConversionRateDetails = row.Tag as CurrencyConversionRateDetail;
                    CurrencyConversionRateDetails.Add(currencyConversionRateDetails);
                }
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void conversionRateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Utility.Utility.MakeTextBoxToTakeNumbersOnly(sender, e);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //CurrencyConversionRateDetail detail = null;
                //if (currencyConversionListView.SelectedItems.Count > 0)
                //{
                //    ListViewItem item = currencyConversionListView.SelectedItems[0];
                //    detail = (CurrencyConversionRateDetail)item.Tag;
                //    fromCurrencyComboBox.Text = detail.FromCurrency;
                //    toCurrencyComboBox.Text = detail.ToCurrency;
                //    conversionRateTextBox.Text = detail.ConversionRate.ToString();
                //    SetFormToDetailChangeMode(item);
                //}
                //else
                //{
                //    throw new UiException("Not selected an item to edit!");
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormToDetailChangeMode(DataGridViewRow detail)
        {
            try
            {
                if (detail == null)
                {
                    throw new Exception("There is no data found for changing!");
                }
                _upCurrencyConversionRateDetail = detail;
                _isUpdateMode = true;
                addButton.Text = @"Update";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (currencyConversionListView.SelectedItems.Count > 0)
                //{
                //    currencyConversionListView.SelectedItems[0].Remove();
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fromCurrencyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(toCurrencyComboBox.SelectedValue) != Convert.ToDecimal(DefaultItem.Value) &&
                Convert.ToDecimal(fromCurrencyComboBox.SelectedValue) != Convert.ToDecimal(DefaultItem.Value))
                {
                    string errorMessage = string.Empty;
                    if (Convert.ToDecimal(toCurrencyComboBox.SelectedValue) == Convert.ToDecimal(fromCurrencyComboBox.SelectedValue))
                    {
                        errorMessage += "Same Currency conversion is not possible." + Environment.NewLine;
                        fromCurrencyComboBox.SelectedIndex = 0;
                        throw new UiException(errorMessage);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toCurrencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(toCurrencyComboBox.SelectedValue) != Convert.ToDecimal(DefaultItem.Value) &&
                Convert.ToDecimal(fromCurrencyComboBox.SelectedValue) != Convert.ToDecimal(DefaultItem.Value))
                {
                    string errorMessage = string.Empty;
                    if (Convert.ToDecimal(toCurrencyComboBox.SelectedValue) == Convert.ToDecimal(fromCurrencyComboBox.SelectedValue))
                    {
                        errorMessage += "Same Currency conversion is not possible." + Environment.NewLine;
                        toCurrencyComboBox.SelectedIndex = 0;
                        throw new UiException(errorMessage);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void currencyConversionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        CurrencyConversionRateDetail detail = null;
                        if (currencyConversionDataGridView.SelectedRows.Count > 0)
                        {
                            DataGridViewRow row = currencyConversionDataGridView.SelectedRows[0];
                            detail = (CurrencyConversionRateDetail)row.Tag;
                            fromCurrencyComboBox.Text = detail.FromCurrency;
                            toCurrencyComboBox.Text = detail.ToCurrency;
                            conversionRateTextBox.Text = detail.ConversionRate.ToString();
                            SetFormToDetailChangeMode(row);
                        }
                        else
                        {
                            throw new UiException("No item is selected to edit.");
                        }
                    }
                    if (e.ColumnIndex == 1)
                    {
                        if (currencyConversionDataGridView.SelectedRows.Count > 0)
                        {
                            foreach (DataGridViewCell oneCell in currencyConversionDataGridView.SelectedCells)
                            {
                                if (oneCell.Selected)
                                    currencyConversionDataGridView.Rows.RemoveAt(oneCell.RowIndex);
                            }
                        }
                        GenerateSerialNumber(currencyConversionDataGridView);
                    }
                }    
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateSerialNumber(DataGridView gridView)
        {
            int serial = 1;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.Cells[2].Value = serial.ToString();
                serial++;
            }
        }
    }
}
