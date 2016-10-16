using Salvis.Entities;

namespace Salvis.DataLayer.Repositories
{
    public interface ITipRepository : IRepositoryBaseOperation<Tip>
    {
        
        System.Collections.Generic.IEnumerable<Tip> GetRandomItemsByQuantity(int quantity);

        System.Collections.Generic.IEnumerable<Tip> GetRandomItemsByPercent(int percent);
    }
}
