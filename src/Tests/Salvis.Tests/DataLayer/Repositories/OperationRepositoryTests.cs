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
    public class OperationRepositoryTests
    {

        [Test]
        public void AddMany_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IOperationRepository>();

                    var id = fixture.Create<long>();
                    var parentTypeId = fixture.Create<GoalEntityType>();
                    var items = fixture.Build<Operation>()
                                        .With(i => i.GoalId, id)
                                        .With(op => op.GoalTypeId, parentTypeId)
                                        .CreateMany<Operation>();

                    var result = repository.Add(items);

                    Assert.IsNotEmpty(result);
                    Assert.IsTrue(result.Where(i => i.GoalId == id && i.GoalTypeId == parentTypeId).Count() == items.Count());
                    Assert.IsTrue(result.Count() == items.Count());
                }
            }
        }

        [Test]
        public void Get_GetAllItems_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //
                    var repository = scope.Resolve<IOperationRepository>();

                    var id = fixture.Create<long>();
                    var parentTypeId = fixture.Create<GoalEntityType>();
                    var items = fixture.Build<Operation>()
                                        .With(i => i.GoalId, id)
                                        .With(op => op.GoalTypeId, parentTypeId)
                                        .CreateMany<Operation>();
                    repository.Add(items);

                    //
                    var result = repository.Get(id, parentTypeId);

                    //
                    Assert.IsNotEmpty(result);
                    Assert.IsTrue(result.Where(i => i.GoalId == id && i.GoalTypeId == parentTypeId).Count() == items.Count());
                    Assert.IsTrue(result.Count() == items.Count());
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
                    //
                    var repository = scope.Resolve<IOperationRepository>();

                    var item = fixture.Create<Goal>();
                    fixture.Inject(item);
                    var items = fixture.CreateMany<Operation>();
                    repository.Add(items);

                    //
                    repository.Delete(items);

                    //
                    var result = repository.Get(item.ParentId, item.ParentTypeId);
                    Assert.IsEmpty(result);
                }
            }
        }

    }
}
