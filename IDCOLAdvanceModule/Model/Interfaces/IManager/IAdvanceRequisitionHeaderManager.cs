using System;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.SearchModels;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IAdvanceRequisitionHeaderManager : IManager.BaseManager.IManager<AdvanceRequisitionHeader>
    {
        ICollection<AdvanceRequisitionHeader> GetByEmployeeUserName(string username);
        ICollection<AdvanceRequisitionHeader> GetByCreatedUser(string username);
        ICollection<AdvanceRequisitionSearchCriteriaVM> GetBySearchCriteria(AdvanceRequisitionSearchCriteria criteria);
        bool IsSourceofFundVerifiedForRequisition(long requisitionId);
        bool PayRequisition(long requisitionId, string paidBy, DateTime paidOn);
        bool RequisitionPayReceived(long requisitionId, string paidBy, DateTime paidOn);
        int GetSerialNo(DateTime createdOn);
        List<string> GetRecipientName(AdvanceRequisitionHeader header);
    }
}
