using System.Collections.Generic;

namespace Common
{
    public class Item
    {
        public int itemId { get; set; }
        public string requestType { get; set; }
        public string encryptionProvider { get; set; }
        public string encryptionKey { get; set; }
        public string sourceDevice { get; set; }
        public string sourceEntity { get; set; }
        public string content { get; set; }
        public Dictionary<string, string> MetaItem { get; set; }

    }
    public class MetaItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
