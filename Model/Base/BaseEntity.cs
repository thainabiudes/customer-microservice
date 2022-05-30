using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customers.API.Model.Base
{
    public class BaseEntity
    {

        [Key]
        [Column("id")]
        public long? Id { get; set; }
    }
}
