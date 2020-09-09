using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class RequisitionHistoryManager : IRequisitionHistoryHeaderManager
    {
        private IRequisitionHistoryHeaderRepository _requisitionHistoryHeaderRepository;
        private IRequisitionHistoryDetailRepository _requisitionHistoryDetailRepository;

        public RequisitionHistoryManager()
        {
            _requisitionHistoryHeaderRepository = new RequisitionHistoryHeaderRepository();
            _requisitionHistoryDetailRepository = new RequisitionHistoryDetailRepository();
        }

        public RequisitionHistoryManager(IRequisitionHistoryHeaderRepository requisitionHistoryHeaderRepository, IRequisitionHistoryDetailRepository requisitionHistoryDetailRepository)
        {
            _requisitionHistoryHeaderRepository = requisitionHistoryHeaderRepository;
            _requisitionHistoryDetailRepository = requisitionHistoryDetailRepository;
        }
        public bool Insert(RequisitionHistoryHeader entity)
        {
            return _requisitionHistoryHeaderRepository.Insert(entity);
        }

        public bool Insert(ICollection<RequisitionHistoryHeader> entityCollection)
        {
            return _requisitionHistoryHeaderRepository.Insert(entityCollection);
        }

        public bool Edit(RequisitionHistoryHeader entity)
        {
            return _requisitionHistoryHeaderRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _requisitionHistoryHeaderRepository.Delete(entity);
        }

        public RequisitionHistoryHeader GetById(long id)
        {
            return _requisitionHistoryHeaderRepository.GetFirstOrDefaultBy(c => c.Id == id,
                c => c.RequisitionHistoryDetails,
                c => c.AdvanceCategory,
                c => c.AdvanceRequisitionHeader,
                c => c.AdvanceRequisitionStatus);
        }

        public ICollection<RequisitionHistoryHeader> GetAll()
        {
            return _requisitionHistoryHeaderRepository.GetAll(c => c.RequisitionHistoryDetails,
                c => c.AdvanceCategory,
                c => c.AdvanceRequisitionHeader,
                c => c.AdvanceRequisitionStatus);
        }

        public ICollection<RequisitionHistoryHeader> GetAllByRequisitionHeaderId(long requisitionHeaderId)
        {
            return _requisitionHistoryHeaderRepository.Get(c => c.AdvanceRequisitionHeaderId == requisitionHeaderId,
                c => c.RequisitionHistoryDetails,
                c => c.AdvanceCategory,
                c => c.AdvanceRequisitionHeader,
                c => c.AdvanceRequisitionStatus,
                c => c.HistoryMode,
                c =>c.RequisitionHistoryDetails.Select(d=>d.HistoryMode)
                ).ToList();
        }
    }
    
}
