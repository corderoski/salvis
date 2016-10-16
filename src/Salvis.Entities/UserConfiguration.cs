    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

namespace Salvis.Entities
{

    public partial class UserConfiguration
    {
        [Key]
        [Column(Order = 1)]
        public string Category { get; set; }
        [Key]
        [Column(Order = 2)]
        public int SubCategory { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
