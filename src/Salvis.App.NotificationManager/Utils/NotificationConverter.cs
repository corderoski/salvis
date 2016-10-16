using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salvis.App.NotificationManager.Models;
using Salvis.Entities;
using Salvis.Entities.Notifications;

namespace Salvis.App.NotificationManager.Utils
{
    class NotificationConverter
    {

        public NotificationConverter()
        {

        }

        /// <summary>
        /// Converts the passed notifications.
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        public IEnumerable<UserDeliveryMessage> Transform(IEnumerable<Notification> notifications, IEnumerable<UserDeliveryInformation> deliveryInformations)
        {
            var result = notifications.Select(i => new UserDeliveryMessage()
            {
                Types = CreateType(i),
                Deliveries = CreateDeliveries(i, deliveryInformations.Single(information => i.UserId == information.Id)),
                Content = CreateBody(),
                Date = i.ReleaseDate,
                User = deliveryInformations.Single(information => i.UserId == information.Id).User
            });
            return result;
        }

        private IEnumerable<MessageType> CreateType(Notification notification)
        {
            var types = new Collection<MessageType>();
            if (notification.IsEmailEnabled)
            {
                types.Add(MessageType.Email);
            }
            if (notification.IsSmsEnabled)
            {
                types.Add(MessageType.SMS);
            }
            if (notification.IsPushEnabled)
            {
                types.Add(MessageType.Push);
            }

            return types;
        }

        private MessageDelivery CreateDeliveries(Notification notification, UserDeliveryInformation deliveryInformation)
        {
            var destination = new MessageDelivery();
            if (notification.IsEmailEnabled)
            {
                destination.EmailContact = deliveryInformation.Email;
            }
            if (notification.IsSmsEnabled)
            {
                destination.SMSContact = deliveryInformation.Phone;
            }
            if (notification.IsPushEnabled)
            {
                destination.PushContact = deliveryInformation.DeviceId;
            }
            return destination;
        }

        private String CreateBody()
        {
            var @string = new StringBuilder();

            @string.Append("-body-");

            return @string.ToString();
        }

    }
}
