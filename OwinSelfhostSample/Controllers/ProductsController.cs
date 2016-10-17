using OwinSelfhostSample.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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

        [Route("")]
        [Queryable(PageSize = 10)]
        public HttpResponseMessage Get()
        {
            var products = this.productRepository.Products.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, products.AsQueryable());

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
