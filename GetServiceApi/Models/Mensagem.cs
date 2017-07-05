using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetServiceApi.Models
{
    [Table("Mensagens")]
    public class Mensagem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string RemetenteId { get; set; }
        
        public virtual Usuario Remetente { get; set; }

        [Required]
        public string DestinatarioId { get; set; }

        public virtual Usuario Destinatario { get; set; }

        [Required]
        [StringLength(1000)]
        public string Texto { get; set; }

        public DateTime Data { get; set; }
    }
}