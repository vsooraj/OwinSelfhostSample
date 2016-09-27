using System.Configuration;

namespace miGuardClient.Utilities
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string HostUrl { get; private set; }
        ApplicationSettings()
        {
            HostUrl = ConfigurationManager.AppSettings["hostUrl"];

        }
    }
}