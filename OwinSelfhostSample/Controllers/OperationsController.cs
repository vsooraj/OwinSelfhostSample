using Common;
using OwinSelfhostSample.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace OwinSelfhostSample.Controllers
{
    [RoutePrefix("api/operations")]
    public class OperationsController : ApiController
    {
        private OperationsRepository operationsRepository;

        public OperationsController()
        {
            operationsRepository = new OperationsRepository();
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var operations = this.operationsRepository.Operations.ToList();

            return Ok(operations);
        }
        // GET: api/operations/5
        [Route("{id:int}")]
        public Operation Get(int id)
        {
            var operation = operationsRepository.Operations.FirstOrDefault(c => c.operationId == id);
            if (operation == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return operation;
        }
        // GET: api/operations/Name
        [Route("{name}")]
        public IHttpActionResult Get(bool name)
        {
            var item = this.operationsRepository.Operations.FirstOrDefault(c => c.required == name);
            if (item == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return Ok(item);
        }
        // GET: api/operations/pageSize/pageNumber/orderBy(optional)         
        [Route("{pageSize:int}/{pageNumber:int}/{filterBy}/{orderBy:alpha?}/{reverse:alpha?}")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string filterBy = "", string orderBy = "", bool reverse = true)
        {
            var totalCount = operationsRepository.Operations.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var operations = operationsRepository.Operations.ToList();
            if (!String.IsNullOrWhiteSpace(filterBy) && filterBy != "Search")
            {
                filterBy = filterBy.ToLower();
                operations = operationsRepository.Operations.Where(s => s.operationId.ToString().Contains(filterBy)).ToList();
            }
            if (!String.IsNullOrEmpty(orderBy))
            {
                orderBy = "operationId";
            }
            operations = operations.AsQueryable().OrderByPropertyName(orderBy, reverse).ToList();
            operations = operations.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Operations = operations
            };
            return Ok(result);



        }
        public IHttpActionResult Post(Operation operation)
        {
            return Ok();
        }
        public IHttpActionResult Put(Operation operation)
        {
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
