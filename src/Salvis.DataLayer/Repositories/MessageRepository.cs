using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Salvis.Entities;

namespace Salvis.DataLayer.Repositories
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {

        public MessageRepository(IDbConnection connection)
            : base(connection)
        {
        }

        new public Message Add(Message item)
        {
            item.State = (int)MessageState.Unread;
            return base.Add(item);
        }

        public IEnumerable<Message> GetByUserId(string userId)
        {
            var sql = String.Format("SELECT * FROM {0} WHERE UserId = @userId", EntityTableSchema);
            return Connection.Query<Message>(sql, new { userId });
        }


        public IEnumerable<Message> GetByUserId(string userId, MessageState state)
        {
            var sql = String.Format("SELECT * FROM {0} WHERE UserId = @userId AND State = @stateId", EntityTableSchema);

            return Connection.Query<Message>(sql, new { userId, stateId = (int)state });
        }

        public Task MarkAsReadAsync(Int64 id)
        {
            var sql = String.Format("UPDATE {0} SET State = @state WHERE Id = @id", EntityTableSchema);
            var r = Connection.ExecuteAsync(sql, new { id, state = (int)MessageState.Read });
            return r;
        }

    }
}
