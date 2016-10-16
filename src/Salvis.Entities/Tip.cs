using System;
using System.ComponentModel.DataAnnotations;

namespace Salvis.Entities
{
    public class Tip
    {
        [Key]
        public Int64 Id { get; set; }

        public TipTypeEnum Type { get; set; }

        [StringLength(500)]
        public String DescriptionES { get; set; }

        [StringLength(500)]
        public String DescriptionEN { get; set; }
        
        public Boolean Enable { get; set; }
    }

    public enum TipTypeEnum
    {
        Remember = 0,
        Good = 1,
        Warning = 2,
        Danger = 3
    }

}
