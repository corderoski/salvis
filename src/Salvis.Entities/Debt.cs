using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salvis.Entities
{

    public class Debt : IGoalEntity<long>
    {
        [Key]
        public long Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public int ReasonTypeId { get; set; }
        public string UserId { get; set; }

        [NotMapped]
        public virtual Goal Goal { get; set; }
    }
}
