using System;
using System.Collections.Generic;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class PlaceOfVisitManager : IPlaceofVistManager
    {
        private IPlaceOfVisitRepository _placeOfVisitRepository;

        public PlaceOfVisitManager()
        {
            _placeOfVisitRepository = new PlaceOfVisitRepository();
        }

        public bool Insert(PlaceOfVisit entity)
        {
            return _placeOfVisitRepository.Insert(entity);
        }

        public bool Insert(ICollection<PlaceOfVisit> entityCollection)
        {
            return _placeOfVisitRepository.Insert(entityCollection);
        }

        public bool Edit(PlaceOfVisit entity)
        {
            return _placeOfVisitRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            PlaceOfVisit entity = GetById(id);
            return _placeOfVisitRepository.Delete(entity);
        }

        public PlaceOfVisit GetById(long id)
        {
            return _placeOfVisitRepository.GetFirstOrDefaultBy(c => c.Id == id,c=>c.LocationGroup);
        }

        public ICollection<PlaceOfVisit> GetAll()
        {
            return _placeOfVisitRepository.GetAll(c => c.LocationGroup);
        }

        public ICollection<PlaceOfVisit> GetByLocationGroupId(long locationGroupId)
        {
            return _placeOfVisitRepository.Get(c => c.LocationGroupId == locationGroupId, c => c.LocationGroup);
        }

        public bool IsLocationIdNull(ICollection<PlaceOfVisit> placeOfVisits)
        {
            string errorMessage = "The following places are already added in another group." + Environment.NewLine;
            string existingPlaceNames = string.Empty;
            foreach (PlaceOfVisit placeOfVisit in placeOfVisits)
            {
                if (placeOfVisit.LocationGroupId != null)
                {
                    existingPlaceNames += placeOfVisit.Name + Environment.NewLine;
                }
            }
            if (!string.IsNullOrEmpty(existingPlaceNames))
            {
                errorMessage += existingPlaceNames;
                throw new BllException(errorMessage);
            }
            return true;
        }

      
    }
}