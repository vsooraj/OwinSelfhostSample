using System.Web.Http;

namespace OwinSelfhostSample
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);

        }
    }
}
