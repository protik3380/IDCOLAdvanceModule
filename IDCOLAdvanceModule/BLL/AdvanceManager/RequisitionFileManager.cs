using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class RequisitionFileManager : IRequisitionFileManager
    {
        private readonly IRequisitionFileRepository _requisitionFileRepository;

        public RequisitionFileManager()
        {
            _requisitionFileRepository = new RequisitionFileRepository();
        }

        public RequisitionFileManager(IRequisitionFileRepository requisitionFileRepository)
        {
            _requisitionFileRepository = requisitionFileRepository;
        }

        public bool Insert(RequisitionFile entity)
        {
            return _requisitionFileRepository.Insert(entity);
        }

        public bool Insert(ICollection<RequisitionFile> entityCollection)
        {
            return _requisitionFileRepository.Insert(entityCollection);
        }

        public bool Edit(RequisitionFile entity)
        {
            return _requisitionFileRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _requisitionFileRepository.Delete(entity);
        }

        public RequisitionFile GetById(long id)
        {
            return _requisitionFileRepository.GetFirstOrDefaultBy(c => c.Id == id && !c.IsDeleted);
        }

        public ICollection<RequisitionFile> GetAll()
        {
            return _requisitionFileRepository.GetAll(c => !c.IsDeleted);
        }

        public ICollection<RequisitionFile> GetByHeaderId(long headerId)
        {
            return _requisitionFileRepository.Get(c => c.AdvanceRequisitionHeaderId == headerId && !c.IsDeleted);
        }
    }
}
