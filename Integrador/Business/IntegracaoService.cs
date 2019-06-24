using Integrador.Entity;
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

            //Integrar clientes
            _ = repositorio.BuscarNovosClientes(date);

        }
        public void ProcessarNovosClientes(List<Cliente> clientes) {
            try
            {
                if (clientes.Count > 0)
                {

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
