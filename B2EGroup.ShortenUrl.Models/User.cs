using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2EGroup.ShortenUrl.Models
{
    [Table("Users")]
    public class User : Auxiliar
    {
        [Index]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
