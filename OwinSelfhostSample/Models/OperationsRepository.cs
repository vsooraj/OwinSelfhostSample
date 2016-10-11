using Common;
using System.Collections.Generic;
using System.Linq;

namespace OwinSelfhostSample.Models
{
    public class OperationsRepository
    {
        private static List<Operation> _operations = new List<Operation>
            {
                new Operation { operationId= 1, required=true, type=new string[] {"1","2" } },
                new Operation { operationId =2, required=false,type=new string[] {"2","4" } },
                new Operation { operationId =3, required=false,type=new string[]{"3","6" } },
                new Operation { operationId= 4, required=true, type=new string[] {"4","2" } },
                new Operation { operationId= 5, required=true, type=new string[] {"5","2" } },
                new Operation { operationId =6, required=false,type=new string[] {"6","4" } },
                new Operation { operationId =7, required=false,type=new string[] {"7","4" } },
                new Operation { operationId= 8, required=true, type=new string[] {"8","2" } },
                new Operation { operationId =9, required=false,type=new string[] {"9","4" } },
                new Operation { operationId= 10, required=true, type=new string[] {"10","2" } },
                new Operation { operationId =11, required=false,type=new string[] {"11","4" } },
                new Operation { operationId =12, required=false,type=new string[] {"12","4" } },
                new Operation { operationId =13, required=false,type=new string[] {"13","4" } },
                new Operation { operationId =14, required=false,type=new string[] {"14","4" } },
                new Operation { operationId =15, required=false,type=new string[] {"15","4" } },
                new Operation { operationId =16, required=false,type=new string[] {"16","4" } },
                new Operation { operationId =17, required=false,type=new string[] {"17","4" } },
                new Operation { operationId =18, required=false,type=new string[] {"18","4" } }
            };
        public IQueryable<Operation> Operations
        {
            get { return _operations.AsQueryable(); }
        }
    }
}
