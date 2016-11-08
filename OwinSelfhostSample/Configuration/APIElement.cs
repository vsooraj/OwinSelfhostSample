using System.Configuration;

namespace OwinSelfhostSample.Configuration
{
    public class APIElement : ConfigurationElement
    {
        [ConfigurationProperty("enabled", IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
        }

        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public string BaseURL
        {
            get { return (string)base["baseUrl"]; }
        }

        [ConfigurationProperty("authentication", IsRequired = true)]
        public AuthenticationElement Authentication
        {
            get { return (AuthenticationElement)base["authentication"]; }
        }
    }
}