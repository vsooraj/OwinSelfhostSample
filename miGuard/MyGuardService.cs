using Microsoft.Owin.Hosting;
using miGuard.Models;
using System.Collections.Generic;
using System.ServiceProcess;

namespace miGuard
{
    partial class MyGuardService : ServiceBase
    {
        public MyGuardService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.WriteLog("MiGuard service started");

            string baseUri = null;
            if (string.IsNullOrEmpty(baseUri))
            {
                baseUri = SiteGlobal.BaseUrl;

            }
            else
            {
                baseUri = "http://localhost:8080/";
            }
            WebApp.Start<Startup>(baseUri);
            Logger.WriteLog("Starting web Server..." + baseUri);
            //var stateTimer = new Timer(CheckStatus, true, 30000, 10000);
        }

        private void WriteCompaniesList(IEnumerable<Company> companies)
        {
            foreach (var company in companies)
            {
                Logger.WriteLog(string.Format("Id: {0} Name: {1}", company.Id, company.Name));
            }
        }

        private void CheckStatus(object state)
        {
            Logger.WriteLog("Timer ticked and some job has been done sucessfully");
        }

        protected override void OnStop()
        {
            Logger.WriteLog("MiGuard service stopped");
        }
    }
}
