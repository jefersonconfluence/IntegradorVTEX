using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using Integrador.Business;

namespace Integrador
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer = null;

        private string _path = ConfigurationManager.AppSettings["Path"];

        public Scheduler()
        {
            InitializeComponent();
        }

        public void TesteWriter()
        {
            
            Util.Log.WriteErrorLog("Testando LOG");

            Repositorio repositorio = new Repositorio();
            //repositorio.BuscarNovosClientes(DateTime.Now.ToString("s") + "Z");
        }
        protected override void OnStart(string[] args)
        {
            try
            {
                string tempoProcessamento = ConfigurationManager.AppSettings["tempoProcessamento"];

                //Setando variavel de ambiente para controle da integração
                Environment.SetEnvironmentVariable("controleExecucao", DateTime.Now.ToString("s") + "Z");

                timer = new Timer();
                this.timer.Interval = Convert.ToInt32(tempoProcessamento); //a cada 30seg
                timer.Enabled = true;
                this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Tick);
                
                
                Util.Log.WriteErrorLog(" #### Integração Inicializada.");

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            Util.Log.WriteErrorLog("Chamou método.");

            try
            {
                IntegracaoService integracaoService = new IntegracaoService();

                integracaoService.IniciarProcesso(Environment.GetEnvironmentVariable("controleExecucao"));

            }
            catch (Exception)
            {

                throw;
            }

            //throw new NotImplementedException();
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Util.Log.WriteErrorLog(" #### Integração Finalizada.");
        }
    }
}
