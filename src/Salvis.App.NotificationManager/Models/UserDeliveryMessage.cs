using System;
using System.Collections.Generic;

namespace Salvis.App.NotificationManager.Models
{
    class UserDeliveryMessage
    {

        public IEnumerable<MessageType> Types { get; set; }
        public MessageDelivery Deliveries { get; set; }

        public String Content { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// Could be UserName or Full Name
        /// </summary>
        public String User { get; set; }

    }
}