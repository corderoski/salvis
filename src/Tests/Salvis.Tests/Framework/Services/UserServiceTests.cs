using System.Collections.Generic;
using System.Transactions;
using Autofac;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Salvis.Entities;
using Salvis.Framework.Services;

namespace Salvis.Tests.Framework.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void Add_PassUserObj_NotNull()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IUserService>();

                    var user = fixture.Create<User>();
                    user.Id = 0;
                    var itemSaved = service.Add(user);

                    Assert.IsTrue(itemSaved.Id > 0);
                }
            }
        }

        [Test]
        public void Add_UserName_NotNull()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IUserService>();

                    var username = fixture.Create<string>();
                    var itemSaved = service.Add(username);

                    Assert.IsNotNull(itemSaved);
                }
            }
        }

        [Test]
        public void Add_UserName_UserTypeNormal()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IUserService>();

                    var username = fixture.Create<string>();
                    var itemSaved = service.Add(username);

                    Assert.AreEqual(1, itemSaved.UserTypeId);
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
                    var service = scope.Resolve<IUserService>();

                    AddUser(scope, fixture);

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
                    var service = scope.Resolve<IUserService>();

                    var itemSaved = AddUser(scope, fixture);

                    var result = service.Get(itemSaved.Id);

                    Assert.AreEqual(itemSaved.UserName, result.UserName);
                }
            }
        }

        [Test]
        public void Get_ByUserName_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IUserService>();

                    var itemSaved = service.Add(fixture.Create<User>());
                    
                    var result = service.GetUserByUserName(itemSaved.UserName);

                    Assert.AreEqual(itemSaved.Id, result.Id);
                }
            }
        }

        [Test]
        public void Disable_ReturnNull()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {

                    var fixture = CompositionRoot.FixtureInstance;
                    var service = scope.Resolve<IUserService>();

                    var itemSaved = AddUser(scope, fixture);

                    service.Disable(itemSaved);

                    var result = service.Get(itemSaved.Id);
                    Assert.IsFalse(result.Enable, "No ha desactivado el usuario");
                }
            }
        }

        [Test]
        public void IsValidUser_AddAndDisableUser_Success()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var fixture = CompositionRoot.FixtureInstance;

                    var service = scope.Resolve<IUserService>();
                    var user = AddUser(scope, fixture);

                    service.Disable(user);

                    Assert.IsFalse(service.IsValidUser(user.UserName));
                }
            }
        }
        private User AddUser(ILifetimeScope scope, IFixture fixture)
        {
            var user = fixture.Create<User>();
            var service = scope.Resolve<IUserService>();
            return service.Add(user);
        }
    }
}