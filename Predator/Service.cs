using Predator.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Predator
{
    partial class Service : ServiceBase
    {
        private bool flagActive = false;
        private ServiceWork sw = new ServiceWork();

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TimerSearch.Start();
        }

        protected override void OnStop()
        {
            TimerSearch.Start();
        }

        private void TimerSearch_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine(flagActive);
            if (flagActive) return;

            try
            {
                flagActive = true;
                sw.InitService();
            }catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

            flagActive = false;
        }
    }
}
