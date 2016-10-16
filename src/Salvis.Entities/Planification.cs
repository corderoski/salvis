using System.ComponentModel.DataAnnotations;

namespace Salvis.Entities
{
    public partial class Planification
    {
        [Key]
        public long UserId { get; set; }
        public long? GoalId { get; set; }
    }
}
