using System.Configuration;

namespace OwinSelfhostSample.Configuration
{
    public class ParameterElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)base["key"]; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)base["value"]; }
        }
    }
}
