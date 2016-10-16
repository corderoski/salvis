using System.Diagnostics;
using System.Transactions;
using Autofac;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Salvis.Entities;
using Salvis.Framework.Services;

namespace Salvis.Tests.Framework.UnitTests.Services
{
    [TestFixture]
    public class MessageServiceTests
    {
        [Test]
        public void Add_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var message = fixture.Create<Message>();
                    message.State = (int)MessageState.Read;
                    var itemSaved = service.Add(message);
                    //
                    Assert.IsTrue(itemSaved.Id > 0);
                }
            }
        }

        [Test]
        public void Get_WithoutId_NotEmpty()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var message = fixture.Create<Message>();
                    service.Add(message);

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
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var message = fixture.Create<Message>();
                    var itemSaved = service.Add(message);

                    var result = service.Get(itemSaved.Id);

                    Assert.IsNotNull(result);
                }
            }
        }

        [Test]
        public void GetUnreadByUserId_WithUserId()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var message = fixture.Create<Message>();
                    var userId = fixture.Create<int>();

                    message.UserId = userId;
                    service.Add(message);
                    var result = service.GetByUserId(userId);

                    Assert.IsNotNull(result);
                }
            }
        }

        [TestCase(MessageState.Unread)]
        public void GetByUserid_PassMessageState_Success(MessageState state)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var userId = fixture.Create<int>();
                    var message = fixture.Create<Message>();

                    message.UserId = userId;
                    var stateId = (int)state;
                    message.State = stateId;
                    service.Add(message);

                    var result = service.GetByUserId(userId, state);

                    Assert.IsNotEmpty(result);
                }
            }
        }

        [TestCase(MessageState.Delete)]
        [TestCase(MessageState.Read)]
        [TestCase(MessageState.Unread)]
        public void GetByUserid_PassMessageStateAndNotExistsUser_Null(MessageState state)
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var userId = fixture.Create<int>();
                    var message = fixture.Create<Message>();
                    message.UserId = userId;
                    message.State = (int)state;
                    service.Add(message);

                    var result = service.GetByUserId(userId, state);

                    Assert.IsNotNull(result);
                }
            }
        }

        [Test]
        public void MarkAsReadAsync_AddEditAndGetsUpdatedUser_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var message = fixture.Create<Message>();

                    service.Add(message);

                    //  operation
                    service.MarkAsReadAsync(message.Id).Wait();

                    //  assert
                    var result = service.Get(message.Id);
                    Assert.IsNotNull(result);
                    Assert.AreEqual((int)MessageState.Read, result.State, "State must be changed.");
                }
            }
        }

        [Test]
        public void Delete_ReturnNull()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IMessageService>();
                    var message = fixture.Create<Message>();
                    var itemSaved = service.Add(message);

                    service.Delete(itemSaved);

                    var result = service.Get(itemSaved.Id);
                    Assert.IsNull(result);
                }
            }
        }
    }
}