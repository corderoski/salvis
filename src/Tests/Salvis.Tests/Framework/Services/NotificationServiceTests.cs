//using System;
//using System.Linq;
//using System.Transactions;
//using Autofac;
//using NUnit.Framework;
//using Ploeh.AutoFixture;
//using Salvis.Entities;
//using Salvis.Entities.Notifications;
//using Salvis.Framework.Services;

//namespace Salvis.Tests.Framework.UnitTests.Services
//{

//    [TestFixture]
//    public class NotificationServiceTests
//    {

//        [Test]
//        public void Add_Success()
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var service = scope.Resolve<INotificationService>();

//                    var item = fixture.Create<Notification>();

//                    var result = service.Add(item);

//                    Assert.IsNotNull(result);
//                }
//            }
//        }


//        [Test]
//        public void Add_AddIEnumerable_Success()
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var service = scope.Resolve<INotificationService>();

//                    var result = service.Add(fixture.Create<Goal>(),
//                        fixture.CreateMany<Notification>(), fixture.CreateMany<TimeInterval>());

//                    Assert.IsNotNull(result);
//                }
//            }
//        }

//        [Test]
//        public void Delete_Success()
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var service = scope.Resolve<INotificationService>();

//                    var item = fixture.Create<Notification>();

//                    var result = service.Add(item);
//                    service.Delete(result);

//                    Assert.IsNotNull(result);
//                }
//            }
//        }

//        [TestCase(4)]
//        public void GetByTimeIntervals_AddIEnumerable_Success(int count)
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationService>();

//                    var items = fixture.CreateMany<Notification>(count);
//                    repository.Add(fixture.Create<Goal>(),items, fixture.CreateMany<TimeInterval>(count));

//                    var init = items.OrderBy(i => i.ReleaseDate).First().ReleaseDate;
//                    var final = items.OrderBy(i => i.ReleaseDate).Last().ReleaseDate;

//                    var result = repository.GetByTimeIntervals(init, final);

//                    Assert.IsNotNull(result);
//                    Assert.GreaterOrEqual(result.Count(), count);
//                }
//            }
//        }

//        [Test]
//        public void GetByTimeIntervals_AddIEnumerableUsingDateNow_Success()
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationService>();

//                    var items = fixture.CreateMany<Notification>();
//                    foreach (var notification in items)
//                        notification.ReleaseDate = DateTimeOffset.Now.DateTime.AddSeconds(fixture.Create<double>());
//                    repository.Add(fixture.Create<Goal>(), items, fixture.CreateMany<TimeInterval>());
//                    //
//                    var result = repository.GetByTimeIntervals(DateTimeOffset.Now.DateTime, DateTimeOffset.Now.DateTime);
//                    //
//                    Assert.IsNotNull(result);
//                }
//            }
//        }

//        [Test]
//        public void GetLastExeceution_Success()
//        {
//            using (var trans = new TransactionScope())
//            {
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationBoardService>();

//                    var result = repository.GetLastExecution();
//                    //Could be Null or Not Null, do not test.
//                    //Assert.IsNull(result);
//                }
//            }
//        }



//    }

//}
