using System;
using System.Linq;
using NUnit.Framework;
using System.Transactions;
using Ploeh.AutoFixture;
using Autofac;
using Salvis.Entities;
using Salvis.Framework.Services;

namespace Salvis.Tests.Framework.UnitTests.Services
{
    [TestFixture]
    public class DebtServiceTests
    {

        [TestCase("2014/4/15", TimeInterval.BiWeekly, 1000f, null)]
        [TestCase("", TimeInterval.Monthly, 10000f, 50000f)]
        [TestCase("2014/3/1", TimeInterval.Monthly, null, 225000f)]
        [TestCase("",TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/3/1", TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/6/1", TimeInterval.Monthly, null, 90000f)]
        public void Add_ValidItem_Success(string stringEndDate, TimeInterval tm, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<IDebtService>();

                    var startDate = new DateTime(2014, 1, 1);
                    DateTime? endDate = String.IsNullOrEmpty(stringEndDate) ? fixture.Create<DateTime>(new DateTime(2014, 2, 1)) : DateTime.Parse(stringEndDate);

                    var result = service.Validate(startDate, endDate, partAmount, fullAmount, tm);
                    var goal = result.Result as Goal;

                    var item = fixture.Create<Debt>();
                    {
                        item.Goal.Amount = goal.Amount;
                        item.Goal.EndDate = goal.EndDate;
                        item.Goal.StartDate = goal.StartDate;
                    }

                    service.Add(item, tm);

                    Assert.IsTrue(item != null);
                }
            }
        }

        [TestCase("", TimeInterval.Monthly, 50000f)]
        [TestCase("2014/3/1", TimeInterval.Monthly,  225000f)]
        [TestCase("2014/6/1", TimeInterval.Monthly,  90000f)]
        public void Get_Success(string stringEndDate, TimeInterval tm,  float fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //  init
                    var service = scope.Resolve<IDebtService>();
                    var startDate = new DateTime(2014, 1, 1);
                    DateTime? endDate = String.IsNullOrEmpty(stringEndDate) ? fixture.Create<DateTime>(new DateTime(2014, 2, 1)) : DateTime.Parse(stringEndDate);

                    var item = fixture.Create<Debt>();
                    {
                        item.Goal.Amount = fullAmount;
                        item.Goal.EndDate = endDate.Value;
                        item.Goal.StartDate = startDate;
                    }

                    //  
                    service.Add(item, tm);
                    var result = service.Get(item.Id);

                    //  assert
                    Assert.IsTrue(item != null);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Goal);
                    Assert.IsNotEmpty(result.Goal.OperationDetails);
                    Assert.Greater(result.Goal.OperationDetails.Count(), 0);
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
                    var service = scope.Resolve<IDebtService>();

                    DateTime startDate = new DateTime(2014, 1, 1);
                    DateTime? endDate = new DateTime(2014, 3, 1);

                    var result = service.Validate(startDate, endDate, partAmount, fullAmount, tm);

                    Assert.IsTrue(result.Result != null);
                }
            }
        }


        [TestCase(TimeInterval.Monthly, 10000f, 50000f)]
        public void Validate_NoEndDate_Success(TimeInterval tm, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<IDebtService>();

                    var startDate = new DateTime(2014, 1, 1);

                    var result = service.Validate(startDate, null, partAmount, fullAmount, tm);

                    Assert.IsTrue(result.Result != null);
                }
            }
        }


        [TestCase(TimeInterval.BiWeekly, null, null)]
        public void Validate_NoSendingAnyAmount_Fail(TimeInterval tm, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //  init
                    var service = scope.Resolve<IDebtService>();
                    //  action
                    DateTime startDate = new DateTime(2014, 1, 1);
                    DateTime? endDate = new DateTime(2014, 3, 1);
                    
                    var result = service.Validate(startDate, endDate, partAmount, fullAmount, tm);
                    //  assert
                    Assert.IsNull(result.Result);
                }
            }
        }

    }
}
