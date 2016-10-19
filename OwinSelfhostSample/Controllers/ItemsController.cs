using Common;
using OwinSelfhostSample.Models;
using System;
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
        //// GET: api/Items
        //[Route("")]
        //public IHttpActionResult Get()
        //{
        //    var items = this.itemRepository.Items.ToList();
        //    if (items == null)
        //    {
        //        throw new HttpResponseException(
        //            System.Net.HttpStatusCode.NotFound);
        //    }

        //    return Ok(items);
        //}

        //[Route("")]
        //[Queryable]
        //public HttpResponseMessage Get()
        //{
        //    var items = this.itemRepository.Items.ToList();

        //    return Request.CreateResponse(HttpStatusCode.OK, items.AsQueryable());

        //}

        public PageResult<Item> Get(ODataQueryOptions options)
        {

            //if (options.OrderBy != null)
            //{
            //    options.OrderBy.Validator = new MyOrderByValidator();
            //}

            //var settings = new ODataValidationSettings()
            //{
            //    // Initialize settings as needed.
            //    AllowedFunctions = AllowedFunctions.AllMathFunctions
            //};

            //// Validate
            //options.Validate(settings);
            var items = this.itemRepository.Items.ToList();
            var results = options.ApplyTo(items.AsQueryable());
            // var count = (long)Request.GetInlineCount();

            return new PageResult<Item>(results as IEnumerable<Item>, Request.RequestUri, Request.ODataProperties().TotalCount);
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
        [Route("{pageSize:int}/{pageNumber:int}/{filterBy}/{orderBy:alpha?}/{reverse:alpha?}")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string filterBy = "", string orderBy = "", bool reverse = true)
        {
            var totalCount = itemRepository.Items.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var items = itemRepository.Items.ToList();
            if (!String.IsNullOrWhiteSpace(filterBy) && filterBy.ToLower() != "search")
            {
                filterBy = filterBy.ToLower();
                items = itemRepository.Items.Where(s => s.sourceDevice.ToLower().Contains(filterBy)
                              || s.requestType.ToLower().Contains(filterBy) || s.itemId.ToString().Contains(filterBy)).ToList();
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
