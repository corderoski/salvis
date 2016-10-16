using Salvis.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.Services
{
    public class CatalogService : ICatalogService
    {

        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public IEnumerable<Entities.Catalog> Get(string categoryId)
        {
            return _catalogRepository.Get(categoryId);
        }

        public Entities.Catalog Get(string categoryId, int subCategoryId)
        {
            return _catalogRepository.Get(categoryId, subCategoryId);
        }

    }
}
