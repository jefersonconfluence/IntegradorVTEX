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

namespace Integrador
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer = null;
        public Scheduler()
        {
            InitializeComponent();
        }

        public void TesteWriter() {
            string _teste = ConfigurationManager.AppSettings["teste"];
            Util.Log.WriteErrorLog("Testando LOG");
        }
        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            this.timer.Interval = 30000; //a cada 30seg
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Tick);
        }

        private void timer_Tick(object sender, ElapsedEventArgs e)
        {

            //throw new NotImplementedException();
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Util.Log.WriteErrorLog(System.DateTime.Now.ToString() + ": #### Integração finalizada.");
        }
    }
}
