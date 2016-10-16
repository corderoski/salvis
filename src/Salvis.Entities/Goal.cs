using Salvis.Entities.Notifications;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salvis.Entities
{
    
    public partial class Goal
    {
        [Key]
        [Column(Order = 1)]
        public long ParentId { get; set; }
        [Key]
        [Column(Order = 2)]
        public GoalEntityType ParentTypeId { get; set; }
        public int TypeId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double Amount { get; set; }

        [NotMapped]
        public IEnumerable<Notification> Notifications { get; set; }

        public virtual IEnumerable<Operation> OperationDetails { get; set; }

    }
}
