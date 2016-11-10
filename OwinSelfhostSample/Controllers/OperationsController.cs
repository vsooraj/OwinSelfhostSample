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

    [RoutePrefix("api/operations")]
    public class OperationsController : ApiController
    {
        private readonly IOperationsRepository _operationsRepository;

        public OperationsController(IOperationsRepository operationsRepository)
        {
            _operationsRepository = operationsRepository;
        }
        [Authorize]
        [HttpGet]
        public PageResult<Operation> Get(ODataQueryOptions options)
        {
            var operations = this._operationsRepository.GetOperations().ToList();
            var results = options.ApplyTo(operations.AsQueryable());
            return new PageResult<Operation>(results as IEnumerable<Operation>, Request.RequestUri, Request.ODataProperties().TotalCount);
        }
        [Authorize]
        [HttpGet]
        // GET: api/operations/5
        [Route("{id:int}")]
        public Operation Get(int id)
        {
            var operation = _operationsRepository.GetOperations().FirstOrDefault(c => c.operationId == id);
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
            var item = this._operationsRepository.GetOperations().FirstOrDefault(c => c.required == name);
            if (item == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return Ok(item);
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
