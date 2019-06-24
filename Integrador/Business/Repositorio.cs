using Integrador.Entity;
using Integrador.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Business
{
    class Repositorio : BaseService
    {
        public async Task BuscarNovosClientes(string date)
        {
            try
            {
                string _fiedParam = "_fields=_all";

                string _whereParam = "&_where=(createdIn>#DATE OR createdIn=#DATE) OR (updatedIn>#DATE OR updatedIn=#DATE)".Replace("#DATE",date);

                HttpResponseMessage response = await BuildClient().GetAsync("api/dataentities/CL/search?"+_fiedParam+_whereParam);

                var jsonResponse = response.Content.ReadAsStringAsync();

                var novosClientes = JsonConvert.DeserializeObject<List<Cliente>>(jsonResponse.Result);

                if (novosClientes.Count > 0)
                {
                    IntegracaoService integracaoService = new IntegracaoService();
                    integracaoService.ProcessarNovosClientes(novosClientes);
                }

                #region código teste
                /*var task = BuildClient().GetAsync("api/dataentities/CL/search?_fields=_all")
                    .ContinueWith((taskWithResponse) =>
                    {
                        List<Cliente> model = null;

                        var response = taskWithResponse.Result;
                        var jsonResponseString = response.Content.ReadAsStringAsync();

                        jsonResponseString.Wait();

                        model = JsonConvert.DeserializeObject<List<Cliente>>(jsonResponseString.Result);

                    });
                task.Wait();*/
                #endregion

            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
