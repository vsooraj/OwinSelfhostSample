using Common;
using System.Collections.Generic;
using System.Linq;

namespace OwinSelfhostSample.Models
{
    public class ItemRepository
    {
        private static List<Item> _items = new List<Item>
            {
                new Item { itemId = 1, content="Lumia",encryptionKey="22",encryptionProvider="sha64", MetaItem=new Dictionary<string, string>() { {"Lumia", "Lumia Emtatdata" } },requestType="GET",sourceEntity="Lumia Entity", sourceDevice = "Lumia" },
                new Item { itemId = 2, content="Nexus",encryptionKey="2234",encryptionProvider="sha128",MetaItem= new Dictionary<string, string>(){ {"Nexus", "Nexus Emtatdata" } },requestType="POST",sourceEntity="Nexus Entity", sourceDevice = "Nexus"},
                new Item { itemId = 3,content="iPhone 3",encryptionKey="32345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 4,content="iPhone 4",encryptionKey="423456",encryptionProvider="sha512",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 5,content="iPhone 5",encryptionKey="523457",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 6,content="iPhone 6",encryptionKey="623458",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 7,content="iPhone 7",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 8,content="iPhone 8",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 9,content="iPhone 9",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 10,content="iPhone10",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 11,content="iPhone11",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 12,content="iPhone12",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 13,content="iPhone13",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 14,content="iPhone14",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" }

            };
        public IQueryable<Item> Items
        {
            get { return _items.AsQueryable(); }
        }

        //public void Remove(Item item)
        //{
        //    _items.Remove(item);

        //}
        public void Remove(int id)
        {
            _items.RemoveAll(p => p.itemId == id);
        }


    }
}
