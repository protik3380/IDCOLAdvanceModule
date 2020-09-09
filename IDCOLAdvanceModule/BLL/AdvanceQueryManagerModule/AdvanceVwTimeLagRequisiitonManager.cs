using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.ViewModels;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwTimeLagRequisiitonManager:IAdvance_VW_TimeLagRequisitionManager
    {
        private IAdvance_VW_TimeLagRequisitionRepository _repository;
        private IApprovalPanelManager _approvalPanelManager;

        public AdvanceVwTimeLagRequisiitonManager()
        {
            _repository = new Advance_VW_TimeLagRequisitionRepository();
            _approvalPanelManager = new ApprovalPanelManager();
        }

        public ICollection<Advance_VW_TimeLagRequisition> GetAll(params Expression<Func<Advance_VW_TimeLagRequisition, object>>[] includes)
        {
            return _repository.GetAll(includes);
        }

        public ICollection<Advance_VW_TimeLagRequisition> Get(Expression<Func<Advance_VW_TimeLagRequisition, bool>> predicate, params Expression<Func<Advance_VW_TimeLagRequisition, object>>[] includes)
        {
            return _repository.Get(predicate, includes);
        }

        public Advance_VW_TimeLagRequisition GetFirstOrDefaultBy(Expression<Func<Advance_VW_TimeLagRequisition, bool>> predicate, params Expression<Func<Advance_VW_TimeLagRequisition, object>>[] includes)
        {
            return _repository.GetFirstOrDefaultBy(predicate, includes);
        }


        public ICollection<TimeLagReportForRequisitionVM> GetTimeLagReportForRequisition()
        {
            var approvalPanels = _approvalPanelManager.GetAllForTimeLagRequisition();


            foreach (var approvalPanel in approvalPanels)
            {
                foreach (var approvalLevel in approvalPanel.ApprovalLevels)
                {
                    var requisitionApprovalTrackers = approvalLevel.RequisitionApprovalTrackers.GroupBy(c=>c.RequisitionApprovalTicket.AdvanceRequisitionHeaderId).Select(c=>new{RequisitionId = c.Key, Trackers=c});
                    
                    foreach (var trackers in requisitionApprovalTrackers)
                    {

                        var timeLagReportVm = new TimeLagReportForRequisitionVM();
                        timeLagReportVm.TrackerLevelId = approvalLevel.Id;
                        timeLagReportVm.TrackerLevelName = approvalLevel.Name;
                        timeLagReportVm.TrackerPanelId = approvalPanel.Id;
                        timeLagReportVm.TrackerPanelName = approvalPanel.Name;
                        timeLagReportVm.AdvanceRequisitionHeaderId = trackers.RequisitionId;
                        var waitingDays = GetWaitingDays(0,trackers.Trackers.ToList(),0);
                        timeLagReportVm.WaitingTime = waitingDays;
                        
                    }
                }
            }

            return null;
        }


        public double GetWaitingDays(long currentTrackerId, List<RequisitionApprovalTracker> trackers, long waitingDays)
        {
            long waitingTrackerId = currentTrackerId;
            var nextWaitingTracker = trackers
                                .OrderBy(c => c.Id)
                                .FirstOrDefault( c => 
                                    c.ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval 
                                    && c.Id > currentTrackerId);

            DateTime? waitingStartTime = null;
            DateTime? waitingEndTime = null;

            if (nextWaitingTracker != null)
            {
                waitingTrackerId = nextWaitingTracker.Id;
                waitingStartTime = nextWaitingTracker.AuthorizedOn;
            }
            else
            {
                return waitingDays;
            }

            if (waitingTrackerId > 0)
            {
                var nextTracker =
                    trackers.OrderBy(c => c.Id).FirstOrDefault(
                            c =>
                                c.ApprovalStatusId != (long)ApprovalStatusEnum.WaitingForApproval && c.Id > waitingTrackerId);

                if (nextTracker != null)
                {
                    waitingEndTime = nextTracker.AuthorizedOn;
                    waitingDays += (waitingEndTime - waitingStartTime).Value.Days;
                    waitingTrackerId = nextTracker.Id;
                    return GetWaitingDays(waitingTrackerId, trackers, waitingDays);

                }
                else
                {
                    waitingEndTime = DateTime.Now;
                    return waitingDays + (waitingEndTime - waitingStartTime).Value.Days;
                }


            }
            else
            {
                return waitingDays;
            }
        }
    }
}
