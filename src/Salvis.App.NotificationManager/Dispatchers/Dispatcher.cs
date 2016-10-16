using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salvis.App.NotificationManager.Models;
using Salvis.Entities;
using Salvis.Framework.Net;
using Salvis.Framework.Services;

namespace Salvis.App.NotificationManager.Dispatchers
{
    class Dispatcher
    {

        public Dispatcher()
        {
            
        }

        public Task SendPush(IEnumerable<UserDeliveryMessage> notifications)
        {
            Console.WriteLine("Sending Push to {0} recipts...", notifications.Count());
            return null;
        }

        public Task SendSMS(IEnumerable<UserDeliveryMessage> notifications)
        {
            Console.WriteLine("Sending SMS to {0} recipts...", notifications.Count());
            return null;
        }

        public Task SendEMAIL(IEnumerable<UserDeliveryMessage> notifications)
        {
            Console.WriteLine("Sending E-Mail to {0} recipts...", notifications.Count());

            var mailer = new Mailer();

            return null;
        }

        public Task SendMessage(IEnumerable<UserDeliveryMessage> notifications)
        {
            Console.WriteLine("Sending Messages (Internal) to {0} recipts...", notifications.Count());
            return null;
        }


    }
}
