using System.ComponentModel.DataAnnotations;

namespace Salvis.Entities
{

    public class User
    {
        [Key]
        [StringLength(128)]
        public string Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public bool HasRegisteredDevice { get; set; }
        public bool Enable { get; set; }

        public enum Roles
        {
            Normal = 1,
            Membership = 2,
            Administrator = 3
        }

    }

}
