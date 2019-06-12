using System;

namespace IntegradorAticOs.DAL
{
    public class BusinessLineDAL : BaseUDO
    {
        private SAPbobsCOM.Company oCompany;
        private SAPbobsCOM.Recordset oRecordSet;

        internal BusinessLineDAL(SAPbobsCOM.Company company)
            : base(company)
        {
            this.oCompany = company;
        }

        public string ValidateBLId(string blId)
        {
            string _query = string.Empty;
            try
            {
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                _query = string.Format("SELECT [CctName] FROM [dbo].[OCCT] WHERE CctCode = '{0}'", blId);

                oRecordSet.DoQuery(_query);
                LogDAL _log = new LogDAL();
                _log.WriteEntry(_query);
                if (oRecordSet.RecordCount == 1)
                {
                    return oRecordSet.Fields.Item("CctName").Value.ToString();
                }
            }
            catch (Exception)
            {
                return "false";
            }

            return "false";
        }
    }
}
