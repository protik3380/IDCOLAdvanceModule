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
    public class BaseAdvanceRequisitionCategoryManager : IBaseAdvanceRequisitionCategoryManager
    {
        private IBaseAdvanceRequisitionCategoryRepository _baseAdvanceRequisitionCategoryRepository;

        public BaseAdvanceRequisitionCategoryManager()
        {
            _baseAdvanceRequisitionCategoryRepository = new BaseAdvanceRequisitionCategoryRepository();
        }

        public BaseAdvanceRequisitionCategoryManager(IBaseAdvanceRequisitionCategoryRepository baseAdvanceRequisitionCategoryRepository)
        {
            _baseAdvanceRequisitionCategoryRepository = baseAdvanceRequisitionCategoryRepository;
        }

        public bool Insert(BaseAdvanceCategory entity)
        {
            return _baseAdvanceRequisitionCategoryRepository.Insert(entity);
        }

        public bool Insert(ICollection<BaseAdvanceCategory> entityCollection)
        {
            return _baseAdvanceRequisitionCategoryRepository.Insert(entityCollection);
        }

        public bool Edit(BaseAdvanceCategory entity)
        {
            return _baseAdvanceRequisitionCategoryRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            BaseAdvanceCategory entity = GetById(id);
            return _baseAdvanceRequisitionCategoryRepository.Delete(entity);
        }

        public BaseAdvanceCategory GetById(long id)
        {
            return _baseAdvanceRequisitionCategoryRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<BaseAdvanceCategory> GetAll()
        {
            return _baseAdvanceRequisitionCategoryRepository.GetAll(c=>c.AdvanceCategories);
        }
    }
}
