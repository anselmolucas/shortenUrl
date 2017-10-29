using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2EGroup.ShortenUrl.Models
{
    [Table("Urls")]
    public class Url : Auxiliar
    {
        [Index]
        [MaxLength(200)]
        public string LongUrl { get; set; }

        [Index]
        [MaxLength(10)]
        public string ShortUrl { get; set; }

        [Index]
        public int? UserId { get; set; }

        [Index]
        public int Hits { get; set; }
        //public virtual User User { get; set; }
    }
}
