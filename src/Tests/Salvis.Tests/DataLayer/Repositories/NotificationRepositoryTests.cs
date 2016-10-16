//using System.Linq;
//using NUnit.Framework;
//using System.Transactions;
//using Ploeh.AutoFixture;
//using Autofac;
//using Salvis.Entities;
//using Salvis.DataLayer.Repositories;
//using Salvis.Entities.Notifications;

//namespace Salvis.Tests.DataLayer.Repositories
//{
//    [TestFixture]
//    public class NotificationRepositoryTests
//    {
//        [Test]
//        public void Add_Success()
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationRepository>();

//                    var item = fixture.Create<Notification>();

//                    var result = repository.Add(item);

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
//                    var repository = scope.Resolve<INotificationRepository>();

//                    var items = fixture.CreateMany<Notification>();
//                    var result = repository.Add(items);

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
//                    var repository = scope.Resolve<INotificationRepository>();

//                    var item = fixture.Create<Notification>();

//                    var result = repository.Add(item);
//                    repository.Delete(result);

//                    Assert.IsNotNull(result);
//                }
//            }
//        }

//        [Ignore("Could be Null or Not Null, do not test")]
//        public void GetLastExeceution_NotExecutionRegistered_ReturnsNull()
//        {
//            using (var trans = new TransactionScope())
//            {
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationRepository>();
//                    var result = repository.GetLastExecution();
                    
//                    //Could be Null or Not Null, do not test.
//                    //Assert.IsNull(result);
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
//                    var repository = scope.Resolve<INotificationRepository>();

//                    var items = fixture.CreateMany<Notification>(count);
//                    repository.Add(items);

//                    var init = items.OrderBy(i => i.ReleaseDate).First().ReleaseDate;
//                    var final = items.OrderBy(i => i.ReleaseDate).Last().ReleaseDate;

//                    var result = repository.GetByTimeIntervals(init, final);

//                    Assert.IsNotNull(result);
//                    //Assert.AreEqual(count, result.Count());
//                }
//            }
//        }

//    }
//}
