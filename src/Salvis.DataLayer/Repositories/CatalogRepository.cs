using Dapper;
using Salvis.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Salvis.DataLayer.Repositories
{
    public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
    {

        public CatalogRepository(IDbConnection connection)
            :base(connection)
        {

        }

        public IEnumerable<Catalog> GetAll(string categoryId)
        {
            var sql = String.Format("Select * from {0} where Category = @cat", EntityTableSchema);
            return Connection.Query<Catalog>(sql, new { cat = categoryId });
        }

        public IEnumerable<Catalog> Get(string categoryId)
        {
            var sql = String.Format("Select * from {0} where Enable = 1 AND Category = @cat", EntityTableSchema);
            return Connection.Query<Catalog>(sql, new { cat = categoryId });
        }

        public Catalog Get(string categoryId, int subCategoryId)
        {
            var sql = String.Format("Select * from {0} where Enable = 1 AND Category = @cat AND SubCategoryId = @subId", EntityTableSchema);
            return Connection.Query<Catalog>(sql, new { cat = categoryId, subId = subCategoryId }).FirstOrDefault();
        }
    }
}
