using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class RequisitionSourceOfFundManager : IRequisitionSourceOfFundManager
    {
        public IRequisitionSourceOfFundRepository _requisitionSourceOfFundRepository;
        public IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;

        public RequisitionSourceOfFundManager()
        {
            _requisitionSourceOfFundRepository = new RequisitionSourceOfFundRepository();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
        }

        public RequisitionSourceOfFundManager(IRequisitionSourceOfFundRepository requisitionSourceOfFundRepository, IAdvanceExpenseHeaderManager advanceExpenseHeaderManager)
        {
            _requisitionSourceOfFundRepository = requisitionSourceOfFundRepository;
            _advanceExpenseHeaderManager = advanceExpenseHeaderManager;
        }
        public bool Insert(RequisitionSourceOfFund entity)
        {
            return _requisitionSourceOfFundRepository.Insert(entity);
        }

        public bool Insert(ICollection<RequisitionSourceOfFund> entityCollection)
        {
            return _requisitionSourceOfFundRepository.Insert(entityCollection);
        }

        public bool Edit(RequisitionSourceOfFund entity)
        {
            return _requisitionSourceOfFundRepository.Edit(entity);

        }

        public bool Delete(long id)
        {
            var requisitionSourceOfFund = GetById(id);
            return _requisitionSourceOfFundRepository.Delete(requisitionSourceOfFund);
        }

        public RequisitionSourceOfFund GetById(long id)
        {
            return _requisitionSourceOfFundRepository.GetFirstOrDefaultBy(c=>c.Id == id,c=>c.SourceOfFund);

        }

        public ICollection<RequisitionSourceOfFund> GetAll()
        {
            return _requisitionSourceOfFundRepository.GetAll(c => c.SourceOfFund);
        }

        public ICollection<RequisitionSourceOfFund> GetAllByAdvanceRequisitionHeaderId(long id)
        {
            return
                _requisitionSourceOfFundRepository.Get(c => c.AdvanceRequisitionHeaderId == id, c => c.SourceOfFund)
                    .ToList();
        }

        public bool Insert(ICollection<RequisitionSourceOfFund> entityCollection, long requisitionHeaderId)
        {
            
            var existingSourceOfFunds =
                _requisitionSourceOfFundRepository.Get(c => c.AdvanceRequisitionHeaderId == requisitionHeaderId)
                    .ToList();

            var updatedSourceOfFunds = entityCollection.ToList();

            var updateableItems = updatedSourceOfFunds.Where(c => c.AdvanceRequisitionHeaderId > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            var deleteableItems = existingSourceOfFunds.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedSourceOfFunds.Where(c => c.Id == 0).ToList();

            using (var ts = new TransactionScope())
            {
               
                int deleteCount = 0, updateCount=0, addCount=0;
                bool isDeleted = false, isUpdated=true,isAdded=false;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var sourceOfFund in deleteableItems)
                    {
                        isDeleted = _requisitionSourceOfFundRepository.Delete(sourceOfFund);
                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    foreach (RequisitionSourceOfFund addeableItem in addeableItems)
                    {
                        addeableItem.SourceOfFund = null;
                    }
                    addeableItems.ForEach(c => { c.AdvanceRequisitionHeaderId =requisitionHeaderId; });
                    isAdded=_requisitionSourceOfFundRepository.Insert(addeableItems);
                    if (isAdded)
                    {
                        addCount++;
                    }
                }
                isAdded = addCount == (addeableItems == null ? 0 : addeableItems.Count());


                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var item in updateableItems)
                    {
                        isUpdated = _requisitionSourceOfFundRepository.Edit(item);
                        if (isUpdated)
                        {
                            updateCount++;
                        }
                    }
                }

                ts.Complete();

                return isUpdated || isDeleted || isAdded;
            }
        }
    }
}
