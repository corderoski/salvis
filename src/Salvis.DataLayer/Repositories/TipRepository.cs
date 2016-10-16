using System.Collections.Generic;
using System.Linq;
using Dapper;
using Salvis.Entities;
using System.Data;

namespace Salvis.DataLayer.Repositories
{
    public class TipRepository : RepositoryBase<Tip>, ITipRepository
    {

        public TipRepository(IDbConnection connection)
            : base(connection)
        {

        }

        public IEnumerable<Tip> GetRandomItemsByQuantity(int quantity)
        {
            return Connection.Query<Tip>(
                string.Format("SELECT TOP {1} * FROM {0} Order By NewId()", EntityTableSchema, quantity));
        }

        public IEnumerable<Tip> GetRandomItemsByPercent(int percent)
        {
            return Connection.Query<Tip>(
                string.Format("SELECT TOP {1} PERCENT * FROM {0} Order By NewId() ", EntityTableSchema, percent));
        }
    }
}
