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
    public class GoalServiceTests
    {
        /*
        [TestCase("2015/12/31", null, null, 60000f)]
        [TestCase("2014/4/15", TimeInterval.BiWeekly, 1000f, null)]
        [TestCase("", TimeInterval.Monthly, 10000f, 50000f)]
        [TestCase("2014/3/1", TimeInterval.Monthly, null, 80000f)]
        [TestCase("", TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/3/1", TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/6/1", TimeInterval.BiWeekly, null, 18000f)]
        */

        [TestCase("2015/6/15", "2015/12/15", null, 15000f, TimeInterval.Quarterly)]
        [TestCase("2015/6/30", null, 2000f, 8000f, TimeInterval.Monthly)]
        [TestCase(null, "2015/12/1", null, 12000f, TimeInterval.BiWeekly)]
        [TestCase(null, "2015/12/1", 0f, 12000f, TimeInterval.BiWeekly)]
        [TestCase("2015/6/15", "2015/10/31", 1375f, 0f, TimeInterval.BiWeekly)]
        [TestCase(null, "2015/10/31", 1375f, 0f, TimeInterval.BiWeekly)]
        [TestCase("2015/06/19", null, 1000f, 4000f, TimeInterval.Weekly)]
        [TestCase(null, "2015/11/30", 0f, 32000f, TimeInterval.Weekly)]
        public void Validate_SendValidCombinations_Success(string stringStartDate, string stringEndDate, 
            float? partAmount, float? fullAmount, TimeInterval tm)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingService = scope.Resolve<ISavingService>();

                    var startDate = String.IsNullOrEmpty(stringStartDate) ?
                        (DateTime?)null : DateTime.Parse(stringStartDate);

                    var endDate = String.IsNullOrEmpty(stringEndDate) ?
                        (DateTime?)null : DateTime.Parse(stringEndDate);

                    var validationResult = savingService.Validate(startDate, endDate, partAmount, fullAmount, tm);
                    var result = validationResult.Result as Goal;
                    //
                    Assert.IsTrue(result != null, String.Join("|", validationResult.Errors));
                }
            }
        }

        [TestCase(null, null, null, 60000f)]
        [TestCase(null, null, 0f, 0f)]
        [TestCase(null, null, null, null)]
        [TestCase("", "", null, null)]
        [TestCase("2015/6/30", null, 0f, 8000f)]
        [TestCase("2015/12/30", "2014/05/5", null, null)]
        [TestCase("2015/12/30", "2014/05/5", null, 15000f)]
        public void Validate_SendInvalidCombinationsWithoutTimeInterval_Fail(string stringStartDate, string stringEndDate, float? partAmount, float? fullAmount)
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var savingService = scope.Resolve<ISavingService>();

                    var startDate = String.IsNullOrEmpty(stringStartDate) ?
                        (DateTime?)null : DateTime.Parse(stringStartDate);

                    var endDate = String.IsNullOrEmpty(stringEndDate) ?
                        (DateTime?)null : DateTime.Parse(stringEndDate);

                    var validationResult = savingService.Validate(startDate, endDate, partAmount, fullAmount);
                    //
                    Assert.IsNull(validationResult.Result);
                    Assert.IsTrue(validationResult.Errors.Any());
                }
            }
        }

        [TestCase("2014/4/15", TimeInterval.BiWeekly, 1000f, null)]
        [TestCase("", TimeInterval.Monthly, 10000f, 50000f)]
        [TestCase("2014/3/1", TimeInterval.Monthly, null, 80000f)]
        [TestCase("", TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/3/1", TimeInterval.BiWeekly, 2000f, null)]
        [TestCase("2014/6/1", TimeInterval.BiWeekly, null, 18000f)]
        public void NextOperationExpected_Success(string stringEndDate, TimeInterval tm, float? partAmount, float? fullAmount)
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

                    var validationResult = savingService.Validate(startDate, endDate, partAmount, fullAmount, tm);
                    var goal = validationResult.Result as Goal;
                    goal.Name = fixture.Create<String>();
                    goal.Description = fixture.Create<String>();
                    goal.TypeId = fixture.Create<int>();

                    var saving = fixture.Create<Saving>();
                    saving.Goal = goal;

                    savingService.Add(saving, tm);

                    var result = savingService.NextExpectedOperation(saving.Goal);

                    Assert.IsTrue(saving != null);
                    Assert.IsTrue(result != null);
                }
            }
        }



    }
}
