using Dapper;
using Salvis.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Salvis.DataLayer.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(IDbConnection connection)
            : base(connection)
        {

        }

        public void AddRole(string userId, User.Roles role)
        {
            throw new NotImplementedException();
            var r = Connection.Execute(
                "INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId],[RoleId]) VALUES (@userId, @role)",
                new { userId, role });
        }

        public bool Delete(string userName)
        {
            var id = Connection.ExecuteScalar<int>(
                String.Format("SELECT [Id] FROM {0} WHERE UserName = @userName", EntityTableSchema),
                new { userName });

            if (id <= 0) return false;

            string deleteQuery = ""
                  + "DELETE FROM dbo.[webpages_Membership] WHERE UserId = @id; "
                  + "DELETE FROM dbo.[webpages_OAuthMembership] WHERE UserId = @id; "
                  + "DELETE FROM dbo.[webpages_UsersInRoles] WHERE UserId = @id; "
                  + String.Format("DELETE FROM {0} WHERE Id = @id; ", EntityTableSchema);

            var r = Connection.Execute(deleteQuery, new { id });
            //why 2? Users + webpages_UsersInRoles
            return r >= 2;
        }

        public IDictionary<int, string> GetUserRoles(string userId)
        {
            throw new NotImplementedException();
            //var result = userId > 0 ?
            //                       Connection.Query("SELECT rol.RoleId, r.RoleName FROM [dbo].[webpages_UsersInRoles] rol left join dbo.webpages_Roles r ON rol.RoleId = r.RoleId WHERE rol.UserId = @id", new { id = userId }) : 
            //                       Connection.Query("SELECT r.RoleId, r.RoleName FROM dbo.webpages_Roles r");

            //return result.ToDictionary<dynamic, int, string>(item => item.RoleId, item => item.RoleName);
        }

        public User GetUserByUserName(string userName)
        {
            var entity = Connection.Query<User>(String.Format("SELECT * FROM {0} WHERE UserName = @user", EntityTableSchema), new { user = userName });
            return entity.SingleOrDefault();
        }

        public string GetNameByUserId(string userId)
        {
            var sql = string.Format("SELECT Name FROM {0} WHERE Id = @userId", EntityTableSchema);
            return Connection.ExecuteScalar<string>(sql, new { userId });
        }

        public string GetUserNameByUserId(string userId)
        {
            var sql = string.Format("SELECT UserName FROM {0} WHERE Id = @userId", EntityTableSchema);
            return Connection.ExecuteScalar<string>(sql, new { userId });
        }

        public bool IsValidToken(string token)
        {
            const string sql =
                "SELECT UserId FROM [dbo].[webpages_Membership] WHERE [PasswordVerificationToken] = @token";
            var userId = Connection.ExecuteScalar<int>(sql, new { token });
            return userId > 0;
        }

        public bool IsValidUser(string userName)
        {
            string sql = String.Format("SELECT Id FROM {0} WHERE Enable = 1 AND UserName = @id", EntityTableSchema);
            var userId = Connection.ExecuteScalar<int>(sql, new { id = userName });
            return userId > 0;
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
