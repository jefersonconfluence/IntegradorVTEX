using System;

namespace IntegradorAticOs.DAL
{
    internal class ParameterDAL : BaseUDO
    {
        private SAPbobsCOM.Company oCompany;
        private SAPbobsCOM.Recordset oRecordSet;

        internal ParameterDAL(SAPbobsCOM.Company company)
            : base(company)
        {
            this.oCompany = company;
        }

        public SAPbobsCOM.Recordset GetParameter()
        {
            try
            {
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                oRecordSet.DoQuery("SELECT TOP(1) U_UsageNFId FROM [dbo].[@INT_CONFIGURATION] order by CreateDate, CreateTime desc");

                if (oRecordSet.RecordCount > 0)
                {
                    oRecordSet.MoveFirst();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return oRecordSet;
        }
    }
}
