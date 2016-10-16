using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Salvis.Entities;

namespace Salvis.Framework.Services
{
    public interface IMessageService : IServiceBaseOperation<Message>
    {
        IEnumerable<Message> GetUnreadByUserId(string userId);
        IEnumerable<Message> GetByUserId(string userId, MessageState state);
        IEnumerable<Message> GetByUserId(string userId);

        Task MarkAsReadAsync(Int64 id);
    }
}
