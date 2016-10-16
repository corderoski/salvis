using System.Linq;
using System.Diagnostics;
using System.Transactions;
using Autofac;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Salvis.Entities;
using Salvis.Framework.Services;

namespace Salvis.Tests.Framework.UnitTests.Services
{
    [TestFixture]
    public class TipServiceTests
    {
        [Test]
        public void Add_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<ITipService>();
                    var message = fixture.Create<Tip>();
                    var itemSaved = service.Add(message);
                    //
                    Assert.IsTrue(itemSaved.Id > 0);
                }
            }
        }


        [Test]
        public void Get_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<ITipService>();
                    var item = fixture.Create<Tip>();
                    service.Add(item);

                    var result = service.Get(item.Id);

                    Assert.IsNotNull(result);
                }
            }
        }


        [TestCase(5, 1)]
        [TestCase(7, 4)]
        [TestCase(4, 2)]
        public void GetRandomItems_Success(int itemsForCreation, int itemsForRequest)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<ITipService>();
                    var fixture = CompositionRoot.FixtureInstance;

                    var items = fixture.CreateMany<Tip>(itemsForCreation);
                    foreach (var item in items)
                        service.Add(item);

                    var result = service.GetRandom(itemsForRequest);

                    Assert.IsNotEmpty(result);
                    Assert.AreEqual(itemsForRequest, result.Count());
                }
            }
        }

        [TestCase(4)]
        public void GetRandomItems__SaveAndCompareGetsFunctions_Success(int itemsForCreation)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<ITipService>();
                    var fixture = CompositionRoot.FixtureInstance;

                    var items = fixture.CreateMany<Tip>(itemsForCreation);
                    foreach (var item in items)
                        service.Add(item);

                    var result1 = service.GetRandom(itemsForCreation);
                    var result2 = service.Get();

                    Assert.IsNotEmpty(result1);
                    Assert.IsNotEmpty(result2);
                    Assert.AreEqual(itemsForCreation, result1.Count());
                    Assert.AreEqual(result1.Count(), result2.Count());
                }
            }
        }

         //[Test]
        //public void Delete_ReturnsNull()
        //{
        //    using (var trans = new TransactionScope())
        //    {
        //        using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
        //        {
        //            var fixture = CompositionRoot.FixtureInstance;
        //            var service = scope.Resolve<ITipService>();
        //            var item = fixture.Create<Tip>();
        //            var itemSaved = service.Add(item);

        //            service.Delete(itemSaved);

        //            var result = service.Get(itemSaved.Id);
        //            Assert.IsNull(result);
        //        }
        //    }
        //}

        
    }
}