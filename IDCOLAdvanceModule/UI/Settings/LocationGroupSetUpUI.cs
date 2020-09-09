using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using MetroFramework;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class LocationGroupSetupUI : Form
    {
        private readonly IPlaceofVistManager _placeofVistManager;
        private readonly ILocationGroupManager _locationGroupManager;
        private bool _isUpdateMode;
        //private LocationGroup _locationGroup;
        private LocationGroup _updateableDetailItem;
        private List<PlaceOfVisit> _placeOfVisitsList;

        public LocationGroupSetupUI()
        {
            InitializeComponent();
            _placeofVistManager = new PlaceOfVisitManager();
            _locationGroupManager = new LocationGroupManager();
        }

        //private void LoadAllPlaces()
        //{
        //    placeNameListView.Items.Clear();
        //    var placeOfVisits = _placeofVistManager.GetAll().ToList();

        //    foreach (PlaceOfVisit placeOfVisit in placeOfVisits)
        //    {
        //        ListViewItem item = new ListViewItem(placeOfVisit.Name);

        //        item.Tag = placeOfVisit;
        //        placeNameListView.Items.Add(item);
        //    }
        //}
        private void LoadAllPlaces()
        {
            placeNameListDataGridView.Rows.Clear();
            var placeOfVisits = _placeofVistManager.GetAll().ToList();

            foreach (PlaceOfVisit placeOfVisit in placeOfVisits)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(placeNameListDataGridView);
                row.Cells[1].Value = placeOfVisit.Name;

                row.Tag = placeOfVisit;
                placeNameListDataGridView.Rows.Add(row);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSave();
                //if (placeNameListView.CheckedItems.Count > 0)
                //{
                //    LocationGroup locationGroup = new LocationGroup();
                //    _placeOfVisitsList = new List<PlaceOfVisit>();
                //    foreach (ListViewItem item in placeNameListView.CheckedItems)
                //    {
                //        var placeOfVisit = item.Tag as PlaceOfVisit;
                //        _placeOfVisitsList.Add(placeOfVisit);
                //    }
                //    locationGroup.Name = nameTextBox.Text;
                //    if (!_isUpdateMode)
                //    {
                //        bool isInserted = _locationGroupManager.InsertLocationGroupAndUpdatePlaceOfVisit(locationGroup,
                //            _placeOfVisitsList);

                //        if (isInserted)
                //        {
                //            MessageBox.Show(@"Location group saved successfully.", @"Success", MessageBoxButtons.OK,
                //                MessageBoxIcon.Information);
                //            LoadLocationListView();
                //            nameTextBox.Text = "";
                //            UncheckPlaceofVistGridView();
                //        }
                //        else
                //        {
                //            MessageBox.Show(@"Location group save failed.", @"Error!", MessageBoxButtons.OK,
                //                MessageBoxIcon.Error);
                //        }
                //    }
                //    else
                //    {
                //        locationGroup.PlaceOfVisits = _placeOfVisitsList;
                //        locationGroup.Id = _updateableDetailItem.Id;
                //        bool isUpdated = _locationGroupManager.Edit(locationGroup);

                //        if (isUpdated)
                //        {
                //            MessageBox.Show(@"Location group updated successfully.", @"Success", MessageBoxButtons.OK,
                //                MessageBoxIcon.Information);
                //            saveButton.Text = @"Save";
                //            nameTextBox.Text = "";
                //            _isUpdateMode = false;
                //        }
                //        else
                //        {
                //            MessageBox.Show(@"Location group update failed.", @"Error!", MessageBoxButtons.OK,
                //               MessageBoxIcon.Error);
                //        }
                //    }

                //    LoadLocationGridView();
                //    UncheckPlaceofVistGridView();
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select at least one place to save.", @"Warning!",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}

                int count = 0;

                foreach (DataGridViewRow row in placeNameListDataGridView.Rows)
                {
                    bool isChecked = (bool)row.Cells[0].EditedFormattedValue;

                    if (isChecked)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    LocationGroup locationGroup = new LocationGroup();
                    _placeOfVisitsList = new List<PlaceOfVisit>();
                    foreach (DataGridViewRow row in placeNameListDataGridView.Rows)
                    {
                        if ((bool)row.Cells[0].EditedFormattedValue)
                        {
                            var placeOfVisit = row.Tag as PlaceOfVisit;
                            _placeOfVisitsList.Add(placeOfVisit);
                        }

                    }
                    locationGroup.Name = nameTextBox.Text;
                    if (!_isUpdateMode)
                    {
                        bool isInserted = _locationGroupManager.InsertLocationGroupAndUpdatePlaceOfVisit(locationGroup,
                            _placeOfVisitsList);

                        if (isInserted)
                        {
                            MessageBox.Show(@"Location group saved successfully.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            LoadLocationGridView();
                            nameTextBox.Text = "";
                            UncheckPlaceofVistGridView();
                        }
                        else
                        {
                            MessageBox.Show(@"Location group save failed.", @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        locationGroup.PlaceOfVisits = _placeOfVisitsList;
                        locationGroup.Id = _updateableDetailItem.Id;
                        bool isUpdated = _locationGroupManager.Edit(locationGroup);

                        if (isUpdated)
                        {
                            MessageBox.Show(@"Location group updated successfully.", @"Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            saveButton.Text = @"Save";
                            nameTextBox.Text = "";
                            _isUpdateMode = false;
                        }
                        else
                        {
                            MessageBox.Show(@"Location group update failed.", @"Error!", MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                        }
                    }

                    LoadLocationGridView();
                    UncheckPlaceofVistGridView();
                }
                else
                {
                    MessageBox.Show(@"Please select at least one place to save.", @"Warning!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void LoadLocationListView()
        //{
        //    int serial = 1;
        //    locationGroupListView.Items.Clear();
        //    var locationGroups = _locationGroupManager.GetAll().ToList();
        //    foreach (LocationGroup locationGroup in locationGroups)
        //    {
        //        ListViewItem item = new ListViewItem(serial.ToString());
        //        item.SubItems.Add(locationGroup.Name);
        //        item.Tag = locationGroup;
        //        locationGroupListView.Items.Add(item);
        //        serial++;
        //    }
        //}
        private void LoadLocationGridView()
        {
            int serial = 1;
            locationGroupDataGridView.Rows.Clear();
            var locationGroups = _locationGroupManager.GetAll().ToList();
            foreach (LocationGroup locationGroup in locationGroups)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(locationGroupDataGridView);
                row.Cells[0].Value = "Edit";
                row.Cells[1].Value = serial;
                row.Cells[2].Value = locationGroup.Name;
                
                row.Tag = locationGroup;
                locationGroupDataGridView.Rows.Add(row);
                serial++;
            }
        }

      
        private void LocationGroupSetUpUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllPlaces();
                LoadLocationGridView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void LoadSelectedPlaceOfBVisit(ICollection<PlaceOfVisit> placeOfVisits)
        //{
        //    placeNameListView.Items.Clear();
        //    var placeList = _placeofVistManager.GetAll().ToList();
        //    if (placeList.Any() && placeList != null)
        //    {
        //        foreach (var _placelist in placeList)
        //        {
        //            ListViewItem item = new ListViewItem(_placelist.Name);
        //            foreach (var visit in placeOfVisits)
        //            {
        //                if (visit.Id == _placelist.Id)
        //                    item.Checked = true;
        //            }
        //            item.Tag = _placelist;
        //            placeNameListView.Items.Add(item);
        //        }
        //    }
        //}
        private void LoadSelectedPlaceOfBVisit(ICollection<PlaceOfVisit> placeOfVisits)
        {
            placeNameListDataGridView.Rows.Clear();
            var placeList = _placeofVistManager.GetAll().ToList();
            if (placeList.Any() && placeList != null)
            {
                foreach (var _placelist in placeList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(placeNameListDataGridView);
                    row.Cells[1].Value = _placelist.Name;
                    foreach (var visit in placeOfVisits)
                    {
                        if (visit.Id == _placelist.Id)
                            row.Cells[0].Value = true;
                    }
                    row.Tag = _placelist;
                    placeNameListDataGridView.Rows.Add(row);
                }
            }
        }

        //private void UncheckPlaceofVistGridView()
        //{
        //    if (placeNameListView.CheckedItems.Count > 0)
        //    {
        //        foreach (ListViewItem item in placeNameListView.CheckedItems)
        //        {
        //            PlaceOfVisit placeOfVisit = item.Tag as PlaceOfVisit;
        //            item.Tag = placeOfVisit;
        //            item.Checked = false;
        //        }
        //    }
        //}
        private void UncheckPlaceofVistGridView()
        {
            foreach (DataGridViewRow row in placeNameListDataGridView.Rows)
            {
                PlaceOfVisit placeOfVisit = row.Tag as PlaceOfVisit;
                row.Tag = placeOfVisit;
                row.Cells[0].Value = false;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //_isUpdateMode = true;
                //saveButton.Text = @"Update";

                //if (locationGroupListView.SelectedItems.Count > 0)
                //{
                //    _updateableDetailItem = locationGroupListView.SelectedItems[0].Tag as LocationGroup;

                //    nameTextBox.Text = _updateableDetailItem.Name;
                //    LoadSelectedPlaceOfBVisit(_updateableDetailItem.PlaceOfVisits);

                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidateSave()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                errorMessage += "Location group name is not provided." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }

            return true;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                UncheckPlaceofVistGridView();
                nameTextBox.Text = "";
                saveButton.Text = @"Save";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void placeNameListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
              ToggleCellCheck(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        private void ToggleCellCheck(int row, int column)
        {
            bool isChecked = (bool)placeNameListDataGridView[column, row].EditedFormattedValue;
            placeNameListDataGridView.Rows[row].Cells[column].Value = !isChecked;
        }

        private void locationGroupDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _isUpdateMode = true;
                saveButton.Text = @"Update";

                if (locationGroupDataGridView.SelectedRows.Count > 0)
                {
                    _updateableDetailItem = locationGroupDataGridView.SelectedRows[0].Tag as LocationGroup;

                    nameTextBox.Text = _updateableDetailItem.Name;
                    LoadSelectedPlaceOfBVisit(_updateableDetailItem.PlaceOfVisits);

                }
                else
                {
                    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                } 

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }
    }
}
