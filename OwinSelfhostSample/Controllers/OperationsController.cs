using Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OwinSelfhostSample.Controllers
{

    public class OperationsController : ApiController
    {
        private static List<Operation> _Db = new List<Operation>
            {
                new Operation { operationId= 1, required=true, type=new string[] {"1","2" } },
                new Operation { operationId =2, required=true,type=new string[] {"1","2" } },
                new Operation { operationId =3, required=true ,type=new string[] {"1","2" }}
            };
        public IEnumerable<Operation> Get()
        {
            return _Db;
        }
        public Operation Get(int id)
        {
            var operation = _Db.FirstOrDefault(c => c.operationId == id);
            if (operation == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return operation;
        }
        public IHttpActionResult Post(Operation operation)
        {
            if (operation == null)
            {
                return BadRequest("Argument Null");
            }
            var companyExists = _Db.Any(c => c.operationId == operation.operationId);

            if (companyExists)
            {
                return BadRequest("Exists");
            }

            _Db.Add(operation);
            return Ok();
        }
        public IHttpActionResult Put(Operation operation)
        {
            if (operation == null)
            {
                return BadRequest("Argument Null");
            }
            var existing = _Db.FirstOrDefault(c => c.operationId == operation.operationId);

            if (existing == null)
            {
                return NotFound();
            }

            existing.required = operation.required;
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var operation = _Db.FirstOrDefault(c => c.operationId == id);
            if (operation == null)
            {
                return NotFound();
            }
            _Db.Remove(operation);
            return Ok();
        }
    }
}
