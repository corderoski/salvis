using Salvis.Entities;
using System;
using System.Collections.Generic;
using Salvis.Entities.Notifications;

namespace Salvis.Framework.Services
{
    public interface IUserService : IService
    {

        string CreateUserName(string seed);
        void Disable(User item);
        string GetName(string userId);
        IEnumerable<UserDeliveryInformation> GetUsersDeliveryInformation(IEnumerable<string> users);
    }
}
