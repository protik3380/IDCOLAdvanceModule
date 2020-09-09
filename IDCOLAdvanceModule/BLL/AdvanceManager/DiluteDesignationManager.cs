using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class DiluteDesignationManager:IDiluteDesignationManager,IDisposable
    {
        private IDiluteDesignationRepository _repository;
        public DiluteDesignationManager()
        {
            _repository = new DiluteDesignationRepository();
        }

        public bool Insert(DiluteDesignation entity)
        {
            var existingDeletedEntity =
                _repository.GetFirstOrDefaultBy(c => c.DesignationId == entity.DesignationId && c.IsDeleted);

            if (existingDeletedEntity!=null)
            {
                existingDeletedEntity.IsDeleted = false;
                return Edit(existingDeletedEntity);
            }
            else
            {
                return _repository.Insert(entity);
            }
            
        }

        public bool Insert(ICollection<DiluteDesignation> entityCollection)
        {
            int successCount = entityCollection.Select(Insert).Count(isSuccess => isSuccess);

            return successCount == entityCollection.Count;

        }

        public bool Edit(DiluteDesignation entity)
        {
            return _repository.Edit(entity);
        }
        
        public bool Delete(long id)
        {
            var entity =  GetById(id);

            if (entity!=null)
            {
                entity.IsDeleted = true;
                return _repository.Edit(entity);
            }
            return false;
        }

        public DiluteDesignation GetById(long id)
        {
            return _repository.GetFirstOrDefaultBy(c => c.Id == id && c.IsDeleted ==false);
        }

        public ICollection<DiluteDesignation> GetAll()
        {
            return _repository.Get(c=>c.IsDeleted==false);
        }

        public bool Edit(ICollection<DiluteDesignation> diluteDesignations)
        {
            var existingDiluteDesignations = GetAll();
            if (!diluteDesignations.Any() && (existingDiluteDesignations==null || !existingDiluteDesignations.Any()))
            {
                throw new Exception("No Item to be saved!");
            }


            var existingDiluteDesignationIds = existingDiluteDesignations.Select(c => c.DesignationId).ToList();
            var diluteDesignationIds = diluteDesignations.Select(c => c.DesignationId).ToList();
            var addeableItems = diluteDesignations.Where(c => !existingDiluteDesignationIds.Contains(c.DesignationId)).ToList();

            var deleteableItems = existingDiluteDesignations.Where(c => !diluteDesignationIds.Contains(c.DesignationId)).ToList();

            bool isAdded = false;
            bool isDeleted = false;
            if (addeableItems.Any())
            {
              isAdded =   Insert(addeableItems.ToList());
            }

            if (deleteableItems!=null && deleteableItems.Any())
            {

                isDeleted = Delete(deleteableItems.Select(c=>(long)c.Id).ToList());
            }

            return isAdded || isDeleted;
        }

        public bool Delete(List<long> idList)
        {
            int successCount = idList.Select(Delete).Count(isSuccess => isSuccess);

            return successCount == idList.Count;
        }
        
        
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
