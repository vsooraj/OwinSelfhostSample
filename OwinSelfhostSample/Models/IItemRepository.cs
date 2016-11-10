using Common;
using System.Linq;

namespace OwinSelfhostSample.Models
{
    public interface IItemRepository
    {
        IQueryable<Item> GetItems();
        void Remove(int id);
    }
}
