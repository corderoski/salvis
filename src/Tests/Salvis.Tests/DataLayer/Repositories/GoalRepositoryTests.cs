using System;
using System.Linq;
using NUnit.Framework;
using System.Transactions;
using Ploeh.AutoFixture;
using Autofac;
using Salvis.Entities;
using Salvis.DataLayer.Repositories;

namespace Salvis.Tests.DataLayer.Repositories
{

    [TestFixture]
    public class GoalRepositoryTests
    {
        [Test]
        public void Add_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IGoalRepository>();

                    var item = fixture.Create<Goal>();

                    var result = repository.Add(item);

                    Assert.IsNotNull(result);
                }
            }
        }

        [Test]
        public void Get_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IGoalRepository>();
                    var item = fixture.Create<Goal>();
                    repository.Add(item);

                    //
                    var result = repository.Get(item.ParentId, item.ParentTypeId);

                    //
                    Assert.IsNotNull(result);
                }
            }
        }

        [Test]
        public void Delete_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IGoalRepository>();
                    var item = fixture.Create<Goal>();
                    repository.Add(item);

                    //
                    repository.Delete(item.ParentId, item.ParentTypeId);

                    //
                    var result = repository.Get(item.ParentId, item.ParentTypeId);
                    Assert.IsNull(result);
                }
            }
        }


    }
}
