using System.Drawing;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IDCOLAdvanceModule.BLL;
using IDCOLAdvanceModule.BLL.AdvanceManager.ApprovalProcessManagement;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.UI.Entry;
using IDCOLAdvanceModule.UI._360View;
using CurrencyManager = IDCOLAdvanceModule.BLL.MISManager.CurrencyManager;

namespace IDCOLAdvanceModule.UI
{
    public partial class RequisitionSearchUI : Form
    {
        private readonly IDepartmentManager _departmentManager;
        private readonly IDesignationManager _designationManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly ICurrencyManager _currencyManager;
        private readonly IAdvanceRequisitionCategoryManager _advanceRequisitionCategoryManager;
        private UserTable _employee;
        private readonly IAdvance_VW_GetAdvanceRequisitionManager _advanceVwGetAdvanceRequisitionManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IApprovalProcessManager _approvalProcessManager;
        private readonly IRequisitionApprovalTicketManager _requisitionApprovalTicketManager;


        public RequisitionSearchUI()
        {
            InitializeComponent();
            _departmentManager = new DepartmentManager();
            _designationManager = new DesignationManager();
            _employeeManager = new EmployeeManager();
            _currencyManager = new CurrencyManager();
            _advanceVwGetAdvanceRequisitionManager = new AdvanceVwGetAdvanceRequisitionManager();
            _advanceRequisitionCategoryManager = new AdvanceRequisitionCategoryManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _approvalProcessManager = new ApprovalProcessManager();
            _requisitionApprovalTicketManager = new RequisitionApprovalTicketManager();
        }

       
        private void LoadAdvanceRequisitionCategoryComboBox()
        {
            List<AdvanceCategory> categoryList = _advanceRequisitionCategoryManager.GetAll().ToList();
            categoryList.Insert(0, new AdvanceCategory { Id = DefaultItem.Value, Name = DefaultItem.Text });
            advanceRequisitionCategoryComboBox.DataSource = null;
            advanceRequisitionCategoryComboBox.DisplayMember = "Name";
            advanceRequisitionCategoryComboBox.ValueMember = "Id";
            advanceRequisitionCategoryComboBox.DataSource = categoryList;
        }

        private void LoadDepartmentComboBox()
        {
            var departmentList = _departmentManager.GetAll().ToList();
            if (departmentList.Any())
            {
                departmentList.Insert(0, new Admin_Departments { DepartmentID = DefaultItem.Value, DepartmentName = DefaultItem.Text });
                departmentComboBox.DataSource = null;
                departmentComboBox.DisplayMember = "DepartmentName";
                departmentComboBox.ValueMember = "DepartmentID";
                departmentComboBox.DataSource = departmentList;
            }
        }

        private void LoadDesignationByDepartment()
        {
            if (int.Parse(departmentComboBox.SelectedValue.ToString()) != DefaultItem.Value)
            {
                var selectedDepartmentId = (decimal)departmentComboBox.SelectedValue;
                var designationList = _designationManager.GetByDepartmentId((long)selectedDepartmentId).ToList();
                if (designationList.Any())
                {
                    designationList.Insert(0, new Admin_Rank { RankID = DefaultItem.Value, RankName = DefaultItem.Text });
                    designationComboBox.DataSource = null;
                    designationComboBox.DisplayMember = "RankName";
                    designationComboBox.ValueMember = "RankID";
                    designationComboBox.DataSource = designationList;
                }
            }
            else
            {
                var designationList = new List<Admin_Rank>();
                designationList.Insert(0, new Admin_Rank { RankID = DefaultItem.Value, RankName = DefaultItem.Text });
                designationComboBox.DataSource = null;
                designationComboBox.DisplayMember = "RankName";
                designationComboBox.ValueMember = "RankID";
                designationComboBox.DataSource = designationList;
            }
        }

