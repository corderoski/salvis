using System.ComponentModel.DataAnnotations;

namespace Salvis.Entities
{
    
    public partial class Group
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
