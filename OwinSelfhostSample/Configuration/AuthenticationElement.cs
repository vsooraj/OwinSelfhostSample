using System.Configuration;

namespace OwinSelfhostSample.Configuration
{
    public class AuthenticationElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)base["type"]; }
        }

        [ConfigurationProperty("parameters")]
        public ParametersElementCollection Parameters
        {
            get { return (ParametersElementCollection)base["parameters"]; }
        }
    }
}