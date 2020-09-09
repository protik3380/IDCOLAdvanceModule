using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class OverseasTravellingAllowanceManager
    {
        private IExecutiveOverseasTravellingAllowanceManager _executiveOverseasTravellingAllowanceManager;
        private IEmployeeCategorySettingManager _employeeCategorySettingManager;
        private IPlaceofVistManager _placeofVistManager;
        private ICostItemManager _costItemManager;

        public OverseasTravellingAllowanceManager()
        {
           _executiveOverseasTravellingAllowanceManager = new ExecutiveOverseasTravellingAllowanceManager();
            _employeeCategorySettingManager = new EmployeeCategorySettingManager();
            _placeofVistManager = new PlaceOfVisitManager();
            _costItemManager= new CostItemManager();
        }

        public OverseasTravellingAllowanceManager(IExecutiveOverseasTravellingAllowanceManager executiveOverseasTravellingAllowanceManager)
        {
            _executiveOverseasTravellingAllowanceManager = executiveOverseasTravellingAllowanceManager;
        }

        
        public ExecutiveOverseasTravellingAllowance GetEntitlementAmountByPlaceOfVisitId(long placeOfVisitId,long costItemId, decimal adminRankId)
        {

            var employeeCategorySetting = _employeeCategorySettingManager.GetByAdminRankId(adminRankId);
            var placeOfVisit = _placeofVistManager.GetById(placeOfVisitId);
            if (employeeCategorySetting == null)
            {
                throw new BllException("Employee Category Setting not found!");
            }

            if (employeeCategorySetting != null && placeOfVisit.LocationGroupId != null)
            {
                var executiveOverseasTravellingAllowance =
                _executiveOverseasTravellingAllowanceManager.GetByLocationGroupIdCostItemAndEmployeeCategoryId(
                    (long)placeOfVisit.LocationGroupId, costItemId, employeeCategorySetting.EmployeeCategoryId);
                if (executiveOverseasTravellingAllowance != null)
                {
                    return executiveOverseasTravellingAllowance;
                }
                else
                {
                    bool isMandatory = _costItemManager.IsEntitlementMandatoryFor((long)AdvanceCategoryEnum.OversearTravel, costItemId);
                    if (isMandatory)
                    {
                        throw new BllException("Entitlement is mandatory but entitlement amount is not set for this cost item!");
                    }
                }
            }
            
            return null;
        }

      
    }
}
