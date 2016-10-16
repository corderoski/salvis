using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salvis.Entities
{
    public class Operation
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 1)]
        public long Id { get; set; }
        public long GoalId { get; set; }
        public GoalEntityType GoalTypeId { get; set; }
        public TimeInterval IntervalTypeId { get; set; }
        public DateTime InputDate { get; set; }
        public double ExpValue { get; set; }
        public double? RealValue { get; set; }
        public DateTime? EffectedDate { get; set; }
    }
}
