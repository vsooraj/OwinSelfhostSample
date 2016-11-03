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
        [Authorize]
        [HttpGet]
        public PageResult<Item> Get(ODataQueryOptions options)
        {
            var items = this.itemRepository.Items.ToList();
            var results = options.ApplyTo(items.AsQueryable());
            return new PageResult<Item>(results as IEnumerable<Item>, Request.RequestUri, Request.ODataProperties().TotalCount);
        }

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("forall")]
        //public IHttpActionResult Get()
        //{
        //    return Ok("Now server time is: " + DateTime.Now.ToString());
        //}

        //[Authorize(Roles = "user")]
        //[HttpGet]
        //[Route("authenticate")]
        //public IHttpActionResult GetForAuthenticate()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    return Ok("Hello " + identity.Name);
        //}
        //[Authorize(Roles = "admin")]
        //[HttpGet]
        //[Route("authorize")]
        //public IHttpActionResult GetForAdmin()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var roles = identity.Claims
        //                .Where(c => c.Type == ClaimTypes.Role)
        //                .Select(c => c.Value);
        //    return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        //}
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

        //[Authorize]
        //public IHttpActionResult Delete([FromUri]int id)
        //{
        //    var company = itemRepository.Items.FirstOrDefault(c => c.itemId == id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }
        //    //itemRepository.Items.Remove(company);
        //    return Ok();
        //}
        //[Authorize]
        //[HttpDelete]
        public IHttpActionResult Delete(string ids)
        {

            if (ids == null)
            {
                return BadRequest("Argument Null");
            }
            var items = ids.Split(',');
            foreach (var tempItem in items)
            {
                Item item = itemRepository.Items.FirstOrDefault(c => c.itemId == Convert.ToInt16(tempItem));
                if (item == null)
                    return BadRequest("Item with id " + tempItem + "not found");
                else
                    itemRepository.Remove(Convert.ToInt16(tempItem));
            }

            return Ok();


        }


    }

}

