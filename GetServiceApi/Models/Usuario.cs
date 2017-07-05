using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetServiceApi.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string NomeCompleto { get; set; }

        [StringLength(150)]
        public string Status { get; set; }

        public int CidadeId { get; set; }

        public virtual Cidade Cidade { get; set; }

        [StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        public bool Profissional { get; set; }
    }
}