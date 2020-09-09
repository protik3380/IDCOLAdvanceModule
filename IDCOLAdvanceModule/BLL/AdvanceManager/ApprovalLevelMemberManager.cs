using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ApprovalLevelMemberManager : IApprovalLevelMemberManager
    {
        private readonly IApprovalLevelMemberRepository _approvalLevelMemberRepository;
        private readonly IEmployeeLeaveManager _employeeLeaveManager;
        private readonly IApprovalLevelRepository _approvalLevelRepository;

        public ApprovalLevelMemberManager()
        {
            _approvalLevelMemberRepository = new ApprovalLevelMemberRepository();
            _approvalLevelRepository = new ApprovalLevelRepository();
            _employeeLeaveManager = new EmployeeLeaveManager();
        }

        public ApprovalLevelMemberManager(IApprovalLevelMemberRepository approvalLevelMemberRepository, IEmployeeLeaveManager employeeLeaveManager, IApprovalLevelRepository approvalLevelRepository)
        {
            _approvalLevelMemberRepository = approvalLevelMemberRepository;
            _employeeLeaveManager = employeeLeaveManager;
            _approvalLevelRepository = approvalLevelRepository;
        }

        public bool Insert(ApprovalLevelMember entity)
        {
            return _approvalLevelMemberRepository.Insert(entity);
        }

        public bool Insert(ICollection<ApprovalLevelMember> entityCollection)
        {
            return _approvalLevelMemberRepository.Insert(entityCollection);
        }

        public bool Edit(ApprovalLevelMember entity)
        {
            return _approvalLevelMemberRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            ApprovalLevelMember entity = GetById(id);
            return _approvalLevelMemberRepository.Delete(entity);
        }

        public ApprovalLevelMember GetById(long id)
        {
            return _approvalLevelMemberRepository.GetFirstOrDefaultBy(c => c.Id == id && !c.IsDeleted);
        }

        public ICollection<ApprovalLevelMember> GetAll()
        {
            return _approvalLevelMemberRepository.GetAll(c => !c.IsDeleted);
        }

        public ICollection<ApprovalLevelMember> GetByLevelId(long id)
        {
            return _approvalLevelMemberRepository.Get(c => c.ApprovalLevelId == id && !c.IsDeleted);
        }

        public bool CheckAndInsert(ICollection<ApprovalLevelMember> newLevelMembers, long designationId,
            ICollection<Advance_VW_GetApprovalLevelMember> existingLevelMembers)
        {
            if (newLevelMembers.Any())
            {
                List<string> employeeUsernameList = newLevelMembers.Select(c => c.EmployeeUserName).ToList();
                existingLevelMembers = existingLevelMembers.Where(c => c.RankID == designationId).ToList();
                List<ApprovalLevelMember> existingsMembers =
                    existingLevelMembers.Select(
                        c =>
                            new ApprovalLevelMember
                            {
                                Id = c.ApprovalLevelMemberId,
                                ApprovalLevelId = c.ApprovalLevelId,
                                EmployeeUserName = c.EmployeeUserName,
                                CreatedOn = DateTime.UtcNow.AddHours(6)
                            }).ToList();
                List<ApprovalLevelMember> deleteabelMembers =
                    existingsMembers.Where(c => !employeeUsernameList.Contains(c.EmployeeUserName)).ToList();
                List<ApprovalLevelMember> addableMembers =
                    newLevelMembers.Where(c => existingLevelMembers.All(d => d.EmployeeUserName != c.EmployeeUserName))
                        .ToList();

                using (var ts = new TransactionScope())
                {
                    int addCount = 0;
                    bool isAdded = false;
                    int deleteCount = 0;
                    bool isDeleted = false;
                    if (deleteabelMembers.Any())
                    {
                        foreach (ApprovalLevelMember approvalLevelMember in deleteabelMembers)
                        {
                            isDeleted = Delete(approvalLevelMember.Id);
                            if (isDeleted)
                            {
                                deleteCount++;
                            }
                        }
                        isDeleted = deleteCount == (deleteabelMembers == null ? 0 : deleteabelMembers.Count());
                    }
                    if (addableMembers.Any())
                    {
                        foreach (ApprovalLevelMember approvalLevelMember in addableMembers)
                        {
                            isAdded = Insert(approvalLevelMember);
                            if (isAdded)
                            {
                                addCount++;
                            }
                        }
                        isAdded = addCount == (addableMembers == null ? 0 : addableMembers.Count());
                    }
                    ts.Complete();

                    return isAdded || isDeleted;
                }
            }
            return false;
        }

        public int GetMaxValueInPriorityOrder(long approvalLevelId)
        {
            var priorityOrder =
                      _approvalLevelMemberRepository.Get(c => c.ApprovalLevelId == approvalLevelId && c.IsDeleted == false).OrderBy(d => d.PriorityOrder).LastOrDefault();
            if (priorityOrder == null)
            {
                return 0;
            }
            return priorityOrder.PriorityOrder;
        }

        public bool UpdatePrioritySerial(List<ApprovalLevelMember> currentLevelItems)
        {
            using (var ts = new TransactionScope())
            {
                foreach (ApprovalLevelMember member in currentLevelItems)
                {
                    _approvalLevelMemberRepository.Edit(member);
                }
                ts.Complete();
                return true;
            }
        }

        public void ReArrangePriorityOrder(long approvalLevelId)
        {
            using (var ts = new TransactionScope())
            {
                int i = 1;
                var level = _approvalLevelRepository.GetFirstOrDefaultBy(c => c.Id == approvalLevelId);
                if (level==null)
                {
                  throw  new BllException("Approval level not found!");
                }
                if (level.IsLineSupervisor || level.IsHeadOfDepartment)
                {
                    i = 2;
                }

                var orderList =
                    _approvalLevelMemberRepository.Get(c => c.ApprovalLevelId == approvalLevelId && c.IsDeleted == false)
                        .OrderBy(c => c.PriorityOrder)
                        .ToList();
                orderList.ForEach(d => d.PriorityOrder = i++);
                foreach (ApprovalLevelMember member in orderList)
                {
                    _approvalLevelMemberRepository.Edit(member);
                }
                ts.Complete();
            }
        }

        public ApprovalLevelMember GetByLevelAndPriority(long approvalLevelId, int priority = 1)
        {
            var member = _approvalLevelMemberRepository.GetFirstOrDefaultBy(
                c => c.ApprovalLevelId == approvalLevelId && c.PriorityOrder == priority && !c.IsDeleted);
            return member;
        }
    }
}
