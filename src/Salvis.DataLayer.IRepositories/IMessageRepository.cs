using System.Collections.Generic;
using System.Threading.Tasks;
using Salvis.Entities;

namespace Salvis.DataLayer.Repositories
{
    public interface IMessageRepository : IRepositoryBaseOperation<Message>
    {
        IEnumerable<Message> GetByUserId(string userId, MessageState state);
        IEnumerable<Message> GetByUserId(string userId);

        Task MarkAsReadAsync(long id);
    }
}
