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
    public class UserRepositoryTests
    {

        [Test]
        public void Add_ManualUser_SavedUser()
        {
           using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {

                    var repository = scope.Resolve<IUserRepository>();
                    var user = new User
                    {
                        Description = "Description000",
                        Enable = false,
                        Name = "Name000",
                        UserName = "UserName000",
                        UserTypeId = 1
                    };

                    var result = repository.Add(user);

                    Assert.IsTrue(user.Id == result.Id, "Must be not the same!");
                }
            }
        }

        [Test]
        public void Add_FilledUser_SavedUser()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {

                    var repository = scope.Resolve<IUserRepository>();
                    var user = fixture.Create<User>();

                    var result = repository.Add(user);

                    Assert.IsTrue(user.Id == result.Id, "Must be not the same!");
                }
            }
        }


        [Test]
        public void Get_AddedUser_ValidUser()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //
                    var repository = scope.Resolve<IUserRepository>();
                    var user = fixture.Create<User>();

                    user = repository.Add(user);
                    //
                    var result = repository.Get(user.Id);
                    //
                    Assert.IsTrue((result.Id >= 0) && (result.Id == user.Id), "Must be the same.");
                }
            }

        }

        [Test]
        public void GetUserRoles_RequestRoles()
        {
            using (var trans = new TransactionScope())
            {
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //
                    var repository = scope.Resolve<IUserRepository>();
                    //
                    var result = repository.GetUserRoles();
                    //
                    Assert.IsNotEmpty(result);
                }
            }
        }

        [Test]
        public void GetUserRoles_AddUserAndRequestRoles()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    //
                    var repository = scope.Resolve<IUserRepository>();
                    var user = fixture.Create<User>();
                    repository.Add(user);

                    var result = repository.GetUserRoles(user.Id);
                    //
                    Assert.IsEmpty(result, "Users must not have any role.");
                }
            }
        }

        [Test]
        public void Update_GetRandomUserAndDisableIt_ReturnsDIsabledUser()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IUserRepository>();

                    var user = repository.Get().FirstOrDefault(u => u.Enable);

                    if (user != null)
                    {
                        user.Enable = false;
                        repository.Update(user);
                        user = repository.Get(user.Id);
                    }
                    Assert.IsFalse(user != null && user.Enable);
                }
            }
        }   
        
        [Test]
        public void Delete_AddUserAndDelete_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IUserRepository>();

                    var user = fixture.Create<User>();
                    repository.Add(user);
                    repository.AddRole(user.Id, User.Roles.Normal);

                    //
                    var exec = repository.Delete(user.UserName);

                    var result = repository.Get(user.Id);
                    var result2 = repository.GetUserByUserName(user.UserName);

                    //
                    Assert.IsTrue(exec);
                    Assert.IsNull(result);
                    Assert.IsNull(result2);
                }
            }
        } 

        [Test]
        public void DeleteByUserName_DeleteExistingUser_Success()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                    var repository = scope.Resolve<IUserRepository>();

                    var user = repository.Get().FirstOrDefault(u => u.Enable);
                    var exec = repository.Delete(user.UserName);
                    var result = repository.Get(user.Id);
                    //
                    Assert.IsTrue(exec);
                    Assert.IsNull(result);
                }
            }
        }


    }

}
