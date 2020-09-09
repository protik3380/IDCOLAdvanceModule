
using System;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class Requisition360ViewManager
    {
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvance_VW_GetApprovalLevelMemberManager _advanceVwGetApprovalLevelMemberManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvance_VW_GetHeadOfDepartmentManager _advanceVwGetHeadOfDepartmentManager;
        private readonly IDestinationUserForTicketManager _destinationUserForTicketManager;
        private readonly IRequisitionApprovalTicketManager _requisitionApprovalTicketManager;
        public Requisition360ViewManager()
        {
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _employeeManager = new EmployeeManager();
            _advanceVwGetApprovalLevelMemberManager = new AdvanceVwGetApprovalLevelMemberManager();
            _advanceVwGetHeadOfDepartmentManager = new AdvanceVwGetHeadOfDepartmentManager();
            _destinationUserForTicketManager = new DestinationUserForTicketManager();
            _requisitionApprovalTicketManager = new RequisitionApprovalTicketManager();
        }

        public Requisition360ViewManager(IAdvanceRequisitionHeaderManager advanceRequisitionHeaderManager, IEmployeeManager employeeManager, AdvanceVwGetApprovalLevelMemberManager advanceVwGetApprovalLevelMemberManager)
        {
            _advanceRequisitionHeaderManager = advanceRequisitionHeaderManager;
            _employeeManager = employeeManager;
            _advanceVwGetApprovalLevelMemberManager = advanceVwGetApprovalLevelMemberManager;
        }

        public Requisition360ViewVM GetRequisition360View(long requisitionId)
        {
            AdvanceRequisitionHeader requisitionHeader = _advanceRequisitionHeaderManager.GetById(requisitionId);
            EmployeeVM employeeVm = _employeeManager.GetDetailInformation(requisitionHeader.RequesterUserName);
            var requisition360View = new Requisition360ViewVM(requisitionHeader, employeeVm);
            if (requisition360View.ApprovalLevel != null)
            {
                var approvalLevel = requisition360View.ApprovalLevel;
                var approvalLevelMembers = _advanceVwGetApprovalLevelMemberManager.GetByApprovalLevel(approvalLevel.Id);
                //requisition360View.ApprovalLevelMemberList = approvalLevelMembers == null ? new List<Advance_VW_GetApprovalLevelMember>() : approvalLevelMembers.ToList();
                requisition360View.ApprovalLevelMemberList = new List<Advance_VW_GetApprovalLevelMember>();

                //if (approvalLevel.IsLineSupervisor)
                //{
                //    UserTable employee = new UserTable();
                //    employee = _employeeManager.GetById((long)requisition360View.AdvanceRequisitionHeader.RequesterSupervisorId);
                //    Advance_VW_GetApprovalLevelMember supervisor = new Advance_VW_GetApprovalLevelMember()
                //    {
                //        EmployeeID = employee.EmployeeID,
                //        EmployeeFullName = employee.FullName,
                //        RankName = employee.Admin_Rank.RankName,
                //    };
                //    requisition360View.ApprovalLevelMemberList.Add(supervisor);
                //}
                //if (approvalLevel.IsHeadOfDepartment)
                //{
                //    Advance_VW_GetHeadOfDepartment departmentHeadOfDepartment = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(employeeVm.EmployeeUserName);
                //    UserTable employee = new UserTable();
                //    if (departmentHeadOfDepartment == null)
                //    {
                //        throw new Exception("No Department Head is set, please talk to IT Admin");
                //    }
                //    employee = _employeeManager.GetByUserName(departmentHeadOfDepartment.HeadOfDepartmentUserName);
                //    Advance_VW_GetApprovalLevelMember deptOfHead = new Advance_VW_GetApprovalLevelMember
                //    {
                //        EmployeeID = employee.EmployeeID,
                //        EmployeeFullName = employee.FullName,
                //        RankName = employee.Admin_Rank.RankName,
                //    };
                //    requisition360View.ApprovalLevelMemberList.Add(deptOfHead);
                //}
                if (requisitionHeader.RequisitionApprovalTickets.Any())
                {
                    var ticket = requisitionHeader.RequisitionApprovalTickets.FirstOrDefault();
                    if (ticket != null)
                    {
                        //var diluteMembers = _destinationUserForTicketManager.GetBy(ticket.Id, ticket.ApprovalLevelId,
                        //    ticket.ApprovalPanelId);
                        var diluteMembers = new List<DestinationUserForTicket>();
                        var destinationUser = _destinationUserForTicketManager.GetLastDestinationUser(ticket.Id, ticket.ApprovalLevelId, ticket.ApprovalPanelId);

                        if (destinationUser != null)
                        {
                            diluteMembers.Add(destinationUser);
                        }
                        foreach (DestinationUserForTicket userForTicket in diluteMembers)
                        {
                            var employee = _employeeManager.GetByUserName(userForTicket.DestinationUserName);
                            Advance_VW_GetApprovalLevelMember diluteMember = new Advance_VW_GetApprovalLevelMember
                            {
                                EmployeeID = employee.EmployeeID,
                                EmployeeFullName = employee.FullName,
                                RankName = employee.Admin_Rank.RankName,
                            };
                            requisition360View.ApprovalLevelMemberList.Add(diluteMember);
                        }
                    }
                }
            }
            else
            {
                requisition360View.ApprovalLevelMemberList=new List<Advance_VW_GetApprovalLevelMember>();
            }
           
            return requisition360View;
        }
    }
}
