using Salvis.Entities;
using System.Collections.Generic;

namespace Salvis.DataLayer.Repositories
{
    public interface ICatalogRepository : IRepository<Catalog>
    {

        IEnumerable<Catalog> GetAll(string categoryId);

        IEnumerable<Catalog> Get(string categoryId);

        Catalog Get(string categoryId, int subCategoryId);

    }
}
