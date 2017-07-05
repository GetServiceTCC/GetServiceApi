using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetServiceApi.Models
{
    [Table("SubCategorias")]
    public class SubCategoria
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Descricao { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}