using Autofac;
using NUnit.Framework;
using Salvis.Entities;
using Salvis.Framework.Services;

namespace Salvis.Tests.Framework.UnitTests.Services
{
    [TestFixture]
    public class CatalogServiceTests
    {

        [TestCase(Catalog.DEBT_TYPE)]
        [TestCase(Catalog.SAVING_TYPE)]
        public void Get_WithCategoryId_NotEmpty(string categoryId)
        {
            using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
            {
                var service = scope.Resolve<ICatalogService>();

                var result = service.Get(categoryId);

                Assert.IsNotEmpty(result, string.Format("Ups!, No se encontrado ningún resultado basado en esta category {0}", categoryId));
            }
        }

        [TestCase(Catalog.API_PROVIVDER)]
        [TestCase(Catalog.APP_CURRENCY)]
        [TestCase(Catalog.DEBT_TYPE)]
        [TestCase(Catalog.SAVING_TYPE)]
        public void Get_WithCategoryIdAndWithSubCategoryId_NotEmpty(string categoryId)
        {
            using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
            {
                var service = scope.Resolve<ICatalogService>();
                var subCategory = 1;
                var result = service.Get(categoryId, subCategory);

                Assert.IsNotNull(result, string.Format("Ups!, No se encontrado ningún resultado basado en esta category {0} y esta subCategory {1}", categoryId, subCategory));
            }

        }

    }
}