using OwinSelfhostSample.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace OwinSelfhostSample
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly ProductRepository productRepository;
        public ProductsController()
        {
            productRepository = new ProductRepository();
        }
        // GET api/values 
        [Authorize]
        [HttpGet]
        [Route("")]
        //[Queryable(PageSize = 10)]
        //public HttpResponseMessage Get()
        //{
        //    var products = this.productRepository.Products.ToList();

        //    return Request.CreateResponse(HttpStatusCode.OK, products.AsQueryable());

        //}
        public PageResult<Product> Get(ODataQueryOptions options)
        {

            var products = this.productRepository.Products.ToList();
            IQueryable results = options.ApplyTo(products.AsQueryable());
            return new PageResult<Product>(results as IEnumerable<Product>, Request.RequestUri, products.Count());
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
