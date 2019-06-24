using Integrador.DAL;
using Integrador.Entity;
using Integrador.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Business
{
    class IntegracaoService
    {

        public void IniciarProcesso(string date) {

            Repositorio repositorio = new Repositorio();

            CommonConn.InitializeCompany();

            //Integrar clientes
            _ = repositorio.BuscarNovosClientes(date);

            CommonConn.FinalizeCompany();

        }
        public void ProcessarNovosClientes(List<Cliente> clientes) {
            try
            {
                BusinessPartnersDAL bpDAL = new BusinessPartnersDAL(CommonConn.oCompany);
                string errorMessage = "";

                if (clientes.Count > 0)
                {
                    foreach (Cliente cl in clientes)
                    {
                        BusinessPartner businessPartners = new BusinessPartner();

                        businessPartners.CardCode = cl.id;

                        bpDAL.InserirBusinessPartner(businessPartners, out errorMessage);

                    }
                }

            }
            catch (Exception e)
            {
                Log.WriteLog("ProcessarNovosClientes Exception:"+e.Message);
                throw;
            }
        }
    }
}
