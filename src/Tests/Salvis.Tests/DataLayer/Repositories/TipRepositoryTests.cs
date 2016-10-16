using Autofac;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using System;
using System.Linq;
using System.Transactions;

namespace Salvis.Tests.DataLayer.Repositories
{

    [TestFixture]
    public class TipRepositoryTests
    {

        [Test]
        public void TryConnection_Success()
        {
            using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
            {
                var repository = scope.Resolve<ITipRepository>();
                var result = repository.TryConnection();
                //
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void Add_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<ITipRepository>();
                    var fixture = CompositionRoot.FixtureInstance;

                    var item = fixture.Create<Tip>();
                    var result = repository.Add(item);

                    Assert.IsTrue(item.Id == result.Id);
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
                    var repository = scope.Resolve<ITipRepository>();
                    var fixture = CompositionRoot.FixtureInstance;

                    var item = fixture.Create<Tip>();
                    item = repository.Add(item);

                    var result = repository.Get(item.Id);

                    Assert.IsNotNull(result.Id);
                    Assert.IsTrue(item.Id == result.Id);
                }
            }
        }

        [TestCase(4)]
        public void GetRandomItems_Success(int itemsForCreation)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<ITipRepository>();
                    var fixture = CompositionRoot.FixtureInstance;

                    var items = fixture.CreateMany<Tip>(itemsForCreation);
                    foreach (var item in items)
                        repository.Add(item);

                    var result = repository.GetRandomItemsByQuantity(itemsForCreation);

                    Assert.IsNotEmpty(result);
                    Assert.AreEqual(itemsForCreation, result.Count());
                }
            }
        }

        [TestCase(10, 20)]
        [TestCase(10, 50)]
        [TestCase(8, 50)]
        public void GetRandomItemsByPercent_Success(int itemsForCreation, int percentForRequest)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<ITipRepository>();
                    var fixture = CompositionRoot.FixtureInstance;

                    var items = fixture.CreateMany<Tip>(itemsForCreation);
                    foreach (var item in items)
                        repository.Add(item);

                    var result = repository.GetRandomItemsByPercent(percentForRequest);

                    Assert.IsNotEmpty(result);
                    Assert.AreEqual((itemsForCreation * percentForRequest) / 100, result.Count());
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
                    var repository = scope.Resolve<ITipRepository>();
                    var fixture = CompositionRoot.FixtureInstance;

                    var items = fixture.CreateMany<Tip>(itemsForCreation);
                    foreach (var item in items)
                        repository.Add(item);

                    var result1 = repository.GetRandomItemsByQuantity(itemsForCreation);
                    var result2 = repository.Get();

                    Assert.IsNotEmpty(result1);
                    Assert.IsNotEmpty(result2);
                    Assert.AreEqual(itemsForCreation, result1.Count());
                    Assert.AreEqual(result1.Count(), result2.Count());
                }
            }
        }


    }
}
