using NUnit.Framework;
using Salvis.Framework.OnlineServices;
using System;
using System.Transactions;
using Ploeh.AutoFixture;
using Autofac;
using Salvis.Framework.OnlineServices.Factory;

namespace Salvis.Tests.Framework.OnlineServices.UnitTests
{

    [TestFixture]
    public class AppleITunesOnlineServiceTests
    {

        
        [Test]
        [TestCase("iTunes")]
        public void GetContentTypes_ReturnsValidList_Success(string category)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {

                    var factory = scope.Resolve<IOnlineServiceFactory>();
                    var itunesApi = factory.Create(OnlineProvider.iTunes);

                    var result = itunesApi.GetContentTypes();

                    Assert.IsNull(result, "The list must not be empty.");
                }
            }
        }

        [Test]
        [TestCase("podcast")]
        [TestCase("ebook")]
        [TestCase("audiobook")]
        [TestCase("tvShow")]
        public void GetResultOfSearch_ReturnsData_Success(string type)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {

                    var factory = scope.Resolve<IOnlineServiceFactory>();
                    var itunesApi = factory.Create(OnlineProvider.iTunes);

                    var result = itunesApi.GetContentByType(type, "batman");

                    Assert.IsNotEmpty(result, "The list must not be empty.");
                }
            }
        }

        [TestCase("ebook", "Ahorros")]
        [TestCase("ebook", "Cuentas_de_Ahorros")]
        [TestCase("ebook", "Cuentas de Ahorros")]
        [TestCase("ebook", "Prestamos+hipotecarios")]
        [TestCase("ebook", "Prestamos,hipotecarios")]
        [TestCase("ebook", "Prestamos hipotecarios")]
        public void GetResultOfSearchWithInvalidTerm_ReturnsData_Success(string type, string term)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {

                    var factory = scope.Resolve<IOnlineServiceFactory>();
                    var itunesApi = factory.Create(OnlineProvider.iTunes);

                    var result = itunesApi.GetContentByType(type, term);

                    Assert.IsNotNull(result, "The list must not be empty.");
                }
            }
        }

    }

}
