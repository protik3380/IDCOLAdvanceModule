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
    public class VatTaxTypeManager : IVatTaxTypeManager
    {
        private readonly IVatTaxRepository _vatTaxRepository;

        public VatTaxTypeManager()
        {
            _vatTaxRepository = new VatTaxRepository();
        }

        public VatTaxTypeManager(IVatTaxRepository vatTaxRepository)
        {
            _vatTaxRepository = vatTaxRepository;
        }

        public bool Insert(VatTaxType entity)
        {
            return _vatTaxRepository.Insert(entity);
        }

        public bool Insert(ICollection<VatTaxType> entityCollection)
        {
            return _vatTaxRepository.Insert(entityCollection);
        }

        public bool Edit(VatTaxType entity)
        {
            return _vatTaxRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _vatTaxRepository.Delete(entity);
        }

        public VatTaxType GetById(long id)
        {
            return _vatTaxRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<VatTaxType> GetAll()
        {
            return _vatTaxRepository.GetAll();
        }
    }
}
