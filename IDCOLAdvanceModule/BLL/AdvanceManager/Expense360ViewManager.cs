using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.ViewModels;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class Expense360ViewManager
    {
        private readonly IAdvanceExpenseHeaderManager _expenseHeaderManager;
        private readonly IAdvance_VW_GetApprovalLevelMemberManager _advanceVwGetApprovalLevelMemberManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvance_VW_GetHeadOfDepartmentManager _advanceVwGetHeadOfDepartmentManager;
        private readonly IDestinationUserForTicketManager _destinationUserForTicketManager;
        public Expense360ViewManager()
        {
            _expenseHeaderManager = new AdvanceExpenseManager();
            _employeeManager = new EmployeeManager();
            _advanceVwGetApprovalLevelMemberManager = new AdvanceVwGetApprovalLevelMemberManager();
            _advanceVwGetHeadOfDepartmentManager  = new AdvanceVwGetHeadOfDepartmentManager();
            _destinationUserForTicketManager = new DestinationUserForTicketManager();
        }

        public Expense360ViewManager(IAdvanceExpenseHeaderManager expenseHeaderManager, IAdvance_VW_GetApprovalLevelMemberManager advanceVwGetApprovalLevelMemberManager, IEmployeeManager employeeManager, IAdvance_VW_GetHeadOfDepartmentManager advanceVwGetHeadOfDepartmentManager)
        {
            _expenseHeaderManager = expenseHeaderManager;
            _advanceVwGetApprovalLevelMemberManager = advanceVwGetApprovalLevelMemberManager;
            _employeeManager = employeeManager;
            _advanceVwGetHeadOfDepartmentManager = advanceVwGetHeadOfDepartmentManager;
        }

        public  Expense360ViewVM GetExpense360View(long expenseHeaderId)
        {
            var expenseHeader = _expenseHeaderManager.GetById(expenseHeaderId);
            var employeeVm = _employeeManager.GetDetailInformation(expenseHeader.RequesterUserName);

            var expense360View = new Expense360ViewVM(expenseHeader,employeeVm);

            if (expense360View.ApprovalLevel!=null)
            {
                var approvalLevel = expense360View.ApprovalLevel;
                var approvalLevelMembers = _advanceVwGetApprovalLevelMemberManager.GetByApprovalLevel(approvalLevel.Id);
                expense360View.ApprovalLevelMemberList = new List<Advance_VW_GetApprovalLevelMember>();

                //if (approvalLevel.IsLineSupervisor)
                //{
                //    UserTable employee = new UserTable();
                //    employee = _employeeManager.GetById((long)expense360View.AdvanceExpenseHeader.RequesterSupervisorId);
                //    Advance_VW_GetApprovalLevelMember supervisor = new Advance_VW_GetApprovalLevelMember()
                //    {
                //        EmployeeID = employee.EmployeeID,
                //        EmployeeFullName = employee.FullName,
                //        RankName = employee.Admin_Rank.RankName,
                //    };
                //    expense360View.ApprovalLevelMemberList.Add(supervisor);
                //}
                //if (approvalLevel.IsHeadOfDepartment)
                //{
                //    Advance_VW_GetHeadOfDepartment departmentHeadOfDepartment = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(employeeVm.EmployeeUserName);
                //    UserTable employee = new UserTable();
                //    employee = _employeeManager.GetByUserName(departmentHeadOfDepartment.HeadOfDepartmentUserName);
                //    Advance_VW_GetApprovalLevelMember deptOfHead = new Advance_VW_GetApprovalLevelMember
                //    {
                //        EmployeeID = employee.EmployeeID,
                //        EmployeeFullName = employee.FullName,
                //        RankName = employee.Admin_Rank.RankName,
                //    };
                //    expense360View.ApprovalLevelMemberList.Add(deptOfHead);
                //}
                if (expenseHeader.ExpenseApprovalTickets.Any())
                {
                    var ticket = expenseHeader.ExpenseApprovalTickets.FirstOrDefault();
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
                            expense360View.ApprovalLevelMemberList.Add(diluteMember);
                        }
                    }
                }
            }
            else
            {
                expense360View.ApprovalLevelMemberList = new List<Advance_VW_GetApprovalLevelMember>();
            }

            return expense360View;
        }
    }
}
