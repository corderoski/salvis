using System;

namespace Salvis.Entities.Notifications
{

    public class Notification
    {
        public long ParentId { get; set; }
        public int ParentTypeId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsSmsEnabled { get; set; }
        public bool IsEmailEnabled { get; set; }
        public bool IsPushEnabled { get; set; }
        public string UserId { get; set; }
    }
}
