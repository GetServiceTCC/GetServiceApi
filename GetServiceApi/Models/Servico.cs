using GetServiceApi.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetServiceApi.Models
{
    [Table("Servicos")]
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(150)]
        public string Sobre { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public int SubCategoriaId { get; set; }

        public virtual SubCategoria SubCategoria { get; set; }

        public TipoValor TipoValor { get; set; }

        public double Valor { get; set; }
        
        public string ProfissionalId { get; set; }
        
        public virtual Usuario Profissional { get; set; }
    }
}