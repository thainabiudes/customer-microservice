using Custumers.API.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Custumers.API.Model
{
    [Table("custumer")]
    public class Custumer : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("last_name")]
        [Required]
        [StringLength(150)]
        public string LastName { get; set; }

        [Column("age")]
        [Range(1, 100)]
        public int Age { get; set; }

        [Column("gender")]
        [StringLength(50)]
        public string Gender { get; set; }

    }
}
