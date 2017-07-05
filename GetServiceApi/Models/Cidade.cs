using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetServiceApi.Models
{
    [Table("Cidades")]
    public class Cidade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public int EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
    }
}