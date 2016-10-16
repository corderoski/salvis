using NUnit.Framework;
using System.Transactions;
using Ploeh.AutoFixture;
using Autofac;
using Salvis.Entities;
using Salvis.DataLayer.Repositories;

namespace Salvis.Tests.DataLayer.Repositories
{

    [TestFixture]
    public class RecurrentRepositoryTests
    {

        [Test]
        public void Add_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IRecurrentRepository>();

                    var item = fixture.Create<Recurrent>();

                    var result = repository.Add(item);

                    Assert.IsNotNull(result);
                    Assert.Greater(result.Id, 0);
                }
            }
        }

        [Test]
        public void Get_SaveAndRquest_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IRecurrentRepository>();
                    var item = fixture.Create<Recurrent>();
                    repository.Add(item);
                    //  
                    var result = repository.Get(item.Id);

                    Assert.IsNotNull(result);
                    Assert.AreEqual(result.Goal.ParentId, item.Id);
                    Assert.AreEqual(result.Goal.ParentTypeId, GoalEntityType.Recurrent);
                }
            }
        }

        [Test]
        public void Delete_SaveAndDelete_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IRecurrentRepository>();
                    var item = fixture.Create<Recurrent>();
                    repository.Add(item);
                    //  
                    repository.Delete(item);
                    var result = repository.Get(item.Id);

                    Assert.IsNull(result);
                }
            }
        }

        [Test]
        public void DeleteByCode_SaveAndDelete_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IRecurrentRepository>();
                    var item = fixture.Create<Recurrent>();
                    repository.Add(item);
                    //  
                    repository.DeleteByCode(item.Code);
                    var result = repository.Get(item.Id);

                    Assert.IsNull(result);
                }
            }
        }

    }
}
