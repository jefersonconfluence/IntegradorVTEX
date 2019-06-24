using Integrador.Entity;
using Integrador.Util;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.DAL
{
    public class BusinessPartnersDAL : BaseUDO
    {

        private SAPbobsCOM.Company oCompany;

        internal BusinessPartnersDAL(SAPbobsCOM.Company company)
        : base(company)
        {
            this.oCompany = company;
        }

        public void InserirBusinessPartner(BusinessPartner businessPartner, out string messageError)
        {
            int addBPNumber = 0;

            try
            {
                
                SAPbobsCOM.BusinessPartners oBusinessPartner = (SAPbobsCOM.BusinessPartners)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                oBusinessPartner.CardCode = businessPartner.CardCode;

                addBPNumber =  oBusinessPartner.Add();

            }
            catch (Exception e)
            {
                Log.WriteLog("InserirBusinessPartner Exception: " + e.Message);
                throw;
            }

            if (addBPNumber != 0)
            {
                messageError = oCompany.GetLastErrorDescription();
                Log.WriteLog("InserirBusinessPartner error SAP: " + messageError);
            }
            else
            {
                string CardCode = oCompany.GetNewObjectKey();
                messageError = "";
                Log.WriteLog("BusinessPartner " + CardCode + " inserido com sucesso.");
            }

        }

    }
}
