using Salvis.Entities;
using System.Collections.Generic;

namespace Salvis.Framework.Services
{
    public interface ICatalogService
    {

        IEnumerable<Catalog> Get(string categoryId);

        Catalog Get(string categoryId, int subCategoryId);

    }
}
