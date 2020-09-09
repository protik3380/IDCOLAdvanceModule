using System;
using System.Collections.Generic;
using System.ServiceProcess;
using IDCOLAdvanceModule.UI;
using System.Windows.Forms;
using AutoMapper;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Migrations;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.WindowsService;

namespace IDCOLAdvanceModule
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartupConfiguration();
            //var service = new AdvanceScheduler();
            //ServiceBase.Run(service);
            //service.SendEMail("Email Service is running","The Email is sent immediately after the service running!");
            
            Application.Run(new LoginUI());
        }

        private static void StartupConfiguration()
        {
            Mapper.Initialize(conf =>
            {
                conf.CreateMap<RequisitionSourceOfFund, ExpenseSourceOfFund>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<ExpenseSourceOfFund, RequisitionSourceOfFund>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvanceTravelRequisitionHeader, TravelRequisitionHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());
                conf.CreateMap<TravelRequisitionHistoryHeader, AdvanceTravelRequisitionHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());

                conf.CreateMap<AdvanceOverseasTravelRequisitionHeader, OverseasTravelRequisitionHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());
                conf.CreateMap<OverseasTravelRequisitionHistoryHeader, AdvanceOverseasTravelRequisitionHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());

                conf.CreateMap<AdvancePettyCashRequisitionHeader, PettyCashRequisitionHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());
                conf.CreateMap<PettyCashRequisitionHistoryHeader, AdvancePettyCashRequisitionHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());

                conf.CreateMap<AdvanceMiscelleneousRequisitionHeader, MiscelleneousRequisitionHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());
                conf.CreateMap<MiscelleneousRequisitionHistoryHeader, AdvanceMiscelleneousRequisitionHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());

                conf.CreateMap<AdvanceCorporateAdvisoryRequisitionHeader, CorporateAdvisoryRequisitionHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());
                conf.CreateMap<CorporateAdvisoryRequisitionHistoryHeader, AdvanceCorporateAdvisoryRequisitionHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceRequisitionStatus, y => y.Ignore());
                
                conf.CreateMap<AdvanceTravelRequisitionDetail, TravelRequisitionHistoryDetail>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.TravelCostItem, y => y.Ignore());
                conf.CreateMap<TravelRequisitionHistoryDetail, AdvanceTravelRequisitionDetail>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvanceOverseasTravelRequisitionDetail, OverseasTravelRequisitionHistoryDetail>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.OverseasTravelCostItem, y => y.Ignore());
                conf.CreateMap<OverseasTravelRequisitionHistoryDetail, AdvanceOverseasTravelRequisitionDetail>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvancePettyCashRequisitionDetail, PettyCashRequisitionHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<PettyCashRequisitionHistoryDetail, AdvancePettyCashRequisitionDetail>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvanceMiscelleneousRequisitionDetail, MiscelleneousRequisitionHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<MiscelleneousRequisitionHistoryDetail, AdvanceMiscelleneousRequisitionDetail>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvanceCorporateAdvisoryRequisitionDetail, CorporateAdvisoryRequisitionHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<CorporateAdvisoryRequisitionHistoryDetail, AdvanceCorporateAdvisoryRequisitionDetail>().ForMember(x => x.Id, y => y.Ignore());
                
                conf.CreateMap<AdvancePettyCashExpenseHeader, PettyCashExpenseHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());
                conf.CreateMap<PettyCashExpenseHistoryHeader, AdvancePettyCashExpenseHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());
                
                conf.CreateMap<AdvanceMiscelleaneousExpenseHeader, MiscellaneousExpenseHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());
                conf.CreateMap<MiscellaneousExpenseHistoryHeader, AdvanceMiscelleaneousExpenseHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());

                conf.CreateMap<AdvanceTravelExpenseHeader, TravelExpenseHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());
                conf.CreateMap<TravelExpenseHistoryHeader, AdvanceTravelExpenseHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());

                conf.CreateMap<AdvanceOverseasTravelExpenseHeader, OverseasTravelExpenseHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());
                conf.CreateMap<OverseasTravelExpenseHistoryHeader, AdvanceOverseasTravelExpenseHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());
                
                conf.CreateMap<AdvanceCorporateAdvisoryExpenseHeader, CorporateAdvisoryExpenseHistoryHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());
                conf.CreateMap<CorporateAdvisoryExpenseHistoryHeader, AdvanceCorporateAdvisoryExpenseHeader>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.AdvanceCategory, y => y.Ignore()).ForMember(x => x.AdvanceExpenseStatus, y => y.Ignore());

               conf.CreateMap<AdvancePettyCashExpenseDetail, PettyCashExpenseHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<PettyCashExpenseHistoryDetail, AdvancePettyCashExpenseDetail>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvanceMiscelleaneousExpenseDetail, MiscellaneousExpenseHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<MiscellaneousExpenseHistoryDetail, AdvanceMiscelleaneousExpenseDetail>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvanceTravelExpenseDetail, TravelExpenseHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<TravelExpenseHistoryDetail, AdvanceTravelExpenseDetail>().ForMember(x => x.Id, y => y.Ignore());

                conf.CreateMap<AdvanceOverseasTravelExpenseDetail, OverseasTravelExpenseHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<OverseasTravelExpenseHistoryDetail, AdvanceOverseasTravelExpenseDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<AdvanceCorporateAdvisoryExpenseDetail, CorporateAdvisoryExpenseHistoryDetail>().ForMember(x => x.Id, y => y.Ignore());
                conf.CreateMap<CorporateAdvisoryExpenseHistoryDetail, AdvanceCorporateAdvisoryExpenseDetail>().ForMember(x => x.Id, y => y.Ignore());

               
            });
        }
    }
}
