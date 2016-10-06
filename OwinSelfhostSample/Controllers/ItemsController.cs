using Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OwinSelfhostSample.Controllers
{
    public class ItemsController : ApiController
    {
        private static List<Item> _Db = new List<Item>
            {
                new Item { itemId = 1, content="Lumia",encryptionKey="12345",encryptionProvider="sha256", MetaItem=new Dictionary<string, string>() { {"Lumia", "Lumia Emtatdata" } },requestType="GET",sourceEntity="Lumia Entity", sourceDevice = "Lumia" },
                new Item { itemId = 2, content="Nexus",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"Nexus", "Nexus Emtatdata" } },requestType="POST",sourceEntity="Nexus Entity", sourceDevice = "Nexus"},
                new Item { itemId = 3,content="iPhone 3",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 4,content="iPhone 4",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 5,content="iPhone 5",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 6,content="iPhone 6",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 7,content="iPhone 7",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 8,content="iPhone 8",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 9,content="iPhone 9",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 10,content="iPhone10",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 11,content="iPhone11",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 12,content="iPhone12",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 13,content="iPhone13",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" },
                new Item { itemId = 14,content="iPhone14",encryptionKey="12345",encryptionProvider="sha256",MetaItem= new Dictionary<string, string>(){ {"iPhone", "iPhone Emtatdata"} },requestType="PUT",sourceEntity="iPhone Entity", sourceDevice = "iPhone" }

            };
        public IEnumerable<Item> Get()
        {
            return _Db;
        }
        public Item Get(int id)
        {
            var item = _Db.FirstOrDefault(c => c.itemId == id);
            if (item == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return item;
        }
        public IHttpActionResult Post(Item item)
        {
            if (item == null)
            {
                return BadRequest("Argument Null");
            }
            var companyExists = _Db.Any(c => c.itemId == item.itemId);

            if (companyExists)
            {
                return BadRequest("Exists");
            }

            _Db.Add(item);
            return Ok();
        }
        public IHttpActionResult Put(Item item)
        {
            if (item == null)
            {
                return BadRequest("Argument Null");
            }
            var existing = _Db.FirstOrDefault(c => c.itemId == item.itemId);

            if (existing == null)
            {
                return NotFound();
            }

            existing.sourceDevice = item.sourceDevice;
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var company = _Db.FirstOrDefault(c => c.itemId == id);
            if (company == null)
            {
                return NotFound();
            }
            _Db.Remove(company);
            return Ok();
        }
    }
}
