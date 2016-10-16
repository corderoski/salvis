using System;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;
using System.Transactions;
using Ploeh.AutoFixture;
using Autofac;
using Ploeh.AutoFixture.AutoMoq;
using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using Salvis.Framework.Services;

namespace Salvis.Tests.Framework.UnitTests.Services
{
    [TestFixture]
    public class SavingServiceTests
    {
        //TODO: The 'BuilderGraphic_' test cases should be relocated to a more generic class .


        [TestCase("2014/4/15", TimeInterval.BiWeekly, 1000f, null)]
        [TestCase("2014/3/1", TimeInterval.Monthly, null, 80000f)]
        [TestCase("", TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/3/1", TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/6/1", TimeInterval.BiWeekly, null, 18000f)]
        public void Add_ValidItem_Success(string stringEndDate, TimeInterval tm, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingService = scope.Resolve<ISavingService>();

                    var startDate = new DateTime(2014, 1, 1);
                    DateTime? endDate = String.IsNullOrEmpty(stringEndDate)
                                            ? fixture.Create(new DateTime(2014, 2, 1))
                                            : DateTime.Parse(stringEndDate);

                    var result = savingService.Validate(startDate, endDate, partAmount, fullAmount, tm);
                    var goal = result.Result as Goal;

                    var saving = fixture.Create<Saving>();
                    {
                        saving.Goal.Amount = goal.Amount;
                        saving.Goal.EndDate = goal.EndDate;
                        saving.Goal.StartDate = goal.StartDate;
                    }

                    savingService.Add(saving, tm);

                    Assert.IsTrue(saving != null);
                }
            }
        }

        [TestCase(TimeInterval.BiWeekly, 2000f, null)]
        [TestCase(TimeInterval.BiWeekly, null, 18000f)]
        public void Validate_SendingDate_Success(TimeInterval tm, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingService = scope.Resolve<ISavingService>();

                    DateTime startDate = new DateTime(2014, 1, 1);
                    DateTime? endDate = new DateTime(2014, 3, 1);

                    var result = savingService.Validate(startDate, endDate, partAmount, fullAmount, tm);

                    Assert.IsTrue(result.Result != null);
                }
            }
        }


        [TestCase(TimeInterval.Monthly, 10000f, 50000f)]
        public void Validate_NoEndDate_Success(TimeInterval tm, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingService = scope.Resolve<ISavingService>();

                    DateTime startDate = new DateTime(2014, 1, 1);

                    var result = savingService.Validate(startDate, null, partAmount, fullAmount, tm);

                    Assert.IsTrue(result.Result != null);
                }
            }
        }


        [TestCase(TimeInterval.BiWeekly, null, null)]
        public void Validate_NoSendingAnyAmount_Fail(TimeInterval tm, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingService = scope.Resolve<ISavingService>();

                    DateTime startDate = new DateTime(2014, 1, 1);
                    DateTime? endDate = new DateTime(2014, 3, 1);

                    var result = savingService.Validate(startDate, endDate, partAmount, fullAmount, tm);

                    Assert.IsNull(result.Result);
                }
            }
        }

        [Test]
        public void Add_ItemFilled_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var savingService = scope.Resolve<ISavingService>();
                    var saving = fixture.Create<Saving>();

                    var timeInterval = fixture.Create<TimeInterval>();
                    var result = savingService.Add(saving, timeInterval);
                    Assert.IsTrue(result.Id > 0);
                }
            }
        }

        [TestCase(TimeInterval.BiWeekly)]
        [TestCase(TimeInterval.Dialy)]
        [TestCase(TimeInterval.Monthly)]
        [TestCase(TimeInterval.Weekly)]
        public void Add_ItemOperationNull_OperationNotNull(TimeInterval timeInterval)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var savingService = scope.Resolve<ISavingService>();
                    var saving = fixture.Create<Saving>();
                    saving.Goal.OperationDetails = null;


                    var result = savingService.Add(saving, timeInterval);

                    Assert.IsNotNull(result.Goal.OperationDetails);
                }
            }
        }

        [TestCase("2014/4/15", "2014/7/15", TimeInterval.Dialy, 50000f)]
        [TestCase("2014/4/15", "2014/5/15", TimeInterval.Monthly, 50000f)]
        [TestCase("2014/3/1", "2014/9/15", TimeInterval.BiWeekly, 80000f)]
        [TestCase("2014/3/1", "2014/8/15", TimeInterval.Dialy, 25000f)]
        [TestCase("2014/3/1", "2014/7/15", TimeInterval.Weekly, 2000f)]
        [TestCase("2014/6/1", "2014/12/15", TimeInterval.BiWeekly, 18000f)]
        public void Add_ItemOperationDetailNull_OperationDetailNotEmpty(string stringStartDate, string stringEndDate,
                                                                        TimeInterval timeInterval, float fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var savingService = scope.Resolve<ISavingService>();
                    var saving = fixture.Create<Saving>();

                    saving.Goal.OperationDetails = null;
                    saving.Goal.StartDate = Convert.ToDateTime(stringStartDate);
                    saving.Goal.EndDate = Convert.ToDateTime(stringEndDate);
                    saving.Goal.Amount = fullAmount;


                    var result = savingService.Add(saving, timeInterval);

                    Assert.IsNotEmpty(result.Goal.OperationDetails);
                }
            }
        }

        [Test]
        public void BuilderGraphic_GoalWithOperations_ReturnNotNull()
        {

            using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                var savingService = scope.Resolve<ISavingService>();
                var goal = fixture.Create<Goal>();


                var result = savingService.BuilderGraphic(goal);

                Assert.IsNotNull(result);
            }

        }

        //TODO: re-check this TestCase
        [TestCase]
        public void BuilderGraphic_GoalWithOperation_ReturnAnyLine()
        {
            using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                var savingService = scope.Resolve<ISavingService>();
                var operations = fixture.CreateMany<Operation>(150);

                var result = savingService.BuilderGraphic(new Goal { OperationDetails = operations });

                Assert.IsTrue(result.Line.Count == 9);
            }
        }

        [TestCase(TimeInterval.BiWeekly, 5000, 150514)]
        [TestCase(TimeInterval.Dialy, 1000, 300500)]
        [TestCase(TimeInterval.Monthly, 15000, 475500)]
        [TestCase(TimeInterval.Weekly, 600, 250500)]
        public void BuilderGraphic_GoalValidated_ReturnObjWithNextOperation(TimeInterval timeInterval, double partAmount, double amount)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var savingService = scope.Resolve<ISavingService>();
                    var goal = savingService.Validate(DateTime.Today.AddDays(-7), null, partAmount, amount, timeInterval).Result as Goal;

                    var result = savingService.BuilderGraphic(goal);

                    Assert.IsNotNull(result.NextOperation.NextDate > DateTime.Today);
                }
            }
        }
      
        [Test]
        public void Get_WithoutId_NotEmptyEnumerable()
        {
            using (var trans = new TransactionScope())
            {
                IFixture fixture = CompositionRoot.FixtureInstance;
                using (ILifetimeScope scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<ISavingService>();
                    AddItem(scope, fixture);
                    var result = service.Get();

                    Assert.IsNotEmpty(result);
                }
            }
        }

        [Test]
        public void Get_WithId_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<ISavingService>();

                    var itemSaved = AddItem(scope, fixture);
                    var result = service.Get(itemSaved.Id);

                    Assert.AreEqual(itemSaved.Code, result.Code);
                }
            }
        }

        [Test]
        public void Delete_ReturnNull()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<ISavingService>();
                    var itemSaved = AddItem(scope, fixture);

                    service.Delete(itemSaved);

                    var result = service.Get(itemSaved.Id);

                    Assert.IsNull(result);
                }
            }
        }

        private Saving AddItem(ILifetimeScope scope, IFixture fixture)
        {
            var service = scope.Resolve<ISavingService>();
            var saving = fixture.Create<Saving>();
            saving.ReasonTypeId = 1;
            var timeInterval = fixture.Create<TimeInterval>();

            return service.Add(saving, timeInterval);
        }

    }
}
