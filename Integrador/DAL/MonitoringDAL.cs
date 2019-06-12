using IntegradorAticOs.Entity;
using SAPbobsCOM;
using System;

namespace IntegradorAticOs.DAL
{
    public class MonitoringDAL : BaseUDO
    {
        private SAPbobsCOM.Company oCompany;
        private SAPbobsCOM.CompanyService oCompService;
        private SAPbobsCOM.GeneralService oGeneralService;
        private SAPbobsCOM.GeneralData oGeneralData;

        internal MonitoringDAL(SAPbobsCOM.Company company)
            : base(company)
        {
            this.oCompany = company;
        }

        public bool InsertMonitoring(MonitoringEntity monitoring)
        {
            bool success = false;

            try
            {
                oCompService = oCompany.GetCompanyService();
                oCompany.StartTransaction();
                oGeneralService = oCompService.GetGeneralService("UD_INT_MONITORING");

                oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                oGeneralData.SetProperty("Code", monitoring.DocCode);

                oGeneralData.SetProperty("U_SketchInv", monitoring.Sketch_Invoice);

                oGeneralData.SetProperty("U_DocDate", DateTime.Now.ToString("yyyyMMdd"));

                oGeneralData.SetProperty("U_DocTotal", monitoring.DocTot);

                if (monitoring.BL != null)
                    oGeneralData.SetProperty("U_Bl", monitoring.BL);

                if (monitoring.BLId != null)
                    oGeneralData.SetProperty("U_BlId", monitoring.BLId);

                if (monitoring.Draft != null)
                    oGeneralData.SetProperty("U_Draft", monitoring.Draft);

                if (monitoring.Branch != null)
                    oGeneralData.SetProperty("U_Branch", monitoring.Branch);

                if (monitoring.BranchId != null)
                    oGeneralData.SetProperty("U_BranchId", monitoring.BranchId);

                oGeneralData.SetProperty("U_StatusId", monitoring.StatusId);
                oGeneralData.SetProperty("U_Status", monitoring.Status);

                oGeneralData.SetProperty("U_XMLPath", monitoring.XMLPath);
                oGeneralData.SetProperty("U_Message", monitoring.Message);

                oGeneralService.Add(oGeneralData);

            }
            catch (Exception e)
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }

                LogDAL _log = new LogDAL();
                _log.WriteEntry("InsertMonitoring error: " + e.Message);

                throw e;
            }
            if (oCompany.InTransaction)
            {
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
            }
            return success;
        }
    }
}
