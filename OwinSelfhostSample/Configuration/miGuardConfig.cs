using System.Configuration;

namespace OwinSelfhostSample.Configuration
{
    public class miGuardConfig : ConfigurationSection
    {
        public static miGuardConfig Settings { get; } = (miGuardConfig)ConfigurationManager.GetSection("miGuard");
        [ConfigurationProperty("api", IsRequired = true)]
        public APIElement API
        {
            get { return (APIElement)base["api"]; }
        }
    }
}