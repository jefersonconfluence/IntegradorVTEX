using System.Configuration;

namespace IntegradorAticOs.DAL
{
    public class CommonConn
    {
        public static SAPbobsCOM.Company oCompany;

        public static void InitializeCompany()
        {
            oCompany = new SAPbobsCOM.Company();
            oCompany.Server = ConfigurationManager.AppSettings["Server"];
            oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
            oCompany.UseTrusted = false;
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
            oCompany.CompanyDB = ConfigurationManager.AppSettings["CompanyDB"];
            oCompany.UserName = ConfigurationManager.AppSettings["UserName"];
            oCompany.Password = ConfigurationManager.AppSettings["UserPass"];
            oCompany.DbUserName = ConfigurationManager.AppSettings["DbUserName"];
            oCompany.DbPassword = ConfigurationManager.AppSettings["DbPassword"];

            if (oCompany.Connected == true)
            {
                oCompany.Disconnect();
            }

            long lRetCode = oCompany.Connect();

            if (lRetCode != 0)
            { 
                int temp_int = 0;
                string temp_string = "";
                oCompany.GetLastError(out temp_int, out temp_string);

                LogDAL log = new LogDAL();

                log.WriteEntry(temp_string);
            }
        }

        public static void FinalizeCompany()
        {
            if (oCompany.Connected == true)
            {
                oCompany.Disconnect();
            }
        }
    }
}
