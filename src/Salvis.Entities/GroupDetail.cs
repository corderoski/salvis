using System.ComponentModel.DataAnnotations;

namespace Salvis.Entities
{
    
    public partial class GroupDetail
    {
        [Key]
        public long Id { get; set; }
        public string MemberId { get; set; }
        public string Description { get; set; }
    }
}
