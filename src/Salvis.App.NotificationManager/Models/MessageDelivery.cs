using System;

namespace Salvis.App.NotificationManager.Models
{
    struct MessageDelivery
    {
        public String EmailContact { get; set; }
        public String PushContact { get; set; }
        public String SMSContact { get; set; }
    }
}
