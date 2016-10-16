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
    public class RecurrentServiceTests
    {

        [TestCase("2015/4/15", TimeInterval.BiWeekly, 1000f)]
        [TestCase("2015/2/01", TimeInterval.Dialy, 150f)]
        [TestCase("2015/10/10", TimeInterval.Dialy, 1889.45f)]
        [TestCase("2016/06/30", TimeInterval.Monthly, 3497f)]
        public void Add_ValidItem_Success(string stringEndDate, TimeInterval tm, float amount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var service = scope.Resolve<IRecurrentService>();

                    var startDate = new DateTime(2015, 1, 1);
                    DateTime? endDate = DateTime.Parse(stringEndDate);

                    var result = service.Validate(startDate, endDate, amount, tm);
                    var goal = result.Result as Goal;

                    var item = fixture.Create<Recurrent>();
                    {
                        item.Goal.Amount = goal.Amount;
                        item.Goal.EndDate = goal.EndDate;
                        item.Goal.StartDate = goal.StartDate;
                    }

                    //
                    var serviceResult = service.Add(item, tm);

                    //
                    Assert.IsTrue(item != null);
                }
            }
        }


        [TestCase("2015/4/15", TimeInterval.BiWeekly, 1000f)]
        [TestCase("2015/2/28", TimeInterval.Dialy, 150f)]
        [TestCase("2015/01/22", TimeInterval.Monthly, 1889.45f)]
        [TestCase("2015/11/15", TimeInterval.Monthly, 3497f)]
        public void Validate_ValidItem_Success(string strEndtDate, TimeInterval tm, float amount)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //
                    var service = scope.Resolve<IRecurrentService>();

                    var startDate = new DateTime(2015, 1, 1);
                    var endDate = DateTime.Parse(strEndtDate);

                    //
                    var result = service.Validate(startDate, endDate, amount, tm);

                    //
                    Assert.IsEmpty(result.Errors);
                    Assert.IsTrue(result.Result is Goal);
                }
            }
        }


        [TestCase("2015/4/15", TimeInterval.BiWeekly, 1000f)]
        [TestCase("2015/3/01", TimeInterval.Dialy, 150f)]
        [TestCase("2015/01/22", TimeInterval.Monthly, 1889.45f)]
        [TestCase("2014/11/15", TimeInterval.Monthly, 3497f)]
        public void Validate_ValidItemWithoutEndDate_Success(string stringStartDate, TimeInterval tm, float amount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //
                    var service = scope.Resolve<IRecurrentService>();

                    var startDate = DateTime.Parse(stringStartDate);
                    //
                    var result = service.Validate(startDate, null, amount, tm);

                    //
                    Assert.IsEmpty(result.Errors);
                    Assert.IsTrue(result.Result is Goal);
                }
            }
        }



        [TestCase("2015/4/15", 45, 1000f)]
        [TestCase("2015/2/28", TimeInterval.Dialy, 0f)]
        [TestCase("2014/11/15", TimeInterval.Monthly, 0f)]
        public void Validate_InvalidItem_ReturnsError(string strStartDate, TimeInterval tm, float amount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //
                    var service = scope.Resolve<IRecurrentService>();

                    var startDate = DateTime.Parse(strStartDate);
                    //
                    var result = service.Validate(startDate, null, amount, tm);

                    //
                    Assert.IsNull(result.Result);
                    Assert.IsNotEmpty(result.Errors);
                }
            }
        }

    }
}
