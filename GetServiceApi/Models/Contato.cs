using GetServiceApi.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetServiceApi.Models
{
    [Table("Contatos")]
    public class Contato
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("UsuarioContato")]
        public string UsuarioContatoId { get; set; }

        public virtual Usuario UsuarioContato { get; set; }

        [Required]
        public ContatoStatus Status { get; set; }
        
    }
}