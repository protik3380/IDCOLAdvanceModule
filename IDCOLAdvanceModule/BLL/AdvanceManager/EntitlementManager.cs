using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class EntitlementManager : IEntitlementManager
    {
        private readonly IEntitlementRepository _entitlementRepository;
        public EntitlementManager()
        {
            _entitlementRepository = new EntitlementRepository();
        }

        public bool Insert(Entitlement entity)
        {
            return _entitlementRepository.Insert(entity);
        }

        public bool Insert(ICollection<Entitlement> entityCollection)
        {
            return _entitlementRepository.Insert(entityCollection);
        }

        public bool Edit(Entitlement entity)
        {
            return _entitlementRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            Entitlement entity = GetById(id);
            return _entitlementRepository.Delete(entity);
        }

        public Entitlement GetById(long id)
        {
            return _entitlementRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<Entitlement> GetAll()
        {
            return _entitlementRepository.GetAll();
        }

        public Entitlement GetByTravelCostItemId(long id)
        {
            return _entitlementRepository.Get(c => c.CostItemId == id).FirstOrDefault();
        }
    }
}
