using Common;
using System.Linq;

namespace OwinSelfhostSample.Models
{
    public interface IOperationsRepository
    {
        IQueryable<Operation> GetOperations();
    }
}
