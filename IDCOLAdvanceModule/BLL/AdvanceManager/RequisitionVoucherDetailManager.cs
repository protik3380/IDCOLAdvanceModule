using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class RequisitionVoucherDetailManager : IRequisitionVoucherDetailManager
    {
        private readonly IRequisitionVoucherDetailRepository _requisitionVoucherDetailRepository;

        public RequisitionVoucherDetailManager()
        {
            _requisitionVoucherDetailRepository = new RequisitionVoucherDetailRepository();
        }

        public RequisitionVoucherDetailManager(IRequisitionVoucherDetailRepository requisitionVoucherDetailRepository)
        {
            _requisitionVoucherDetailRepository = requisitionVoucherDetailRepository;
        }

        public bool Insert(RequisitionVoucherDetail entity)
        {
            return _requisitionVoucherDetailRepository.Insert(entity);
        }

        public bool Insert(ICollection<RequisitionVoucherDetail> entityCollection)
        {
            return _requisitionVoucherDetailRepository.Insert(entityCollection);
        }

        public bool Edit(RequisitionVoucherDetail entity)
        {
            return _requisitionVoucherDetailRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _requisitionVoucherDetailRepository.Delete(entity);
        }

        public RequisitionVoucherDetail GetById(long id)
        {
            return _requisitionVoucherDetailRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<RequisitionVoucherDetail> GetAll()
        {
            return _requisitionVoucherDetailRepository.GetAll();
        }
    }
}
