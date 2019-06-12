using System;

namespace IntegradorAticOs.DAL
{
    public class ClientDAL : BaseUDO
    {
        private SAPbobsCOM.Company oCompany;
        private SAPbobsCOM.Recordset oRecordSet;

        internal ClientDAL(SAPbobsCOM.Company company)
            : base(company)
        {
            this.oCompany = company;
        }

        public bool ValidateClientId(string clientId)
        {
            string query = string.Empty;

            try
            {
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                query = string.Format("SELECT CardCode FROM [dbo].[OCRD] WHERE CardCode = '{0}'", clientId);

                oRecordSet.DoQuery(query);

                if (oRecordSet.RecordCount == 1)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }
    }
}
