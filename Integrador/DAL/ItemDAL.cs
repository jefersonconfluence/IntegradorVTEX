using System;

namespace IntegradorAticOs.DAL
{
    class ItemDAL : BaseUDO
    {
        private SAPbobsCOM.Company oCompany;
        private SAPbobsCOM.Recordset oRecordSet;

        internal ItemDAL(SAPbobsCOM.Company company)
            : base(company)
        {
            this.oCompany = company;
        }

        public string GetServiceGroupSAP(string itemCodeSAP)
        {
            string _serviceGroupSAP = string.Empty;

            try
            {
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                oRecordSet.DoQuery(string.Format("SELECT U_Skill_SerMun, U_Skill_LisSer FROM [dbo].[OITM] WHERE ItemCode = '{0}' ", itemCodeSAP));

                if (oRecordSet.RecordCount == 1)
                {
                    _serviceGroupSAP = oRecordSet.Fields.Item("U_Skill_SerMun").Value.ToString();

                    if (_serviceGroupSAP == null || _serviceGroupSAP == string.Empty)
                    {
                        _serviceGroupSAP = oRecordSet.Fields.Item("U_Skill_LisSer").Value.ToString();
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return _serviceGroupSAP;
        }
    }
}
