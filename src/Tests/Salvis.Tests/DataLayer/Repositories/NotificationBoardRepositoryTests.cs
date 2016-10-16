//using System;
//using NUnit.Framework;
//using System.Transactions;
//using Ploeh.AutoFixture;
//using Autofac;
//using Salvis.DataLayer.Repositories;

//namespace Salvis.Tests.DataLayer.Repositories
//{
//    [TestFixture]
//    public class NotificationBoardRepositoryTests
//    {

//        [Test]
//        public void GetLastExeceution_Success()
//        {
//            //  initialize  -   Call another Test for creating dummydata
//            CloseExecution_Success(null);

//            using (var trans = new TransactionScope())
//            {
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationRepository>();
                    
//                    var result = repository.GetLastExecution();
                    
//                    Assert.IsNotNull(result);
//                }
//            }
//        }

//        [TestCase("2014/12/03")]
//        [TestCase(null)]
//        public void GetNewExecution_Success(string date)
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationRepository>();

//                    var startDate = String.IsNullOrEmpty(date) ? fixture.Create<DateTime>() : DateTime.Parse(date);
//                    var @new = repository.GetNewExecution(startDate);

//                    Assert.IsNotNull(@new);
//                }
//            }
//        }

//        [TestCase("2015/03/03")]
//        [TestCase(null)]
//        public void CloseExecution_Success(string date)
//        {
//            using (var trans = new TransactionScope())
//            {
//                var fixture = CompositionRoot.FixtureInstance;
//                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
//                {
//                    var repository = scope.Resolve<INotificationRepository>();

//                    var startDate = String.IsNullOrEmpty(date) ? fixture.Create<DateTime>() : DateTime.Parse(date);
//                    var entity = repository.GetNewExecution(startDate);
//                    var result = repository.CloseExecution(entity);

//                    Assert.IsTrue(result);
//                }
//            }
//        }

//    }
//}
