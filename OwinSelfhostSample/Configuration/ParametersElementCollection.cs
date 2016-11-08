using System.Configuration;

namespace OwinSelfhostSample.Configuration
{
    public class ParametersElementCollection : ConfigurationElementCollection
    {
        public ParameterElement this[int index]
        {
            get { return (ParameterElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public ParameterElement this[string id]
        {
            get { return (ParameterElement)BaseGet(id); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ParameterElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ParameterElement).Key;
        }
    }
}
