using Salvis.Entities;
using System.Collections.Generic;

namespace Salvis.DataLayer.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        
        void AddRole(string userId, User.Roles role);

        bool Delete(string userName);
        
        /// <summary>
        /// Gets all the users by role or just the roles one.
        /// If greater then zero, will return an user with his roles, otherwise, all roles.
        /// </summary>
        /// <param name="userId">An int indicating a single user roles or not.</param>
        /// <returns>users with roles, otherwise, roles.</returns>
        IDictionary<int, string> GetUserRoles(string userId);

        User GetUserByUserName(string userName);

        User Get(string id);

        string GetNameByUserId(string userId);

        string GetUserNameByUserId(string userId);
        /// <summary>
        /// Checks for a valid token.
        /// </summary>
        /// <param name="token">string containing the token to validate.</param>
        /// <returns></returns>
        bool IsValidToken(string token);

        bool IsValidUser(string userName);

        void Update(User item);
    }
}
