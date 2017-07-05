using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetServiceApi.Models
{
    [Table("Estados")]
    public class Estado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(2)]
        public string Uf { get; set; }
    }
}