using Common;
using OwinSelfhostSample.Models;
using System;
using System.Linq;
using System.Web.Http;

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
        // GET: api/Items
        [Route("")]
        public IHttpActionResult Get()
        {
            var items = this.itemRepository.Items.ToList();

            return Ok(items);
        }
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
        // GET: api/items/pageSize/pageNumber/filterBy(optional)/orderBy(optional)  
        [Route("{pageSize:int}/{pageNumber:int}/{filterBy:alpha?}/{orderBy:alpha?}/{reverse:alpha?}")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string filterBy = "", string orderBy = "", bool reverse = true)
        {
            var totalCount = itemRepository.Items.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var items = itemRepository.Items.ToList();
            if (!String.IsNullOrWhiteSpace(filterBy) && filterBy != "Search")
            {
                filterBy = filterBy.ToLower();
                items = itemRepository.Items.Where(s => s.sourceDevice.ToLower().Contains(filterBy)
                              || s.requestType.ToLower().Contains(filterBy)).ToList();
            }
            if (!String.IsNullOrEmpty(orderBy))
            {
                orderBy = "itemId";
            }
            items = items.AsQueryable().OrderByPropertyName(orderBy, reverse).ToList();
            items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = items
            };
            return Ok(result);

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
