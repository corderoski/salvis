using System;

namespace Salvis.Entities.Notifications
{
    /// <summary>
    /// Represents the contact and delivery information for an user.
    /// </summary>
    public class UserDeliveryInformation
    {

        public string Id { get; set; }

        public String User { get; set; }

        /// <summary>
        /// Represents the device-uniqueId in the system.
        /// </summary>
        public String DeviceId { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public String Phone { get; set; }

        public String Email { get; set; }

    }
}
