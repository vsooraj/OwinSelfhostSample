using Common;
using OwinSelfhostSample.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;

namespace OwinSelfhostSample.Controllers
{

    [RoutePrefix("api/Items")]
    public class ItemsController : ApiController
    {
        private ItemRepository itemRepository;
        public ItemsController()
        {
            itemRepository = new ItemRepository();
        }
        public PageResult<Item> Get(ODataQueryOptions options)
        {
            var items = this.itemRepository.Items.ToList();
            var results = options.ApplyTo(items.AsQueryable());
            return new PageResult<Item>(results as IEnumerable<Item>, Request.RequestUri, Request.ODataProperties().TotalCount);
        }
        [Authorize]
        [HttpGet]
        // GET: api/Items/5
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var item = itemRepository.Items.FirstOrDefault(c => c.itemId == id);
            if (item == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return Ok(item);
        }
        // GET: api/Items/Name
        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            var item = this.itemRepository.Items.FirstOrDefault(c => c.sourceDevice.ToLower() == name.ToLower());
            if (item == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return Ok(item);
        }

        public IHttpActionResult Post(Item item)
        {
            if (item == null)
            {
                return BadRequest("Argument Null");
            }
            var itemExists = itemRepository.Items.Any(c => c.itemId == item.itemId);

            if (itemExists)
            {
                return BadRequest("Exists");
            }

            //itemRepository.Items.Add(item);
            return Ok();
        }
        public IHttpActionResult Put(Item item)
        {
            if (item == null)
            {
                return BadRequest("Argument Null");
            }
            var existing = itemRepository.Items.FirstOrDefault(c => c.itemId == item.itemId);

            if (existing == null)
            {
                return NotFound();
            }

            existing.sourceDevice = item.sourceDevice;
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var company = itemRepository.Items.FirstOrDefault(c => c.itemId == id);
            if (company == null)
            {
                return NotFound();
            }
            //itemRepository.Items.Remove(company);
            return Ok();
        }
    }
}
