using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetServiceApi.Models
{
    [Table("Comentarios")]
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Descricao { get; set; }
        
        public string ProfissionalId { get; set; }

        [ForeignKey("ProfissionalId")]
        public virtual Usuario Profissional { get; set; }

        [Required]
        public int ServicoId { get; set; }

        public virtual Servico Servico { get; set; }
        
        public DateTime Data { get; set; }

        [Required]
        [Range(minimum: 1, maximum: 5)]
        public int Avaliacao { get; set; }
    }
}