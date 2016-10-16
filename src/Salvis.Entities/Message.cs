using System;

namespace Salvis.Entities
{
    
    public partial class Message
    {

        public long Id { get; set; }
        public long ParentId { get; set; }
        public int ParentTypeId { get; set; }
        public DateTime InputDate { get; set; }
        public int TypeId { get; set; }
        public int State { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string FromUserId { get; set; }
    }
}
