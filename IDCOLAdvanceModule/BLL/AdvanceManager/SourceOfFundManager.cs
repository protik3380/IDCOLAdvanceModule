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
    public class SourceOfFundManager : ISourceOfFundManager
    {
        private readonly ISourceOfFundRepository _sourceOfFundRepository;

        public SourceOfFundManager()
        {
            _sourceOfFundRepository = new SourceOfFundRepository();
        }

        public SourceOfFundManager(ISourceOfFundRepository sourceOfFundRepository)
        {
            _sourceOfFundRepository = sourceOfFundRepository;
        }

        public bool Insert(SourceOfFund entity)
        {
            return _sourceOfFundRepository.Insert(entity);
        }

        public bool Insert(ICollection<SourceOfFund> entityCollection)
        {
            return _sourceOfFundRepository.Insert(entityCollection);
        }

        public bool Edit(SourceOfFund entity)
        {
            return _sourceOfFundRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _sourceOfFundRepository.Delete(entity);
        }

        public SourceOfFund GetById(long id)
        {
            return _sourceOfFundRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<SourceOfFund> GetAll()
        {
            return _sourceOfFundRepository.GetAll();
        }
    }
}