        private void LoadCurrencyComboBox()
        {
            List<Solar_CurrencyInfo> currencyList =
                _currencyManager.GetAllForAdvanceRequisitionAndExpense().ToList();
            currencyList.Insert(0, new Solar_CurrencyInfo { CurrencyID = DefaultItem.Value, ShortName = DefaultItem.Text });
            currencyComboBox.DataSource = null;
            currencyComboBox.DisplayMember = "ShortName";
            currencyComboBox.ValueMember = "CurrencyID";
            currencyComboBox.DataSource = currencyList;
        }

        private void departmentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadDesignationByDepartment();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AdvanceRequisitionSearchUI_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDesignationComboBox();
                LoadDepartmentComboBox();
                LoadCurrencyComboBox();
                LoadAdvanceRequisitionCategoryComboBox();
                if (Session.LoginUserName.ToLower().Equals("admin"))
                {
                    moveToolStripMenuItem.Visible = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadDesignationComboBox()
        {
            var designationList = _designationManager.GetAll().ToList();
            designationList.Insert(0, new Admin_Rank { RankID = DefaultItem.Value, RankName = DefaultItem.Text });
            designationComboBox.DataSource = null;
            designationComboBox.DisplayMember = "RankName";
            designationComboBox.ValueMember = "RankID";
            designationComboBox.DataSource = designationList;
        }

        private void selectEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedDesignation = designationComboBox.SelectedItem;
                var selectEmployeeUi = new SelectEmployeeUI((Admin_Rank)selectedDesignation);
                selectEmployeeUi.ShowDialog();
                UserTable selectedEmployee = selectEmployeeUi.SelectedEmployee;
                employeeTextBox.Text = selectedEmployee != null ? selectedEmployee.FullName : string.Empty;
                _employee = selectedEmployee;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                AdvanceRequisitionSearchCriteria criteria = new AdvanceRequisitionSearchCriteria();
                if (int.Parse(departmentComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    criteria.DepartmentId = (decimal?)departmentComboBox.SelectedValue;
                }
                if (int.Parse(designationComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    criteria.RankId = (decimal?)designationComboBox.SelectedValue;
                }
                if (_employee != null)
                {
                    criteria.RequesterUserName = _employee.UserName;
                }
                if (int.Parse(advanceRequisitionCategoryComboBox.SelectedValue.ToString()) != DefaultItem.Value)
                {
                    criteria.AdvanceCategoryId = Convert.ToInt32(advanceRequisitionCategoryComboBox.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(remarksTextBox.Text))
                {
                    criteria.Remarks = remarksTextBox.Text;
                }
                criteria.CurrencyName = currencyComboBox.Text;
                criteria.FromDate = fromDateTimePicker.Checked ? fromDateTimePicker.Value : (DateTime?)null;
                criteria.ToDate = toDateTimePicker.Checked ? toDateTimePicker.Value : (DateTime?)null;
                criteria.RequesterUserName = _employee != null ? _employee.UserName : null;

                advanceRequisitionSearchDataGridView.Rows.Clear();
                var requisitionList = _advanceVwGetAdvanceRequisitionManager.GetBySearchCriteria(criteria);
                if (requisitionList != null && requisitionList.Any())
                {
                    int serial = 1;
                    foreach (var criteriaVm in requisitionList)
                    {
                        DataGridViewRow row =new DataGridViewRow();
                        row.CreateCells(advanceRequisitionSearchDataGridView);
                        row.Cells[0].Value = "360 View";
                        row.Cells[1].Value = "Edit";
                        row.Cells[2].Value = "View";
                         row.Cells[3].Value = "Move";
                        row.Cells[4].Value = serial.ToString();
                        row.Cells[5].Value = criteriaVm.RequisitionNo;
                        row.Cells[6].Value = criteriaVm.EmployeeName;
                        row.Cells[7].Value = criteriaVm.RequisitionDate.ToString("dd MMM, yyyy");
                        row.Cells[8].Value = criteriaVm.FromDate.Date.ToString("dd MMM, yyyy");
                        row.Cells[9].Value = criteriaVm.ToDate.Date.ToString("dd MMM, yyyy");

                        if (criteriaVm.AdvanceAmountInBDT != null)
                            row.Cells[10].Value = criteriaVm.AdvanceAmountInBDT.Value.ToString("N");
                        row.Tag = criteriaVm;
                        advanceRequisitionSearchDataGridView.Rows.Add(row);
                        serial++;
                    }
                }
                else
                {
                    MessageBox.Show(@"No data found.", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                //if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    if (requisition != null)
                //    {
                //        BaseAdvanceCategory category =
                //            _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                //        if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                //        {
                //            if ((long)AdvanceCategoryEnum.LocalTravel == requisition.AdvanceCategoryId)
                //            {
                //                AdvanceTravelRequisitionHeader header =
                //                    (AdvanceTravelRequisitionHeader)
                //                        _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionEntryForLocalTravelUI localTravelUi = new RequisitionEntryForLocalTravelUI(
                //                    header, AdvancedFormMode.Update);
                //                localTravelUi.Show();
                //            }
                //            else if ((long)AdvanceCategoryEnum.OversearTravel == requisition.AdvanceCategoryId)
                //            {
                //                AdvanceOverseasTravelRequisitionHeader header =
                //                   (AdvanceOverseasTravelRequisitionHeader)
                //                       _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //                RequisitionEntryForOverseasTravelUI travelUi = new RequisitionEntryForOverseasTravelUI(
                //                    header, AdvancedFormMode.Update);
                //                travelUi.Show();
                //            }
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                //        {
                //            AdvancePettyCashRequisitionHeader header =
                //            (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForPettyUI pettyUi = new RequisitionEntryForPettyUI(header, AdvancedFormMode.Update);
                //            pettyUi.Show();
                //        }
                //        else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                //        {
                //            AdvanceMiscelleneousRequisitionHeader header =
                //            (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                //            RequisitionEntryForMiscellaneousUI miscellaneousUi = new RequisitionEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
                //            miscellaneousUi.Show();
                //        }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    if (requisition != null)
                //    {
                //        RequisitionViewUI requisitionViewUi = new RequisitionViewUI(requisition.HeaderId);
                //        requisitionViewUi.Show();
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void view360ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    if (requisition == null)
                //    {
                //        throw new Exception("Selected requisition has no data");
                //    }
                //    var requisition360ViewUi = new Requisition360ViewUI(requisition.HeaderId);
                //    requisition360ViewUi.Show();
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an item to view.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (advanceRequisitionSearchListView.SelectedItems.Count > 0)
                //{
                //    var requisition =
                //        advanceRequisitionSearchListView.SelectedItems[0].Tag as Advance_VW_GetAdvanceRequisition;
                //    if (requisition == null)
                //    {
                //        throw new UiException("Expense is not tagged in listview.");
                //    }
                //    if (requisition.RequistionApprovalTicketId == null)
                //    {
                //        throw new UiException("Sorry! You cannot move this adjustment, because the requester has not sent it for approval yet.");
                //    }
                //    var ticket = _requisitionApprovalTicketManager.GetByAdvanceRequisitionHeaderId(requisition.HeaderId);
                //    var isMoved = _approvalProcessManager.Move(ticket, Session.LoginUserName);
                //    if (isMoved)
                //    {
                //        DestinationUserForTicketManager destinationUserForTicketManagerObj = new DestinationUserForTicketManager();
                //        var destinationUser =
                //            destinationUserForTicketManagerObj.GetBy(ticket.Id, ticket.ApprovalLevelId,
                //                ticket.ApprovalPanelId).LastOrDefault();
                //        var employee = _employeeManager.GetByUserName(destinationUser.DestinationUserName);
                //        MessageBox.Show(@"Selected requisition has been moved to  " + employee.FullName, @"Movement completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //    else
                //    {
                //        MessageBox.Show(@"Sorry! This requisition cannot be moved.", @"Warning!", MessageBoxButtons.OK,
                //            MessageBoxIcon.Warning);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(@"Please select an adjustment to move.", @"Warning!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
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
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                advanceRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            if (requisition == null)
                            {
                                throw new Exception("Selected requisition has no data");
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
                    else if (e.ColumnIndex == 1)
                    {
                        if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                advanceRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            if (requisition != null)
                            {
                                BaseAdvanceCategory category =
                                    _advanceRequisitionCategoryManager.GetById(requisition.AdvanceCategoryId).BaseAdvanceCategory;

                                if (category.Id == (long)BaseAdvanceCategoryEnum.Travel)
                                {
                                    if ((long)AdvanceCategoryEnum.LocalTravel == requisition.AdvanceCategoryId)
                                    {
                                        AdvanceTravelRequisitionHeader header =
                                            (AdvanceTravelRequisitionHeader)
                                                _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                        RequisitionEntryForLocalTravelUI localTravelUi = new RequisitionEntryForLocalTravelUI(
                                            header, AdvancedFormMode.Update);
                                        localTravelUi.Show();
                                    }
                                    else if ((long)AdvanceCategoryEnum.OversearTravel == requisition.AdvanceCategoryId)
                                    {
                                        AdvanceOverseasTravelRequisitionHeader header =
                                           (AdvanceOverseasTravelRequisitionHeader)
                                               _advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                        RequisitionEntryForOverseasTravelUI travelUi = new RequisitionEntryForOverseasTravelUI(
                                            header, AdvancedFormMode.Update);
                                        travelUi.Show();
                                    }
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.PettyCash)
                                {
                                    AdvancePettyCashRequisitionHeader header =
                                    (AdvancePettyCashRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForPettyUI pettyUi = new RequisitionEntryForPettyUI(header, AdvancedFormMode.Update);
                                    pettyUi.Show();
                                }
                                else if (category.Id == (long)BaseAdvanceCategoryEnum.Miscellaneous)
                                {
                                    AdvanceMiscelleneousRequisitionHeader header =
                                    (AdvanceMiscelleneousRequisitionHeader)_advanceRequisitionHeaderManager.GetById(requisition.HeaderId);

                                    RequisitionEntryForMiscellaneousUI miscellaneousUi = new RequisitionEntryForMiscellaneousUI(header, AdvancedFormMode.Update);
                                    miscellaneousUi.Show();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        } 
                    }

                    else if (e.ColumnIndex == 2)
                    {
                        if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                advanceRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            if (requisition != null)
                            {
                                RequisitionViewUI requisitionViewUi = new RequisitionViewUI(requisition.HeaderId);
                                requisitionViewUi.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an item to edit.", @"Warning!", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }  
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        if (advanceRequisitionSearchDataGridView.SelectedRows.Count > 0)
                        {
                            var requisition =
                                advanceRequisitionSearchDataGridView.SelectedRows[0].Tag as Advance_VW_GetAdvanceRequisition;
                            if (requisition == null)
                            {
                                throw new UiException("Expense is not tagged in listview.");
                            }
                            if (requisition.RequistionApprovalTicketId == null)
                            {
                                throw new UiException("Sorry! You cannot move this adjustment, because the requester has not sent it for approval yet.");
                            }
                            var ticket = _requisitionApprovalTicketManager.GetByAdvanceRequisitionHeaderId(requisition.HeaderId);
                            var isMoved = _approvalProcessManager.Move(ticket, Session.LoginUserName);
                            if (isMoved)
                            {
                                DestinationUserForTicketManager destinationUserForTicketManagerObj = new DestinationUserForTicketManager();
                                var destinationUser =
                                    destinationUserForTicketManagerObj.GetBy(ticket.Id, ticket.ApprovalLevelId,
                                        ticket.ApprovalPanelId).LastOrDefault();
                                var employee = _employeeManager.GetByUserName(destinationUser.DestinationUserName);
                                MessageBox.Show(@"Selected requisition has been moved to  " + employee.FullName, @"Movement completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(@"Sorry! This requisition cannot be moved.", @"Warning!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Please select an adjustment to move.", @"Warning!", MessageBoxButtons.OK,
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
    }
}
