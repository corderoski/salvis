using System;
using System.ComponentModel.DataAnnotations;

namespace Salvis.Entities.Notifications
{
    public class NotificationBoard
    {
        public NotificationBoard()
        {
            ItemsCount = 0;
            Duration = null;
            EndDate = null;
        }

        [Key]
        public Int64 Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Durations ins seconds.
        /// </summary>
        public Int32? Duration { get; set; }

        /// <summary>
        /// Count of affected items.
        /// </summary>
        public Int64 ItemsCount { get; set; }
    }
}
