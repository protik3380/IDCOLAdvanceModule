using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExecutiveOverseasTravellingAllowanceManager : IExecutiveOverseasTravellingAllowanceManager
    {
        private IExecutiveOverseasTravellingAllowanceRepository _executiveOverseasTravellingAllowanceRepository;

        public ExecutiveOverseasTravellingAllowanceManager()
        {
            _executiveOverseasTravellingAllowanceRepository = new ExecutiveOverseasTravellingAllowanceRepository();
        }

        public ExecutiveOverseasTravellingAllowanceManager(IExecutiveOverseasTravellingAllowanceRepository executiveOverseasTravellingAllowanceRepository)
        {
            _executiveOverseasTravellingAllowanceRepository = executiveOverseasTravellingAllowanceRepository;
        }
        public bool Insert(ExecutiveOverseasTravellingAllowance entity)
        {
            IsExecutiveOverseasTravellingAllowanceAvailable(entity);
            return _executiveOverseasTravellingAllowanceRepository.Insert(entity);
        }

        public bool Insert(ICollection<ExecutiveOverseasTravellingAllowance> entityCollection)
        {
            return _executiveOverseasTravellingAllowanceRepository.Insert(entityCollection);

        }

        public bool Edit(ExecutiveOverseasTravellingAllowance entity)
        {
            IsExecutiveOverseasTravellingAllowanceAvailable(entity);
            return _executiveOverseasTravellingAllowanceRepository.Edit(entity);

        }

        public bool Delete(long id)
        {
            var travellingAllowance = GetById(id);
            return _executiveOverseasTravellingAllowanceRepository.Delete(travellingAllowance);
        }

        public ExecutiveOverseasTravellingAllowance GetById(long id)
        {
            return _executiveOverseasTravellingAllowanceRepository.GetFirstOrDefaultBy(c=>c.Id==id);

        }

        public ICollection<ExecutiveOverseasTravellingAllowance> GetAll()
        {
            return _executiveOverseasTravellingAllowanceRepository.GetAll(c => c.EmployeeCategory,
                c => c.CostItem, c => c.LocationGroup);
        }

        public bool IsExecutiveOverseasTravellingAllowanceAvailable(
            ExecutiveOverseasTravellingAllowance executiveOverseasTravellingAllowance)
        {
            if (executiveOverseasTravellingAllowance.Id > 0)
            {
                var wantedExecutiveOverseasTravellingAllowance =
                     GetByLocationGroupIdCostItemAndEmployeeCategoryId(executiveOverseasTravellingAllowance.LocationGroupId,
                         executiveOverseasTravellingAllowance.CostItemId, executiveOverseasTravellingAllowance.EmployeeCategoryId);
                var existingExecutiveOverseasTravellingAllowance = GetById(executiveOverseasTravellingAllowance.Id);
                if (wantedExecutiveOverseasTravellingAllowance.CostItemId ==
                    existingExecutiveOverseasTravellingAllowance.CostItemId &&
                    wantedExecutiveOverseasTravellingAllowance.LocationGroupId ==
                    existingExecutiveOverseasTravellingAllowance.LocationGroupId)
                    return true;
                throw new BllException("Location travel group is already mapped with employee group");
            }
            else
            {
                var wantedExecutiveOverseasTravellingAllowance =
                    GetByLocationGroupIdCostItemAndEmployeeCategoryId(executiveOverseasTravellingAllowance.LocationGroupId,
                        executiveOverseasTravellingAllowance.CostItemId, executiveOverseasTravellingAllowance.EmployeeCategoryId);
                if (wantedExecutiveOverseasTravellingAllowance == null)
                    return true;
                throw new BllException("Location travel group is already mapped with employee group");
            }
        }

        public ExecutiveOverseasTravellingAllowance GetByLocationGroupIdCostItemAndEmployeeCategoryId(long locationGroupId, long costItemId, long employeeCategoryId)
        {
           return _executiveOverseasTravellingAllowanceRepository.GetFirstOrDefaultBy(
                    c => c.LocationGroupId == locationGroupId && c.CostItemId == costItemId && c.EmployeeCategoryId == employeeCategoryId);
        }
    }
}
