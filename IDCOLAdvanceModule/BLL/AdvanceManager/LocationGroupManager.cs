using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL
{
    public class LocationGroupManager : ILocationGroupManager
    {
        private ILocationGroupRepository _locationGroupRepository;
        private IPlaceofVistManager _placeofVistManager;

        public LocationGroupManager()
        {
            _locationGroupRepository = new LocationGroupRepository();
            _placeofVistManager = new PlaceOfVisitManager();
        }

        public LocationGroupManager(ILocationGroupRepository locationGroupRepository, PlaceOfVisitManager placeofVistManager)
        {
            _locationGroupRepository = locationGroupRepository;
            _placeofVistManager = placeofVistManager;
        }

        public bool Insert(LocationGroup entity)
        {
            IsLocationAvailable(entity);
            return _locationGroupRepository.Insert(entity);
        }

        public bool Insert(ICollection<LocationGroup> entityCollection)
        {
            return _locationGroupRepository.Insert(entityCollection);
        }

        public bool Edit(LocationGroup locationGroup)
        {
            var existingLocationGroup = GetById((long)locationGroup.Id);
            var existingPlaceOfVisit = existingLocationGroup.PlaceOfVisits.ToList();
            var updateablePlaceOfVisit = locationGroup.PlaceOfVisits;
            locationGroup.PlaceOfVisits = null;
            var updatableItem = updateablePlaceOfVisit.Where(c => c.LocationGroupId > 0).ToList();
            var itemIdList = updatableItem.Select(c => c.LocationGroupId).ToList();
            var addableItem = updateablePlaceOfVisit.Where(c =>!existingPlaceOfVisit.Select(d=>d.Id).Contains(c.Id)).ToList();
            var delatableItem =
                existingPlaceOfVisit.Where(c => itemIdList.Contains(c.LocationGroupId))
                    .Select(c => c.Id)
                    .ToList();
            _placeofVistManager.IsLocationIdNull(addableItem);
            IsLocationAvailable(locationGroup);
            
            using (var ts=new TransactionScope())
            {
                bool  isUpdatetableLocationGroup= _locationGroupRepository.Edit(locationGroup);
                bool isDeleted = false, isUpdated = false, isAdded = false;
                 if (delatableItem != null && delatableItem.Any())
                 {
                     int delateCount = 0;
                    foreach (var id in delatableItem)
                    {
                        var placeOfVisit = _placeofVistManager.GetById(id);
                        placeOfVisit.LocationGroupId = null;
                        isDeleted=_placeofVistManager.Edit(placeOfVisit);
                        if (isDeleted)
                        {
                            delateCount++;
                        }
                    }
                     isDeleted = delateCount == (delatableItem == null ? 0 : delatableItem.Count);


                 }

                if (addableItem!=null && addableItem.Any())
                {
                    int addCount = 0;
                    foreach (PlaceOfVisit placeOfVisit in addableItem)
                    {
                        placeOfVisit.LocationGroupId = locationGroup.Id;
                        isAdded=_placeofVistManager.Edit(placeOfVisit);
                        if (isAdded)
                        {
                            addCount++;
                        }
                    }
                    isAdded = addCount == (addableItem == null ? 0 : addableItem.Count());

                }
                if (updatableItem!=null &&updatableItem.Any())
                {
                    int updateCount = 0;
                    foreach (var placeOfVisit in updatableItem)
                    {
                        isUpdated = _placeofVistManager.Edit(placeOfVisit);  
                        if (isUpdated)
                        {
                            updateCount++;
                        }
                    }
                    isUpdated = updateCount == (updatableItem == null ? 0 : updatableItem.Count());
                }
                ts.Complete();
                return isAdded || isDeleted || isUpdatetableLocationGroup || isUpdated;
            }
          
            

        }

        public bool Delete(long id)
        {
            LocationGroup entity = GetById(id);
            return _locationGroupRepository.Delete(entity);
        }

        public LocationGroup GetById(long id)
        {
            return _locationGroupRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.OverseasTravelGroups,
                c => c.PlaceOfVisits);
        }

        public ICollection<LocationGroup> GetAll()
        {
            return _locationGroupRepository.GetAll(c => c.PlaceOfVisits, c => c.OverseasTravelGroups);
        }

        public bool InsertLocationGroupAndUpdatePlaceOfVisit(LocationGroup entity, ICollection<PlaceOfVisit> placeOfVisitList)
        {
            using (var ts = new TransactionScope())
            {
                bool isUpdated = false;
                bool isInserted = Insert(entity);
                if (isInserted)
                {
                   
                    _placeofVistManager.IsLocationIdNull(placeOfVisitList);
                    foreach (PlaceOfVisit placeOfVisit in placeOfVisitList)
                    {
                        placeOfVisit.LocationGroupId = (long)entity.Id;
                        isUpdated = _placeofVistManager.Edit(placeOfVisit);
                    }
                }
                if (!isUpdated)
                {
                    throw new BllException("Save failed. Error occured.");
                }
                ts.Complete();
                return true;
            }
        }

        public bool IsLocationAvailable(LocationGroup locationGroup)
        {

            if (locationGroup.Id>0)
            {
                var location = GetLocationName(locationGroup.Name);
                if (location==null)
                {
                    return true;
                }
                if (locationGroup.Id==location.Id )
                {
                    return true;
                }
                throw new BllException("The location name is already exist.");
            }
            else
            {
                var location = GetLocationName( locationGroup.Name);
                if (location!=null)
                {
                    throw new BllException("The location name is already exist.");
                }
                return true;
            }
           
        }

        private LocationGroup GetLocationName(string locationName)
        {
            return _locationGroupRepository.GetFirstOrDefaultBy(c => c.Name.Equals(locationName));
               
        }
           
    }
}
