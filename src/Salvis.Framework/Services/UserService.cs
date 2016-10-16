using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using Salvis.Entities.Notifications;

namespace Salvis.Framework.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }


        public string CreateUserName(string seed)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(seed))
            {
                result = Regex.Match(seed, "[^@]+").Value;
            }
            else
            {
                result = $"User{DateTime.Now.Millisecond}";
            }
            return result;
        }

        public User Get(string id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> Get()
        {
            throw new NotImplementedException();
            //return _userRepository.Get();
        }

        public void Disable(User item)
        {
            item.Enable = false;
            _userRepository.Update(item);
        }

        public bool Delete(String userName)
        {
            return _userRepository.Delete(userName);
        }

        public IDictionary<int, string> GetUserRoles(string userId)
        {
            return _userRepository.GetUserRoles(userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _userRepository.GetUserByUserName(userName);
        }

        public bool IsValidateToken(string token)
        {
            return _userRepository.IsValidToken(token);
        }

        public bool IsValidUser(string userName)
        {
            return _userRepository.IsValidUser(userName);
        }

        public string GetName(string userId)
        {
            var name = _userRepository.GetNameByUserId(userId);
            if (!string.IsNullOrWhiteSpace(name)) return name;

            var userName = _userRepository.GetUserNameByUserId(userId);
            if (string.IsNullOrWhiteSpace(userName)) return null;
            var result = Regex.Match(userName, "[^@]+");
            return result.Value;
        }

        public IEnumerable<UserDeliveryInformation> GetUsersDeliveryInformation(IEnumerable<string> users)
        {
            var result = new Collection<UserDeliveryInformation>();
            foreach (var id in users.Distinct())
            {
                var user = _userRepository.Get(id);
                var item = new UserDeliveryInformation
                {
                    Id = user.Id,
                    User = GetUserValidName(user),
                    Phone = "8095551234",
                    DeviceId = "clientDeviceId-123",
                    Email = user.UserName
                };
                result.Add(item);
            }
            return result;
        }

        private static string GetUserValidName(User user)
        {
            return String.IsNullOrEmpty(user.Name) ? Regex.Match(user.UserName, "[^@]+").Value : user.Name;
        }

    }
}
