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
    public class SavingRepositoryTests
    {

        [Test]
        public void Add_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingRepository = scope.Resolve<ISavingRepository>();

                    var saving = fixture.Create<Saving>();
                    saving.UserId = fixture.Create<string>();
                    var result = savingRepository.Add(saving);

                    Assert.IsNotNull(result);
                    Assert.Greater(result.Id, 0);
                    Assert.AreEqual(result.Goal.ParentId, saving.Id);
                    Assert.AreEqual(result.Goal.ParentTypeId, GoalEntityType.Saving);
                }
                trans.Complete();
            }
        }

        [Test]
        public void Get_ReturnsList_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingRepository = scope.Resolve<ISavingRepository>();

                    var savings = fixture.CreateMany<Saving>();

                    foreach (var item in savings)
                    {
                        savingRepository.Add(item);
                    }

                    var result = savingRepository.Get();

                    Assert.IsNotNull(result);
                    Assert.Greater(result.Count(), 0);
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
                    var savingRepository = scope.Resolve<ISavingRepository>();

                    var saving = fixture.Create<Saving>();

                    savingRepository.Add(saving);

                    savingRepository.Delete(saving);

                    var result = savingRepository.Get(saving.Id);

                    Assert.IsNull(result);
                }
            }
        }

  

        

    }
}
